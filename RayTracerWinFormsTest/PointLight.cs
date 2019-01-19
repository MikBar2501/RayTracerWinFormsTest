﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerWinFormsTest
{
    class PointLight
    {
        public PointLight(Vector3 position, ColorRgb color) {
            this.Position = position;
            this.Color = color;
        }

        public Vector3 Position { get; private set; }
        public ColorRgb Color { get; private set; }
        public static double constant { get; private set; }
        public static double linear { get; private set; }
        public static double quadratic { get; private set; }
    }
}
