using LibraryManagementSystem.Models;
using System.Collections.Generic;

namespace LibraryManagementSystem.Repositories
{
    public interface IReaderRepository
    {
        IReader GetReaderById(int id);
        IEnumerable<IReader> GetAllReaders();
        void AddReader(IReader reader);
        void UpdateReader(IReader reader);
        bool DeleteReader(int id);
        void InitializeTestData();
    }
} 