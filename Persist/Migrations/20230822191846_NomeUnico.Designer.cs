﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persist.Context;

#nullable disable

namespace Persist.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230822191846_NomeUnico")]
    partial class NomeUnico
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dominio.models.Tarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Custo")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("DataLimite")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("OrdemApresentacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.HasIndex("OrdemApresentacao")
                        .IsUnique();

                    b.ToTable("tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
