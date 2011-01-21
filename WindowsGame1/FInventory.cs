using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CobaltAlchemy
{
    class FInventory
    {
        public FItem[] bag;
        public FItem[] belt;
        public int current_set;
        public int bag_item_selected;
        FActor player;

        public FInventory(FActor _player)
        {
            player = _player;
            bag = new FItem[16];
            belt = new FItem[12];
            current_set = 0;
            bag_item_selected = 0;
        }

        public void setSlot(int slot) 
        {
            if (slot > 3)
                slot = 3;
            else if (slot < 0)
                slot = 0;

            belt[4 * current_set + slot] = bag[bag_item_selected];
        }

        public void addItem(FItem _item)
        {
        }
    }
}
