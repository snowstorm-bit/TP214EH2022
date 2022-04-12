#region MÉTADONNÉES

// Nom du fichier : Note.cs
// Date de création : 2022-04-10
// Date de modification : 2022-04-12

#endregion

#region USING

using MonCine.Data.Classes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace MonCine.Data
{
    public class Note
    {
        #region PROPRIÉTÉS ET INDEXEURS

        public ObjectId AbonneId { get; set; }

        [BsonIgnore] public Abonne Abonne { get; set; }

        public int NoteFilm { get; set; }

        #endregion

        #region CONSTRUCTEURS

        public Note(ObjectId pAbonne, int pNoteFilm)
        {
            AbonneId = pAbonne;
            NoteFilm = pNoteFilm;
        }

        #endregion
    }
}