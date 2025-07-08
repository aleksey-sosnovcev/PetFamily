using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Configurations
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value)); //конвертация id

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("name");

            builder.HasMany(v => v.Breeds)
                .WithOne()
                .HasForeignKey("species_id");

        }
    }
}
