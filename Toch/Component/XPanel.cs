using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Toch.Component
{
    public partial class XPanel : Panel
    {
        private Theme thm = Theme.MSOffice2010_BLUE;

        #region Constructor

        public XPanel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                      ControlStyles.Opaque |
                      ControlStyles.ResizeRedraw |
                      ControlStyles.OptimizedDoubleBuffer |
                      ControlStyles.CacheText | // We gain about 2% in painting by avoiding extra GetWindowText calls
                      ControlStyles.StandardClick,
                      true);

            this.colorTable = new Colortable();
        }

        #endregion

        Colortable colorTable = null;

        public Theme Theme
        {
            get
            {
                if (this.colorTable == Colortable.Office2010Green)
                {
                    return Theme.MSOffice2010_Green;
                }
                else if (this.colorTable == Colortable.Office2010Red)
                {
                    return Theme.MSOffice2010_RED;
                }
                else if (this.colorTable == Colortable.Office2010Pink)
                {
                    return Theme.MSOffice2010_Pink;
                }
                else if (this.colorTable == Colortable.Office2010Yellow)
                {
                    return Theme.MSOffice2010_Yellow;
                }
                else if (this.colorTable == Colortable.Office2010White)
                {
                    return Theme.MSOffice2010_WHITE;
                }
                else if (this.colorTable == Colortable.Office2010Publisher)
                {
                    return Theme.MSOffice2010_Publisher;
                }

                return Theme.MSOffice2010_BLUE;
            }

            set
            {
                this.thm = value;

                if (thm == Theme.MSOffice2010_Green)
                {
                    this.colorTable = Colortable.Office2010Green;
                }
                else if (thm == Theme.MSOffice2010_RED)
                {
                    this.colorTable = Colortable.Office2010Red;
                }
                else if (thm == Theme.MSOffice2010_Pink)
                {
                    this.colorTable = Colortable.Office2010Pink;
                }
                else if (thm == Theme.MSOffice2010_WHITE)
                {
                    this.colorTable = Colortable.Office2010White;
                }
                else if (thm == Theme.MSOffice2010_Yellow)
                {
                    this.colorTable = Colortable.Office2010Yellow;
                }
                else if (thm == Theme.MSOffice2010_Publisher)
                {
                    this.colorTable = Colortable.Office2010Publisher;
                }
                else
                {
                    this.colorTable = Colortable.Office2010Blue;
                }
            }
        }

        #region Path

        public static GraphicsPath GetRoundedRect(RectangleF r, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.StartFigure();
            r = new RectangleF(r.Left, r.Top, r.Width, r.Height);
            if (radius <= 0.0F || radius <= 0.0F)
            {
                gp.AddRectangle(r);
            }
            else
            {
                gp.AddArc((float)r.X, (float)r.Y, radius, radius, 180, 90);
                gp.AddArc((float)r.Right - radius, (float)r.Y, radius - 1, radius, 270, 90);
                gp.AddArc((float)r.Right - radius, ((float)r.Bottom) - radius - 1, radius - 1, radius, 0, 90);
                gp.AddArc((float)r.X, ((float)r.Bottom) - radius - 1, radius - 1, radius, 90, 90);
            }
            gp.CloseFigure();
            return gp;
        }

        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            this.PaintTransparentBackground(e.Graphics, e.ClipRectangle);

            #region Painting

            //now let's we begin painting
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            #endregion

            #region Color

            Color tTopColorBegin = this.colorTable.ButtonNormalColor1;
            Color tTopColorEnd = this.colorTable.ButtonNormalColor2;
            Color tBottomColorBegin = this.colorTable.ButtonNormalColor3;
            Color tBottomColorEnd = this.colorTable.ButtonNormalColor4;
            Color Textcol = this.colorTable.TextColor;


            if (!this.Enabled)
            {
                tTopColorBegin = Color.FromArgb((int)(tTopColorBegin.GetBrightness() * 255),
                    (int)(tTopColorBegin.GetBrightness() * 255),
                    (int)(tTopColorBegin.GetBrightness() * 255));
                tBottomColorEnd = Color.FromArgb((int)(tBottomColorEnd.GetBrightness() * 255),
                    (int)(tBottomColorEnd.GetBrightness() * 255),
                    (int)(tBottomColorEnd.GetBrightness() * 255));
            }


            #endregion

            #region Theme 2010

            if (thm == Theme.MSOffice2010_BLUE || thm == Theme.MSOffice2010_Green || thm == Theme.MSOffice2010_Yellow || thm == Theme.MSOffice2010_Publisher ||
                thm == Theme.MSOffice2010_RED || thm == Theme.MSOffice2010_WHITE || thm == Theme.MSOffice2010_Pink)
            {
                Paint2010Background(e, g, tTopColorBegin, tTopColorEnd, tBottomColorBegin, tBottomColorEnd);
            }

            #endregion
        }

        #endregion

        #region Paint 2010 Background

        protected void Paint2010Background(PaintEventArgs e, Graphics g, Color tTopColorBegin, Color tTopColorEnd, Color tBottomColorBegin, Color tBottomColorEnd)
        {
            int _roundedRadiusX = 6;

            Rectangle r = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, e.ClipRectangle.Height);
            Rectangle j = r;
            Rectangle r2 = r;
            r2.Inflate(-1, -1);
            Rectangle r3 = r2;
            r3.Inflate(-1, -1);

            //rectangle for gradient, half upper and lower
            RectangleF halfup = new RectangleF(r.Left, r.Top, r.Width, r.Height);
            RectangleF halfdown = new RectangleF(r.Left, r.Top + (r.Height / 2) - 1, r.Width, r.Height);

            //BEGIN PAINT BACKGROUND
            //for half upper, we paint using linear gradient
            using (GraphicsPath thePath = GetRoundedRect(r, _roundedRadiusX))
            {
                LinearGradientBrush lgb = new LinearGradientBrush(halfup, tBottomColorEnd, tBottomColorBegin, 90f, true);

                Blend blend = new Blend(4);
                blend.Positions = new float[] { 0, 0.18f, 0.35f, 1f };
                blend.Factors = new float[] { 0f, .4f, .9f, 1f };
                lgb.Blend = blend;
                g.FillPath(lgb, thePath);
                lgb.Dispose();

                //for half lower, we paint using radial gradient
                using (GraphicsPath p = new GraphicsPath())
                {
                    p.AddEllipse(halfdown); //make it radial
                    using (PathGradientBrush gradient = new PathGradientBrush(p))
                    {
                        gradient.WrapMode = WrapMode.Clamp;
                        gradient.CenterPoint = new PointF(Convert.ToSingle(halfdown.Left + halfdown.Width / 2), Convert.ToSingle(halfdown.Bottom));
                        gradient.CenterColor = tBottomColorEnd;
                        gradient.SurroundColors = new Color[] { tBottomColorBegin };

                        blend = new Blend(4);
                        blend.Positions = new float[] { 0, 0.15f, 0.4f, 1f };
                        blend.Factors = new float[] { 0f, .3f, 1f, 1f };
                        gradient.Blend = blend;

                        g.FillPath(gradient, thePath);
                    }
                }
                //END PAINT BACKGROUND

                //BEGIN PAINT BORDERS
                using (GraphicsPath gborderDark = thePath)
                {
                    using (Pen p = new Pen(tTopColorBegin, 1))
                    {
                        g.DrawPath(p, gborderDark);
                    }
                }
                using (GraphicsPath gborderLight = GetRoundedRect(r2, _roundedRadiusX))
                {
                    using (Pen p = new Pen(tTopColorEnd, 1))
                    {
                        g.DrawPath(p, gborderLight);
                    }
                }
                using (GraphicsPath gborderMed = GetRoundedRect(r3, _roundedRadiusX))
                {
                    SolidBrush bordermed = new SolidBrush(Color.FromArgb(50, tTopColorEnd));
                    using (Pen p = new Pen(bordermed, 1))
                    {
                        g.DrawPath(p, gborderMed);
                    }
                }
                //END PAINT BORDERS
            }
        }

        #endregion

        #region Paint TEXT AND IMAGE

        #endregion

        #region Paint Transparent Background

        protected void PaintTransparentBackground(Graphics g, Rectangle clipRect)
        {
            // check if we have a parent
            if (this.Parent != null)
            {
                // convert the clipRects coordinates from ours to our parents
                clipRect.Offset(this.Location);

                PaintEventArgs e = new PaintEventArgs(g, clipRect);

                // save the graphics state so that if anything goes wrong 
                // we're not fubar
                GraphicsState state = g.Save();

                try
                {
                    // move the graphics object so that we are drawing in 
                    // the correct place
                    g.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);

                    // draw the parents background and foreground
                    this.InvokePaintBackground(this.Parent, e);
                    this.InvokePaint(this.Parent, e);

                    return;
                }
                finally
                {
                    // reset everything back to where they were before
                    g.Restore(state);
                    clipRect.Offset(-this.Location.X, -this.Location.Y);
                }
            }

            // we don't have a parent, so fill the rect with
            // the default control color
            g.FillRectangle(SystemBrushes.Control, clipRect);
        }

        #endregion
    }
}