using System;
using System.Collections.Generic;

namespace MineSweeper
{
    class MineSweeper
    {
        //[rows, columns, mines]
        static int[] gameParams = new int[3];

        static private GameBoard board = new GameBoard();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to MineSweeper." +
                              Environment.NewLine +
                              "Have fun.");
            Console.WriteLine("Press any key to begin");
            Console.ReadKey();
            Init();
            Exit();
        }

        private static void Init()
        {
            Clean();

            GetGameParameters();

            Clean();

            board.InitBoard(gameParams);

            int[] play = GetFirstPlay();

            board.AddMines(play);

            board.AddFields();

            PlayGame(play);
            PlayAgain();
        }

        private static int[] GetFirstPlay()
        {
            Clean();
            Console.WriteLine("Minesweeper");
            AddSeparator();

            Console.WriteLine(board.ToString());

            Console.WriteLine("Select a field to dig:");

            AddSeparator();

            int[] play = Array.ConvertAll(Console.ReadLine().Split(' '), playTemp => Convert.ToInt32(playTemp));

            return play;
        }

        private static void PlayAgain()
        {
            Clean();
            Console.WriteLine("Play Again?");
            string again = Console.ReadLine();
            if (again == "y" ||
                again == "yes")
                Init();
        }

        private static void PlayGame(int [] firstPlay)
        {
            bool gameRunning = DoPlay(firstPlay[0], firstPlay[1]);

            while (gameRunning)
            {
                Clean();
                Console.WriteLine("Minesweeper");
                AddSeparator();

                Console.WriteLine(board.ToString());

                Console.WriteLine("Select a field to dig:");

                AddSeparator();

                int[] play = Array.ConvertAll(Console.ReadLine().Split(' '), playTemp => Convert.ToInt32(playTemp));

                gameRunning = DoPlay(play[0], play[1]);
            }
        }

        private static bool DoPlay(int playRow, int playColumn)
        {
            try
            {
                if (playRow == 0 ||
                    playColumn == 0)
                {
                    return false;
                }

                board.Play(playRow - 1,
                           playColumn - 1);

                Console.Beep();

                return true;
            }
            catch (GameWonException)
            {
                Clean();

                board.UncoverBoard();
                Console.WriteLine(board.ToString());

                Console.WriteLine("Congratulations!!");
                Console.WriteLine("You Win");

                Console.Beep(540, 1000);

                Console.ReadKey();
                return false;
            }
            catch (AlreadyDugException)
            {
                Console.WriteLine("Already dug that position.");
                Console.ReadKey();
                return true;
            }
            catch (InvalidPositionException)
            {
                Console.WriteLine("Invalid position to dig.");
                Console.ReadKey();
                return true;
            }
            catch (GameLostException)
            {
                Clean();
                Console.WriteLine("Minesweeper");
                AddSeparator();

                board.UncoverBoard();

                Console.WriteLine(board.ToString());

                Console.WriteLine("Game Over!");
                Console.Beep(180, 1000);
                Console.ReadKey();
                return false;
            }
            catch (Exception) 
            {
                //Hmmmm.... Let's ignore this one
                return true;
            }
        }

        private static void GetGameParameters()
        {
            try
            {
                Console.WriteLine("Please select number of rows");
                gameParams[0] = Convert.ToInt32(Console.ReadLine());
                if (gameParams[0] < 1)
                    throw new ArgumentOutOfRangeException("Rows",
                                                          "Number of rows cannot be less than 1");

                Console.WriteLine("Please select number of columns");
                gameParams[1] = Convert.ToInt32(Console.ReadLine());
                if (gameParams[1] < 1)
                    throw new ArgumentOutOfRangeException("Columns",
                                                          "Number of columns cannot be less than 1");

                Console.WriteLine("Please select number of mines");
                gameParams[2] = Convert.ToInt32(Console.ReadLine());
                if (gameParams[2] < 1)
                    throw new ArgumentOutOfRangeException("Mines",
                                                          "Number of mines cannot be less than 1");
            }
            catch
            {
                Console.WriteLine("Invalid number selected");
                Exit();
            }
        }

        private static void Exit()
        {
            Clean();

            Console.WriteLine("Bye!");
            Console.WriteLine("Press any key to leave");
            Console.ReadKey();
        }

        public static void Clean()
        {
            Console.Clear();
            AddSeparator();
        }

        public static void AddSeparator()
        {
            Console.WriteLine();
            Console.WriteLine("---+----+----+----+----+----+----+----+----+----+---+----+----+----+----+----+----+----+----+----+");
            Console.WriteLine();
        }
    }
}
