using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

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
            //Ray transformRay = ray * reverse;
            Ray transformRay = ray;
            double t;
            Vector3 distance = transformRay.Origin - center;


            double a = transformRay.Direction.LengthSq;
            double b = (distance * 2).Dot(transformRay.Direction);
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

            Vector3 hitPoint = (transformRay.Origin + transformRay.Direction * t);
            Vector3 normal = (hitPoint - center).Normalised;
            Matrix4x4 newMatrix;
            Matrix4x4.Invert(transform, out newMatrix);
            newMatrix = Matrix4x4.Transpose(newMatrix);
            normal = newMatrix * normal;
            outNormal = (hitPoint - center).Normalised;
            minDistance = t;
            return true;
        }

    }
}
