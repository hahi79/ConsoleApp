using GP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi_CS
{
    internal class AI
    {
        // CPUプレーヤの打つ場所を得る
        public static Vector2 GetCpuPlayerPosition(Reversi reversi)
        {
            List<Vector2> list = new List<Vector2>(100);
            // 打てるリストからランダムに選ぶ.
            reversi.ListCanPlaceAll(reversi.turn, list);
            int idx = Utility.GetRand(list.Count);
            return list[idx];
        }
    }
}
