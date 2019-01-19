using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracerWinFormsTest
{
    class FileReader
    {
        World world;
        List<Vector3> vertexList = new List<Vector3>();
        //Vector3[] vertexList;
        List<Transformation> TransformationsStack = new List<Transformation>();
        IMaterial material = new PerfectDiffuse(Color.Purple);;
        string savePath;
        int resolutionWidth;
        int resolutionHeight;
        

        public void ReadFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            foreach(string line in lines)
            {
                if(line[0].Equals("#") || line.Equals(""))
                {
                    continue;
                }
                List<string> lineString = new List<string>();
                string tempString = "";
                for(int i = 0; i<line.Length; i++)
                {
                    if(line[i].Equals(" "))
                    {
                        lineString.Add(tempString);
                        tempString = "";
                    }

                    tempString += line[i];

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
                        
                        break;

                    case "camera":
                        CreateCamera(lineString);
                        break;

                    case "size":
                        
                        break;

                    case "sphere":
                        
                        break;

                    case "ambient":
                        CreateWorld(lineString);
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
                        
                        break;

                    case "point":
                        
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

                        break;

                    default:
                        continue;
                }

            }

        }

        /*void MaxVertsFunction(List<String> list)
        {
            vertexList = new Vector3[int.Parse(list[1])];
        }*/

        void NewVertex(List<String> list)
        {
            vertexList.Add(new Vector3(double.Parse(list[1]), double.Parse(list[2]), double.Parse(list[3])));
        }

        void CreateTriangle(List<String> list)
        {
            world.Add(new Triangle(vertexList[int.Parse(list[1])], vertexList[int.Parse(list[2])],vertexList[int.Parse(list[3])], material));
        }

        void CreateWorld(List<String> list)
        {
            world = new World(new ColorRgb(double.Parse(list[1]), double.Parse(list[2]), double.Parse(list[3])));
        }

        void CreateCamera(List<String> list)
        {
            ICamera camera = new Pinhole(new Vector3(double.Parse(list[1]), double.Parse(list[2]), double.Parse(list[3])), new Vector3(double.Parse(list[4]), double.Parse(list[5]), double.Parse(list[6])), new Vector3(double.Parse(list[7]), double.Parse(list[8]), double.Parse(list[9])), 1);
        }

        void CreateSphere(List<String> list)
        {
            world.Add(new Sphere(new Vector3(double.Parse(list[1]), double.Parse(list[2]), double.Parse(list[3])), double.Parse(list[4]), material));
        }




        }
}
