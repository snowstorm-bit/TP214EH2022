#region MÉTADONNÉES

// Nom du fichier : TicketGratuit.cs
// Auteur : Mélina Hotte (1933760)
// Date de création : 2022-04-10
// Date de modification : 2022-04-10

#endregion

#region USING

using MongoDB.Bson;

#endregion

namespace MonCine.Data.Classes
{
    public class TicketGratuit : Recompense
    {
        #region CONSTRUCTEURS

        public TicketGratuit(ObjectId pId, ObjectId pFilmId) : base(pId, pFilmId)
        {
        }

        #endregion
    }
}