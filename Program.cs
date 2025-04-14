using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DeckCards deckCards = new DeckCards();
            Player player = new Player(deckCards);

            int.TryParse(Console.ReadLine(), out int userInpt);

            Croupier croupier = new Croupier(userInpt, deckCards, player);
            player.ShowAllCards();

            Console.ReadKey();
        }
    }

    class Croupier
    {
        private Player _player;
        private DeckCards _deckCards;
        public int NumberOfCards { get; private set; }

        public Croupier(int numberOfCards, DeckCards deckCards, Player player)
        {
            _deckCards = deckCards;
            _player = player;
            NumberOfCards = numberOfCards;

            deckCards.AddCards(NumberOfCards);
        }
    }

    class Player
    {
        private DeckCards _deckCards;

        public Player(DeckCards deckCards)
        {
            _deckCards = deckCards;
        }

        public void ShowAllCards()
        {
            foreach (var card in _deckCards._cards)
            {
                Console.WriteLine($"Номер карты: {card.Number}");
            }
        }
    }

    class DeckCards
    {
        public List<Card> _cards { get; } = new List<Card>();

        public void AddCards(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                _cards.Add(new Card(i + 1));
            }
        }
    }

    class Card
    {
        public Card(int number)
        {
            Number = number;
        }

        public int Number { get; private set; }
    }
}
