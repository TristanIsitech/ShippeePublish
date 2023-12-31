﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShippeeAPI.Context;

#nullable disable

namespace ShippeeAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231017100534_firstmigration")]
    partial class firstmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShippeeAPI.Annoucement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<int?>("id_diplome")
                        .HasColumnType("int");

                    b.Property<int?>("id_naf_division")
                        .HasColumnType("int");

                    b.Property<int?>("id_status")
                        .HasColumnType("int");

                    b.Property<int?>("id_type")
                        .HasColumnType("int");

                    b.Property<int?>("id_user")
                        .HasColumnType("int");

                    b.Property<DateTime?>("publish_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("title")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("id_diplome");

                    b.HasIndex("id_naf_division");

                    b.HasIndex("id_status");

                    b.HasIndex("id_type");

                    b.HasIndex("id_user");

                    b.ToTable("Annoucements");
                });

            modelBuilder.Entity("ShippeeAPI.Annoucement_State", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Annoucement_Status");
                });

            modelBuilder.Entity("ShippeeAPI.Chat", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .HasColumnType("longtext");

                    b.Property<int?>("id_recipient")
                        .HasColumnType("int");

                    b.Property<int?>("id_sender")
                        .HasColumnType("int");

                    b.Property<DateTime?>("send_time")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("id_recipient");

                    b.HasIndex("id_sender");

                    b.HasIndex("status");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("ShippeeAPI.Chat_State", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Chat_Status");
                });

            modelBuilder.Entity("ShippeeAPI.Company", b =>
                {
                    b.Property<int>("siren")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("city")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("cp")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("id_effective")
                        .HasColumnType("int");

                    b.Property<int?>("id_naf")
                        .HasColumnType("int");

                    b.Property<string>("legal_form")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("name")
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("payment")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("picture")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("street")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("web_site")
                        .HasColumnType("varchar(255)");

                    b.HasKey("siren");

                    b.HasIndex("id_effective");

                    b.HasIndex("id_naf");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ShippeeAPI.Diplome", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("diplome")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Diplomes");
                });

            modelBuilder.Entity("ShippeeAPI.Effective", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Effectives");
                });

            modelBuilder.Entity("ShippeeAPI.Favorite", b =>
                {
                    b.Property<int>("id_user")
                        .HasColumnType("int");

                    b.Property<int>("id_annoucement")
                        .HasColumnType("int");

                    b.HasKey("id_user", "id_annoucement");

                    b.HasIndex("id_annoucement");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("ShippeeAPI.Naf_Division", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("id_naf_section")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("id_naf_section");

                    b.ToTable("Naf_Divisions");
                });

            modelBuilder.Entity("ShippeeAPI.Naf_Section", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Naf_Sections");
                });

            modelBuilder.Entity("ShippeeAPI.Qualification", b =>
                {
                    b.Property<int>("id_annoucement")
                        .HasColumnType("int");

                    b.Property<int>("id_skill")
                        .HasColumnType("int");

                    b.HasKey("id_annoucement", "id_skill");

                    b.HasIndex("id_skill");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("ShippeeAPI.Recent", b =>
                {
                    b.Property<int>("id_user")
                        .HasColumnType("int");

                    b.Property<int>("id_annoucement")
                        .HasColumnType("int");

                    b.Property<DateTime>("consult_date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id_user", "id_annoucement");

                    b.HasIndex("id_annoucement");

                    b.ToTable("Recents");
                });

            modelBuilder.Entity("ShippeeAPI.Recent_Search", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("id_user")
                        .HasColumnType("int");

                    b.Property<string>("text")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("id_user");

                    b.ToTable("Recents_Searches");
                });

            modelBuilder.Entity("ShippeeAPI.Skill", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("ShippeeAPI.Student_Skill", b =>
                {
                    b.Property<int>("id_user")
                        .HasColumnType("int");

                    b.Property<int>("id_skill")
                        .HasColumnType("int");

                    b.HasKey("id_user", "id_skill");

                    b.HasIndex("id_skill");

                    b.ToTable("Student_Skills");
                });

            modelBuilder.Entity("ShippeeAPI.Type_User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Type_Users");
                });

            modelBuilder.Entity("ShippeeAPI.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("city")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("cp")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("cv")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("firstname")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("id_company")
                        .HasColumnType("int");

                    b.Property<int?>("id_type_user")
                        .HasColumnType("int");

                    b.Property<bool?>("is_conveyed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("is_online")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("password")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("picture")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("surname")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("web_site")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("id_company");

                    b.HasIndex("id_type_user");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShippeeAPI.Annoucement", b =>
                {
                    b.HasOne("ShippeeAPI.Diplome", "Diplome")
                        .WithMany()
                        .HasForeignKey("id_diplome");

                    b.HasOne("ShippeeAPI.Naf_Division", "Naf_Division")
                        .WithMany()
                        .HasForeignKey("id_naf_division");

                    b.HasOne("ShippeeAPI.Annoucement_State", "Annoucement_State")
                        .WithMany()
                        .HasForeignKey("id_status");

                    b.HasOne("ShippeeAPI.Type_User", "Type_User")
                        .WithMany()
                        .HasForeignKey("id_type");

                    b.HasOne("ShippeeAPI.User", "User")
                        .WithMany()
                        .HasForeignKey("id_user");

                    b.Navigation("Annoucement_State");

                    b.Navigation("Diplome");

                    b.Navigation("Naf_Division");

                    b.Navigation("Type_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShippeeAPI.Chat", b =>
                {
                    b.HasOne("ShippeeAPI.User", "User2")
                        .WithMany()
                        .HasForeignKey("id_recipient");

                    b.HasOne("ShippeeAPI.User", "User")
                        .WithMany()
                        .HasForeignKey("id_sender");

                    b.HasOne("ShippeeAPI.Chat_State", "Chat_State")
                        .WithMany()
                        .HasForeignKey("status");

                    b.Navigation("Chat_State");

                    b.Navigation("User");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("ShippeeAPI.Company", b =>
                {
                    b.HasOne("ShippeeAPI.Effective", "Effective")
                        .WithMany()
                        .HasForeignKey("id_effective");

                    b.HasOne("ShippeeAPI.Naf_Section", "Naf_Section")
                        .WithMany()
                        .HasForeignKey("id_naf");

                    b.Navigation("Effective");

                    b.Navigation("Naf_Section");
                });

            modelBuilder.Entity("ShippeeAPI.Favorite", b =>
                {
                    b.HasOne("ShippeeAPI.Annoucement", "Annoucement")
                        .WithMany("favorites_users")
                        .HasForeignKey("id_annoucement")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShippeeAPI.User", "User")
                        .WithMany("favorites_annoucements")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Annoucement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShippeeAPI.Naf_Division", b =>
                {
                    b.HasOne("ShippeeAPI.Naf_Section", "Naf_Section")
                        .WithMany()
                        .HasForeignKey("id_naf_section");

                    b.Navigation("Naf_Section");
                });

            modelBuilder.Entity("ShippeeAPI.Qualification", b =>
                {
                    b.HasOne("ShippeeAPI.Annoucement", "Annoucement")
                        .WithMany("skills")
                        .HasForeignKey("id_annoucement")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShippeeAPI.Skill", "Skill")
                        .WithMany("annoucements")
                        .HasForeignKey("id_skill")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Annoucement");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("ShippeeAPI.Recent", b =>
                {
                    b.HasOne("ShippeeAPI.Annoucement", "Annoucement")
                        .WithMany("recents_visits")
                        .HasForeignKey("id_annoucement")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShippeeAPI.User", "User")
                        .WithMany("recents_annoucements")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Annoucement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShippeeAPI.Recent_Search", b =>
                {
                    b.HasOne("ShippeeAPI.User", "User")
                        .WithMany()
                        .HasForeignKey("id_user");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShippeeAPI.Student_Skill", b =>
                {
                    b.HasOne("ShippeeAPI.Skill", "Skill")
                        .WithMany("students")
                        .HasForeignKey("id_skill")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShippeeAPI.User", "User")
                        .WithMany("skills")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShippeeAPI.User", b =>
                {
                    b.HasOne("ShippeeAPI.Company", "Company")
                        .WithMany()
                        .HasForeignKey("id_company");

                    b.HasOne("ShippeeAPI.Type_User", "Type_User")
                        .WithMany()
                        .HasForeignKey("id_type_user");

                    b.Navigation("Company");

                    b.Navigation("Type_User");
                });

            modelBuilder.Entity("ShippeeAPI.Annoucement", b =>
                {
                    b.Navigation("favorites_users");

                    b.Navigation("recents_visits");

                    b.Navigation("skills");
                });

            modelBuilder.Entity("ShippeeAPI.Skill", b =>
                {
                    b.Navigation("annoucements");

                    b.Navigation("students");
                });

            modelBuilder.Entity("ShippeeAPI.User", b =>
                {
                    b.Navigation("favorites_annoucements");

                    b.Navigation("recents_annoucements");

                    b.Navigation("skills");
                });
#pragma warning restore 612, 618
        }
    }
}
