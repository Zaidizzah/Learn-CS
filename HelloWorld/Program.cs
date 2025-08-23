using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //GuessingGame Game = new(2, 1);
            //Game.GuesingNumberGame();

            //SimpleCalculator Calculator = new('+');
            //Calculator.Calculate();

            //TaskLists taskLists = new();
            //taskLists.Run();

            //AuthenticateAPP Authenticate = new();
            //Authenticate.run();

            //Program.Calculate(10, 15, 15);
        }

        // Learning how params keyword work in C# methods
        public static void Calculate(params uint[] Nums)
        {
            uint Total = 0;
            for (ushort Index = 0; Index < Nums.Length; Index++)
            {
                Total += Nums[Index];
            }

            Console.WriteLine($"Total is {Total}");
        }
    }
}