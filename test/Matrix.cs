class Matrix {
    Row row1;
    Row row2;
    Row row3;
    public Matrix(Row row1, Row row2, Row row3) {
        this.row1 = row1;
        this.row2 = row2;
        this.row3 = row3;
    }

    public Vector Solve() {
        this.row1 = this.row1.Scale(1 / this.row1.a);
        this.row2 = this.row2.Subtract(this.row1.Scale(this.row2.a));
        this.row3 = this.row3.Subtract(this.row1.Scale(this.row3.a));
        this.row2 = this.row2.Scale(1 / this.row2.b);
        this.row1 = this.row1.Subtract(this.row2.Scale(this.row1.b));
        this.row3 = this.row3.Subtract(this.row2.Scale(this.row3.b));
        this.row3 = this.row3.Scale(1 / this.row3.c);
        this.row1 = this.row1.Subtract(this.row3.Scale(this.row1.c));
        this.row2 = this.row2.Subtract(this.row3.Scale(this.row2.c));
        return new Vector(row1.i, row2.i, row3.i);
    }
}