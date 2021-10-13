using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace test {
    class Program : Form {

        public static void Main(string[] args) {
            Application.Run(new Program(500, 500));
        }

        int width;
        int height;
        Camera camera;
        public Program(int width, int height) {
            this.Name = "CS3DRenderer";
            this.Text = "3DRenderer";

            this.width = width;
            this.height = height;
            this.Size = new System.Drawing.Size(width, height);
            this.camera = new Camera(new Vector(10, 10, 0), Vector.Polar(20, 90, 45));

            this.StartPosition = FormStartPosition.CenterScreen;
            ResizeRedraw = true;
        }

        public void DrawPoint(PaintEventArgs pea, Vector vector) {
            Pen pen = new Pen(ForeColor);
            if(vector != null) pea.Graphics.DrawEllipse(pen, (int) vector.x - 1, (int) vector.y - 1, 2, 2);
        }

        public void DrawLine(PaintEventArgs pea, Vector v1, Vector v2) {
            Pen pen = new Pen(ForeColor);
            Point point1 = new Point((int) v1.x, (int) v1.y);
            Point point2 = new Point((int) v2.x, (int) v2.y);
            pea.Graphics.DrawLine(pen, point1, point2);
        }

        public void DrawTriangle(PaintEventArgs pea, Triangle triangle) {
            this.DrawLine(pea, triangle.vectors[0], triangle.vectors[1]);
            this.DrawLine(pea, triangle.vectors[1], triangle.vectors[2]);
            this.DrawLine(pea, triangle.vectors[2], triangle.vectors[0]);
        }

        public void DrawRectangle(PaintEventArgs pea, float x, float y, float width, float height) {
            Pen pen = new Pen(ForeColor);
            pea.Graphics.DrawRectangle(pen, x, y, width, height);
        }

        public void DrawPixels(PaintEventArgs pea, Vector[,] pixels) {
            var pixelWidth = (this.width / pixels.GetLength(0));
            var pixelHeight = (this.height / pixels.GetLength(1));
            for(int y = 0; y < pixels.GetLength(1); y++)
                for(int x = 0; x < pixels.GetLength(0); x++) 
                    if(pixels[x, y] != null) this.DrawRectangle(pea, x * pixelWidth, y * pixelHeight, pixelWidth, pixelHeight);
        }

        public void DrawCamera(PaintEventArgs pea) {
            this.DrawPoint(pea, this.camera.position);
            this.DrawLine(pea, this.camera.position, this.camera.position.Add(this.camera.direction));
        }

        protected override void OnPaint(PaintEventArgs pea) {
            pea.Graphics.Clear(Color.White);

            Vector[] vectors = {new Vector(50, 80, 0), new Vector(80, 50, 0), new Vector(80, 80, 30)};
            Triangle triangle = new Triangle(vectors);

            //this.DrawCamera(pea);
            //this.DrawTriangle(pea, triangle);

            Vector[,] pixels = this.camera.CastRays(triangle);
            this.DrawPixels(pea, pixels);

            //this.DrawPixels(pea, this.camera.pixels);
        }
    } 
}