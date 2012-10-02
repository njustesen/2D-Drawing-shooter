using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FarseerPrototype1 {
    class Floor : DrawablePhysicsObject{
        public Floor(World world, Texture2D texture, Vector2 size, float mass) : base(world, texture, size) {

            body = BodyFactory.CreateRectangle(world, size.X * CoordinateHelper.pixelToUnit, size.Y * CoordinateHelper.pixelToUnit, 1);
           
            this.Body.BodyType = BodyType.Static;
            this.Body.FixedRotation = true;
            this.Body.Rotation = 0.0f;
            this.Body.Mass = mass;

        }
    }
}
