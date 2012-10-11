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
    class Point : DrawablePhysicsObject{
        int life;
        bool dead;

        public Point(World world, Texture2D texture, Vector2 size, float mass) : base(world, texture, size) {

            body = BodyFactory.CreateCircle(world, (int)(size.Y/2) * CoordinateHelper.pixelToUnit, mass, null);
            this.Body.Mass = mass;
            this.Body.BodyType = BodyType.Static;
            this.Body.FixedRotation = true;
            this.Body.Rotation = 0.0f;
            this.Body.Friction = 0.2f;
            life = 3;
            dead = false;
            
            Body.OnCollision += Body_OnCollision; // or use a lambda
        }

        bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact){       
            if (fixtureB.Body.Mass == 101){
                fixtureB.Body.ApplyForce(new Vector2(this.Body.LinearVelocity.X*1000, this.Body.LinearVelocity.Y*1000));
            } else if (fixtureB.Body.Mass == 0.0124407075f) {
                life--;
                if (life < 1){
                    fixtureA.Body.IsSensor = true;
                    fixtureA.Body.BodyType = BodyType.Dynamic;
                }
            } else if (fixtureB.Body.Mass == 12.7999992f) {
                fixtureA.Body.IsSensor = true;
            }
            
            return true;
        }

        public void takeHit() {
            life--;
            if (life < 1){
                dead = true;
            }
        }

        public bool isDead() {
            return dead;
        }

        public void die() {

        }
    }
}
