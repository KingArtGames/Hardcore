﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.map
{
    [Serializable]
    public class TileMap
    {
        public int height;
        public int width;
        public TileLayer[] layers;
        public int tileheight;
        public int tilewidth;
        public TileSet[] tilesets;
    }
    
    [Serializable]
    public class TileLayer
    {
        public int[] data;
        public int height;
        public string name;
        public bool opacity;
        public string type;
        public bool visible;
        public int width;
        public int x;
        public int y;
    }

    [Serializable]
    public class TileSet
    {
        public string image;
        public string name;
    }
}