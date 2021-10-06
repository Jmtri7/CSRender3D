using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace test {
    class Program : Form {
        Vector[] vectors = {

                //new Vector(-200, -200, 500),
                new Vector(-200, -200, 1000),

                //new Vector(-200, -100, 500),
                new Vector(-200, -100, 1000),

                //new Vector(-100, -200, 500),
                new Vector(-100, -200, 1000),

                //new Vector(-100, -100, 500),
                new Vector(-100, -100, 1000),
            };

        static void Main(string[] args) {
            Application.Run(new Program());
        }

        public Program() {
            this.Name = "CS3DRenderer";
            this.Text = "3DRenderer";
            this.Size = new System.Drawing.Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            ResizeRedraw = true;

            
        }

        public PointF GetPoint(Vector vector) {
            double r1 = Math.Sqrt(Math.Pow(vector.x, 2) + Math.Pow(vector.y, 2));

            double r2 = r1 * 500 / vector.z;

            double rho2 = vector.GetRho() * r2 / r1;

            Vector point = Vector.Polar(rho2, vector.GetTheta(), vector.GetPhi());

            return new PointF((float)(point.x + 250), (float)(point.y + 250));
        }

        protected override void OnPaint(PaintEventArgs pea) {
            
            Pen pen = new Pen(ForeColor);
            PointF pto = new PointF(0, 0);

            pea.Graphics.DrawEllipse(pen, 0 - 1, 0 - 1, 2, 2);
            pea.Graphics.DrawEllipse(pen, 250 - 1, 250 - 1, 2, 2);

            for(int i = 0; i < this.vectors.Length; i++) {
                PointF pt = this.GetPoint(this.vectors[i]);
                pea.Graphics.DrawEllipse(pen, pt.X - 1, pt.Y - 1, 2, 2);
            }
            
        }
    }
}