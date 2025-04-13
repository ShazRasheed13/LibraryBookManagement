using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBookManagement
{
    public class RefactoredBook(string title, string status, int condition)
    {
        private const int OverdueThreshold = 14;
        private const int RepairDuration = 5;
        private const int MaxCondition = 5;
        private const int MinCondition = 1;
        private const int RepairImprovement = 2;

        private const string StatusAvailable = "Available";
        private const string StatusBorrowed = "Borrowed";
        private const string StatusOverdue = "Overdue";
        private const string StatusInRepair = "In Repair";

        private readonly string _title = title;
        private string _status = status;
        private int _daysInCurrentStatus = 0;
        private int _condition = Math.Clamp(condition, MinCondition, MaxCondition);
        private int _daysInRepair = 0;

        public string GetTitle() => _title;
        public string GetStatus() => _status;
        public int GetCondition() => _condition;

        public void UpdateStatus()
        {
            if (_status == StatusBorrowed)
                UpdateBorrowedStatus();
            else if (_status == StatusInRepair)
                UpdateRepairStatus();
        }

        private void UpdateBorrowedStatus()
        {
            _daysInCurrentStatus++;
            CheckIfOverdue();
        }

        private void CheckIfOverdue()
        {
            if (_daysInCurrentStatus > OverdueThreshold)
                _status = StatusOverdue;
        }

        private void UpdateRepairStatus()
        {
            _daysInRepair++;
            CheckIfRepairComplete();
        }

        private void CheckIfRepairComplete()
        {
            if (_daysInRepair >= RepairDuration)
                CompleteRepair();
        }

        private void CompleteRepair()
        {
            ImproveCondition();
            _status = StatusAvailable;
            _daysInRepair = 0;
        }

        private void ImproveCondition()
        {
            _condition = Math.Min(_condition + RepairImprovement, MaxCondition);
        }

        public void MarkAsReturned()
        {
            DecreaseCondition();
            UpdateStatusAfterReturn();
            ResetBorrowedDays();
        }

        private void DecreaseCondition()
        {
            if (_condition > MinCondition)
                _condition--;
        }

        private void UpdateStatusAfterReturn()
        {
            _status = (_condition <= MinCondition)
                ? StatusInRepair
                : StatusAvailable;
        }

        private void ResetBorrowedDays()
        {
            _daysInCurrentStatus = 0;
        }

        public bool CanBeBorrowed()
        {
            return _status != StatusOverdue &&
                   _status != StatusInRepair;
        }

        public void MarkAsBorrowed()
        {
            if (!CanBeBorrowed())
                throw new InvalidOperationException("Book cannot be borrowed");

            _status = StatusBorrowed;
            _daysInCurrentStatus = 0;
        }
    }
}
