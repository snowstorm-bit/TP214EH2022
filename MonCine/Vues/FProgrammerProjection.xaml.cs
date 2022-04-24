﻿#region MÉTADONNÉES

// Nom du fichier : FProgrammerProjection.xaml.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using System.Collections.Generic;
using System.Windows;
using MonCine.Data.Classes;
using MonCine.Data.Classes.BD;
using MonCine.Data.Classes.DAL;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Navigation;

#endregion

namespace MonCine.Vues
{
    public partial class FProgrammerProjection : Page
    {
        #region ATTRIBUTS

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;
        private readonly Film _film;
        private readonly DALSalle _dalSalle;
        private readonly DALFilm _dalFilm;

        #endregion

        #region CONSTRUCTEURS

        public FProgrammerProjection(Film pFilm, IMongoClient pClient, IMongoDatabase pDb)
        {
            InitializeComponent();

            _client = pClient;
            _db = pDb;
            _film = pFilm;
            _dalSalle = new DALSalle(_client, _db);
            _dalFilm = new DALFilm(_client, _db);

            AfficherInformationDuFilm();
        }

        #endregion

        #region MÉTHODES

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AfficherInformationDuFilm()
        {
            DpDateDebut.BlackoutDates.AddDatesInPast();
            DpDateDebut.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now));

            DpDateFin.BlackoutDates.AddDatesInPast();
            DpDateFin.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddDays(1)));

            List<Salle> salles = _dalSalle.ObtenirTout();
            salles.ForEach(x => CboSalles.Items.Add(x));
        }

        #endregion

        private void BtnAjouter_OnClick(object pSender, RoutedEventArgs pE)
        {
            if (ValiderFormulaire())
            {
                try
                {
                    _film.AjouterProjection((DateTime)DpDateDebut.SelectedDate, (DateTime)DpDateFin.SelectedDate,
                        (Salle)CboSalles.SelectedItem);
                    _dalFilm.MAJProjections(_film);
                    NavigationService.GoBack();
                }
                catch (ArgumentNullException e)
                {
                    AfficherMsgErreur(e.Message);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    AfficherMsgErreur(e.Message);
                }
                catch (InvalidOperationException e)
                {
                    AfficherMsgErreur(e.Message);
                }
                catch (ExceptionBD e)
                {
                    AfficherMsgErreur(e.Message);
                }
                catch (Exception e)
                {
                    AfficherMsgErreur(e.Message);
                }
            }
        }

        private bool ValiderFormulaire()
        {
            string msgErr = "";

            if (CboSalles.SelectedIndex < 0)
            {
                msgErr += "Il faut sélectionner une salle pour la projection du film\n";
            }

            if (DpDateDebut.SelectedDate == null)
            {
                msgErr += "Il faut sélectionner une date de début pour la projection du film\n";
            }
            else
            {
                if ((DateTime)DpDateDebut.SelectedDate <= DateTime.Now)
                    msgErr += "Il faut sélectionner une date de début supérieure à la date actuelle";
                else
                {
                    if (DpDateFin.SelectedDate == null)
                    {
                        msgErr += "Il faut sélectionner une date de fin pour la projection du film\n";
                    }
                    else if ((DateTime)DpDateFin.SelectedDate < (DateTime)DpDateDebut.SelectedDate)
                    {
                        msgErr +=
                            "Il faut sélectionner une date de fin supérieure à la date de début de la projection du film";
                    }
                }
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
    }
}