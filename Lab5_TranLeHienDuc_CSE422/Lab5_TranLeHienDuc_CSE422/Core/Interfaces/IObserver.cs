using System;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IObserver
    {
        void Update(string message);
    }
}