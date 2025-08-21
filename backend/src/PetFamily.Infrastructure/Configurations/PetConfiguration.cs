using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetId.Create(value)); //конвертация id

            builder.ComplexProperty(p => p.Name, tb =>
            {
                tb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Name.MAX_LENGTH)
                .HasColumnName("name");
            });

            builder.ComplexProperty(p => p.SpeciasAndBreed, tb =>
            {
                tb.Property(sb => sb.SpeciesId)
                .IsRequired()
                .HasColumnName("sptcies_id")
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value));

                tb.Property(sb => sb.BreedId)
                .IsRequired()
                .HasColumnName("breed_id")
                .HasConversion(
                    id => id.Value,
                    value => BreedId.Create(value));
            });

            builder.ComplexProperty(p => p.Description, tb =>
            {
                tb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_DESCRIPTION_LENGTH)
                .HasColumnName("description");
            });

            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("color");

            builder.ComplexProperty(p => p.InfoHealth, tb =>
            {
                tb.Property(ih => ih.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_DESCRIPTION_LENGTH)
                .HasColumnName("info_health");
            });

            builder.ComplexProperty(p => p.Address, tb =>
            {
                tb.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("city");

                tb.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("street");

                tb.Property(a => a.HouseNumber)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("house_number");

                tb.Property(a => a.Apartment)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("apartment");

                tb.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("postal_code");
            });

            builder.Property(p => p.Weight)
                .IsRequired()
                .HasColumnName("weight");

            builder.Property(p => p.Growth)
                .IsRequired()
                .HasColumnName("growth");

            builder.ComplexProperty(p => p.PhoneNumber, tb =>
            {
                tb.Property(pn => pn.Value)
                .IsRequired()
                .HasMaxLength(PhoneNumber.MAX_LENGTH)
                .HasColumnName("phone_number");
            });

            builder.Property(p => p.Castration)
                .IsRequired()
                .HasColumnName("castration");

            builder.Property(p => p.BirthDate)
                .IsRequired()
                .HasColumnName("birth_date");

            builder.Property(p => p.Vaccination)
                .IsRequired()
                .HasColumnName("vaccination");

            builder.Property(p => p.Status)
                .IsRequired()
                .HasColumnName("status");

            builder.ComplexProperty(p => p.Details, tb =>
            {
                tb.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("details_name");

                tb.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_DESCRIPTION_LENGTH)
                .HasColumnName("details_description");
            });

            builder.Property(p => p.CreateDate)
                .IsRequired()
                .HasColumnName("create_date");

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");

            builder.OwnsMany(p => p.Files, pb =>
            {
                pb.ToJson("files");

                pb.Property(f => f.PathToStorage)
                 .IsRequired(false)
                 .HasColumnName("file");
            });

            builder.ComplexProperty(p => p.Position, tb =>
            {
                tb.Property(s => s.Value)
                .IsRequired()
                .HasColumnName("serial_number");
            });
        }
    }

}
