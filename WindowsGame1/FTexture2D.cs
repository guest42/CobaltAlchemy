using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CobaltAlchemy
{
    public class FTexture2D
    {
        public  Texture2D texture;
        public int width_per_cell, height_per_cell, offset_x, offset_y;

        public FTexture2D(Texture2D _texture)
        {
            texture = _texture;
            width_per_cell = 0;
            height_per_cell = 0;
            offset_x = 0;
            offset_x = 0;
        }

        public FTexture2D(Texture2D _texture, int _width_per_cell, int _height_per_cell, int _offset_x, int _offset_y)
        {
            texture = _texture;
        }
    }
}
