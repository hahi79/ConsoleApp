//======================================
//      コマンド
//======================================using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnBattle = TurnBattle_CS.TurnBattle;
using Utility = GP2.Utility;
using System.Reflection.Metadata.Ecma335;

namespace TurnBattle_CS
{
    enum Command
    {
        Fight,  // たたかう
        Spell,  // じゅもん
        Escape, // にげる
        Max,
    }

    static class CommandUI
    {
        static string[] commandName = new string[]
        {
            "たたかう",
            "じゅもん",
            "にげる",
        };
        // プレーヤコマンド取得
        public static Command GetPlayerCommand(TurnBattle btl)
        {
            const int cmdMax = (int)Command.Max;

            int cmd = 0;
            while (true)
            {
                btl.DrawBattleScreen();
                for (int i = 0; i < cmdMax; i++)
                {
                    string cur=(i==cmd)?"＞":"　";
                    Utility.Printf("{0}{1}\n", cur, commandName[i]);
                }
                switch (Utility.GetKey()) {
                    case ConsoleKey.UpArrow:
                        cmd--;
                        if (cmd < 0)
                        {
                            cmd = cmdMax - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        cmd++;
                        if (cmd >= cmdMax)
                        {
                            cmd = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return (Command)cmd;
                }
            }
        }
        // 敵のコマンド取得
        public static Command GetEnemyCommand()
        {
            return Command.Fight;
        }
    } // class CommandUI
} // namespace
