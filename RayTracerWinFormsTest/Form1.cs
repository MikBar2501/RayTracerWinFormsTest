using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //double diff = double.Parse(txtDiff.Text);
            //double spec = double.Parse(txtSpec.Text);
            //double exp = double.Parse(txtExp.Text);
            //double refl = double.Parse(txtRef.Text);
            
            World world = new World(Color.Gray);


            IMaterial redMat = new Reflective(Color.LightCoral, 0.4, 1, 300, 0.6);
            IMaterial greenMat = new Reflective(Color.Green, 0.4, 1, 300, 0.6);
            IMaterial blueMat = new Reflective(Color.LightBlue, 0.4, 1, 300, 0.6);
            IMaterial grayMat = new Reflective(Color.Gray, 0.4, 1, 300, 0.6);
            IMaterial purpMat = new PerfectDiffuse(Color.Purple);
            IMaterial pinkMat = new PerfectDiffuse(Color.Pink);
            IMaterial whitekMat = new PerfectDiffuse(Color.White);


            //world.Add(new Sphere(new Vector3(-4, 0, 0), 2, redMat));
            //world.Add(new Sphere(new Vector3(4, 0, 0), 2, greenMat));
            world.Add(new Sphere(new Vector3(0, 0, 3), 2, blueMat));
            //world.Add(new Sphere(new Vector3(-2.5, 0, 3), 2, purpMat));
            //world.Add(new Sphere(new Vector3(2.5, 0, 3), 2, pinkMat));
            //world.Add(new Sphere(new Vector3(0, 0, 5), 2, purpMat));
            world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0), whitekMat));
            //world.Add(new Triangle(new Vector3(-2, 0, 1), new Vector3(2, 0, 1), new Vector3(0, 2, 4), purpMat));
            world.AddLight(new PointLight(new Vector3(0, 0, -1), Color.White));
            ICamera camera = new Pinhole(new Vector3(0, 4, -1), new Vector3(0, 0, 0), new Vector3(0, -1, 0), 1);

            //ICamera camera = new Pinhole(new Vector3(0, 0, 4), new Vector3(0, 0, 0), new Vector3(0, 1, 0), 1);
            Vector3 vertex0 = new Vector3(-1, -1, 0);
            Vector3 vertex1 = new Vector3(1, -1, 0);
            Vector3 vertex2 = new Vector3(1, 1, 0);
            Vector3 vertex3 = new Vector3(-1, 1, 0);

            world.Add(new Triangle(vertex0, vertex1, vertex2, redMat));
            world.Add(new Triangle(vertex0, vertex2, vertex3, redMat));

            Raytracer tracer = new Raytracer(5);
            
       
            Bitmap image = tracer.Raytrace(world, camera, new Size(1024, 1024));
            DateTime data = DateTime.UtcNow;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            string saveName = "raytraced-" + data.ToString("dd-MM-yyyy") + ".png";
            image.Save(saveName);
            pictureBox1.Image = image;
            //pictureBox1.ImageLocation = @"G:\Projekty\Elementy Grafiki\RaytracerTest\RayTracerWinFormsTest — kopia\RayTracerWinFormsTest\bin\Debug\raytraced.png";
        }
    }
}
