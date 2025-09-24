using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adding 1 default user for Admin role
            Users.AddUser("admin@gmail.com", "Admin123", "Admin");

            bool Exit = false;
            Users? CurrentSession = null;

            while (Exit == false)
            {
                Console.Clear();
                Console.WriteLine("=== SELAMAT DATANG DI APLIKASI PERPUSTAKAAN CONSOLE ===");
                Console.Write("Silahkan pilih dari ketiga menu berikut untuk memulai atau berinteraksi dengan aplikasi:\n1. Sign In\n2. Sign Up\n0. Keluar\nPilih menu: ");
                string? Choice = Console.ReadLine();

                switch (Choice)
                {
                    case "1":
                        CurrentSession = Users.SignIn();
                        if (CurrentSession != null)
                        {
                            ShowMainMenu(CurrentSession);
                        }
                        break;

                    case "2":
                        Users.SignUp();
                        break;

                    case "0":
                        Exit = true;
                        break;

                    default:
                        Console.WriteLine("Pilihan and tidak valid. Pastikan and memilih pilihan dari 0 (Keluar), 1 (Sign In), atau 2 (Sign Up).");
                        break;
                }

                Console.WriteLine("\nTekan keypass apapun untuk melanjutkan...");
                Console.ReadKey();
            }
        }

        static void ShowMainMenu(Users UserSession)
        {
            bool IsSignOut = false;
            while (IsSignOut == false)
            {
                Console.Clear();
                Console.WriteLine($"=== DASHBOARD ({UserSession.Role.ToUpper()}) ===");
                Console.WriteLine("Silahkan memilih menu-menu yang ada untuk berinterkasi dalam applikasi:\n1. Lihat daftar buku yang tersedia\n2. Tambah buku baru\n3. Cari Buku yang ada");
                if (UserSession.Role == "Admin")
                {
                    Console.WriteLine("4. Lihat Semua Pengguna perpustakaan");
                }
                Console.WriteLine("5. Biodata saya\n9. Sign Out/Keluar dari aplikasi\nPilih menu: ");
                string? Choice = Console.ReadLine();

                switch (Choice)
                {
                    case "1":
                        Library.ShowBooks();
                        break;
                    case "2":
                        if (UserSession.Role == "Admin")
                            Library.AddBook();
                        else
                            Console.WriteLine("Hanya pengguna yang berstatus 'ADMIN' yang bisa menambahkan, menghapus, atau memodifikasi metadata buku!");
                        break;
                    case "3":
                        Library.SearchBook();
                        break;
                    case "4":
                        if (UserSession.Role == "Admin")
                            Users.ShowAllUsers();
                        else
                            Console.WriteLine("Akses ditolak, anda mengakses sumber daya yang tidak diperuntukan!");
                        break;
                    case "5":
                        Users.ShowUserAttributes(UserSession.UserId);
                        break;
                    case "9":
                        IsSignOut = true;
                        break;
                }

                if (IsSignOut == false)
                {
                    Console.WriteLine("\nTekan keypass apapun untuk melanjutkan...");
                    Console.ReadKey();
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
        private static Dictionary<string, User> _Users = new();

        public string UserId { get; private set; }
        public string Role { get; private set; }

        private Users(User User)
        {
            UserId = User.Id;
            Role = User.Role;
        }

        public static User? GetUserAttributes(string UserId) {
            if (_Users.TryGetValue(UserId, out User User))
            {
                return User;
            } else
            {
                return null;
            }
        }

        public static void AddUser(string Email, string Password, string role)
        {
            if (_Users.Values.Any(u => u.Email == Email)) return;
            string id = $"ID-{(_Users.Count + 1).ToString("D2")}";
            _Users[id] = new User { Id = id, Email = Email, Password = Password, Role = role };
        }

        public static Users? SignIn()
        {
            Console.Clear();
            Console.WriteLine("=== HALAMAN SIGN IN ===");
            Console.Write("Masukkan Email: ");
            string? Email = Console.ReadLine();
            Console.Write("Masukkan Password: ");
            string? Password = Console.ReadLine();

            User? User = _Users.Values.FirstOrDefault(u => u.Email == Email && u.Password == Password);
            if (User != null)
            {
                Console.WriteLine($"Selamat datang diaplikasi perpustakaan, {User.Email}. Silahkan menggunakan aplikasi secara bijak.");
                return new Users(User);
            }
            else
            {
                Console.WriteLine("Prosess Sign In/Masuk gagal. Harap coba lagi dan periksa nilai Email dan Password anda.");
                return null;
            }
        }

        public static void SignUp()
        {
            Console.Clear();
            Console.WriteLine("=== HALAMAN SIGN UP ===");
            Console.Write("Masukkan Email: ");
            string? Email = Console.ReadLine();
            Console.Write("Masukkan Password: ");
            string? Password = Console.ReadLine();

            if (_Users.Values.Any(u => u.Email == Email))
            {
                Console.WriteLine("Email telah terdaftar sebelumnya diaplikasi perpustakaan! silahkan gunakan email lain");
            }
            else
            {
                AddUser(Email, Password, "User");
                Console.WriteLine("Prosess Registrasi berhasil silahkan masuk dengan akun pengguna baru anda!");
            }
        }

        public static void ShowAllUsers()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR PENGGUNA APLIKASI PERPUSTAKAAN ===");
            foreach (var u in _Users.Values)
            {
                Console.WriteLine($"{u.Id} - {u.Email} ({u.Role})");
            }
        }

        public static void ShowUserAttributes(string UserId)
        {
            Console.Clear();
            if (_Users.TryGetValue(UserId, out User User))
            {
                Console.WriteLine("=== BIODATA ANDA ===");
                List<PropertyInfo> UserAttributes = typeof(User).GetProperties().OrderByDescending(p => p.Name.Length).ToList();
                int LongestPropertyName = UserAttributes.Max(p => p.Name.Length);
                string PropertyName;

                foreach (PropertyInfo p in UserAttributes)
                {
                    if (p.Name == "Id") continue;
                    PropertyName = p.Name.PadRight(LongestPropertyName, ' ');
                    Console.WriteLine($"{PropertyName} = {p.GetValue(User)}");
                }
            }
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
        private static List<Book> _Books = new();

        public static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("=== TAMBAH BUKU BARU ===");
            Console.Write("Masukkan Judul Buku: ");
            string? Title = Console.ReadLine();
            Console.Write("Masukkan Pengarang Buku: ");
            string? Author = Console.ReadLine();
            Console.Write("Masukkan Penerbit Buku: ");
            string? Publisher = Console.ReadLine();
            Console.Write("Masukkan ISBN Buku: ");
            string? ISBN = Console.ReadLine();

            if (Title == null || Author == null || Publisher == null || ISBN == null)
            {
                Console.WriteLine("Salah satu metdata buku yang anda masukkan tidak valid ataur bernilai kosong [NULL]. Mohon coba lagi.");
                return;
            }

            _Books.Add(new Book { Title = Title, Author = Author, Publisher = Publisher, ISBN = ISBN });
            Console.WriteLine($"Buku dengan judul '{Title}' berhasil ditambahkan kedaftar buku perpustakaan!");
        }

        public static void ShowBooks()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR BUKU PERPUSTAKAAN ===");
            if (_Books.Count == 0) Console.WriteLine("Belum ada buku yang tersimpan dalam perpustakaan.");
            foreach (var b in _Books)
            {
                Console.WriteLine($"{b.Title} - {b.Author} ({b.Publisher}) [{b.ISBN}]");
            }
        }

        public static void SearchBook()
        {
            Console.Clear();
            Console.Write("Masukkan kata kunci judul yang ingin anda cari: ");
            string? Keyword = Console.ReadLine();
            List<Book> results = _Books.Where(b => b.Title.Contains(Keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            Console.WriteLine($"=== HASIL PENCARIAN: '{Keyword}' ===");
            if (results.Count == 0) Console.WriteLine($"Tidak ditemukan judul buku terkait pencarian: '{Keyword}'.");
            foreach (Book b in results)
            {
                Console.WriteLine($"{b.Title} - {b.Author} ({b.Publisher}) [{b.ISBN}]");
            }
        }
    }
}
