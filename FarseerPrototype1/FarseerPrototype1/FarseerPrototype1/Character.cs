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
    class Character : DrawablePhysicsObject {
        public Character(World world, Texture2D texture, Vector2 size, float mass) : base(world, texture, size) {

            body = BodyFactory.CreateCapsule(world, (size.Y-(2*size.X)) * CoordinateHelper.pixelToUnit, size.X * CoordinateHelper.pixelToUnit, mass);
           
            this.Body.BodyType = BodyType.Dynamic;
            this.Body.FixedRotation = true;
            this.Body.Rotation = 0.0f;
            this.Body.Mass = mass;

        }

        public void jump() {
            this.Body.ApplyForce(new Vector2(0f,-29000f));
        }

        public void moveRight() {
            if (this.body.LinearVelocity.X < 3) {
                this.Body.ApplyForce(new Vector2(600f, 0f));
            }
        }

        public void moveLeft() {
            if (this.body.LinearVelocity.X > -3) {
                this.Body.ApplyForce(new Vector2(-600f, 0f));
            }
        }
    }
}
