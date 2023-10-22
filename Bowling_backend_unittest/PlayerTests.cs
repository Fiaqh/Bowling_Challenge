using Bowling_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_backend_unittest
{
    public class PlayerTests
    {
        [Fact] public void IfUpdateCurrentFrameNumberUpdatesCurrentFrameNumberCorrectlyWhenLastBallRolledWasAStrikeInNotLastFrame()
        {
            Player player = new Player("test");
            player.RollBall(GameOptions.numberOfPins);
            Assert.True(player.currentFrameNumber == 1);
        }
        [Fact]
        public void IfRollBallUpdatesCurrentFrameCorrectlyOnLastFrameIfStrikeOnFirstThrowInLastFrame()
        {
            Player player = new Player("test");
            for(int i = 0; i<GameOptions.numberOfFrames; i++)
            {
                player.RollBall(GameOptions.numberOfPins);
            }
            Assert.True(player.currentFrameNumber == GameOptions.numberOfFrames-1);
        }

        [Fact]
        public void IfRollBallUpdatesCurrentFrameCorrectlyOnLastFrameIfStrikeOnFirstAndOnSecondThrowInLastFrame()
        {
            Player player = new Player("test");
            for (int i = 0; i < GameOptions.numberOfFrames+1; i++)
            {
                player.RollBall(GameOptions.numberOfPins);
            }
            Assert.True(player.currentFrameNumber == GameOptions.numberOfFrames - 1);
        }
        [Fact]
        public void IfRollBallUpdatesCurrentFrameCorrectlyOnLastFrameIfSpareOnLastFrame()
        {
            Player player = new Player("test");
            for (int i = 0; i < GameOptions.numberOfFrames - 1; i++)
            {
                player.RollBall(GameOptions.numberOfPins);
            }
            player.RollBall(GameOptions.numberOfPins - 1);
            player.RollBall(1);
            Assert.True(player.currentFrameNumber == GameOptions.numberOfFrames - 1);
        }

        [Fact]
        public void PlayerCanThrowTwiceAfterAStrikeOnTheFirstShotInTheLastFrame()
        {
            Player player = new Player("test");
            
            for(int i = 0; i<GameOptions.numberOfFrames; i++)
            {
                player.RollBall(GameOptions.numberOfPins);
            }
            player.RollBall(GameOptions.numberOfPins - 1);
            Assert.True(player.currentFrameNumber == GameOptions.numberOfFrames - 1);
        }
     }
}
