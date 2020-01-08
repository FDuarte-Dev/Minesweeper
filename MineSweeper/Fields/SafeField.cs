using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    class SafeField : GenericField
    {
        public int Value { get; private set; }

        public SafeField(int row, int column) 
            : base(row, column)
        {
            Init(false);
        }

        public SafeField(int row, int column, bool neighbor)
            : base(row, column)
        {
            Init(neighbor);
        }

        private void Init(bool neighbor)
        {
            Value = neighbor ?
                1 :
                0;
        }

        public void AddValue()
        {
            Value++;
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
