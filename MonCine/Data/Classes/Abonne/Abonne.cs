#region MÉTADONNÉES

// Nom du fichier : Abonne.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-20

#endregion

#region USING

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data.Classes
{
    /// <summary>
    /// Classe représentant un abonné de la cinémathèque.
    /// </summary>
    public class Abonne : Utilisateur
    {
        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou défini la date à laquelle l'abonné a été enregistré dans le système
        /// </summary>
        public DateTime DateAdhesion { get; set; }

        /// <summary>
        /// Obitient ou défini l'objet <see cref="Classes.Preference"/> de l'abonné
        /// </summary>
        public Preference Preference { get; set; }

        /// <summary>
        /// Nombre de séances auxquelles l'abonné a assisté
        /// </summary>
        [BsonIgnore]
        public int NbSeances { get; set; }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur permettant la création d'un abonné.
        /// </summary>
        /// <param name="pId">Identifiant de l'abonné</param>
        /// <param name="pNom">Nom de l'abonné</param>
        /// <param name="pCourriel">Courriel de l'abonné</param>
        /// <param name="pMdp">Mot de passe de l'abonné</param>
        /// <param name="pPreference">Préférence de l'abonné</param>
        public Abonne(ObjectId pId, string pNom, string pCourriel, string pMdp, Preference pPreference) :
            base(pId, pNom, pCourriel, pMdp)
        {
            DateAdhesion = DateTime.Now;
            Preference = pPreference;
        }

        #endregion

        #region MÉTHODES

        #region Overrides of Object

        public override string ToString()
        {
            return Preference.ToString();
        }

        #endregion

        #endregion
    }
}