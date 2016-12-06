using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    public class Camera2D
    {
        private readonly Viewport _viewport;
        public Camera2D(GraphicsDevice device)
        {
            _viewport = device.Viewport;

            Rotation = 0;
            Zoom = GameSettings.DefaultCameraZoom;

            Origin   = new Vector2(_viewport.Width / 2f, _viewport.Height / 2f);
            Position = new Vector2((GameSettings.Width - GameSettings.Width) / 2, 0);//Vector2.Zero;
        }

        public Vector2 Position { get; set; }

        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Vector2 Origin { get; set; }

        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }
    }
}