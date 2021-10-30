using BuisnessLogicLayer.DataTransferObjects;
using BuisnessLogicLayer.Parameters;
using System;
using System.Collections.Generic;

namespace BuisnessLogicLayer.Interfaces
{
    public interface IStockService : IDisposable
    {
        IEnumerable<object> GetAllEntities(ListParameters parameters);
        IEnumerable<AuthorDto> GetAuthors();
        IEnumerable<PhotoDto> GetPhotos(ListParameters parameters);
        IEnumerable<TextDto> GetTexts();
        PhotoDto GetPhoto(int id);
        void AddText(TextDto text);
        void UpdatePhoto(int id, PhotoDto photo);
        void SetRating(int photoId, int rating);
        AuthorDto GetAuthor(string nickname, string name);
    }
}
