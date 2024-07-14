using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackProject.Blackjack
{
    public class Blackjack
    {
        public List<Card> Deck { get; private set; }
        public List<Card> PlayerHand { get; private set; }
        public List<Card> DealerHand { get; private set; }

        public Blackjack()
        {
            Deck = new List<Card>();
            PlayerHand = new List<Card>();
            DealerHand = new List<Card>();
        }

        public void Init()
        {
            String[] suits = { "Club", "Spade", "Hearts", "Diamond" };
            String[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    Deck.Add(new Card(suit, rank));
                }
            }

            ShuffleDeck();
        }

        private void ShuffleDeck()
        {
            Random rng = new Random();
            int n = Deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = Deck[k];
                Deck[k] = Deck[n];
                Deck[n] = value;
            }
        }

        public void DealInitialCards()
        {
            PlayerHand.Add(DealCard());
            DealerHand.Add(DealCard());
            PlayerHand.Add(DealCard());
            DealerHand.Add(DealCard());
        }

        public Card DealCard()
        {
            if (Deck.Count == 0) throw new InvalidOperationException("No cards left in the deck.");
            Card card = Deck[0];
            Deck.RemoveAt(0);
            return card;
        }

        public int CalculateHandValue(List<Card> hand)
        {
            int value = 0;
            int aceCount = 0;

            foreach (var card in hand)
            {
                value += card.GetBlackjackValue();
                if (card.Rank == "Ace") aceCount++;
            }

            while (aceCount > 0 && value + 10 <= 21)
            {
                value += 10;
                aceCount--;
            }

            return value;
        }

        public bool IsBust(List<Card> hand)
        {
            return CalculateHandValue(hand) > 21;
        }
    }
}
