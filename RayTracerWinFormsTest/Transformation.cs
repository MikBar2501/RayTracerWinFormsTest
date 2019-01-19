using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RayTracerWinFormsTest
{
    class Transformation
    {
        public Matrix4x4 translate;
        public Matrix4x4 rotation;
        public Matrix4x4 scale;

        public Matrix4x4 transform;

        public List<Matrix4x4> transList;

        public Transformation(Transformation trans)
        {
            translate = trans.translate;
            rotation = trans.rotation;
            scale = trans.scale;
            transform = new Matrix4x4();
            transList = new List<Matrix4x4>();
            foreach (Matrix4x4 tran in trans.transList)
            {
                transList.Add(tran);
            }
        }

        public Transformation()
        {
            translate = new Matrix4x4();
            rotation = new Matrix4x4();
            scale = new Matrix4x4();
            transform = new Matrix4x4();
            transList = new List<Matrix4x4>();
        }

        public Matrix4x4 GetMatrix()
        {
            transform = new Matrix4x4();
            for (int i = transList.Count - 1; i >= 0; i--)
            {
                transform = transList[i] * transform;
            }
            return transform;
        }
    }
}
