﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exile.Mobile.Models
{
    public enum MenuItemType
    {
        Home,
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
