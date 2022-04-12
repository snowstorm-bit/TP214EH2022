#region MÉTADONNÉES

// Nom du fichier : Abonne.cs
// Date de création : 2022-04-10
// Date de modification : 2022-04-12

#endregion

#region USING

using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data.Classes
{
    public class Abonne : Utilisateur
    {
        #region PROPRIÉTÉS ET INDEXEURS

        public DateTime DateAdhesion { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<ObjectId> RecompensesId { get; set; }

        [BsonIgnore] public List<Recompense> Recompenses { get; set; }

        public Preference Preference { get; set; }

        public int NbSeances
        {
            get { return Reservations.Count; }
        }

        #endregion

        #region CONSTRUCTEURS

        public Abonne(ObjectId pId, string pNom, string pCourriel, string pMdp, DateTime pDateAdhesion,
            List<Reservation> pReservationsId, List<ObjectId> pRecompensesId, Preference pPreference) :
            base(pId, pNom, pCourriel, pMdp)
        {
            DateAdhesion = pDateAdhesion;
            Reservations = pReservationsId;
            RecompensesId = pRecompensesId;
            Preference = pPreference;
        }

        #endregion
    }
}