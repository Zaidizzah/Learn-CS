using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

public class RockPapperScissorGame
{
	protected string[] SYSTEM_OPTIONS = new string[3] { "BATU", "KERTAS", "GUNTING" }; 

	public RockPapperScissorGame() {}

	public void run()
	{
		char IsDone = 'Y'; bool IsLose = false, IsDraw = false, IsWin = false; Random Random = new(); string? PlayerOption, ComputerOption;

		while (IsDone != 'N') {
			Console.Clear();
			Console.WriteLine("--------------------------------------------------");
			Console.WriteLine("=== SELAMAT DATANG DI GAME BATU GUNTING KERTAS ===");
			Console.WriteLine("--------------------------------------------------");

			Console.WriteLine("\nPilih antara 3 pilihan dalam game ini:\n1. BATU\n2. GUNTING\n3. KERTAS\n");

			Console.Write("Inputkan pilihan anda: ");
			PlayerOption = Console.ReadLine().ToUpper();
			ComputerOption = SYSTEM_OPTIONS[Random.Next(0, SYSTEM_OPTIONS.Length)];

            if (PlayerOption == null || SYSTEM_OPTIONS.Contains(PlayerOption) == false)
			{
				Console.Write("Pilihan yang anda inputkan tidak valid. Coba lagi.");
			} else
			{
				// Set all value of variables IsDraw, IsLose, and IsWin to false
				IsLose = IsDraw = IsWin = false;

				switch (ComputerOption)
				{
					case "BATU":
						if (PlayerOption == "BATU")
						{
							IsDraw = true;
						} else if (PlayerOption == "KERTAS")
						{
							IsWin = true;
						} else
						{
							IsLose = true;
						}
						break;
					case "KERTAS":
						if (PlayerOption == "KERTAS")
						{
                            IsDraw = true;
                        } else if (PlayerOption == "GUNTING")
						{
                            IsWin = true;
                        } else
						{
							IsLose = true;
						}
						break;
					case "GUNTING":
						if (PlayerOption == "GUNTING")
						{
                            IsDraw = true;
                        } else if (PlayerOption == "BATU")
						{
                            IsWin = true;
                        } else
						{
							IsLose = true;
						}
						break;
					default:
						Console.Write("Pilihan yang anda inputkan tidak valid. Coba lagi.");
						break;
				}

				if (IsWin)
				{
                    Console.WriteLine($"Pilihan kamu adalah \"{PlayerOption}\" sedangkan pilihan komputer adalah \"{ComputerOption}\", maka dari itu KAMU MENANG!");
                } else if (IsLose)
				{
                    Console.WriteLine($"Pilihan kamu adalah \"{PlayerOption}\" sedangkan pilihan komputer adalah \"{ComputerOption}\", maka dari itu KAMU KALAHH!");
                } else if (IsDraw)
				{
                    Console.WriteLine($"Pilihan kamu sama dengan pilihan komputer, yaitu \"{PlayerOption}\"");
                } else
				{
					Console.WriteLine($"Hal aneh terjadi karena kamu telah memasukkan nilai yang tidak seharusnya, yaitu \"{PlayerOption}\"");
				}
			}

			Console.WriteLine("\nIngin bermain lagi? ketik 'Y' untuk mengulang permainan dan 'N' untuk sebaliknya.");
			if (char.TryParse(Console.ReadLine().ToUpper(), out IsDone) == false)
			{
				Console.WriteLine("Aku menangkap apa yang kamu ketik bahwa kamu tidak lagi ingin memainkan game ini lagi");
			}

			// Check if IsDone valid [Y/N] char value and what user choosing
			if (IsDone == 'N')
			{
				Console.WriteLine("Terimakasih telah memainkan permainan batu gunting kertas ini :)");
			}
        }
	}
}
