﻿using System;

namespace test132132
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            //Title = item?.Text;
            Item = item;
        }
    }
}
