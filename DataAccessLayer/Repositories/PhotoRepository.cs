using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class PhotoRepository : IRepository<Photo>
    {
        private readonly PhotoStockContext db;

        public PhotoRepository(PhotoStockContext context)
        {
            this.db = context;
        }

        public IEnumerable<Photo> GetAll()
        {
            return db.Photos;
        }

        public Photo Get(int id)
        {
            return db.Photos.Find(id);
        }

        public void Create(Photo photo)
        {
            db.Photos.Add(photo);
        }

        public void Update(Photo photo)
        {
            db.Photos.Update(photo);
        }

        public void Delete(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo != null)
                db.Photos.Remove(photo);
        }
        public Photo Find(Func<Photo, Boolean> predicate)
        {
            return db.Photos.FirstOrDefault(predicate);
        }
    }
}
