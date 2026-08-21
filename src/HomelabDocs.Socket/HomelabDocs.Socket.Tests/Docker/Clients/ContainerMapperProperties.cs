using Docker.DotNet.Models;
using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;
using HomelabDocs.Socket.Api.Docker.Clients;

namespace HomelabDocs.Socket.Tests.Docker.Clients;

public sealed class ContainerMapperProperties
{
    private const string ComposeProjectLabel = "com.docker.compose.project";
    private const int ShortIdLength = 12;

    [Property]
    public bool ShortenId_never_exceeds_short_id_length(string? containerId)
    {
        var shortened = ContainerMapper.ShortenId(containerId!);
        return shortened.Length <= ShortIdLength;
    }

    [Property]
    public Property ShortenId_whitespace_or_empty_returns_empty()
    {
        return Prop.ForAll(
            WhitespaceOrEmptyStringGen().ToArbitrary(),
            static input => ContainerMapper.ShortenId(input) == string.Empty);
    }

    [Property]
    public Property ShortenId_is_stable_prefix_of_non_whitespace_id()
    {
        return Prop.ForAll(
            NonWhitespaceString().ToArbitrary(),
            static containerId =>
            {
                var shortened = ContainerMapper.ShortenId(containerId);
                var expected = containerId.Length <= ShortIdLength
                    ? containerId
                    : containerId[..ShortIdLength];

                return shortened == expected
                       && containerId.StartsWith(shortened, StringComparison.Ordinal);
            });
    }

    [Property]
    public Property ResolveName_uses_first_usable_name_without_leading_slashes()
    {
        Gen<NameResolutionSample> samples =
            from usableName in NonWhitespaceString()
            from prefixCount in Gen.Choose(0, 5)
            from suffixCount in Gen.Choose(0, 5)
            from suffix in Gen.ListOf(ArbMap.Default.GeneratorFor<string>(), suffixCount)
            select new NameResolutionSample(usableName, prefixCount, suffix);

        return Prop.ForAll(
            samples.ToArbitrary(),
            static sample =>
            {
                var names = Enumerable
                    .Repeat("   ", sample.PrefixCount)
                    .Append(sample.UsableName)
                    .Concat(sample.Suffix)
                    .ToList();

                var resolved = ContainerMapper.ResolveName(names, "abcdef0123456789");
                return resolved == sample.UsableName.TrimStart('/');
            });
    }

    [Property]
    public Property ResolveName_falls_back_to_shortened_id_when_names_unusable()
    {
        Gen<FallbackNameSample> samples =
            from names in Gen.OneOf(
                Gen.Constant<IList<string>?>(null),
                Gen.Constant<IList<string>?>(Array.Empty<string>()),
                Gen.NonEmptyListOf(WhitespaceOrEmptyStringGen()).Select(static list => (IList<string>?)list))
            from containerId in ArbMap.Default.GeneratorFor<string?>()
            select new FallbackNameSample(names, containerId);

        return Prop.ForAll(
            samples.ToArbitrary(),
            static sample =>
            {
                var id = sample.ContainerId ?? string.Empty;
                return ContainerMapper.ResolveName(sample.Names, id) == ContainerMapper.ShortenId(id);
            });
    }

    [Property]
    public bool ResolveStack_null_labels_returns_null()
        => ContainerMapper.ResolveStack(null) is null;

    [Property]
    public Property ResolveStack_missing_or_blank_compose_project_returns_null()
    {
        return Prop.ForAll(
            LabelsWithoutUsableComposeProject().ToArbitrary(),
            static labels => ContainerMapper.ResolveStack(labels) is null);
    }

    [Property]
    public Property ResolveStack_returns_trimmed_compose_project()
    {
        Gen<StackSample> samples =
            from project in NonWhitespaceString()
            from leadingSpaces in Gen.Choose(0, 3)
            from trailingSpaces in Gen.Choose(0, 3)
            from extraLabels in ArbMap.Default.GeneratorFor<Dictionary<string, string>>()
            select new StackSample(project, leadingSpaces, trailingSpaces, extraLabels);

        return Prop.ForAll(
            samples.ToArbitrary(),
            static sample =>
            {
                var padded = new string(' ', sample.LeadingSpaces)
                             + sample.Project
                             + new string(' ', sample.TrailingSpaces);

                var labels = new Dictionary<string, string>(sample.ExtraLabels, StringComparer.Ordinal);
                labels[ComposeProjectLabel] = padded;

                return ContainerMapper.ResolveStack(labels) == sample.Project.Trim();
            });
    }

    [Property]
    public Property Map_projects_fields_consistently()
    {
        return Prop.ForAll(
            ContainerListResponseGen().ToArbitrary(),
            static container =>
            {
                var mapped = ContainerMapper.Map(container);
                var id = container.ID ?? string.Empty;

                return mapped.Id == id
                       && mapped.Name == ContainerMapper.ResolveName(container.Names, id)
                       && mapped.State == (container.State ?? string.Empty)
                       && mapped.Stack == ContainerMapper.ResolveStack(container.Labels);
            });
    }

    [Fact]
    public void Map_null_container_throws()
    {
        Assert.Throws<ArgumentNullException>(static () => ContainerMapper.Map(null!));
    }

    private static Gen<string> NonWhitespaceString()
        => ArbMap.Default.GeneratorFor<string>()
            .Where(static value => !string.IsNullOrWhiteSpace(value));

    private static Gen<string> WhitespaceOrEmptyStringGen()
        => Gen.Elements(string.Empty, " ", "  ", "\t", "\n", " \t\n ");

    private static Gen<IDictionary<string, string>?> LabelsWithoutUsableComposeProject()
        => Gen.OneOf(
            Gen.Constant<IDictionary<string, string>?>(null),
            ArbMap.Default.GeneratorFor<Dictionary<string, string>>()
                .Select(static labels =>
                {
                    labels.Remove(ComposeProjectLabel);
                    return (IDictionary<string, string>?)labels;
                }),
            WhitespaceOrEmptyStringGen()
                .Select(static blank => (IDictionary<string, string>?)new Dictionary<string, string>
                {
                    [ComposeProjectLabel] = blank,
                }));

    private static Gen<ContainerListResponse> ContainerListResponseGen()
        => from id in ArbMap.Default.GeneratorFor<string?>()
           from state in ArbMap.Default.GeneratorFor<string?>()
           from names in Gen.OneOf(
               Gen.Constant<IList<string>?>(null),
               Gen.ListOf(ArbMap.Default.GeneratorFor<string>())
                   .Select(static list => (IList<string>?)list))
           from labels in Gen.OneOf(
               Gen.Constant<IDictionary<string, string>?>(null),
               ArbMap.Default.GeneratorFor<Dictionary<string, string>>()
                   .Select(static dict => (IDictionary<string, string>?)dict))
           select new ContainerListResponse
           {
               ID = id,
               State = state,
               Names = names,
               Labels = labels,
           };

    private sealed record NameResolutionSample(
        string UsableName,
        int PrefixCount,
        IList<string> Suffix);

    private sealed record FallbackNameSample(
        IList<string>? Names,
        string? ContainerId);

    private sealed record StackSample(
        string Project,
        int LeadingSpaces,
        int TrailingSpaces,
        Dictionary<string, string> ExtraLabels);
}
