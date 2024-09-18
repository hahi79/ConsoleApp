//======================================
//      ブロックくずし　メイン
//======================================

using GP2;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace BlickBreaker_CS
{
    // フィールド内容
    enum Field
    {
        NONE,  // 空間
        BLOCK, // ブロック
        OUT,   // (外側)
    }
    // DrawScreenモージ
    enum DrawMode
    {
        READY,
        GAME,
        PAUSE,
        CLEAR,
    }

    internal class Stage
    {
        const int FIELD_WIDTH = 14;
        const int FIELD_HEIGHT = FIELD_WIDTH * 2;
        const int PADDLE_WIDTH = 4;
        const int SCREEN_WIDTH = FIELD_WIDTH + 2;
        const int SCREEN_HEIGHT = FIELD_HEIGHT + 2;


        int m_ballX;
        int m_ballY;
        int m_ballVelocityX;
        int m_ballVelocityY;
        int m_paddleX;
        int m_paddleY;
        Field[,] m_field;

        const string AA_WALL = "■";
        const string AA_BALL = "●";
        const string AA_PADDLE = "回";
        const string AA_BLOCK = "□";
        const string AA_NONE = "　";

        // コンストラクター
        public Stage()
        {
            m_field = new Field[FIELD_HEIGHT, FIELD_WIDTH];
            ResetBall();
            InitPaddle();
            InitField();
        }
        protected void InitPaddle()
        {
            m_paddleX = (FIELD_WIDTH- PADDLE_WIDTH) / 2;
            m_paddleY = FIELD_HEIGHT - 3;
        }
        protected void InitField()
        {
            for (int y = 0; y < FIELD_HEIGHT; y++) {
                Field blk = (y < FIELD_HEIGHT / 4) ? Field.BLOCK : Field.NONE;
                for(int x=0; x<FIELD_WIDTH; x++)
                {
                    SetField(x, y, blk);
                }
            }
        }
        // ボール位置をリセット
        public void ResetBall()
        {
            m_ballX = Utility.GetRand(FIELD_WIDTH);
            m_ballY = FIELD_HEIGHT / 3;
            m_ballVelocityX = Utility.GetRand(2) == 0 ? 1 : -1;
            m_ballVelocityY = 1;
        }
        // スリクーン描画
        public void DrawScreen(DrawMode mode)
        {
            Utility.ClearScreen();
            DrawHorizontalWall();
            for(int y=0; y<FIELD_HEIGHT; y++)
            {
                Utility.Printf(AA_WALL);
                for(int x=0; x<FIELD_WIDTH; x++)
                {
                    if (IsBallPosition(x, y))
                    {
                        Utility.Printf(AA_BALL);
                    }else if (IsPaddlePosition(x,y))
                    {
                        Utility.Printf(AA_PADDLE);
                    }
                    else
                    {
                        Field blk = GetField(x, y);
                        Utility.Printf(blk == Field.BLOCK ? AA_BLOCK : AA_NONE);
                    }
                }
                Utility.Printf(AA_WALL);
                Utility.Putchar('\n');
            }
            DrawHorizontalWall();

            string? msg = null;
            switch (mode)
            {
                case DrawMode.PAUSE:
                    msg = "ＰＡＵＳＥ";
                    break;
                case DrawMode.READY:
                    msg = "ＲＥＡＤＹ";
                    break;
                case DrawMode.CLEAR:
                    msg = "ＳＴＡＧＥ　ＣＬＥＡＲ";
                    break;
            }
            if (msg != null)
            {
                Utility.SaveCursor();
                int len = msg.Length;
                Utility.PrintCursor((SCREEN_WIDTH - len) / 2 * 2 + 1, SCREEN_HEIGHT / 2 + 1);
                Utility.Printf(msg);
                Utility.RestoreCursor();
            }
        }
        // ボール位置か?
        protected bool IsBallPosition(int x,int y)
        {
            return x == m_ballX && y == m_ballY;
        }
        // パドル位置か?
        protected bool IsPaddlePosition(int x,int y)
        {
            if (y == m_paddleY)
            {
                int dx = x - m_paddleX;
                return 0 <= dx && dx < PADDLE_WIDTH;
            }
            return false;
        }
        // 水平壁を描画
        protected void DrawHorizontalWall()
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Utility.Printf(AA_WALL);
            }
            Utility.Putchar('\n');
        }
        // ボール移動
        public void MoveBall()
        {
            m_ballX += m_ballVelocityX;
            m_ballY += m_ballVelocityY;
            // ボールが端にあるなら速度反転
            if (m_ballX <= 0)
            {
                m_ballVelocityX = 1;
            }
            else if (m_ballX >= FIELD_WIDTH - 1)
            {
                m_ballVelocityX = -1;
            }
            if (m_ballY <= 0)
            {
                m_ballVelocityY = 1;
            }
            else if (m_ballY >= FIELD_HEIGHT - 1)
            {
                m_ballVelocityY = -1;
            }
            // ボールがハドルに当たったら反射
            if(IsPaddlePosition(m_ballX, m_ballY + 1))
            {
                int dx = m_ballX - m_paddleX;
                m_ballVelocityX = dx < PADDLE_WIDTH / 2 ? -1 : 1;
                m_ballVelocityY = -1;
            }
            // ボールの上3コマのブロックを消す
            for(int x=m_ballX-1; x<=m_ballX+1; x++)
            {
                int y = m_ballY - 1;
                if (GetField(x, y) == Field.BLOCK)
                {
                    SetField(x, y, Field.NONE);
                    m_ballVelocityY = 1;
                }
            } 
        }
        // ボールを落としてしまった?
        public bool IsBallMiss()
        {
            return m_ballY == FIELD_HEIGHT - 1;
        }
        // 面クリア?
        public bool IsClear()
        {
            for (int y = 0; y < FIELD_HEIGHT; y++)
            {
                for (int x = 0; x < FIELD_WIDTH; x++)
                {
                    if (GetField(x, y) == Field.BLOCK)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        // パドル移動
        public void MovePaddle(int addX)
        {
            int x = m_paddleX + addX;
            if (0 <= x && x + PADDLE_WIDTH - 1 < FIELD_WIDTH)
            {
                m_paddleX = x;
            }
        }
        // フィールドのセッター
        protected void SetField(int x, int y, Field value)
        {
            if (IsInField(x, y))
            {
                m_field[y, x] = value;
            }
        }
        // フィールドのゲッター
        protected Field GetField(int x, int y)
        {
            if (IsInField(x, y))
            {
                return m_field[y, x];
            }
            return Field.OUT;
        }
        // フィールド内か?
        protected bool IsInField(int x, int y)
        {
            return 0 <= x && x < FIELD_WIDTH
                && 0 <= y && y < FIELD_HEIGHT;
        }
    }

}
