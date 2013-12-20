using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FarseerPhysics.DebugView;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RescueGame
{
    /// <summary>
    /// 물리엔진 관리해줌
    /// </summary>
    public class WorldManager
    {
        public World World { get; private set; }
        DebugViewXNA debugWorldView;
        SpriteBatch spriteBatch;

        public Camera2D Camera { get; private set; }
        public Dictionary<string, Texture2D> Textures { get; private set; }

        LinkedList<GameObject> gameObjects = new LinkedList<GameObject>();

        public WorldManager()
        {
            Textures = new Dictionary<string, Texture2D>();
        }

        public void Load(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            World = new World(new Vector2(0, 0));

            debugWorldView = new DebugViewXNA(World);
            debugWorldView.LoadContent(GraphicsDevice, Content);

            Camera = new Camera2D(GraphicsDevice);
            Camera.Zoom = 0.3f;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadTexture(GraphicsDevice, "report1.png");
            LoadTexture(GraphicsDevice, "report2.png");
            LoadTexture(GraphicsDevice, "report3.png");
            LoadTexture(GraphicsDevice, "report4.png");
            LoadTexture(GraphicsDevice, "FireLight.png");
            LoadTexture(GraphicsDevice, "AGrade.png");
            LoadTexture(GraphicsDevice, "FireSprite.png");
            LoadTexture(GraphicsDevice, "player.png");
            LoadTexture(GraphicsDevice, "panel.png");
            LoadTexture(GraphicsDevice, "painted.png");
        }

        private void LoadTexture(GraphicsDevice GraphicsDevice, string FileName)
        {
            Textures.Add(FileName, Texture2D.FromStream(GraphicsDevice, new FileStream(@"Content\"  +FileName, FileMode.Open)));
        }


        public void Update(GameTime gameTime)
        {
            World.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            Camera.Update(gameTime);

            foreach (var obj in gameObjects)
                obj.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            //debugWorldView.RenderDebugData(ref Camera.SimProjection, ref Camera.SimView);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            foreach (var obj in gameObjects)
                obj.Draw(spriteBatch);
            spriteBatch.End();
        }

        public LinkedListNode<GameObject> RegisterGameObject(GameObject GameObject)
        {
            if (GameObject == null) throw new ArgumentNullException("GameObject");
            return gameObjects.AddLast(GameObject);
        }

        public void UnregisterGameObject(LinkedListNode<GameObject> GameObjectNode)
        {
            if (GameObjectNode.Value == null || GameObjectNode.Value.IsDestroyed == true) throw new InvalidOperationException();
            gameObjects.Remove(GameObjectNode);
        }
    }
}
