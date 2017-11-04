using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Block
    {
        public int[][] Pattern { get; set; }

        public Block(int[][] pattern)
        {
            this.Pattern = pattern;
        }

        public Block()
        { }

        public static int[][] GetDefaultPattern(int row, int col)
        {
            int[][] pattern = new int[row][];
            for (int r = 0; r < row; r++)
            {
                int[] rowArray = new int[col];
                pattern[r] = rowArray;
            }
            return pattern;
        }

        public static int[][] RotatePattern(int[][] pattern)
        {
            //standard of result
            int row = pattern[0].Length;
            int col = pattern.Length;
            //set result
            int[][] result = new int[row][];    // when the pattern is rotated, col become row, row become col
            for (int i = 0; i < row; i++)
            {
                int[] array = new int[col];
                result[i] = array;
            }
            //rotate right
            for (int rBase = 0, cResult = col - 1; rBase < col; rBase++, cResult--)
            {
                for (int cBase = 0, rResult = 0; cBase < row; cBase++, rResult++)
                {
                    result[rResult][cResult] = pattern[rBase][cBase];
                }
            }
            return result;
        }

        public static void PrintPattern(int[][] pattern)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                for (int j = 0; j < pattern[i].Length; j++)
                {
                    int number = pattern[i][j];
                    Console.Write("{0} ", number);
                }
                Console.WriteLine("");
            }
        }

        public static List<Block> GetBasicBlockList()
        {
            List<Block> blockList = new List<Block>();
            blockList.Add(Get_I_Block());
            blockList.Add(Get_J_Block());
            blockList.Add(Get_L_Block());
            blockList.Add(Get_O_Block());
            blockList.Add(Get_S_Block());
            blockList.Add(Get_T_Block());
            blockList.Add(Get_Z_Block());
            return blockList;
        }

        #region Basic Blocks
        public static Block Get_I_Block()
        {
            int[][] pattern = GetDefaultPattern(4, 4);
            int[] row0 = { 0, 1, 0, 0 };
            int[] row1 = { 0, 1, 0, 0 };
            int[] row2 = { 0, 1, 0, 0 };
            int[] row3 = { 0, 1, 0, 0 };
            pattern[0] = row0;
            pattern[1] = row1;
            pattern[2] = row2;
            pattern[3] = row3;
            Block result = new Block(pattern);
            return result;
        }
        public static Block Get_J_Block()
        {
            int[][] pattern = GetDefaultPattern(3, 3);
            int[] row0 = { 0, 7, 0 };
            int[] row1 = { 0, 7, 0 };
            int[] row2 = { 7, 7, 0 };
            pattern[0] = row0;
            pattern[1] = row1;
            pattern[2] = row2;
            Block result = new Block(pattern);
            return result;
        }
        public static Block Get_L_Block()
        {
            int[][] pattern = GetDefaultPattern(3, 3);
            int[] row0 = { 0, 2, 0 };
            int[] row1 = { 0, 2, 0 };
            int[] row2 = { 0, 2, 2 };
            pattern[0] = row0;
            pattern[1] = row1;
            pattern[2] = row2;
            Block result = new Block(pattern);
            return result;
        }
        public static Block Get_O_Block()
        {
            int[][] pattern = GetDefaultPattern(2, 2);
            int[] row0 = { 3, 3 };
            int[] row1 = { 3, 3 };
            pattern[0] = row0;
            pattern[1] = row1;
            Block result = new Block(pattern);
            return result;
        }
        public static Block Get_S_Block()
        {
            int[][] pattern = GetDefaultPattern(3, 3);
            int[] row0 = { 0, 4, 4 };
            int[] row1 = { 4, 4, 0 };
            int[] row2 = { 0, 0, 0 };
            pattern[0] = row0;
            pattern[1] = row1;
            pattern[2] = row2;
            Block result = new Block(pattern);
            return result;
        }
        public static Block Get_Z_Block()
        {
            int[][] pattern = GetDefaultPattern(3, 3);
            int[] row0 = { 5, 5, 0 };
            int[] row1 = { 0, 5, 5 };
            int[] row2 = { 0, 0, 0 };
            pattern[0] = row0;
            pattern[1] = row1;
            pattern[2] = row2;
            Block result = new Block(pattern);
            return result;
        }
        public static Block Get_T_Block()
        {
            int[][] pattern = GetDefaultPattern(3, 3);
            int[] row0 = { 6, 6, 6 };
            int[] row1 = { 0, 6, 0 };
            int[] row2 = { 0, 0, 0 };
            pattern[0] = row0;
            pattern[1] = row1;
            pattern[2] = row2;
            Block result = new Block(pattern);
            return result;
        }

        #endregion

    }
}
