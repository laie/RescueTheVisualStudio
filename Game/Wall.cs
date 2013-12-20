using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace TestMonoGame
{
    public class Wall : StaticObject
    {
        public Wall(WorldManager WorldManager, Vector2[] PolygonPoints)
            : base(WorldManager)
        {
            if (PolygonPoints == null || PolygonPoints.Length == 0) throw new InvalidOperationException();

            FixtureFactory.AttachPolygon(new Vertices(PolygonPoints), 1, Body);
        }

        public Wall(WorldManager WorldManager, Vector2 Size)
            : base(WorldManager)
        {
            FixtureFactory.AttachRectangle(Size.X, Size.Y, 1, Vector2.Zero, Body);
        }
    }
}
