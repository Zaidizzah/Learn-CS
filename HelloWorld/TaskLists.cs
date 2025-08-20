using System;
using System.Linq;
using System.Text.RegularExpressions;

public class TaskLists
{
    protected string?[] Tasks = new string[5];
    // Display keyword helping to manage tasklists
    protected string HelpKeyword = "\nKetik '--create' untuk membuat tugas baru.\nKetik '--exit' untuk keluar dari aplikasi.\nKetik '--edit {id 1-5}' untuk mengedit tasks yang ada.\nKetik '--delete {id 1-5}' untuk menghapus atau mereset tasks yang ada.\nKetik '--destroy' untuk menghapus atau meresett task yang ada.";

    // Constructor
    public TaskLists() { }

    public void Run()
    {
        Console.Clear();
        Console.WriteLine("=== SELAMAT DATANG DI APLIKASI TASK LIST ===");

        while (true)
        {
            Console.WriteLine(this.HelpKeyword);
            this.Display();

            Console.Write("\nMasukkan perintah: ");
            string? command = Console.ReadLine()?.Trim().ToLower();

            if (command == "--exit")
            {
                break;
            }
            else if (command == "--create")
            {
                this.Create();
            } 
            else if (Regex.IsMatch(command, @"^(--edit|--delete) [1-5]$"))
            {
                string[] commands = command.Split(' ');
                command = commands[0];
                
                if (ushort.TryParse(commands[1], out ushort Index) == false)
                {
                    Console.WriteLine("Nilai ID tidak valid mungkin nilai yang kamu masukkan bukan angka. Silakan coba lagi.");
                } else
                {
                    if (command == "--edit")
                    {
                        this.Edit(Index);
                    } else if (command == "--delete")
                    {
                        this.Delete(Index);
                    } else
                    {
                        Console.WriteLine("Perintah tidak valid. Silakan coba lagi.");
                    }
                }
            }
            else if (command == "--destroy")
            {
                this.Destroy();
            }
            else
            {
                Console.WriteLine("Perintah tidak valid. Silakan coba lagi.");
            }

            Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
            Console.ReadLine();

            // Clear history of console command
            Console.Clear();
        }
    }

    public bool SetTask(ushort Index, string? TaskName)
    {
        if (Index >= 0 && Index < this.Tasks.Length)
        {
            this.Tasks[Index] = TaskName;
            return true;
        }
        return false;
    }

    public string? GetTask(ushort Index)
    {
        if (Index >= 0 && Index < this.Tasks.Length)
        {
            return this.Tasks[Index];
        }
        return null;
    }

    public bool ValidateIndex(ushort Index)
    {
        // Check if Index is not in range of 1 - 5
        if (Index > 5) return false;

        return true;
    }

    public bool Display()
    {
        bool isAnyTask = this.Tasks.Any(task => !string.IsNullOrWhiteSpace(task));

        if (!isAnyTask)
        {
            Console.WriteLine("\nAnda belum memiliki tugas sama sekali saat ini.");
            return false;
        }
        else
        {
            Console.WriteLine("\n--- Daftar Tugas Anda ---");
            for (ushort Index = 0; Index < this.Tasks.Length; Index++)
            {
                if (this.GetTask(Index) != null && this.GetTask(Index).Trim() != "")
                {
                    Console.WriteLine($"{Index + 1}. {this.GetTask(Index)}");
                }
            }
            return true;
        }
    }

    public bool Create()
    {
        // Cari indeks pertama yang kosong
        short emptyIndex = -1;
        for (ushort Index = 0; Index < this.Tasks.Length; Index++)
        {
            if (this.GetTask(Index) == null || this.GetTask(Index).Trim() == "")
            {
                emptyIndex = Convert.ToInt16(Index);
                break;
            }
        }

        if (emptyIndex == -1)
        {
            Console.WriteLine("Maaf, daftar tugas sudah penuh.");
            return false;
        }

        Console.Write("Buat tugas baru. Masukkan deskripsi tugas: ");
        string? newTask = Console.ReadLine();

        if (newTask == null || newTask.Trim() == "")
        {
            Console.WriteLine("Maaf, deskripsi tugas tidak boleh kosong.");
            return false;
        }

        this.SetTask(Convert.ToUInt16(emptyIndex), newTask);
        Console.WriteLine("Tugas berhasil ditambahkan!");
        return true;
    }

    public bool Edit(ushort Index)
    {
        // Check if Index is not in range of 1 - 5
        if (this.ValidateIndex(Index) == false)
        {
            Console.WriteLine("Nilai ID tidak valid untuk melanjutkan proses edit task, mungkin ID bukan dari rentang 1-5. Silahkan coba lagi.");
            return false;
        }

        // reduce 1 value of Index
        Index--;

        Console.Write("Ubah tugas terkait. Masukkan deskripsi tugas baru: ");
        string? newTask = Console.ReadLine();
        
        this.SetTask(Index, newTask);
        Console.WriteLine($"Nilai untuk task dengan ID {(ushort)(Index + 1)} berhasil diubah.");
        return true;
    }

    public bool Delete(ushort Index)
    {
        // Check if Index is not in range of 1 - 5
        if (this.ValidateIndex(Index) == false)
        {
            Console.WriteLine("Nilai ID tidak valid untuk melanjutkan proses delete task, mungkin ID bukan dari rentang 1-5. Silahkan coba lagi.");
            return false;
        }

        // reduce 1 value of Index
        Index--;

        if (this.GetTask(Index) == null) {
            Console.WriteLine($"Nilai untuk task dengan ID {(ushort)(Index + 1)} tidak terdefinisi sebelumnya. lakukan penhapusan task untuk nilai yang sudah anda tambahkan sebelumnya.");
        } else
        {
            this.SetTask(Index, null);
            Console.WriteLine($"Nilai untuk task dengan ID {(ushort)(Index + 1)} berhasil dihapus.");
        }
        return true;
    }

    public bool Destroy()
    {
        for (ushort Index = 0; Index < this.Tasks.Length; Index++)
        {
            if (this.GetTask(Index) != null)
            {
                this.Tasks[Index] = null;
            }

            continue;
        }

        Console.WriteLine($"Tasks list berhasil dikosongkan.");
        return true;
    }
}