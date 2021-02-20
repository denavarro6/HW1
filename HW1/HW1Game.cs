using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace HW1
{
    public class HW1Game : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseSprite[] mice;
        private DogSprite[] dogs;
        private CatnipSprite[] catnips;
        private CatSprite cat;
        private SpriteFont spriteFont;
        private SpriteFont spriteFont2;
        private int miceLeft;
        private int catnipCaptured = 0;
        //private Texture2D catnip;
        private Texture2D mouse;
        private SoundEffect pickup;
        private SoundEffect jump;
        private SoundEffect hurt;
        private Song backgroundMusic;

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
           /* mice = new MouseSprite[]
            {
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new MouseSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100)))
            };*/
            catnips = new CatnipSprite[]
           {
                new CatnipSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new CatnipSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new CatnipSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new CatnipSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new CatnipSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new CatnipSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new CatnipSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100)))
           };
            dogs = new DogSprite[]
            {
                new DogSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new DogSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
                new DogSprite(new Vector2((float)rand.NextDouble() * (GraphicsDevice.Viewport.Width - 100), (float)rand.NextDouble() * (GraphicsDevice.Viewport.Height - 100))),
            };
            //miceLeft = mice.Length;
            cat = new CatSprite(this);
            timeSpan = TimeSpan.FromSeconds(new Random().Next(20,40));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (var catnip in catnips) catnip.LoadContent(Content);
            cat.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("Hanalei");
            spriteFont2 = Content.Load<SpriteFont>("Yusei");
            //catnip = Content.Load<Texture2D>("Catnip");
            mouse = Content.Load<Texture2D>("mouse");
            foreach (var dog in dogs) dog.LoadContent(Content);
            pickup = Content.Load<SoundEffect>("Collect");
            hurt = Content.Load<SoundEffect>("Hit_Hurt");
            backgroundMusic = Content.Load<Song>("Happy walk");
            MediaPlayer.Play(backgroundMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            cat.Update(gameTime, state);
            if (state == 1)
            {
                foreach (var catnip in catnips)
                {
                    catnip.Update(gameTime);
                    if (!catnip.Collected && catnip.Bounds.CollidesWith(cat.Bounds))
                    {
                        catnip.Collected = true;
                        catnipCaptured++;
                        pickup.Play();
                    }
                }
                foreach (var dog in dogs)
                {
                    dog.Update(gameTime, state);
                    if (dog.Bounds.CollidesWith(cat.Bounds))
                    {
                        //dog.Collected = true;
                        state = 0;
                        //catnipCaptured++;
                        hurt.Play();
                    }
                }
            }


            if (timeSpan <= TimeSpan.Zero) { 
                state = 0;
                timeSpan = TimeSpan.Zero;
            }
            else if(catnipCaptured == 7)
            {
                state = 2;
            }
            else
            {
                timeSpan -= gameTime.ElapsedGameTime;
            }
                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach(var catnip in catnips)
            {
                catnip.Draw(gameTime, _spriteBatch);
                /*
                var rect = new Rectangle((int)(catnip.Bounds.X ),
                                            (int)(catnip.Bounds.Y),
                                            (int)(catnip.Bounds.Width), (int)(catnip.Bounds.Height));
                    _spriteBatch.Draw(mouse, rect, Color.White);*/
                
                
                
                    
            }
            foreach(var dog in dogs)
            {
                dog.Draw(gameTime, _spriteBatch);
                /*
                var rect = new Rectangle((int)(dog.Bounds.X ),
                                            (int)(dog.Bounds.Y),
                                            (int)(dog.Bounds.Width), (int)(dog.Bounds.Height));
                    _spriteBatch.Draw(mouse, rect, Color.White);*/
            }
            /*
            var rectG = new Rectangle((int)(cat.Bounds.X),
                                        (int)(cat.Bounds.Y ),
                                        (int)( cat.Bounds.Width), (int)( cat.Bounds.Height));
            _spriteBatch.Draw(catnip, rectG, Color.White);
            */



            cat.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(spriteFont2, $"Time Left: {timeSpan}", new Vector2(2, 32), Color.Black, 0, new Vector2(2, 6), .6f, SpriteEffects.None, 0); ;
            _spriteBatch.DrawString(spriteFont2, $"Capture as much catnip as possible before time runs out!", new Vector2(2, 0), Color.Black, 0, new Vector2(2, 6), .5f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(spriteFont2, $"Hold space to jump and avoid the dogs!", new Vector2(200, 450), Color.Black, 0, new Vector2(2, 6), .3f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(spriteFont2, $"Catnip secured: {catnipCaptured}", new Vector2(2,65), Color.Black, 0, new Vector2(2, 6), .5f, SpriteEffects.None, 0);
            if(state == 0)
            {
                _spriteBatch.DrawString(spriteFont, $"You Lose", new Vector2(200, 200), Color.Gold);
                timeSpan = TimeSpan.Zero;
                MediaPlayer.Pause();
            }
            else if(state == 2)
            {
                _spriteBatch.DrawString(spriteFont, $"You Win", new Vector2(200, 200), Color.Gold, 0 ,new Vector2(0,2) , 1f, SpriteEffects.None, 0);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
