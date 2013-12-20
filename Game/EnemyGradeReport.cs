using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace TestMonoGame
{
    class EnemyGradeReport : Actor
    {
        public EnemyGradeReport(WorldManager WorldManager): base (WorldManager)
        {
            FixtureFactory.AttachRectangle(10, 14, 0.1f, Vector2.Zero, Body);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Body.Rotation = 0;
        }
    }
}
