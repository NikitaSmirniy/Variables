using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DeckCards deckCards = new DeckCards();
            Player player = new Player();

            Croupier croupier = new Croupier(deckCards, player);

            croupier.SetCardsToPlayer();

            player.ShowAllCards();

            Console.ReadKey();
        }
    }

    class Croupier
    {
        private Player _player;
        private DeckCards _deckCards;

        public Croupier(DeckCards deckCards, Player player)
        {
            _deckCards = deckCards;
            _player = player;
        }

        public void SetCardsToPlayer()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.Write("Введите сколько карт должен получить игрок: ");

                if (int.TryParse(Console.ReadLine(), out int userInpt))
                {
                    if (userInpt > 0 && userInpt <= _deckCards.NumberOfCards)
                    {
                        PassCardsToPlayer(userInpt);
                        isOpen = false;

                        Console.WriteLine("Карты были добавлены в колоду игрока");
                    }
                    else
                    {
                        Console.WriteLine("Введено неверное кол-во карт! Попробуйте снова");
                    }
                }
                else
                {
                    Console.WriteLine("Введено нечисловое значение! Попробуйте снова");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void PassCardsToPlayer(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                _player.AddCardToDeck(_deckCards.GetCardFromDeck(i));
            }
        }
    }

    class Player
    {
        private List<Card> _playerCards = new List<Card>();

        public void ShowAllCards()
        {
            Console.WriteLine("Карты игрока");

            foreach (var card in _playerCards)
            {
                Console.WriteLine($"Номер карты: {card.Number}");
            }
        }

        public void AddCardToDeck(Card card)
        {
            _playerCards.Add(card);
        }
    }

    class DeckCards
    {
        private List<Card> _cards = new List<Card>();

        public DeckCards()
        {
            FillDeck(NumberOfCards);
        }

        public int NumberOfCards { get; private set; } = 36;

        public Card GetCardFromDeck(int index)
        {
            return _cards[index];
        }

        private void FillDeck(int numberOfCards)
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
