using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Models
{
    
    public class Line
    {
        public int id { set; get; }

        public int startX { set; get; }
        public int startY { set; get; }
        public int endX { set; get; }
        public int endY { set; get; }

        public int squareX { set; get; }
        public int squareY { set; get; }
        public int squareWidth { set; get; }
        public int squareHeight { set; get; }

        public int weight { set; get; }

    }
}
