using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace FarseerPrototype1 {
    class Bullet : DrawablePhysicsObject{
        public Bullet(World world, Texture2D texture, Vector2 size, float mass, Vector2 position, Vector2 angle) : base(world, texture, size) {
            body = BodyFactory.CreateCircle(world, size.X/2 * CoordinateHelper.pixelToUnit, mass);
            this.Body.BodyType = BodyType.Dynamic;
            this.Position = position;
            applyForce(angle);
            Body.OnCollision += Body_OnCollision; // or use a lambda
        }

        bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact){
            fixtureB.Body.BodyType = BodyType.Dynamic;
            if (fixtureB.Body.Mass == 101){
                fixtureB.Body.ApplyForce(new Vector2(this.Body.LinearVelocity.X*1000, this.Body.LinearVelocity.Y*1000));
            }
            return true;
        }

        private void applyForce(Vector2 velocity) {
            base.Body.ApplyForce(velocity/15);
        }
    }
}
