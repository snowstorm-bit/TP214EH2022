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
using System.Linq;
using System.Windows.Navigation;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour FAbonnes.xaml
    /// </summary>
    public partial class FAbonnes : Page
    {
        private IMongoClient _client;
        private IMongoDatabase _db;
        private List<Abonne> _abonnes;
        private DALAbonne _dalAbonne;
        public FAbonnes(IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _dalAbonne = new DALAbonne(_client, _db);
            _abonnes = _dalAbonne.ObtenirAbonnes();

            LstAbonnes.Items.Clear();

            _abonnes.OrderByDescending(x=> x.NbSeances).ToList().ForEach(film => LstAbonnes.Items.Add(film));

            //foreach (Abonne abonne in _abonnes)
            //{
            //    lstAbonnes.Items.Add(abonne);
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
            //}
        }

        private void LstAbonnes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                OffrirRecompense offrirRecompense = new OffrirRecompense(_client, _db, (Abonne)item);
                offrirRecompense.Show();
            }
        }
    }
}

