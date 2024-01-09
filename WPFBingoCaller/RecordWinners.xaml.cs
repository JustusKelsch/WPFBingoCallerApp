using BingoCallerLibrary;
using BingoCallerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFBingoCaller
{
    /// <summary>
    /// Interaction logic for RecordWinners.xaml
    /// </summary>
    public partial class RecordWinners : Window
    {

        //private BindingList<(string, int)> winners = new BindingList<(string name, int count)>();
        ObservableCollection<PersonModel> winners = new ObservableCollection<PersonModel>();

        public RecordWinners()
        {
            InitializeComponent();

            winners.Add(new PersonModel {
                Name = "<new>",
                Wins = 0
            });

            WireUpDictionary();

        }

        public void CanSaveWinner() {

            if (string.IsNullOrWhiteSpace(winnerNameText.Text) == false || previousWinnersComboBox.SelectedIndex > 0) {

                saveWinnerButton.IsEnabled = true;

            }
            else {

                saveWinnerButton.IsEnabled = false;

            }

        }

        public void CanAddWinner() {

            if (previousWinnersComboBox.SelectedIndex > 0) {

                winnerNameText.IsEnabled = false;
                winnerNameText.Clear();

            }
            else {

                winnerNameText.IsEnabled = true;

            }

        }

        private void WireUpDictionary() {

            previousWinnersComboBox.ItemsSource = winners;

        }

        private bool ContainsName(string name) {

            bool containsName = false;

            foreach (PersonModel model in winners) {

                if (model.Name == name) {

                    containsName = true;

                }

            }

            return containsName;

        }

        private void saveWinnerButton_Click(object sender, RoutedEventArgs e) {

            if (ContainsName(winnerNameText.Text) == false) {

                if (previousWinnersComboBox.SelectedIndex > 0) {

                    winners[previousWinnersComboBox.SelectedIndex].Wins++;

                }
                else {

                    winners.Add(new PersonModel {
                        Name = winnerNameText.Text,
                        Wins = 1
                    });

                }

                previousWinnersComboBox.SelectedItem = "<new>";
                winnerNameText.Clear();
                winnerNameText.Focus();

            }
            else {

                MessageBox.Show("That name has already been used in the current session. Please enter a new name.", "Name already taken.", MessageBoxButton.OK, MessageBoxImage.Error);
                winnerNameText.Focus();

            }

        }

        private void winnerNameText_TextChanged(object sender, TextChangedEventArgs e) {

            CanSaveWinner();

        }

        private void previousWinnersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            CanSaveWinner();
            CanAddWinner();

        }
    }
}
