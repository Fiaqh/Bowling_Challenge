using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Backend
{
    public class Player
    {
        public string name { get; set; }
        public Score score;
        public List<Ball> rolledBalls;
        public List<Frame> frames;
        public int currentFrameNumber { get; set; }

        public Player(string name)
        {
            this.name = name;
            rolledBalls = new List<Ball>();
            score = new Score(rolledBalls);
            frames = new List<Frame>();

            for (int frameNumber = 0; frameNumber < GameOptions.numberOfFrames; frameNumber++)
            {
                frames.Add(new Frame(frameNumber));
            }
        }

        private void UpdateScore()
        {
            score.rolls = rolledBalls;
            score.UpdateTotalScore();
        }

        //We update the currentFrameNumber by looking at the last ball that was thrown by the Player. Depending on the result of the ball and the frame it
        //was thrown at we update the currentFrameNumber accordingly
        private void updateCurrentFrameNumber()
        {
            if (rolledBalls.Count == 0) currentFrameNumber = 0;

            else
            {
                switch(rolledBalls[rolledBalls.Count - 1].frame.shotsInFrame)
                {
                    case 0:
                        currentFrameNumber = rolledBalls[rolledBalls.Count - 1].frame.frameNumber;
                        break;

                    case 1:
                        currentFrameNumber = GetCurrentFrameIfOneThrowInFrame();
                        break;

                    case 2:
                        currentFrameNumber = GetCurrentFrameIfTwoThrowsInFrame();
                        break;
                    //If we have ever thrown 3 times in a frame the frame is over.
                    case 3:
                        currentFrameNumber = rolledBalls[rolledBalls.Count - 1].frame.frameNumber + 1;
                        break;
                    default:
                        break;
                }
            }
        }

        private int GetCurrentFrameIfOneThrowInFrame()
        {
            //If we have only thrown once on a frame we check if we have hit a strike or not. If we did hit a strike and are not on the final frame currentFrame is the next frame.
            //If we have not hit a strike we still have a roll left in the frame so this is the currentFrame
            Ball lastBall = rolledBalls.Last();
            if (lastBall.isStrike)
            {
                if (lastBall.frame.frameNumber == GameOptions.numberOfFrames - 1) return lastBall.frame.frameNumber;
                else return lastBall.frame.frameNumber + 1;
            }
            else return lastBall.frame.frameNumber;
        }

        private int GetCurrentFrameIfTwoThrowsInFrame()
        {
            Ball lastBall = rolledBalls.Last();
            Ball secondLastBall = rolledBalls[rolledBalls.Count - 2];
            //If we have thrown 2 balls on a frame without hitting a spare or a strike we progress to the next frame
            if (!lastBall.isSpare && !lastBall.isStrike)
            {
                if (secondLastBall.isStrike) return lastBall.frame.frameNumber;
                else return lastBall.frame.frameNumber + 1;
            }
            //If we have thrown 2 balls and either is strike or spare we check if we are on the final frame
            else
            {
                if (lastBall.frame.frameNumber == GameOptions.numberOfFrames - 1) return lastBall.frame.frameNumber;
                else return lastBall.frame.frameNumber + 1;
            }
        }

        //Makes a new ball, rolls it and adds the roll to the list of rolledBalls. Then updates the score and currentFrameNumber
        public void RollBall(int pinsHit)
        {
            Ball ball = new Ball(frames[currentFrameNumber]);
            ball.Roll(pinsHit);
            rolledBalls.Add(ball);
            if(currentFrameNumber==GameOptions.numberOfFrames-1)
            {
                //If we hit a strike or spare or the last frame we reset the pins on this frame, so we can potentially roll again on this frame
                if(ball.isStrike || ball.isSpare)
                {
                    frames[currentFrameNumber].ResetPins();
                }
            }
            //We then update the score and currentFrameNumber
            UpdateScore();
            updateCurrentFrameNumber();
        }

        //Returns the balls a player has rolled in a frame given the frameNumber.
        public List<Ball> GetRolledBallsInFrame(int frameNumber)
        {
            List<Ball> ballsInFrame = new List<Ball>();
            foreach (Ball ball in rolledBalls)
            {
                if (ball.frame.frameNumber == frameNumber) ballsInFrame.Add(ball);
            }
            return ballsInFrame;
        }
    }
}
