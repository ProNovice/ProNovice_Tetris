using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Picture
    {
        public static Image red_block = Image.FromFile("images/red_block.jpg");
        public static Image orange_block = Image.FromFile("images/orange_block.jpg");
        public static Image yellow_block = Image.FromFile("images/yellow_block.jpg");
        public static Image green_block = Image.FromFile("images/green_block.jpg");
        public static Image skyblue_block = Image.FromFile("images/skyblue_block.jpg");
        public static Image blue_block = Image.FromFile("images/blue_block.jpg");
        public static Image purple_block = Image.FromFile("images/purple_block.jpg");
        public static Image grey_block = Image.FromFile("images/grey_block.jpg");
        public static Image start = Image.FromFile("images/start.png");
        public static Image pause = Image.FromFile("images/pause.png");
        public static Image gameOver = Image.FromFile("images/gameOver.png");
    }
}
