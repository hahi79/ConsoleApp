//======================================
//      ユーティリティ
//======================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP2
{
    enum Key
    {
        ARROW_U = ConsoleKey.UpArrow,
        ARROW_D = ConsoleKey.DownArrow,
        ARROW_L = ConsoleKey.LeftArrow,
        ARROW_R = ConsoleKey.RightArrow,
        DECIDE = ConsoleKey.Enter,
    }

    static class Utility
    {
        public const string EscBLACK   = "\x1b[30m";
        public const string EscRED     = "\x1b[31m";
        public const string EscGREEN   = "\x1b[32m";
        public const string EscYELLOW  = "\x1b[33m";
        public const string EscBLUE    = "\x1b[34m";
        public const string EscMAZENTA = "\x1b[35m";
        public const string EscCYAN    = "\x1b[36m";
        public const string EscWHITE   = "\x1b[37m";
        public const string EscDEFAULT = "\x1b[39m";

        private static Random s_rand = new Random();
        // 乱数初期化
        public static void InitRand()
        {
            //s_rand = new Random();
        }
        // 乱数取得
        public static int GetRand(int max)
        {
            return s_rand.Next(max);
        }
        // キー入力待ち
        public static void WaitKey()
        {
            Console.ReadKey(true);
        }
        // キー取得
        public static Key GetKey()
        {
            ConsoleKeyInfo info = Console.ReadKey(true);
            return (Key)info.Key;
        }
        // 画面クリア
        public static void ClearScreen()
        {
            Console.Clear();
        }
        // printf()関数
        public static void Printf(string fmt,params Object[] list)
        {
            string tmp = string.Format(fmt, list);
            Console.Write(tmp);
        }
        // putchar()関数
        public static void Putchar(Char c)
        {
            Console.Write(c);
        }
    } // class Utility
} // namespace