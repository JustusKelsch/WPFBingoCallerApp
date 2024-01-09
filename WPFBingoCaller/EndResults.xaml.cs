using BingoCallerLibrary;
using BingoCallerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EndResults.xaml
    /// </summary>
    public partial class EndResults : Window
    {

        ISaveWinner _parent;

        public EndResults(ISaveWinner parent)
        {
            InitializeComponent();

            _parent = parent;

            ListWinners();
        }

        public ObservableCollection<PersonModel> SortWinners(ObservableCollection<PersonModel> winners) {

            PersonModel person = new PersonModel {

                Name = "",
                Wins = 0

            };

            ObservableCollection<PersonModel> tempWinners = new ObservableCollection<PersonModel>();

            for (int i = 0; i < winners.Count; i++) {

                foreach (var winner in winners) {

                    if (winner.Wins > person.Wins) {

                        if (tempWinners.Contains(winner)) {

                            continue;

                        }
                        else {

                            person = winner;

                        }

                    }

                }

                tempWinners.Add(person);

                person = new PersonModel {

                    Name = "",
                    Wins = 0

                };

            }

            winners = tempWinners;

            return winners;
        
        }

        public void ListWinners() {

            ObservableCollection<PersonModel> winners = _parent.GetAllWinners();

            winners.Remove(winners.First());

            winners = SortWinners(winners);

            winnersListBox.ItemsSource = winners;

        }

        private void closeApplicationButton_Click(object sender, RoutedEventArgs e) {

            this.Close();

        }
    }
}
