//======================================
//      ライフゲーム　パターン
//======================================
using GP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame_CS
{

    internal class Pattern
    {
        protected int m_width;        // パターンの横幅
        protected int m_height;       // パターンの縦幅
        protected byte[] m_data;      // パターンデータ
        protected string m_name;      // パターンの名前
        protected bool m_isLoopCells; // フィールドの左右・上下がループしているか?

        // コンストラクター
        public Pattern(int width,int height, byte[] data,string name,bool isLoopCells)
        {
            m_width = width;
            m_height = height;
            m_data = data;
            m_name = name;
            m_isLoopCells = isLoopCells;
        }
        // 横幅ゲッター
        public int width
        {
            get { return m_width; }
        }
        // 縦幅ゲッター
        public int height
        {
            get { return m_height; }
        }
        // データゲッター
        public byte[] data
        {
            get { return m_data; }
        }
        // isLoopCellsゲッター
        public bool isLoopCells
        {
            get { return m_isLoopCells; }
        }

        protected static byte[] _block = new byte[]
        {
            0,0,0,0,
            0,1,1,0,
            0,1,1,0,
            0,0,0,0,
        };
        protected static byte[] _tab = new byte[]
        {
            0,0,0,0,0,
            0,0,1,0,0,
            0,1,0,1,0,
            0,0,1,0,0,
            0,0,0,0,0,
        };
        protected static byte[] _boat=new byte[]
        {
            0,0,0,0,0,
            0,1,1,0,0,
            0,1,0,1,0,
            0,0,1,0,0,
            0,0,0,0,0,
        };
        protected static byte[] _snake=new byte[]
        {
            0,0,0,0,0,0,
            0,1,1,0,1,0,
            0,1,0,1,1,0,
            0,0,0,0,0,0,
        };
        protected static byte[] _ship=new byte[]
        {
            0,0,0,0,0,
            0,1,1,0,0,
            0,1,0,1,0,
            0,0,1,1,0,
            0,0,0,0,0,
        };
        protected static byte[] _aircarrier=new byte[]
        {
            0,0,0,0,0,0,
            0,1,1,0,0,0,
            0,1,0,0,1,0,
            0,0,0,1,1,0,
            0,0,0,0,0,0,
        };
        protected static byte[] _beehive=new byte[]
        {
            0,0,0,0,0,0,
            0,0,1,1,0,0,
            0,1,0,0,1,0,
            0,0,1,1,0,0,
            0,0,0,0,0,0,
        };
        protected static byte[] _barge=new byte[]
        {
            0,0,0,0,0,0,
            0,0,1,0,0,0,
            0,1,0,1,0,0,
            0,0,1,0,1,0,
            0,0,0,1,0,0,
            0,0,0,0,0,0,
        };
        protected static byte[] _pond = new byte[]
        {
            0,0,0,0,0,0,
            0,0,1,1,0,0,
            0,1,0,0,1,0,
            0,1,0,0,1,0,
            0,0,1,1,0,0,
            0,0,0,0,0,0,
        };

        // パターンテーブル
        protected static Pattern[] patterns = new Pattern[]
        {
            new Pattern(4, 4, _block, "ブロック", true),
            new Pattern(5, 5, _tab, "タブ", true),
            new Pattern(5, 5, _boat, "ボート", true),
            new Pattern(6, 4, _snake, "へび", true),
            new Pattern(5, 5, _ship, "船", true),
            new Pattern(6, 5, _aircarrier, "空母", true),
            new Pattern(6, 5, _beehive, "蜂の巣", true),
            new Pattern(6, 6, _barge,"はしけ", true),
            new Pattern(6, 6, _pond, "池", true),
        };

        protected static int sel = 0;
        public static Pattern SelectPattern()
        {
            while (true)
            {
                Utility.ClearScreen();
                for(int i=0; i<patterns.Length; i++)
                {
                    string cur=(sel==i)? "＞" : "　";
                    Utility.Printf("{0}{1}\n", cur, patterns[i].m_name);
                }
                Utility.PrintOut();
                switch(Utility.GetKey())
                {
                    case ConsoleKey.UpArrow:
                        sel--;
                        if (sel < 0)
                        {
                            sel = patterns.Length - 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        sel++;
                        if ((sel >= patterns.Length))
                        {
                            sel = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return patterns[sel];
                    case ConsoleKey.Escape:
                        return null;
                }
            }
        } // class
    }
} // namespace 
