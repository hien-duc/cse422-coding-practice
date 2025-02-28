using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private List<IReader> _readers;

        public ReaderRepository()
        {
            _readers = new List<IReader>();
        }

        public IReader GetReaderById(int id)
        {
            return _readers.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<IReader> GetAllReaders()
        {
            return _readers;
        }

        public void AddReader(IReader reader)
        {
            _readers.Add(reader);
        }

        public void UpdateReader(IReader reader)
        {
            var existingReader = GetReaderById(reader.Id);
            if (existingReader != null)
            {
                var index = _readers.IndexOf(existingReader);
                _readers[index] = reader;
            }
        }

        public bool DeleteReader(int id)
        {
            var reader = GetReaderById(id);
            if (reader != null)
            {
                return _readers.Remove(reader);
            }
            return false;
        }

        public void InitializeTestData()
        {
            _readers = new List<IReader>
            {
                new Reader { Id = 1, Name = "John Doe" },
                new Reader { Id = 2, Name = "Jane Smith" },
                new Reader { Id = 3, Name = "Bob Johnson" }
            };
        }
    }
} 