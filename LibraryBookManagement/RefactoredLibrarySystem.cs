using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBookManagement
{
    public class RefactoredLibrarySystem(List<RefactoredBook> books)
    {

        private List<RefactoredBook> _books = books ?? [];

        public void UpdateBooks()
        {
            foreach (var book in _books)
            {
                book.UpdateStatus();
            }
        }

        public void ReturnBook(string title)
        {
            var book = FindBook(title);
            book?.MarkAsReturned();
        }

        public bool BorrowBook(string title)
        {
            var book = FindBook(title);
            if (!book.CanBeBorrowed())
                return false;

            book.MarkAsBorrowed();
            return true;
        }

        private RefactoredBook FindBook(string title)
        {
            return _books.FirstOrDefault(b => b.GetTitle() == title)!;
        }

    }


}
