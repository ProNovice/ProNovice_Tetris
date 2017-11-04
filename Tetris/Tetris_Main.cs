using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Tetris
{
    public partial class Tetris_Main : Form
    {
        public int[][] nextBlock;
        public Uri bgmLocation;
        public Thread bgmThread;
        public bool bgmOn;


        public Tetris_Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TetrisManager.InitializeTetris();
            Sound.PlayOpening();
        }
        
        private void tmrTetris_Tick(object sender, EventArgs e)
        {
            Run();
            tmrTetris.Interval = (int)TetrisManager.speed;
        }
        
        #region KeyPress

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    MoveBlock(0, -1);
                    break;
                case Keys.Right:
                    MoveBlock(0, 1);
                    break;
                case Keys.Up:
                    RotateBlock();
                    break;
                case Keys.Down:
                    MoveBlock(1, 0);
                    break;
                case Keys.Space:
                    if (TetrisManager.gameStatus != 1)
                        Start();
                    else
                        DropBlock();
                    break;
                case Keys.A:
                    Start();
                    break;
                case Keys.S:
                    bgmOn = false;
                    Pause();
                    break;
                case Keys.D:
                    Stop();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        #endregion


        private void Run()
        {
            //prevent duplication of execution
            tmrTetris.Enabled = false;
            Graphics nextBlockBoard = pnlNextBlock.CreateGraphics();
            Graphics tetrisBoard = pnlTetris.CreateGraphics();
            switch (TetrisManager.gameStatus)
            {
                //default
                case 0:
                    tetrisBoard.DrawImage(Picture.start, 50, 200, 200, 200);
                    ShowStatus();
                    break;
                //running
                case 1:
                    TetrisManager.ProgressTetris();
                    DisplayTetris();
                    ShowStatus();
                    if(nextBlock != TetrisManager.nextBlock.Pattern)
                        DisplayNextBlock();
                    tmrTetris.Enabled = true;   //continue Tetris
                    break;
                //pausing
                case 2:
                    break;
                //gameover
                case 3:
                    tetrisBoard.DrawImage(Picture.gameOver, 50, 220, 200, 150);
                    break;
                default:
                    TetrisManager.gameStatus = 0;
                    break;
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void ShowStatus()
        {
            lblLevel.Text = TetrisManager.level.ToString();
            lblScore.Text = TetrisManager.score.ToString();
            lblHighstScore.Text = TetrisManager.highstScore.ToString();
            lblTurn.Text = TetrisManager.turn.ToString();
            lblElapsedTime.Text = TetrisManager.stopWatch.Elapsed.ToString("mm\\:ss");
        }

        private void Start()
        {
            switch (TetrisManager.gameStatus)
            {
                //from default
                case 0:
                    Sound.PlayRunning();
                    TetrisManager.StartTetris();
                    DisplayNextBlock();
                    break;
                //from pause
                case 2:
                    TetrisManager.RestartTetris();
                    DisplayTetris();
                    DisplayNextBlock();
                    break;
                //from gameover
                case 3:
                    TetrisManager.StartTetris();
                    DisplayNextBlock();
                    break;
            }
            tmrTetris.Enabled = true;
        }

        private void Pause()
        {
            if (TetrisManager.gameStatus == 1)   //only works when the Tetris was running
            {
                Graphics tetrisBoard = pnlTetris.CreateGraphics();
                tetrisBoard.DrawImage(Picture.pause, 50, 220, 200, 150);
                TetrisManager.PauseTetris();
                tmrTetris.Enabled = false;
            }
        }

        private void Stop()
        {
            Sound.PlayOpening();
            TetrisManager.StopTetris();
            tmrTetris.Enabled = false;
            Graphics tetrisBoard = pnlTetris.CreateGraphics();
            tetrisBoard.Clear(Color.Black);
            tetrisBoard.DrawImage(Picture.start, 50, 200, 200, 200);
        }
        
        private void DisplayTetris()
        {
            int unit = TetrisManager.unit;
            int rows = TetrisManager.rows;
            int cols = TetrisManager.cols;
            int[][] pattern = TetrisManager.displayed_Board;
            Bitmap img = new Bitmap(unit * 10, unit * 20);
            Graphics graphics = Graphics.FromImage(img);
            Pen blackPen = new Pen(Color.Black, 600);
            graphics.DrawRectangle(blackPen, 0, 0, unit * 10, unit * 20);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int element = pattern[r][c];
                    DisplayElement(graphics, element, unit, c, r);
                }
            }
            
            Graphics tetrisBoard = pnlTetris.CreateGraphics();
            tetrisBoard.DrawImage(img, 0, 0);            
        }

        private void DisplayNextBlock()
        {
            nextBlock = TetrisManager.nextBlock.Pattern;
            Graphics board = pnlNextBlock.CreateGraphics();
            board.Clear(Color.Black);
            int unit = TetrisManager.unit * 5 / 6;
            int rows = nextBlock.Count();
            int cols = nextBlock[0].Count();
            int topPadding = 1;
            int leftPadding = 1;
            int pnlWidth = pnlNextBlock.Width;
            int pnlHeight = pnlNextBlock.Height;
            
            //to place image centre
            leftPadding = ((pnlWidth - cols * unit) / 2);
            topPadding = ((pnlHeight - rows * unit) / 2);

            Bitmap img = new Bitmap(pnlWidth, pnlHeight);
            Graphics graphics = Graphics.FromImage(img);
            Pen blackPen = new Pen(Color.Black, 600);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int element = nextBlock[r][c];
                    DisplayElement(graphics, element, unit, c, r);
                }
            }

            Graphics nbGraphics = pnlNextBlock.CreateGraphics();
            nbGraphics.DrawRectangle(blackPen, 0, 0, pnlWidth, pnlHeight);
            nbGraphics.DrawImage(img, leftPadding, topPadding);

        }

        private void DisplayElement(Graphics board, int element, int unit, int x, int y)
        {
            switch (element)
            {
                //grey
                case -1:
                    board.DrawImage(Picture.grey_block, unit * x, unit * y, unit, unit);
                    break;
                //void
                case 0:
                    break;
                //red
                case 1:
                    board.DrawImage(Picture.red_block, unit * x, unit * y, unit, unit);
                    break;
                //orange
                case 2:
                    board.DrawImage(Picture.orange_block, unit * x, unit * y, unit, unit);
                    break;
                //yellow
                case 3:
                    board.DrawImage(Picture.yellow_block, unit * x, unit * y, unit, unit);
                    break;
                //green
                case 4:
                    board.DrawImage(Picture.green_block, unit * x, unit * y, unit, unit);
                    break;
                //skyblue
                case 5:
                    board.DrawImage(Picture.skyblue_block, unit * x, unit * y, unit, unit);
                    break;
                //blue
                case 6:
                    board.DrawImage(Picture.blue_block, unit * x, unit * y, unit, unit);
                    break;
                //purple
                case 7:
                    board.DrawImage(Picture.purple_block, unit * x, unit * y, unit, unit);
                    break;
                default:
                    break;
            }
        }

        //image

        /// <summary>
        /// Time Delay
        /// </summary>
        /// <param name="MS"></param>
        /// <returns></returns>
        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        private void MoveBlock(int row, int col)
        {
            if (TetrisManager.gameStatus == 1)
            {
                TetrisManager.MoveBlock(row, col);
                DisplayTetris();
            }
        }

        private void RotateBlock()
        {
            TetrisManager.RotateBlock();
            DisplayTetris();
        }


        private void DropBlock()
        {
            if (TetrisManager.gameStatus == 1)
            {
                TetrisManager.DropBlock();
                DisplayTetris();
                
                new Thread(() => {
                    var c = new System.Windows.Media.MediaPlayer();
                    c.Open(new Uri(@"sounds/blockLanding.wav", UriKind.Relative));
                    c.Play();
                }).Start();
            }
        }

    }
}
