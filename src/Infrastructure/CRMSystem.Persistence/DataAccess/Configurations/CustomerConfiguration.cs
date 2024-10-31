using System;
using CRMSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.Persistence.DataAccess.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired();

        builder.Property(u => u.Surname)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired();

        builder.Property(u => u.PhoneNumber)
            .IsRequired();

        builder.HasIndex(u => u.Email);

        builder.HasOne(u => u.Creator)
            .WithMany()
            .HasForeignKey(u => u.CreatorId);

        builder.HasOne(u => u.Modifier)
            .WithMany()
            .HasForeignKey(u => u.ModifierId);

        // Audit fields
        builder.Property(u => u.CreateDate).IsRequired(false);
        builder.Property(u => u.ModifyDate).IsRequired(false);
        builder.Property(u => u.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(d => d.IsDeleted == false);
    }
}