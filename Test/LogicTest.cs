// <copyright file="LogicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Test
{
    using System.Collections.ObjectModel;
    using BusinessLogic;
    using Data;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// TestClass of the logic.
    /// </summary>
    [TestFixture]
    public class LogicTest
    {
        private Mock<ICardRepository> mock;
        private GameLogic logic;
        private Player p;
        private Player comp;

        /// <summary>
        /// Creates a new logic.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            this.logic = new GameLogic();
        }

        /// <summary>
        /// Mock player, comp and deck.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.p = new Player()
            {
                Money = 1000, Hand = new Hand()
                {
                    Cards = new ObservableCollection<Card>()
                    {
                new Card("C4", "testimage1.jpg", 4),
                new Card("C5", "testimage1.jpg", 5),
                    },
                },
            };
            this.comp = new Player();

            this.mock = new Mock<ICardRepository>();
            Card c1 = new Card("C2", "testimage1.jpg", 2);
            Card c2 = new Card("C3", "testimage1.jpg", 3);
            Card c3 = new Card("H4", "testimage1.jpg", 4);
            Card c4 = new Card("HK", "testimage1.jpg", 10);
            Card c5 = new Card("DA", "testimage1.jpg", 11);
            Card c6 = new Card("D3", "testimage1.jpg", 3);

            this.mock.Setup(m => m.GetDeck()).Returns(new ObservableCollection<Card>() { c1, c2, c3, c4, c5, c6 });
        }

        /// <summary>
        /// Test for hitcommand.
        /// </summary>
        [Test]
        public void WhenClickHitCommand_DeckIsDownwsizingByOne()
        {
            int bet = 20;
            this.logic.HitCommand(this.p, this.comp, ref bet, this.mock.Object.GetDeck());
            Assert.That(this.mock.Object.GetDeck().Count, Is.EqualTo(5));
        }

        /// <summary>
        /// Test for betcommand.
        /// </summary>
        [Test]
        public void WhenBetCommand_PlayerMoneyIsReduces()
        {
            int bet = 32;
            int money = this.p.Money - bet;
            this.logic.BetCommand(this.p, this.comp, ref bet, this.mock.Object.GetDeck());
            Assert.That(this.p.Money, Is.EqualTo(money));
        }

        /// <summary>
        /// Test for standcommand.
        /// </summary>
        [Test]
        public void WhenStandCommand_PlayerHandIsNotActive()
        {
            int bet = 100;
            this.logic.StandCommand(this.p, this.comp, ref bet, this.mock.Object.GetDeck());
            Assert.That(this.p.Hand.IsNotActive, Is.EqualTo(true));
        }

        /// <summary>
        /// Test for doublecommand.
        /// </summary>
        [Test]
        public void WhenDoubleCommand_BetIsDoubled()
        {
            int bet = 100;
            int money = this.p.Money - bet;
            this.logic.DoubleCommand(this.p, this.comp, ref bet, this.mock.Object.GetDeck());
            Assert.That(bet, Is.EqualTo(0));
            Assert.That(this.p.Money, Is.EqualTo(money));
        }

        /// <summary>
        /// Test for surrendercommand.
        /// </summary>
        [Test]
        public void WhenSurrenderCommand_BetIsHalf_PlayerMoneyChanges()
        {
            int bet = 100;
            this.logic.SurrenderCommand(this.p, this.comp, ref bet, this.mock.Object.GetDeck());
            Assert.That(bet, Is.EqualTo(0));
            Assert.That(this.p.Money, Is.EqualTo(1050));
        }

        /// <summary>
        /// Test for splitcommand.
        /// </summary>
        [Test]
        public void WhenSplitCommand_SecondHandCountOne_PlayerMoneyReduces()
        {
            int bet = 100;
            this.logic.SplitCommand(this.p, ref bet);
            Assert.That(this.p.SecondHand.Cards.Count, Is.EqualTo(1));
            Assert.That(this.p.Money, Is.EqualTo(900));
        }

        /// <summary>
        /// Test for hitfirsthandcommand.
        /// </summary>
        [Test]
        public void WhenHitFirstHandCommand_PlayerHandIncrements()
        {
            this.logic.HitFirstHand(this.p, this.comp, this.mock.Object.GetDeck(), 100);
            Assert.That(this.p.Hand.Cards.Count, Is.EqualTo(3));
        }

        /// <summary>
        /// Test for hitsecondhandcommand.
        /// </summary>
        [Test]
        public void WhenHitSecondHandCommand_PlayerSecondHandIncrements()
        {
            int bet = 100;
            this.logic.SplitCommand(this.p, ref bet);
            this.logic.HitSecondHand(this.p, this.comp, this.mock.Object.GetDeck(), 100);
            Assert.That(this.p.SecondHand.Cards.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// Test for standfirsthandcommand.
        /// </summary>
        [Test]
        public void WhenStandFirstHandCommand_PlayerHandIsNotActive()
        {
            int bet = 100;
            this.logic.StandFirstHand(this.p, this.comp, this.mock.Object.GetDeck(), ref bet);
            Assert.That(this.p.Hand.IsNotActive, Is.EqualTo(true));
        }

        /// <summary>
        /// Test for standsecondhandcommand.
        /// </summary>
        [Test]
        public void WhenStandSecondHandCommand_PlayerSecondHandIsNotActive()
        {
            int bet = 100;
            this.logic.StandSecondHand(this.p, this.comp, this.mock.Object.GetDeck(), ref bet);
            Assert.That(this.p.SecondHand.IsNotActive, Is.EqualTo(true));
        }
    }
}
