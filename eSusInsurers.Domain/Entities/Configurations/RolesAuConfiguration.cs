﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities.Configurations
{
    public partial class RolesAuConfiguration : IEntityTypeConfiguration<RolesAu>
    {
        public void Configure(EntityTypeBuilder<RolesAu> entity)
        {
            entity.Property(x => x.Id)
                  .HasColumnName("HistoryRowId");

            entity.HasKey(e => e.Id).HasName("PK__Roles_AU__5F38963857F8F541");

            entity.ToTable("Roles_AU");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HistoryCreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<RolesAu> entity);
    }
}