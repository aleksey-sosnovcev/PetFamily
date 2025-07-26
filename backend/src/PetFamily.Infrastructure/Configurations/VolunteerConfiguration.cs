using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                    id => id.Value,
                    value => VolunteerId.Create(value)); //конвертация id

            builder.ComplexProperty(v => v.FullName, tb =>
            {
                tb.Property(f => f.Surname)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("surname");

                tb.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("firstname");

                tb.Property(f => f.Patronymic)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("patronymic");
            });

            builder.ComplexProperty(v => v.Email, tb =>
            {
                tb.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_EMAIL_LENGTH)
                .HasColumnName("email");
            });

            builder.ComplexProperty(v => v.Description, tb =>
            {
                tb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_DESCRIPTION_LENGTH)
                .HasColumnName("description");
            });

            builder.ComplexProperty(v => v.PhoneNumber, tb =>
            {
                tb.Property(pn => pn.Value)
                .IsRequired()
                .HasMaxLength(PhoneNumber.MAX_LENGTH)
                .HasColumnName("phone_number");
            });

            builder.OwnsMany(v => v.SocialNetworks, sb =>
            {
                sb.ToJson("social_networks");

                sb.Property(v => v.Link)
                .IsRequired()
                .HasColumnName("social_network_link");

                sb.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("social_network_name");
            });

            builder.ComplexProperty(v => v.Details, tb =>
            {
                tb.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_NAME_LENGTH)
                .HasColumnName("name");

                tb.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_DESCRIPTION_LENGTH)
                .HasColumnName("details_description");
            });

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(v => v.Pets).AutoInclude();
        }
    }
}
