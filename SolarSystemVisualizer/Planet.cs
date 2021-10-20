using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SolarSystemVisualizer
{
	public class Planet
	{
		public static List<Planet> Planets = new List<Planet>();



		public string Name { get; set; }
		public float Distance { get; set; }
		public float Diameter { get; set; }
		public float ApparentSize { get; set; }
		public float OrbitalPeriod { get; set; }
		public float ReferenceAngle { get; set; }
		public float Angle { get; set; }
		public String ColorString { get; set; }
		public Color Color { get; set; }

		

		//Methods
		public void PrintInfo ()
		{
			Console.WriteLine("");
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("Name: " + Name);
			Console.WriteLine("Distance to the sun: " + Distance + " AU");
			Console.WriteLine("Planets diameter: " + Diameter + " km");
			Console.WriteLine("Apparent size: " + ApparentSize + " px");
			Console.WriteLine("Color: " + Color.Name);
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("Orbital period: " + OrbitalPeriod + " hours per revolution around the sun");
			Console.WriteLine("leading to " + (360/OrbitalPeriod)*24 + "° revolutions per day");
			Console.WriteLine("Angle at 1920-01-01 was " + ReferenceAngle + "°");
			Console.WriteLine("Angle now is " + Angle + "°");
			Console.WriteLine("-------------------------------------");
			Console.WriteLine("");
		}

		public void Format (DateTime time)
		{
			Color = Color.FromName(ColorString);
			Angle = (ReferenceAngle + (float)((time - new DateTime(1980, 1, 1)).TotalHours * (24 * 360/OrbitalPeriod))) % 360;
		}
	}
}
