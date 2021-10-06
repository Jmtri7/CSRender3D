using System;

namespace test {
    class Program {
        static void Main(string[] args) {
            Vector vector = Vector.Polar(10, 90, 0);

            for(int i = 0; i < 9; i++) {
                vector.Rotate(0, 45 * i).PrintPolar();
            }
        }
    }
}