using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

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

        public static Vector3 Reflect(Vector3 vec, Vector3 normal)
        {
            double dot = normal.Dot(vec);
            return normal * dot * 2 - vec;
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
    }

    abstract class GeometricObject
    {
        public IMaterial Material { get; set; }
        public abstract bool HitTest(Ray ray, ref double distance, ref Vector3 normal);

        public void Translated(Vector3 translated)
        {

        }

        public void Scale(Vector3 scale)
        {

        }

        public void RotX(double angle)
        {

        }

        public void RotY(double angle)
        {

        }

        public void RotZ(double angle)
        {

        }

        public void TripleRot(Vector3 angle)
        {

        }
    }

    
}
