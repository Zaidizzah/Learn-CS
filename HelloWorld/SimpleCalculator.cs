using System;
using System.Linq;

class SimpleCalculator
{
	protected char[] ValidOperators = { '+', '-', '/', '*', '%', '^' };
	public char Operator { get; set; }

	public SimpleCalculator(char Operator) 
	{
		// Check if operator is not one of (+, -, /, *, %, ^)
		if (this.ValidOperators.Contains(Operator) == false)
		{
			throw new IndexOutOfRangeException($"operator '{Operator}' is not valid.");
        }

		this.Operator = Operator;
	}


	public void Calculate()
	{
        // Clear the command/console window first
        Console.Clear();
        Console.WriteLine("=== SEALAMAT DATANG APLIKASI KALKULATOR SEDERHANA ===");

        int Input1,
            Input2;
		Console.Write("Inputkan nilai pertama: ");
		if (int.TryParse(Console.ReadLine(), out Input1) == false)
		{
			Console.WriteLine("Nilai pertama yang kamu inputkan tidak valid atau bukan angka valid.");
			return;
		}
		Console.Write("Inputkan nilai kedua: ");
        if (int.TryParse(Console.ReadLine(), out Input2) == false)
        {
            Console.WriteLine("Nilai kedua yang kamu inputkan tidak valid atau bukan angka valid.");
            return;
        }

        int Output;
        switch (this.Operator)
		{
			case '+':
				Output = Input1 + Input2;
				break;
			case '-':
				Output = Input1 - Input2;
				break; 
			case '/':
				Output = Input1 / Input2;
				break;
			case '^': 
				Output = Input1 ^ Input2;
				break;
			case '*':
				Output = Input1 * Input2;
				break;
			case '%': 
				Output = Input1 % Input2;
				break;
			default:
                throw new IndexOutOfRangeException($"operator '{Operator}' is not valid.");
				break;
        }

		Console.WriteLine($"Hasil dari perhitungan '{Input1} {this.Operator} {Input2}' adalah: {Output}");
    }
}
