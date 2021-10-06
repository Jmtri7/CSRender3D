using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace test {
    class Program : Form {
        static void Main(string[] args) {
            Application.Run(new Program());
        }

        public Program() {
            this.Name = "CS3DRenderer";
            this.Text = "3DRenderer";
            this.Size = new System.Drawing.Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            ResizeRedraw = true;

            Vector vector = Vector.Polar(10, 90, 0);

            for(int i = 0; i < 9; i++) {
                vector.Rotate(0, 45 * i).PrintPolar();
            }
        }

        protected override void OnPaint(PaintEventArgs pea) {
            Pen pen = new Pen(ForeColor);

            PointF pt1 = new PointF(10, 10);
            PointF pt2 = new PointF(100, 100);
            pea.Graphics.DrawLine(pen, pt1, pt2);
        }

        public void FormLayout() {
            
        }
    }
}