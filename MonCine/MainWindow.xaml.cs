#region MÉTADONNÉES

// Nom du fichier : MainWindow.xaml.cs
// Date de création : 2022-04-06
// Date de modification : 2022-04-10

#endregion

#region USING

using System.Windows;
using MonCine.Vues;

#endregion

namespace MonCine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region CONSTRUCTEURS

        public MainWindow()
        {
            InitializeComponent();
            _NavigationFrame.Navigate(new Accueil());
        }

        #endregion
    }
}