using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TicTacToe
{
}
public class TicTacToe2
{
    private char[,] board = new char[3, 3];
    private char currentPlayer = 'X';
    private Timer playerTimer = new Timer();
    private int timeLimit = 10; // seconds

    public TicTacToe2()
    {
        // Initialize board with empty cells
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                board[row, col] = '-';
            }
        }

        // Set up player timer
        playerTimer.Interval = timeLimit * 1000; // Convert seconds to milliseconds
        playerTimer.AutoReset = false;
        playerTimer.Elapsed += OnPlayerTimerElapsed;
    }

    public void StartGame()
    {
        while (!IsGameOver())
        {
            DisplayBoard();
            Console.WriteLine("Player {0}'s turn. Time limit: {1} seconds", currentPlayer, timeLimit);
            playerTimer.Start();
            MakeMove(GetValidMove());
            playerTimer.Stop();
            SwitchPlayer();
        }

        DisplayBoard();
        Console.WriteLine(GetGameOverMessage());
    }

    private void OnPlayerTimerElapsed(Object source, ElapsedEventArgs e)
    {
        Console.WriteLine("Player {0}'s time ran out. Placing symbol at random position...", currentPlayer);

        // Find a random empty cell to place the symbol
        Random random = new Random();
        int row, col;
        do
        {
            row = random.Next(3);
            col = random.Next(3);
        } while (board[row, col] != '-');

        board[row, col] = currentPlayer;
    }

    private void DisplayBoard()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private bool IsGameOver()
    {
        // Check rows
        for (int row = 0; row < 3; row++)
        {
            if (board[row, 0] != '-' && board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
            {
                return true;
            }
        }

        // Check columns
        for (int col = 0; col < 3; col++)
        {
            if (board[0, col] != '-' && board[0, col] == board[1, col] && board[1, col] == board[2, col])
            {
                return true;
            }
        }

        // Check diagonals
        if (board[0, 0] != '-' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return true;
        }
        if (board[0, 2] != '-' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return true;
        }

        // Check if board is full
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0;;
                )if (board[row, col] == '-')
                {
                    return false;
                }
        }

        return true;
    }

    private string GetGameOverMessage()
    {
        if (IsGameOver())
        {
            return "Player " + currentPlayer + " wins!";
        }
        else
        {
            return "It's a tie!";
        }
    }

    private void SwitchPlayer()
    {
        if (currentPlayer == 'X')
        {
            currentPlayer = 'O';
        }
        else
        {
            currentPlayer = 'X';
        }
    }

    private void MakeMove((int, int) move)
    {
        board[move.Item1, move.Item2] = currentPlayer;
    }

    private (int, int) GetValidMove()
    {
        int row, col;
        do
        {
            Console.Write("Enter row (0-2): ");
            row = int.Parse(Console.ReadLine());
            Console.Write("Enter column (0-2): ");
            col = int.Parse(Console.ReadLine());
        } while (!IsValidMove(row, col));

        return (row, col);
    }

    private bool IsValidMove(int row, int col)
    {
        if (row < 0 || row > 2 || col < 0 || col > 2)
        {
            Console.WriteLine("Invalid row or column. Please enter a number between 0 and 2.");
            return false;
        }

        if (board[row, col] != '-')
        {
            Console.WriteLine("That cell is already occupied. Please choose an empty cell.");
            return false;
        }

        return true;
    }
}