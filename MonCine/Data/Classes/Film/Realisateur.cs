#region MÉTADONNÉES

// Nom du fichier : Realisateur.cs
// Date de création : 2022-04-12
// Date de modification : 2022-04-12

#endregion

#region USING

using MongoDB.Bson;

#endregion

namespace MonCine.Data.Classes
{
    public class Realisateur
    {
        #region PROPRIÉTÉS ET INDEXEURS

        public ObjectId Id { get; set; }
        public string Nom { get; set; }

        #endregion

        #region CONSTRUCTEURS

        public Realisateur(ObjectId pId, string pNom)
        {
            Id = pId;
            Nom = pNom;
        }

        #endregion
    }
}