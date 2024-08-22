//======================================
//      王道RPG マップデータ
//======================================
using System;

namespace ClassicRPG_CS
{
    static class MapData
    {
        static string[] s_mapField = new string[]
        {
            "~~~~~~~~~~~~~~~~",
            "~~MMMMM~~MMMM.~~",
            "~M...M.##..M...~",
            "~M.M.M.~~M.M.M.~",
            "~M.M...~~M...M.~",
            "~M.MMMM~~MMMM..~",
            "~M..MM.~~~~~~#~~",
            "~~M.M.~~~~~~~#~~",
            "~~M.MM~~~~BMM..~",
            "~~...MM~~M.MMM.~",
            "~...~~M~~M...M.~",
            "~..~~~K~~MMM.M.~",
            "~..~~~.~~M...M.~",
            "~......~~M.MM..~",
            "~~....~~~~....~~",
            "~~~~~~~~~~~~~~~~"
        };
        static string[] s_mapKingCastle = new string[]
        {
            "HHH.......HHH",
            "H.H.......H.H",
            "HHHHHHHHHHHHH",
            ".H.........H.",
            ".H.HHH.HHH.H.",
            ".H.H0H.H1H.H.",
            ".H.........H.",
            ".HW.......WH.",
            ".HY.......YH.",
            "HHHHHH.HHHHHH",
            "H.H~~~#~~~H.H",
            "HHH~~~#~~~HHH",
            ".............."
        };
        static string[] s_mapBossCastle = new string[]
        {
            "HHH.......HHH",
            "H.H.......H.H",
            "HHHHHHHHHHHHH",
            ".H....H....H.",
            ".H..WHHHW..H.",
            ".H..YH2HY..H.",
            ".H.........H.",
            ".H..W...W..H.",
            ".H..Y...Y..H.",
            ".H.........H.",
            "HHHHHH.HHHHHH",
            "H.H~~~#~~~H.H",
            "HHH~~~#~~~HHH",
            "~~~~~~#~~~~~~",
            "~~~~~~#~~~~~~",
            "............."
        };

        static NextMap s_nextMapNone = new NextMap(MapId.None, 0, 0);

        // フィールドマップ
        static MapSpec specField = new MapSpec(
            s_mapField,
            '~',
            s_nextMapNone,
            new NextMap(MapId.KingCastle, 6, 12),
            new NextMap(MapId.BossCastle, 6, 15),
            true
            );
        // 王の城マップ
        static MapSpec specKingCastle = new MapSpec(
            s_mapKingCastle,
            '.',
            new NextMap(MapId.Field, 6, 12),
            s_nextMapNone,
            s_nextMapNone,
            false
            );
        //　魔王の城マップ
        static MapSpec specBossCastle = new MapSpec(
            s_mapBossCastle,
            '.',
            new NextMap(MapId.Field, 10, 9),
            s_nextMapNone,
            s_nextMapNone,
            false
            );
        // マップスペックを取得
        public static MapSpec GetMapSpec(MapId id)
        {
            switch (id)
            {
                default:
                case MapId.Field: return specField;
                case MapId.KingCastle: return specKingCastle;
                case MapId.BossCastle: return specBossCastle;
            }
        }
        // マップのAA(アスキーアート)を取得
        public static string GetMapAA(char c)
        {
            switch (c)
            {
                case '~': return "～";  // 海
                case '.': return "．";  // 平地
                case 'M': return "Ｍ";  // 山
                case '#': return "＃";  // 橋
                case 'K': return "王";  // 王様の城
                case 'B': return "魔";  // 魔王の城
                case 'H': return "□";  // 壁
                case 'W': return "炎";  // 炎
                case 'Y': return "Ｙ";  // 燭台
                case '0': return "王";  // 王
                case '1': return "姫";  // 姫
                case '2': return "魔";  // 魔王
            }
            //   assert(false);
            return "";
        }
    } // class
} // namespace
