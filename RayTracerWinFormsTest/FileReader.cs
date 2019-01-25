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
    class FileReader
    {
        World world = new World(Color.White);
        List<Vector3> vertexList = new List<Vector3>();

        List<Transformation> transformationsStack = new List<Transformation>();
        Transformation transform = new Transformation();

        ColorRgb matColor = new ColorRgb(1,1,1);
        IMaterial material = new Reflective(new ColorRgb(1, 1, 1), 0.8, 1, 300, 0.2);
        ICamera camera;

        string savePath = "n.png";
        int resolutionWidth = 1024;
        int resolutionHeight = 1024;
        int maxDepth = 5;


        public void ReadFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            
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
                    if (line[i]!=' ')
                    {
                        tempString += line[i];
                        
                    } else
                    {
                        lineString.Add(tempString);
                        tempString = "";
                    }

                }
                lineString.Add(tempString);
                tempString = "";

                switch (lineString[0])
                {
                    case "maxverts":
                        //MaxVertsFunction(lineString);
                        break;

                    case "vertex":
                        NewVertex(lineString);
                        break;

                    case "tri":
                        CreateTriangle(lineString);
                        break;

                    case "camera":
                        CreateCamera(lineString);
                        break;

                    case "size":
                        ImageSize(lineString);
                        break;

                    case "sphere":
                        CreateSphere(lineString);
                        break;

                    case "ambient":
                        CreateWorld(lineString);
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

                    case "scale":
                        SetScale(lineString);
                        break;

                    case "rotate":
                        SetRotate(lineString);
                        break;

                    case "directional":
                        CreateLight(lineString);
                        break;

                    case "point":
                        CreateLight(lineString);
                        break;

                    case "attenuation":
                        
                        break;

                    case "diffuse":
                        SetDiffuse(lineString);
                        break;

                    case "specular":
                        
                        break;

                    case "emission":

                        break;

                    case "shininess":
                        SetShinines(lineString);
                        break;

                    case "maxdepth":
                        SetMaxDepth(lineString);
                        break;

                    case "output":
                        OutputFile(lineString);
                        break;

                    default:
                        continue;
                }

            }
            //world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0), material));
            if(world.Lights.Count == null)
            {
                world.AddLight(new PointLight(new Vector3(0, 2, -1), Color.White));
            }

            Raytracer tracer = new Raytracer(5);
            Bitmap image = tracer.Raytrace(world, camera, new Size(resolutionWidth, resolutionHeight));
            image.Save(savePath);

        }

        /*void MaxVertsFunction(List<String> list)
        {
            vertexList = new Vector3[int.Parse(list[1])];
        }*/

        void NewVertex(List<String> list)
        {
            vertexList.Add(new Vector3(ConvertDouble(list[1]), ConvertDouble(list[2]), ConvertDouble(list[3])));
        }

        void CreateTriangle(List<String> list)
        {
            world.Add(new Triangle(vertexList[int.Parse(list[1])], vertexList[int.Parse(list[2])], vertexList[int.Parse(list[3])], material, transform));
        }

        void CreateWorld(List<String> list)
        {
            world = new World(new ColorRgb(ConvertDouble(list[1])*255, ConvertDouble(list[2])*255, ConvertDouble(list[3])* 255));
        }

        void CreateCamera(List<String> list)
        {
            camera = new Pinhole(new Vector3(ConvertDouble(list[1]), ConvertDouble(list[2]), ConvertDouble(list[3])), new Vector3(ConvertDouble(list[4]), ConvertDouble(list[5]), ConvertDouble(list[6])), new Vector3(ConvertDouble(list[7]), ConvertDouble(list[8]), ConvertDouble(list[9])), 1);
        }

        void CreateSphere(List<String> list)
        {
            world.Add(new Sphere(new Vector3(ConvertDouble(list[1]), ConvertDouble(list[2]), ConvertDouble(list[3])), ConvertDouble(list[4]), material, transform));
        }

        void CreateLight(List<String> list)
        {
            world.AddLight(new PointLight(new Vector3(ConvertDouble(list[1]), ConvertDouble(list[2]), ConvertDouble(list[3])), Color.White));
        }

        void OutputFile(List<String> list)
        {
            savePath = list[1];
        }

        void ImageSize(List<String> list)
        {
            resolutionWidth = int.Parse(list[1]);
            resolutionHeight = int.Parse(list[2]);
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

        void SetMaxDepth(List<String> list)
        {
            maxDepth = int.Parse(list[1]);
        }

        void SetDiffuse(List<String> list)
        {
            matColor = new ColorRgb(ConvertDouble(list[1]) * 255, ConvertDouble(list[2]) * 255, ConvertDouble(list[3]) * 255);
            material.ChangeColor(matColor);
        }

        void SetShinines(List<String> list)
        {
            int shine = int.Parse(list[1]);
            if(shine < 20)
            {
                material = new PerfectDiffuse(matColor);
            } else if(shine < 40)
            {
                material = new Phong(matColor, 0.8, 1, 30);
            } else
            {
                material = new Reflective(matColor, 0.1, 1, 300, 0.9);
            }
        }

        void SetRotate(List<String> list)
        {
            if(int.Parse(list[1]) != 0)
            {
                transform.transformList.Add(Matrix4x4.CreateRotationX(DegreeToRadian(ConvertDouble(list[4]))));
            }

            if (int.Parse(list[2]) != 0)
            {
                transform.transformList.Add(Matrix4x4.CreateRotationY(DegreeToRadian(ConvertDouble(list[4]))));
            }

            if (int.Parse(list[3]) != 0)
            {
                transform.transformList.Add(Matrix4x4.CreateRotationZ(DegreeToRadian(ConvertDouble(list[4]))));
            }
        }

        void SetScale(List<String> list)
        {
            float x = (float)ConvertDouble(list[1]);
            float y = (float)ConvertDouble(list[2]);
            float z = (float)ConvertDouble(list[3]);
            transform.transformList.Add(Matrix4x4.CreateScale(x, y, z));
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


        float DegreeToRadian(double angle)
        {
            return (float)(angle * Math.PI / (float)180);
        }

        

        public static double ConvertDouble(string tekst)
        {
            if(tekst.Length > 2)
            {
                if (tekst[1] != 0)
                {
                    tekst.Insert(1, "0");
                }
            }
            
            double returned;
            return returned = double.Parse(tekst, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
