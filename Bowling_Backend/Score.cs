using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling_Backend
{
    public class Score
    {
        public int totalScore { get; set; } = 0;
        public List<Ball> rolls { get; set; }
        public int[] scoreOnFrames;
        public Score(List<Ball> roll) 
        {
            scoreOnFrames = new int[GameOptions.numberOfFrames];
            this.rolls = roll;
        }

        public void UpdateTotalScore()
        {
            totalScore = 0;
            UpdateScoreOnFrames();
            for(int i = 0; i<scoreOnFrames.Length;i++)
            {
                totalScore += scoreOnFrames[i];
            }
        }

        public void UpdateScoreOnFrames()
        {
            scoreOnFrames = new int[GameOptions.numberOfFrames];
            if (rolls.Count != 0)
            {
                for (int i = 0; i < rolls.Count; i++)
                {
                    if (rolls[i].frame.frameNumber != GameOptions.numberOfFrames-1)
                    {
                        if (rolls[i].isSpare)
                        {
                            scoreOnFrames[rolls[i].frame.frameNumber] += ScoreSpareOnRoll(i);
                        }
                        
                        else if (rolls[i].isStrike)
                        {
                            scoreOnFrames[rolls[i].frame.frameNumber] += ScoreStrikeOnRoll(i);
                        }

                        //If we didnt roll a strike or a spare we just add the amount of knockedPins to the total score.
                        else scoreOnFrames[rolls[i].frame.frameNumber] += rolls[i].knockedPins;
                    }
                    //Handles the points for the last frame. There is some potential danger here if GameOptions.NumberOfFrames is set to 1
                    else scoreOnFrames[rolls[i].frame.frameNumber] += ScoreLastFrame(i);
                }
            }
        }

        private int ScoreLastFrame(int throwIndex)
        {
            //We first check if the previous roll was in the last frame
            //If the previous roll is still on the last frame and was a spare or a strike, then this roll should add no points in itself.
            //If we hit a strike on the first shot in the last frame, then the next two shots shouldn't count points themselves.
            //If we didnt hit a strike on the first shot but hit a spare on the second, then this should count as normal spare
            if (rolls[throwIndex - 1].frame.frameNumber == GameOptions.numberOfFrames - 1)
            {
                if (rolls[throwIndex - 1].isSpare || rolls[throwIndex - 1].isStrike) return 0;
                else if (rolls[throwIndex - 2].isStrike && rolls[throwIndex - 2].frame.frameNumber == GameOptions.numberOfFrames - 1) return 0;
                else if (rolls[throwIndex].isSpare)
                {
                    return ScoreSpareOnRoll(throwIndex);
                }
                //If we are on the second throw in the last frame and dont hit a strike we add the points as normal.
                else if (rolls[throwIndex - 2].frame.frameNumber != GameOptions.numberOfFrames - 1)
                {
                    return rolls[throwIndex].knockedPins;
                }
                else return 0;
            }
            //If we havent thrown any balls yet we on the last frame we are either a strike or a spare 
            else if (rolls[throwIndex].isStrike)
            {
                return ScoreStrikeOnRoll(throwIndex);
            }
            else return rolls[throwIndex].knockedPins;
        }


        //Gets the score in a frame specified by its frameNumber
        public int GetScoreInFrame(int frameNumber)
        {
            UpdateScoreOnFrames();
            if(frameNumber < GameOptions.numberOfFrames) return scoreOnFrames[frameNumber];
            return 0;

        }


        //If we roll a spare we check if there has been a suceeding roll, if so we also add the result from that roll and return the sum
        //If there has been no suceeding roll we just add that pins we knocked to the score
        public int ScoreSpareOnRoll(int rollNumber)
        {
            if(rollNumber<rolls.Count)
            {
                if (rolls[rollNumber].isSpare)
                {
                    if (CheckForSuceedingRoll(rollNumber))
                    {
                        return rolls[rollNumber].knockedPins + rolls[rollNumber + 1].knockedPins;
                    }
                    else return rolls[rollNumber].knockedPins;
                }
            }
            return 0;
        }
        //If we roll a strike we check if there has been 1,2 or no suceeding rolls, if so we also add the result from the rolls and return the sum
        //If there has been no suceeding roll we just add that pins we knocked to the score
        public int ScoreStrikeOnRoll(int rollNumber)
        {
            if(rollNumber<rolls.Count)
            {
                if (rolls[rollNumber].isStrike)
                {
                    if (CheckForTwoSuceedingRolls(rollNumber))
                    {
                        return rolls[rollNumber].knockedPins + rolls[rollNumber + 1].knockedPins + rolls[rollNumber + 2].knockedPins;
                    }
                    else if (CheckForSuceedingRoll(rollNumber))
                    {
                        return rolls[rollNumber].knockedPins + rolls[rollNumber + 1].knockedPins;
                    }
                    else return rolls[rollNumber].knockedPins;
                }
            }
            return 0;
        }




        public bool CheckForSuceedingRoll(int index) 
        {
            if (rolls.Count-1 > index) return true;
            return false;
        }

        public bool CheckForTwoSuceedingRolls(int index)
        {
            if (rolls.Count-2 > index) return true;
            return false;
        }


    }
}
