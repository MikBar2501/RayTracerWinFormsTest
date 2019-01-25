using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    class Phong : IMaterial
    {
        ColorRgb materialColor;
        double diffuseCoeff;
        double specular;
        double specularExponent;

        public Phong(ColorRgb materialColor, double diffuse, double specular, double specularExponent)
        {
            this.materialColor = materialColor;
            this.diffuseCoeff = diffuse;
            this.specular = specular;
            this.specularExponent = specularExponent;
        }

        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb totalColor = ColorRgb.Black;
            foreach (var light in hit.World.Lights)
            {
                Vector3 inDirection = (light.Position - hit.HitPoint).Normalised;
                double diffuseFactor = inDirection.Dot(hit.Normal);
                if (diffuseFactor < 0) { return ColorRgb.Black; }
                if (hit.World.AnyObstacleBetween(hit.HitPoint, light.Position)) { continue; }
                ColorRgb result = light.Color * materialColor * diffuseFactor * diffuseCoeff; double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction);
                if (phongFactor != 0) { result += materialColor * specular * phongFactor; }
                totalColor += result;
            }
            return totalColor;
        }

        double PhongFactor(Vector3 inDirection, Vector3 normal, Vector3 toCameraDirection)
        {
            Vector3 reflected = Vector3.Reflect(inDirection, normal);
            double cosAngle = reflected.Dot(toCameraDirection);

            if (cosAngle <= 0) { return 0; }

            return Math.Pow(cosAngle, specularExponent);
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

        public void SetColor(ColorRgb color)
        {
            materialColor = color;
        }
    }
}
