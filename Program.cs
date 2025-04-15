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
        private Random _random = new Random();

        private Stack<Card> _cards = new Stack<Card>();

        public Deck()
        {
            Fill();
            Shuffle();
        }

        public int NumberOfCards => _cards.Count;

        public Card GetLastCard()
        {
            return _cards.Peek();
        }

        public void DeleteCard()
        {
            _cards.Pop();
        }

        private void Fill()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Push(new Card(suit, rank));
                }
            }
        }

        private void Shuffle()
        {
            List<Card> tempCards = new List<Card>(_cards);
            int secondElementOfArray = 1;

            for (int i = _cards.Count - 1; i > secondElementOfArray; i--)
            {
                Card tempCard = tempCards[i];
                int randomNumber = _random.Next(_cards.Count);

                tempCards[i] = tempCards[randomNumber];
                tempCards[randomNumber] = tempCard;
            }

            _cards = new Stack<Card>(tempCards);
            Console.WriteLine("Карты перемешаны");
        }
    }
}
