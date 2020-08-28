﻿using System;

namespace HalfChess
{
    public class Board
    {
        public Piece KingBlack, KingWhite, QueenWhite, RookWhiteLeft, RookWhiteRight;
        public object[,] Matrix;

        public object this[byte i, byte j]
        {
            get { return Matrix[i, j]; }
            set { Matrix[i, j] = value; }
        }

        public Board()
        {
            Piece.board = this;
            Matrix = new object[8, 8];
            KingBlack = new Piece("King", "Black", 2, 2); // 0 4
            KingWhite = new Piece("King", "White", 6, 4); // 7 4
            RookWhiteLeft = new Piece("Rook", "White", 2, 4); // 7 0
            RookWhiteRight = new Piece("Rook", "White", 6, 6); // 7 7
            QueenWhite = new Piece("Queen", "White", 4, 4); // 7 3

            Create();
        }

        public void Create()
        {
            for (byte i = 0; i < 8; i++)
            {
                for (byte j = 0; j < 8; j ++)
                {
                    Matrix[i, j] = ' ';
                }
            }

            RookWhiteLeft.PutOnBoard();
            RookWhiteRight.PutOnBoard();
            KingBlack.PutOnBoard();
            KingWhite.PutOnBoard();
            QueenWhite.PutOnBoard();

            //Temporary, later you should add this into the Move() method
            RookWhiteLeft.SetAvailableCells();
            RookWhiteRight.SetAvailableCells();
            KingBlack.SetAvailableCells();
            KingWhite.SetAvailableCells();
            QueenWhite.SetAvailableCells();
        } //temporary shit in here

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("|   | A | B | C | D | E | F | G | H |   |");
            Console.WriteLine("-----------------------------------------");
            Console.ResetColor();

            for (byte i = 0; i < 8; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"| {8 - i} |");
                Console.ResetColor();

                for (byte j = 0; j < 8; j++)
                {
                    if (!(Matrix[i, j] is Piece))
                    {
                        switch ((i + j) % 2)
                        {
                            case 0:
                                {
                                    Console.BackgroundColor = ConsoleColor.Gray;
                                    break;
                                }
                            case 1:
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Piece piece = Matrix[i, j] as Piece;
                        switch(piece.Color)
                        {
                            case "Black":
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    break;
                                }
                            case "White":
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                }
                        }
                    }

                    ////
                    foreach (var item in KingBlack.AvailableCells)
                    {
                        if (Matrix[i, j] == item)
                            Console.BackgroundColor = ConsoleColor.Green;
                    }
                    ////
                    Console.Write($" {Matrix[i, j]} ");
                    Console.ResetColor();

                    if (j < 7)
                        Console.Write("|");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"| {8 - i} |");
                Console.ResetColor();
                Console.WriteLine();

                if (i == 7)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-----------------------------------------");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|   | A | B | C | D | E | F | G | H |   |");
            Console.WriteLine("-----------------------------------------");
            Console.ResetColor();
        }
    }
}