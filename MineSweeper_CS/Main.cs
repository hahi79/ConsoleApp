//======================================
//	マインスィーパー メイン
//======================================
using Utility = GP2.Utility;
using System;   // ConsoleKey

namespace MineSweeper_CS
{
    internal class MineSweeperMain
    {
        static int Main()
        {
            Utility.InitRand();

            ConsoleKey c;
            do
            {
                game();
                Utility.Printf("もう一度(y/n)?");
                Utility.PrintOut();
                while (true)
                {
                    c = Utility.GetKey();
                    if (c == ConsoleKey.Y || c == ConsoleKey.N)
                    {
                        break;
                    }
                }
            } while (c == ConsoleKey.Y);

            return 0;
        }
        static void game()
        {
            Stage stage = new Stage();

            bool isEnd = false;
            while (isEnd == false)
            {
                stage.DrawScreen();
                Utility.PrintOut();
                ConsoleKey c = Utility.GetKey();
                switch (c)
                {
                    case ConsoleKey.UpArrow:
                        stage.MoveCursor(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        stage.MoveCursor(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        stage.MoveCursor(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        stage.MoveCursor(1, 0);
                        break;
                    case ConsoleKey.F:
                        stage.FlipCursorFlag();
                        break;
                    case ConsoleKey.Spacebar:
                        isEnd = stage.OpenCursorCell();
                        break;
                }
            }
            if (stage.IsLose())
            {
                stage.DrawScreen();
                Utility.Printf("\nＢＯＭ！！　ＹＯＵ　ＬＯＳＥ．\n");
                Utility.PrintOut();
            }
            else
            {
                stage.DrawScreen();
                Utility.Printf("\nＣＬＥＡＲ！　ＹＯＵ　ＷＩＮ．\n");
                Utility.PrintOut();
            }
        }
    } // class
} // namespace
