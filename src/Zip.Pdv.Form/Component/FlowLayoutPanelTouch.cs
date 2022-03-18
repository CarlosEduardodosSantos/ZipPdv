using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zip.Pdv.Component
{
    public class FlowLayoutPanelTouch
    {
        private Point _mouseDownPoint;
        private FlowLayoutPanel _flowLayoutPanel;
        public FlowLayoutPanelTouch(FlowLayoutPanel flowLayoutPanel)
        {
            _flowLayoutPanel = flowLayoutPanel;

            AssingEvent(flowLayoutPanel);
        }
        private void AssingEvent(Control control)
        {
            control.MouseDown += Control_MouseDown;
            control.MouseMove += Control_MouseMove;

            foreach (Control item in control.Controls)
            {
                AssingEvent(item);
            }
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _mouseDownPoint = Cursor.Position;
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Point pointDirefference = new Point(Cursor.Position.X + _mouseDownPoint.X, Cursor.Position.Y - _mouseDownPoint.Y);

            if (_mouseDownPoint.X == Cursor.Position.X && _mouseDownPoint.Y == Cursor.Position.Y) return;

            Point currAutoS = _flowLayoutPanel.AutoScrollPosition;
            _flowLayoutPanel.AutoScrollPosition = new Point(Math.Abs(currAutoS.X) - pointDirefference.X, Math.Abs(currAutoS.Y) - pointDirefference.Y);
            _mouseDownPoint = Cursor.Position;
        }
    }
}
