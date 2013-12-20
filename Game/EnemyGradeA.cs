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
    public class EnemyGradeA : Actor
    {
        public override float Health
        {
            get { return base.Health; }
            set { }
        }

        public EnemyGradeA(WorldManager WorldManager): base(WorldManager)
        {
            FixtureFactory.AttachRectangle(3f, 4.2f, 0.2f, Vector2.Zero, Body);
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

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            base.Draw(SpriteBatch);
            var texture = worldManager.Textures["AGrade.png"];
            SpriteBatch.Draw(texture, worldManager.Camera.ConvertWorldToScreen(Position), null, Color.White, Rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1.5f, SpriteEffects.None, 0);
        }

    }
}
