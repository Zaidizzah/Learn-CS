using System;

public class Car
{
	public string Model { get; set; }
	public static uint NumbersOfCars { get; set; } = 0;

	public Car(string Model)
	{
		this.Model = Model;
		Car.NumbersOfCars++;
	}
}
