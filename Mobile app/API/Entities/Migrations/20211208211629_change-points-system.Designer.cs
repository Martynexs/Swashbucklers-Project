﻿// <auto-generated />
using System;
using EncounterAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EncounterAPI.Migrations
{
    [DbContext(typeof(EncounterContext))]
    [Migration("20211208211629_change-points-system")]
    partial class changepointssystem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("EncounterAPI.Models.Rating", b =>
                {
                    b.Property<long>("RouteId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("RouteId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("EncounterAPI.Models.RouteModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("RateSum")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Raters")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Rating")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CreatorID");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("EncounterAPI.Models.UserModel", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EncounterAPI.Models.Waypoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ClosingTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OpeningHours")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("PictureURL")
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<long>("RouteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Waypoints");
                });

            modelBuilder.Entity("Entities.Models.Quiz", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("WaypointId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WaypointId");

                    b.ToTable("Ouizzes");
                });

            modelBuilder.Entity("Entities.Models.QuizAnswers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<long>("QuizId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("QuizzAnswers");
                });

            modelBuilder.Entity("Entities.Models.RouteCompletion", b =>
                {
                    b.Property<long>("RouteId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastVisit")
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.HasKey("RouteId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RouteCompletions");
                });

            modelBuilder.Entity("Entities.Models.WaypointCompletion", b =>
                {
                    b.Property<long>("RouteCompletionRouteId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RouteCompletionUserId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("WaypointId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.HasKey("RouteCompletionRouteId", "RouteCompletionUserId", "WaypointId");

                    b.HasIndex("WaypointId");

                    b.ToTable("WaypointCompletions");
                });

            modelBuilder.Entity("EncounterAPI.Models.Rating", b =>
                {
                    b.HasOne("EncounterAPI.Models.RouteModel", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncounterAPI.Models.UserModel", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EncounterAPI.Models.RouteModel", b =>
                {
                    b.HasOne("EncounterAPI.Models.UserModel", "Creator")
                        .WithMany("Routes")
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("EncounterAPI.Models.Waypoint", b =>
                {
                    b.HasOne("EncounterAPI.Models.RouteModel", "Route")
                        .WithMany("Waypoints")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Entities.Models.Quiz", b =>
                {
                    b.HasOne("EncounterAPI.Models.Waypoint", "Wayoint")
                        .WithMany("Quiz")
                        .HasForeignKey("WaypointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wayoint");
                });

            modelBuilder.Entity("Entities.Models.QuizAnswers", b =>
                {
                    b.HasOne("Entities.Models.Quiz", "quiz")
                        .WithMany("Answers")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("quiz");
                });

            modelBuilder.Entity("Entities.Models.RouteCompletion", b =>
                {
                    b.HasOne("EncounterAPI.Models.RouteModel", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncounterAPI.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Models.WaypointCompletion", b =>
                {
                    b.HasOne("EncounterAPI.Models.Waypoint", "Waypoint")
                        .WithMany()
                        .HasForeignKey("WaypointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.RouteCompletion", "RouteCompletion")
                        .WithMany("CompletedWaypoints")
                        .HasForeignKey("RouteCompletionRouteId", "RouteCompletionUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RouteCompletion");

                    b.Navigation("Waypoint");
                });

            modelBuilder.Entity("EncounterAPI.Models.RouteModel", b =>
                {
                    b.Navigation("Waypoints");
                });

            modelBuilder.Entity("EncounterAPI.Models.UserModel", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Routes");
                });

            modelBuilder.Entity("EncounterAPI.Models.Waypoint", b =>
                {
                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("Entities.Models.Quiz", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Entities.Models.RouteCompletion", b =>
                {
                    b.Navigation("CompletedWaypoints");
                });
#pragma warning restore 612, 618
        }
    }
}
