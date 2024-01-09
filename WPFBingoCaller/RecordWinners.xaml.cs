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

        ISaveWinner _parent;

        public RecordWinners(ISaveWinner parent)
        {
            InitializeComponent();

            _parent = parent;

            previousWinnersComboBox.SelectedIndex = 0;

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

            previousWinnersComboBox.ItemsSource = _parent.GetAllWinners();

        }

        private void saveWinnerButton_Click(object sender, RoutedEventArgs e) {

            PersonModel model = new PersonModel {

                Name = winnerNameText.Text,
                Wins = +1

            };

            if (previousWinnersComboBox.SelectedIndex > 0) {

                _parent.UpdateWins(_parent.GetPerson(previousWinnersComboBox.SelectedIndex));
                previousWinnersComboBox.SelectedIndex = 0;

            }
            else if (_parent.ContainsPerson(model) == true) {

                MessageBox.Show("That name has already been used in the current session. Please enter a new name or select an existing name from the drop down.", "Name already taken.", MessageBoxButton.OK, MessageBoxImage.Error);
                winnerNameText.Focus();

            }
            else {

                _parent.SaveWinner(model);

                winnerNameText.Clear();
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

        private void closeRecordWinnersButton_Click(object sender, RoutedEventArgs e) {

            this.Close();

        }
    }
}
