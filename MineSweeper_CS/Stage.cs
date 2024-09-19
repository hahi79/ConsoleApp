//======================================
//	マインスィーパー ステージ
//======================================using System;
using System.Diagnostics;   // Debug
using System;               // Console
using Utility = GP2.Utility;

namespace MineSweeper_CS
{
    internal class Stage
    {
        const int FIELD_WIDTH = 9;
        const int FIELD_HEIGHT = 9;
        // ボムの設置数
        const int BOMB_COUNT = 10;

        Cell[,] m_field;    // フィールド
        int m_cursorX;      // カーソル座標
        int m_cursorY;
        bool m_isExplosion; // 爆発した
        bool m_isClear;     // クリアした
        // コンストラクタ
        public Stage()
        {
            // フィールド生成
            m_field = new Cell[FIELD_HEIGHT, FIELD_WIDTH];
            for(int y=0; y<FIELD_HEIGHT; y++)
            {
                for(int x=0; x<FIELD_WIDTH; x++)
                {
                    m_field[y, x] = new Cell();
                }
            }
            SetupField();
            UpdateField();
            m_cursorX = 0;
            m_cursorY = 0;
            m_isExplosion = false;
            m_isClear = false;
        }

        // フィールド初期化
        protected void SetupField()
        {
            // 全部リセット
            for(int y=0; y<FIELD_HEIGHT; y++)
            {
                for(int x=0; x<FIELD_WIDTH; x++)
                {
                    Cell cell = GetFieldCell(x, y);
                    cell.Setup();
                }
            }
            // ランダムにBOMB配置
            int count = BOMB_COUNT;
            while (count > 0)
            {
                int x = Utility.GetRand(FIELD_WIDTH);
                int y = Utility.GetRand(FIELD_HEIGHT);
                Cell cell = GetFieldCell(x, y);
                if (cell.bomb == false)
                {
                    cell.bomb = true;
                    count--;
                }
            }
        }
        // 隣接ボムの数を更新
        protected void UpdateField()
        {
            for (int y = 0; y < FIELD_HEIGHT; y++)
            {
                for (int x = 0; x < FIELD_WIDTH; x++)
                {
                    Cell cell = GetFieldCell(x, y);
                    cell.adjacentBombs = GetAdjacentBombsCount(x, y);
                }
            }

        }
        // フィールドほ全部開示する
        protected void OpenFieldAll()
        {
            for(int y=0; y<FIELD_HEIGHT; y++)
            {
                for(int x=0; x<FIELD_WIDTH; x++)
                {
                    Cell cell = GetFieldCell(x, y);
                    cell.Open();
                }
            }
        }
        // 隣接ボムの数を取得
        protected int GetAdjacentBombsCount(int x, int y)
        {
            int count = 0;
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }
                    Cell cell = GetFieldCell(x + dx, y + dy);
                    if (cell != null && cell.bomb)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        // 自動開示(再帰)
        protected void AutoEraseMine(int x,int y)
        {
            Cell cell = GetFieldCell(x, y);
            if(cell==null || cell.bomb || cell.hide == false)
            {
                return;
            }
            cell.hide = false;
            if (cell.adjacentBombs == 0)
            {
                // 隣接マスを開いてみる
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        if (dx == 0 && dy == 0)
                        {
                            continue;
                        }
                        AutoEraseMine(x + dx, y + dy);
                    }
                }
            }
        }
        const string AA_CURSOR = "＠";  // カーソル
        const string AA_EXPLOSION = "※"; // 爆発
        const string AA_FLAG = "▲";  // フラグ
        const string AA_BOMB = "●";  // 爆弾
        const string AA_MINE = "■";  // 未開放
        string[] AA_NUMBERS = new string[]{   // 開放済
	        "・",
            "１",
            "２",
            "３",
            "４",
            "５",
            "６",
            "７",
            "８",
            "９",
        };
        // スクリーン描画
        public void DrawScreen()
        {
            int hideCount = 0;
            int bombCount = 0;
            int flagCount = 0;
            Utility.ClearScreen();
            for(int y=0; y<FIELD_HEIGHT; y++)
            {
                for(int x=0; x<FIELD_WIDTH; x++)
                {
                    Cell cell = GetFieldCell(x, y);
                    if(IsCursorPosition(x,y) && m_isClear == false)
                    {
                        string tmp = m_isExplosion ? AA_EXPLOSION : AA_CURSOR;
                        Utility.Printf(tmp);
                    }
                    else
                    {
                        if (cell.flag)
                        {
                            Utility.Printf(AA_FLAG);
                        }else if (cell.hide)
                        {
                            Utility.Printf(AA_MINE);
                        }
                        else if (cell.bomb)
                        {
                            Utility.Printf(AA_BOMB);
                        }
                        else
                        {
                            int n = cell.adjacentBombs;
                            Debug.Assert(0 <= n && n < AA_NUMBERS.Length);
                            Utility.Printf(AA_NUMBERS[n]);
                        }
                    }
                    if (cell.bomb) bombCount++;
                    if (cell.hide) hideCount++;
                    if (cell.flag) flagCount++;
                }
                Utility.Putchar('\n');
            }
            Utility.Putchar('\n');

            Utility.Printf("フラグ／ボム:{0,3}/{1,3}\n", flagCount, bombCount);
            int open1 = FIELD_WIDTH * FIELD_HEIGHT - hideCount;   // 開放済の数
            int open2 = FIELD_WIDTH * FIELD_HEIGHT - bombCount; // 開放すべき数
            Utility.Printf("開放　　　　:{0,3}/{1,3}\n", open1, open2);

        }
        // カーソル位置か?
        protected bool IsCursorPosition(int x,int y)
        {
            return x == m_cursorX && y == m_cursorY;
        }
        // カーソルのセルのflagを反転
        public void FlipCursorFlag()
        {
            Cell cell = GetCursorCell();
            cell.FlipFlag();
        }
        // カールのセルを開放(終了フラグを返す)
        public bool OpenCursorCell()
        {
            Cell cell = GetCursorCell();
            cell.hide = false;
            if (cell.bomb)
            {
                Beep();
                m_isExplosion = true;
                OpenFieldAll();
            }
            else if(IsClear())
            {
                Beep();
                m_isClear = true;
            }
            // 自動開示
            for(int dy=-1; dy<=1; dy++)
            {
                for(int dx=-1; dx<=1; dx++)
                {
                    if(dx==0 && dy==0)
                    {
                        continue;
                    }
                    AutoEraseMine(m_cursorX + dx, m_cursorY + dy);
                }
            }
            return m_isExplosion || m_isClear;
        }
        // クリアか?
        protected bool IsClear()
        {
            for(int y=0; y<FIELD_HEIGHT; y++)
            {
                for(int x=0; x<FIELD_WIDTH; x++)
                {
                    Cell cell = GetFieldCell(x, y);
                    // ボムでないことろが空いてないなら、非クリア
                    if(cell.bomb==false && cell.hide)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        // カーソル移動
        public void MoveCursor(int addX,int addY)
        {
            int x = m_cursorX + addX;
            int y = m_cursorY + addY;
            if (IsInField(x,y)){
                m_cursorX = x;
                m_cursorY = y;
            }
        }
        // カーソル位置のセルを取得
        protected Cell GetCursorCell()
        {
            return GetFieldCell(m_cursorX, m_cursorY);
        }
        // フィールトのセルを取得(範囲外ならnull)
        public Cell GetFieldCell(int x,int y)
        {
            if (IsInField(x, y))
            {
                return m_field[y, x];
            }
            return null;
        }
        // 勝った?
        public bool IsWin()
        {
            return m_isClear;
        }
        // 負けた?
        public bool IsLose()
        {
            return m_isExplosion;
        }
        // フィールド内か?
        protected bool IsInField(int x, int y)
        {
            return 0 <= x && x < FIELD_WIDTH
                && 0 <= y && y < FIELD_HEIGHT;
        }
        // ビープ音を鳴らす
        protected void Beep()
        {
            Console.Write("\a");
        }
    }
}
