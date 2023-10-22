using Bowling_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_backend_unittest
{
    public class ScoreTests
    {
        [Fact]
        public void IfAllStrikesGivesCorrectScore()
        {
            Player player= new Player("test");
            for(int i = 0; i<GameOptions.numberOfFrames+2;i++)
            {
                player.RollBall(GameOptions.numberOfPins);
            }
            int perfectScore =3 * GameOptions.numberOfFrames * GameOptions.numberOfPins;
            Assert.True(player.score.totalScore == perfectScore);
        }
        [Fact]
        public void IfSpareGivesCorrectPoints()
        {
            Player player = new Player("test");
            player.RollBall(GameOptions.numberOfPins-1);
            player.RollBall(1);
            player.RollBall(GameOptions.numberOfPins);
            int expectedScore = GameOptions.numberOfPins + GameOptions.numberOfPins + GameOptions.numberOfPins;
            Assert.True(player.score.totalScore == expectedScore);
        }

        [Fact]
        public void IfThreeStrikesOnLastFrameGivesCorrectFrameScore()
        {
            Frame frame = new Frame(GameOptions.numberOfFrames - 2);
            Frame lastFrame = new Frame(GameOptions.numberOfFrames - 1);
            Ball ball1 = new Ball(frame);
            Ball ball2 = new Ball(frame);
            Ball ball3 = new Ball(lastFrame);
            Ball ball4 = new Ball(lastFrame);
            Ball ball5 = new Ball(lastFrame);

            ball3.Roll(GameOptions.numberOfPins);
            lastFrame.ResetPins();
            ball4.Roll(GameOptions.numberOfPins);
            lastFrame.ResetPins();
            ball5.Roll(GameOptions.numberOfPins);

            List<Ball> balls = new List<Ball>();

            balls.Add(ball1);
            balls.Add(ball2);
            balls.Add(ball3);
            balls.Add(ball4);
            balls.Add(ball5);

            Score score = new Score(balls);

            score.UpdateScoreOnFrames();

            Assert.True(score.scoreOnFrames[GameOptions.numberOfFrames - 1] == 3*GameOptions.numberOfPins);
        }
    }
}
