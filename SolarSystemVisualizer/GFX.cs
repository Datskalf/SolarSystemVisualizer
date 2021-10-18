using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SolarSystemVisualizer
{
	class GFX
	{
		Graphics gfx;
		private Pen orbitPen = new Pen(Color.Gray, 3);
		private Brush bgBrush = new SolidBrush(Color.Black);

		public GFX(Graphics g)
		{
			gfx = g;
		}



		#region Methods

		public void Clear (Rectangle areaToClear)
		{
			gfx.FillRectangle(bgBrush, areaToClear);
		}

		public void DrawCircle (Point loc, int radius, Color color)
		{
			gfx.FillEllipse(new SolidBrush(color), new Rectangle(new Point(Convert.ToInt32(loc.X - (radius / 2)), Convert.ToInt32(loc.Y - (radius / 2))), new Size(radius, radius)));
		}

		public void DrawOrbit (Rectangle area, Color color, int width)
		{
			gfx.DrawArc(new Pen(color), area, 0, 360);
		}

		#endregion
	}
}
