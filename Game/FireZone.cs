using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace RescueGame
{
    /// <summary>
    /// 적들이나 플레이어가 닿으면 불타죽도록 만듦
    /// A학점 적들을 물리치자!
    /// </summary>
    class FireZone : StaticObject
    {
        double frame = 0;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            frame += gameTime.ElapsedGameTime.TotalSeconds*5;
            if (frame > 12) frame = 0;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            var texture = worldManager.Textures["FireSprite.png"];
            SpriteBatch.Draw(texture, worldManager.Camera.ConvertWorldToScreen(Position), new Rectangle(304 / 4 * ((int)frame % 4), 336 / 4 * ((int)frame / 4), 304 / 4, 336 / 4), Color.White, 0, new Vector2(304 / 8, 336 / 8), 2.0f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }
    }
}
