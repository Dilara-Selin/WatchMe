﻿// <auto-generated />
using System;
using MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MVC.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GenreId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MovieId"));

                    b.Property<string>("Overview")
                        .HasColumnType("text");

                    b.Property<double?>("Popularity")
                        .HasColumnType("double precision");

                    b.Property<string>("PosterPath")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MovieId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieComment", b =>
                {
                    b.Property<int>("MovieCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MovieCommentId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("MovieCommentId");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("MovieComments");
                });

            modelBuilder.Entity("MovieDislike", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("MovieDislikeId")
                        .HasColumnType("integer");

                    b.HasKey("MovieId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MovieDislikes");
                });

            modelBuilder.Entity("MovieGenre", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("MovieLike", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("MovieLikeId")
                        .HasColumnType("integer");

                    b.HasKey("MovieId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MovieLikes");
                });

            modelBuilder.Entity("MovieWatchList", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("MovieWatchListId")
                        .HasColumnType("integer");

                    b.HasKey("MovieId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MovieWatchLists");
                });

            modelBuilder.Entity("TVShow", b =>
                {
                    b.Property<int>("TVShowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TVShowId"));

                    b.Property<string>("Overview")
                        .HasColumnType("text");

                    b.Property<double?>("Popularity")
                        .HasColumnType("double precision");

                    b.Property<string>("PosterPath")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TVShowId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("TVShows");
                });

            modelBuilder.Entity("TVShowComment", b =>
                {
                    b.Property<int>("TVShowCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TVShowCommentId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TVShowId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("TVShowCommentId");

                    b.HasIndex("TVShowId");

                    b.HasIndex("UserId");

                    b.ToTable("TVShowComments");
                });

            modelBuilder.Entity("TVShowDislike", b =>
                {
                    b.Property<int>("TVShowId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("TVShowDislikeId")
                        .HasColumnType("integer");

                    b.HasKey("TVShowId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TVShowDislikes");
                });

            modelBuilder.Entity("TVShowGenre", b =>
                {
                    b.Property<int>("TVShowId")
                        .HasColumnType("integer");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.HasKey("TVShowId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("TVShowGenres");
                });

            modelBuilder.Entity("TVShowLike", b =>
                {
                    b.Property<int>("TVShowId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("TVShowLikeId")
                        .HasColumnType("integer");

                    b.HasKey("TVShowId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TVShowLikes");
                });

            modelBuilder.Entity("TVShowWatchList", b =>
                {
                    b.Property<int>("TVShowId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("TVShowWatchListId")
                        .HasColumnType("integer");

                    b.HasKey("TVShowId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TVShowWatchLists");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MovieComment", b =>
                {
                    b.HasOne("Movie", "Movie")
                        .WithMany("MovieComments")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("MovieComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieDislike", b =>
                {
                    b.HasOne("Movie", "Movie")
                        .WithMany("MovieDislikes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("MovieDislikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieGenre", b =>
                {
                    b.HasOne("Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieLike", b =>
                {
                    b.HasOne("Movie", "Movie")
                        .WithMany("MovieLikes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("MovieLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieWatchList", b =>
                {
                    b.HasOne("Movie", "Movie")
                        .WithMany("MovieWatchLists")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("MovieWatchLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TVShowComment", b =>
                {
                    b.HasOne("TVShow", "TVShow")
                        .WithMany("TVShowComments")
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("TVShowComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TVShow");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TVShowDislike", b =>
                {
                    b.HasOne("TVShow", "TVShow")
                        .WithMany("TVShowDislikes")
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("TVShowDislikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TVShow");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TVShowGenre", b =>
                {
                    b.HasOne("Genre", "Genre")
                        .WithMany("TVShowGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TVShow", "TVShow")
                        .WithMany("TVShowGenres")
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("TVShow");
                });

            modelBuilder.Entity("TVShowLike", b =>
                {
                    b.HasOne("TVShow", "TVShow")
                        .WithMany("TVShowLikes")
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("TVShowLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TVShow");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TVShowWatchList", b =>
                {
                    b.HasOne("TVShow", "TVShow")
                        .WithMany("TVShowWatchLists")
                        .HasForeignKey("TVShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("TVShowWatchLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TVShow");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Genre", b =>
                {
                    b.Navigation("MovieGenres");

                    b.Navigation("TVShowGenres");
                });

            modelBuilder.Entity("Movie", b =>
                {
                    b.Navigation("MovieComments");

                    b.Navigation("MovieDislikes");

                    b.Navigation("MovieGenres");

                    b.Navigation("MovieLikes");

                    b.Navigation("MovieWatchLists");
                });

            modelBuilder.Entity("TVShow", b =>
                {
                    b.Navigation("TVShowComments");

                    b.Navigation("TVShowDislikes");

                    b.Navigation("TVShowGenres");

                    b.Navigation("TVShowLikes");

                    b.Navigation("TVShowWatchLists");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("MovieComments");

                    b.Navigation("MovieDislikes");

                    b.Navigation("MovieLikes");

                    b.Navigation("MovieWatchLists");

                    b.Navigation("TVShowComments");

                    b.Navigation("TVShowDislikes");

                    b.Navigation("TVShowLikes");

                    b.Navigation("TVShowWatchLists");
                });
#pragma warning restore 612, 618
        }
    }
}
