using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CobaltAlchemy
{
    class FTile
    {
        public byte sprite_index, data_one, data_two;

        public FTile(int _sprite_index, bool _isTraversable)
        {
            setTraversable(_isTraversable);
            setSpriteIndex(_sprite_index);
        }

        public void setSpriteIndex(int _sprite_index)
        {
            sprite_index = unsignedIntToByte(_sprite_index);
        }

        public void setTraversable(bool _isTraversable)
        {
            byte n = (byte)(Convert.ToByte(_isTraversable));
            byte l = (byte)(data_one << 4);
            data_one = (byte)((l >> 4) | (n << 4));
        }

        public void setEffectOne(int effectIndex)
        {
            byte n = unsignedIntToByte(effectIndex);
            byte x = (byte)(data_one >> 4);
            data_one = (byte)((x << 4) | n);
        }

        public void setEffectTwo(int effectIndex)
        {
            byte n = unsignedIntToByte(effectIndex);
            byte x = (byte)(data_two >> 4);
            data_two = (byte)((x << 4) | n);
        }

        public void setEffectThree(int effectIndex)
        {
            byte n = unsignedIntToByte(effectIndex);
            byte x = (byte)(data_two << 4);
            data_two = (byte)((x >> 4) | (n << 4));
        }

        public int getSpriteIndex()
        {
            return sprite_index;
        }

        public bool isTraversable()
        {
            return Convert.ToBoolean(((data_one >> 4) & 1));
        }

        public int getEffectOne()
        {
            return (int)data_one & 15;
        }

        public int getEffectTwo()
        {
            return (int)data_two & 15;
        }

        public int getEffectThree()
        {
            return (int)(data_two >> 4);
        }

        public byte unsignedIntToByte(int n)
        {
            if (n < 0)
            {
                n = 0;
            }
            else if (n > 255)
            {
                n = 255;
            }
            return (byte)n;
        }
    }
    class FTileEffect
    {
        bool damaging, speed_effect, nontraversable;
        int damage_per_second;
        float speed_multiplier;

        public FTileEffect()
        {
            damaging = false;
            speed_effect = false;
            damage_per_second = 0;
            speed_multiplier = 0.0f;
        }
    }

    class FMap
    {
        public int tile_width, tile_height;
        public FTile[,] mapTiles;
        LinkedList<Texture2D> tileTextures;
        LinkedList<FTileEffect> tileEffects;

        public FMap(int _tile_width, int _tile_height)
        {
            tile_width = _tile_width;
            tile_height = _tile_height;
            mapTiles = new FTile[tile_width, tile_height];
            for (int x = 0; x < tile_width; x++)
            {
                for (int y = 0; y < tile_height; y++)
                {
                    mapTiles[x, y] = new FTile(0, true);
                }

            }

            mapTiles[10, 10].setTraversable(false);
            mapTiles[10, 10].setSpriteIndex(1);
            mapTiles[11, 10].setTraversable(false);
            mapTiles[11, 10].setSpriteIndex(1);
            mapTiles[12, 10].setTraversable(false);
            mapTiles[12, 10].setSpriteIndex(1);

            tileTextures = new LinkedList<Texture2D>();
            tileEffects = new LinkedList<FTileEffect>();
        }
    }
}
