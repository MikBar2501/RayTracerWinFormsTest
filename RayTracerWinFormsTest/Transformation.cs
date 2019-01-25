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
        public Matrix4x4 transform;
        public List<Matrix4x4> transformList;

        public Transformation(Transformation transforms)
        {
            transform = Matrix();
            transformList = new List<Matrix4x4>();
            foreach (Matrix4x4 transform in transforms.transformList)
            {
                transformList.Add(transform);
            }
        }

        public Transformation()
        {
            transform = Matrix();
            transformList = new List<Matrix4x4>();
        }

        public Matrix4x4 GetMatrix()
        {
            if(transformList == null)
            {
                return transform;
            } else
            {
                transform = Matrix();
                for (int i = transformList.Count - 1; i >= 0; i--)
                {
                  transform = transformList[i] * transform;
                }
                return transform;
            }
           
        }

        public Matrix4x4 GetReverseTransform()
        {
            Matrix4x4 reverseTransform;
            Matrix4x4.Invert(transform, out reverseTransform);
            return reverseTransform;
        }

        public Matrix4x4 Matrix()
        {
            Matrix4x4 newMatrix = new Matrix4x4();
            newMatrix.M11 = 1;
            newMatrix.M12 = 0;
            newMatrix.M13 = 0;
            newMatrix.M14 = 0;

            newMatrix.M21 = 0;
            newMatrix.M22 = 1;
            newMatrix.M23 = 0;
            newMatrix.M24 = 0;

            newMatrix.M31 = 0;
            newMatrix.M32 = 0;
            newMatrix.M33 = 1;
            newMatrix.M34 = 0;

            newMatrix.M41 = 0;
            newMatrix.M42 = 0;
            newMatrix.M43 = 0;
            newMatrix.M44 = 1;

            return newMatrix;
        }
    }
}
