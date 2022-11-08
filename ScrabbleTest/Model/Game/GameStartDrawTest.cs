using NUnit.Framework;
using Scrabble2018;
using Scrabble.Model;
using Scrabble.Model.Game;
using System;

namespace ScrabbleTest
{
    public class GameStartDrawTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameStartDraw_Draw_Should_Pass()
        {
            GameStartDraw.Draw();
            Assert.IsTrue(true);

        }
    }
}