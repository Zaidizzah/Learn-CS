using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tambahkan 1 admin default
            Users.AddUser("admin@gmail.com", "Admin123", "Admin");

            bool exit = false;
            Users currentSession = null;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== LibraryApp Console ===");
                Console.WriteLine("1. Sign In");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("0. Keluar");
                Console.Write("Pilih menu: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        currentSession = Users.SignIn();
                        if (currentSession != null)
                        {
                            ShowMainMenu(currentSession);
                        }
                        break;

                    case "2":
                        Users.SignUp();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowMainMenu(Users session)
        {
            bool logout = false;
            while (!logout)
            {
                Console.Clear();
                Console.WriteLine($"=== Dashboard ({session.Role}) ===");
                Console.WriteLine("1. Lihat Buku");
                Console.WriteLine("2. Tambah Buku");
                Console.WriteLine("3. Cari Buku");
                if (session.Role == "Admin")
                {
                    Console.WriteLine("4. Lihat Semua User");
                }
                Console.WriteLine("9. Logout");
                Console.Write("Pilih menu: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Library.ShowBooks();
                        break;
                    case "2":
                        if (session.Role == "Admin")
                            Library.AddBook();
                        else
                            Console.WriteLine("Hanya Admin yang bisa menambahkan buku!");
                        Console.ReadKey();
                        break;
                    case "3":
                        Library.SearchBook();
                        break;
                    case "4":
                        if (session.Role == "Admin")
                            Users.ShowAllUsers();
                        else
                            Console.WriteLine("Akses ditolak!");
                        Console.ReadKey();
                        break;
                    case "9":
                        logout = true;
                        break;
                }
            }
        }
    }

    class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; } = "Active";
    }

    class Users
    {
        private static Dictionary<string, User> _users = new();

        public string UserId { get; private set; }
        public string Role { get; private set; }

        private Users(User user)
        {
            UserId = user.Id;
            Role = user.Role;
        }

        public static void AddUser(string email, string password, string role)
        {
            if (_users.Values.Any(u => u.Email == email)) return;
            string id = $"ID-{(_users.Count + 1).ToString("D2")}";
            _users[id] = new User { Id = id, Email = email, Password = password, Role = role };
        }

        public static Users SignIn()
        {
            Console.Clear();
            Console.WriteLine("=== Sign In ===");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            var user = _users.Values.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                Console.WriteLine($"Selamat datang, {user.Email}!");
                Console.ReadKey();
                return new Users(user);
            }
            else
            {
                Console.WriteLine("Login gagal.");
                Console.ReadKey();
                return null;
            }
        }

        public static void SignUp()
        {
            Console.Clear();
            Console.WriteLine("=== Sign Up ===");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (_users.Values.Any(u => u.Email == email))
            {
                Console.WriteLine("Email sudah terdaftar!");
            }
            else
            {
                AddUser(email, password, "User");
                Console.WriteLine("Registrasi berhasil!");
            }
            Console.ReadKey();
        }

        public static void ShowAllUsers()
        {
            Console.Clear();
            Console.WriteLine("=== Daftar Pengguna ===");
            foreach (var u in _users.Values)
            {
                Console.WriteLine($"{u.Id} - {u.Email} ({u.Role})");
            }
            Console.ReadKey();
        }
    }

    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
    }

    class Library
    {
        private static List<Book> _books = new();

        public static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("=== Tambah Buku ===");
            Console.Write("Judul: ");
            string title = Console.ReadLine();
            Console.Write("Pengarang: ");
            string author = Console.ReadLine();
            Console.Write("Penerbit: ");
            string publisher = Console.ReadLine();
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();

            _books.Add(new Book { Title = title, Author = author, Publisher = publisher, ISBN = isbn });
            Console.WriteLine("Buku berhasil ditambahkan!");
        }

        public static void ShowBooks()
        {
            Console.Clear();
            Console.WriteLine("=== Daftar Buku ===");
            if (_books.Count == 0) Console.WriteLine("Belum ada buku.");
            foreach (var b in _books)
            {
                Console.WriteLine($"{b.Title} - {b.Author} ({b.Publisher}) [{b.ISBN}]");
            }
            Console.ReadKey();
        }

        public static void SearchBook()
        {
            Console.Clear();
            Console.Write("Masukkan kata kunci judul: ");
            string keyword = Console.ReadLine();
            var results = _books.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            Console.WriteLine("=== Hasil Pencarian ===");
            if (results.Count == 0) Console.WriteLine("Tidak ditemukan.");
            foreach (var b in results)
            {
                Console.WriteLine($"{b.Title} - {b.Author} ({b.Publisher}) [{b.ISBN}]");
            }
            Console.ReadKey();
        }
    }
}
