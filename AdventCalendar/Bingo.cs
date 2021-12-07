using System;
using System.Collections.Generic;
using System.IO;

namespace AdventCalendar
{
    public class Bingo
    {
        static public List<BingoBoard> bingoBoards { get; set; }
        static public List<BingoBoard> winningBoards { get; set; }
        static public List<int> calledNumbers { get; set; }
        public Bingo(string filename, int width, int height)
        {
            bingoBoards = new List<BingoBoard>();
            calledNumbers = new List<int>();
            GetBingoData(filename, width, height);
        }
        static void GetBingoData(string filename, int width, int height)
        {
            int[,] boardData = new int[width, height];
            List<int[]> rows = new List<int[]>();
            string line;
            int lineNumber = 1;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                //read in called numbers
                if (lineNumber == 1)
                {
                    string[] lineArray = line.Split(',');
                    foreach (string item in lineArray)
                    {
                        calledNumbers.Add(Int32.Parse(item));
                    }
                }
                //start of board data
                if (lineNumber > 2)
                {
                    if (line != "")
                    {
                        string[] lineArray = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        int[] row = new int[width];
                        for (int i = 0; i < width; i++)
                        {
                            row[i] = Int32.Parse(lineArray[i]);
                        }
                        rows.Add(row);
                        if (rows.Count == height)
                        {
                            boardData = new int[width, height];
                            for (int y = 0; y < rows.Count; y++)
                            {
                                for (int x = 0; x < rows[y].Length; x++)
                                {
                                    boardData[x, y] = rows[y][x];
                                }
                            }
                            bingoBoards.Add(new BingoBoard(width, height, boardData));
                            rows = new List<int[]>();
                        }
                    }
                }
                lineNumber++;
            }
        }
        public int PlayBingo()
        {
            foreach (int number in calledNumbers)
            {
                foreach (BingoBoard board in bingoBoards)
                {
                    board.CallNumber(number);
                    if (board.CheckWin())
                    {
                        return board.finalScore * number;
                    }
                }
            }
            return 0;
        }
        public int LoseBingo()
        {
            winningBoards = new List<BingoBoard>();
            foreach (int number in calledNumbers)
            {
                foreach (BingoBoard board in bingoBoards)
                {
                    if (!winningBoards.Contains(board))
                    {
                        board.CallNumber(number);
                        if (board.CheckWin())
                        {
                            winningBoards.Add(board);
                        }
                        if (winningBoards.Count == bingoBoards.Count)
                        {
                            return board.finalScore * number;
                        }
                    }
                }
            }
            return 0;
        }
    }
    public class BingoBoard
    {
        public int finalScore {
            get
            {
                int score = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (calledNumbers[x, y] == 0) { score += board[x, y]; }
                    }
                }
                return score;
            }
        }
        public int[,] board { get; set; }
        public int[,] calledNumbers { get; set; }
        public int width { get;  set; }
        public int height { get; set; }
        
        public BingoBoard(int width, int height, int[,] boardData)
        {
            board = boardData;
            this.width = width;
            this.height = height;
            calledNumbers = new int[width, height];
        }
        public void CallNumber(int number)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (board[x, y] == number)
                    {
                        calledNumbers[x, y] = 1;
                        return;
                    }
                }
            }
        }
        public bool CheckWin()
        {
            int value = 0;
            //check rows
            for(int y = 0; y < height; y++)
            {
                value = 0;
                for(int x = 0; x < width; x++)
                {
                    value += calledNumbers[x, y];
                }
                if (value == width) { return true; }
            }
            //check columns
            for (int x = 0; x < width; x++)
            {
                value = 0;
                for (int y = 0; y < height; y++)
                {
                    value += calledNumbers[x, y];
                }
                if (value == height) { return true; }
            }
            return false;
        }
    }



    public class BingoSpace
    {
        public int x { get; set; }
        public int y { get; set; }
        public int value { get; set; }
        public BingoSpace(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }
    }


}
