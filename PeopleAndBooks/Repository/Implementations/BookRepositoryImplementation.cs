using PeopleAndBooks.Data;
using PeopleAndBooks.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleAndBooks.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private readonly SqlServerContext _context;

        public BookRepositoryImplementation(SqlServerContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Book.ToList();
        }

        public Book FindById(int id)
        {
            return _context.Book.SingleOrDefault(x => x.Id.Equals(id));
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
                return book;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            var result = _context.Book.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Book.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public Book Update(Book book)
        {
            if (!Exists(book)) return null;

            var result = _context.Book.SingleOrDefault(p => p.Id.Equals(book.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return book;
        }

        public bool Exists(Book book)
        {
            return _context.Book.Any(x => x.Id.Equals(book.Id));
        }
    }
}
