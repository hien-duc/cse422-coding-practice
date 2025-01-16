using System;

namespace Lab1_TranLeHienDuc_CSE422.Problem3
{
    public class Problem3
    {
        public static void Main()
        {
            try
            {
                string[][] board = ReadBoard();
                string word = ReadWordToSearch();
                bool exists = CheckExist(board, word);
                DisplayResult(word, exists);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid numbers for rows and columns");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static string[][] ReadBoard()
        {
            int rows = ReadBoardDimension("rows");
            int cols = ReadBoardDimension("columns");

            string[][] board = new string[rows][];
            Console.WriteLine("Enter the board elements row by row (space-separated):");

            for (int i = 0; i < rows; i++)
            {
                board[i] = ReadBoardRow(i + 1, cols);
            }

            return board;
        }

        private static int ReadBoardDimension(string dimensionName)
        {
            Console.WriteLine($"Enter the number of {dimensionName}:");
            if (!int.TryParse(Console.ReadLine(), out int dimension))
            {
                throw new FormatException($"Invalid {dimensionName} number");
            }
            return dimension;
        }

        private static string[] ReadBoardRow(int rowNumber, int expectedLength)
        {
            Console.WriteLine($"Row {rowNumber}:");
            string[] row = (Console.ReadLine() ?? string.Empty).Split(' ');

            if (row.Length != expectedLength)
            {
                throw new ArgumentException($"Row {rowNumber} must have exactly {expectedLength} elements");
            }

            return row;
        }

        private static string ReadWordToSearch()
        {
            Console.WriteLine("Enter the word to search:");
            return Console.ReadLine() ?? string.Empty;
        }

        private static void DisplayResult(string word, bool exists)
        {
            Console.WriteLine($"Word '{word}' exists in the board: {exists}");
        }

        public static bool CheckExist(string[][] board, string word)
        {
            if (!IsValidInput(board, word))
                return false;

            int rows = board.Length;
            int cols = board[0].Length;
            bool[,] visited = new bool[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    bool isStartingLetter = board[row][col] == word[0].ToString();
                    if (isStartingLetter && DFS(board, word, row, col, 0, visited))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsValidInput(string[][] board, string word)
        {
            bool isBoardValid = board != null && board.Length > 0;
            bool isWordValid = !string.IsNullOrEmpty(word);
            return isBoardValid && isWordValid;
        }

        private static bool DFS(string[][] board, string word, int row, int col, int index, bool[,] visited)
        {
            if (index == word.Length)
                return true;

            if (!IsValidPosition(board, row, col))
                return false;

            if (visited[row, col] || board[row][col] != word[index].ToString())
                return false;

            visited[row, col] = true;

            bool found = CheckAdjacentCells(board, word, row, col, index, visited);

            visited[row, col] = false;
            return found;
        }

        private static bool CheckAdjacentCells(string[][] board, string word, int row, int col, int index, bool[,] visited)
        {
            return DFS(board, word, row + 1, col, index + 1, visited) ||  // Down
                   DFS(board, word, row - 1, col, index + 1, visited) ||  // Up
                   DFS(board, word, row, col + 1, index + 1, visited) ||  // Right
                   DFS(board, word, row, col - 1, index + 1, visited);    // Left
        }

        private static bool IsValidPosition(string[][] board, int row, int col)
        {
            bool isRowValid = row >= 0 && row < board.Length;
            if (!isRowValid) return false;

            bool isColValid = col >= 0 && col < board[0].Length;
            if (!isColValid) return false;

            return true;
        }
    }
}
