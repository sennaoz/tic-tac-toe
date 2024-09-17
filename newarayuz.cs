using System;
using Xamarin.Forms;

namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        private char[,] board = new char[3, 3];
        private bool isPlayer1Turn = true; // Oyuncu 1 = X, Oyuncu 2 = O
        private int player1Score = 0, player2Score = 0;

        public MainPage()
        {
            InitializeComponent();
            ResetBoard();
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            string player1 = player1Entry.Text;
            string player2 = player2Entry.Text;

            if (string.IsNullOrEmpty(player1) || string.IsNullOrEmpty(player2))
            {
                DisplayAlert("Hata", "Lütfen oyuncu isimlerini giriniz.", "Tamam");
                return;
            }

            turnLabel.Text = $"Sıra: {player1} (X)";
            ResetBoard();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (string.IsNullOrEmpty(button.Text))
            {
                button.Text = isPlayer1Turn ? "X" : "O";
                int row = Grid.GetRow(button);
                int col = Grid.GetColumn(button);
                board[row, col] = isPlayer1Turn ? 'X' : 'O';

                if (CheckWinner())
                {
                    string winner = isPlayer1Turn ? player1Entry.Text : player2Entry.Text;
                    DisplayAlert("Tebrikler!", $"{winner} kazandı!", "Tamam");

                    if (isPlayer1Turn)
                        player1Score++;
                    else
                        player2Score++;

                    UpdateScore();
                    ResetBoard();
                }
                else if (IsBoardFull())
                {
                    DisplayAlert("Berabere!", "Oyun berabere bitti.", "Tamam");
                    ResetBoard();
                }
                else
                {
                    isPlayer1Turn = !isPlayer1Turn;
                    turnLabel.Text = $"Sıra: {(isPlayer1Turn ? player1Entry.Text : player2Entry.Text)} ({(isPlayer1Turn ? 'X' : 'O')})";
                }
            }
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            ResetBoard();
        }

        private void ResetBoard()
        {
            board = new char[3, 3];

            foreach (Button button in gameGrid.Children)
            {
                button.Text = string.Empty;
            }

            isPlayer1Turn = true;
            turnLabel.Text = $"Sıra: {player1Entry.Text} (X)";
        }

        private bool IsBoardFull()
        {
            foreach (char cell in board)
            {
                if (cell == '\0')
                    return false;
            }
            return true;
        }

        private bool CheckWinner()
        {
            // Satırları ve sütunları kontrol et
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != '\0' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
                if (board[0, i] != '\0' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }

            // Çaprazları kontrol et
            if (board[0, 0] != '\0' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;
            if (board[0, 2] != '\0' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;

            return false;
        }

        private void UpdateScore()
        {
            scoreLabel.Text = $"Skor: {player1Score} - {player2Score}";
        }
    }
}
