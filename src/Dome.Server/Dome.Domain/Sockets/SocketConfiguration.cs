using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dome.Domain.Sockets;

internal sealed class SocketConfiguration : IEntityTypeConfiguration<Socket>
{
    public void Configure(EntityTypeBuilder<Socket> builder)
    {
        builder.ToTable("Sockets");
        builder.HasKey(socket => socket.Id);
        builder.Property(socket => socket.Name)
            .HasMaxLength(128)
            .IsRequired();
        builder.HasIndex(socket => socket.Name)
            .IsUnique();
        builder.Property(socket => socket.Address)
            .HasMaxLength(2048)
            .IsRequired();
        builder.Property(socket => socket.ProtectedApiKey)
            .HasMaxLength(1024)
            .IsRequired()
            .HasDefaultValue(string.Empty);
        builder.Property(socket => socket.CertificateFingerprint)
            .HasMaxLength(64);
        builder.Property(socket => socket.CreatedAt)
            .IsRequired();
    }
}
