using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    interface ICamera
    {
        Ray GetRayTo(Vector2 relativeLocation);
    }
}
