using ExpressCraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressCraftPaint
{
    public class frmMain : Form
    {
        public bool IsMouseDown = false;

        public static void Main()
        {
            new frmMain().Show();
        }

        public frmMain()
        {
            this.Text = "Paint";

            var x = new CanvasControl();
            Point prevPoint = null;
            x.ClearOnResize = false;

            var g = x.CreateGraphics();
            var pen = new Pen(new SolidBrush(Color.Black));
            
            x.MouseDown += (s, ev) =>
            {
                IsMouseDown = true;
                prevPoint = new Point() { X = (int)ev.layerX, Y = (int)ev.layerY };
            };

            x.MouseUp += (s, ev) =>
            {
                if(IsMouseDown)
                {
                    IsMouseDown = false;
                    var current = new Point() { X = (int)ev.layerX, Y = (int)ev.layerY };

                    g.DrawLine(pen, prevPoint, current);

                    prevPoint = current;
                }                
            };

            x.MouseMove += (s, ev) =>
            {
                if (!IsMouseDown)
                    return;

                var current = new Point() { X = (int)ev.layerX, Y = (int)ev.layerY };

                g.DrawLine(pen, prevPoint, current);

                prevPoint = current;
            };


            x.SetBoundsFull();
            this.StartPosition = FormStartPosition.Center;
            this.LinkResize(x, true);
        }
    }
}
