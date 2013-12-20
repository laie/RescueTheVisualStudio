using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RescueGame
{
    public class Wall : StaticObject
    {
        Vector2 size;

        public Wall(WorldManager WorldManager, Vector2 Size)
            : base(WorldManager)
        {
            FixtureFactory.AttachRectangle(Size.X, Size.Y, 1, Vector2.Zero, Body);
            this.size = Size;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            base.Draw(SpriteBatch);
            var texture = worldManager.Textures["painted.png"];
            Vector2 pos = worldManager.Camera.ConvertWorldToScreen(Position),
                sz = worldManager.Camera.ConvertWorldToScreen(Position+size)-pos;

            SpriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, (int)sz.X, (int)sz.Y), null, Color.White, 0, new Vector2(texture.Width/2, texture.Height/2), SpriteEffects.None, 0);
        }
    }
}
