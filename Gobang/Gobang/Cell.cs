using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gobang
{
    public class Cell
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public Cell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        /// <summary>
        /// 棋子的类型
        /// </summary>
        public PieceType PieceType { get; set; }

        private static SolidBrush _blackBrush = new SolidBrush(Color.Black);
        private static SolidBrush _whiteBrush = new SolidBrush(Color.White);

        /// <summary>
        /// 指定的坐标是否是指定的
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contain(int x, int y)
        {
            Point point = this.GetRealXY();
            int cellX = point.X;
            int cellY = point.Y;

            //在同一个X的有效范围内
            if (cellX - Const.ValidRadius <= x && cellX + Const.ValidRadius >= x)
            {
                if (cellY - Const.ValidRadius <= y && cellY + Const.ValidRadius >= y)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(Graphics g)
        {
            if (PieceType != PieceType.Empty)
            {
                SolidBrush sb = PieceType == PieceType.Black ? _blackBrush : _whiteBrush;

                Point point = GetRealXY();
                int x = point.X - Const.PieceRadius;
                int y = point.Y - Const.PieceRadius;

                g.FillEllipse(sb, x, y, Const.PieceRadius * 2, Const.PieceRadius * 2);
            }
        }

        public override string ToString()
        {
            return $"Row:{this.Row} Column{this.Column}";
        }

        /// <summary>
        /// 获取真实的坐标
        /// </summary>
        /// <returns></returns>
        private Point GetRealXY()
        {
            int cellX = (this.Column - 1) * Const.CellOffset + Const.CellOffset / 2;
            int cellY = (this.Row - 1) * Const.CellOffset + Const.CellOffset / 2;
            return new Point(cellX, cellY);
        }
    }
}
