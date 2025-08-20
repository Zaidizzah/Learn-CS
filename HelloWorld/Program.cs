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

            TaskLists taskLists = new();
            taskLists.Run();
         }
    }
}