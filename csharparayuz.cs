using System;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public partial class Form1 : Form
    {
        private Button[,] buttons;
        private string player1Name, player2Name;
        private string currentPlayer;
        private int boardSize = 3; // Varsayılan olarak 3x3
        private int moveCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            player1Name = player1TextBox.Text;
            player2Name = player2TextBox.Text;

            if (string.IsNullOrEmpty(player1Name) || string.IsNullOrEmpty(player2Name))
            {
                MessageBox.Show("Lütfen her iki oyuncunun ismini de girin.");
                return;
            }

            currentPlayer = player1Name; // İlk olarak 1. oyuncu başlar
            turnLabel.Text = $"{currentPlayer}'ın sırası";

            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            gameTable.Controls.Clear(); // Önceki oyun tahtasını temizle
            gameTable.RowCount = boardSize;
            gameTable.ColumnCount = boardSize;

            buttons = new Button[boardSize, boardSize];
            moveCount = 0;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Dock = DockStyle.Fill,
                        Font = new System.Drawing.Font("Arial", 24),
                        Tag = new int[] { i, j }
                    };
                    buttons[i, j].Click += ButtonClick;
                    gameTable.Controls.Add(buttons[i, j], j, i);
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null && string.IsNullOrEmpty(clickedButton.Text))
            {
                clickedButton.Text = currentPlayer == player1Name ? "X" : "O";
                moveCount++;

                if (CheckForWinner())
                {
                    MessageBox.Show($"{currentPlayer} kazandı!");
                    ResetGame();
                }
                else if (moveCount == boardSize * boardSize)
                {
                    MessageBox.Show("Berabere!");
                    ResetGame();
                }
                else
                {
                    SwitchPlayer();
                }
            }
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == player1Name ? player2Name : player1Name;
            turnLabel.Text = $"{currentPlayer}'ın sırası";
        }

        private bool CheckForWinner()
        {
            // Satırları kontrol et
            for (int i = 0; i < boardSize; i++)
            {
                if (CheckRow(i))
                {
                    return true;
                }
            }

            // Sütunları kontrol et
            for (int i = 0; i < boardSize; i++)
            {
                if (CheckColumn(i))
                {
                    return true;
                }
            }

            // Çaprazları kontrol et
            return CheckDiagonals();
        }

        private bool CheckRow(int row)
        {
            char symbol = buttons[row, 0].Text[0];
            if (buttons[row, 0].Text == "")
                return false;

            for (int i = 1; i < boardSize; i++)
            {
                if (buttons[row, i].Text != buttons[row, 0].Text)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckColumn(int col)
        {
            char symbol = buttons[0, col].Text[0];
            if (buttons[0, col].Text == "")
                return false;

            for (int i = 1; i < boardSize; i++)
            {
                if (buttons[i, col].Text != buttons[0, col].Text)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckDiagonals()
        {
            bool diagonal1 = true, diagonal2 = true;
            char symbol = buttons[0, 0].Text[0];
            if (buttons[0, 0].Text == "")
                diagonal1 = false;

            for (int i = 1; i < boardSize && diagonal1; i++)
            {
                if (buttons[i, i].Text != buttons[0, 0].Text)
                {
                    diagonal1 = false;
                }
            }

            symbol = buttons[0, boardSize - 1].Text[0];
            if (buttons[0, boardSize - 1].Text == "")
                diagonal2 = false;

            for (int i = 1; i < boardSize && diagonal2; i++)
            {
                if (buttons[i, boardSize - i - 1].Text != buttons[0, boardSize - 1].Text)
                {
                    diagonal2 = false;
                }
            }


            return diagonal1 || diagonal2;
        }



        private void ResetGame()
        {
            InitializeGameBoard();
            currentPlayer = player1Name;
            turnLabel.Text = $"{currentPlayer}'ın sırası";
        }
        
    }
}
