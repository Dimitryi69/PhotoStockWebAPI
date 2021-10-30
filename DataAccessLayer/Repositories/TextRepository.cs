using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class TextRepository : IRepository<Text>
    {
        private readonly PhotoStockContext db;

        public TextRepository(PhotoStockContext context)
        {
            this.db = context;
        }

        public IEnumerable<Text> GetAll()
        {
            return db.Texts;
        }

        public Text Get(int id)
        {
            return db.Texts.Find(id);
        }

        public void Create(Text text)
        {
            db.Texts.Add(text);
        }

        public void Update(Text text)
        {
            db.Entry(text).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Text text = db.Texts.Find(id);
            if (text != null)
                db.Texts.Remove(text);
        }
        public Text Find(Func<Text, bool> predicate)
        {
            return db.Texts.FirstOrDefault(predicate);
        }
    }
}
