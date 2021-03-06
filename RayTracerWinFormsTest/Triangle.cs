﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override bool HitTest(Ray ray, ref double distance, ref Vector3 outNormal)
        {
            //normal = (Vector3.Cross(v1, v0) - Vector3.Cross(v2, v0)).Normalised;
            normal = Vector3.Cross(v1-v0, v2-v0).Normalised;
            double t = (v0.Dot(normal) - ray.Origin.Dot(normal)) / ray.Direction.Dot(normal);
            Vector3 middle = (v1 + v2 + v0)/3;
            Vector3 hitPoint = (ray.Origin + ray.Direction * t);
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
            if(!((u < 0 || u> 1) || (v < 0 || v > 1) || (w < 0 || w > 1)))
            {
                //outNormal = (hitPoint - normal).Normalised;
                distance = normal.Dot(hitPoint);
                outNormal = normal.Normalised;
                return true;
            }
            
            //double a = ray.Direction.Dot(normal);
            distance = normal.Dot(hitPoint);
            //outNormal = (middle - normal).Normalised;
            //outNormal = -(hitPoint - normal).Normalised;
            outNormal = normal.Normalised;

            return false;


        }
    }
}
