using MonCine.Data.Classes.BD;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MonCine.Data.Interfaces
{
    public interface ICRUD<TDocument>
    {
        public List<TDocument> ObtenirTout();

        public List<TDocument> ObtenirPlusieurs<TField>(Expression<Func<TDocument, TField>> pField,
            List<TField> pObjects);

        public List<TDocument> ObtenirObjetsDansLst(List<TDocument> pDocuments);

        public void InsererPlusieurs(List<TDocument> pDocuments);
    }
}
