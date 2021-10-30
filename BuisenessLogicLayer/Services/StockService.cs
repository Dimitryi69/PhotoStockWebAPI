using AutoMapper;
using BuisnessLogicLayer.DataTransferObjects;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Parameters;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BuisnessLogicLayer.Services
{
    public class StockService : IStockService
    {
        IUnitOfWork Database { get; set; }
        public StockService(IUnitOfWork db)
        {
            Database = db;
        }

        public void AddText(TextDto text)
        {
            Author author = Database.Authors.Get(text.Author.Id);
            if (author == null)
            {
                throw new ValidationDtoException("Can't find the author", nameof(text.Author));
            }
            Text newText = new()
            {
                Name = text.Name,
                Content = text.Content,
                CreationDate = text.CreationDate,
                Price = text.Price,
                Length = text.Length,
                PurchaseCount = text.PurchaseCount,
                Rating = text.Rating,
                AuthorId = text.Author.Id
            };
            Database.Texts.Create(newText);
            Database.Save();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            Database.Dispose();
        }
        public IEnumerable<object> GetAllEntities(ListParameters parameters)
        {
            var allEntities = new List<object>();
            allEntities.AddRange(GetAuthors());
            allEntities.AddRange(GetPhotos(null));
            allEntities.AddRange(GetTexts());
            int totalPages = (int)Math.Ceiling(allEntities.Count / (double)parameters.PageSize);
            if (parameters.PageNumber > totalPages)
            {
                parameters.PageNumber = totalPages;
            }
            allEntities = allEntities.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize).ToList();
            return allEntities;
        }
        public IEnumerable<AuthorDto> GetAuthors()
        {
            var authors = Database.Authors.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDto>()).CreateMapper();
            return mapper.Map<IEnumerable<Author>, List<AuthorDto>>(authors);
        }

        public PhotoDto GetPhoto(int id)
        {
            Photo photo = Database.Photos.Get(id);
            if (photo == null)
            {
                throw new ValidationDtoException("Can't find a photo", "");
            }
            return new PhotoDto()
            {
                Id = photo.Id,
                Name = photo.Name,
                ContentURI = photo.ContentURI,
                CreationDate = photo.CreationDate,
                Price = photo.Price,
                Height = photo.Height,
                Width = photo.Width,
                PurchaseCount = photo.PurchaseCount,
                Rating = photo.Rating,
                Author = new AuthorDto() { Id = photo.Author.Id, Name = photo.Author.Name, Age = photo.Author.Age, CreationDate = photo.Author.CreationDate, Nickname = photo.Author.Nickname },
            };

        }

        public IEnumerable<PhotoDto> GetPhotos(ListParameters parameters)
        {
            var photos = Database.Photos.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Photo, PhotoDto>()
            .ForMember("Author", opt => opt.MapFrom(src => new AuthorDto()
            {
                Id = src.Author.Id,
                Name = src.Author.Name,
                Age = src.Author.Age,
                Nickname = src.Author.Nickname,
                CreationDate = src.Author.CreationDate
            }))).CreateMapper();
            var mappedPhotos = mapper.Map<IEnumerable<Photo>, List<PhotoDto>>(photos);
            if (parameters != null)
            {
                int totalPages = (int)Math.Ceiling(mappedPhotos.Count / (double)parameters.PageSize);
                if (parameters.PageNumber > totalPages)
                {
                    parameters.PageNumber = totalPages;
                }
                mappedPhotos = mappedPhotos.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize).ToList();
            }
            return mappedPhotos;
        }

        public IEnumerable<TextDto> GetTexts()
        {
            var texts = Database.Texts.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Text, TextDto>()
            .ForMember("Author", opt => opt.MapFrom(src => new AuthorDto()
            {
                Id = src.Author.Id,
                Name = src.Author.Name,
                Age = src.Author.Age,
                Nickname = src.Author.Nickname,
                CreationDate = src.Author.CreationDate
            }))).CreateMapper();
            return mapper.Map<IEnumerable<Text>, List<TextDto>>(texts);
        }

        public void SetRating(int photoId, int rating)
        {
            Photo updatedPhoto = Database.Photos.Get(photoId);
            if (updatedPhoto == null)
            {
                throw new ValidationDtoException("Can't find photo with this Id", nameof(photoId));
            }
            updatedPhoto.Rating = rating;
            Database.Photos.Update(updatedPhoto);
            Database.Save();
        }

        public void UpdatePhoto(int id, PhotoDto photo)
        {
            Photo updatedPhoto = Database.Photos.Get(id);
            if (updatedPhoto == null)
            {
                throw new ValidationDtoException("Can't find photo with this Id", nameof(photo));
            }
            updatedPhoto.Name = photo.Name;
            updatedPhoto.ContentURI = photo.ContentURI;
            updatedPhoto.CreationDate = photo.CreationDate;
            updatedPhoto.Price = photo.Price;
            updatedPhoto.Height = photo.Height;
            updatedPhoto.Width = photo.Width;
            updatedPhoto.PurchaseCount = photo.PurchaseCount;
            updatedPhoto.Rating = photo.Rating;
            Database.Photos.Update(updatedPhoto);
            Database.Save();
        }

        public AuthorDto GetAuthor(string nickname, string name)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDto>()).CreateMapper();
            var author = mapper.Map<Author, AuthorDto>(Database.Authors.Find(author => author.Nickname == nickname && author.Name == name));
            if (author == null)
            {
                throw new ValidationDtoException("Can't find the author with this nickname", nameof(author));
            }
            return author;
        }
    }
}
