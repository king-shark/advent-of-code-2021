using System;
using System.Collections.Generic;

namespace AdventCalendar
{
    public class Bingo
    {
        public List<BingoBoard> bingoBoards { get; set; }
        public List<BingoBoard> winningBoards { get; set; }
        public List<int> calledNumbers { get; set; }
        public Bingo()
        {
            bingoBoards = new List<BingoBoard>();
            calledNumbers = new List<int>();
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
