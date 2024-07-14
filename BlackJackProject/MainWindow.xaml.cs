using BlackJackProject.Blackjack;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;

namespace BlackJackProject
{
    public partial class MainWindow : Window
    {
        BlackJackProject.Blackjack.Blackjack game = new BlackJackProject.Blackjack.Blackjack();

        public MainWindow()
        {
            InitializeComponent();
            game.Init();
            game.DealInitialCards();
            UpdateUI(false);
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            game.PlayerHand.Add(game.DealCard());
            UpdateUI(false);

            if (game.IsBust(game.PlayerHand))
            {
                MessageBox.Show("Player busts! Dealer wins.");
                ResetGame();
            }
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            while (game.CalculateHandValue(game.DealerHand) < 17)
            {
                game.DealerHand.Add(game.DealCard());
            }

            UpdateUI(true);

            if (game.IsBust(game.DealerHand))
            {
                MessageBox.Show("Dealer busts! Player wins.");
            }
            else
            {
                int playerValue = game.CalculateHandValue(game.PlayerHand);
                int dealerValue = game.CalculateHandValue(game.DealerHand);

                if (playerValue > dealerValue)
                {
                    MessageBox.Show("Player wins!");
                }
                else if (dealerValue > playerValue)
                {
                    MessageBox.Show("Dealer wins!");
                }
                else
                {
                    MessageBox.Show("It's a tie!");
                }
            }

            ResetGame();
        }

        private void ShowCard(string filename, Panel panel)
        {
            string path = "D:\\Sheridan CST-SDNE\\Semester 4\\PROG32356 .Net Technologies using C#\\Projects\\BlackJackProject\\BlackJackProject\\card_images\\JPEG\\";

            BitmapImage bitmap = new BitmapImage(new Uri(System.IO.Path.Combine(path, filename)));
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Source = bitmap;
            panel.Children.Add(image);
        }

        private void UpdateUI(bool revealDealerHand)
        {
            PlayerPanel.Children.Clear();
            DealerPanel.Children.Clear();

            foreach (var card in game.PlayerHand)
            {
                ShowCard(card.GetImageFilename(), PlayerPanel);
            }

            if (revealDealerHand)
            {
                foreach (var card in game.DealerHand)
                {
                    ShowCard(card.GetImageFilename(), DealerPanel);
                }
            }
            else
            {
                ShowCard(game.DealerHand[0].GetImageFilename(), DealerPanel);
                ShowCard("back_cards-07.jpg", DealerPanel); // assuming this is the back of the card image filename
            }
        }

        private void ResetGame()
        {
            game = new BlackJackProject.Blackjack.Blackjack();
            game.Init();
            game.DealInitialCards();
            UpdateUI(false);
        }
    }
}