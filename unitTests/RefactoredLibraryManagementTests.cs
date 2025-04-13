using LibraryBookManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unitTests
{
    public class RefactoredLibraryManagementTests
    {
        [Fact]
        public void WhenBookBorrowedForMoreThan14Days_StatusBecomesOverdue()
        {
            var book = new RefactoredBook("Test Book", "Borrowed", 5);
            var library = new RefactoredLibrarySystem([book]);

            for (int i = 0; i < 15; i++)
            {
                library.UpdateBooks();
            }

            Assert.Equal("Overdue", book.GetStatus());
        }

        [Fact]
        public void WhenBookReturned_ConditionDecreases()
        {
            var book = new RefactoredBook("Test Book", "Borrowed", 3);
            var library = new RefactoredLibrarySystem([book]);

            library.ReturnBook("Test Book");

            Assert.Equal(2, book.GetCondition());
            Assert.Equal("Available", book.GetStatus());
        }

        [Fact]
        public void WhenBookConditionBecomesOne_StatusBecomesInRepair()
        {
            var book = new RefactoredBook("Test Book", "Borrowed", 2);
            var library = new RefactoredLibrarySystem([book]);

            library.ReturnBook("Test Book");

            Assert.Equal(1, book.GetCondition());
            Assert.Equal("In Repair", book.GetStatus());
        }

        [Fact]
        public void WhenBookRepaired_ConditionImproves()
        {
            var book = new RefactoredBook("Test Book", "In Repair", 1);
            var library = new RefactoredLibrarySystem([book]);

            for (int i = 0; i < 5; i++)
            {
                library.UpdateBooks();
            }
            
            Assert.Equal(3, book.GetCondition());
            Assert.Equal("Available", book.GetStatus());
        }

        [Fact]
        public void CannotBorrowOverdueBook()
        {
            var book = new RefactoredBook("Test Book", "Overdue", 5);
            var library = new RefactoredLibrarySystem([book]);
            
            var result = library.BorrowBook("Test Book");

            Assert.False(result);
        }

        [Fact]
        public void CannotBorrowBookInRepair()
        {
            var book = new RefactoredBook("Test Book", "In Repair", 1);
            var library = new RefactoredLibrarySystem([book]);
            
            var result = library.BorrowBook("Test Book");

            Assert.False(result);
        }

        [Fact]
        public void CanBorrowAvailableBook()
        {
            var book = new RefactoredBook("Test Book", "Available", 5);
            var library = new RefactoredLibrarySystem([book]);
            
            var result = library.BorrowBook("Test Book");

            Assert.True(result);
            Assert.Equal("Borrowed", book.GetStatus());
        }

        [Fact]
        public void BookConditionCannotExceedMax()
        {
            var book = new RefactoredBook("Test Book", "In Repair", 4);
            var library = new RefactoredLibrarySystem([book]);

            for (int i = 0; i < 5; i++)
            {
                library.UpdateBooks();
            }
            
            Assert.Equal(5, book.GetCondition());
        }

        [Fact]
        public void BookConditionCannotGoBelowMin()
        {
            var book = new RefactoredBook("Test Book", "Borrowed", 1);
            var library = new RefactoredLibrarySystem([book]);

            library.ReturnBook("Test Book");

            Assert.Equal(1, book.GetCondition());
        }
    }
}
