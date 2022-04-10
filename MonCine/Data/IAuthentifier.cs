#region MÉTADONNÉES

// Nom du fichier : IAuthentifier.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2022-04-10
// Date de modification : 2022-04-10

#endregion

#region USING

using MongoDB.Bson;

#endregion

namespace MonCine.Data
{
    /// <summary>
    /// Ce comportement permet à un objet implémentant cette interface de s'authentifier à la cinémathèque
    /// </summary>
    public interface IAuthentifier
    {
        #region PROPRIÉTÉS ET INDEXEURS

        public ObjectId Id { get; set; }
        /// <summary>
        /// Courriel d'un objet implémentant cette interface
        /// </summary>
        public string Courriel { get; set; }

        /// <summary>
        /// Mot de passe d'un objet implémentant cette interface
        /// </summary>
        public string Mdp { get; set; }

        #endregion

        /// <summary>
        /// Permet d'authentifier l'objet courrant implémentant cette interface à 
        /// </summary>
        /// <param name="pCourriel"></param>
        /// <param name="pMdp"></param>
        /// <returns></returns>
        public bool Authentifier(string pCourriel, string pMdp);
    }
}