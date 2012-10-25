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
using WiimoteLib;

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
        Vector2 cursorAPosition; //<--wiimote IR-sensor
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
        SpriteFont UVfont;
        //WIIII!!!
        //wiimote1
        Wiimote myWiimote1;
        bool wii1A = false;
        bool wii1B = false;
        bool nunchuck1Z;
        //wiimote2
        Wiimote myWiimote2;
        bool wii2A = false;
        bool wii2B = false;
        bool nunchuck2Z;
        
        int screenWidth;
        int screenHeight;

        WiimoteCollection wiimotes;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            screenWidth = graphics.PreferredBackBufferWidth = displayMode.Width;
            screenHeight = graphics.PreferredBackBufferHeight = displayMode.Height;
            graphics.IsFullScreen = true;
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
            UVfont = Content.Load<SpriteFont>("SpriteFont1");

            Vector2 size = new Vector2(50, 50);
            texture = this.Content.Load<Texture2D>("crate");
            bar = this.Content.Load<Texture2D>("bar");
            
            random = new Random();

            circle = Content.Load<Texture2D>("circle");

            cursorA = this.Content.Load<Texture2D>("crosshair");
            cursorB = this.Content.Load<Texture2D>("crosshair");

            // Floor
            floor = new Floor(world, Content.Load<Texture2D>("floor"), new Vector2(GraphicsDevice.Viewport.Width, 100.0f), 10000010.0f);
            floor.Position = new Vector2(GraphicsDevice.Viewport.Width / 2.0f, GraphicsDevice.Viewport.Height);

            leftWall = new Wall(world, Content.Load<Texture2D>("wall"), new Vector2(100.0f, GraphicsDevice.Viewport.Height), 10000001.0f);
            leftWall.Position = new Vector2(0.0f, GraphicsDevice.Viewport.Height / 2);

            rightWall = new Wall(world, Content.Load<Texture2D>("wall"), new Vector2(100.0f, GraphicsDevice.Viewport.Height), 10000001.0f);
            rightWall.Position = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height / 2);

            characterA = new Character(world, this.Content.Load<Texture2D>("charRed"), new Vector2(20f, 60f), 101f);
            characterA.Position = new Vector2(150, 50);

            characterB = new Character(world, this.Content.Load<Texture2D>("charBlue"), new Vector2(20f, 60f), 101f);
            characterB.Position = new Vector2(GraphicsDevice.Viewport.Width - 150, 50);

            crateList = new List<DrawablePhysicsObject>();
            pointList = new List<Point>();
            bullets = new List<Bullet>();
            star = new Star(world, this.Content.Load<Texture2D>("star"), new Vector2(32f, 32f), 1001f);

            prevKeyboardState = Keyboard.GetState();

           

            wiimotes = new WiimoteCollection();
            wiimotes.FindAllWiimotes();

            foreach(Wiimote wm in wiimotes){
                wm.Connect();
                wm.SetReportType(InputReport.IRExtensionAccel, true);
            }

            myWiimote1 = wiimotes[0];
            myWiimote2 = wiimotes[1];
      
        }

        protected override void UnloadContent() {
           
        }

        //WIIIIIII!!!!-update
        public void wiimoteUpdate()
        {
            //Check for player1
            if (myWiimote1.WiimoteState.ButtonState.A)
                wii1A = true;
            else wii1A = false;

            if (myWiimote1.WiimoteState.ButtonState.B)
                wii1B = true;
            else wii1B = false;

            if (myWiimote1.WiimoteState.NunchukState.Z)
                nunchuck1Z = true;
            else nunchuck1Z = false;

            characterA.moveRight(MathHelper.Clamp(myWiimote1.WiimoteState.NunchukState.RawJoystick.X - 128, -100, 100)/50);
            characterA.moveLeft(MathHelper.Clamp(myWiimote1.WiimoteState.NunchukState.RawJoystick.X - 128, -100, 100) /50);

            cursorAPosition.X = -((int)computeIntervals(myWiimote1.WiimoteState.IRState.RawMidpoint.X, 0, 1023, 0, screenWidth)) + screenWidth;
            cursorAPosition.Y = ((int)computeIntervals(myWiimote1.WiimoteState.IRState.RawMidpoint.Y, 0, 767, 0, screenHeight));

            //Check for player 2
            if (myWiimote2.WiimoteState.ButtonState.A)
                wii2A = true;
            else wii2A = false;

            if (myWiimote2.WiimoteState.ButtonState.B)
                wii2B = true;
            else wii2B = false;

            if (myWiimote2.WiimoteState.NunchukState.Z)
                nunchuck2Z = true;
            else nunchuck2Z = false;

            characterB.moveRight(MathHelper.Clamp(myWiimote2.WiimoteState.NunchukState.RawJoystick.X - 128, -100, 100) / 50);
            characterB.moveLeft(MathHelper.Clamp(myWiimote2.WiimoteState.NunchukState.RawJoystick.X - 128, -100, 100) / 50);
      
            cursorBPosition.X = -((int)computeIntervals(myWiimote2.WiimoteState.IRState.RawMidpoint.X, 0, 1023, 0, screenWidth)) + screenWidth;
            cursorBPosition.Y = ((int)computeIntervals(myWiimote2.WiimoteState.IRState.RawMidpoint.Y, 0, 767, 0, screenHeight));
        }

        //WIIII!!!!!!
        public double computeIntervals(double numberToCompute, double interval1Start, double interval1End, double interval2Start, double interval2End)
        {

            double l1 = interval1End - interval1Start;
            double l2 = interval2End - interval2Start;
            double offset = Math.Min(interval1Start, interval2Start);
            double dif = Math.Max(interval1Start, interval2Start) - Math.Min(interval1Start, interval2Start);
            double scale = l2 / l1;

            //ups.. gamle java printouts.. Lader dem blive for hyggens skyld.
            /*	System.out.println("INTERVAL 1 goes from "+interval1Start+" to "+interval1End);
                System.out.println("INTERVAL 2 goes from "+interval2Start+" to "+interval2End);
                System.out.println("numberToCompute = "+numberToCompute);
                System.out.println("offset = "+offset);
                System.out.println("dif = "+dif);
                System.out.println("scale = "+scale);
                System.out.println("number computed.. from "+numberToCompute+" to "+(((numberToCompute-dif) * scale) + dif + offset));
                System.out.println(((numberToCompute-dif) * scale) + dif + offset);
            */
            return ((numberToCompute - dif) * scale) + dif + offset;
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
                characterA.Ink += 0.03f * 5;
            }
            if (characterB.Ink < maxInk) {
                characterB.Ink += 0.03f * 5;
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

            if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed) {
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
            float xMove1 = padState1.ThumbSticks.Right.X * 10;
            float yMove1 = padState1.ThumbSticks.Right.Y * 10 * -1;
            float xMove2 = padState2.ThumbSticks.Right.X * 10;
            float yMove2 = padState2.ThumbSticks.Right.Y * 10 * -1;

            if ((xMove1 < 0 && cursorAPosition.X > 50) || (xMove1 > 0 && cursorAPosition.X < GraphicsDevice.Viewport.Width - 50)){
                cursorAPosition.X += xMove1;
            }

            if ((yMove1 < 0 && cursorAPosition.Y > 0) || (yMove1 > 0 && cursorAPosition.Y < GraphicsDevice.Viewport.Height - 50)){
                cursorAPosition.Y += yMove1;
            }

            if ((xMove2 < 0 && cursorBPosition.X > 50) || (xMove2 > 0 && cursorBPosition.X < GraphicsDevice.Viewport.Width - 50)) {
                cursorBPosition.X += xMove2;
            }

            if ((yMove2 < 0 && cursorBPosition.Y > 0) || (yMove2 > 0 && cursorBPosition.Y < GraphicsDevice.Viewport.Height - 50)) {
                cursorBPosition.Y += yMove2;
            }

      //      if (padState1.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !characterA.getIsJumping()) {
      //          characterA.jump();
      //      }
            
            //nu med WIIII!!!!-support PLAYER1 jump
            if (nunchuck1Z && !characterA.getIsJumping())
            {
                characterA.jump();
            }

            if (padState1.ThumbSticks.Left.X > 0) {
                characterA.moveRight(padState1.ThumbSticks.Left.X * 2);
                characterA.Body.Friction = Math.Max(0.2f, 0.1f);
            } else if (padState1.ThumbSticks.Left.X < 0) {
                characterA.moveLeft(padState1.ThumbSticks.Left.X * 2);
                characterA.Body.Friction = Math.Max(0.2f, 0.1f);
            } else {
                characterA.Body.Friction = Math.Max(0.2f, 10);
            }
      /*      if (padState1.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed && characterA.Ink > 0)
            {
                SpawnPoint((int)cursorAPosition.X, (int)cursorAPosition.Y);
                characterA.Ink -= 1f;
            }
      */    //nu med WIIIII!!!!-support PLAYER1 draw
            if (wii1A && characterA.Ink > 0)
            {
                SpawnPoint((int)cursorAPosition.X, (int)cursorAPosition.Y);
                characterA.Ink -= 1f;
            }
      /*    if (padState1.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed && characterA.Ink > 0)
            {
                if (timeSinceLastShotA > shootCooldown) {
                    spawnBulletA();
                    timeSinceLastShotA = 0;
                    characterA.Ink -= 3f;
                }
            }
*/          //nu med WIIIIII!!!!!-support PLAYER1 shoot
            if (wii1B && characterA.Ink > 0)
            {
                if (timeSinceLastShotA > shootCooldown)
                {
                    spawnBulletA();
                    timeSinceLastShotA = 0;
                    characterA.Ink -= 3f;
                }
            }

    /*        if (padState2.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !characterB.getIsJumping())
            {
                characterB.jump();
            }
  */
            //nu med WIIII!!!!-support PLAYER2 jump
            if (nunchuck2Z && !characterB.getIsJumping())
            {
                characterB.jump();
            }

            if (padState2.ThumbSticks.Left.X > 0) {
                characterB.moveRight(padState2.ThumbSticks.Left.X * 2);
                characterB.Body.Friction = Math.Max(0.2f, 0.1f);
            } else if (padState2.ThumbSticks.Left.X < 0) {
                characterB.moveLeft(padState2.ThumbSticks.Left.X * 2);
                characterB.Body.Friction = Math.Max(0.2f, 0.1f);
            } else {
                characterB.Body.Friction = Math.Max(0.2f, 10);
            }
            
   /*         if (padState2.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed && characterB.Ink > 0)
            {
                SpawnPoint((int)cursorBPosition.X, (int)cursorBPosition.Y);
                characterB.Ink -= 1f;
            }
     */     
            //nu med WIIIIIII!!!!-support PLAYER2 draw
            if (padState2.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed && characterB.Ink > 0)
            {
                SpawnPoint((int)cursorBPosition.X, (int)cursorBPosition.Y);
                characterB.Ink -= 1f;
            }

     /*       if (padState2.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed && characterB.Ink > 0)
            {
                if (timeSinceLastShotB > shootCooldown) {
                    spawnBulletB();
                    timeSinceLastShotB = 0;
                    characterB.Ink -= 3f;
                }
            }
*/
            //nu med WIIIIII!!!!!-support PLAYER 2 shoot
            if (padState2.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed && characterB.Ink > 0)
            {
                if (timeSinceLastShotB > shootCooldown)
                {
                    spawnBulletB();
                    timeSinceLastShotB = 0;
                    characterB.Ink -= 3f;
                }
            }

            //WIIIII!!!!
            wiimoteUpdate();

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

            spriteBatch.DrawString(UVfont, "Score: " + characterA.Score, new Vector2(150, 10), Color.Red);

            spriteBatch.DrawString(UVfont, "Score: " + characterB.Score, new Vector2(GraphicsDevice.Viewport.Width-350, 10), Color.Blue);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
