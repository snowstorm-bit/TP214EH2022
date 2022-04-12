using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Films.xaml
    /// </summary>
    public partial class FFilms : Page
    {
        public FFilms()
        {
            InitializeComponent();
        }

        private void radioEstPasAffiche_Checked(object sender, RoutedEventArgs e)
        {
            txtTest.Text = "Film pas a l'affiche";
        }

        private void radioEstAffiche_Checked(object sender, RoutedEventArgs e)
        {
            txtTest.Text = "Film a l'affiche";
        }
    }
}
