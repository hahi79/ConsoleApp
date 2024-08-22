//======================================
//      王道RPG ステージ
//======================================
using Utility = GP2.Utility;

namespace ClassicRPG_CS
{
    internal class Stage
    {
        const int SCREEN_WIDTH = 16;
        const int SCREEN_HEIGHT = 12;

        MapSpec m_mapSpec;
        int m_playerX;
        int m_playerY;
        Character m_player;
        Character m_boss;
        List<Character> m_zako;

        // コンストラクター
        public Stage(Character player, Character boss)
        {
            m_mapSpec = null;
            m_playerX = 0;
            m_playerY = 0;
            m_player = player;
            m_boss = boss;
            m_player.Start();  // プレーヤ生存状態に
            m_boss.Start();    // ボス生存状態に
            m_zako = new List<Character>();
        }
        // プロパティ
        public MapSpec mapSpec
        {
            get { return m_mapSpec; }
        }
        public int playerX
        {
            get { return m_playerX; }
        }
        public int playerY
        {
            get { return m_playerY; }
        }
        public Character player
        {
            get { return m_player; }
        }
        // ザコ敵を登録
        public void RegistZako(Character zako)
        {
            m_zako.Add(zako);
        }
        // マップ描画
        public void DrawMap()
        {
            Utility.ClearScreen();
            for (int y = m_playerY - SCREEN_HEIGHT / 2; y < m_playerY + SCREEN_HEIGHT / 2; y++)
            {
                for (int x = m_playerX - SCREEN_WIDTH / 2; x < m_playerX + SCREEN_WIDTH / 2; x++)
                {
                    if (x == m_playerX && y == m_playerY)
                    {
                        Utility.Printf("勇");
                    }
                    else
                    {
                        char c = m_mapSpec.GetMapData(x, y);
                        if (c == MapSpec.outOfMap)
                        {
                            c = m_mapSpec.outData;
                        }
                        Utility.Printf(MapData.GetMapAA(c));
                    }
                }
                Utility.Putchar('\n');
            }
            Utility.Putchar('\n');
            m_player.IndicatePlayer();
            Utility.Putchar('\n');
        }
        // マップ遷移
        public void ChangeMap(NextMap next)
        {
            m_mapSpec = MapData.GetMapSpec(next.Id);
            m_playerX = next.X;
            m_playerY = next.Y;
        }
        // スタートマップへ遷移
        public void ChangeStartMap()
        {
            NextMap next = new NextMap(MapId.KingCastle, 4, 6);
            ChangeMap(next);
        }
        // プレーヤの位置セット
        public void SetPlayerPosition(int x, int y)
        {
            m_playerX = x;
            m_playerY = y;
        }
        // サゴバトル実行
        public void BattleZako()
        {
            int idx = Utility.GetRand(m_zako.Count);
            Character zako = m_zako[idx];
            ExecBattle(zako);
        }
        // ボスバトル実行
        public void BattleBoss()
        {
            ExecBattle(m_boss);
        }
        // バトル実行
        protected void ExecBattle(Character enemy)
        {
            TurnBattle btl = new TurnBattle(m_player, enemy);
            btl.Start();
            btl.Intro();
            bool isEnd = false;
            Command cmd;
            while (true)
            {
                cmd = UI.GetPlayerCommand(btl);
                isEnd = btl.ExecPlayerTurn(cmd);
                if (isEnd)
                {
                    break;
                }
                cmd = AI.GetEnemyCommand();
                isEnd = btl.ExecEnemyTurn(cmd);
                if (isEnd)
                {
                    break;
                }
                btl.NextTurn();
            }
        }
        // プレーヤ死亡?
        public bool IsPlayerDead()
        {
            return m_player.IsDead();
        }
        // ボス死亡?
        public bool IsBossDead()
        {
            return m_boss.IsDead();
        }
    } // class
} // namespace
