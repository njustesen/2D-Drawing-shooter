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

        bool isJumping;
        bool jumpCheck;
        float ink;
        int score = 0;

        public int Score {
            get { return score; }
            set { score = value; }
        }

        public float Ink {
            get { return ink; }
            set { ink = value; }
        }

        public Character(World world, Texture2D texture, Vector2 size, float mass)
            : base(world, texture, size) {

            body = BodyFactory.CreateCapsule(world, (size.Y - (2 * size.X)) * CoordinateHelper.pixelToUnit, size.X * CoordinateHelper.pixelToUnit, mass);

            this.Body.BodyType = BodyType.Dynamic;
            this.Body.FixedRotation = true;
            this.Body.Rotation = 0.0f;
            this.Body.Mass = mass;
            this.isJumping = false;
            this.jumpCheck = false;
            this.ink = 50;
            Body.OnCollision += Body_OnCollision; // or use a lambda
        }

        bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact){
            if (fixtureB.Body.Mass == 80.5051956f) {
                score++;
            }
            return true;
        }

        public void jump() {
            jumpCheck = false;
            isJumping = true;
            this.Body.ApplyForce(new Vector2(0f,-29000f));
        }

        public void checkIfJumping() {
            if (Body.LinearVelocity.Y < 0.05 && Body.LinearVelocity.Y > -0.05){
                if (jumpCheck) {
                    isJumping = false;
                } else {
                    jumpCheck = true;
                }
            }
        }

        public bool getIsJumping() {
            return isJumping;
        }

        public void moveRight(float amount) {
            if (this.body.LinearVelocity.X < 3) {
                this.Body.ApplyForce(new Vector2(amount/2 * 1000f, 0f));
            }
        }

        public void moveLeft(float amount) {
            if (this.body.LinearVelocity.X > -3) {
                this.Body.ApplyForce(new Vector2(amount / 2 * 1000f, 0f));
            }
        }
    }
}
