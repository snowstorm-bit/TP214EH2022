#region MÉTADONNÉES

// Nom du fichier : Realisateur.cs
// Date de création : 2022-04-20
// Date de modification : 2022-04-21

#endregion

#region USING

using MongoDB.Bson;

#endregion

namespace MonCine.Data.Classes
{
    /// <summary>
    /// Classe représentant un réalisateur d'un film.
    /// </summary>
    public class Realisateur
    {
        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou défini l'identifiant du réalisateur
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Obtient ou défini le nom du réalisateur
        /// </summary>
        public string Nom { get; set; }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur permettant la création d'un réalisateur.
        /// </summary>
        /// <param name="pId">Identifiant du réalisateur</param>
        /// <param name="pNom">Nom du réalisateur</param>
        public Realisateur(ObjectId pId, string pNom)
        {
            Id = pId;
            Nom = pNom;
        }

        #endregion

        #region MÉTHODES

        public override string ToString()
        {
            return Nom;
        }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Realisateur realisateur)
            {
                return Id == realisateur.Id;
            }

            return false;
        }

        #endregion
    }
}