using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CobaltAlchemy
{
    class FItem
    {   
        int hold_type;
        int spawn_type;
        public int sprite_index;
        //animated 

        public FItem(int _sprite_index)
        {
            sprite_index = _sprite_index;
        }
    }
}
