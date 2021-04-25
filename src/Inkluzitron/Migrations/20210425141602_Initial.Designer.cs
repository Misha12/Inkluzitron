﻿// <auto-generated />
using System;
using Inkluzitron.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inkluzitron.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210425141602_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Inkluzitron.Data.BaseTestResult", b =>
                {
                    b.Property<Guid>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("SubmittedAt")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("SubmittedById")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubmittedByName")
                        .HasColumnType("TEXT");

                    b.HasKey("ResultId");

                    b.ToTable("TestResults");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseTestResult");
                });

            modelBuilder.Entity("Inkluzitron.Data.BaseTestResultItem", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TestResultResultId")
                        .HasColumnType("TEXT");

                    b.HasKey("ItemId");

                    b.HasIndex("TestResultResultId");

                    b.ToTable("TestResultItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseTestResultItem");
                });

            modelBuilder.Entity("Inkluzitron.Data.BorgTestResult", b =>
                {
                    b.HasBaseType("Inkluzitron.Data.BaseTestResult");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("BorgTestResult");
                });

            modelBuilder.Entity("Inkluzitron.Data.DoubleTestResultItem", b =>
                {
                    b.HasBaseType("Inkluzitron.Data.BaseTestResultItem");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasDiscriminator().HasValue("DoubleTestResultItem");
                });

            modelBuilder.Entity("Inkluzitron.Data.BaseTestResultItem", b =>
                {
                    b.HasOne("Inkluzitron.Data.BaseTestResult", "TestResult")
                        .WithMany("Items")
                        .HasForeignKey("TestResultResultId");

                    b.Navigation("TestResult");
                });

            modelBuilder.Entity("Inkluzitron.Data.BaseTestResult", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
