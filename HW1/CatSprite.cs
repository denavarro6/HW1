using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using HW1.Collisions;
namespace HW1
{
    public class CatSprite
    {
        private GamePadState gamePadState;

        Game game;

        Viewport viewport;

        private double animationTimer;

        private short animationFrame = 0;

        private KeyboardState keyboardState;

        private SoundEffect jump;

        private Texture2D texture;

        private Vector2 position = new Vector2(0, 0);

        Vector2 velocity;

        private bool flipped;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(200, 200 ), 16, 16);

        public BoundingRectangle Bounds => bounds;
        /// <summary>
        /// color overlay of ghost
        /// </summary>
        public Color Color { get; set; } = Color.White;

        public CatSprite(Game game)
        {
            this.game = game;
            viewport = game.GraphicsDevice.Viewport;
            this.position = new Vector2(100, viewport.Height);
        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("cat");
            //jump = content.Load<SoundEffect>("jump");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime, int state)
        {
            keyboardState = Keyboard.GetState();

            if (state == 1) {
                // Apply keyboard movement
                /*if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    if (animationTimer > .3)
                    {
                        animationFrame++;
                        if (animationFrame > 7) animationFrame = 0;
                        animationTimer -= .3;
                    }
                    position += new Vector2(0, -1);
                }*/
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    if (animationTimer > .3)
                    {
                        animationFrame++;
                        if (animationFrame > 7) animationFrame = 0;
                        animationTimer -= .3;
                    }
                    position += new Vector2(0, 1);
                }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                    if (animationTimer > .3)
                    {
                        animationFrame++;
                        if (animationFrame > 7) animationFrame = 0;
                        animationTimer -= .3;
                    }
                    position += new Vector2(-1, 0);
                flipped = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                    if (animationTimer > .3)
                    {
                        animationFrame++;
                        if (animationFrame > 7) animationFrame = 0;
                        animationTimer -= .3;
                    }
                    position += new Vector2(1, 0);
                flipped = false;
            }
                float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 acceleration = new Vector2(0, 2);
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    acceleration = new Vector2(0, -7);
                    //jump.Play(.25f,0,0);
                }
                position += acceleration;
                if (position.Y < 32) position.Y = 32;
                if (position.Y > viewport.Height) position.Y = viewport.Height;
                if (position.X < 32) position.X = 32;
                if (position.X > viewport.Width) position.X = viewport.Width;
            
            //update the bounds
            bounds.X = position.X - 48;
            bounds.Y = position.Y - 48;
            }
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Update animation frme
            
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            var source = new Rectangle(animationFrame * 50, 0, 48, 48);
            spriteBatch.Draw(texture, position, source, Color, 0, new Vector2(64, 64), 1, spriteEffects, 0);
        }
    }
}
