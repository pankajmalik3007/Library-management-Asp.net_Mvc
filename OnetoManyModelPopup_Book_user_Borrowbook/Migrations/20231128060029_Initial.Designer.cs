﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnetoManyModelPopup_Book_user_Borrowbook.Models;

#nullable disable

namespace OnetoManyModelPopup_Book_user_Borrowbook.Migrations
{
    [DbContext(typeof(MainDBContext))]
    [Migration("20231128060029_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnetoManyModelPopup_Book_user_Borrowbook.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<double>("ISBN")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("OnetoManyModelPopup_Book_user_Borrowbook.Models.BorrowBook", b =>
                {
                    b.Property<int>("BorrowBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BorrowBookId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BorrowBookId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("BorrowBooks");
                });

            modelBuilder.Entity("OnetoManyModelPopup_Book_user_Borrowbook.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnetoManyModelPopup_Book_user_Borrowbook.Models.BorrowBook", b =>
                {
                    b.HasOne("OnetoManyModelPopup_Book_user_Borrowbook.Models.Book", "Book")
                        .WithMany("BorrowBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnetoManyModelPopup_Book_user_Borrowbook.Models.User", "User")
                        .WithMany("BorrowBooks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnetoManyModelPopup_Book_user_Borrowbook.Models.Book", b =>
                {
                    b.Navigation("BorrowBooks");
                });

            modelBuilder.Entity("OnetoManyModelPopup_Book_user_Borrowbook.Models.User", b =>
                {
                    b.Navigation("BorrowBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
