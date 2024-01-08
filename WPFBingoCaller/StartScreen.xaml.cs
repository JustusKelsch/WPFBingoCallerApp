using BingoCallerLibrary;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Window, ISaveResults {

        public StartScreen()
        {
            InitializeComponent();
        }

        public bool SaveResults() {

            bool isSavingResults = true;

            if (trackResultsCheckBox.IsChecked == false) {

                isSavingResults = false;

            }

            return isSavingResults;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

            MainWindow mainWindow = new MainWindow(this);

            this.Close();
            mainWindow.Show();

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {

            
            
        }
    }
}
