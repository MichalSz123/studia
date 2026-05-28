using System;

namespace TicTacToe
{
    class Player
    {
        // Właściwości tylko do odczytu z zewnątrz
        public string Name { get; }
        public char Symbol { get; }

        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }
    }

    class Board
    {
        private char[] grid;

        public Board()
        {
            // Inicjalizacja pustymi znakami
            grid = new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine("Kółko i Krzyżyk - Pola od 1 do 9\n");
            Console.WriteLine($" {grid[0]} | {grid[1]} | {grid[2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {grid[3]} | {grid[4]} | {grid[5]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {grid[6]} | {grid[7]} | {grid[8]} \n");
        }

        public bool PlaceSymbol(int position, char symbol)
        {
            if (grid[position] == ' ')
            {
                grid[position] = symbol;
                return true;
            }
            return false; // Pole zajęte
        }

        public bool CheckWinner()
        {
            int[,] winConditions = new int[,] 
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Wiersze
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Kolumny
                {0, 4, 8}, {2, 4, 6}             // Przekątne
            };

            for (int i = 0; i < winConditions.GetLength(0); i++)
            {
                int a = winConditions[i, 0];
                int b = winConditions[i, 1];
                int c = winConditions[i, 2];

                if (grid[a] != ' ' && grid[a] == grid[b] && grid[b] == grid[c])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFull()
        {
            foreach (char cell in grid)
            {
                if (cell == ' ') return false;
            }
            return true;
        }
    }

    class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private Player currentPlayer;

        public Game()
        {
            board = new Board();
            player1 = new Player("Gracz 1", 'X');
            player2 = new Player("Gracz 2", 'O');
            currentPlayer = player1;
        }

        private void SwitchTurn()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        public void Start()
        {
            while (true)
            {
                board.Display();
                Console.WriteLine($"Ruch wykonuje: {currentPlayer.Name} ({currentPlayer.Symbol})");
                Console.Write("Wybierz pole (1-9): ");

                string input = Console.ReadLine();
                
                // Walidacja wejścia
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 9)
                {
                    int index = choice - 1; // Dopasowanie do indeksu 0-8

                    if (board.PlaceSymbol(index, currentPlayer.Symbol))
                    {
                        if (board.CheckWinner())
                        {
                            board.Display();
                            Console.WriteLine($"Gratulacje! {currentPlayer.Name} wygrywa!");
                            break; // Koniec pętli - wygrana
                        }
                        else if (board.IsFull())
                        {
                            board.Display();
                            Console.WriteLine("Koniec gry. Mamy remis!");
                            break; // Koniec pętli - remis
                        }

                        SwitchTurn();
                    }
                    else
                    {
                        Console.WriteLine("To pole jest już zajęte! Naciśnij Enter i spróbuj ponownie.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy wybór! Podaj cyfrę od 1 do 9. Naciśnij Enter.");
                    Console.ReadLine();
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game ticTacToe = new Game();
            ticTacToe.Start();
        }
    }
}
