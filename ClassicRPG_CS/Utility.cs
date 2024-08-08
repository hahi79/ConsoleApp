//======================================
//      ユーティリティ
//======================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GP2
{
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
        public static ConsoleKey GetKey()
        {
            ConsoleKeyInfo info = Console.ReadKey(true);
            return info.Key;
        }
        // キーが押されたか?
        public static bool KeyAvailable()
        {
            return Console.KeyAvailable;
        }
        public static void Sleep_mSec(int mSec)
        {
            Thread.Sleep(mSec);
        }
        static StringBuilder s_console = new StringBuilder();
        //--------------------------------------
        //      コンソール出力
        //--------------------------------------
        // 使い方 : StringBuilderにため込んで PrintOut()で実際の描画を行う
        //    Utility.ClearScreen();
        //    Utilitu.Printf(....);
        //    Utility.Putchar(.);
        //    ....
        //    Utility.PrintOut();
        //--------------------------------------
        // 画面クリア
        public static void ClearScreen()
        {
            s_console.Clear();
        }
        // printf()関数
        public static void Printf(string fmt,params Object[] list)
        {
            s_console.AppendFormat(fmt, list);
        }
        // putchar()関数
        public static void Putchar(Char c)
        {
            s_console.Append(c);
        }
        // puts()関数
        public static void Puts(string str)
        {
            s_console.Append(str).Append('\n');
        }
        // プリント出力
        public static void PrintOut()
        {
            Console.Clear();
            Console.Write(s_console.ToString());
        }
    } // class Utility
} // namespace