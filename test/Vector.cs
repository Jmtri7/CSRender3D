using System;

class Vector {
    public double x;
    public double y;
    public double z;

    public Vector(double x, double y, double z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector Polar(double rho, double theta, double phi, bool radians = false) {
        if(!radians) {
            theta *= Math.PI / 180;
            phi *= Math.PI / 180;
        }
        double r = rho * Math.Sin(theta);
        double z = rho * Math.Cos(theta);
        double y = r * Math.Sin(phi);
        double x = r * Math.Cos(phi);
        return new Vector(x, y, z);
    }

    public double GetRho() {
        return Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2));
    }

    public double GetPhi() {
        double phi = Math.Atan2(this.y, this.x) * 180 / Math.PI;
        if(phi < 0) phi += 360;
        return phi;
    }

    public double GetTheta() {
        double r = Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2));
        double theta = Math.Atan2(r, this.z) * 180 / Math.PI;
        if(theta < 0) theta += 360;
        return theta;
    }

    public Vector Scale(double scalar) {
        return new Vector(this.x * scalar, this.y * scalar, this.z * scalar);
    }
    
    public Vector Add(Vector that) {
        return new Vector(this.x + that.x, this.y + that.y, this.z + that.z);
    }

    public Vector Subtract(Vector that) {
        return new Vector(this.x - that.x, this.y - that.y, this.z - that.z);
    }

    public Vector Rotate(double dTheta, double dPhi) {
        return Vector.Polar(this.GetRho(), this.GetTheta() + dTheta, this.GetPhi() + dPhi);
    }

    public double Dot(Vector that) {
        return this.x * that.x + this.y * that.y + this.z * that.z;
    }

    public Vector Cross(Vector that) {
        double x = this.y * that.z - this.z * that.y;
        double y = this.z * that.x - this.x * that.z;
        double z = this.x * that.y - this.y * that.x;
        return new Vector(x, y, z);
    }

    public void PrintCartesian() {
        Console.WriteLine(String.Format("X: {0}, Y: {1}, Z: {2}", this.x, this.y, this.z));
    }

    public void PrintPolar() {
        Console.WriteLine(String.Format("RHO: {0}, THETA: {1}, PHI: {2}", this.GetRho(), this.GetTheta(), this.GetPhi()));
    }
}