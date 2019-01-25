using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    class Reflective : IMaterial
    {
        Phong direct;
        double reflectivity;
        ColorRgb reflectionColor;
        


        public Reflective(ColorRgb materialColor, double diffuse, double specular, double exponent, double reflectivity)
        {
            this.direct = new Phong(materialColor, diffuse, specular, exponent);
            this.reflectivity = reflectivity;
            this.reflectionColor = materialColor;
        }
        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            
            Vector3 toCameraDirection = -hit.Ray.Direction;
            ColorRgb radiance = direct.Shade(tracer, hit);
            Vector3 reflectionDirection = Vector3.Reflect(toCameraDirection, hit.Normal);
            Ray reflectedRay = new Ray(hit.HitPoint, reflectionDirection);
            ColorRgb reflected = tracer.ShadeRay(hit.World, reflectedRay, hit.Depth) * reflectionColor * reflectivity;
            radiance += tracer.ShadeRay(hit.World, reflectedRay, hit.Depth) * reflectionColor * reflectivity;
            return radiance;
        }

        public void ChangeColor(double R, double G, double B)
        {
            R = 255 * R;
            G = 255 * G;
            B = 255 * B;

            direct.SetColor(new ColorRgb(R, G, B));
        }

        public void ChangeColor(ColorRgb color)
        {
            direct.SetColor(color);
        }
    }
}
