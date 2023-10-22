using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Backend
{
    public class Ball
    {
        public int knockedPins { get; set; } = 0;
        public bool isSpare { get; set; } = false;
        public bool isStrike { get; set; } = false;
        public Frame frame { get; set; }

        public Ball(Frame frame)
        {
            this.frame = frame;
        }

        public void Roll(int pinsHit)
        {

            if (pinsHit > frame.pinsLeft) throw new Exception("input is higher than number of pins left in frame");
            if (pinsHit < 0) throw new Exception("Input is lower than number of pins left in frame");

            knockedPins = pinsHit;
            //We check if we knocked all pins on the first hit, if so we have hit a strike. If we have knocked all pins not on the first throw
            //We have hit a spare unless we are in the last frame. However pointwise the second last shot yields the same amount of points independent
            //of it being a strike or spare, so we can treat the two cases identically.
            if (pinsHit == GameOptions.numberOfPins && frame.shotsInFrame == 0) isStrike = true;
            else if (pinsHit == frame.pinsLeft) isSpare = true;

            //Update the information of the frame. Important that this happens after the spare check!
            frame.pinsLeft -= pinsHit;
            frame.shotsInFrame++;

        }
    }
}
