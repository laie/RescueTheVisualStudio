using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace TestMonoGame
{
    /// <summary>
    /// 적들이나 플레이어가 닿으면 불타죽도록 만듦
    /// A학점 적들을 물리치자!
    /// </summary>
    class FireZone : StaticObject
    {
        public FireZone(WorldManager WorldManager) :
            base(WorldManager)
        {
            FixtureFactory.AttachCircle(2, 0, Body);
            Body.OnCollision += Body_OnCollision;
        }

        bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            var diffvec = fixtureA.Body.Position - fixtureB.Body.Position;
            fixtureB.Body.LinearVelocity += -diffvec*10;
            return true;
        }
    }
}
