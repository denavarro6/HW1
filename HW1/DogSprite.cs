using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using HW1.Collisions;


namespace HW1
{
    /*public enum Direction
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3,
    }*/
    public class DogSprite
    {
        private double directionTimer;

        private double animationTimer;

        private short animationFrame = 0;

        private Texture2D texture;

        private BoundingRectangle bounds;

        /// <summary>
        /// direction of the mouse
        /// </summary>
        public Direction Direction;
        /// <summary>
        /// the position of the mouse
        /// </summary>
        public Vector2 Position;

        public bool Collected { get; set; } = false;
        /// <summary>
        /// bounding volume of sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;


        /// <summary>
        /// Creates a new mouse sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public DogSprite(Vector2 position)
        {
            this.Position = position;
            this.bounds = new BoundingRectangle(Position + new Vector2(0, 0), 80, 64);
        }


        public void Update(GameTime gameTime, int state)
        {
            if (state == 1)
            {
                //Update direction timer
                directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

                //Switch directions every 2 seconds
                if (directionTimer > 4.0)
                {
                    switch (Direction)
                    {
                        case Direction.Up:
                            Direction = Direction.Down;
                            break;
                        case Direction.Down:
                            Direction = Direction.Up;
                            break;
                    }
                    directionTimer -= 4;
                }

                //Move the mouse in direction it is moving
                switch (Direction)
                {
                    case Direction.Up:
                        Position += new Vector2(0, -1) * 25 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Direction.Down:
                        Position += new Vector2(0, 1) * 25 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;

                }
                this.bounds = new BoundingRectangle(Position + new Vector2(0, 8), 40, 16);
            }
        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Dog");
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (Collected)
                return;
            if (animationTimer > .3)
            {
                animationFrame++;
                if (animationFrame > 1) animationFrame = 0;
                animationTimer -= .3;
            }
            var source = new Rectangle(animationFrame * 96, 0, 80, 64);
            spriteBatch.Draw(texture, Position, source, Color.White, 0, new Vector2(0,0),.5f, SpriteEffects.None, 0);
        }
    }
}
