using System;
using System.IO;
using System.Windows.Forms;

namespace TicTacToeGame
{
    public partial class Form1 : Form
    {
        private Button[,] buttons;
        private string player1Name, player2Name;
        private string currentPlayer;
        private int player1Score = 0, player2Score = 0;
        private int boardSize = 3; // Varsayılan olarak 3x3
        private int moveCount = 0;
        private string scoreFilePath = "scores.txt"; // Skor dosyası yolu

        public Form1()
        {
            InitializeComponent();
            LoadScores();
        }

        // Oyuncuların skorlarını dosyadan yükle
        private void LoadScores()
       
