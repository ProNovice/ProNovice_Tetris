using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Tetris
{
    class Sound
    {
        public static string PlayOpening()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/Opening.wav";
            sound.Play();
            return sound.SoundLocation;
        }
        public static void PlayStart()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/Starting.wav";
            sound.Play();
        }
        public static void PlayRunning()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/running.wav";
            sound.Play();
        }
        public static void PlayVictory()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/Victory.wav";
            sound.Play();
        }
        public static void PlayGameOver()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/GameOver.wav";
            sound.Play();
        }
        public static void PlayLevelup()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/Levelup.wav";
            sound.Play();
        }
        public static void PlayItemGetting()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/ItemGetting.wav";
            sound.Play();
        }
        public static void PlayItemUsing()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/ItemUsing.wav";
            sound.Play();
        }
        public static void PlaySlowHeartBeat()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/SlowHeartBeat.wav";
            sound.Play();
        }
        public static void PlayFastHeartBeat()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/FastHeartBea.wav";
            sound.Play();
        }
        public static void PlayBlockMoving()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/BlockMoving.wav";
            sound.Play();
        }
        public static void PlayBlockLanding()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/BlockLanding.wav";
            sound.Play();
        }
        public static void PlayBlockRemoving()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/BlockRemoving.wav";
            sound.Play();
        }
        public static void PlayNyangCat()
        {
            SoundPlayer sound = new SoundPlayer();
            sound.SoundLocation = "sounds/NyangCat.wav";
            sound.Play();
        }
    }
}
