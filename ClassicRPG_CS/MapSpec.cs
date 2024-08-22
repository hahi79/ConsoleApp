//======================================
//      王道RPG マップスペック
//======================================
using System;

namespace ClassicRPG_CS
{
    // マップID
    public enum MapId {
        Field,
        KingCastle,
        BossCastle,
        None,
    }
    public class NextMap
    {
        MapId m_nextId; // マップID
        int m_nextX;      // X座標
        int m_nextY;      // Y座標
                          // コンストラクタ
        public NextMap(MapId id, int x, int y)
        {
            m_nextId = id;
            m_nextX = x;
            m_nextY = y;
        }
        // プロパティ
        public MapId Id
        {
            get { return m_nextId; }
        }
        public int X
        {
            get { return m_nextX; }
        }
        public int Y
        {
            get { return m_nextY; }
        }
    }

    internal class MapSpec
    {
        public const int MAP_WIDTH = 16;
        public const int MAP_HEIGHT = 16;
        public const char outOfMap = '*';

        char[,] m_array = new char[MAP_HEIGHT, MAP_WIDTH]; // マップ
        char m_outData;          // マップ外のデータ
        NextMap m_nextMap_Out;   // マップ出たときの行先
        NextMap m_nextMap_K;     // K 踏んだときの行先
        NextMap m_nextMap_B;     // B 踏んだときの行先
        bool m_isBattleEncount;   // バトルエンカウントするか?

        // プロパティ
        public bool isBattleEncount
        {
            get { return m_isBattleEncount; }
        }
        public char outData
        {
            get { return m_outData; }
        }
        public NextMap nextMap_Out
        {
            get { return m_nextMap_Out; }
        }
        public NextMap nextMap_K
        {
            get { return m_nextMap_K; }
        }
        public NextMap nextMap_B
        {
            get { return m_nextMap_B; }
        }
        // コンストラクター
        public MapSpec(string[] array, char outData, NextMap nextOut, NextMap nextK, NextMap nextB, bool isBattleEncount)
        {
            m_array = CreateArray(array);
            m_outData = outData;
            m_nextMap_Out = nextOut;
            m_nextMap_K = nextK;
            m_nextMap_B = nextB;
            m_isBattleEncount = isBattleEncount;
        }
        protected char[,] CreateArray(string[] array)
        {
            char[,] tmp = new char[MAP_HEIGHT, MAP_WIDTH];
            for(int y=0; y<MAP_HEIGHT; y++)
            {
                string str = (y < array.Length) ? array[y] : "";
                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    tmp[y, x] = (x < str.Length) ? str[x] : '\0';
                }
            }
            return tmp;
        }
        // 指定座標のマップデータ取得
        public char GetMapData(int x,int y)
        {
            if(IsInMap(x, y))
            {
                char c = m_array[y, x];
                if(c != '\0')
                {
                    return c;
                }
            }
            return outOfMap;
        }
        protected bool IsInMap(int x,int y)
        {
            return 0 <= x && x < MAP_WIDTH
                && 0 <= y && y < MAP_HEIGHT;
        }
    } // class
} // namespace
