using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    class DirectionaLight 
    {
        public DirectionaLight(Vector3 Position, ColorRgb color, Vector3 Direction)
        {
            this.Position = Position;
            this.Color = color;
            this.Direction = Direction;
        }
        public Vector3 Position { get; private set; }
        public ColorRgb Color { get; private set; }
        public Vector3 Direction;
    }
}
