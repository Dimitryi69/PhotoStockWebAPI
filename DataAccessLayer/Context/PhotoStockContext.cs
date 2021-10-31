using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer.Context
{
    //CREATE DATABASE [StockDatabase]
    public class PhotoStockContext : DbContext
    {
        //CREATE TABLE[dbo].[Authors]
        //(
        //[Id][int] IDENTITY(1,1) NOT NULL,
        //[Name] [nvarchar] (max) NOT NULL,
        //[Nickname] [nvarchar] (450) NOT NULL,
        //[Age] [int] NOT NULL,
        //[CreationDate] [datetime2] (7) NOT NULL,
        //CONSTRAINT[PK_Authors] PRIMARY KEY CLUSTERED
        //)
        public DbSet<Author> Authors { get; set; }

        //CREATE TABLE[dbo].[Photos]
        //(
        //[Id][int] IDENTITY(1,1) NOT NULL,
        //[Name] [nvarchar] (max) NOT NULL,
        //[ContentURI] [nvarchar] (max) NOT NULL,
        //[CreationDate] [datetime2] (7) NOT NULL,
        //[Height] [int] NOT NULL,
        //[Width] [int] NOT NULL,
        //[Price] [int] NOT NULL,
        //[PurchaseCount] [int] NOT NULL,
        //[Rating] [real] NOT NULL,
        //[AuthorId] [int] NOT NULL,
        //CONSTRAINT[PK_Photos] PRIMARY KEY CLUSTERED)
        //ALTER TABLE [dbo].[Photos] WITH CHECK ADD CONSTRAINT [FK_Photos_Authors_AuthorId] FOREIGN KEY([AuthorId])
        //REFERENCES [dbo].[Authors] ([Id])
        //ON DELETE CASCADE
        public DbSet<Photo> Photos { get; set; }

        //CREATE TABLE[dbo].[Texts](
        //[Id][int] IDENTITY(1,1) NOT NULL,
        //[Name] [nvarchar](max) NOT NULL,
        //[Content] [nvarchar] (max) NOT NULL,
        //[CreationDate] [datetime2] (7) NOT NULL,
        //[Price] [int] NOT NULL,
        //[Length] [int] NOT NULL,
        //[PurchaseCount] [int] NOT NULL,
        //[Rating] [real] NOT NULL,
        //[AuthorId] [int] NOT NULL,
        //CONSTRAINT[PK_Texts] PRIMARY KEY CLUSTERED)
        //ALTER TABLE[dbo].[Texts] WITH CHECK ADD CONSTRAINT[FK_Texts_Authors_AuthorId] FOREIGN KEY([AuthorId])
        //REFERENCES[dbo].[Authors]
        //([Id])
        //ON DELETE CASCADE
        public DbSet<Text> Texts { get; set; }

        public PhotoStockContext(DbContextOptions<PhotoStockContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasIndex(u => u.Nickname)
                .IsUnique();
            modelBuilder.Entity<Author>().HasData(
                new Author[]
                {
                new Author { Id = 1, Name="Andrew", Nickname = "HowlingWolf", Age = 31, CreationDate = DateTime.Now},
                new Author { Id = 2, Name="Alex", Nickname = "GorgeousLizard", Age = 24, CreationDate = DateTime.Now},
                new Author { Id = 3, Name="Henry", Nickname = "TiredBear", Age = 47, CreationDate = DateTime.Now},
                new Author { Id = 4,Name="John", Nickname = "RestingZebra", Age = 19, CreationDate = DateTime.Now},
                });
            modelBuilder.Entity<Photo>().HasData(
                new Photo[]
                {
                new Photo { Id=1, AuthorId = 1, ContentURI = "SomeUri1/AnotherUri.jpg", CreationDate = new DateTime(2021, 10, 29), Height = 100, Name = "AwesomePhoto", Price = 400, PurchaseCount = 5, Rating = 4.2f, Width = 100},
                new Photo { Id=2, AuthorId = 1, ContentURI = "SomeUri2/AnotherUri.jpg", CreationDate = new DateTime(2021, 10, 28), Height = 200, Name = "OutstandingPhoto", Price = 500, PurchaseCount = 10, Rating = 4.8f, Width = 200},
                new Photo { Id=3, AuthorId = 3, ContentURI = "SomeUri3/AnotherUri.jpg", CreationDate = new DateTime(2021, 10, 27), Height = 300, Name = "BreathtakingPhoto", Price = 600, PurchaseCount = 15, Rating = 3.9f, Width = 300},
                new Photo { Id=4, AuthorId = 4, ContentURI = "SomeUri4/AnotherUri.jpg", CreationDate = new DateTime(2021, 10, 26), Height = 400, Name = "AmazingPhoto", Price = 700, PurchaseCount = 16, Rating = 4.5f, Width = 400},
                });
            modelBuilder.Entity<Text>().HasData(
                new Text[]
                {
                new Text { Id=1, AuthorId = 2, Content = "This is and awesome sample of text content", CreationDate = new DateTime(2021, 10, 24), Name = "AwesomeText", Price = 150, PurchaseCount = 17, Rating = 3.2f, Length = 42},
                new Text { Id=2, AuthorId = 4, Content = "This is and outstainding sample of text content", CreationDate = new DateTime(2021, 10, 20), Name = "OutstaindingText", Price = 175, PurchaseCount = 25, Rating = 2.5f, Length = 47},
                new Text { Id=3, AuthorId = 2, Content = "This is and breathtaking sample of text content", CreationDate = new DateTime(2021, 10, 14), Name = "BreathtakingText", Price = 236, PurchaseCount = 21, Rating = 5f, Length = 47},
                });
        }
    }
}
