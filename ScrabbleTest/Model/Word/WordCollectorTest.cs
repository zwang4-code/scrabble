using NUnit.Framework;
using Scrabble.Model;
using Scrabble.Model.Word;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrabbleTest
{
    public class WordCollectorTest
    {
        [Test]
        public static void VCollect_ReturnZero_GivenNullGameState()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;

            int result = WordCollector.VCollect(1, 2, bc, gs);
            Assert.AreEqual(0, result);
        }

        [Test]
        public static void VCollect_ReturnValidScore_GivenValidWord()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int i = 13, j = 6;
            bc[i, j] = 'T';
            bc[i+1, j] = 'O';

            int result = WordCollector.VCollect(i, j, bc, gs);
            Assert.AreEqual(2, result);
        }

        [Test]
        public static void VCollect_ReturnNegativeScore_GivenInvalidWord()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int i = 13, j = 6;
            bc[i, j] = 'Z';
            bc[i + 1, j] = 'X';

            int result = WordCollector.VCollect(i, j, bc, gs);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public static void VCollect_ReturnValidScore_GivenVFound()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int i = 12, j = 6;
            bc[i, j] = 'A';
            bc[i + 1, j] = 'S';
            bc[i + 2, j] = '\0';

            int result = WordCollector.VCollect(i, j, bc, gs);
            Assert.AreEqual(3, result);
        }

        [Test]
        public static void VCollect_ReturnNegativeScore_GivenVNotFound()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int i = 12, j = 6;
            bc[i, j] = 'Z';
            bc[i + 1, j] = 'X';
            bc[i + 2, j] = '\0';

            int result = WordCollector.VCollect(i, j, bc, gs);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public static void HCollect_ReturnZero_GivenNullGameState()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;

            int result = WordCollector.HCollect(1, 2, bc, gs);
            Assert.AreEqual(0, result);
        }

        [Test]
        public static void HCollect_ReturnValidScore_GivenValidWord()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int j = 13, i = 6;
            bc[i, j] = 'T';
            bc[i, j + 1] = 'O';

            int result = WordCollector.HCollect(i, j, bc, gs);
            Assert.AreEqual(2, result);
        }

        [Test]
        public static void HCollect_ReturnNegativeScore_GivenInvalidWord()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int j = 13, i = 6;
            bc[i, j] = 'Z';
            bc[i, j + 1] = 'X';

            int result = WordCollector.HCollect(i, j, bc, gs);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public static void HCollect_ReturnValidScore_GivenVFound()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int j = 12, i = 6;
            bc[i, j] = 'A';
            bc[i, j + 1] = 'S';
            bc[i, j + 2] = '\0';

            int result = WordCollector.HCollect(i, j, bc, gs);
            Assert.AreEqual(3, result);
        }

        [Test]
        public static void HCollect_ReturnNegativeScore_GivenVNotFound()
        {
            GameState gs = new GameState();
            char[,] bc = gs.BoardChar;
            int j = 12, i = 6;
            bc[i, j] = 'Z';
            bc[i, j + 1] = 'X';
            bc[i, j + 2] = '\0';

            int result = WordCollector.HCollect(i, j, bc, gs);
            Assert.AreEqual(-1, result);
        }
    }
}
