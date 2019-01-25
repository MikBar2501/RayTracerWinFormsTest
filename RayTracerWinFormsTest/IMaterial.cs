using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    interface IMaterial
    {
        ColorRgb Shade(Raytracer tracer, HitInfo hit);
        void ChangeColor(double R, double G, double B);
        void ChangeColor(ColorRgb color);
    }
}
