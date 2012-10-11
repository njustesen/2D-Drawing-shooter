using System;
using System.Collections.Generic;
using System.Linq;
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

    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        World world;
        Vector2 worldSize = new Vector2(1000, 800);
        List<DrawablePhysicsObject> crateList;
        List<Point> pointList;
        List<Bullet> bullets;
        DrawablePhysicsObject floor;
        DrawablePhysicsObject leftWall;
        DrawablePhysicsObject rightWall;
        KeyboardState prevKeyboardState;
        Random random;
        float elapseTime;
        int frameCounter;
        int FPS;
        Texture2D cursorA;
        Vector2 cursorAPosition;
        Texture2D cursorB;
        Vector2 cursorBPosition;
        Character characterA;
        Character characterB;
        int timeSinceLastShotA;
        int timeSinceLastShotB;
        float shootCooldown = 100;
        Star star;
        Texture2D circle;
        Texture2D bar;
        float maxInk = 100f;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            graphics.PreferredBackBufferWidth = displayMode.Width;
            graphics.PreferredBackBufferHeight = displayMode.Height;
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            world = new World(new Vector2(0, 9.8f));
            worldSize = new Vector2(1000, 800);
        }

        protected override void Initialize() {
            base.Initialize();
            cursorAPosition = new Vector2(200,200);
            cursorBPosition = new Vector2(GraphicsDevice.Viewport.Width- 200, 200);
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Vector2 size = new Vector2(50, 50);
            texture = this.Content.Load<Texture2D>("crate");
            bar = this.Content.Load<Texture2D>("bar");
            
            random = new Random();

            circle = Content.Load<Texture2D>("circle");

            cursorA = this.Content.Load<Texture2D>("cursor");
            cursorB = this.Content.Load<Texture2D>("cursor");

            // Floor
            floor = new Floor(world, Content.Load<Texture2D>("floor"), new Vector2(GraphicsDevice.Viewport.Width, 100.0f), 10000010.0f);
            floor.Position = new Vector2(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height);

            leftWall = new Wall(world, Content.Load<Texture2D>("wall"), new Vector2(100.0f, GraphicsDevice.Viewport.Height), 10000001.0f);
            leftWall.Position = new Vector2(0.0f, GraphicsDevice.Viewport.Height / 2);

            rightWall = new Wall(world, Content.Load<Texture2D>("wall"), new Vector2(100.0f, GraphicsDevice.Viewport.Height), 10000001.0f);
            rightWall.Position = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height / 2);

            characterA = new Character(world, this.Content.Load<Texture2D>("char"), new Vector2(20f, 60f), 101f);
            characterA.Position = new Vector2(150, 50);

            characterB = new Character(world, this.Content.Load<Texture2D>("char"), new Vector2(20f, 60f), 101f);
            characterB.Position = new Vector2(GraphicsDevice.Viewport.Width - 150, 50);

            crateList = new List<DrawablePhysicsObject>();
            pointList = new List<Point>();
            bullets = new List<Bullet>();
            star = new Star(world, this.Content.Load<Texture2D>("star"), new Vector2(32f, 32f), 1000);

            prevKeyboardState = Keyboard.GetState();
        }

        protected override void UnloadContent() {
           
        }

        private void SpawnCrate() {
            Crate crate = new Crate(world, Content.Load<Texture2D>("crate"), new Vector2(50.0f, 50.0f), 300f);
            crate.Position = new Vector2(random.Next(50, GraphicsDevice.Viewport.Width - 50), 1); 
            
            crateList.Add(crate);
        }

        float minDistance = 50;

        private void SpawnPoint(int x, int y) {
            if (distance(characterA.Position.X, characterA.Position.Y, x, y) >= minDistance) {
                if (distance(characterB.Position.X, characterB.Position.Y, x, y) >= minDistance) {
                    if (x > 50 && x < GraphicsDevice.Viewport.Width - 50) {
                        if (y > 0 && y < GraphicsDevice.Viewport.Height - 50) {
                            Point point = new Point(world, circle, new Vector2(12.0f, 12.0f), 1.2f);
                            //point.Body.Mass = 1.2f;
                            point.Body.BodyType = BodyType.Static;
                            point.Position = new Vector2(x, y);

                            pointList.Add(point);
                        }
                    }
                }
            }
        }

        private void spawnBulletA() { 
            // Ignore if to close to character
            Vector2 angle = new Vector2(cursorAPosition.X - characterA.Position.X, cursorAPosition.Y - characterA.Position.Y);

            // Convert to unit length
            float x = (float)(angle.X / Math.Sqrt((double)((angle.X * angle.X) + (angle.Y * angle.Y))));
            float y = (float)(angle.Y / Math.Sqrt((double)((angle.X * angle.X) + (angle.Y * angle.Y))));
            angle = new Vector2(x,y);

            angle *= 100;

            Bullet bullet = new Bullet(world, circle, new Vector2(12f, 12f), 1.1f, characterA.Position + angle / 2, angle);
            bullet.Body.Friction = 0.5f;
            bullets.Add(bullet);
        }

        private void spawnBulletB() {
            Vector2 angle = new Vector2(cursorBPosition.X - characterB.Position.X, cursorBPosition.Y - characterB.Position.Y);

            // Convert to unit length
            float x = (float)(angle.X / Math.Sqrt((double)((angle.X * angle.X) + (angle.Y * angle.Y))));
            float y = (float)(angle.Y / Math.Sqrt((double)((angle.X * angle.X) + (angle.Y * angle.Y))));
            angle = new Vector2(x, y);
            angle *= 100;

            Bullet bullet = new Bullet(world, circle, new Vector2(12f, 12f), 1.1f, characterB.Position + angle / 2, angle);
            bullet.Body.Friction = 0.5f;
            bullets.Add(bullet);
        }

        private double distance(float x1, float y1, float x2, float y2) {
            double dx = x1 - x2;         //horizontal difference 
            double dy = y1 - y2;         //vertical difference 
            double dist = Math.Sqrt(dx * dx + dy * dy); //distance using Pythagoras theorem
            return dist;
        }

        protected override void Update(GameTime gameTime) {

            if (bullets.Count > 100){
                int i = 1;
                i++;
            }

            if (pointList.Count > 100) {
                int i = 1;
                i++;
            }

            // Remove points
            List<Point> pointsToBeRemoved = new List<Point>();
            foreach(Point p in pointList){
                if (p.Position.Y > GraphicsDevice.Viewport.Height) {
                    pointsToBeRemoved.Add(p);
                }
            }
            foreach (Point p in pointsToBeRemoved) {
                pointList.Remove(p);
                world.RemoveBody(p.Body);
            }

            List<Bullet> bulletsToBeRemoved = new List<Bullet>();
            foreach (Bullet b in bullets) {
                if (b.Position.Y > GraphicsDevice.Viewport.Height) {
                    bulletsToBeRemoved.Add(b);
                }
            }
            foreach (Bullet b in bulletsToBeRemoved) {
                bullets.Remove(b);
                world.RemoveBody(b.Body);
            }

            if (characterA.Ink < maxInk) {
                characterA.Ink += 0.03f * 100;
            }
            if (characterB.Ink < maxInk) {
                characterB.Ink += 0.03f * 100;
            }
 
            // Update timeSinceLastShot
            timeSinceLastShotA += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceLastShotB += gameTime.ElapsedGameTime.Milliseconds;

            // Check if characters are jumping
            characterA.checkIfJumping();
            characterB.checkIfJumping();

            float time = Math.Min(100,((float)gameTime.ElapsedGameTime.TotalSeconds));

            world.Step(time);

            // FPS
            elapseTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter++;

            if (elapseTime > 1) {
                FPS = frameCounter;
                frameCounter = 0;
                elapseTime = 0;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;
                SpawnPoint(x, y);
            }

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && !prevKeyboardState.IsKeyDown(Keys.Enter)) {
                SpawnCrate();
            }
            if (keyboardState.IsKeyDown(Keys.Escape) && !prevKeyboardState.IsKeyDown(Keys.Escape)) {
                Exit();
            }

            GamePadState padState1 = GamePad.GetState(PlayerIndex.One);
            GamePadState padState2 = GamePad.GetState(PlayerIndex.Two);

            // Update cursor position
            cursorAPosition.X += padState1.ThumbSticks.Right.X*10;
            cursorAPosition.Y -= padState1.ThumbSticks.Right.Y*10;

            cursorBPosition.X += padState2.ThumbSticks.Right.X*10;
            cursorBPosition.Y -= padState2.ThumbSticks.Right.Y*10;

            if (padState1.Buttons.A == ButtonState.Pressed && !characterA.getIsJumping()) {
                characterA.jump();
            }
            if (padState1.ThumbSticks.Left.X > 0) {
                characterA.moveRight(padState1.ThumbSticks.Left.X);
            }
            if (padState1.ThumbSticks.Left.X < 0) {
                characterA.moveLeft(padState1.ThumbSticks.Left.X);
            }
            if (padState1.Buttons.RightShoulder == ButtonState.Pressed && characterA.Ink > 0) {
                SpawnPoint((int)cursorAPosition.X, (int)cursorAPosition.Y);
                characterA.Ink -= 3f;
            }
            if (padState1.Triggers.Right > 0.9f && characterA.Ink > 0) {
                if (timeSinceLastShotA > shootCooldown) {
                    spawnBulletA();
                    timeSinceLastShotA = 0;
                    characterA.Ink -= 3f;
                }
            }
            characterA.Body.Friction = Math.Max(0.2f,padState1.Triggers.Left*10);

            if (padState2.Buttons.A == ButtonState.Pressed && !characterB.getIsJumping()) {
                characterB.jump();
            }
            if (padState2.ThumbSticks.Left.X > 0) {
                characterB.moveRight(padState2.ThumbSticks.Left.X);
            }
            if (padState2.ThumbSticks.Left.X < 0) {
                characterB.moveLeft(padState2.ThumbSticks.Left.X);
            }
            if (padState2.Buttons.RightShoulder == ButtonState.Pressed && characterB.Ink > 0) {
                SpawnPoint((int)cursorBPosition.X, (int)cursorBPosition.Y);
                characterB.Ink -= 1f;
            }
            if (padState2.Triggers.Right > 0.9f && characterB.Ink > 0) {
                if (timeSinceLastShotB > shootCooldown) {
                    spawnBulletB();
                    timeSinceLastShotB = 0;
                    characterB.Ink -= 1f;
                }
            }
            characterB.Body.Friction = Math.Max(0.2f, padState2.Triggers.Left * 10);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

            foreach (DrawablePhysicsObject crate in crateList) {
                crate.Draw(spriteBatch);
            }

            foreach (Point point in pointList) {
                point.Draw(spriteBatch);
            }

            foreach (Bullet bullet in bullets) {
                bullet.Draw(spriteBatch);
            }

            floor.Draw(spriteBatch);
            leftWall.Draw(spriteBatch);
            rightWall.Draw(spriteBatch);

            star.Draw(spriteBatch);

            //bars
            spriteBatch.Draw(bar, new Rectangle(150,50,(int)characterA.Ink*2, 8), Color.White);
            int x = GraphicsDevice.Viewport.Width - 150 - 200;
            spriteBatch.Draw(bar, new Rectangle(x, 50, (int)characterB.Ink*2, 8), Color.White);

            spriteBatch.Draw(cursorA, new Vector2(cursorAPosition.X - cursorA.Width/2, cursorAPosition.Y - cursorA.Height/2), Color.White);
            spriteBatch.Draw(cursorB, new Vector2(cursorBPosition.X - cursorB.Width/2, cursorBPosition.Y - cursorB.Height/2), Color.White);

            characterA.Draw(spriteBatch);
            characterB.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
