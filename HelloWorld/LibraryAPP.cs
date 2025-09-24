using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace LibraryAPP
{
	class Library
	{
		List<string> BookLists = 

		public void Library()
		{
		}
	}

	class Book
	{
		public string Title { get; set; }, public string Author { get; set; }, public string Publisher { get; set; }, public DateTime PublicationDate { get; set; }
		private string _ISBN;
		private DateTime _ModifierDate;

        public string ISBN
		{
			get
			{
				return this._ISBN;
			}
			set
			{
				if (value.Length > 13)
				{
					throw new ArgumentException("ISBN number is invalid, exceeding the specified length of 13 digits.");
				} else
				{
					this._ISBN = value;
				}
			}
		}
		public DateTime ModifierDate
		{
			get
			{
				return _ModifierDate;
			}
			set
			{
				if (value < this.PublicationDate)
				{
					throw new ArgumentException($"Modifier date is invalid, must not exceed the publication date: {this.PublicationDate.ToString("dddd, MMMM dd, yyyy h:mm tt")}.");
				} else
				{
					_ModifierDate = value;
				}
			}
		}

        private enum Status
		{
			Available, // Tersedia
			Borrowed, // Sedang dipinjam
			InTransit, // Sedang dalam perjalanan
			InProcess, // Dalam pemrosesan staff perpustakaan
			Renewed, // Diperpanjang oleh peminjam terkait
			Lost, // Hilang
			Damaged, // Rusak
			Archived, // Diarsipkan
		};

		public void Book(string Title, string Author, string Publisher, string ISBN, DateTime PublicationDate) 
		{
			this.Title = Title;
			this.Author = Author;
			this.Publisher = Publisher;
			this.PublicationDate = PublicationDate;
			this.ISBN = ISBN;
			this.ModifierDate = PublicationDate;
		}
	}

	class Users
	{
		private static Dictionary<string, string[]> _Users = new Dictionary<string, string[]>()
		// default user for role 'Admin' and 'User' for Index 3. with index sequence is: 0 => for Email, 1=> for Password, 2 => for Role, and 3 For Active status
		{
			{ "ID-01", new string[4] { "Admin@gmail.com", "Admin123", "Admin", "Active" } },
			{ "ID-02", new string[4] { "User@gmail.com", "User123", "Admin", "Active" } }
        };
		protected string UserId { get; set; }
		protected bool IsLoggedIn { get; set; } = false;

		public Dictionary<string, string[]> _GetUsers(string Role)
		{
			if (Role == "Admin")
			{
				return this._Users.Values;
			} else
			{
				return this._Users[this.UserId];
			}
		}
		public void _AddUsers(string[] User)
		{
            this._Users.Add($"ID-{(this._Users.Count + 1 >= 10 ? this._Users.Count + 1 : $"0{this._Users.Count + 1}")}", User);
        }
		public bool _RemoveUsers(string Id)
		{
			return this._Users.Remove(Id);
		}

		public bool _ContainUsers<T>(T UsersSearch)
		{
			// Check if UserSearch is STRING
			if (UsersSearch is string UserSearch)
			{
				foreach (KeyValuePair<string, string[]> User in this._Users)
				{
					if (User.Value.Contains<string>(UserSearch)) return true;
				}
			} else if (UsersSearch is string[] UserSearchs)
			{
                foreach (KeyValuePair<string, string[]> User in this._Users)
                {
                    if (User.Value.Contains<string>(...UserSearchs)) return true;  
                }
            } else
			{
				return false;
			}
        }

		public void Users(string[]? User = null)
		{
			// Check if User is not NULL, then add new user to list of users
			if (User != null) this._AddUsers(User);
		}

		public bool SignIn()
		{
			Console.WriteLine("---+ Sign In Process +---\n");
			Console.Write("Inputkan email anda: ");
			string? Email = Console.ReadLine();
			Console.Write("Inputkan password anda: ");
			string? Password = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || this._ContainUsers(new string[2] { Email, Password }) == false)
			{
				Console.WriteLine("Terjadi kesalahan dalam melakukan proses Sign In atau masuk kedalam aplikasi.");
				return false;
			} else
			{
				Console.WriteLine($"Anda telah berhasil masuk kedalam aplikasi dengan email: {Email}.");
				return true;
			}
		}

		public bool SignUp()
		{
            Console.WriteLine("---+ Sign Up Process +---\n");
            Console.Write("Inputkan email anda: ");
            string? Email = Console.ReadLine();
            Console.Write("Inputkan password anda: ");
            string? Password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                Console.WriteLine("Terjadi kesalahan dalam melakukan proses Sign Up atau registrasi kedalam aplikasi. Periksa nilai Email dan password anda karena applikasi mendeteksi nilai yang tidak valid dari keduanya.");
                return false;
            } else if (this._ContainUsers(new string[2] { Email, Password }) == false)

			else
			{
				Console.WriteLine($"Anda telah berhasil masuk kedalam aplikasi dengan email: {Email}.");
				return true;
			}
        }
	}
}
