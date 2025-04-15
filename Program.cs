using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deckCards = new Deck();
            Player player = new Player();

            Croupier croupier = new Croupier(deckCards, player);

            croupier.SetCardsToPlayer();

            player.ShowAllCards();

            Console.ReadKey();
        }
    }

    public enum Suit
    {
        Spades,
        Clubs,
        Diamonds,
        Hearts
    }

    public enum Rank
    {
        Six = 6,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    class Croupier
    {
        private Player _player;
        private Deck _deck;

        public Croupier(Deck deckCards, Player player)
        {
            _deck = deckCards;
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
                    if (userInpt > 0 && userInpt <= _deck.NumberOfCards)
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
                _player.AddCardToDeck(_deck.GetLastCard());
                _deck.DeleteCard();
            }
        }
    }

    class Player
    {
        private List<Card> _cards = new List<Card>();

        public void ShowAllCards()
        {
            Console.WriteLine("Карты игрока");

            foreach (var card in _cards)
            {
                Console.WriteLine($"Значение: {(int)card.Rank} Масть: {card.Suit}");
            }
        }

        public void AddCardToDeck(Card card)
        {
            _cards.Add(card);
        }
    }

    class Deck
    {
        Random random = new Random();

        private List<Card> _cards = new List<Card>();

        public Deck()
        {
            Fill();
            Shuffle();
        }

        public int NumberOfCards => _cards.Count;

        public Card GetLastCard()
        {
            var lastCard = _cards[_cards.Count - 1];

            return lastCard;
        }

        public void DeleteCard()
        {
            _cards.Remove(GetLastCard());
        }

        public void Fill()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }

        private void Shuffle()
        {
            int secondElementOfArray = 1;

            for (int i = _cards.Count - 1; i > secondElementOfArray; i--)
            {
                Card tempCard = _cards[i];
                int randomNumber = random.Next(_cards.Count);

                _cards[i] = _cards[randomNumber];
                _cards[randomNumber] = tempCard;
            }

            Console.WriteLine("Карты перемешаны");
        }
    }

    class Card
    {
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }
    }
}
