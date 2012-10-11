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
    class Star : DrawablePhysicsObject {

        Vector2 realPosition;

        public Vector2 RealPosition {
            get { return realPosition; }
            set { realPosition = value; }
        }

        public Star(World world, Texture2D texture, Vector2 size, float mass)
            : base(world, texture, size) {

            body = BodyFactory.CreateCircle(world, (int)(size.Y / 2) * CoordinateHelper.pixelToUnit, mass, null);

            this.Body.BodyType = BodyType.Static;
            this.Body.FixedRotation = true;
            this.Body.Rotation = 0.0f;
            this.Body.Mass = mass;

            Random rand = new Random();
            this.Position = new Vector2(rand.Next(900), rand.Next(600));
            realPosition = Position;
            Body.OnCollision += Body_OnCollision; // or use a lambda
        }

        bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact){
            fixtureB.Body.BodyType = BodyType.Dynamic;
            if (fixtureB.Body.Mass == 101){
                Random rand = new Random();
                this.Position = new Vector2(rand.Next(900), rand.Next(700));
                realPosition = Position;
            }
            return true;
        }
    }
}
