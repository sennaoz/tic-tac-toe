using System;

class Program
{
    static char[,] board;
    static int boardSize;
    static string player1, player2;
    static char player1Symbol = 'X', player2Symbol = 'O';
    static bool isPlayer1Turn = true;
    static Random random = new Random();

    static void Main(string[] args)
    {
        ShowMenu();
        SetupPlayers();
        PlayGame();
    }

    static void ShowMenu()
    {
        Console.WriteLine("Tic Tac Toe Oyununa Hoşgeldiniz!");
        Console.WriteLine("1. Bilgisayara karşı oyna");
        Console.WriteLine("2. 2 kişilik oyna");
        Console.Write("Seçiminizi yapın (1 veya 2): ");
        int choice = int.Parse(Console.ReadLine());

        Console.WriteLine("Tahta boyutunu seçin: ");
        Console.WriteLine("1. 3x3");
        Console.WriteLine("2. 6x6");
        Console.WriteLine("3. 9x9");
        Console.Write("Seçiminizi yapın (1, 2 veya 3): ");
        int boardChoice = int.Parse(Console.ReadLine());

        switch (boardChoice)
        {
            case 1:
                boardSize = 3;
                break;
            case 2:
                boardSize = 6;
                break;
            case 3:
                boardSize = 9;
                break;
            default:
                boardSize = 3;
                break;
        }

        board = new char[boardSize, boardSize];
        InitializeBoard();
    }

    static void SetupPlayers()
    {
        Console.Write("1. oyuncu ismini girin: ");
        player1 = Console.ReadLine();
        
        Console.Write("2. oyuncu ismini girin: ");
        player2 = Console.ReadLine();
    }

    static void InitializeBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                board[i, j] = '-';
            }
        }
    }

    static void PlayGame()
    {
        bool isGameRunning = true;
        while (isGameRunning)
        {
            DisplayBoard();
            if (isPlayer1Turn)
            {
                Console.WriteLine($"{player1}'in sırası ({player1Symbol}): ");
            }
            else
            {
                Console.WriteLine($"{player2}'nin sırası ({player2Symbol}): ");
            }

            Console.Write("Satır numarasını girin (1-{0}): ", boardSize);
            int row = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Sütun numarasını girin (1-{0}): ", boardSize);
            int col = int.Parse(Console.ReadLine()) - 1;

            if (board[row, col] == '-')
            {
                board[row, col] = isPlayer1Turn ? player1Symbol : player2Symbol;
                if (CheckWin())
                {
                    DisplayBoard();
                    Console.WriteLine($"{(isPlayer1Turn ? player1 : player2)} kazandı!");
                    isGameRunning = false;
                }
                else if (IsBoardFull())
                {
                    DisplayBoard();
                    Console.WriteLine("Berabere!");
                    isGameRunning = false;
                }
                isPlayer1Turn = !isPlayer1Turn;
            }
            else
            {
                Console.WriteLine("Bu hücre zaten dolu, başka bir hücre seçin.");
            }
        }
    }

    static void DisplayBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static bool CheckWin()
    {
        for (int i = 0; i < boardSize; i++)
        {
            if (CheckRow(i) || CheckColumn(i))
            {
                return true;
            }
        }
        return CheckDiagonals();
    }

    static bool CheckRow(int row)
    {
        char symbol = board[row, 0];
        if (symbol == '-')
            return false;

        for (int i = 1; i < boardSize; i++)
        {
            if (board[row, i] != symbol)
                return false;
        }
        return true;
    }

    static bool CheckColumn(int col)
    {
        char symbol = board[0, col];
        if (symbol == '-')
            return false;

        for (int i = 1; i < boardSize; i++)
        {
            if (board[i, col] != symbol)
                return false;
        }
        return true;
    }

    static bool CheckDiagonals()
    {
        char symbol = board[0, 0];
        bool diagonal1 = symbol != '-';
        for (int i = 1; i < boardSize && diagonal1; i++)
        {
            if (board[i, i] != symbol)
                diagonal1 = false;
        }

        symbol = board[0, boardSize - 1];
        bool diagonal2 = symbol != '-';
        for (int i = 1; i < boardSize && diagonal2; i++)
        {
            if (board[i, boardSize - i - 1] != symbol)
                diagonal2 = false;
        }

        return diagonal1 || diagonal2;
    }

    static bool IsBoardFull()
    {
        foreach (char cell in board)
        {
            
            if (cell == '-')
            {
                return false;
            }
        }
        return true;
    }
}
