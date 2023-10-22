using Bowling_Backend;
using Microsoft.VisualBasic;
namespace Bowling_Frontend
{
    public partial class MainWindow : Form
    {
        public Player player;
        public GUIMaker GUI;
        public MainWindow()
        {
            InitializeComponent();
            this.Text = "Bowling_challenge";
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            string userInput = Interaction.InputBox("Enter your name:", "Name:", "Test");
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                player = new Player(userInput);
                this.Text = "Bowling with " + player.name;
                GUI = new GUIMaker(player, MainPanel);
                GUI.SetupGame();
            }
            
        }
    }
}