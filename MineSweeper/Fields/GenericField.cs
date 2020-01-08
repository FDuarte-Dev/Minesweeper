using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    class GenericField
    {
        internal Tuple<int, int> Position;
        public bool Dug { get; internal set; }

        public GenericField(int row, int column)
        {
            Position = new Tuple<int, int>(row, column);
            Dug = false;
        }

        public void Dig()
        {
            Dug = true;
        }
    }
}
