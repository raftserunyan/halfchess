﻿using System;
using System.Threading;
using System.Collections.Generic;

namespace HalfChess
{
    class Program
    {
        static Board board;
        public static bool isMate = false;

        static void Main(string[] args)
        {
            board = new Board();
            board.Show();

            while (!isMate)
            {
                //Checking for mate
                if (!board.Pieces[0].HasSomewhereToGo)
                    Mate();

                if (!isMate)
                {
                    Console.WriteLine();
                    Console.Write("Enter new coordinates for the black king (example: 7 F): ");
                    string coordinates = Console.ReadLine();

                    try
                    {
                        board.Pieces[0].Move(coordinates);

                        if (board.WhitePieces.Count < 2)
                        {
                            Program.isMate = true;

                            board.Pieces[0].AvailableCells.Clear();
                            Console.Clear();
                            board.Show();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("You Won! Congratulations!!");
                            Console.ReadKey();
                        }

                        Thread.Sleep(1200);
                        try
                        {
                            SystemMakeMove();
                        }
                        catch (Exception e)
                        {
                            if (e.Message == "raf")
                                SystemMakeMove();
                        }

                        //Check for shakh
                        if (IsShakh())
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("System> Shakh!");
                            Console.ResetColor();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                }
            }
        }

        public static void Mate()
        {
            isMate = true;

            board.Pieces[0].AvailableCells.Clear();
            Console.Clear();
            board.Show();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MATE!");
            Console.ReadKey();
        }

        private static byte i = 1;
        private static byte ind
        {
            get
            {
                return i;
            }
            set
            {
                if (value >= board.WhitePieces.Count)
                    i = 1;
                else if (value < 1)
                    i = 1;
                else
                    i = value;
            }
        }
        private static void SystemMakeMove()
        {
            Piece piece = board.WhitePieces[ind++];

            List<int> lst = new List<int>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int c = 0; c < piece.AvailableCells.Count; c++)
                    {
                        if (board.Matrix[i, j] == piece.AvailableCells[c])
                        {
                            bool letgo = true;
                            foreach (object kcell in board.Pieces[0].EatableCells)
                            {
                                if (kcell == board.Matrix[i, j])
                                {
                                    letgo = false;
                                    break;
                                }
                            }

                            if (letgo)
                            {
                                if (i == board.Pieces[0].I || Math.Abs(i - board.Pieces[0].I) == 1)
                                {
                                    bool g = true;
                                    foreach (Piece item in board.WhitePieces)
                                    {
                                        if (item.I == i)
                                        {
                                            g = false;
                                            break;
                                        }
                                    }
                                    if (g)
                                        lst.Add(c);
                                }
                            }
                        }
                    }
                }
            }

            if (lst.Count > 0)
            {
                Random rnd = new Random();
                int indx = rnd.Next(0, lst.Count);

                bool t = true;
                for (int i = 0; i < 8; i++)
                {
                    if (t)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (board.Matrix[i, j] == piece.AvailableCells[lst[indx]])
                            {
                                piece.Move(i, j);
                                return;
                            }
                        }
                    }
                }
            }
            else
                throw new Exception("raf");
        }

        private static bool IsShakh()
        {
            foreach (Piece piece in board.Pieces)
            {
                if (piece.CanEat(board.Pieces[0]))
                    return true;
            }
            return false;
        }
    }
}