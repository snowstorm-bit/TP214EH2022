#region MÉTADONNÉES

// Nom du fichier : OffrirRecompense.xaml.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Windows;
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;

#endregion

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour OffrirRecompense.xaml
    /// </summary>
    public partial class OffrirRecompense : Window
    {
        #region ATTRIBUTS

        private Abonne _abonne;
        private List<Recompense> _recompenses;
        private IMongoClient _client;
        private IMongoDatabase _db;
        private DALFilm _dalFilm;
        private DALRecompense _dalRecompense;

        #endregion

        #region CONSTRUCTEURS

        public OffrirRecompense(IMongoClient pClient, IMongoDatabase pDb, Abonne pAbonneSelectionner)
        {
            _client = pClient;
            _db = pDb;
            _abonne = pAbonneSelectionner;

            InitializeComponent();
            InitialiserObjets();
            InitialiserComposantes();
        }

        #endregion

        #region MÉTHODES

        private void InitialiserObjets()
        {
            try
            {
                _dalFilm = new DALFilm(_client, _db);
                _dalRecompense = new DALRecompense(_dalFilm, _client, _db);
                _recompenses = _dalRecompense.ObtenirRecompenses();
            }
            catch (Exception e)
            {
                AfficherMsgErreur(e.Message);
            }
        }

        private void InitialiserComposantes()
        {
            TxtNomAbonne.Text = _abonne.Nom;
            _recompenses.ForEach(x => OptRecompense.Items.Add(x));
        }

        /// <summary>
        /// Permet d'afficher le message reçu en paramètre dans un dialogue pour afficher ce dernier.
        /// </summary>
        /// <param name="pMsg">Message d'erreur à afficher</param>
        private void AfficherMsgErreur(string pMsg)
        {
            MessageBox.Show(
                "Une erreur s'est produite !!\n\n" + pMsg, "Erreur",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOffrirRecompense_Click(object pSender, RoutedEventArgs pE)
        {
            MessageBox.Show(
                "La fonctionnalité n'a pas encore été implémentée. De ce fait, le bouton ne fonctionne pas",
                "Bouton non fonctionnel",
                MessageBoxButton.OK,
                MessageBoxImage.Warning
            );
        }

        #endregion
    }
}