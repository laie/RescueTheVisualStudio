using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;

namespace TestMonoGame
{
    public class Player : Actor
    {
        public Player(WorldManager WorldManager): base(WorldManager)
        {
            FixtureFactory.AttachCircle(1, 1, Body);

            Body.OnCollision += Body_OnCollision;
        }

        bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            var opposite = fixtureB.Body.UserData as Actor;
            if (IsDashing && opposite != null)
            {
                opposite.Health -= (float)Math.Sqrt(Velocity.X * Velocity.X + Velocity.Y * Velocity.Y);

                var diffvec = fixtureB.Body.Position - fixtureA.Body.Position;
                fixtureA.Body.LinearVelocity += -diffvec * 10;
                fixtureB.Body.LinearVelocity += diffvec * 10;
            }
            return true;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
    }
}
