using LibraryBookManagement;

namespace unitTests
{
    public class LibraryManagementTests
    {
        [Fact]
        public void WhenBookBorrowedForMoreThan14Days_StatusBecomesOverdue()
        {
            var book = new Book("Test Book", "Borrowed", 5);
            var library = new LibrarySystem(new List<Book> { book });

            for (int i = 0; i < 15; i++)
            {
                library.UpdateBooks();
            }

            Assert.Equal("Overdue", book.Status);
        }

        [Fact]
        public void WhenBookReturned_ConditionDecreases()
        {
            var book = new Book("Test Book", "Borrowed", 3);
            var library = new LibrarySystem(new List<Book> { book });

            library.ReturnBook("Test Book");

            Assert.Equal(2, book.ConditionRating);
            Assert.Equal("Available", book.Status);
        }

        [Fact]
        public void WhenBookConditionBecomesOne_StatusBecomesInRepair()
        {
            var book = new Book("Test Book", "Borrowed", 2);
            var library = new LibrarySystem(new List<Book> { book });

            library.ReturnBook("Test Book");

            Assert.Equal(1, book.ConditionRating);
            Assert.Equal("In Repair", book.Status);
        }

        [Fact]
        public void WhenBookRepaired_ConditionImproves()
        {
            var book = new Book("Test Book", "In Repair", 1);
            var library = new LibrarySystem(new List<Book> { book });

            for (int i = 0; i < 5; i++)
            {
                library.UpdateBooks();
            }
            
            Assert.Equal(3, book.ConditionRating);
            Assert.Equal("Available", book.Status);
        }

        [Fact]
        public void CannotBorrowOverdueBook()
        {
            var book = new Book("Test Book", "Overdue", 5);
            var library = new LibrarySystem(new List<Book> { book });
            
            var result = library.BorrowBook("Test Book");

            Assert.False(result);
        }
    }
}
