using System.Drawing;

class Camera {
    public Vector position;
    public Vector direction;
    public Bitmap bitmap;

    public Camera(Vector position, Vector direction) {
        this.position = position;
        this.direction = direction;
        this.bitmap = new Bitmap(200, 200);
    }

    public void CastRays(Triangle triangle) {
        double FOV = 90;
        double dTheta = - FOV / 2;
        for(int y = 0; y < bitmap.Height; y++) {
            double dPhi = - FOV / 2;
            for(int x = 0; x < bitmap.Width; x++) {
                Vector direction = this.position.Add(this.direction.Rotate(dTheta, dPhi));
                Vector intersect = this.RayCast(direction, triangle);
                if(intersect != null) this.bitmap.SetPixel(x, y, triangle.color);
                dPhi += FOV / bitmap.Width;
            }
            dTheta += FOV / bitmap.Height;
        }      
    }

    public Vector RayCast(Vector direction, Triangle triangle) {
        Vector v1 = triangle.vectors[1].Subtract(triangle.vectors[0]);
        Vector v2 = triangle.vectors[2].Subtract(triangle.vectors[0]);
        Vector n = v1.Cross(v2);

        double t = triangle.vectors[0].Subtract(this.position).Dot(n) / n.Dot(direction);
        
        Vector intersect = this.position.Add(direction.Scale(t));

        Matrix matrix = new Matrix(
            new Row(triangle.vectors[0].x, triangle.vectors[1].x, triangle.vectors[2].x, intersect.x),
            new Row(triangle.vectors[0].y, triangle.vectors[1].y, triangle.vectors[2].y, intersect.y),
            new Row(triangle.vectors[0].z, triangle.vectors[1].z, triangle.vectors[2].z, intersect.z)
        );

        Vector weights = matrix.Solve();
        int numNegative = 0;
        if(weights.x < 0) numNegative++;
        if(weights.y < 0) numNegative++;
        if(weights.z < 0) numNegative++;
        if(numNegative == 0 || numNegative == 3) return intersect;
        else return null;
    }
}