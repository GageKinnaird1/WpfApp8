using System.Windows;
using System.Windows.Controls;
using System.Media;
using WpfApp8;
using System.Windows.Input;
using System.Windows.Threading;
using System;
using System.Timers;
using System.Diagnostics.Eventing.Reader;
using System.Transactions;
using System.Windows.Media;
namespace TicTacToe
{
    
    public partial class MainWindow : Window
    {   //Text creating the gameboard
        private string[,] board = new string[3, 3];
        private string currentPlayer = "🦌";
        private DispatcherTimer timer;
        private int remainingTime = 6;
        private Page1 menuWindow;
        //private string winner;
        private MediaPlayer playerT = new MediaPlayer();
       
        public MainWindow(Page1 p)
        {
            InitializeComponent();
            menuWindow = p;
            InitializeBoard();
            playerT.Open(new Uri($"Assets/Clock.wav", UriKind.Relative));
            WindowState = WindowState.Maximized;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        //Goes over the rows and columns of the board and makes sure none are empty
        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = "";
                }
            }
        }

        //When any of the buttons is pressed it comes from a button sender all going to one spot
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Sound effects for Santa and Reindeer
            if (currentPlayer == "🎅")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri($"Assets/Santa.wav", UriKind.Relative));
                player.Play();

            }

            if (currentPlayer == "🦌")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri($"Assets/Bells.wav", UriKind.Relative));
                player.Play();
            }


            Button button = (Button)sender;
            int row = Grid.GetRow(button) - 1;
            int column = Grid.GetColumn(button);
            if (board[row, column] == "")
            {
                board[row, column] = currentPlayer;
                button.Content = currentPlayer;
                //Checks for winner
                if (CheckForWinner())
                {
                    UpdateWinCounter(currentPlayer);
                    MessageBox.Show(currentPlayer + " wins!");
                    ResetBoard();
                }
                //Time for each player
                if (currentPlayer == "🦌")
                {
                    currentPlayer = "🎅";
                    remainingTime = 5;
                }

                else if (currentPlayer == "🎅")
                {
                    currentPlayer = "🦌";
                    remainingTime = 5;
                }
                //Is it a tie?
                if (CheckForTie())
                {
                    MessageBox.Show("Tie game!");
                    ResetBoard();
                }
                //MessageBox.Show($"current player = {currentPlayer}");
            }
        }
        

        private bool CheckForWinner()
        {
            // Check rows for winner
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return true;
                }
            }

            // Check columns for winner
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] != "" && board[0, j] == board[1, j] && board[1, j] == board[2, j])
                {
                    return true;
                }
            }

            // Check diagonals for winner
            if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return true;
            }
            if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return true;
            }

            return false;
        }
        //Shows a counter for Santa and Reindeer
        private int xWins = 0;
        private int oWins = 0;
        private void UpdateWinCounter(string currentPlayer)
        {
            if (currentPlayer == "🦌")
            {
                xWins++;
                xWinsCounter.Text = "🦌 Wins: " + xWins;
            }
            else if (currentPlayer == "🎅")
            {
                oWins++;
                oWinsCounter.Text = "🎅 Wins: " + oWins;

            }
        }
        //Displays Timer and if the other players turn gets skipped
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(this.Visibility != Visibility.Visible)
             {
                return;
            }
            remainingTime--;
            if (remainingTime <= 0)
            {
                //MessageBox.Show($"current player = {currentPlayer}");

                currentPlayer = currentPlayer == "🎅" ? "🦌" : "🎅";
                MessageBox.Show("Times up!" +
                          " Switching Players");
                       //$"Next Players Turn...{currentPlayer}");
                remainingTime = 5;
            }
           
            if (remainingTime <= 3)
            {
                
                playerT.Play();
            }
            if (remainingTime <= 1)
            {
              
                playerT.Stop();
            }

            if (remainingTime <= 0)
            {
                timer.Stop();

            }

            timerTextBlock.Text = $"Time Left On Turn: {remainingTime}";
        }
        //Checks for if no one won and says its a tie
        private bool CheckForTie()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == "")
                    {
                        return false;
                    }
                }
            }

            return true;
        }
            //Resets board after game is complete
            //Starts board with all button names listed
            private void ResetBoard()
        {
            InitializeBoard();

            button00.Content = "";
            button01.Content = "";
            button02.Content = "";
            button10.Content = "";
            button11.Content = "";
            button12.Content = "";
            button20.Content = "";
            button21.Content = "";
            button22.Content = "";
           
            currentPlayer = "🦌";
        }
        // When this is clicked it hides the main window going back to the starting screen
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            menuWindow.Show();
            ResetBoard();
            remainingTime = 5;
            this.Hide();

        }
    }
}