using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dome.Domain.Settings;

internal sealed class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
{
    public void Configure(EntityTypeBuilder<SystemSetting> builder)
    {
        builder.ToTable("SystemSettings");
        builder.HasKey(setting => setting.Key);
        builder.Property(setting => setting.Key)
            .HasMaxLength(128);
        builder.Property(setting => setting.Value)
            .IsRequired();
    }
}
