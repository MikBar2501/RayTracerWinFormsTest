using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    class Light
    {
        public Light(Vector3 Position, ColorRgb color)
        {
            this.Position = Position;
            this.Color = color;
        }
        public Vector3 Position { get; private set; }
        public ColorRgb Color { get; private set; }
    }
}
