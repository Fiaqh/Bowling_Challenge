using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Backend;

namespace Bowling_Frontend
{
    public class GUIMaker
    {
        private static int frameLabelSizeX = 100;
        private static int frameLabelSizeY = 60;
        private static int buttonSizeX = 40;
        private static int buttonSizeY = 30;
        private static int startLocationX = 10;
        private static int startLocationY = 10;
        public Player player;
        public Panel panel;

        public GUIMaker(Player player, Panel panel) 
        {
            this.player = player;
            this.panel = panel;
        }

        public void AddFrameToPanel(int xPos, int yPos, Panel panel, string firstShot, string secondShot, string thirdShot, int frameNumber)
        {
            Label firstShotLabel = new Label();
            Label header = new Label();
            Label pointsInFrame = new Label();

            //Sets the borderstyle
            firstShotLabel.BorderStyle = BorderStyle.FixedSingle;
            header.BorderStyle = BorderStyle.FixedSingle;
            pointsInFrame.BorderStyle = BorderStyle.FixedSingle;

            //Make labels transparent so they can overlap
            firstShotLabel.BackColor = Color.Transparent;
            pointsInFrame.BackColor = Color.Transparent;



            //Sets the size of the labels
            int firstShotLabelSizeX = frameLabelSizeX;
            int firstShotLabelSizeY = frameLabelSizeY;
            int headerSizeX = firstShotLabelSizeX;
            int headerSizeY = firstShotLabelSizeX / 5;
            int pointsInFrameSizeX = firstShotLabelSizeX;
            int pointsInFrameSizeY = firstShotLabelSizeX / 5;

            //Adjust the label positions and places the secondShotLabel and thirdShotLabel in the top right corner of the firstShotLabel
            firstShotLabel.Size = new Size(firstShotLabelSizeX, firstShotLabelSizeY);
            firstShotLabel.Location = new Point(xPos, yPos + headerSizeY);

            header.Size = new Size(headerSizeX, headerSizeY);
            header.Location = new Point(xPos, yPos);
            pointsInFrame.Size = header.Size;
            pointsInFrame.Location = new Point(xPos, yPos + headerSizeY + firstShotLabelSizeY);

            //Sets the text for the labels and the header
            firstShotLabel.Text = firstShot.ToString();
            firstShotLabel.Text += (secondShot != "") ? " + " + secondShot : "";
            firstShotLabel.Text += (thirdShot != "") ? " + " + thirdShot : "";
            header.Text = frameNumber.ToString();

            //Computes the players current score
            int scoreSoFar = 0;
            for (int i = 0; i <= frameNumber; i++)
            {
                scoreSoFar += player.score.GetScoreInFrame(i);
            }

            pointsInFrame.Text = (player.currentFrameNumber  > frameNumber) ? "Score: " + scoreSoFar : "Score:";

            //Adds the labels to the panel
            panel.Controls.Add(firstShotLabel);
            panel.Controls.Add(header);
            panel.Controls.Add(pointsInFrame);

        }



        public void SetupGame()
        {
            panel.Controls.Clear();
            int buttonLocationY = 10;
            int StartFrameLocationX = 10;
            int frameLocationY = buttonSizeY+10;

            //Adds all frames to panel except the last frame
            for(int frameNumber = 0; frameNumber<GameOptions.numberOfFrames; frameNumber++)
            {
                List<Ball> rolledBallsInFrame = player.GetRolledBallsInFrame(frameNumber);
                switch (rolledBallsInFrame.Count)
                {
                    case 0:
                        AddFrameToPanel(StartFrameLocationX + frameNumber * frameLabelSizeX, frameLocationY, panel, "", "","", frameNumber);
                        break;
                    case 1:
                        AddFrameToPanel(StartFrameLocationX + frameNumber * frameLabelSizeX, frameLocationY, panel, rolledBallsInFrame[0].knockedPins.ToString(), "","", frameNumber);
                        break;
                    case 2:
                        AddFrameToPanel(StartFrameLocationX + frameNumber * frameLabelSizeX, frameLocationY, panel, rolledBallsInFrame[0].knockedPins.ToString(), rolledBallsInFrame[1].knockedPins.ToString(),"", frameNumber);
                        break;
                    case 3:
                        AddFrameToPanel(StartFrameLocationX + (GameOptions.numberOfFrames - 1) * frameLabelSizeX, frameLocationY, panel, rolledBallsInFrame[0].knockedPins.ToString(), rolledBallsInFrame[1].knockedPins.ToString(), rolledBallsInFrame[2].knockedPins.ToString(),frameNumber);
                        break;
                    default:
                        break;
                }
            }
            //Adds buttons for the user to enter desired outcome of RollBall
            if(player.currentFrameNumber < GameOptions.numberOfFrames) MakeRollBallButtons(startLocationX, startLocationY, panel, player.frames[player.currentFrameNumber].pinsLeft + 1);

            //Make a reset button
            Button resetButton = new Button();
            resetButton.Text = "Reset game";
            resetButton.Size = new Size(100, 100);
            resetButton.Location = new Point(panel.Size.Width - 200, panel.Size.Height - 200);
            resetButton.Click += Reset;
            panel.Controls.Add(resetButton);
        }
        //Handles the reset button click
        public void Reset(object sender, EventArgs e)
        {
            player = new Player(player.name);
            SetupGame();
        }

        //Makes a specified number of buttons, with the i'th button rolling a ball hitting i pins
        public void MakeRollBallButtons(int posX, int posY, Panel panel, int numberOfButtons)
        {
            for(int i = 0; i< numberOfButtons; i++)
            {
                Button button = new Button();
                button.Tag = i;
                button.Size = new Size(buttonSizeX,buttonSizeY);
                button.Location = new Point(posX + 10 + i * buttonSizeX, posY);
                button.Text = i.ToString();
                button.Click += RollBallButtonClick;
                panel.Controls.Add(button);
            }
        }


        //Handles the RollBall button click 
        public void RollBallButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // 
                if (clickedButton.Tag != null)
                {
                    player.RollBall(Convert.ToInt32(clickedButton.Tag));
                    SetupGame();
                }
            }
        }


    }
}

