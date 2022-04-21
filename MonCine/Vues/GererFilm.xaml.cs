#region MÉTADONNÉES

// Nom du fichier : GererFilm.xaml.cs
// Date de création : 2022-04-21
// Date de modification : 2022-04-21

#endregion

#region USING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using MonCine.Data.Classes;
using MonCine.Data.Classes.DAL;
using MongoDB.Bson;
using MongoDB.Driver;

#endregion

namespace MonCine.Vues
{
    public partial class GererFilm : Page
    {
        #region ATTRIBUTS

        private IMongoClient _client;
        private IMongoDatabase _db;
        private DALCategorie _dalCategorie;
        private DALActeur _dalActeur;
        private DALRealisateur _dalRealisateur;
        private DALFilm _dalFilm;
        private List<Categorie> _categories;
        private List<Acteur> _acteurs;
        private List<Realisateur> _realisateurs;
        private List<Acteur> _acteursChoisis;
        private List<Realisateur> _realisateursChoisis;
        private Film _film;
        private bool _actionEstAjout;

        #endregion

        #region CONSTRUCTEURS

        public GererFilm(IMongoClient pClient, IMongoDatabase pDb, Film pFilm = null)
        {
            InitializeComponent();
            _client = pClient;
            _db = pDb;
            _film = pFilm;
            _actionEstAjout = pFilm == null;

            InitialiserAttributs();
            InitialiserFormulaire();
            ChargerLsts();
        }

        #endregion

        #region MÉTHODES

        public void InitialiserAttributs()
        {
            _dalCategorie = new DALCategorie(_client, _db);
            _dalActeur = new DALActeur(_client, _db);
            _dalRealisateur = new DALRealisateur(_client, _db);
            _dalFilm = new DALFilm(_dalCategorie, _dalActeur, _dalRealisateur, _client, _db);
            _categories = _dalCategorie.ObtenirCategories();
            _acteurs = _dalActeur.ObtenirActeurs();
            _realisateurs = _dalRealisateur.ObtenirRealisateurs();
            _acteursChoisis = _actionEstAjout ? new List<Acteur>() : _film.Acteurs;
            _realisateursChoisis = _actionEstAjout ? new List<Realisateur>() : _film.Realisateurs;
        }

        public void InitialiserFormulaire()
        {
            ActionTitre.Text = _actionEstAjout ? "Ajouter un film" : "Modifier le film";
            BtnActionFilm.Content = ActionTitre.Text;

            _categories.ForEach(cat => CboCategories.Items.Add(cat));
            if (!_actionEstAjout)
            {
                TxtNom.Text = _film.Nom;
                CboCategories.SelectedIndex = _categories.IndexOf(_film.Categorie);
                DpDateSortie.BlackoutDates.Add(new CalendarDateRange(_film.DateSortie.AddDays(1), DateTime.Now));
                DpDateSortie.BlackoutDates.Add(new CalendarDateRange(new DateTime(1000, 1, 1),
                    _film.DateSortie.AddDays(-1)));
                DpDateSortie.SelectedDate = _film.DateSortie;
                DpDateSortie.DisplayDate = _film.DateSortie;
            }
            else
            {
                DpDateSortie.BlackoutDates.AddDatesInPast();
            }

            BtnAjouterActeur.IsEnabled = false;
            BtnRetirerActeur.IsEnabled = false;
            BtnAjouterRealisateur.IsEnabled = false;
            BtnRetirerRealisateur.IsEnabled = false;
        }

        public void ChargerLsts()
        {
            // Affecte tous les acteurs n'étant pas choisi
            LstActeursDispos.Items.Clear();
            _acteurs.Where(x => !_acteursChoisis.Contains(x))
                .ToList()
                .ForEach(x => LstActeursDispos.Items.Add(x));

            // Affecte tous les réalisateurs n'étant pas choisi
            LstRealisateursDispos.Items.Clear();
            _realisateurs.Where(x => !_realisateursChoisis.Contains(x))
                .ToList()
                .ForEach(x => LstRealisateursDispos.Items.Add(x));

            // Affecte tous les acteurs choisis
            LstActeursChoisis.Items.Clear();
            _acteursChoisis.ForEach(x => LstActeursChoisis.Items.Add(x));

            // Affecte tous les réalisateurs choisis
            LstRealisateursChoisis.Items.Clear();
            _realisateursChoisis.ForEach(x => LstRealisateursChoisis.Items.Add(x));
        }

