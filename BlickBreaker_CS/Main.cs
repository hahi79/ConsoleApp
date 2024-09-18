//======================================
//      ブロックくずし　メイン
//======================================
using GP2;
using System;  // ConsoleKey
using Utility = GP2.Utility;
using IntervalTimer = GP2.IntervalTimer;

namespace BlickBreaker_CS
{
    internal class BlockBreakerMain
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

        const int FPS = 10;

        static void game()
        {
            Stage stage = new Stage();
            IntervalTimer timer = new IntervalTimer();

            stage.DrawScreen(DrawMode.READY);
            Utility.PrintOut();
            Utility.WaitKey();


            timer.StartTimer(FPS);
            while (true)
            {
                if (timer.IsInterval)
                {
                    stage.MoveBall();
                    stage.DrawScreen(DrawMode.GAME);
                    Utility.PrintOut();

                    if (stage.IsBallMiss())
                    {
                        stage.DrawScreen(DrawMode.READY);
                        Utility.PrintOut();
                        Utility.WaitKey();
                        stage.ResetBall();
                    }
                    if (stage.IsClear())
                    {
                        stage.DrawScreen(DrawMode.CLEAR);
                        Utility.PrintOut();
                        Utility.WaitKey();
                        break;
                    }
                }
                if (Utility.KeyAvailable())
                {
                    ConsoleKey c = Utility.GetKey();
                    switch (c)
                    {
                        case ConsoleKey.LeftArrow:
                            stage.MovePaddle(-1);
                            break;
                        case ConsoleKey.RightArrow:
                            stage.MovePaddle(+1);
                            break;
                        case ConsoleKey.Spacebar: //一時停止
                            stage.DrawScreen(DrawMode.PAUSE);
                            Utility.PrintOut();
                            do
                            {
                                c = Utility.GetKey();

                            } while (c != ConsoleKey.Spacebar);
                            break;
                    }
                }
            }
        }
    }
}
