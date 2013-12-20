using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestMonoGame
{
    /// <summary>
    /// 파티클, 플레이어, 적 등을 포함한 게임 오브젝트
    /// </summary>
    public abstract class GameObject
    {
        WorldManager worldManager;
        LinkedListNode<GameObject> selfNode;

        public bool IsDestroyed { get; private set; }
        public virtual Vector2 Position { get; set; }
        
        public GameObject(WorldManager WorldManager)
        {
            if (WorldManager == null) throw new ArgumentNullException("WorldManager");
            this.worldManager = WorldManager;
            selfNode = WorldManager.RegisterGameObject(this);
        }

        public virtual void Destroy()
        {
            if (IsDestroyed) throw new InvalidOperationException();
            worldManager.UnregisterGameObject(selfNode);
            IsDestroyed = true;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw()
        {
        }
    }

    /// <summary>
    /// Body를 가진 GameObject(물리엔진에 영향을 주는 애들은 모두 얘를 상속)
    /// </summary>
    public abstract class PhysicalObject : GameObject
    {
        public Body Body { get; protected set; }
        public override Vector2 Position { get { return Body.Position; } set { Body.Position = value; } }
        public Vector2 Velocity { get { return Body.LinearVelocity; } set { Body.LinearVelocity = value; } }
        public float Rotation { get { return Body.Rotation; } set { Body.Rotation = value; } }

        public PhysicalObject(WorldManager WorldManager): base(WorldManager)
        {
            this.Body = new Body(WorldManager.World);
            Body.BodyType = BodyType.Dynamic;
        }
    }

    /// <summary>
    /// 움직이지 않는 애들: 벽, 장애물 등
    /// </summary>
    public abstract class StaticObject : PhysicalObject
    {
        public StaticObject(WorldManager WorldManager)
            : base(WorldManager)
        {
            this.Body.BodyType = BodyType.Static;
        }
    }

    /// <summary>
    /// 움직이는 애들: 플레이어 / 적 / 보스
    /// </summary>
    public abstract class Actor : PhysicalObject
    {
        public Actor(WorldManager WorldManager): base(WorldManager)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Body.LinearVelocity *= (float)(Math.Pow(0.9f, 0.1f*gameTime.ElapsedGameTime.TotalMilliseconds));

            Body.Rotation = (float)Math.Atan2(Body.LinearVelocity.Y, Body.LinearVelocity.X);
        }

    }

}
