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
using MonCine.Data;
using MonCine.Data.Classes;
using System.Data;
using System.Windows.Navigation;
using MongoDB.Bson;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour FAbonnes.xaml
    /// </summary>
    public partial class FAbonnes : Page
    {
        private List<Abonne> _abonnes;
        private Cinematheque _cinematheque;
        public FAbonnes(DAL pDal, Cinematheque pCinematheque)
        {
            InitializeComponent();
            _cinematheque = pCinematheque;
            _abonnes = pCinematheque.Abonnes;
            foreach (Abonne abonne in _abonnes)
            {
                lstAbonnes.Items.Add(abonne);
                //string itemListRealisateur = "";
                //Preference preferenceAbonne = abonne.Preference;
                //foreach (ObjectId realisateurId in preferenceAbonne.RealisateursId)
                //{
                //    foreach (Realisateur realisateur in realisateursBD)
                //    {
                //        if (realisateur.Id == realisateurId)
                //        {
                //            itemListRealisateur += " " + realisateur.Nom;
                //            break;
                //        }
                //    }
                //}
                //ListViewRealisateurs.Items.Add(itemListRealisateur);
                //string itemListActeur = "";
                //foreach (ObjectId acteurId in preferenceAbonne.ActeursId)
                //{
                //    foreach (Acteur acteur in acteursBD)
                //    {
                //        if (acteur.Id == acteurId)
                //        {
                //            itemListActeur += " " + acteur.Nom;
                //            break;
                //        }
                //    }
                //}
                //ListViewActeurs.Items.Add(itemListActeur);
            }
        }

        private void lstAbonnes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                OffrirRecompense offrirRecompense = new OffrirRecompense(_cinematheque, (Abonne)item);
                offrirRecompense.Show();
            }
        }
    }
}

