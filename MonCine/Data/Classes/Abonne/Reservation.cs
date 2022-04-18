#region MÉTADONNÉES

// Nom du fichier : Reservation.cs
// Date de création : 2022-04-12
// Date de modification : 2022-04-12

#endregion

#region USING

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data.Classes
{
    public class Reservation
    {
        #region PROPRIÉTÉS ET INDEXEURS

        public DateTime Date { get; set; }

        public ObjectId FilmId { get; set; }

        [BsonIgnore] public Film Film { get; set; }

        public int NbPlaces { get; set; }

        #endregion

        #region CONSTRUCTEURS

        public Reservation(DateTime pDate, ObjectId pFilmId, int pNbPlaces)
        {
            Date = pDate;
            FilmId = pFilmId;
            NbPlaces = pNbPlaces;
        }

        #endregion
    }
}