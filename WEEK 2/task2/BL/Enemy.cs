using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2.BL
{
    internal class Enemy
    {
        public Enemy(char symbol, int x, int y) 
        {
            this.symbol = symbol;
            this.x = x;
            this.y = y;

        }
        public char symbol;
        public int x;
        public int y;
    }
}
