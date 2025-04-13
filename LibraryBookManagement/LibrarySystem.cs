using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBookManagement
{
    public class LibrarySystem
    {
        private List<Book> _books;

        public LibrarySystem(List<Book> books)
        {
            _books = books;
        }

        public void UpdateBooks()
        {
            foreach (var book in _books)
            {
                if (book.Status == "Borrowed")
                {
                    book.DaysBorrowed = book.DaysBorrowed + 1;
                    if (book.DaysBorrowed > 14)
                    {
                        book.Status = "Overdue";
                    }
                }

                if (book.Status == "In Repair")
                {
                    book.RepairDays = book.RepairDays + 1;
                    if (book.RepairDays >= 5)
                    {
                        if (book.ConditionRating + 2 > 5)
                        {
                            book.ConditionRating = 5;
                        }
                        else
                        {
                            book.ConditionRating = book.ConditionRating + 2;
                        }
                        book.Status = "Available";
                        book.RepairDays = 0;
                    }
                }
            }
        }

        public void ReturnBook(string title)
        {
            var book = _books.FirstOrDefault(b => b.Title == title);
            if (book != null)
            {
                if (book.ConditionRating > 1)
                {
                    book.ConditionRating = book.ConditionRating - 1;
                }
                if (book.ConditionRating == 1)
                {
                    book.Status = "In Repair";
                    book.RepairDays = 0;
                }
                else
                {
                    book.Status = "Available";
                }
                book.DaysBorrowed = 0;
            }
        }

        public bool BorrowBook(string title)
        {
            var book = _books.FirstOrDefault(b => b.Title == title);
            if (book != null)
            {
                if (book.Status != "Overdue" && book.Status != "In Repair")
                {
                    book.Status = "Borrowed";
                    book.DaysBorrowed = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
