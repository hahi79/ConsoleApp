//======================================
//	マインスィーパー セル
//======================================using System;

namespace MineSweeper_CS
{
    internal class Cell
    {
        bool m_bomb;        // ボムが置いてある
        bool m_hide;        // 未開放
        bool m_flag;        // プレーヤがつけたフラグ
        int m_adjacentBombs;  // 隣接ボムの数
        // プロパティ bomb
        public bool bomb
        {
            get { return m_bomb; }
            set { m_bomb = value; }
        }
        // プロパティ hide
        public bool hide
        {
            get { return m_hide; }
            set { m_hide = value; }
        }
        // プロパティ flag
        public bool flag
        {
            get { return m_flag; }
        }
        // プロパティ adjacentBombs
        public int adjacentBombs
        {
            get { return m_adjacentBombs; }
            set { m_adjacentBombs = value; }
        }
        // コンストラクター
        public Cell()
        {
            Setup();
        }
        // セットアップ(初期状態に)
        public void Setup()
        {
            m_bomb = false;
            m_hide = true;
            m_flag = false;
            m_adjacentBombs = 0;
        }
        // フラグを反転
        public void FlipFlag()
        {
            m_flag = !m_flag;
        }
        // (爆発後)オープンにする
        public void Open()
        {
            m_hide = false;
            m_flag = false;
        }
    } // class
} // namespace
