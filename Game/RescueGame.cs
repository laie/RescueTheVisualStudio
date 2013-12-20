#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
using FarseerPhysics.Dynamics;
using FarseerPhysics.DebugView;
using FarseerPhysics.Factories;
#endregion

namespace TestMonoGame
{
    public class RescueGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WorldManager worldManager = new WorldManager();
        Player player;
        Controller controller;
        FireZone firezone;

        Texture2D texture;

        public RescueGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        { base.Initialize(); }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Texture2D.FromStream(GraphicsDevice, new FileStream(@"Content\painted.png", FileMode.Open));

            worldManager.Load(GraphicsDevice, Content);

            player = new Player(worldManager);
            worldManager.Camera.TrackingBody = player.Body;
            controller = new Controller(player);

            var wall = new Wall(worldManager, new Vector2(200, 10));
            wall.Position = new Vector2(0, 10);

            wall = new Wall(worldManager, new Vector2(10, 200));
            wall.Position = new Vector2(-0, -80);

            var enemygradea = new EnemyGradeA(worldManager);
            enemygradea.Position = new Vector2(10, 0);

            var enemyreport = new EnemyGradeReport(worldManager);
            enemyreport.Position = new Vector2(30, 0);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(40, -20);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            controller.Update(gameTime);
            worldManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(10, 10), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            worldManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
