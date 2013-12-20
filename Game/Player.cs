using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Factories;

namespace TestMonoGame
{
    public class Player : Actor
    {
        public Player(WorldManager WorldManager): base(WorldManager)
        {
            FixtureFactory.AttachCircle(1, 1, Body);
        }
    }
}
