using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;


namespace RayTracerWinFormsTest
{
    class scene5File
    {
        ICamera camera = new Pinhole(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, 1, 0), 1);
        World world = new World(ColorRgb.Black);
        List<Transformation> transformationsStack = new List<Transformation>();
        Transformation transform = new Transformation();
        IMaterial material = new Reflective(Color.Gray, 0.4, 1, 300, 0.6);


        public void Readscene5File(string path)
        {
            world.AddLight(new PointLight(new Vector3(0, 20, -40), Color.White));
            

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            foreach (string line in lines)
            {
                if (line.Equals("") || line[0].Equals('#'))
                {
                    continue;
                }
                List<string> lineString = new List<string>();
                string tempString = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != ' ')
                    {
                        tempString += line[i];

                    }
                    else
                    {
                        lineString.Add(tempString);
                        tempString = "";
                    }

                }
                lineString.Add(tempString);
                tempString = "";

                switch (lineString[0])
                {
                    case "sphere":
                        CreateSphere(lineString);
                        break;

                    case "pushTransform":
                        PushTransformation();
                        break;

                    case "popTransform":
                        PopTransformation();
                        break;

                    case "translate":
                        SetTranslate(lineString);
                        break;

                    default:
                        break;
                }
            }

                Raytracer tracer = new Raytracer(5);
            Bitmap image = tracer.Raytrace(world, camera, new Size(640, 480));
            image.Save("scene5.png");
        }


        void SetTranslate(List<String> list)
        {
            float x = (float)ConvertDouble(list[1]);
            float y = (float)ConvertDouble(list[2]);
            float z = (float)ConvertDouble(list[3]);
            //transform.transformList.Add(Matrix4x4.CreateTranslation(x, y, z));

            Matrix4x4 translate = new Matrix4x4();
            translate.M11 = 1;
            translate.M12 = 0;
            translate.M13 = 0;
            translate.M14 = x;

            translate.M21 = 0;
            translate.M22 = 1;
            translate.M23 = 0;
            translate.M24 = y;

            translate.M31 = 0;
            translate.M32 = 0;
            translate.M33 = 1;
            translate.M34 = z;

            translate.M41 = 0;
            translate.M42 = 0;
            translate.M43 = 0;
            translate.M44 = 1;

            //trans.transformList.Add(Matrix4x4.CreateTranslation(x, y, z));
            transform.transformList.Add(translate);
        }

        public static double ConvertDouble(string tekst)
        {
            if (tekst.Length > 2)
            {
                if (tekst[1] != 0)
                {
                    tekst.Insert(1, "0");
                }
            }

            double returned;
            return returned = double.Parse(tekst, System.Globalization.CultureInfo.InvariantCulture);
        }

        void PushTransformation()
        {
            transformationsStack.Add(new Transformation(transform));
            transform = new Transformation();
        }

        bool PopTransformation()
        {
            if (transformationsStack.Count == 0)
            {
                return false;
            }
            int topOfStack = transformationsStack.Count - 1;
            transform = new Transformation(transformationsStack[topOfStack]);
            transformationsStack.RemoveAt(topOfStack);
            return true;

        }

        void CreateSphere(List<String> list)
        {
            world.Add(new Sphere(new Vector3(ConvertDouble(list[1]), ConvertDouble(list[2]), ConvertDouble(list[3])), ConvertDouble(list[4]), material, transform));
        }
    }
}
