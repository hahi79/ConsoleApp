//======================================
//	戦国シミュレーション データ
//======================================

namespace SengokuSimulation_CS
{
    static class Data
    {
        public const int START_YEAR = 1570;
        public const int TROOP_BASE = 5;
        public const int TROOP_MAX = 9;
        public const int TROOP_UNIT = 1000;

        static string s_map =
        //0       1         2         3         4   
        //23456789012345678901234567890123456789012345678901234
        "1570ねん　～～～～～～～～～～～～～～～～　　　　　～\n" +     // 01
        "　　　　　～～～～～～～～～～～～～～～～　0米沢5　～\n" +     // 02
        "～～～～～～～～～～～～～～～～～～1春日5　伊達　～～\n" +     // 03
        "～～～～～～～～～～～～～～～　～～上杉　　　　　～～\n" +     // 04
        "～～～～～～～～～～～～～～～　～　　　　　　　　～～\n" +     // 05
        "～～～～～～～～～～～～～～　　　　　2躑躅5　　　～～\n" +     // 06
        "～～～～～～～～～～～～～　　　　　　武田　　　～～～\n" +     // 07
        "～～～～～～　　　　　　　5岐阜5　　　　　　　　～～～\n" +     // 08
        "～～～～　7吉田5　6二条5　織田　4岡崎5　3小田5　～～～\n" +     // 09
        "～～～　　毛利　　足利　　　　　徳川　　北条～～～～～\n" +     // 10
        "～～　～～～～～～～　　　～～～～～～～～～～～～～～\n" +     // 11
        "～　　　～　8岡豊5～～　～～～～～～～～～～～～～～～\n" +     // 12
        "～　　　～～長宗～～～～～～～～～～～～～～～～～～～\n" +     // 13
        "～9内城5～～～～～～～～～～～～～～～～～～～～～～～\n" +     // 14
        "～島津～～～～～～～～～～～～～～～～～～～～～～～～\n" +     // 15
        "～～～～～～～～～～～～～～～～～～～～～～～～～～～\n" +     // 16
        "\n";
        // プロパティ
        public static string map
        {
            get { return s_map; }
        }

        static LordName[] s_lordNames = new LordName[]
        {
           new LordName("伊達",    "輝宗", "伊達"),    // LORD_DATE        伊達輝宗
           new LordName("上杉",    "謙信", "上杉"),    // LORD_UESUGI      上杉謙信
           new LordName("武田",    "信玄", "武田"),    // LORD_TAKEDA      武田信玄
           new LordName("北条",    "氏政", "北条"),    // LORD_HOJO        北条氏政
           new LordName("徳川",    "家康", "徳川"),    // LORD_TOKUGAWA    徳川家康
           new LordName("織田",    "信長", "織田"),    // LORD_ODA         織田信長
           new LordName("足利",    "義昭", "足利"),    // LORD_ASHIKAGA    足利義昭
           new LordName("毛利",    "元就", "毛利"),    // LORD_MORI        毛利元就
           new LordName("長宗我部","元親", "長宗"),    // LORD_CHOSOKABE   長宗我部元親
           new LordName("島津",    "義久", "島津"),    // LORD_SIMAZU      島津義久
           new LordName("羽柴",    "秀吉", "羽柴"),    // LORD_HASHIBA     羽柴秀吉
        };
        // プロパティ
        public static LordName[] lordNames
        {
            get { return s_lordNames; }
        }

