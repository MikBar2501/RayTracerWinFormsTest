using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    class Light
    {
        public Light(Vector3 vector, bool Directed, ColorRgb color)
        {
            this.Vector = vector;
            this.Directed = Directed;
            this.Color = color;
        }
        public Vector3 Vector { get; private set; }
        public ColorRgb Color { get; private set; }
        public bool Directed { get; private set; }
    }
}
