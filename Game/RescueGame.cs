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

namespace RescueGame
{
    public class RescueGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        WorldManager worldManager = new WorldManager();
        Player player;
        Controller controller;
        FireZone firezone;
        Wall lastwall;

        public EnemyGradeA[] enemygradeas = new EnemyGradeA[4];

        double panelCloseTick = 5;

        public RescueGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        { base.Initialize(); }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            worldManager.Load(GraphicsDevice, Content);

            player = new Player(worldManager);
            worldManager.Camera.TrackingBody = player.Body;
            player.Position = new Vector2(2, 18 * 2);
            controller = new Controller(player);

            var wall = new Wall(worldManager, new Vector2(100 * 2, 20 * 2));
            wall.Position = new Vector2(50 * 2, -10 * 2);

            wall = new Wall(worldManager, new Vector2(20 * 2, 140 * 2));
            wall.Position = new Vector2(-10 * 2, 50 * 2);

            wall = new Wall(worldManager, new Vector2(140 * 2, 20 * 2));
            wall.Position = new Vector2(50 * 2, 46 * 2);

            wall = new Wall(worldManager, new Vector2(20 * 2, 100 * 2));
            wall.Position = new Vector2(72 * 2, 50 * 2);

            wall = new Wall(worldManager, new Vector2(6 * 2, 8 * 2));
            wall.Position = new Vector2(3 * 2, 12 * 2);

            wall = new Wall(worldManager, new Vector2(6 * 2, 8 * 2));
            wall.Position = new Vector2(3 * 2, 24 * 2);

            wall = new Wall(worldManager, new Vector2(16 * 2, 8 * 2));
            wall.Position = new Vector2(18 * 2, 12 * 2);

            wall = new Wall(worldManager, new Vector2(16 * 2, 8 * 2));
            wall.Position = new Vector2(18 * 2, 24 * 2);

            wall = new Wall(worldManager, new Vector2(16 * 2, 8 * 2));
            wall.Position = new Vector2(38 * 2, 12 * 2);

            wall = new Wall(worldManager, new Vector2(16 * 2, 8 * 2));
            wall.Position = new Vector2(38 * 2, 24 * 2);

            wall = new Wall(worldManager, new Vector2(4 * 2, 8 * 2));
            wall.Position = new Vector2(18 * 2, 4 * 2);

            wall = new Wall(worldManager, new Vector2(4 * 2, 8 * 2));
            wall.Position = new Vector2(18 * 2, 32 * 2);

            wall = new Wall(worldManager, new Vector2(26 * 2, 8 * 2));
            wall.Position = new Vector2(49 * 2, 4 * 2);

            wall = new Wall(worldManager, new Vector2(26 * 2, 8 * 2));
            wall.Position = new Vector2(49 * 2, 32 * 2);

            lastwall = new Wall(worldManager, new Vector2(4 * 2, 4 * 2));
            lastwall.Position = new Vector2(88, 36);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(2, 2);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(30, 14);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(2, 70);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(30, 60);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(42, 8);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(70, 8);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(55, 70);

            firezone = new FireZone(worldManager);
            firezone.Position = new Vector2(70, 60);

            enemygradeas[0] = new EnemyGradeA(worldManager);
            enemygradeas[0].Position = new Vector2(30, 0);

            enemygradeas[1] = new EnemyGradeA(worldManager);
            enemygradeas[1].Position = new Vector2(10, 60);

            enemygradeas[2] = new EnemyGradeA(worldManager);
            enemygradeas[2].Position = new Vector2(56, 8);

            enemygradeas[3] = new EnemyGradeA(worldManager);
            enemygradeas[3].Position = new Vector2(45, 60);

            var enemyreport = new EnemyGradeReport(worldManager);
            enemyreport.Position = new Vector2(120, 37);
          
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            float count = 0;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < 4; i++)
                if (enemygradeas[i].IsDestroyed == false) count++;

            if (count == 0 )
                lastwall.Position = new Vector2(300, 36);

            controller.Update(gameTime);
            worldManager.Update(gameTime);

            base.Update(gameTime);

            panelCloseTick -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(new Vector3(0.1f, 0.1f, 0.1f)));

            // TODO: Add your drawing code here
            worldManager.Draw(gameTime);

            base.Draw(gameTime);

            float alpha = (float)panelCloseTick/5;
            Color color = new Color(Color.White, alpha);

            var texture = worldManager.Textures["panel.png"];
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(texture, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), null, color, 0, new Vector2(texture.Width/2, texture.Height/2), 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
