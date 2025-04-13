using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBookManagement
{
    public class Book
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public int DaysBorrowed { get; set; }
        public int ConditionRating { get; set; }
        public int RepairDays { get; set; }

        public Book(string title, string status, int conditionRating)
        {
            Title = title;
            Status = status;
            ConditionRating = conditionRating;
            DaysBorrowed = 0;
            RepairDays = 0;
        }
    }
}
