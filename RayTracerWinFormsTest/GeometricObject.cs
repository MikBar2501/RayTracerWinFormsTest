using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Numerics;

namespace RayTracerWinFormsTest
{
    struct ColorRgb
    {
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
        public ColorRgb(double r, double g, double b) : this() {
            this.R = r;
            this.G = g;
            this.B = b;
        }
        public static implicit operator ColorRgb(Color color) {
            return new ColorRgb(color.R / 255.0, color.G / 255.0, color.B / 255.0);
        }
        public static ColorRgb operator +(ColorRgb col1, ColorRgb col2) {
            return new ColorRgb(col1.R + col2.R, col1.G + col2.G, col1.B + col2.B);
        }
        public static ColorRgb operator *(ColorRgb col1, double val) {
            return new ColorRgb(col1.R * val, col1.G * val, col1.B * val);
        }
        public static ColorRgb operator *(ColorRgb col1, ColorRgb col2) {
            return new ColorRgb(col1.R * col2.R, col1.G * col2.G, col1.B * col2.B);
        }
        public static ColorRgb operator /(ColorRgb col1, double val) {
            return col1 * (1 / val);
        }
        public static readonly ColorRgb White = new ColorRgb(1, 1, 1);
        public static readonly ColorRgb Black = new ColorRgb(0, 0, 0);
    }


    struct Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y, double z)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3 operator +(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }

        public static Vector3 operator -(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }

        public static Vector3 operator *(Vector3 vec, double val)
        {
            return new Vector3(vec.X * val, vec.Y * val, vec.Z * val);
        }

        public static Vector3 operator /(Vector3 vec, double val)
        {
            return new Vector3(vec.X / val, vec.Y / val, vec.Z / val);
        }

        public static Vector3 Cross(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.Y * vec2.Z - vec1.Z * vec2.Y,
                vec1.Z * vec2.X - vec1.X * vec2.Z,
                vec1.X * vec2.Y - vec1.Y * vec2.X);
        }

        public static Vector3 Projection(Vector3 vecTo, Vector3 vecOn)
        {
            return vecOn * (vecOn.Dot(vecTo) / vecOn.Dot(vecOn));
        }

        /*public static Vector3 operator *(Vector3 vec, Matrix4x4 matrix)
        {
            Matrix oneColumnMatrix = new Matrix(4, 1);
            oneColumnMatrix.cells[0, 0] = vec.X;
            oneColumnMatrix.cells[1, 0] = vec.Y;
            oneColumnMatrix.cells[2, 0] = vec.Z;
            oneColumnMatrix.cells[3, 0] = 1;
            Matrix newMatrix = matrix * oneColumnMatrix;

            return new Vector3(newMatrix.cells[0, 0], newMatrix.cells[1, 0], newMatrix.cells[2, 0]); 
        }*/

        public static Vector3 operator *(Matrix4x4 matrix, Vector3 vec)
        {
            float[] oneColumnMatrix = {(float) vec.X, (float)vec.Y, (float)vec.Z, 1 };
            double row0 = matrix.M11 * oneColumnMatrix[0] + matrix.M12 * oneColumnMatrix[1] + matrix.M13 * oneColumnMatrix[2] + matrix.M14 * oneColumnMatrix[3];
            double row1 = matrix.M21 * oneColumnMatrix[0] + matrix.M22 * oneColumnMatrix[1] + matrix.M23 * oneColumnMatrix[2] + matrix.M24 * oneColumnMatrix[3];
            double row2 = matrix.M31 * oneColumnMatrix[0] + matrix.M32 * oneColumnMatrix[1] + matrix.M33 * oneColumnMatrix[2] + matrix.M34 * oneColumnMatrix[3];
            double row3 = matrix.M41 * oneColumnMatrix[0] + matrix.M42 * oneColumnMatrix[1] + matrix.M43 * oneColumnMatrix[2] + matrix.M44 * oneColumnMatrix[3];
            return new Vector3(row0, row1, row2);
        }

        public Vector3 CreateVector(Matrix4x4 matrix)
        {
            return new Vector3(matrix.M11, matrix.M21, matrix.M31);
        }

        public static Vector3 Reflect(Vector3 vec, Vector3 normal)
        {
            return normal * normal.Dot(vec) * 2 - vec;
        }

        public static Vector3 operator -(Vector3 vec)
        {
            return new Vector3(-vec.X, -vec.Y, -vec.Z);
        }

        public double Dot(Vector3 vec)
        {
            return (this.X * vec.X + this.Y * vec.Y + this.Z * vec.Z);
        }

        public double Length
        { get { return Math.Sqrt(X * X + Y * Y + Z * Z); } }

        public double LengthSq
        { get { return X * X + Y * Y + Z * Z; } }

        public Vector3 Normalised
        {
            get { return this / this.Length; }
        }
    }

    struct Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }
    }

    struct Ray
    {
        public const double Epsilon = 0.00001;
        public const double Huge = double.MaxValue;

        public Ray(Vector3 origin, Vector3 direction)
            : this()
        {
            this.Origin = origin;
            this.Direction = direction.Normalised;
        }

        public Vector3 Origin { get; set; }
        public Vector3 Direction { get; set; }

        /*public static Ray operator *(Ray ray, Matrix4x4 matrix)
        {
            Matrix newMatrix = new Matrix(matrix);
            for(int x = 0; x < matrix.rowsCount; x++)
            {
                newMatrix.cells[x, 3] = 0;
            }

            for (int y = 0; y < matrix.columnsCount; y++)
            {
                newMatrix.cells[3, y] = 0;
            }

            return new Ray(ray.Origin * matrix, (ray.Direction* newMatrix).Normalised);

        }*/

        public static Ray operator *(Ray ray, Matrix4x4 matrix)
        {
            //Matrix newMatrix = new Matrix(matrix);
            Matrix4x4 newMatrix = matrix;
            newMatrix.M14 = 0;
            newMatrix.M24 = 0;
            newMatrix.M34 = 0;
            newMatrix.M44 = 0;
            newMatrix.M41 = 0;
            newMatrix.M42 = 0;
            newMatrix.M43 = 0;
            newMatrix.M44 = 0;

            return new Ray(matrix* ray.Origin, (newMatrix * ray.Direction).Normalised);

        }
    }

    abstract class GeometricObject
    {
        public IMaterial Material { get; set; }
        public abstract bool HitTest(Ray ray, ref double distance, ref Vector3 normal);
        public Transformation transform;
    }

    
}
