using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracerWinFormsTest
{
    class Raytracer
    {
        int maxDepth;

        public Raytracer(int maxDepth) { this.maxDepth = maxDepth; }

        public Bitmap Raytrace(World world, ICamera camera, Size imageSize)
        {
            Bitmap bmp = new Bitmap(imageSize.Width, imageSize.Height);
            for (int y = 0; y < imageSize.Height; y++) for (int x = 0; x < imageSize.Width; x++)
                {            // przeskalowanie x i y do zakresu [-1; 1]            
                    Vector2 pictureCoordinates = new Vector2(                
                        ((x + 0.5) / (double)imageSize.Width) * 2 - 1,                
                        ((y + 0.5) / (double)imageSize.Height) * 2 - 1);
                    // wysłanie promienia i sprawdzenie w co właściwie trafił            
                    Ray ray = camera.GetRayTo(pictureCoordinates);
                    bmp.SetPixel(x, y, StripColor(ShadeRay(world, ray, 0))); 
                }
            return bmp;
        }

    

        public ColorRgb ShadeRay(World world, Ray ray, int currentDepth)
        {
            if (currentDepth > maxDepth) { return ColorRgb.Black; }
            HitInfo info = world.TraceRay(ray);
            info.Depth = currentDepth + 1;
            if (info.HitObject == null) {return world.BackgroundColor;}
            //ColorRgb finalColor = ColorRgb.Black;
            IMaterial material = info.HitObject.Material;
            return material.Shade(this, info);
        }

        Color StripColor(ColorRgb colorInfo)
        {
            colorInfo.R = colorInfo.R < 0 ? 0 : colorInfo.R > 1 ? 1 : colorInfo.R;
            colorInfo.G = colorInfo.G < 0 ? 0 : colorInfo.G > 1 ? 1 : colorInfo.G;
            colorInfo.B = colorInfo.B < 0 ? 0 : colorInfo.B > 1 ? 1 : colorInfo.B;
            return Color.FromArgb((int)(colorInfo.R * 255), (int)(colorInfo.G * 255), (int)(colorInfo.B * 255));
        }

        


    }
}
