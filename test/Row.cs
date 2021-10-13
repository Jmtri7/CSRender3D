class Row {
    public double a, b, c, i;

    public Row(double a, double b, double c, double i) {
        this.a = a;
        this.b = b;
        this.c = c;
        this.i = i;
    }

    public Row Scale(double scalar) {
        return new Row(this.a * scalar, this.b * scalar, this.c * scalar, this.i * scalar);
    }
    
    public Row Add(Row that) {
        return new Row(this.a + that.a, this.b + that.b, this.c + that.c, this.i + that.i);
    }

    public Row Subtract(Row that) {
        return new Row(this.a - that.a, this.b - that.b, this.c - that.c, this.i - that.i);
    }
}