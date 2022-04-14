﻿#region MÉTADONNÉES

// Nom du fichier : ErreurLog.cs
// Date de création : 2022-04-12
// Date de modification : 2022-04-12

#endregion

#region USING

using System;
using System.Diagnostics;

#endregion

namespace MonCine.Data.Classes
{
    /// <summary>
    /// Classe permettant de journalisé les erreurs lancées par le programme.
    /// </summary>
    public static class ErreurLog
    {
        #region CONSTANTES ET ATTRIBUTS STATIQUES

        /// <summary>
        /// Permet de tracer les erreurs
        /// </summary>
        private static readonly TraceSource _traceur = new TraceSource("TraceSourceApp");

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'enregistrer la trace d'une erreur son type.
        /// </summary>
        /// <remarks>Voir fichier app.config pour la configuration</remarks>
        /// <param name="pMessage">Message d'erreur</param>
        /// <param name="pType">Niveau du message</param>
        /// <param name="pId">Identifiant du message</param>
        public static void Journaliser(string pMsg, TraceEventType pType, int pId)
        {
            ErreurLog._traceur.TraceEvent(pType, pId, $"{DateTime.Now} : {pMsg}");
            ErreurLog._traceur.Flush();
        }

        #endregion
    }
}