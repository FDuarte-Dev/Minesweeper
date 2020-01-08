using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    class MineField : GenericField
    {
        public string Value = "X";

        public MineField(int row, int column) 
            : base(row, column)
        {
        }

        public override string ToString()
        {
            /*
             ║V
             */
            string field = "║";
            if (Dug)
                field += Value;
            else
                field += " ";

            return field.PadRight(3);
        }

        public string ToString(bool final)
        {
            Dig();
            return ToString();
        }
    }
}
