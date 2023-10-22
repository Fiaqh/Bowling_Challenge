using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Backend
{
    public class Frame
    {
        public int frameNumber {  get; set; }
        public int shotsInFrame { get; set; }
        public int pinsLeft { get; set; } = GameOptions.numberOfPins;

        public Frame(int frameNumber)
        {
            this.frameNumber = frameNumber;
        }

        public void ResetPins()
        {
            pinsLeft = GameOptions.numberOfPins;
        }
        



    }
}
