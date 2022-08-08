﻿// <auto-generated />
using System;
using Challenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Challenge.Infrastructure.Migrations
{
    [DbContext(typeof(FinanceContext))]
    partial class FinanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Challenge.Domain.Despesas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("Despesa_Data");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("Descrição");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1000000000)
                        .HasColumnType("DECIMAL(7,3)")
                        .HasDefaultValue(0m)
                        .HasColumnName("Valor");

                    b.HasKey("Id");

                    b.ToTable("Despesas", (string)null);
                });

            modelBuilder.Entity("Challenge.Domain.Receitas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("Receita_Data");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("Descrição");

                    b.Property<decimal>("Valor")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1000000000)
                        .HasColumnType("DECIMAL(7,3)")
                        .HasDefaultValue(0m)
                        .HasColumnName("Valor");

                    b.HasKey("Id");

                    b.ToTable("Receitas", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}