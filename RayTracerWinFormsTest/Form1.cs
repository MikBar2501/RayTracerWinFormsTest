using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace RayTracerWinFormsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generaturButton_Click(object sender, EventArgs e)
        {

            /*

            World world = new World(Color.Gray);


            IMaterial redMat = new Reflective(Color.LightCoral, 0.4, 1, 300, 0.6);
            IMaterial greenMat = new Reflective(Color.Green, 0.2, 1, 300, 0.8);
            IMaterial blueMat = new Reflective(Color.LightBlue, 0.4, 1, 300, 0.6);
            IMaterial refMat = new Reflective(Color.Blue, 0.1, 1, 300, 0.9);
            IMaterial perfMat = new PerfectDiffuse(Color.LightBlue);
            IMaterial phongMat = new Phong(Color.LightBlue, 0.8, 1, 30);
            IMaterial pinkMat = new PerfectDiffuse(Color.Pink);
            IMaterial whitekMat = new PerfectDiffuse(Color.White);

            Vector3 vertex0 = new Vector3(0, 1, 1);
            Vector3 vertex1 = new Vector3(-1, 1, -1);
            Vector3 vertex2 = new Vector3(-1, 1, -1);
            Vector3 vertex3 = new Vector3(-1, 1, 0);
            Vector3 vertex4 = new Vector3(-1, 0.32324, 3);

            Transformation transform = new Transformation();

            world.Add(new Sphere(new Vector3(-4, 0, 0), 2, redMat,transform));
            SetScale(1f, 2f, 1f, transform);
            world.Add(new Sphere(new Vector3(4, 0, 0), 2, greenMat, transform));
            //SetScale(1f, 1f, 2f, transform);
            world.Add(new Sphere(new Vector3(0, 0, 3), 2, pinkMat, transform));
            SetRotate(1f, 2f, 1f, 90, transform);
            //SetScale(0.2f, 0.2f, 0.2f, transform);
            world.Add(new Sphere(new Vector3(-2.5, 0, 3), 2, refMat, transform));
            //SetScale(0.4f, 0.4f, 0.4f, transform);
            SetTranslate(1f, -2f, 1f, transform);
            world.Add(new Sphere(new Vector3(2.5, 0, 3), 2, pinkMat, transform));
            //SetScale(0.1f, 0.4f, 0.1f, transform);
            world.Add(new Sphere(new Vector3(0, 0, 5), 2, pinkMat, transform));
            //world.Add(new Sphere(new Vector3(0, 0, 5), 2, perfMat));
            //world.Add(new Sphere(new Vector3(0, 0, 5), 2, purpMat));

            //world.Add(new Sphere(new Vector3(-4, 0, 0), 2, refMat));
            //world.Add(new Sphere(new Vector3(4, 0, 0), 2, perfMat));
            //world.Add(new Sphere(new Vector3(0, 0, 3), 2, phongMat));
            world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0), whitekMat));
            //world.Add(new Triangle(new Vector3(-2, 0, 1), new Vector3(2, 0, 1), new Vector3(0, 2, 4), purpMat));
            //world.Add(new Triangle(vertex0, vertex1, vertex2, purpMat));
            //world.Add(new Triangle(vertex2, vertex3, vertex4, purpMat));
            //SetScale(1f, 1f, 1f, transform);
            world.Add(new Triangle(vertex0, vertex1, vertex2, pinkMat, transform));
            world.Add(new Triangle(vertex0, vertex2, vertex3, redMat, transform));
            world.AddLight(new PointLight(new Vector3(0, 2, -1), Color.White));
            //ICamera camera = new Pinhole(new Vector3(0, 2, -3), new Vector3(0, 0, 0), new Vector3(0, -1, 0), 1);

            //ICamera camera = new Pinhole(new Vector3(0, 0, 4), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 1);

            ICamera camera = new Pinhole(new Vector3(0, 1, -8), new Vector3(0, 0, 0), new Vector3(0, -1, 0), 1);
            */
            /*World world = new World(Color.Gray);
            Transformation transform = new Transformation();
            ICamera camera = new Pinhole(new Vector3(0, 1, -3), new Vector3(0, 1, 2), new Vector3(0, 1, 0), 1);
            IMaterial material = new PerfectDiffuse(new ColorRgb(128,0,128));
            IMaterial material1 = new Phong(Color.LightBlue, 1, 1, 30);
            //IMaterial material = new Reflective(Color.Blue, 0.1, 1, 300, 0.9);
            world.AddLight(new PointLight(new Vector3(0, 2, -3), Color.White));

            Vector3 vertex1 = new Vector3(2, 2, 2);
            Vector3 vertex2 = new Vector3(-2, 2, 2);
            Vector3 vertex3 = new Vector3(-2, 0, 2);
            Vector3 vertex4 = new Vector3(2, 0, 2);

            world.Add(new Triangle(vertex1, vertex2, vertex3, material, transform));
            world.Add(new Triangle(vertex1, vertex3, vertex4, material, transform));

            world.Add(new Sphere(new Vector3(0,1,-1),0.5,material,transform));
            

        
            


            Raytracer tracer = new Raytracer(5);
            
       
            Bitmap image = tracer.Raytrace(world, camera, new Size(640, 480));
            DateTime data = DateTime.UtcNow;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //string saveName = "raytraced-" + data.ToString("dd-MM-yyyy") + ".png";
            string saveName = "test1.png";
            image.Save(saveName);
            pictureBox1.Image = image;
            //pictureBox1.ImageLocation = @"G:\Projekty\Elementy Grafiki\RaytracerTest\RayTracerWinFormsTest — kopia\RayTracerWinFormsTest\bin\Debug\raytraced.png";
            */
            


             /*string file = txtPath.ToString();
             FileReader fileReader = new FileReader();
             //fileReader.ReadFile(txtPath.ToString());
             fileReader.ReadFile("scene6.test");*/

            string file = txtPath.ToString();
            scene5File fileReader = new scene5File();
            //fileReader.ReadFile(txtPath.ToString());
            fileReader.Readscene5File("scene5.test");


        }

        /*void SetScale(float x,float y, float z, Transformation trans)
        {
            trans.transformList.Add(Matrix4x4.CreateScale(x, y, z));
        }

        void SetTranslate(float x, float y, float z, Transformation trans)
        {
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
            trans.transformList.Add(translate);
        }

        void SetRotate(float x, float y, float z, float angle, Transformation trans)
        {
            if (x != 0)
            {
                trans.transformList.Add(Matrix4x4.CreateRotationX(DegreeToRadian(angle)));
            }

            if (y != 0)
            {
                trans.transformList.Add(Matrix4x4.CreateRotationX(DegreeToRadian(angle)));
            }

            if (z != 0)
            {
                trans.transformList.Add(Matrix4x4.CreateRotationX(DegreeToRadian(angle)));
            }
        }

        float DegreeToRadian(double angle)
        {
            return (float)(angle * Math.PI / (float)180);
        }
       */
    }
}
