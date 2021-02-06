using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace HW1
{
    public class HW1Game : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseSprite[] mice;
        private CatSprite cat;
        private SpriteFont spriteFont;
        private int miceLeft;
        private Texture2D catnip;

        TimeSpan timeSpan = TimeSpan.FromSeconds(20);
        private int state = 1;

        public HW1Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            System.Random rand = new System.Random();
            mice = new MouseSprite[]
            {
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100)))
            };
            miceLeft = mice.Length;
            cat = new CatSprite();
            timeSpan = TimeSpan.FromSeconds(new Random().Next(20,40));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (var mouse in mice) mouse.LoadContent(Content);
            cat.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("Hanalei");
            catnip = Content.Load<Texture2D>("Catnip");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            cat.Update(gameTime, state);

            foreach (var mouse in mice)
            {
                mouse.Update(gameTime);
                if (!mouse.Collected && mouse.Bounds.CollidesWith(cat.Bounds))
                {
                    mouse.Collected = true;
                    miceLeft--;
                }
            }
            

            if (timeSpan <= TimeSpan.Zero) { 
                state = 0;
                timeSpan = TimeSpan.Zero;
            }
            else if(miceLeft == 0)
            {
                state = 1;
            }
            else
            {
                timeSpan -= gameTime.ElapsedGameTime;
            }
                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach(var mouse in mice)
            {
                mouse.Draw(gameTime, _spriteBatch);
                /*
                var rect = new Rectangle((int)(mouse.Bounds.X ),
                                            (int)(mouse.Bounds.Y),
                                            (int)(mouse.Bounds.Width), (int)(mouse.Bounds.Height));
                    _spriteBatch.Draw(catnip, rect, Color.White);
                */
                
                
                    
            }
            /*
            var rectG = new Rectangle((int)(cat.Bounds.X),
                                        (int)(cat.Bounds.Y ),
                                        (int)( cat.Bounds.Width), (int)( cat.Bounds.Height));
            _spriteBatch.Draw(catnip, rectG, Color.White);
            */
            

            cat.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(spriteFont, $"Time Left: {timeSpan}", new Vector2(2, 16), Color.Gold);
            _spriteBatch.DrawString(spriteFont, $"Capture all the mice before time runs out!", new Vector2(2, 0), Color.Black, 0, new Vector2(2, 6), .5f, SpriteEffects.None, 0); 
            _spriteBatch.DrawString(spriteFont, $"Mice left: {miceLeft}", new Vector2(2,75), Color.Gold, 0, new Vector2(2, 6), .5f, SpriteEffects.None, 0);
            if(state == 0 && miceLeft > 0)
            {
                _spriteBatch.DrawString(spriteFont, $"You Lose", new Vector2(200, 200), Color.Gold);

            }
            else if(state == 1 && miceLeft == 0)
            {
                _spriteBatch.DrawString(spriteFont, $"You Win", new Vector2(200, 200), Color.Gold);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