        private void LstActeursDispos_OnSelectionChanged(object pSender, SelectionChangedEventArgs pE)
        {
            bool acteurDispoEstSelectionne = LstActeursDispos.SelectedIndex != -1;
            BtnAjouterActeur.IsEnabled = acteurDispoEstSelectionne;
            BtnRetirerActeur.IsEnabled = !acteurDispoEstSelectionne;
            BtnAjouterRealisateur.IsEnabled = false;
            BtnRetirerRealisateur.IsEnabled = false;

            if (acteurDispoEstSelectionne)
            {
                LstActeursChoisis.SelectedIndex = -1;
            }
        }

        private void LstActeursChoisis_OnSelectionChanged(object pSender, SelectionChangedEventArgs pE)
        {
            bool acteurChoisiEstSelectionne = LstActeursChoisis.SelectedIndex != -1;
            BtnRetirerActeur.IsEnabled = acteurChoisiEstSelectionne;
            BtnAjouterActeur.IsEnabled = !acteurChoisiEstSelectionne;
            BtnAjouterRealisateur.IsEnabled = false;
            BtnRetirerRealisateur.IsEnabled = false;

            if (acteurChoisiEstSelectionne)
            {
                LstActeursDispos.SelectedIndex = -1;
            }
        }

        private void BtnAjouterActeur_Click(object sender, RoutedEventArgs e)
        {
            if (LstActeursDispos.SelectedIndex == -1)
            {
                AfficherMsgErreur("Vous devez sélectionner un des acteurs disponibles dans la liste de gauche.");
            }
            else
            {
                try
                {
                    Acteur acteurChoisi = (Acteur)LstActeursDispos.SelectedItem;
                    _acteursChoisis.Add(acteurChoisi);
                    ChargerLsts();
                    LstActeursChoisis.SelectedIndex = LstActeursChoisis.Items.IndexOf(acteurChoisi);
                }
                catch (Exception exception)
                {
                    AfficherMsgErreur(exception.Message);
                }
            }
        }

        private void BtnRetirerActeur_Click(object sender, RoutedEventArgs e)
        {
            if (LstActeursChoisis.SelectedIndex == -1)
            {
                AfficherMsgErreur("Vous devez sélectionner un des acteurs choisis dans la liste de droite.");
            }
            else
            {
                try
                {
                    Acteur acteurDispo = (Acteur)LstActeursChoisis.SelectedItem;
                    _acteursChoisis.Remove(acteurDispo);
                    ChargerLsts();
                    LstActeursDispos.SelectedIndex = LstActeursDispos.Items.IndexOf(acteurDispo);
                }
                catch (Exception exception)
                {
                    AfficherMsgErreur(exception.Message);
                }
            }
        }

        private void LstRealisateursDispos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool realisateurDispoEstSelectionne = LstRealisateursDispos.SelectedIndex != -1;
            BtnAjouterRealisateur.IsEnabled = realisateurDispoEstSelectionne;
            BtnRetirerRealisateur.IsEnabled = !realisateurDispoEstSelectionne;
            BtnAjouterActeur.IsEnabled = false;
            BtnRetirerActeur.IsEnabled = false;

            if (realisateurDispoEstSelectionne)
            {
                LstRealisateursChoisis.SelectedIndex = -1;
            }
        }

        private void LstRealisateursChoisis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool realisateurChoisiEstSelectionne = LstRealisateursChoisis.SelectedIndex != -1;
            BtnRetirerRealisateur.IsEnabled = realisateurChoisiEstSelectionne;
            BtnAjouterRealisateur.IsEnabled = !realisateurChoisiEstSelectionne;
            BtnAjouterActeur.IsEnabled = false;
            BtnRetirerActeur.IsEnabled = false;

