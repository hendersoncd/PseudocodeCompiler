using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PseudoCodeCompiler.Runtime.Instructions;
using System.Drawing.Drawing2D;

namespace PseudoCodeCompiler.FlowChart
{
    public partial class FlowChartItemControl : UserControl
    {
        public const int CELL_SPACING = 20;
        public const int CELL_PADDING = 20;
        public const int DECISION_CELL_PADDING = 50;
        private Bitmap drawBuffer;

        Font itemFont = new Font(FontFamily.GenericSansSerif, 8);
        PsudoInstruction inst = null;

        public List<FlowChartItemControl> Children { get; set; }
        public List<FlowChartItemControl> Parents { get; set; }
        public FlowChartItemControl LoopBack { get; set; }
        private int requiredWidth = 0;

        public bool IsStartEndProgram { get; set; }
        public bool IsLabelNode { get; set; }
        private Pen outlinePen = new Pen(Color.Black, 3);
        private bool autoPlacementEnabled = true;

        public FlowChartItemControl(string text)
            : this()
        {
            Text = text;

            this.UpdateCellSize();
            requiredWidth = this.Width;

            DrawShape(Graphics.FromImage(drawBuffer));
           // this.BackgroundImage = drawBuffer;
        }

        private void UpdateCellSize()
        {
            Size textSize = TextRenderer.MeasureText(Text, this.itemFont);
            int pad = this.Children.Count <= 1 ? CELL_SPACING : DECISION_CELL_PADDING;
            this.Width = Math.Max(textSize.Width + pad, 100);
            this.Height = textSize.Height + pad;

            drawBuffer = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        public FlowChartItemControl(PsudoInstruction instruction)
            : this(instruction.ToString())
        {
            inst = instruction;
        }

        protected FlowChartItemControl()
        {
            InitializeComponent();
            this.Parents = new List<FlowChartItemControl>();
            this.Children = new List<FlowChartItemControl>();
            LoopBack = null;
            IsStartEndProgram = false;
            IsLabelNode = false;
        }

        public void AddChild(FlowChartItemControl item)
        {
            item.Parents.Add(this);              
            this.Children.Add(item);
            item.ParentLocationChanged(this, EventArgs.Empty);
            if (this.Children.Count > 1)
                item.SiblingLocationChanged(this.Children[this.Children.Count - 2], EventArgs.Empty);

            UpdateCellSize();
            this.UpdateRequiredWidth();
            DrawShape(Graphics.FromImage(drawBuffer));
        }

        public void DisableAutoPlacement()
        {
            autoPlacementEnabled = false;
            foreach (FlowChartItemControl child in Children)
                child.DisableAutoPlacement();
        }

        protected void UpdateRequiredWidth()
        {
            int newWidth = 0;
                foreach (FlowChartItemControl child in this.Children)
                    newWidth += child.requiredWidth + CELL_SPACING;
                newWidth -= CELL_SPACING;

            if (this.Children.Count == 1)
            {
                if (this.Children[0].Parents.Count > 1)
                {
                    newWidth -= (this.Children[0].Parents.Count - 1) * CELL_SPACING;
                    newWidth /= this.Children[0].Parents.Count;
                }
            }

            newWidth = Math.Max(newWidth, this.Width);

            if (newWidth != this.requiredWidth)
            {
                this.requiredWidth = newWidth;
                foreach (FlowChartItemControl parent in this.Parents)
                    parent.UpdateRequiredWidth();
            }
        }

        public int GetChildWidth()
        {
            return requiredWidth;
        }

        protected void SiblingLocationChanged(object sender, EventArgs e)
        {
            if (!autoPlacementEnabled)
                return;

            FlowChartItemControl bro = ((FlowChartItemControl)sender);

            this.Location = new Point(
                bro.Right + (bro.requiredWidth - bro.Width) / 2 + CELL_SPACING,
                bro.Top
                );
        }

        protected void ParentLocationChanged(object sender, EventArgs e)
        {
            if (!autoPlacementEnabled)
                return;

            // Do I have only one parent?
            if (this.Parents.Count == 1)
            {
                // am I the only child?
                if (Parents[0].Children.Count == 1)
                {
                    // If so, set me right below my parent
                    this.Location = new Point(
                        ((this.Parents[0].requiredWidth - this.requiredWidth) 
                            + (this.Parents[0].Width - this.Width)) / 2 + this.Parents[0].Left,
                        this.Parents[0].Bottom + CELL_SPACING
                        );
                }
                // more than one child. 
                else
                {
                    int farLeft = this.Parents[0].Left - (this.Parents[0].GetChildWidth() - this.Parents[0].Width) / 2;
                    int myLeft = farLeft + 10 + (this.requiredWidth - this.Width)/2;
                    if (Parents[0].Children[0] == this)
                    {
                        this.Location = new Point(
                            myLeft,
                            this.Parents[0].Bottom + CELL_SPACING
                            );
                       // this.Location = new Point(
                       //     this.Parents[0].Left - (this.Parents[0].GetChildWidth() - this.Parents[0].Width) / 2,
                       //     this.Parents[0].Bottom + CELL_SPACING
                       //     );
                    }
                }
            }
            else // multiple parents
            {
                int minLeft = int.MaxValue;
                int maxRight = 0;
                int maxBottom = 0;

                foreach (FlowChartItemControl parent in this.Parents)
                {
                    minLeft = Math.Min(minLeft, parent.Left);
                    maxRight = Math.Max(maxRight, parent.Right);
                    maxBottom = Math.Max(maxBottom, parent.Bottom);
                }

                int trueWidth = this.requiredWidth;// *this.Parents.Count;
                this.Location = new Point(
                    minLeft + (maxRight - minLeft - this.Width) / 2,
                    maxBottom + CELL_SPACING
                    );
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            foreach (FlowChartItemControl child in this.Children)
                child.ParentLocationChanged(this, e);
            if (this.Parents.Count == 1 && this.Parents[0].Children.Count > 1)
            {
                for (int i = 0; i < this.Parents[0].Children.Count - 1; i++)
                {
                    if (this.Parents[0].Children[i] == this)
                    {
                        this.Parents[0].Children[i + 1].SiblingLocationChanged(this, e);
                        break;
                    }
                }
            }
        }

        protected void DrawShape(Graphics g)
        {
            g.Clear(Color.White);
            if (IsStartEndProgram)
            {
                Rectangle rect = new Rectangle(1, 1, this.Height-3, this.Height-2);
                g.DrawArc(outlinePen, rect, 90, 180);
                Rectangle rect2 = new Rectangle(new Point(this.Width-rect.Width-3, rect.Y), rect.Size);

                g.DrawArc(outlinePen, rect2, 270, 180);

                g.DrawLine(outlinePen, rect.Right - rect.Width / 2, rect.Top, rect2.Left + rect.Width / 2, rect2.Top);
                g.DrawLine(outlinePen, rect.Right - rect.Width / 2, rect.Bottom, rect2.Left + rect.Width / 2, rect2.Bottom);
            }
            else if (IsLabelNode)
            {
                Rectangle rect = new Rectangle(1, 1, this.Height/3 - 3, this.Height/2 - 2);
                g.DrawArc(outlinePen, rect, 90, 180);
                Rectangle rect2 = new Rectangle(new Point(this.Width - rect.Width - 3, rect.Y), rect.Size);
                g.DrawArc(outlinePen, rect2, 90, 180);
                rect.Y = rect.Bottom;
                rect2.Y = rect2.Bottom;
                g.DrawArc(outlinePen, rect, 270, 180);
                g.DrawArc(outlinePen, rect2, 270, 180);

                g.DrawLine(outlinePen, rect.Right - rect.Width / 2, 1, rect2.Left + rect.Width / 2, 1);
                g.DrawLine(outlinePen, rect.Right - rect.Width / 2, rect.Bottom, rect2.Left + rect.Width / 2, rect2.Bottom);
            }
            else if (this.Children.Count > 1)
            {
                Point left = new Point(0, this.Height / 2);
                Point right = new Point(this.Width, this.Height / 2);
                Point top = new Point(this.Width / 2, 0);
                Point bottom = new Point(this.Width / 2, this.Height);

                g.DrawLine(outlinePen, left, top);
                g.DrawLine(outlinePen, top, right);
                g.DrawLine(outlinePen, right, bottom);
                g.DrawLine(outlinePen, bottom, left);
            }
            else
            {
                g.DrawRectangle(outlinePen, 0, 0, this.Width - 1, this.Height - 1);
            }

            Size textSize = TextRenderer.MeasureText(Text, this.itemFont);
            int pad = this.Children.Count <= 1 ? CELL_SPACING : DECISION_CELL_PADDING;
            int extraSpace = this.Width - (textSize.Width + pad);
            g.DrawString(this.Text, itemFont, Brushes.Black, (extraSpace + pad) / 2, pad / 2);

           // string debug = string.Format("W:{0} P:{1} C:{2}", this.requiredWidth, this.Parents.Count, this.Children.Count);
           // g.DrawString(debug, itemFont, Brushes.Black, 10, 10);
        }

        public Rectangle GetTreeBounds()
        {
            Rectangle bounds = this.Bounds;
            foreach (FlowChartItemControl child in Children)
            {
                Rectangle childBounds = child.GetTreeBounds();

                int y = Math.Min(bounds.Top, childBounds.Top);
                int x = Math.Min(bounds.Left, childBounds.Left);
                int w = Math.Max(bounds.Right, childBounds.Right) - x;
                int h = Math.Max(bounds.Bottom, childBounds.Bottom) - y;

                bounds.Y = y;
                bounds.X = x;
                bounds.Width = w;
                bounds.Height = h;
            }

            return bounds;
        }

        bool isMouseDown = false;
        Point startMouseLoc = new Point();
        Point startControlLoc = new Point();

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isMouseDown = true;
            startMouseLoc = Cursor.Position;
            startControlLoc = this.Location;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point newLoc = new Point(
                    startControlLoc.X + (Cursor.Position.X - startMouseLoc.X),
                    startControlLoc.Y + (Cursor.Position.Y - startMouseLoc.Y)
                    );
                if (newLoc.X != this.Location.X || newLoc.Y != this.Location.Y)
                {
                    this.Location = newLoc;
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isMouseDown = false;
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(this.drawBuffer, 0, 0);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}
