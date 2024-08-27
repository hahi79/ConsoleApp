//======================================
//	戦国シミュレーション 城
//======================================

using System.Security.Principal;

namespace SengokuSimulation_CS
{
    enum CastleId
    {
        YONEZAWA,        // 米沢城
        KASUGAYAMA,      // 春日山城
        TSUTSUJIGASAKI,  // 躑躅ヶ崎館
        ODAWARA,         // 小田原城
        OKAZAKI,         // 岡崎城
        GIFU,            // 岐阜城
        NIJO,            // 二条城
        YOSHIDAKORIYAMA, // 吉田郡山城
        OKO,             // 岡豊城
        UCHI,            // 内城
        MAX,             // (種類の数)
        NONE = -1,         // リスト終端
    }
    internal class Castle
    {
        string m_name;      // 名前
        LordId m_owner;     // 城主
        int m_troopCount;   // 兵数
        CastleId[] m_connectedList;  // 接続された城のリスト
        int m_curx, m_cury; // 描画位置
        string m_mapName;   // マップ上の名前

        public string name
        {
            get { return m_name; }
        } 
        public LordId owner
        {
            get { return m_owner; }
            set { m_owner = value; }
        }
        public CastleId[] connectedList
        {
            get { return m_connectedList; }
        } 
        public int troopCount
        {
            get { return m_troopCount; }
            set { m_troopCount = value; }
        }
        public int curx
        {
            get { return m_curx; }
        }
        public int cury
        {
            get { return m_cury; }
        }
        public string mapName
        {
            get { return m_mapName; }
        }

        // コンストラクタ
        public Castle(string name, LordId owenre, CastleId[] connectedList, int curx, int cury, string mapName)
        {
            m_name = name;
            m_owner = owner;
            m_connectedList = connectedList;
            m_curx = curx;
            m_cury = cury;
            m_mapName = mapName;
        }
        // 兵数に加算する
        public void AddTroopCount(int add)
        {
            int value = m_troopCount + add;
            if (value < 0)
            {
                value = 0;
            }else if (value > Data.TROOP_MAX)
            {
                value = Data.TROOP_MAX;
            }
            m_troopCount = value;
        }
    } // class
} // namespace
