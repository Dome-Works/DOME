using Docker.DotNet.Models;
using FsCheck;
using FsCheck.Fluent;
using Dome.Socket.Api.Docker.Clients;

namespace Dome.Socket.Tests.Docker.Clients;

public sealed class ContainerMapperProperties
{
    private const string ComposeProjectLabel = "com.docker.compose.project";
    private const int ShortIdLength = 12;

    [Fact]
    public void ShortenId_never_exceeds_short_id_length()
    {
        Check.QuickThrowOnFailure(
            Prop.ForAll(
                ArbMap.Default.GeneratorFor<string?>().ToArbitrary(),
                static containerId => ContainerMapper.ShortenId(containerId!).Length <= ShortIdLength));
    }

    [Fact]
    public void ShortenId_whitespace_or_empty_returns_empty()
    {
        Check.QuickThrowOnFailure(
            Prop.ForAll(
                WhitespaceOrEmptyStringGen().ToArbitrary(),
                static input => ContainerMapper.ShortenId(input) == string.Empty));
    }

    [Fact]
    public void ShortenId_is_stable_prefix_of_non_whitespace_id()
    {
        Check.QuickThrowOnFailure(
            Prop.ForAll(
                NonWhitespaceString().ToArbitrary(),
                static containerId =>
                {
                    var shortened = ContainerMapper.ShortenId(containerId);
                    var expected = containerId.Length <= ShortIdLength
                        ? containerId
                        : containerId[..ShortIdLength];

                    return shortened == expected
                           && containerId.StartsWith(shortened, StringComparison.Ordinal);
                }));
    }

    [Fact]
    public void ResolveName_uses_first_usable_name_without_leading_slashes()
    {
        Gen<NameResolutionSample> samples =
            from usableName in NonWhitespaceString()
            from prefixCount in Gen.Choose(0, 5)
            from suffixCount in Gen.Choose(0, 5)
            from suffix in Gen.ListOf(ArbMap.Default.GeneratorFor<string>(), suffixCount)
            select new NameResolutionSample(usableName, prefixCount, suffix);

        Check.QuickThrowOnFailure(
            Prop.ForAll(
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
                }));
    }

    [Fact]
    public void ResolveName_falls_back_to_shortened_id_when_names_unusable()
    {
        Gen<FallbackNameSample> samples =
            from names in Gen.OneOf(
                Gen.Constant<IList<string>?>(null),
                Gen.Constant<IList<string>?>(Array.Empty<string>()),
                Gen.NonEmptyListOf(WhitespaceOrEmptyStringGen()).Select(static list => (IList<string>?)list))
            from containerId in ArbMap.Default.GeneratorFor<string?>()
            select new FallbackNameSample(names, containerId);

        Check.QuickThrowOnFailure(
            Prop.ForAll(
                samples.ToArbitrary(),
                static sample =>
                {
                    var id = sample.ContainerId ?? string.Empty;
                    return ContainerMapper.ResolveName(sample.Names, id) == ContainerMapper.ShortenId(id);
                }));
    }

    [Fact]
    public void ResolveStack_null_labels_returns_null()
        => Assert.Null(ContainerMapper.ResolveStack(null));

    [Fact]
    public void ResolveStack_missing_or_blank_compose_project_returns_null()
    {
        Check.QuickThrowOnFailure(
            Prop.ForAll(
                LabelsWithoutUsableComposeProject().ToArbitrary(),
                static labels => ContainerMapper.ResolveStack(labels) is null));
    }

    [Fact]
    public void ResolveStack_returns_trimmed_compose_project()
    {
        Gen<StackSample> samples =
            from project in NonWhitespaceString()
            from leadingSpaces in Gen.Choose(0, 3)
            from trailingSpaces in Gen.Choose(0, 3)
            from extraLabels in ArbMap.Default.GeneratorFor<Dictionary<string, string>>()
            select new StackSample(project, leadingSpaces, trailingSpaces, extraLabels);

        Check.QuickThrowOnFailure(
            Prop.ForAll(
                samples.ToArbitrary(),
                static sample =>
                {
                    var padded = new string(' ', sample.LeadingSpaces)
                                 + sample.Project
                                 + new string(' ', sample.TrailingSpaces);

                    var labels = new Dictionary<string, string>(sample.ExtraLabels, StringComparer.Ordinal);
                    labels[ComposeProjectLabel] = padded;

                    return ContainerMapper.ResolveStack(labels) == sample.Project.Trim();
                }));
    }

    [Fact]
    public void Map_projects_fields_consistently()
    {
        Check.QuickThrowOnFailure(
            Prop.ForAll(
                ContainerListResponseGen().ToArbitrary(),
                static container =>
                {
                    var mapped = ContainerMapper.Map(container);
                    var id = container.ID ?? string.Empty;

                    return mapped.Id == id
                           && mapped.Name == ContainerMapper.ResolveName(container.Names, id)
                           && mapped.State == (container.State ?? string.Empty)
                           && mapped.Stack == ContainerMapper.ResolveStack(container.Labels);
                }));
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

}
