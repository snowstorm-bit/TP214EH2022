using MonCine.Data.Classes;
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
    /// Logique d'interaction pour OffrirRecompense.xaml
    /// </summary>
    public partial class OffrirRecompense : Window
    {
        private Cinematheque _cinematheque;
        private Abonne _abonne;

        public OffrirRecompense(Cinematheque pCinematheque, Abonne pAbonneSelectionner)
        {
            _cinematheque = pCinematheque;
            _abonne = pAbonneSelectionner;
            InitializeComponent();
            AfficherInformationsRecompense();
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AfficherInformationsRecompense()
        {
            txtNomAbonne.Text = _abonne.Nom;
            foreach (Recompense recompense in _cinematheque.Recompenses)
            {
                optRecompense.Items.Add(recompense);
            }
        }
    }
}
