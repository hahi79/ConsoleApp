﻿//======================================
//      リバーシ　メイン
//======================================
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;

namespace GP2
{
    enum Direction4
    {
        LEFT,
        UP,
        RIGHT,
        DOWN,
        MAX,
    }
    enum Direction8
    {
        UP,
        UP_LEFT,
        LEFT,
        DOWN_LEFT,
        DOWN,
        DOWN_RIGHT,
        RIGHT,
        UP_RIGHT,
        MAX,
    }

    internal struct Vector2
    {
        int m_x;  // x座標
        int m_y;  // y座標

        // コンストラクター
        public Vector2(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        // プロパティx
        public int x
        {
            get { return m_x; }
            set { m_x = value; }
        }
        // プロパティ y
        public int y
        {
            get { return m_y; }
            set { m_y = value; }
        }
        // 値セット
        public void Set(int x,int y)
        {
            m_x = x;
            m_y = y;
        }
        // 加算
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            Vector2 c = new Vector2(a.m_x + b.m_x, a.m_y + b.m_y);
            return c;
        }
        // 減算
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            Vector2 c = new Vector2(a.m_x - b.m_x, a.m_y - b.m_y);
            return c;
        }
        // 等価?
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.m_x == b.m_x && a.m_y == b.m_y;
        }
        // 不等価?
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return a.m_x != b.m_x || a.m_y != b.m_y;
        }

        public bool Equals(Vector2 b)
        {
            return m_x == b.m_x && m_y == b.m_y;
        }

        static Vector2[] directions4 = new Vector2[]
        {
                new Vector2( 0,-1),  // UP
                new Vector2(-1, 0),  // LEFT
                new Vector2( 0, 1),  // DOWN
                new Vector2( 1, 0),  // RIGHT
        };
        // ４方向のベクター取得
        public static Vector2 GetDirVector2(Direction4 dir)
        {
            return directions4[(int)dir];
        }
        static Vector2[] directions8 = new Vector2[]
        {
                new Vector2( 0,-1),  // UP
                new Vector2(-1,-1),  // UP_LEFT
                new Vector2(-1, 0),  // LEFT
                new Vector2(-1, 1),  // DOWN_LEFT
                new Vector2( 0, 1),  // DOWN
                new Vector2( 1, 1),  // DOWN_RIGHT
                new Vector2( 1, 0),  // RIGHT
                new Vector2( 1,-1),  // UP_RIGHT
        };
        // ８方向のベクター取得
        public static Vector2 GetDirVector2(Direction8 dir)
        {
            return directions8[(int)dir];
        }
    }
}