        static Castle[] s_castles = new Castle[]
        {
            new Castle(
                "米沢城",   // 名前
		        LordId.DATE,  // 城主
		        // 接続された城のリスト
		        new CastleId[]{
                    CastleId.KASUGAYAMA,  // 春日山城
			        CastleId.ODAWARA,     // 小田原城
                },
                45, 2,      // 描画位置
		        "米沢"      // マップ上の名前
                ),
            new Castle(
                "春日山城",     // 名前
		        LordId.UESUGI,    // 城主
		        // 接続された城のリスト
		        new CastleId[]{
                    CastleId.YONEZAWA,        // 米沢城
                    CastleId.TSUTSUJIGASAKI,  // 躑躅ヶ崎館
			        CastleId.GIFU,             // 岐阜城
                },
                37, 3,          // 描画位置
		        "春日"          // マップ上の名前
                ),
            new Castle(
                "躑躅ヶ崎館",   // 名前
		        LordId.TAKEDA,    // 城主
		        // 接続された城のリスト
		        new CastleId[]{
                    CastleId.KASUGAYAMA,  // 春日山城
			        CastleId.ODAWARA,     // 小田原城
                    CastleId.OKAZAKI,     // 岡崎城
                },
                39, 6,          // 描画位置
		        "躑躅"         // マップ上の名前
                ),
            new Castle(
                "小田原城", // 名前
                LordId.HOJO,  // 城主
		        // 接続された城のリスト
		        new CastleId[]{
                    CastleId.YONEZAWA,        // 米沢城
                    CastleId.TSUTSUJIGASAKI,  // 躑躅ヶ崎館
                    CastleId.OKAZAKI,         // 岡崎城
                },
                41, 9,      // 描画位置
		        "小田"     // マップ上の名前
                ),
            new Castle(
                "岡崎城",       // 名前
		        LordId.TOKUGAWA,  // 城主
		        // 接続された城のリスト
		        new CastleId[]{
                    CastleId.TSUTSUJIGASAKI,  // 躑躅ヶ崎館
			        CastleId.ODAWARA,         // 小田原城
			        CastleId.GIFU,            // 岐阜城
                },
                33, 9,          // 描画位置
		        "岡崎"         // マップ上の名前
                ),
            new Castle(     "岐阜城",   // 名前
                LordId.ODA,   // 城主
                // 接続された城のリスト
                new CastleId[]{
                    CastleId.KASUGAYAMA,  // 春日山城
			        CastleId.OKAZAKI,     // 岡崎城
			        CastleId.NIJO,        // 二条城
		        },
                27, 8,      // 描画位置
		        "岐阜"     // マップ上の名前
                ),
            new Castle(
                "二条城",       // 名前
                LordId.ASHIKAGA,  // 城主
                // 接続された城のリスト
                new CastleId[]{
                    CastleId.GIFU,            // 岐阜城
			        CastleId.YOSHIDAKORIYAMA, // 吉田郡山城
			        CastleId.OKO,             // 岡豊城
		        },
                19, 9,          // 描画位置
		        "二条"         // マップ上の名前)
                ),
            new Castle(
                "吉田郡山城",   // 名前
                LordId.MORI,      // 城主
                // 接続された城のリスト
                new CastleId[]{
                    CastleId.NIJO,    // 二条城
			        CastleId.OKO,     // 岡豊城
			        CastleId.UCHI,    // 内城
		        },
                11, 9,          // 描画位置
		        "吉田"         // マップ上の名前
                ),
            new Castle(
                "岡豊城",       // 名前
                LordId.CHOSOKABE, // 城主
                // 接続された城のリスト
                new CastleId[]{
                    CastleId.NIJO,            // 二条城
			        CastleId.YOSHIDAKORIYAMA, // 吉田郡山城
			        CastleId.UCHI,            // 内城
		        },
                13,12,          // 描画位置
		        "岡豊"         // マップ上の名前
                ),
            new Castle(
                "内城",         // 名前
                LordId.SHIMAZU,    // 城主
                //  接続された城のリスト
                new CastleId[]{
                    CastleId.YOSHIDAKORIYAMA, // 吉田郡山城
			        CastleId.OKO,             // 岡豊城
		        },
                 3,14,          // 描画位置
		        "内城"         // マップ上の名前
                ),
        };
        // プロバティ
        public static Castle[] castles 
        {
            get { return s_castles; }
        }

    } // class
} // namespace 