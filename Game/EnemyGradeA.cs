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
    public class EnemyGradeA : Actor
    {
        public override float Health
        {
            get { return base.Health; }
            set { }
        }

        public EnemyGradeA(WorldManager WorldManager): base(WorldManager)
        {
            FixtureFactory.AttachRectangle(3f, 4.2f, 1, Vector2.Zero, Body);
            Body.OnCollision += Body_OnCollision;
        }

        bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            if (fixtureB.Body.UserData is FireZone)
            {
                health -= 50;
                if (health <= 0) Destroy();
            }

            return true;
        }


    }
}
