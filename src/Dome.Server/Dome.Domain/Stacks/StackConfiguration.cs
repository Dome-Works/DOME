using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dome.Domain.Stacks;

internal sealed class StackConfiguration : IEntityTypeConfiguration<Stack>
{
    public void Configure(EntityTypeBuilder<Stack> builder)
    {
        builder.ToTable("Stacks");
        builder.HasKey(stack => stack.Id);
        builder.Property(stack => stack.ComposeName)
            .HasMaxLength(128)
            .IsRequired();
        builder.Property(stack => stack.ProjectName)
            .HasMaxLength(128)
            .IsRequired();
        builder.HasIndex(stack => new { stack.SocketId, stack.ProjectName })
            .IsUnique();
        builder.Property(stack => stack.ComposeYaml)
            .IsRequired();
        builder.Property(stack => stack.CreatedAt)
            .IsRequired();
        builder.Property(stack => stack.UpdatedAt)
            .IsRequired();
        builder.HasOne(stack => stack.Socket)
            .WithMany()
            .HasForeignKey(stack => stack.SocketId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
