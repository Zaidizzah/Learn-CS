using System;
using System.Linq;

class SimpleCalculator
{
	protected char[] ValidOperators = { '+', '-', '/', '*', '%', '^' }
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

		Console.write("Inputkan nilai pertama: ");
		int Input1 = Console.readLine();
		Console.write("Inputkan nilai kedua: ");
		int Input2 = Console.readLine();

		int Output;
		switch (this.Operator)
		{
			case '+':
				Output = Input1 + Input2;
				break;
			case '-':
				Output = Input1 - Input2;
				break; 
			case "/":
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

		Console.writeLine($"Hasil dari perhitungan '{Input1} {this.Operator} {Input2}' adalah: {Output}");
    }
}
