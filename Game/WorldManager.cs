using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.DebugView;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TestMonoGame
{
    /// <summary>
    /// 물리엔진 관리해줌
    /// </summary>
    public class WorldManager
    {
        public World World { get; private set; }
        DebugViewXNA debugWorldView;

        public Camera2D Camera { get; private set; }

        LinkedList<GameObject> gameObjects = new LinkedList<GameObject>();

        public WorldManager()
        {
        }

        public void Load(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            World = new World(new Vector2(0, 0));

            debugWorldView = new DebugViewXNA(World);
            debugWorldView.LoadContent(GraphicsDevice, Content);

            Camera = new Camera2D(GraphicsDevice);
            Camera.Zoom = 0.2f;
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
            debugWorldView.RenderDebugData(ref Camera.SimProjection, ref Camera.SimView);
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
