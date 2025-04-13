# LibraryBookManagement
A problem to learn more of Testing and Refactoring

# Problem Statement

You're working on a legacy library system that manages books and their loan status. Each book has:
- Title
- Status (Available, Borrowed, Overdue, In Repair)
- Days borrowed (if applicable)
- Condition rating (1-5, where 5 is excellent)

Business Rules:
1. Books that are borrowed for more than 14 days become "Overdue"
2. Each time a book is returned, its condition rating decreases by 1
3. If a book's condition rating drops to 1, it must go to "In Repair" status
4. Books in "In Repair" status get their condition improved by 2 points after repair
5. Condition rating can never exceed 5 or go below 1
6. "In Repair" books take 5 days to repair
7. Books cannot be borrowed if they are "Overdue" or "In Repair"

#Major Refactored Changes:

1. From String Status to Constants

	// Original
	if (book.Status == "Borrowed")

	// Refactored
	private const string STATUS_BORROWED = "Borrowed";
	if (_status == STATUS_BORROWED)

Benefit: Prevents typos and makes status changes centralized

2.From Public Properties to Private Fields with Methods

	// Original
	public string Title { get; set; }
	public string Status { get; set; }

	// Refactored
	private string _title;
	private string _status;
	public string GetTitle() => _title;
	public string GetStatus() => _status;

Benefit: Better encapsulation and control over data access
 
3.From Large Methods to Small, Focused Methods

	// Original
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
			// ... more code
		}
	}

	// Refactored
	public void UpdateStatus()
	{
		if (_status == STATUS_BORROWED)
			UpdateBorrowedStatus();
		else if (_status == STATUS_IN_REPAIR)
			UpdateRepairStatus();
	}

	private void UpdateBorrowedStatus()
	{
		_daysInCurrentStatus++;
		CheckIfOverdue();
	}

Benefit: Each method has a single responsibility

4.From Magic Numbers to Constants

	// Original
	if (book.DaysBorrowed > 14)

	// Refactored
	private const int OVERDUE_THRESHOLD = 14;
	if (_daysInCurrentStatus > OVERDUE_THRESHOLD)
	
Benefit: Makes the code more maintainable and self-documenting

5.From Direct Field Access to Validation Methods

	// Original
	if (book.Status != "Overdue" && book.Status != "In Repair")

	// Refactored
	public bool CanBeBorrowed()
	{
		return _status != STATUS_OVERDUE && 
			   _status != STATUS_IN_REPAIR;
	}
	
Benefit: Encapsulates business logic
