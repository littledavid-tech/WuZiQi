using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gobang
{
    public partial class FrmMain : FrmModel
    {
        GobangBoard _board;
        Graphics _g;
        Bitmap _bitmap;

        bool _isWhite = true;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            DoInit();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void DoInit()
        {
            _isWhite = true;
            if (_g != null)
            {
                _g.Dispose();
            }
            if (_bitmap != null)
            {
                _bitmap.Dispose();
            }

            this._board = new GobangBoard(16);
            _bitmap = new Bitmap(this.pb.Width, this.pb.Height);
            _g = Graphics.FromImage(_bitmap);
            this._board.Draw(_g);

            this.pb.Image = _bitmap;
        }

        private void pb_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Cell cell = this._board.GetCellByMousePosition(e.X, e.Y);
            if (cell != null)
            {
                PutPiece(cell);
            }
            else
            {
                if (Const.IsDebugEnable)
                    Console.WriteLine("Null Cell");
            }
        }

        /// <summary>
        /// 落子
        /// </summary>
        /// <param name="cell"></param>
        private void PutPiece(Cell cell)
        {
            if (Const.IsDebugEnable)
                Console.WriteLine(cell);

            if (cell.PieceType != PieceType.Empty)
            {
                return;
            }

            cell.PieceType = _isWhite ? PieceType.White : PieceType.Black;
            //绘制棋子
            cell.Draw(this._g);
            this.pb.Image = _bitmap;

            if (_board.IsFinish())
            {
                this.FinishGame();
            }
            else if (_board.IsAllPlaceUsed())
            {
                this.Deuce();
            }
            else
            {
                //切换棋手
                this._isWhite = !_isWhite;
            }
        }

        /// <summary>
        /// 结束游戏
        /// </summary>
        private void FinishGame()
        {
            if (_isWhite)
            {
                MessageBox.Show("白棋胜利");
            }
            else
            {
                MessageBox.Show("黑棋胜利");
            }

            StartAgain();
        }

        private void StartAgain()
        {
            if (MessageBox.Show("再来一局?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DoInit();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 平局
        /// </summary>
        private void Deuce()
        {
            MessageBox.Show("平局");
            this.StartAgain();
        }
    }
}
