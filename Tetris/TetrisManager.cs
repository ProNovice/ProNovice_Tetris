using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class TetrisManager
    {
        //system
        public static int highstScore;
        public static double speed = 1;    // 0.8 second

        //messure
        public static int rows = 20;
        public static int cols = 11;
        public static int unit = 30;    // 30px

        //rule
        public static int turnLimit = -1;   // -1: no limit, 100: limit is 100
        public static int timeLimit = -1;   // -1: no limit
        public static bool usingGreyLine = true;
        public static int greyLineStartTime = 60;
        public static int greyLineCoolTime = 20;
        public static int greyLineCount = 0;
        public static bool usingItem = false;

        //status
        public static int gameStatus;   // 0: default, 1: running, 2: pause, 3: gameover
        public static int level;
        public static int turn;
        public static int score;
        public static int scoreUnit;
        public static int scoreRate;
        public static int combo;
        public static Stopwatch stopWatch;

        //board
        public static int[][] basic_Board;
        public static int[][] displayed_Board;

        //block
        public static List<Block> blockList;
        public static Block crtBlock;
        public static Block nextBlock;
        public static int xPos;
        public static int yPos;

        public static void ResetAll()
        {
            //system
            gameStatus = 0;   // 0: default, 1: running, 2: pause
            level = 1;
            stopWatch = new Stopwatch();
            turn = 0;
            score = 0;
            scoreUnit = 100;
            scoreRate = 1;
            combo = 0;
            highstScore = 0;
            speed = 600;    // 0.5 second

            //messure
            rows = 20;
            cols = 10;
            unit = 30;

            //rule
            turnLimit = -1;   // -1: no limit, 100: limit is 100
            timeLimit = -1;   // -1: no limit           
        }

        //initialize
        public static void InitializeTetris()
        {
            //status
            gameStatus = 0;   // 0: default, 1: running, 2: pause
            level = 1;
            turn = 0;
            score = 0;
            scoreUnit = 100;
            scoreRate = 1;
            combo = 0;
            stopWatch = new Stopwatch();
            greyLineCount = 0;

            //board
            basic_Board = GetDefaultBoard(rows, cols);
            displayed_Board = GetDefaultBoard(rows, cols);

            //block
            crtBlock = null;
            nextBlock = null;
            xPos = 0;
            yPos = 0;
        }

        //start
        public static void StartTetris()
        {
            InitializeTetris();
            if(blockList == null)
                blockList = Block.GetBasicBlockList();
            crtBlock = PickRandomBlock();
            nextBlock = PickRandomBlock();
            xPos = (cols - crtBlock.Pattern[0].Count()) / 2;
            yPos = 0;
            stopWatch = new Stopwatch();
            stopWatch.Start();

            gameStatus = 1;
        }

        //restart from pause
        public static void RestartTetris()
        {
            stopWatch.Start();
            gameStatus = 1;
        }

        //pause
        public static void PauseTetris()
        {
            stopWatch.Stop();
            gameStatus = 2;
        }

        //stop
        public static void StopTetris()
        {
            InitializeTetris();
            gameStatus = 0;
        }

        //gameover
        public static void GameOver()
        {
            gameStatus = 3;
        }

        //progress
        public static void ProgressTetris()
        {
            //check if there is a block
            if (crtBlock == null)
            {
                if (nextBlock == null)
                    nextBlock = PickRandomBlock();
                NextBlock();
            }
            //progress
            int blkRows = crtBlock.Pattern.Count();
            int blkCols = crtBlock.Pattern[0].Count();

            level = turn / 20 + 1;
            speed = 600 / (0.9 + 0.1 * level);


            if (usingGreyLine)
            {
                if (stopWatch.Elapsed.Seconds >= greyLineStartTime + greyLineCount * greyLineCoolTime)
                {
                    IncreaseGreyLine();
                    greyLineCount++;
                }
            }


            if(stopWatch.IsRunning)
            MoveBlock(1, 0);            
        }

        public static void NextBlock()
        {
            if (blockList == null)
                blockList = Block.GetBasicBlockList();
            if (nextBlock == null)
            {
                nextBlock = PickRandomBlock();
            }
            crtBlock = nextBlock;
            nextBlock = PickRandomBlock();
            xPos = (cols - crtBlock.Pattern[0].Count()) / 2;
            yPos = 0;
            turn++;
        }

        public static Block PickRandomBlock()
        {
            if (blockList.Count() == 0)
                blockList = Block.GetBasicBlockList();
            Random random = new Random();
            int randomInt = random.Next(0, blockList.Count() - 1);
            Block block = new Block();
            block.Pattern= CopyPattern(blockList[randomInt].Pattern);
            blockList[randomInt] = blockList[blockList.Count() - 1];
            blockList.RemoveAt(blockList.Count() - 1);
            return block;
        }
        

        //Board
        public static int[][] GetDefaultBoard(int row, int col)
        {
            int[][] pattern = new int[row][];
            for (int r = 0; r < row; r++)
            {
                int[] rowArray = new int[col];
                for (int c = 0; c < col; c++)
                    rowArray[c] = 0;
                pattern[r] = rowArray;
            }
            return pattern;
        }


        #region Move Block

        //check collision
        public static bool CheckCollision(int[][] pattern, int verticalMove, int horizontalMove)
        {
            bool collision = false;
            int y = yPos + verticalMove;
            int x = xPos + horizontalMove;
            int blkRows = pattern.Count();
            int blkCols = pattern[0].Count();
            int[][] temp_board = CopyPattern(basic_Board);
            for (int row = y, blkRow = blkRows - 1; row >= 0 && blkRow >= 0; row--, blkRow--)
            {
                for (int col = x, blkCol = 0; col < cols && blkCol < blkCols; col++, blkCol++)
                {
                    int blkElement = pattern[blkRow][blkCol];
                    if (blkElement != 0)
                    {
                        //collision with side walls
                        if (((col < 0) || (col > cols - 2)) || (row > rows - 1 || basic_Board[row][col] != 0))
                        {
                            collision = true;
                            break;
                        }
                        //collision with bottom
                        else if ((row > rows - 1 || basic_Board[row][col] != 0))
                        {
                            collision = true;
                            break;
                        }
                    }
                }
                if (collision)
                    break;
            }
            return collision;
        }

        //move block    
        public static void MoveBlock(int verticalMove, int horizontalMove)
        {
            bool collision = false;
            bool bottom_Collision = false;
            int y = yPos + verticalMove;
            int x = xPos + horizontalMove;
            int blkRows = crtBlock.Pattern.Count();
            int blkCols = crtBlock.Pattern[0].Count();
            int[][] temp_board = CopyPattern(basic_Board);
            for (int row = y, blkRow = blkRows - 1; row >= 0 && blkRow >= 0; row--, blkRow--)
            {
                for (int col = x, blkCol = 0; col < cols && blkCol < blkCols; col++, blkCol++)
                {
                    int blkElement = crtBlock.Pattern[blkRow][blkCol];
                    if (blkElement != 0)
                    {
                        //collision with side walls
                        if (((col < 0) || (col > cols - 2)))
                        {
                            collision = true;
                            break;
                        }
                        //collision at bottom
                        else if ((row > rows - 1 || basic_Board[row][col] != 0) && verticalMove != 0)
                        {
                            if (row == 0)
                                GameOver();
                            else
                                bottom_Collision = true;
                            collision = true;
                            break;
                        }
                        //collision with side blocks
                        else if (basic_Board[row][col] != 0 && horizontalMove != 0)
                        {
                            collision = true;
                            break;
                        }
                        //in range
                        else if (row >= 0 && row < rows && col >= 0 && col < cols)
                        {
                            temp_board[row][col] = crtBlock.Pattern[blkRow][blkCol];
                        }
                    }
                }
                if (collision)
                    break;
            }
            //bottom collision
            if (bottom_Collision)
            {
                basic_Board = CopyPattern(displayed_Board);
                ClearFullLines();
                NextBlock();
            }
            //no accident
            else if (!collision && !bottom_Collision)
            {
                yPos += verticalMove;
                xPos += horizontalMove;
                displayed_Board = CopyPattern(temp_board);
            }
        }

        public static void DropBlock()
        {
            //temporary code
            if (yPos >= 1)
            {
                int startTurn = turn;
                while (startTurn == turn)
                {
                    MoveBlock(1, 0);
                }
            }
        }

        public static void RotateBlock()
        {
            bool collision = CheckCollision(Block.RotatePattern(crtBlock.Pattern), 0, 0);
            crtBlock.Pattern = !collision ? Block.RotatePattern(crtBlock.Pattern) : crtBlock.Pattern;
        }
        

        #endregion

        #region Line



        public static void ClearFullLines()
        {
            for (int r = 0; r < rows; r++)
            {
                bool isFullLine = true;

                for (int c = 0; c < cols - 1; c++)  // temporary code "c < cols - 1"
                {
                    if (basic_Board[r][c] == 0 || basic_Board[r][c] == -1)
                    {
                        isFullLine = false;
                        break;
                    }
                }

                if (isFullLine)
                {
                    ClearLine(r);
                    IncreaseScore();
                    r--;
                }
            }
        }

        public static void ClearLine(int row)
        {
            if(row > 0)
            {
                for (int r = row; r > 0; r--)
                {
                    Array.Copy(basic_Board[r - 1], basic_Board[r], cols);
                }
                basic_Board[0] = new int[cols];
            }
        }

        public static void IncreaseScore()
        {
            int comboRate = combo / 10 + 1;
            score += scoreUnit * scoreRate * comboRate;
            highstScore = highstScore < score ? score : highstScore;
        }
        public static void IncreaseScore(int point)
        {
            score += point;
            highstScore = highstScore < score ? score : highstScore;
        }

        public static void IncreaseGreyLine()
        {
            for (int r = 0; r < rows - 1; r++)
            {
                Array.Copy(basic_Board[r + 1], basic_Board[r], cols);
            }
            basic_Board[rows - 1] = new int[cols];
            for (int c = 0; c < cols; c++)
            {
                basic_Board[rows - 1][c] = -1;
            }            
        }
        #endregion



        public static int[][] CopyPattern(int[][] pattern)
        {
            int rows = pattern.Count();
            int cols = pattern[0].Count();
            int[][] result = GetDefaultBoard(rows, cols);
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    result[r][c] = pattern[r][c];
                }
            }
            return result;
        }

    }
}
