using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RescueGame
{
    class EnemyGradeReport : Actor
    {
        public EnemyGradeReport(WorldManager WorldManager): base (WorldManager)
        {
            Health = 200;
            FixtureFactory.AttachRectangle(10, 14, 0.1f, Vector2.Zero, Body);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Body.Rotation = 0;
        }

        public override void Draw(SpriteBatch SpriteBatch)
        {
            Texture2D texture;
            if (Health >= 200) texture = worldManager.Textures["report1.png"];
            else if (Health >= 150) texture = worldManager.Textures["report2.png"];
            else if ( health >= 100 ) texture = worldManager.Textures["report3.png"];
            else texture = worldManager.Textures["report4.png"];

            SpriteBatch.Draw(texture, worldManager.Camera.ConvertWorldToScreen(Position), null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1.5f, SpriteEffects.None, 0);
        }
    }
}
