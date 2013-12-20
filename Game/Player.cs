using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RescueGame
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
            Body.Rotation = (float)Math.Atan2(Body.LinearVelocity.Y, Body.LinearVelocity.X);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            base.Draw(SpriteBatch);
            var texture = worldManager.Textures["player.png"];
            SpriteBatch.Draw(texture, worldManager.Camera.ConvertWorldToScreen(Position), null, Color.White, (float)(Rotation+Math.PI/2), new Vector2(texture.Width / 2, texture.Height / 2), 0.1f, SpriteEffects.None, 0);
        }
    }
}
