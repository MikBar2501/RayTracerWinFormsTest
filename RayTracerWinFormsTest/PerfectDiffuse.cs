using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    class PerfectDiffuse : IMaterial
    {
        ColorRgb materialColor;
        public PerfectDiffuse(ColorRgb materialColor) {
            this.materialColor = materialColor;
        }

        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb totalColor = ColorRgb.Black;
            foreach (var light in hit.World.Lights)
            {
                Vector3 inDirection = (light.Position - hit.HitPoint).Normalised;
                double diffuseFactor = inDirection.Dot(hit.Normal);
                if (diffuseFactor < 0) { continue; }
                if (hit.World.AnyObstacleBetween(hit.HitPoint, light.Position)) { continue; }
                totalColor += light.Color * materialColor * diffuseFactor;
            }
            return totalColor;
        }

        public void ChangeColor(double R, double G, double B)
        {
            R = 255 * R;
            G = 255 * G;
            B = 255 * B;

            materialColor = new ColorRgb(R, G, B);
        }

        public void ChangeColor(ColorRgb color)
        {
            materialColor = color;
        }
    }
}
