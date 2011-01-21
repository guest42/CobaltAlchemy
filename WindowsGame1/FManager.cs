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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace CobaltAlchemy
{
    public class FManager : Microsoft.Xna.Framework.Game
    {
        public const int MODE_PLAY = 0;
        public const int MODE_INVENTORY = 1;
        public const int MODE_SCRIPTED = 2;
        public const int MODE_CUTSCENE = 3;
        public const int MODE_TITLE = 4;

        //XNA utilities
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldState;

        //Game Objects currently loaded in memory
        int current_mode = MODE_PLAY;
        LinkedList<FTexture2D> sprites;
        FMap current_map;
        FActor player_one;
        FInventory player_one_inventory;
        FCamera game_camera;
        //list of enemies
        //list of projectiles

        //temporary inv stuff
        FCamera inv_camera;
        FTile[,] inv_tiles;
        FInventory inventory;
        //list of drawable entities on screen, to be sorted before drawing

        public FManager()
        {
            graphics = new GraphicsDeviceManager(this);
            //this.graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            oldState = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            sprites = new LinkedList<FTexture2D>();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            game_camera = new FCamera(spriteBatch, 0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height/12*9, 16, 9);
            player_one = new FActor(new Vector3(1, 1, 0));
            player_one_inventory = new FInventory(player_one);


            createTestBits();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            UpdateInput();

            if (current_mode == MODE_PLAY)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                player_one.update(elapsed);

                CollisionTest();
                game_camera.setFocus(player_one.position.X, player_one.position.Y, current_map.tile_width, current_map.tile_height);
            }
            else if (current_mode == MODE_INVENTORY)
            {
            }
            else if (current_mode == MODE_SCRIPTED)
            {
            }
            else if (current_mode == MODE_CUTSCENE)
            {
            }
            else if (current_mode == MODE_TITLE)
            {
            }

            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Up))
            {

                if (!oldState.IsKeyDown(Keys.Up))
                {
                    
                }
            }

            if (newState.IsKeyDown(Keys.Down))
            {

                if (!oldState.IsKeyDown(Keys.Down))
                {
                }
            }

            if (newState.IsKeyDown(Keys.Left))
            {

                if (!oldState.IsKeyDown(Keys.Left))
                {
                    
                }
            }

            if (newState.IsKeyDown(Keys.Right))
            {

                if (!oldState.IsKeyDown(Keys.Right))
                {
                }
            }

            if (newState.IsKeyDown(Keys.Q))
            {
                
                if (!oldState.IsKeyDown(Keys.Q))
                {
                    player_one.hit_points -= 3;
                }
            }
            if (newState.IsKeyDown(Keys.E))
            {
                
                if (!oldState.IsKeyDown(Keys.E))
                {
                    player_one.hit_points += 3;
                }
            }
            if (newState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            oldState = newState;

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            game_camera.drawTiles(current_map.mapTiles, current_map.tile_width, current_map.tile_height, sprites);
            

            //all this will go int either a game_camera draw function or an inventory draw function with camera passed in
            inv_camera.drawTiles(inv_tiles, 16, 3, sprites);
            if (inventory.belt[0] != null)
            {
                inv_camera.drawSprite(sprites.ElementAt(inventory.belt[0].sprite_index), 10, 0);
            }
            if (inventory.belt[1] != null)
            {
                inv_camera.drawSprite(sprites.ElementAt(inventory.belt[1].sprite_index), 9, 1);
            }
            double health_ratio = (player_one.max_hit_points - player_one.hit_points) / player_one.max_hit_points;
            int cull_part = 30 + (int)(health_ratio*162);
            inv_camera.drawSprite(sprites.ElementAt(9), 1, 0);
            inv_camera.drawCulledSprite(sprites.ElementAt(11), 1, 0, 0, cull_part, 128, 192-cull_part);
            inv_camera.drawSprite(sprites.ElementAt(10), 1, 0);

            base.Draw(gameTime);
        }

        //should consolidate code
        public void CollisionTest()
        {
            /*int x = (int)Math.Floor(player_one.tile_x);
            int y = (int)Math.Floor(player_one.tile_y);
            FTile t;
            if (x == 0)
            {
                if (player_one.tile_x - player_one.radius < x)
                {
                    player_one.tile_x = x + player_one.radius;
                }
            }
            else
            {
                t = current_map.mapTiles[x - 1, y];
                if (t.isTraversable() == false)
                {
                    if (player_one.tile_x - player_one.radius < x)
                    {
                        player_one.tile_x = x + player_one.radius;
                    }
                }
            }

            if (x == current_map.tile_width - 1)
            {
                if (player_one.tile_x + player_one.radius > x + 1)
                {
                    player_one.tile_x = x + 1 - player_one.radius;
                }
            }
            else
            {
                t = current_map.mapTiles[x + 1, y];
                if (t.isTraversable() == false)
                {
                    if (player_one.tile_x + player_one.radius > x + 1)
                    {
                        player_one.tile_x = x + 1 - player_one.radius;
                    }
                }
            }

            if (y == current_map.tile_height - 1)
            {
                if (player_one.tile_y + player_one.radius > y + 1)
                {
                    player_one.tile_y = y + 1 - player_one.radius;
                }
            }
            else
            {
                t = current_map.mapTiles[x, y + 1];
                if (t.isTraversable() == false)
                {
                    if (player_one.tile_y + player_one.radius > y + 1)
                    {
                        player_one.tile_y = y + 1 - player_one.radius;
                    }
                }
            }

            if (y == 0)
            {
                if (player_one.tile_y - player_one.radius < y)
                {
                    player_one.tile_y = y + player_one.radius;
                }
            }
            else
            {
                t = current_map.mapTiles[x, y - 1];
                if (t.isTraversable() == false)
                {
                    if (player_one.tile_y - player_one.radius < y)
                    {
                        player_one.tile_y = y + player_one.radius;
                    }
                }
            }*/
        }

        public void createTestBits()
        {
            current_map = new FMap(20, 20);
            player_one.texture_index = 0;
            player_one.max_hit_points = 50;
            player_one.hit_points = 50;

            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("grass"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("darkwood"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("wood"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("dirt"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("invstone"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("ItemCase"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("Item_fireflask"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("Item_frostflask"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("frost_effect"), 64, 64, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("healthbottle_back"), 128, 192, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("healthbottle_front"), 128, 192, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("healthbottle_liquid"), 128, 192, 0, 0));
            sprites.AddLast(new FTexture2D(Content.Load<Texture2D>("tree"), 128, 192, 0, 0));

            inv_camera = new FCamera(spriteBatch, 0, graphics.GraphicsDevice.Viewport.Height / 12 * 9, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height / 12 * 3, 16, 3);
            inv_tiles = new FTile[16, 3];
            for (int x = 0; x < 16; x++)
            {
                for(int y = 0; y < 3; y++) 
                {
                    inv_tiles[x, y] = new FTile(2, false);
                }
            }
            inv_tiles[9, 1].setSpriteIndex(5);
            inv_tiles[11, 1].setSpriteIndex(5);
            inv_tiles[10, 0].setSpriteIndex(5);
            inv_tiles[10, 2].setSpriteIndex(5);

            inventory = new FInventory(player_one);
            inventory.bag[0] = new FItem(6);
            inventory.bag[1] = new FItem(7);
            inventory.bag_item_selected = 0;
            inventory.setSlot(0) ;
            inventory.bag_item_selected = 1;
            inventory.setSlot(1);
        }
    }
}
