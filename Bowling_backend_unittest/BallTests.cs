using Bowling_Backend;

namespace Bowling_backend_unittest
{
    public class BallTests
    {
        [Fact]
        public void IfRollBallKnocksAllPinsOnFirstThrowBecomesStrike()
        {
            Ball ball = new Ball(new Frame(0));

            ball.Roll(ball.frame.pinsLeft);

            Assert.True(ball.isStrike);
        }

        [Fact]
        public void IfRollBallKnocksAllPinsOnSecondThrowNotStrike()
        {
            Ball ball = new Ball(new Frame(0) { shotsInFrame = 1});

            ball.Roll(ball.frame.pinsLeft);

            Assert.False(ball.isStrike);
        }
        [Fact]
        public void IfRollBallKnocksAllPinsOnSecondThrowBecomesSpare()
        {
            Ball ball = new Ball(new Frame(0) { shotsInFrame = 1});

            ball.Roll(ball.frame.pinsLeft);

            Assert.True(ball.isSpare);
        }


    }
}