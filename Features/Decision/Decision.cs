﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ProjectManagementSystem.Features.Decision
{
    public class Decision
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }

        public Decision()
        {
            Id = Guid.NewGuid();
        }
    }

    internal class DecisionEntityConfiguration : IEntityTypeConfiguration<Decision>
    {
        public void Configure(EntityTypeBuilder<Decision> decision)
        {
            decision.ToTable("Decision");

            decision.HasKey(x => x.Id);
            decision.Property(x => x.Name);
        }
    }
}