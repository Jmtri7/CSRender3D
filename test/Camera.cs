using System.Drawing;

class Camera {
    public Vector position;
    public Vector direction;
    public Bitmap bitmap;

    public Camera(Vector position, Vector direction, int resX, int resY) {
        this.position = position;
        this.direction = direction;
        this.bitmap = new Bitmap(resX, resY);
    }

    public void RayCast(Triangle[] triangles) {
        Vector normal1 = this.direction.Rotate(90, 0).Scale(0.005 / this.direction.GetRho());
        Vector normal2 = this.direction.Rotate(0, 90).Scale(0.005 / this.direction.GetRho());

        //double FOV = 90;
        //double dTheta = - FOV / 2;
        for(int y = 0; y < bitmap.Height; y++) {
            //double dPhi = - FOV / 2;
            for(int x = 0; x < bitmap.Width; x++) {
                
                Vector direction = this.direction.Add(normal1.Scale(y - bitmap.Height / 2)).Add(normal2.Scale(x - bitmap.Width / 2));
                //Vector direction = this.position.Add(this.direction.Rotate(dTheta, dPhi));
                //Vector direction = this.direction.Rotate(dTheta, dPhi);

                Triangle triangle = null;
                Vector intersect1 = null;
                for(int i = 0; i < triangles.Length; i++) {
                    Vector intersect2 = this.CastRay(direction, triangles[i]);

                    if(intersect2 != null) {
                        if(intersect1 == null) {
                            triangle = triangles[i];
                            intersect1 = intersect2;
                        }
                        else {
                            double d1 = intersect1.Subtract(this.position).GetRho();
                            double d2 = intersect2.Subtract(this.position).GetRho();
                            if(d2 < d1) {
                                triangle = triangles[i];
                                intersect1 = intersect2;
                            }
                        }
                    }
                    
                    if(triangle != null) this.bitmap.SetPixel(x, y, triangle.color);
                    else this.bitmap.SetPixel(x, y, Color.Black);
                }
                

                //dPhi += FOV / bitmap.Width;
            }
            //dTheta += FOV / bitmap.Height;
        }
    }

    public Vector CastRay(Vector direction, Triangle triangle) {
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