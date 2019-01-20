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

namespace RayTracerWinFormsTest
{
    class FileReader
    {
        World world = new World(Color.White);
        List<Vector3> vertexList = new List<Vector3>();
        //Vector3[] vertexList;
        List<Transformation> TransformationsStack = new List<Transformation>();
        //IMaterial material = new PerfectDiffuse(Color.Purple);
        IMaterial material = new Reflective(Color.LightBlue, 0.4, 1, 300, 0.6);
        ICamera camera;
        string savePath = "n.png";
        int resolutionWidth = 1024;
        int resolutionHeight = 1024;


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
                        //CreateWorld(lineString);
                        break;

                    case "pushTransform":
                        
                        break;

                    case "popTransform":
                        
                        break;

                    case "translate":
                        
                        break;

                    case "scale":
                        
                        break;

                    case "rotate":
                        
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
                        
                        break;

                    case "specular":
                        
                        break;

                    case "emission":
                        
                        break;

                    case "shininess":
                        
                        break;

                    case "maxdepth":
                        
                        break;

                    case "output":
                        OutputFile(lineString);
                        break;

                    default:
                        continue;
                }

            }
            //world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0), material));

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
            world.Add(new Triangle(vertexList[int.Parse(list[1])], vertexList[int.Parse(list[2])], vertexList[int.Parse(list[3])], material));
        }

        void CreateWorld(List<String> list)
        {
            world = new World(new ColorRgb(ConvertDouble(list[1]), ConvertDouble(list[2]), ConvertDouble(list[3])));
        }

        void CreateCamera(List<String> list)
        {
            camera = new Pinhole(new Vector3(ConvertDouble(list[1]), ConvertDouble(list[2]), ConvertDouble(list[3])), new Vector3(ConvertDouble(list[4]), ConvertDouble(list[5]), ConvertDouble(list[6])), new Vector3(ConvertDouble(list[7]), ConvertDouble(list[8]), ConvertDouble(list[9])), 1);
        }

        void CreateSphere(List<String> list)
        {
            world.Add(new Sphere(new Vector3(ConvertDouble(list[1]), ConvertDouble(list[2]), ConvertDouble(list[3])), ConvertDouble(list[4]), material));
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



        double ConvertDouble(string tekst)
        {
            double returned = 0;
            bool sign = false;
            double mod = 1.0;
            bool wasDot = false;
            string number = "";
            for (int i = 0; i < tekst.Length; i++)
            {
                if (tekst[i] == '+')
                {
                    continue;
                }
                if (tekst[i] == '-')
                {
                    sign = true;
                    continue;
                }
                if (tekst[i] == '.')
                {
                    if(!number.Equals(""))
                    {
                        if(sign)
                        {
                            returned += double.Parse(number) * -1;
                            number = "";
                        }
                        else
                        {
                            returned += double.Parse(number) * 1;
                            number = "";
                        }
                        
                    }
                    wasDot = true;
                }
                else
                {
                    number += tekst[i];
                    if(wasDot)
                    {
                        mod *= 0.1;
                    }
                }

                if (!number.Equals(""))
                {
                    if (sign)
                    {
                        returned += double.Parse(number) * mod * -1;
                    } else
                    {
                        returned += double.Parse(number) * mod * 1;
                    }
                        
                }

                
            }
            return returned;
        }
    }
}
