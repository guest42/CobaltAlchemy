using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CobaltAlchemy
{
    class FCamera
    {
        SpriteBatch spriteBatch;
        public int pix_x, pix_y, pix_width, pix_height;
        public int tile_width, tile_height;
        public double tile_offset_x, tile_offset_y;
        double pix_per_tile_width, pix_per_tile_height;

        public FCamera(SpriteBatch _spriteBatch, int _pix_x, int _pix_y, int window_width, int window_height, int _tile_width, int _tile_height)
        {
            spriteBatch = _spriteBatch;
            pix_x = _pix_x;
            pix_y = _pix_y;
            pix_width = window_width;
            pix_height = window_height;
            tile_width = _tile_width;
            tile_height = _tile_height;
            pix_per_tile_width = (double)pix_width / (double)tile_width;
            pix_per_tile_height = (double)pix_height / (double)tile_height;
        }

        public void setFocus(double x, double y, int map_width, int map_height)
        {
            tile_offset_x = x - (float)tile_width / 2;
            tile_offset_y = y - (float)tile_height / 2;
            if (x < (float)tile_width / 2)
                tile_offset_x = 0;
            else if (x > map_width - (tile_width / 2))
                tile_offset_x = map_width - tile_width;
            if (y < (float)tile_height / 2)
                tile_offset_y = 0;
            else if (y > map_height - (tile_height / 2))
                tile_offset_y = map_height - tile_height;
        }

        public void drawSprite(FTexture2D sprite, double tile_x, double tile_y) {
            spriteBatch.Begin();
            Vector2 pos = new Vector2();
            pos.X = pix_x + (((float)tile_x * (float)pix_per_tile_width) - (float)tile_offset_x * (float)pix_per_tile_width) - sprite.offset_x * (float)pix_per_tile_width/64;
            pos.Y = pix_y + (((float)tile_y * (float)pix_per_tile_height) - (float)tile_offset_y * (float)pix_per_tile_height) - sprite.offset_y * (float)pix_per_tile_height / 64;
            Vector2 scale = new Vector2((float)pix_per_tile_width / 64, (float)pix_per_tile_height / 64);
            spriteBatch.Draw(sprite.texture, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public void drawCulledSprite(FTexture2D sprite, double tile_x, double tile_y, int cull_start_x, int cull_start_y, int cull_width, int cull_height)
        {
            spriteBatch.Begin();
            Vector2 pos = new Vector2();
            Rectangle culler = new Rectangle(cull_start_x, cull_start_y, cull_width, cull_height);
            pos.X = pix_x + (((float)tile_x * (float)pix_per_tile_width) - (float)tile_offset_x * (float)pix_per_tile_width) - (sprite.offset_x * (float)pix_per_tile_width / 64) + (culler.X * (float)pix_per_tile_width / 64);
            pos.Y = pix_y + (((float)tile_y * (float)pix_per_tile_height) - (float)tile_offset_y * (float)pix_per_tile_height) - (sprite.offset_y * (float)pix_per_tile_height / 64) + (culler.Y * (float)pix_per_tile_width / 64);
            Vector2 scale = new Vector2((float)pix_per_tile_width / 64, (float)pix_per_tile_height / 64);
            spriteBatch.Draw(sprite.texture, pos, culler, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


        public void drawTiles(FTile[,] tiles, int size_x, int size_y, LinkedList<FTexture2D> sprites)
        {
            int start_x, start_y, end_x, end_y;
            if (tile_offset_x < 0)
                start_x = 0;
            else if (tile_offset_x > size_x)
                return;
            else
                start_x = (int)Math.Floor(tile_offset_x);

            if (tile_offset_y < 0)
                start_y = 0;
            else if (tile_offset_y > size_y)
                return;
            else
                start_y = (int)Math.Floor(tile_offset_y);

            if (tile_offset_x + tile_width > size_x)
                end_x = size_x;
            else if (tile_offset_x + tile_width < 0)
                return;
            else
                end_x = (int)Math.Ceiling(tile_offset_x + tile_width);

            if (tile_offset_y + tile_height > size_y)
                end_y = size_y;
            else if (tile_offset_y + tile_height < 0)
                return;
            else
                end_y = (int)Math.Ceiling(tile_offset_y + tile_height);

            Vector2 pos = new Vector2(100, 100);
            Vector2 scale = new Vector2(1, 1);
            for (int x = start_x; x < end_x; x++)
            {
                for (int y = start_y; y < end_y; y++)
                {
                    spriteBatch.Begin();
                    pos.X = pix_x + (x * (float)pix_per_tile_width) - (float)tile_offset_x * (float)pix_per_tile_width;
                    pos.Y = pix_y + (y * (float)pix_per_tile_height) - (float)tile_offset_y * (float)pix_per_tile_height;
                    scale.X = (float)pix_per_tile_width / 64;
                    scale.Y = (float)pix_per_tile_height / 64;
                    spriteBatch.Draw(sprites.ElementAt(tiles[x, y].getSpriteIndex()).texture, pos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    spriteBatch.End();
                }
            }

        }
    }
}
