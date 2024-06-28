//===========================================
//  テスト メイン
//===========================================
using System;
using System.Runtime.InteropServices;

class test
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

    static void Main(string[] args)
    {
        while (true)
        {
            ConsoleKeyInfo info = Console.ReadKey(true);
            Console.WriteLine(string.Format("{0:x}-{0}",info.Key));
            if (info.Key == ConsoleKey.Enter)
            {
                break;
            }
        }
        Console.Clear();
        Console.WriteLine("Console is clear");

        Console.WriteLine(String.Format("{0}赤{1}", EscRED, EscDEFAULT));
        Console.WriteLine(String.Format("{0}緑{1}", EscGREEN, EscDEFAULT));
        Console.WriteLine(String.Format("{0}黄{1}", EscYELLOW, EscDEFAULT));
        Console.WriteLine(String.Format("{0}青{1}", EscBLUE, EscDEFAULT));
        Console.WriteLine(String.Format("{0}紫{1}", EscMAZENTA, EscDEFAULT));
        Console.WriteLine(String.Format("{0}シ{1}", EscCYAN, EscDEFAULT));

    }
}