            if (realisateurChoisiEstSelectionne)
            {
                LstRealisateursDispos.SelectedIndex = -1;
            }
        }

        private void BtnAjouterRealisateur_Click(object sender, RoutedEventArgs e)
        {
            if (LstRealisateursDispos.SelectedIndex == -1)
            {
                AfficherMsgErreur("Vous devez sélectionner un des réalisateurs disponibles dans la liste de gauche.");
            }
            else
            {
                try
                {
                    Realisateur realisateurChoisi = (Realisateur)LstRealisateursDispos.SelectedItem;
                    _realisateursChoisis.Add(realisateurChoisi);
                    ChargerLsts();
                    LstRealisateursChoisis.SelectedIndex = LstRealisateursChoisis.Items.IndexOf(realisateurChoisi);
                }
                catch (Exception exception)
                {
                    AfficherMsgErreur(exception.Message);
                }
            }
        }

        private void BtnRetirerRealisateur_Click(object sender, RoutedEventArgs e)
        {
            if (LstRealisateursChoisis.SelectedIndex == -1)
            {
                AfficherMsgErreur("Vous devez sélectionner un des réalisateurs choisis dans la liste de droite.");
            }
            else
            {
                try
                {
                    Realisateur realisateurDispo = (Realisateur)LstRealisateursChoisis.SelectedItem;
                    _realisateursChoisis.Remove(realisateurDispo);
                    ChargerLsts();
                    LstRealisateursDispos.SelectedIndex = LstRealisateursDispos.Items.IndexOf(realisateurDispo);
                }
                catch (Exception exception)
                {
                    AfficherMsgErreur(exception.Message);
                }
            }
        }

        private void BtnAnnuler_Click(object pSender, RoutedEventArgs pE)
        {
            NavigationService.GoBack();
        }

        private void BtnActionFilm_Click(object sender, RoutedEventArgs e)
        {
            if (ValiderFormulaire())
            {
                List<ObjectId> acteurIds = new List<ObjectId>();
                _acteursChoisis.ForEach(x => acteurIds.Add(x.Id));
                List<ObjectId> realisateurIds = new List<ObjectId>();
                _realisateursChoisis.ForEach(x => realisateurIds.Add(x.Id));
                Categorie categorieChoisie = (Categorie)CboCategories.SelectedItem;

                string nom = TxtNom.Text;
                DateTime dateSortie = DpDateSortie.DisplayDate;

                if (_actionEstAjout)
                {
                    try
                    {
                        _dalFilm.InsererUnFilm(new Film(
                            new ObjectId(),
                            nom,
                            dateSortie,
                            new List<Projection>(),
                            new List<Note>(),
                            categorieChoisie.Id,
                            acteurIds,
                            realisateurIds
                        ));
                        NavigationService.GoBack();
                    }
                    catch (Exception exception)
                    {
                        AfficherMsgErreur(exception.Message);
                    }
                }
                else
                {
                    List<(Expression<Func<Film, object>> field, object value)> filtre =
                        new List<(Expression<Func<Film, object>> field, object value)>();
                    if (_film.Nom != TxtNom.Text)
                    {
                        filtre.Add((
                            x => x.Nom,
                            nom
                        ));
                    }

                    if (_film.DateSortie.Year != dateSortie.Year || _film.DateSortie.Month != dateSortie.Month ||
                        _film.DateSortie.Day != dateSortie.Day)
                    {
                        filtre.Add((
                            x => x.DateSortie,
                            dateSortie
                        ));
                    }

                    if (!_film.Categorie.Equals(categorieChoisie))
                    {
                        filtre.Add((
                            x => x.Categorie,
                            categorieChoisie
                        ));
                    }

                    if (_film.Acteurs != _acteursChoisis)
                    {
                        filtre.Add((
                            x => x.ActeursId,
                            acteurIds
                        ));
                    }

                    if (_film.Realisateurs != _realisateursChoisis)
                    {
                        filtre.Add((
                            x => x.ActeursId,
                            realisateurIds
                        ));
                    }

                    try
                    {
                        if (filtre.Count > 0)
                        {
                            _dalFilm.MAJUnFilm(x => x.Id == _film.Id, filtre);
                            NavigationService.GoBack();
                        }
                        else
                        {
                            AfficherMsgErreur(
                                "Aucune modification n'a été apporté. Si vous souhaitez retourner à la liste des films, veuillez cliquer sur le bouton 'Annuler'"
                            );
                        }
                    }
                    catch (Exception exception)
                    {
                        AfficherMsgErreur(exception.Message);
                    }
                }
            }
        }

        private bool ValiderFormulaire()
        {
            string msgErr = "";
            if (string.IsNullOrWhiteSpace(TxtNom.Text))
            {
                msgErr += "Il faut entrer un nom pour le film n'étant pas uniquement composé d'espace\n";
            }

            if (CboCategories.SelectedIndex < 0)
            {
                msgErr += "Il faut sélectionner une catégorie pour le film\n";
            }

            if (DpDateSortie.SelectedDate == null)
            {
                msgErr += "Il faut sélectionner une date de sortie internationnale pour le film \n";
            }
            else
            {
                bool dateEstInvalide = DpDateSortie.DisplayDate <= DateTime.Now;
                if (_actionEstAjout && dateEstInvalide ||
                    !_actionEstAjout && DpDateSortie.DisplayDate != _film.DateSortie && dateEstInvalide)
                {
                    msgErr += "Il faut sélectionner une date de sortie internationnale supérieure à la date actuelle";
                }
            }

            if (_acteursChoisis.Count < 1)
            {
                msgErr += "Il faut choisir au moins un acteur\n";
            }

            if (_realisateursChoisis.Count < 1)
            {
                msgErr += "Il faut choisir au moins un réalisateur";
            }

            if (msgErr == "")
            {
                return true;
            }

            AfficherMsgErreur(msgErr);
            return false;
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

        #endregion
    }
}