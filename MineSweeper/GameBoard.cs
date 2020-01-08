using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    class GameBoard
    {
        GenericField[,] Board;

        private List<Tuple<int, int>> GameMines;
        private int Rows;
        private int Columns;
        private int Mines;
        private int AvailablePlays;

        public void Play(int row, int column)
        {
            if (row < 0 ||
               row > Rows ||
               column < 0 ||
               column > Columns)
                throw new InvalidPositionException();
            if (Board[row, column].Dug)
                throw new AlreadyDugException();

            Board[row, column].Dig();

            if(Board[row, column].GetType()
                                 .Equals(typeof(SafeField)) &&
               (Board[row, column] as SafeField).Value == 0)
            {
                UncoverSafeFields(row, column);
            }

            if (Board[row, column].GetType().Equals(typeof(MineField)))
                throw new GameLostException();

            AvailablePlays--;

            if (AvailablePlays == 0)
                throw new GameWonException();
        }

        private void UncoverSafeFields(int row, int column)
        {
            for (int i = 0; i < 8; i++)
            {
                try
                {
                    switch (i)
                    {
                        case 0:
                                Play(row - 1, column - 1);
                            break;
                        case 1:
                                Play(row - 1, column);
                            break;
                        case 2:
                                Play(row - 1, column + 1);
                            break;
                        case 3:
                                Play(row, column - 1);
                            break;
                        case 4:
                            Play(row, column + 1);
                            break;
                        case 5:
                            Play(row + 1, column - 1);
                            break;
                        case 6:
                            Play(row + 1, column);
                            break;
                        case 7:
                            Play(row + 1, column + 1);
                            break;
                    }
                }
                catch(Exception)
                {
                    continue;
                }
            }
        }

        public void InitBoard(int[] parameters)
        { 
            Rows = parameters[0];
            Columns = parameters[1];
            Mines = parameters[2];

            Board = new GenericField[Rows, Columns];

            AvailablePlays = (Rows * Columns) - Mines;
        }

        public void AddMines(int[] parameters)
        {
            GameMines = new List<Tuple<int, int>>(Mines);

            Random rand = new Random();

            int row;
            int column;

            for (int i = 0; i < Mines; i++)
            {
                row = rand.Next(Rows - 1);
                column = rand.Next(Columns - 1);

                if (Board[row, column] == default(GenericField))
                {
                    Board[row, column] = new MineField(row, column);

                    GameMines.Add(new Tuple<int, int>(row, column));
                }
                else
                {
                    i--;
                }
            }
        }

        public void AddFields()
        {
            foreach(Tuple<int, int> mine in GameMines)
            {
                int row, column;
                for(int i = 0; i < 8; i++)
                {
                    switch (i)
                    {
                        case 0:
                            row = mine.Item1 - 1;
                            column = mine.Item2 - 1;
                            break;
                        case 1:
                            row = mine.Item1 - 1;
                            column = mine.Item2;
                            break;
                        case 2:
                            row = mine.Item1 - 1;
                            column = mine.Item2 + 1;
                            break;
                        case 3:
                            row = mine.Item1;
                            column = mine.Item2 - 1;
                            break;
                        case 4:
                            row = mine.Item1;
                            column = mine.Item2 + 1;
                            break;
                        case 5:
                            row = mine.Item1 + 1;
                            column = mine.Item2 - 1;
                            break;
                        case 6:
                            row = mine.Item1 + 1;
                            column = mine.Item2;
                            break;
                        case 7:
                            row = mine.Item1 + 1;
                            column = mine.Item2 + 1;
                            break;
                        default:
                            row = mine.Item1;
                            column = mine.Item2;
                            break;
                    }

                    if(row < 0 ||
                       row >= Rows ||
                       column < 0 ||
                       column >= Columns)
                    {
                        continue;
                    }

                    if (Board[row, column] == default(GenericField))
                    {
                        Board[row, column] = new SafeField(row, column, true);
                    }
                    else if (!Board[row, column]
                                .GetType()
                                .Equals(typeof(MineField)))
                    {
                        (Board[row, column] as SafeField).AddValue();
                    }
                }
            }

            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    if(Board[i, j] == default(GenericField))
                    {
                        Board[i, j] = new SafeField(i, j);
                    }
                }
            }
        }

        internal void UncoverBoard()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Board[i, j].Dig();
                }
            }
        }

        public override string ToString()
        {
            string traceBoard = string.Empty;

            traceBoard += AddCeiling(Columns);

            for (int i = 0; i < Rows; i++)
            {
                traceBoard += i + 1; traceBoard += "  ";
                for (int j = 0; j < Columns; j++)
                {
                    traceBoard += Board[i, j].ToString();
                } traceBoard += "║";

                traceBoard += (i < Rows - 1)? AddLine(Columns) : "";
            }

            traceBoard += AddFloor(Columns);

            return traceBoard + Environment.NewLine;
        }

        private string AddCeiling(int columns)
        {
            string line = "   ";
            for (int i = 1; i < columns + 1; i++)
            {
                line += " " + i + " ";
            }
            line += Environment.NewLine;

            line += "   ╔══";
            for (int i = 1; i < columns; i++)
            {
                line += "╦══";
            }
            line += "╗";

            return line + Environment.NewLine;
        }

        private string AddFloor(int columns)
        {
            string line = Environment.NewLine;
            line += "   ╚══";
            for (int i = 1; i < columns; i++)
            {
                line += "╩══";
            }
            line += "╝";

            return line;
        }

        private string AddLine(int columns)
        {
            string line = Environment.NewLine;
            line += "   ╠══";
            for (int i = 1; i < columns; i++)
            {
                line += "╬══";
            } line += "╣";

            return line + Environment.NewLine;
        }
    }
}
