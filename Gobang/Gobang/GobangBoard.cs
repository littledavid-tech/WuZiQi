using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Gobang
{
    /// <summary>
    /// 五子棋的棋盘
    /// </summary>
    public class GobangBoard
    {
        private static Pen _linePen = new Pen(Color.Black);//绘制棋盘的画笔
        private static SolidBrush _blackBrosh = new SolidBrush(Color.Black);//绘制中心点的笔刷

        private Cell[,] _cells;//落子点
        private int _size;//棋盘的大小

        public GobangBoard(int size)
        {
            _cells = new Cell[size, size];

            this._size = size;

            //初始化棋盘
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _cells[i, j] = new Cell(i + 1, j + 1);
                }
            }
        }


        public void Draw(Graphics g)
        {
            int startOffset = Const.CellOffset / 2;

            //绘制线条
            for (int i = 0; i < this._size; i++)
            {
                //绘制横向的线条
                g.DrawLine(_linePen, new Point(startOffset, i * Const.CellOffset + startOffset), new Point((_size - 1) * Const.CellOffset + startOffset, i * Const.CellOffset + startOffset));
                //绘制纵向的线条
                g.DrawLine(_linePen, new Point(i * Const.CellOffset + startOffset, startOffset), new Point(i * Const.CellOffset + startOffset, (_size - 1) * Const.CellOffset + startOffset));
            }

            //绘制棋盘中心点
            g.FillRectangle(_blackBrosh, (this._size / 2 - 1) * Const.CellOffset - 6 / 2 + startOffset, (this._size / 2 - 1) * Const.CellOffset - 6 / 2 + startOffset, 6, 6);
        }

        /// <summary>
        /// 根据鼠标的坐标获取Cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCellByMousePosition(int x, int y)
        {
            if (Const.IsDebugEnable)
                Console.WriteLine($"X:{x} Y:{y}");

            for (int i = 0; i < this._cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    Cell cell = this._cells[i, j];

                    if (cell.Contain(x, y))
                        return cell;
                }
            }
            return null;
        }

        public bool IsFinish()
        {

            bool isHorizontal;
            bool isVertical;
            bool isRightSlash;
            bool isLeftSlash;

            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    Cell cell = _cells[i, j];
                    if (cell.PieceType == PieceType.Empty)
                    {
                        continue;
                    }

                    isHorizontal = true;
                    isVertical = true;
                    isRightSlash = true;
                    isLeftSlash = true;

                    for (int y = 1; y < 5; y++)
                    {
                        //判断横向的棋子是否满足胜利条件
                        if (i + y >= _cells.GetLength(0) || _cells[i + y, j].PieceType != cell.PieceType)
                        {
                            isHorizontal = false;
                        }
                        //判断纵向的棋子是否满足胜利条件
                        if (j + y >= _cells.GetLength(1) || _cells[i, j + y].PieceType != cell.PieceType)
                        {
                            isVertical = false;
                        }
                        //判断右斜向的棋子是否满足胜利条件
                        if (i + y >= _cells.GetLength(0) ||
                            j + y >= _cells.GetLength(1) ||
                            _cells[i + y, j + y].PieceType != cell.PieceType)
                        {
                            isRightSlash = false;
                        }

                        //判断左斜向的棋子是否满足胜利条件
                        if (i - y < 0 ||
                            j + y >= _cells.GetLength(1) ||
                            _cells[i - y, j + y].PieceType != cell.PieceType
                            )
                        {
                            isLeftSlash = false;
                        }
                    }

                    //如果其中一项满足要求，则游戏结束
                    if (isHorizontal || isVertical || isRightSlash || isLeftSlash)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取已经落子的点的数量
        /// </summary>
        /// <returns></returns>
        public int GetUsedPlace()
        {
            int count = 0;
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j].PieceType != PieceType.Empty)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 判断是否所有的落子点都已经使用了
        /// </summary>
        /// <returns></returns>
        public bool IsAllPlaceUsed()
        {
            return GetUsedPlace() == _size * _size;
        }
    }
}
