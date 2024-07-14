using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackProject.Blackjack
{
    public class Card
    {

        public string Suit;
        public string Rank;

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public int GetBlackjackValue()
        {
            if (int.TryParse(Rank, out int value))
            {
                // Same numeric value for all the number cards
                return value;
            }
            else if (Rank == "Ace")
            {
                // Ace can have 1 or 11 as value (How to do it?)
                return 1;
            }
            else
            {
                // Face cards have value of 10
                return 10;
            }
        }

        public string GetImageFilename()
        {
            string rankInitial = Rank;
            if (Rank == "Ace") rankInitial = "A";
            if (Rank == "Jack") rankInitial = "J";
            if (Rank == "Queen") rankInitial = "Q";
            if (Rank == "King") rankInitial = "K";

            string suitInitial = Suit.Substring(0, 1).ToUpper();

            return $"{rankInitial}{suitInitial}.jpg";
        }

    }
}
