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
        private List<Abonne> abonnes;
        private DAL dal;
        private Cinematheque cinematheque;
        private List<Realisateur> realisateursBD;
        private List<Acteur> acteursBD;
        public FAbonnes(DAL pDal, Cinematheque pCinematheque)
        {
            InitializeComponent();
            dal = pDal;
            cinematheque = pCinematheque;
            cinematheque = dal.ObtenirCinematheque();
            realisateursBD = cinematheque.Realisateurs;
            acteursBD = cinematheque.Acteurs;
            abonnes = cinematheque.Abonnes;
            DataGridAbonnes.DataContext = abonnes;
            foreach (Abonne abonne in abonnes)
            {
                string itemListRealisateur = "";
                Preference preferenceAbonne = abonne.Preference;
                foreach (ObjectId realisateurId in preferenceAbonne.RealisateursId)
                {
                    foreach (Realisateur realisateur in realisateursBD)
                    {
                        if (realisateur.Id == realisateurId)
                        {
                            itemListRealisateur += " " + realisateur.Nom;
                            break;
                        }
                    }
                }
                ListViewRealisateurs.Items.Add(itemListRealisateur);
                string itemListActeur = "";
                foreach (ObjectId acteurId in preferenceAbonne.ActeursId)
                {
                    foreach (Acteur acteur in acteursBD)
                    {
                        if (acteur.Id == acteurId)
                        {
                            itemListActeur += " " + acteur.Nom;
                            break;
                        }
                    }
                }
                ListViewActeurs.Items.Add(itemListActeur);
            }
        }
    }
}

