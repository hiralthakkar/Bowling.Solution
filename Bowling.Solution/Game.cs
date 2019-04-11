using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Solution
{
    public class Game
    {
        private int[] rolls;
        private int currentRoleIndex;

        private const int totalFrameCount = 10;
        private const int maxFrameScore = 10;

        public Game()
        {
            rolls = new int[21];
        }

        /// <summary>
        /// method for Roll
        /// thows exception if the game is over already
        /// </summary>
        /// <param name="pins"></param>
        public void Roll(int pins)
        {
            if (currentRoleIndex >= rolls.Length)
                throw new InvalidOperationException("No more rolls allowed");

            rolls[currentRoleIndex] = pins;
            currentRoleIndex++;
        }

        /// <summary>
        /// Method that calculated total score of the game
        /// considering formula for Strike and Spare and then OpenFrame
        /// </summary>
        /// <returns></returns>
        public int Score()
        {
            int currentFrameIndex = 0;
            int totalScore = 0;
            try
            {
                //this counter is needed to consider total number of frames allowed in a game.
                //assuming 2 rolls in each frame with exception of possibly 3 in the last one
                for (int frameIndex = 0; frameIndex < totalFrameCount; frameIndex++)
                {
                    if (currentFrameIndex >= rolls.Length)
                        break;

                    //if it's a strike, add to the total score including strike bonus 
                    if (rolls[currentFrameIndex] == maxFrameScore)
                    {
                        totalScore += maxFrameScore + getStrikeBonus(currentFrameIndex);
                        currentFrameIndex++;
                    }
                    //if it's a spare, add to the total score and spare bonus
                    else if (getFrameScore(currentFrameIndex) == maxFrameScore)
                    {
                        totalScore += maxFrameScore + getSpareBonus(currentFrameIndex);
                        currentFrameIndex += 2;
                    }
                    else
                    {
                        //this is open frame situation
                        totalScore += getFrameScore(currentFrameIndex);
                        currentFrameIndex += 2;
                    }
                }
            }
            catch
            {
                throw new Exception("An error occurred while trying to calculate final score");
            }

            return totalScore;
        }

        /// <summary>
        /// method to calculate bonus for a strike
        /// since the current roll/frame would be a strike, the score from next two rolls/frames will be the bonus
        /// </summary>
        /// <param name="frameIndex"></param>
        /// <returns></returns>
        private int getStrikeBonus(int frameIndex)
        {
            return rolls[frameIndex + 1] + rolls[frameIndex + 2];
        }

        /// <summary>
        /// method to calculate bonus for a spare
        /// return the score of the roll from next frame which will be 2 rolls after the current
        /// </summary>
        /// <param name="frameIndex"></param>
        /// <returns></returns>
        private int getSpareBonus(int frameIndex)
        {            
            return rolls[frameIndex + 2];
        }

        /// <summary>
        /// method to calculate the current frame's score
        /// by adding the pins knocked on current and next roll
        /// </summary>
        /// <param name="frameIndex"></param>
        /// <returns></returns>
        private int getFrameScore(int frameIndex)
        {            
            return rolls[frameIndex] + rolls[frameIndex + 1];
        }
    }
}