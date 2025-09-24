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

            //string[,] ArrayVegetables = new string[3, 3]
            //{
            //    {
            //        "Jeruk", "Mangga", "Alpukat"
            //    },
            //    {
            //        "Wortel", "Selada", "Terong"
            //    },
            //    {
            //        "Daging Ayam", "Daging Sapi", "Daging Domba"
            //    }
            //};
            //Program.MultidimesionalArrays(ArrayVegetables);

            // Static is different i think in C#
            //Car Car1 = new("Mercedes");
            //Car Car2 = new("Nascar");

            //Console.WriteLine($"Jumlah mobil yang ada diarena balapan adalah: {Car.NumbersOfCars}");


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

        // Learning how multidimensional array work in C#
        public static void MultidimesionalArrays(string[,] Arrays)
        {
            // Foreach 
            foreach (string Food in Arrays)
            {
                Console.WriteLine($"The item is: '{Food}'");
            }
        }
    }
}