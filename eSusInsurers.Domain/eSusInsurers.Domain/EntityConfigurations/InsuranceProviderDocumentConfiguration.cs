﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eSusInsurers.Domain.Models.Configurations
{
    public partial class InsuranceProviderDocumentConfiguration : IEntityTypeConfiguration<InsuranceProviderDocument>
    {
        public void Configure(EntityTypeBuilder<InsuranceProviderDocument> entity)
        {
            entity.Property(e => e.Id).HasColumnName("InsuranceProviderDocumentId");
            entity.HasKey(e => e.Id).HasName("PK__Insuranc__AE9929CDFCAC0CB0");

            entity.Property(e => e.CreatedBy)
            .HasMaxLength(100)
            .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
            entity.Property(e => e.DocumentName)
            .HasMaxLength(250)
            .IsUnicode(false);
            entity.Property(e => e.DocumentPath).IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy)
            .HasMaxLength(100)
            .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.InsuranceProvider).WithMany(p => p.InsuranceProviderDocuments)
            .HasForeignKey(d => d.InsuranceProviderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_InsuranceProviderDocuments_InsuranceProviderId");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<InsuranceProviderDocument> entity);
    }
}
