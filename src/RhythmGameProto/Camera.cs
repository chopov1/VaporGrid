using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameProto
{
    public class Camera : GameComponent, ISceneComponenet
    {
        public float Zoom;
        public Vector2 Position;
        protected Rectangle Bounds;
        protected Rectangle VisibleArea;
        public Matrix Transform;

        private float zoom, prevzoom;

        public Camera(Game game) : base(game)
        {
            
        }

        public override void Initialize()
        {
            Bounds = Game.GraphicsDevice.Viewport.Bounds;
            Zoom = 1f;
            Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateCamera(Game.GraphicsDevice.Viewport, gameTime);
        }

        private void updateVisibleArea()
        {
            Matrix inverseViewMatrix = Matrix.Invert(Transform);

            Vector2 topLeft = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            Vector2 topRight = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            Vector2 bottomLeft = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            Vector2 bottomRight = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            Vector2 min = new Vector2(
                MathHelper.Min(topLeft.X, MathHelper.Min(topRight.X, MathHelper.Min(bottomLeft.X, bottomRight.X))), 
                MathHelper.Min(topLeft.Y, MathHelper.Min(topRight.Y, MathHelper.Min(bottomLeft.Y, bottomRight.Y))));
            Vector2 max = new Vector2(
                MathHelper.Max(topLeft.X, MathHelper.Max(topRight.X, MathHelper.Max(bottomLeft.X, bottomRight.X))),
                MathHelper.Max(topLeft.Y, MathHelper.Max(topRight.Y, MathHelper.Max(bottomLeft.Y, bottomRight.Y))));

            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X-min.X), (int)(max.Y-min.Y));
        }

        private void updateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0))
                * Matrix.CreateScale(Zoom)
                * Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            updateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = Position + movePosition;
            Position = newPosition;
        }

        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;
            if(Zoom < 1f)
            {
                Zoom = 1;
            }
            if (Zoom > 1.1f)
            {
                Zoom = 1.1f;
            }
        }

        public void Bounce() { 

        }

        protected int moveSpeed;
        protected Vector2 cameraMovement;

        public virtual void UpdateCamera(Viewport bounds, GameTime gameTime)
        {
            Bounds = bounds.Bounds;
            updateMatrix();

            cameraMovement = Vector2.Zero;


            if (Zoom > .8f)
            {
                moveSpeed = 15;
            }
            else if (Zoom < .8f && Zoom >= .6f)
            {
                moveSpeed = 20;
            }
            else if (Zoom < .6f && Zoom > .35f)
            {
                moveSpeed = 25;
            }
            else if (Zoom <= .35f)
            {
                moveSpeed = 30;
            }
            else
            {
                moveSpeed = 10;
            }

            UpdateCameraMove(gameTime);

            prevzoom = zoom;
            zoom = Zoom;
            if (prevzoom != zoom)
            {
                //Console.WriteLine(zoom);
            }

            MoveCamera(cameraMovement);
        }

        protected virtual void UpdateCameraMove(GameTime gameTime)
        {
            //nothing
        }

        public void Load()
        {
            Enabled= true;
        }

        public void UnLoad()
        {
            Enabled = false;
        }
    }
}
