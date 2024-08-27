//======================================
//	戦国シミュレーション メイン
//======================================
using Utility = GP2.Utility;
using System;  // ConsoleKey

namespace SengokuSimulation_CS
{
    internal class SengokuSimulationMain
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
            var chro = new Chronology();
            var stage = new Stage(Data.castles, chro, Data.START_YEAR);
            stage.Start();
            CastleId playerCastle = UI.InputPlayerCastle(stage);
            stage.SetPlayerLord(playerCastle);
            DrawIntro(stage, playerCastle);

            while (true)
            {
                // ターンの順番をシャフル
                stage.MakeTurnOrder();
                for (int i = 0; i < stage.castlesSize; i++)
                {
                    // 各城のターン実行
                    stage.ExecTurn(i);
                    // プレーヤの負け?
                    if (stage.IsPlayerLose())
                    {
                        DrawGameOver(stage);
                        goto exit;
                    }
                    // プレーヤの勝ち
                    if (stage.IsPlayerWin())
                    {
                        DrawEnding(stage);
                        goto exit;
                    }
                }
                // 年越し
                stage.NextYear();
                // 「本能寺の変」イベント
                if (stage.IsHonnojiEvent())
                {
                    stage.isDoneHonnojiEvent = true;
                    DrawHonnojiEvent(stage);
                }
            }
        exit:
            /*nothing*/
            ;
        }
        // 本能寺の変を描画
        static void DrawHonnojiEvent(Stage stage)
        {
            stage.DrawScreen(DrawMode.Event, 0);
            Utility.Printf(
                "明智光秀「てきは　本能寺に　あり！\n" +
                "\n" +
                "明智光秀が　本能寺の　織田信長を　しゅうげきした！\n" +
                "\n" +
                "織田信長「ぜひに　およばず…\n" +
                "\n" +
                "織田信長は　本能寺に　ひをはなち　じがいした！\n" +
                "\n" +
                "ごじつ、羽柴秀吉が　山崎のたたかいで　明智光秀を　たおし、\n" +
                "織田けの　こうけいの　ちいを　さんだつした！\n"
                );

            Utility.PrintOut();
            Utility.WaitKey();
        }
        // GameOver を描画
        static void DrawGameOver(Stage stage)
        {
            stage.DrawScreen( DrawMode.GameOver, 0);
            // 年表を表示
            stage.chro.Print();
            Utility.Putchar('\n');
            Utility.Printf("ＧＡＭＥ　ＯＶＥＲ\n");
            Utility.PrintOut();
            Utility.WaitKey();
        }
        // エンディングを描画
        static void DrawEnding(Stage stage)
        {
            stage.DrawScreen(DrawMode.Ending, 0);
            // 年表を表示
            stage.chro.Print();
            int year = stage.year + 3;
            string name1 = stage.GetLordFamilyName(stage.playerLord);
            string name2 = stage.GetLordFirstName(stage.playerLord);
            Utility.Printf("{0}ねん　 {1} {2}が　せいいたいしょうぐんに　にんぜられる\n", year, name1, name2);
            Utility.Printf("{0}ねん　{1}{2}が　{1}ばくふを　ひらく\n", year, name1, name2);
            Utility.Putchar('\n');
            Utility.Printf("ＴＨＥ　ＥＮＤ");
            Utility.PrintOut();
            Utility.WaitKey();
        }
        // イントロ表示
        static void DrawIntro(Stage stage, CastleId playerCastle)
        {
            LordId player = stage.GetCastleOwner(playerCastle);
            Utility.Printf("{0}さま、 {1}から　てんかとういつを\nめざしましょうぞ！\n"
                , stage.GetLordFirstName(player)
                , stage.GetCastleName(playerCastle)
            );
            Utility.PrintOut();
            Utility.WaitKey();
        }
    } // class
} // namespace 
