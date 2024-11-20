﻿// <auto-generated />
using EfcRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EfcRepositories.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20241120173230_DisableForeignKeys")]
    partial class DisableForeignKeys
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Entities.Moderator", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubForumId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "SubForumId");

                    b.HasIndex("SubForumId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Moderators");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("SubForumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SubForumId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Entities.Reaction", b =>
                {
                    b.Property<int>("ReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommentId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLike")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isPost")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReactionId");

                    b.HasIndex("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("Entities.SubForum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SubForums");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entities.Comment", b =>
                {
                    b.HasOne("Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Moderator", b =>
                {
                    b.HasOne("Entities.SubForum", "SubForum")
                        .WithMany("Moderators")
                        .HasForeignKey("SubForumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", "User")
                        .WithOne("Moderator")
                        .HasForeignKey("Entities.Moderator", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubForum");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.HasOne("Entities.SubForum", "SubForum")
                        .WithMany("Posts")
                        .HasForeignKey("SubForumId");

                    b.HasOne("Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubForum");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Reaction", b =>
                {
                    b.HasOne("Entities.Comment", "Comment")
                        .WithMany("Reactions")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.Post", "Post")
                        .WithMany("Reactions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.User", "User")
                        .WithMany("Reactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Comment", b =>
                {
                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("Entities.SubForum", b =>
                {
                    b.Navigation("Moderators");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Moderator")
                        .IsRequired();

                    b.Navigation("Posts");

                    b.Navigation("Reactions");
                });
#pragma warning restore 612, 618
        }
    }
}
