//======================================
//	戦国シミュレーション 城主
//======================================

namespace SengokuSimulation_CS
{
    enum LordId
    {
        DATE,      // 伊達輝宗
        UESUGI,    // 上杉謙信
        TAKEDA,    // 武田信玄
        HOJO,      // 北条氏政
        TOKUGAWA,  // 徳川家康
        ODA,       // 織田信長
        ASHIKAGA,  // 足利義昭
        MORI,      // 毛利元就
        CHOSOKABE, // 長宗我部元親
        SHIMAZU,   // 島津義久
        HASHIBA,   // 羽柴秀吉
        Max,       // (種類の数)
        None = -1,
    }

    struct LordName
    {
        string m_familyName;  // 姓
        string m_firstName;   // 名
        string m_mapName;     // マップ上の名前

        // コンストラクタ
        public LordName(string familyName, string firstName, string mapName)
        {
            m_familyName = familyName;
            m_firstName = firstName;
            m_mapName = mapName;
        }
        // プロパティ
        public string familyName
        {
            get { return m_familyName; }
        }
        public string firstName
        {
            get { return m_firstName; }
        }
        public string mapName
        {
            get { return m_mapName; }
        }
    }

    static class Lord
    {
        // 城主の名を取得
        public static string GetFirstName(LordId id)
        {
            int idx = (int)id;
            if (0 <= idx && idx < Data.lordNames.Length)
            {
                return Data.lordNames[idx].firstName;
            }
            return "??";
        }
        // 城主の姓を取得
        public static string GetFamilyName(LordId id)
        {
            int idx = (int)id;
            if (0 <= idx && idx < Data.lordNames.Length)
            {
                return Data.lordNames[idx].familyName;
            }
            return "??";
        }
        // 城主のマップ上の名前を取得
        public static string GetMapName(LordId id)
        {
            int idx = (int)id;
            if (0 <= idx && idx < Data.lordNames.Length)
            {
                return Data.lordNames[idx].mapName;
            }
            return "??";
        }
    } // class
} // namespace 
