using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PseudoCodeCompiler.FlowChart
{
    public partial class FlowChartPanel : UserControl
    {
        public List<FlowChartItemControl> Roots { get; set; }
        Pen connPen = new Pen(Color.Black, 2);
        Bitmap drawBuffer;

        public FlowChartPanel()
        {
            InitializeComponent();
            Roots = new List<FlowChartItemControl>();
            this.drawBuffer = new Bitmap(this.Width, this.Height);
        }

        public void UpdateTreeBounds()
        {
            if (this.Roots.Count > 0)
            {
                Rectangle treeBounds = this.Roots[0].GetTreeBounds();

                this.Width = treeBounds.Right + 20;
                this.Height = treeBounds.Bottom + 20;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.LocationChanged += new EventHandler(Control_LocationChanged);
            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            e.Control.LocationChanged -= new EventHandler(Control_LocationChanged);
            base.OnControlRemoved(e);
        }

        void Control_LocationChanged(object sender, EventArgs e)
        {
            this.UpdateTreeBounds();
            this.Refresh();
        }

        private void DrawConnections(Graphics g, FlowChartItemControl parent)
        {
            Point parentCenter = new Point(parent.Left + parent.Width / 2,
                parent.Bottom); //parent.Top + parent.Height / 2);

            foreach (FlowChartItemControl child in parent.Children)
            {
                Point childTopCenter = new Point(child.Left + child.Width / 2, child.Top);

                Point p1 = new Point(parentCenter.X, parentCenter.Y);
                Point p2 = new Point(parentCenter.X, childTopCenter.Y - 14);
                g.DrawLine(connPen, p1, p2);

                p1 = new Point(childTopCenter.X, p2.Y);
                g.DrawLine(connPen, p1, p2);
                g.DrawLine(connPen, childTopCenter, p1);

                g.FillPolygon(new SolidBrush(connPen.Color), new Point[]{
                    childTopCenter,
                    new Point (childTopCenter.X - 4, childTopCenter.Y - 8),
                    new Point (childTopCenter.X + 3, childTopCenter.Y - 8)
                    });

                DrawConnections(g, child);
            }

            if (parent.LoopBack != null)
            {
                Point p1 = new Point();
                Point p2 = new Point();
                p1.X = parent.Left;
                p1.Y = parent.Top + parent.Height / 2;

                p2.X = Math.Min(parent.Left, parent.LoopBack.Left) - 14;
                p2.Y = p1.Y;
                g.DrawLine(connPen, p1, p2);

                p1.X = p2.X;
                p1.Y = parent.LoopBack.Top + parent.LoopBack.Height / 2;
                g.DrawLine(connPen, p1, p2);

                p2.Y = p1.Y;
                p2.X = parent.LoopBack.Left;
                g.DrawLine(connPen, p1, p2);

                g.FillPolygon(new SolidBrush(connPen.Color), new Point[]{
                    p2,
                    new Point (p2.X - 8, p2.Y - 4),
                    new Point (p2.X - 8, p2.Y + 3)
                    });
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.drawBuffer.Height < this.Height ||
                this.drawBuffer.Width < this.Width)
            {
                // create a buffer slightly large so this happens less frequently
                this.drawBuffer = new Bitmap(this.Width + 10, this.Height + 10);
            }
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Need to draw the lines
            Graphics g = Graphics.FromImage(drawBuffer);
            g.Clear(Color.White);
            foreach (FlowChartItemControl root in this.Roots)
            {
                DrawConnections(g, root);
            }

            e.Graphics.DrawImageUnscaled(drawBuffer, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}
