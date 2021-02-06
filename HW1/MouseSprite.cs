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
    public enum Direction
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3,
    }
    public class MouseSprite
    {
        private double directionTimer;

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
        public MouseSprite(Vector2 position)
        {
            this.Position = position;
            this.bounds = new BoundingRectangle(Position + new Vector2(0, 16), 32, 24);
        }


        public void Update(GameTime gameTime)
        {
            //Update direction timer
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //Switch directions every 2 seconds
            if (directionTimer > 1.0)
            {
                switch (Direction)
                {
                    case Direction.Up:
                        Direction = Direction.Down;
                        break;
                    case Direction.Down:
                        Direction = Direction.Right;
                        break;
                    case Direction.Right:
                        Direction = Direction.Left;
                        break;
                    case Direction.Left:
                        Direction = Direction.Up;
                        break;
                }
                directionTimer -= 2;
            }

            //Move the mouse in direction it is moving
            switch (Direction)
            {
                case Direction.Up:
                    Position += new Vector2(0, -1) * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Down:
                    Position += new Vector2(0, 1) * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Left:
                    Position += new Vector2(-1, 0) * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Right:
                    Position += new Vector2(1, 0) * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }
            this.bounds = new BoundingRectangle(Position + new Vector2(0, 16), 32, 24);
        }
        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("mouse");
        }

        /// <summary>
        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Collected)
                return;

            var source = new Rectangle(32, (int)Direction * 32, 32, 32);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }
    }
}
