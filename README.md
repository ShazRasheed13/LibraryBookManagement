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
