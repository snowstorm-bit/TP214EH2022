#region MÉTADONNÉES

// Nom du fichier : Reservation.cs
// Date de création : 2022-04-12
// Date de modification : 2022-04-12

#endregion

#region USING

using System;
using System.Security.AccessControl;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data.Classes
{
    public class Reservation
    {
        private Film _film;
        private int _nbPlaces;

        #region PROPRIÉTÉS ET INDEXEURS

        public ObjectId Id { get; set; }

        public DateTime Date { get; set; }

        public ObjectId FilmId { get; set; }

        [BsonIgnore]
        public Film Film
        {
            get { return _film; }
            set
            {
                if (value is null)
                    throw new ArgumentNullException("Le film de la réservation ne peut être nul.", nameof(value));
                _film = value;
                FilmId = _film.Id;
            }
        }

        public ObjectId AbonneId { get; set; }

        public int NbPlaces
        {
            get { return _nbPlaces; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Le nombre de places de la réservation doit être d'au moins 1."
                    );


                if (Film != null)
                {
                    int nbPlacesRestantes = Film.Projections.Find(x => x.DateDebut == Date).NbPlacesRestantes;
                    if (nbPlacesRestantes - _nbPlaces < 0)
                        throw new InvalidOperationException(
                            "Il ne reste plus de places pour créer une réservation avec le nombre de places et la projection sélectionnés.");
                }

                _nbPlaces = value;
            }
        }

        #endregion

        #region CONSTRUCTEURS

        public Reservation(ObjectId pId, DateTime pDate, Film pFilmId, ObjectId pAbonneId, int pNbPlaces)
        {
            Id = pId;
            Date = pDate;
            Film = pFilmId;
            AbonneId = pAbonneId;
            NbPlaces = pNbPlaces;
        }

        #endregion
    }
}