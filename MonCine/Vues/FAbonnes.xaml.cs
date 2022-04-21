#region MÉTADONNÉES

// Nom du fichier : FAbonnes.xaml.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

#endregion

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour FAbonnes.xaml
    /// </summary>
    public partial class FAbonnes : Page
    {
        #region ATTRIBUTS

        private IMongoClient _client;
        private IMongoDatabase _db;
        private List<Abonne> _abonnes;
        private DALAbonne _dalAbonne;

        #endregion

        #region CONSTRUCTEURS

        public FAbonnes(IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _dalAbonne = new DALAbonne(_client, _db);
            _abonnes = _dalAbonne.ObtenirAbonnes();

            LstAbonnes.Items.Clear();

            _abonnes.OrderByDescending(x => x.NbSeances).ToList().ForEach(film => LstAbonnes.Items.Add(film));
        }

        #endregion

        #region MÉTHODES

        private void LstAbonnes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                OffrirRecompense offrirRecompense = new OffrirRecompense(_client, _db, (Abonne)item);
                offrirRecompense.Show();
            }
        }
        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        #endregion
    }
}