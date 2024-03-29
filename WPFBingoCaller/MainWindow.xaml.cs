﻿using BingoCallerLibrary;
using BingoCallerLibrary.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WPFBingoCaller {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    
    public partial class MainWindow : Window, ISaveWinner {

        private List<string> bingoCallOptions = new List<string>();
        private BindingList<string> bColumn = new BindingList<string>();
        private BindingList<string> iColumn = new BindingList<string>();
        private BindingList<string> nColumn = new BindingList<string>();
        private BindingList<string> gColumn = new BindingList<string>();
        private BindingList<string> oColumn = new BindingList<string>();

        ObservableCollection<PersonModel> winners = new ObservableCollection<PersonModel>();

        ITrackResults _parent;

        public MainWindow(ITrackResults parent) {
            InitializeComponent();

            _parent = parent;
            TrackResultsExecute();

            PersonModel person = new PersonModel {
                Name = "<new>",
                Wins = 0
            };

            SaveWinner(person);

            AddBingoCallOptions();
        }

        private void AddBingoCallOptions() {

            bingoCallOptions = BuildBingoBoard.FillInBingoSquares();

        }

        private void GenerateRandomSquare() {

            Random rand = new Random();

            if (bingoCallOptions.Count > 0) {

                currentCallText.Text = bingoCallOptions[rand.Next(bingoCallOptions.Count)];

            }

        }

        private void newCallButton_Click(object sender, RoutedEventArgs e) {

            if (bingoCallOptions.Count > 0) {

                AddSquareToListBox();
                GenerateRandomSquare();

            }
            else {

                MessageBox.Show("You have drawn all of the available options.", "No more available options", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            clearCardsBottomText.Text = clearCardsTopListBox.Text = "";

        }

        private void GenerateListBox(BindingList<string> column) {

            column.Add(currentCallText.Text);
            SortList(column);

        }

        private void SortList(BindingList<string> listToSort) {

            string letter = listToSort.First().Substring(0, 1);
            List<int> sortedList = new List<int>();

            foreach (string item in listToSort) {

                sortedList.Add(int.Parse(item.Substring(1, item.Length - 1)));

            }

            sortedList.Sort();
            listToSort.Clear();

            foreach (int item in sortedList) {

                listToSort.Add($"{letter}{item}");

            }

        }

        private void GenerateListBoxDataSource(ListBox left, ListBox right, BindingList<string> column) {

            left.ItemsSource = column;
            right.ItemsSource = column;

        }

        private void AddSquareToListBox() {

            if (currentCallText.Text.StartsWith("B")) {

                GenerateListBox(bColumn);
                GenerateListBoxDataSource(bLeftColumnListBox, bRightColumnListBox, bColumn);
                
            }
            else if (currentCallText.Text.StartsWith("I")) {

                GenerateListBox(iColumn);
                GenerateListBoxDataSource(iLeftColumnListBox, iRightColumnListBox, iColumn);

            }
            else if (currentCallText.Text.StartsWith("N")) {

                GenerateListBox(nColumn);
                GenerateListBoxDataSource(nLeftColumnListBox, nRightColumnListBox, nColumn);

            }
            else if (currentCallText.Text.StartsWith("G")) {

                GenerateListBox(gColumn);
                GenerateListBoxDataSource(gLeftColumnListBox, gRightColumnListBox, gColumn);

            }
            else if (currentCallText.Text.StartsWith("O")) {

                GenerateListBox(oColumn);
                GenerateListBoxDataSource(oLeftColumnListBox, oRightColumnListBox, oColumn);

            }

            RemoveSquare(currentCallText.Text);

        }

        private void RemoveSquare(string square) {

            bingoCallOptions.Remove(square);

        }

        private void ClearColumns() {

            bColumn.Clear();
            iColumn.Clear();
            nColumn.Clear();
            gColumn.Clear();
            oColumn.Clear();

        }

        private void newCardButton_Click(object sender, RoutedEventArgs e) {

            ClearColumns();
            AddBingoCallOptions();
            currentCallText.Text = "";

            clearCardsBottomText.Text = clearCardsTopListBox.Text = "Clear your cards";

        }

        public void TrackResultsExecute() {

            if (_parent.TrackResults()) {

                recordBingoButton.IsEnabled = true;
                recordBingoButton.Visibility = Visibility.Visible;

            }
            else {

                endGameButton.HorizontalAlignment = HorizontalAlignment.Center;

            }

        }

        private void recordBingoButton_Click(object sender, RoutedEventArgs e) {

            RecordWinners recordWinners = new RecordWinners(this);

            recordWinners.ShowDialog();

            

        }

        private void endGameButton_Click(object sender, RoutedEventArgs e) {

            if (_parent.TrackResults()) {

                EndResults endResults = new EndResults(this);

                endResults.Show();

            }

            this.Close();

        }

        public void SaveWinner(PersonModel person) {

            winners.Add(person);

        }

        public void UpdateWins(PersonModel person) {

            int index = 0;

            if (person.Name != "<new>") {

                index = winners.IndexOf(person);
                winners[index].Wins++;

            }

        }

        public bool ContainsPerson(PersonModel person) {

            bool containsName = false;

            foreach (PersonModel model in winners) {

                if (model.Name == person.Name) {

                    containsName = true;

                }

            }

            return containsName;

        }

        public ObservableCollection<PersonModel> GetAllWinners() {

            return winners;

        }

        public PersonModel GetPerson(int index) {

            PersonModel output = new PersonModel();
        
            if (ContainsPerson(winners[index])) {

                output = winners[index];

            }

            return output;
        
        }

    }
}
