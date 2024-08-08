//======================================
//      ターン制バトル メイン
//======================================
using Utility = GP2.Utility;
using UI = ClassicRPG_CS.UI;

namespace ClassicRPG_CS
{
    class ClassicRPGMain
    {
        static int Main()
        {
            Utility.InitRand();

            ConsoleKey c;
            do
            {
                Game();
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
            } while (c == ConsoleKey.Y) ;
            return 0;
        }
        
        static void Game()
        { 
            Character player = new Character(
                100,        // HP
                15,         // MP
                30,         // 攻撃力
                "ゆうしゃ", // 名前
                ""          // アスキーアート
                );
            Character boss = new Character(
                255,        // HP
                 0,         // MP
                 50,        // 攻撃力
                 "まおう",  // 名前
                "　　Ａ＠Ａ\n" +   // アスキーアート
                "ψ（▼皿▼）ψ"
                );
            Character zako = new Character(
                3,         // HP
                0,         // MP
                2,         // 攻撃力
                "スライム",// 名前
                "／・Д・＼\n" + // アスキーアート
                "～～～～～"
                );

            Stage stage = new Stage(player, boss);
            stage.RegistZako(zako);
            stage.ChangeStartMap();

            while (true)
            {
                MapSpec spec = stage.mapSpec;
                stage.DrawMap();
                Utility.PrintOut();

                int playerX = stage.playerX;
                int playerY = stage.playerY;
                switch (Utility.GetKey())
                {
                    case ConsoleKey.UpArrow: playerY--; break;
                    case ConsoleKey.DownArrow: playerY++; break;
                    case ConsoleKey.LeftArrow: playerX--; break;
                    case ConsoleKey.RightArrow: playerX++; break;
                }
                char c = spec.GetMapData(playerX, playerY);
                switch (c)
                {
                    case MapSpec.outOfMap:
                        stage.ChangeMap(spec.nextMap_Out);
                        break;
                    case 'K':  // 王様の城
                        stage.ChangeMap(spec.nextMap_K);
                        break;
                    case 'B':  // 魔王の城
                        stage.ChangeMap(spec.nextMap_B);
                        break;
                    case '0':  // 王様
                        TalkToKing(stage);
                        break;
                    case '1':  // 姫
                        TalkToPrincess(stage);
                        break;
                    case '2':  // 魔王
                        TalkToBoss(stage);
                        break;
                    case '.':  // 床/平地
                    case '#':  // 階段/橋
                        stage.SetPlayerPosition(playerX, playerY);
                        if(spec.isBattleEncount)
                        {
                            if (Utility.GetRand(16) == 0)
                            {
                                stage.BattleZako();
                            }
                        }
                        break;
                }
                if (stage.IsPlayerDead())
                {
                    RevivePlayer(stage);
                    continue;  // continue while()
                }
                if (stage.IsBossDead())
                {
                    break; // break while()
                }
            }
            DrawEnding();
        }
        // 王様と話す
        static void TalkToKing(Stage stage)
        {
            Utility.Printf("＊「おお　ゆうしゃよ！\n"
                +"ひがしの　まじょうの　まおうを\n"
                +"たおし　せかいを　すくってくれ！\n"
            );
            Utility.PrintOut();
            Utility.WaitKey();
        }
        // 姫と話す
        static void TalkToPrincess(Stage stage)
        {
            Utility.Printf("＊「かみに　いのりを　ささげます。\n"
                +"おお　かみよ！\n"
                +"ゆうしゃさまに　しゅくふくを！\n"
            );
            Utility.PrintOut();
            Utility.WaitKey();
            // プレーヤ全回復
            stage.player.RecoverAllStatus();
        }
        // ボスと話す
        static void TalkToBoss(Stage stage)
        {
            Utility.Printf("＊「おろかな　にんげんよ！\n"
                +"わが　やぼうを　はばむものは\n"
                +"このよから　けしさってくれる！\n"
            );
            Utility.PrintOut();
            Utility.WaitKey();

            stage.BattleBoss();
        }
        // プレーヤ復活
        static void RevivePlayer(Stage stage)
        {
            stage.player.RecoverAllStatus();
            stage.ChangeStartMap();
            stage.DrawMap();
            Utility.Printf("＊「おお　ゆうしゃよ！\n"
              + "かみが　そなたを　すくわれた！\n"
              +  "ゆうしゃに　えいこう　あれ！\n");
            Utility.PrintOut();
            Utility.WaitKey();
        }
        // エンディング描画
        static void DrawEnding()
        {
            Utility.ClearScreen();
            Utility.Printf("　まおうは　ほろび　せかいは\n"
                +"めつぼうのききから　すくわれた！\n"
                +"\n"
                +"　おうは　ふれをだし　ゆうしゃを\n"
                +"さがしもとめたが、だれも\n"
                +"みたものは　いなかったという…\n"
                +"\n"
                +"\n"
                +"　　　　ＴＨＥ　ＥＮＤ\n"
                +"\n");
            Utility.PrintOut();
            Utility.WaitKey();
        }
    } // class
} // namespace 