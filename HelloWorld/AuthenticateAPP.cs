using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class AuthenticateAPP
{
	protected string? Username {  get; set; }
	protected string? Password { get; set; }
	protected bool IsAuthenticated { get; set; } = false;
 
	public AuthenticateAPP() {}

	public void run()
	{
		char IsDone = 'N';

		while (IsDone != 'Y')
		{
			Console.Clear();
			Console.WriteLine("----------------------------------------------");
			Console.WriteLine("=== SELAMAT DATANG DIAPLIKASI AUTHENTIKASI ===");
			Console.WriteLine("----------------------------------------------");

			Console.WriteLine("Pilih dari ketiga opsi ini:\n1. Sign Up\n2. Sign In\n3. Sign Out\n");
			Console.Write("Pilih opsi (1, 2, atau 3): ");

			char UserOption;

			if (char.TryParse(Console.ReadLine(), out UserOption) == false || Regex.IsMatch(UserOption.ToString(), @"^(1|2|3)$") == false)
			{
				Console.WriteLine("Opsi anda tidak valid. Coba lagi.");
			} else
			{
				switch (UserOption)
				{
					case '1':
						this.SignUp();
						break;
					case '2':
						this.SignIn();
						break;
					case '3':
						this.SignOut();
						break;
					default:
                        Console.WriteLine("Opsi anda tidak valid. Coba lagi.");
						break;
                }
			}

            Console.WriteLine("\nIngin mencoba pilihan lain? ketik 'N' untuk melanjutkan dan 'Y' untuk keluar dari program.");
            if (char.TryParse(Console.ReadLine(), out IsDone) == false)
			{
                Console.WriteLine("Input anda tidak valid. Coba lagi.");
            }

            // Check if IsDone valid [Y/N] char value and what user choosing
            if (IsDone == 'Y')
            {
                Console.WriteLine("Terimakasih telah telah menggunakan aplikasi 'AUTHENTIKASI' ini :)");
            }
        }
	}

	private bool CheckIsAuthenticate(string process)
	{
		if (this.IsAuthenticated && process != "Sign Out")
		{
			Console.WriteLine($"Anda telah masuk kedalam aplikasi, maka dari itu proses '{process}' tidak dapat dijalankan.");
			return false;
		} else if (this.IsAuthenticated == false && process == "Sign Out")
		{
            Console.WriteLine($"Anda belum masuk kedalam aplikasi, maka dari itu proses '{process}' tidak dapat dijalankan.");
            return false;
        }
		return true;
	}

	private bool SignUp()
	{
		if (this.CheckIsAuthenticate("Sign Up") == false) return false;

		Console.WriteLine("--- SIGN UP PROCESS ---");
		Console.Write("Inputkan username barumu: ");
		this.Username = Console.ReadLine();
		Console.Write("Inputkan password barumu: ");
		this.Password = Console.ReadLine();

		// After set a value for each property, check if either value is NULL
		if (this.Username == null || this.Password == null)
		{
			Console.WriteLine("\nProses Sign Up atau Registrasi pengguna baru gagal. Coba lagi.");
			this.Username = null;
			this.Password = null;

			return false;
		} else
		{
			Console.WriteLine($"\nPengguna baru dengan username: '{this.Username}' telah berhasil diregistrasikan. Silahkan lanjutkan mungkin dengan mencoba Sign In, :)");
			return true;
		}
	}

	private bool SignIn()
	{
        if (this.CheckIsAuthenticate("Sign In") == false) return false;

        Console.WriteLine("--- SIGN IN PROCESS ---");
        Console.Write("Inputkan usernamemu: ");
		string? UsernameInput = Console.ReadLine();
        Console.Write("Inputkan passwordmu: ");
        string? PasswordInput = Console.ReadLine();

        // After set a value for each property, check if either value is NULL
        if (UsernameInput == this.Username && PasswordInput == this.Password)
        {
            Console.WriteLine($"\nAnda telah berhasil masuk atau melakukan proses Sign In pada aplikasi 'AUTHENTIKASI' ini :)");
			this.IsAuthenticated = true;
            return true;
        } else
		{
            Console.WriteLine("\nProses Sign In atau masuk gagal. Nilai input atau password tidak cocok dengan nilai yang telah kamu daftarkan. Coba lagi.");

			Console.Write("Atau apakah anda ingin melakukan registrasi atau Sign Up? ketik 'Y' untuk registrasi dan 'N' untuk proses selanjutnya.\nKetik pilihan: ");
			if (Console.ReadLine() == "Y") this.SignUp();

            return false;
        }
    }

	private bool SignOut()
	{
        if (this.CheckIsAuthenticate("Sign Out") == true) return false;

        Console.WriteLine("--- SIGN OUT ---");
		Console.Write("Apakah anda yakin ingin keluar atau melanjutkan proses Sign Out? ketik 'Y' untuk keluar dan 'N' untuk membatalkan proses ini.");
		if (char.TryParse(Console.ReadLine(), out char IsOut) == false)
		{
			Console.WriteLine("Anda mengetikkan nilai yang tidak valid. Coba lagi");
			return false;
		}

		if (IsOut == 'Y')
		{
			Console.WriteLine("Anda telah berhasil keluar atau Sign Out dari aplikasi 'AUTHENTIKASI' ini :)");
		}
		return true;
	}
}
