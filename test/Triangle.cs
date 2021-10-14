using System;
using System.Drawing;

class Triangle {
    public Vector[] vectors;
    public Color color;

    public Triangle(Vector[] vectors, Color color) {
        this.vectors = vectors;
        this.color = color;
    }

    public void PrintCartesian() {
        for(int i = 0; i < this.vectors.Length; i++) {
            Console.WriteLine(String.Format("X: {0}, Y: {1}, Z: {2}", this.vectors[i].x, this.vectors[i].y, this.vectors[i].z));
        }
    }

    public void PrintPolar() {
        for(int i = 0; i < this.vectors.Length; i++) {
            Console.WriteLine(String.Format("RHO: {0}, THETA: {1}, PHI: {2}", this.vectors[i].GetRho(), this.vectors[i].GetTheta(), this.vectors[i].GetPhi()));
        }
    }
}