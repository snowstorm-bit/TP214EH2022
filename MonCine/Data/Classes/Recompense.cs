#region MÉTADONNÉES

// Nom du fichier : Recompense.cs
// Date de création : 2022-04-10
// Date de modification : 2022-04-12

#endregion

#region USING

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data
{
    public abstract class Recompense
    {
        #region PROPRIÉTÉS ET INDEXEURS

        public ObjectId Id { get; set; }

        public ObjectId FilmId { get; set; }

        [BsonIgnore] public Film Film { get; set; }

        #endregion

        #region CONSTRUCTEURS

        protected Recompense(ObjectId pId, ObjectId pFilmId)
        {
            FilmId = pFilmId;
            Id = pId;
        }

        #endregion

        #region MÉTHODES

        #region Overrides of Object

        public override string ToString()
        {
            return $"{GetType().Name} - {Film}";
        }

        #endregion

        #endregion
    }
}