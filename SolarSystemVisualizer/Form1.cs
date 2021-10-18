using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json;

namespace SolarSystemVisualizer
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent();
			this.backPanel.MouseWheel += backPanel_MouseWheel;
		}

		GFX engine;
		new private float Scale = 16.0F;
		private const float ScaleDelta = 0.4F;
		private const float MaxScale = 200.0F;
		private const float MinScale = 10.0F;

		private Point Center;
		private Panel InfoPanel;



		private void Form1_Load (object sender, EventArgs e)
		{
			ReadDefaultPlanets();
			Center = new Point(backPanel.Width / 2, backPanel.Height / 2);
		}

		private void Form1_Resize (object sender, EventArgs e)
		{
			Center = new Point(backPanel.Width / 2, backPanel.Height / 2);
			if (engine != null) DrawPlanets();
		}

		private void backPanel_Click (object sender, EventArgs e)
		{
			//Stuff goes here

		}

		private void backPanel_MouseWheel (object sender, MouseEventArgs e)
		{
			Console.WriteLine("Triggered! Direction = " + (e.Delta > 0 ? "Up" : "Down"));

			if(e.Delta < 0)
			{
				if(Scale == MinScale) return;

				if(Scale > MinScale)
					Scale -= ScaleDelta * (Scale/8);
			}
			else
			{
				if(Scale == MaxScale) return;

				if(Scale + ScaleDelta < MaxScale)
					Scale += ScaleDelta * (Scale / (ModifierKeys == Keys.Shift ? 2 : 16));
			}

			DrawPlanets();
			Console.WriteLine(ModifierKeys == Keys.Shift);
			Console.WriteLine("Current Scale: " + Scale);
		}

		private void backPanel_Paint (object sender, PaintEventArgs e)
		{
			Graphics g = (sender as Panel).CreateGraphics();
			engine = new GFX(g);

			DrawPlanets();
		}

		private void ReadDefaultPlanets ()
		{
			DateTime now = DateTime.UtcNow;

			Planet[] planets = new Planet[7];
			planets = JsonConvert.DeserializeObject<Planet[]>(File.ReadAllText("DefaultPlanets.json"));

			Planet.Planets.Clear();
			foreach (Planet p in planets)
			{
				Planet.Planets.Add(p);
				p.Format(now);
				p.PrintInfo();
			}
		}

		private void CreateInfoPanel (Planet planet)
		{
			RemoveInfoPanel();

			InfoPanel = new Panel() {
				Dock = DockStyle.Right,
				Size = new Size(600, 1),
				BackColor = Color.Gray
			};


			Label title = new Label();
			title.Text = planet.Name;
			title.TextAlign = ContentAlignment.MiddleCenter;
			title.Font = new Font("SegoeUI", 24);
			title.Size = new Size(400, 33);
			title.BackColor = Color.DarkGray;
			title.Location = new Point(InfoPanel.Width / 2 - title.Width / 2, 50);

			InfoPanel.Controls.Add(title);


			this.Controls.Add(InfoPanel);
			DrawPlanets();
		}

		private void RemoveInfoPanel ()
		{
			if(InfoPanel == null)
				return;

			this.Controls.Remove(InfoPanel);
			InfoPanel = null;

			DrawPlanets();
		}

		private void DrawPlanets ()
		{
			Point center = new Point(backPanel.Width / 2, backPanel.Height / 2);
			engine.Clear(new Rectangle(backPanel.Location, backPanel.Size));

			//Sun
			engine.DrawCircle(center, Convert.ToInt32(0.31 * Scale), Color.Yellow);

			//Orbits
			foreach (Planet p in Planet.Planets)
			{
				Rectangle orbitArea = new Rectangle(
					new Point(Convert.ToInt32(center.X - p.Distance * Scale), Convert.ToInt32(center.Y - p.Distance * Scale)), 
					new Size(Convert.ToInt32(p.Distance*2*Scale), Convert.ToInt32(p.Distance*2*Scale))
				);

				engine.DrawOrbit(orbitArea, Color.Gray, 3);

				double angle = p.Angle * Math.PI / 180;

				engine.DrawCircle(new Point(Convert.ToInt32(center.X - (p.Distance * Scale * Math.Cos(angle))), Convert.ToInt32(center.Y - (p.Distance * Scale * Math.Sin(angle)))), Convert.ToInt32(p.ApparentSize * Scale), p.Color);
			}
		}

		private void button1_Click (object sender, EventArgs e)
		{
			ReadDefaultPlanets();
		}

		private void button2_Click (object sender, EventArgs e)
		{
			CreateInfoPanel(Planet.Planets[2]);
		}

		private void button3_Click (object sender, EventArgs e)
		{
			RemoveInfoPanel();
		}
	}
}
