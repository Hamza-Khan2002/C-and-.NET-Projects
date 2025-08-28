using System;
using System.Diagnostics.Eventing.Reader;

namespace WebApplication1
{
    class ConsoleApp
    {
        public static void Main(string[] args)
        {
            Books book = new Books();
            bool fullExit = false;
            char option;

            while (!fullExit)
            {
                Show(book);

                Console.WriteLine();
                Console.Write("Do you want to continue (Y/N): ");
                string? input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    option = input[0];
                }
                else
                {
                    option = 'N';
                }

                if (option == 'N' || option == 'n')
                {
                    fullExit = true;
                    Console.WriteLine("Exiting...");
                }
            }

        }

        public static void Show(Books book)
        {
            string? name;
            int rollNumber;
            StartAs selected = StartAs.Staff;

            bool exit = false;

            Console.WriteLine(".....................\nWELCOME TO KHAN'S LIBRARY!\n.....................");
            Console.WriteLine("How do you want to access this?");
            Console.WriteLine("1. Staff\n2. Student");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (Enum.IsDefined(typeof(StartAs), choice))
            {
                selected = (StartAs)choice;
                Console.WriteLine($"Welcome {selected}");
            }
            else Console.WriteLine("Invalid Choice");

            if (selected == StartAs.Staff)
            {
                Librarian staff = new Librarian(book);

                string bookName, bookName1;
                while (!exit)
                {
                    Console.WriteLine("Please select Option:");
                    Console.WriteLine("1. Add Books\n2. Remove Books\n3. Display Available Books \n4. Display Rented Books\n5. Exit");
                    int choose = Convert.ToInt32(Console.ReadLine());

                    switch (choose)
                    {
                        case 1:
                            Console.Write("Please Enter Book Name: ");
                            bookName = Console.ReadLine()!;
                            staff.AddBook(bookName);
                            break;

                        case 2:
                            Console.Write("Please Enter Book Name: ");
                            bookName1 = Console.ReadLine()!;
                            staff.RemoveBook(bookName1);
                            break;

                        case 3:
                            book.DisplayAvailable();
                            Console.WriteLine();
                            break;

                        case 4:
                            book.DisplayRented();
                            Console.WriteLine();
                            break;

                        case 5:
                            exit = true;
                            Console.Write("Thankyou so much for visiting!");
                            break;

                        default:
                            Console.Write("Incorrect Input!");
                            continue;
                    }
                }
            }
            else
            {
                int bookNumber;

                Console.Write("Please Enter your name: ");
                name = Console.ReadLine()!;
                Console.Write("Please Enter your RollNumber: ");
                rollNumber = Convert.ToInt32(Console.ReadLine());

                Student member = new Student(book, name, rollNumber);


                while (!exit)
                {
                    Console.WriteLine("Please select Option:");
                    Console.WriteLine("1. Available Books\n2. Rent a book\n3. Rented Books\n4. Return book\n5. Exit");
                    int choose = Convert.ToInt32(Console.ReadLine());

                    switch (choose)
                    {
                        case 1:
                            book.DisplayAvailable();
                            Console.WriteLine();
                            break;

                        case 2:
                            Console.Write("Enter book you want to rent: ");
                            bookNumber = Convert.ToInt32(Console.ReadLine());
                            member.RentBook(bookNumber, name);
                            break;

                        case 3:
                            book.DisplayRented();
                            Console.WriteLine();
                            break;

                        case 4:
                            Console.Write("Enter book you want to return: ");
                            bookNumber = Convert.ToInt32(Console.ReadLine());
                            member.ReturnBook(bookNumber, name);
                            break;

                        case 5:
                            exit = true;
                            Console.Write("Thankyou so much for visiting!");
                            break;

                        default:
                            Console.Write("Incorrect Input!");
                            continue;
                    }
                }
            }
        }
    }

    enum StartAs
    {
        Staff = 1,
        Student = 2
    }

    class Books
    {
        public List<string> booksAvailable = new List<string>();
        public List<string[]> booksRented = new List<string[]>();
        int i;
        public void DisplayAvailable()
        {
            i = 1;
            Console.WriteLine("..........................");
            Console.WriteLine("AVAILABLE BOOKS");
            Console.WriteLine("..........................");
            foreach (string book in booksAvailable)
            {
                Console.WriteLine($"{i}. {book}");
                i++;
            }
        }
        public void DisplayRented()
        {
            i = 1;
            Console.WriteLine("..........................");
            Console.WriteLine("RENTED BOOKS");
            Console.WriteLine("..........................");
            foreach (var book in booksRented)
            {
                Console.Write(i+" ");
                Console.WriteLine(string.Join(" issued by ", book));
                i++;
            }
        }

    }

    class Librarian
    {
        private Books _book;

        public Librarian(Books book)
        {
            _book = book;
        }

        public void AddBook(string bookName)
        {
            _book.booksAvailable.Add(bookName);
        }

        public void RemoveBook(string bookName)
        {
            _book.booksAvailable.Remove(bookName);
        }
    }

    class Student
    {
        private Books _book;
        private string name { get; set; }
        private int rollNumber { get; set; }

        public Student(Books book, string name, int rollNumber)
        {
            _book = book;
            this.name = name;
            this.rollNumber = rollNumber;
        }

        public void RentBook(int number, string name)
        {
            if (number > 0 && number <= _book.booksAvailable.Count)
            {
                string selectedBook = _book.booksAvailable[number - 1];

                _book.booksAvailable.RemoveAt(number - 1);

                _book.booksRented.Add(new string[] { selectedBook, name });

                Console.WriteLine($"{name} with RollNumber: {rollNumber} issued {selectedBook}");
            }
            else
            {
                Console.WriteLine("Invalid Choice");
            }
        }

        public void ReturnBook(int number, string name)
        {
            if (number > 0 && number <= _book.booksRented.Count)
            {
                string[] rentedBook = _book.booksRented[number - 1];

                _book.booksAvailable.Add(rentedBook[0]);

                _book.booksRented.RemoveAt(number - 1);

                Console.WriteLine($"{name} with RollNumber: {rollNumber} returned {rentedBook[0]}");
            }
            else
            {
                Console.WriteLine("Invalid Choice");
            }
        }
    }
    
}


