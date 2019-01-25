using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RayTracerWinFormsTest
{
    class Triangle : GeometricObject
    {
        Vector3 v0;
        Vector3 v1;
        Vector3 v2;
        Vector3 normal;
        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2, IMaterial material)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            base.Material = material; 
        }

        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2, IMaterial material, Transformation transform)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
            base.Material = material;
            base.transform = transform;
        }

        /*public override bool HitTest(Ray ray, ref double distance, ref Vector3 outNormal)
        {
         
            Ray transformRay = ray * transform.GetReverseTransform();
            //Ray transformRay = ray;
            normal = Vector3.Cross(v1-v0, v2-v0).Normalised;
            //double t = (v0.Dot(normal) - transformRay.Origin.Dot(normal)) / transformRay.Direction.Dot(normal);
            //Vector3 hitPoint = (transformRay.Origin + transformRay.Direction * t);
            Vector3 hitPoint = transformRay.Origin + transformRay.Direction * (((v0 - transformRay.Origin).Dot(normal)) / (transformRay.Direction.Dot(normal)));
            Vector3 V0 = v1 - v0;
            Vector3 V1 = v2 - v0;
            Vector3 V2 = hitPoint - v0;
            double d00 = V0.Dot(V0);
            double d01 = V0.Dot(V1);
            double d11 = V1.Dot(V1);
            double d20 = V2.Dot(V0);
            double d21 = V2.Dot(V1);
            double d = (d00 * d11) - (d01 * d01);
            double v = ((d11 * d20) - (d01 * d21)) / d;
            double w = ((d00 * d21) - (d01 * d20)) / d;
            double u = 1 - (v + w);
            
            if (!((u < Ray.Epsilon || u> 1) || (v < Ray.Epsilon || v > 1) || (w < Ray.Epsilon || w > 1)))
            {
                //hitPoint = transform * hitPoint;
                // Matrix4x4.Invert(transform, out newMatrix);
                // newMatrix = Matrix4x4.Transpose(newMatrix);
                //  normal = newMatrix * normal;

                hitPoint = transform.GetMatrix() * hitPoint;
                Matrix4x4 inverse;
                Matrix4x4.Invert(transform.GetMatrix(), out inverse);
                normal = (Matrix4x4.Transpose(inverse) * normal).Normalised;
                distance = normal.Dot(hitPoint);
                outNormal = (normal).Normalised;
                return true;
            }

            //hitPoint = transform * hitPoint;
            //Matrix4x4.Invert(transform, out newMatrix);
            //newMatrix = Matrix4x4.Transpose(newMatrix);
            //normal = newMatrix * normal;
            distance = normal.Dot(hitPoint);
            outNormal = normal.Normalised;

            return false;


        }*/

        public override bool HitTest(Ray ray, ref double distance, ref Vector3 outNormal)
        {

            Ray transformRay = ray * transform.GetReverseTransform();
            normal = Vector3.Cross(v1 - v0, v2 - v0).Normalised;
            Vector3 hitPoint = transformRay.Origin + transformRay.Direction * (((v0 - transformRay.Origin).Dot(normal)) / (transformRay.Direction.Dot(normal)));
            double b0 = BaricentricCoordinates(v2, v1, v0, hitPoint);
            double b1 = BaricentricCoordinates(v1, v0, v2, hitPoint);
            double b2 = BaricentricCoordinates(v0, v2, v1, hitPoint);

            if((b0 > 1 || b0 < Ray.Epsilon)|| (b1 > 1 || b1 < Ray.Epsilon) || (b2 > 1 || b2 < Ray.Epsilon))
            {
                return false;
            }


                hitPoint = transform.GetMatrix() * hitPoint;
                Matrix4x4 inverse;
                Matrix4x4.Invert(transform.GetMatrix(), out inverse);
                normal = (Matrix4x4.Transpose(inverse) * normal).Normalised;
                distance = normal.Dot(hitPoint);
                outNormal = (normal).Normalised;
                return true;
        }

        double BaricentricCoordinates(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 hitPoint)
        {
            Vector3 V0 = v1 - v0;
            Vector3 V1 = v1 - v2;
            Vector3 V2 = hitPoint - v0;
            Vector3 V3 = V0 - Vector3.Projection(V0, V1);
            return 1 - (V3.Dot(V2) / V3.Dot(V0));

        }
    }
}
