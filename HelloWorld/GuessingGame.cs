using System;
using System.Numerics;

class GuessingGame
{
	public ushort MinValue { get; set; }
	public ushort MaxValue { get; set; }

	public GuessingGame(ushort MinValue = 0, ushort MaxValue = 100) 
	{
		if ((MinValue < 0 || MinValue > MaxValue) || (MaxValue < 0 || MaxValue < MinValue))
		{
            throw new ArgumentOutOfRangeException("You set invalid value for MinValue and MaxValue property, maybe like:\n- MinValue variable less than zero.\n- Or MinValue variable greater than MaxValue variable.\n- MaxValue variable less than zero.\n- Or MaxValue variable less than MinValue variable.\n\n-- Press any button to exit the program --");
		}

		this.MinValue = MinValue;
		this.MaxValue = MaxValue;
	}

	public void GuesingNumberGame()
	{
        // Initialize RANDOM obj
        Random RandomOBJ = new();

		// Initialize game variables
		ushort guesses = 0;
		ushort guesValue;
		int randomNumber = RandomOBJ.Next(this.MinValue, this.MaxValue);
		string? isDone = "Y"; // "N" untuk NO, dan "Y" untuk YES

        // Clear the command/console window first
        Console.Clear();
        Console.WriteLine("=== SEALAMAT DATANG DIGAME TEBAK ANGKA (0 - 100) ===");

        while (isDone == "Y")
		{
			Console.Write("Masukkan tebakkan angkamu: ");
			
			// Check apakah Input mengandung karakter selain angka
			if (ushort.TryParse(Console.ReadLine(), out guesValue) == false)
			{
				Console.WriteLine("Anda memasukkan nilai yang tidak dapat dikenali program. lantak kami menangkap bahwa kamu tidak ingin lagi memainkan game ini :)");
				isDone = "N";
				continue;
			} 

			if (guesValue < this.MinValue ||  guesValue > this.MaxValue) {
				Console.WriteLine($"Tebakanmu diluar dari rentang yang ditentukan, yaitu {this.MinValue} - {this.MaxValue}.");
			}
			else if (guesValue > randomNumber)
			{
				Console.WriteLine("Tebakanmu terlalu tinggi!");
			} else if (guesValue < randomNumber)
			{
				Console.WriteLine("Tebakanmu terlalu rendah!");
			} else
			{
                Console.WriteLine($"Anda telah menebak sebanyak: {guesses} kali.");
                Console.WriteLine("Tebakanmu benar, selamat anda memenangkan game tebak angka ini :]");

				Console.Write("\n=== Apakah Anda ingin bermain kembali? [Y/N] ===\n");
				isDone = Console.ReadLine().ToUpper();

				if ((isDone != "Y" && isDone != "N") || isDone == null) 
				{
					Console.WriteLine("Ku terjemahkan bahwa kamu tidak lagi ingin memainkan game ini :]");
					isDone = "N";
				}

				randomNumber = RandomOBJ.Next(this.MinValue, this.MaxValue); // Reinitialize again a random number
				guesses = 0; // Reset a value of guesses variable

                Console.Clear();
				if (isDone == "Y")
				{
					Console.WriteLine("=== SEALAMAT DATANG DIGAME TEBAK ANGKA (0 - 100) ===");
				}
            }
			guesses++;
		}

		Console.WriteLine("Terimakasih anda telah memainkan game ini :)");
		return;
	}
}
