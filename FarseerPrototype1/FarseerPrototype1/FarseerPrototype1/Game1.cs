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
        DrawablePhysicsObject floor;
        KeyboardState prevKeyboardState;
        Random random;
        float elapseTime;
        int frameCounter;
        int FPS;
        Texture2D cursor;
        Character character;

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
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Vector2 size = new Vector2(50, 50);
            texture = this.Content.Load<Texture2D>("crate");
            
            random = new Random();

            cursor = this.Content.Load<Texture2D>("cursor");

            // Floor
            floor = new Floor(world, Content.Load<Texture2D>("floor"), new Vector2(GraphicsDevice.Viewport.Width, 100.0f), 10000.0f);
            floor.Position = new Vector2(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height);

            character = new Character(world, this.Content.Load<Texture2D>("char"), new Vector2(20f, 60f), 100);
            character.Position = new Vector2(50, 50);

            crateList = new List<DrawablePhysicsObject>();
            pointList = new List<Point>();

            prevKeyboardState = Keyboard.GetState();
        }

        protected override void UnloadContent() {
           
        }

        private void SpawnCrate() {
            Crate crate = new Crate(world, Content.Load<Texture2D>("crate"), new Vector2(50.0f, 50.0f), 300f);
            crate.Position = new Vector2(random.Next(50, GraphicsDevice.Viewport.Width - 50), 1); 
            
            crateList.Add(crate);
        }

        private void SpawnPoint(int x, int y) {
            Point point = new Point(world, Content.Load<Texture2D>("circle"), new Vector2(12.0f, 12.0f), 0.1f);
            point.Body.BodyType = BodyType.Static;
            point.Position = new Vector2(x,y);

            pointList.Add(point);
        }

        protected override void Update(GameTime gameTime) {

            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

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
            if (keyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space)) {
                character.jump();
            }
            if (keyboardState.IsKeyDown(Keys.Right)) {
                character.moveRight();
            }
            if (keyboardState.IsKeyDown(Keys.Left)) {
                character.moveLeft();
            }

            prevKeyboardState = keyboardState;

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

            floor.Draw(spriteBatch);

            spriteBatch.Draw(cursor, new Vector2(Mouse.GetState().X-cursor.Width/2, Mouse.GetState().Y-cursor.Height/2), Color.White);

            character.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
