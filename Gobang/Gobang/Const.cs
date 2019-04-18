using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gobang
{
    public class Const
    {
        public const bool IsDebugEnable = true;

        /// <summary>
        /// 网格之间的偏移量
        /// </summary>
        public static int CellOffset = 30;

        /// <summary>
        /// 落子点有效半径
        /// </summary>
        public static int ValidRadius = 10;

        /// <summary>
        /// 棋子的半径
        /// </summary>
        public static int PieceRadius = CellOffset / 2 - 3;

    }
}
