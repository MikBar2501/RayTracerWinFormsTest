using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracerWinFormsTest
{
    class Sphere : GeometricObject
    {
        Vector3 center;
        double radius;
        public Sphere(Vector3 center, double radius, IMaterial material)
        {
            this.center = center;
            this.radius = radius;
            base.Material = material;
        }

        public override bool HitTest(Ray ray, ref double minDistance, ref Vector3 outNormal)
        {
            double t;
            Vector3 distance = ray.Origin - center;


            double a = ray.Direction.LengthSq;
            double b = (distance * 2).Dot(ray.Direction);
            double c = distance.LengthSq - radius * radius;
            double disc = b * b - 4 * a * c;

            if (disc < 0)
            {
                return false;
            }
            double discSq = Math.Sqrt(disc);
            double denom = 2 * a;
            t = (-b - discSq) / denom;

            if (t < Ray.Epsilon)
            {
                t = (-b + discSq) / denom;
            }
            if (t < Ray.Epsilon)
            {
                return false;
            }

            Vector3 hitPoint = (ray.Origin + ray.Direction * t);
            outNormal = (hitPoint - center).Normalised;
            minDistance = t;
            return true;
        }

        Vector3 Translated(Vector3 translated)
        {
            double[,] matrix = new double[4, 4] { { 1, 0, 0, translated.X }, { 0, 1, 0, translated.Y }, { 0, 0, 1, translated.Z }, { 0, 0, 0, 1 }, };
            return translated;
        }
    }
}
