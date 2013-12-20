using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RescueGame
{
    class Controller
    {
        public Actor Actor { get; private set; }

        public Controller(Actor Actor)
        { this.Actor = Actor; }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) Actor.Velocity += new Vector2(-1.5f, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) Actor.Velocity += new Vector2(+1.5f, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) Actor.Velocity += new Vector2(0, -1.5f);
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) Actor.Velocity += new Vector2(0, +1.5f);

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) Actor.IsDashing = true;
        }
    }
}
