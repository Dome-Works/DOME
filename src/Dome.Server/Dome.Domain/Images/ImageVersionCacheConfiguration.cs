using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dome.Domain.Images;

internal sealed class ImageVersionCacheConfiguration : IEntityTypeConfiguration<ImageVersionCache>
{
    public void Configure(EntityTypeBuilder<ImageVersionCache> builder)
    {
        builder.ToTable("ImageVersionCaches");
        builder.HasKey(entry => entry.Id);
        builder.Property(entry => entry.ImageReference)
            .HasMaxLength(512)
            .IsRequired();
        builder.HasIndex(entry => entry.ImageReference)
            .IsUnique();
        builder.Property(entry => entry.RemoteDigest)
            .HasMaxLength(128);
        builder.Property(entry => entry.RemoteTags)
            .IsRequired();
        builder.Property(entry => entry.LastError)
            .HasMaxLength(1024);
        builder.Property(entry => entry.CheckedAt)
            .IsRequired();
    }
}
