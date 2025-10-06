using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Configurations.Read
{
    public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
    {
        public void Configure(EntityTypeBuilder<VolunteerDto> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                .HasColumnName("id");

            builder.Property(v => v.Surname)
                .HasColumnName("surname");

            builder.Property(v => v.FirstName)
                .HasColumnName("firstname");

            builder.Property(v => v.Patronymic)
                .HasColumnName("patronymic");

            builder.Property(v => v.Email)
                .HasColumnName("email");

            builder.Property(v => v.PhoneNumber)
                .HasColumnName("phone_number");

            builder.Property(v => v.Description)
                .HasColumnName("description");

            builder.Property(v => v.SocialNetworks)
                .HasColumnName("social_networks")
                .HasColumnType("jsonb")
                .HasConversion(d => JsonSerializer.Serialize(d, JsonSerializerOptions.Default),
                    d => JsonSerializer.Deserialize<SocialNetworksDto[]>(d, (JsonSerializerOptions.Default))!);
        }
    }
}
