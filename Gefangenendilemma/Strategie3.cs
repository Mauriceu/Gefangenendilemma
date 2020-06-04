using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    /// <summary>
    /// Nur nutzen, wenn es ein 3. Gruppenmitglied gibt.
    /// </summary>
    public class Strategie3 : BasisStrategie
    {
        private int runden;
        private int schwere;
        private List<int> enemyReactions = new List<int>();
        private List<int> ownReactions = new List<int>();
        private bool antiVerrat;
        public override string Name()
        {
            return "Anti-Verrat";
        }

        public override string Autor()
        {
            return "An Duc Hoang";
        }

        public override void Start(int runde, int schwere)
        {
            this.schwere = schwere;
            
        }

        public override int Verhoer(int letzteReaktion)
        {
            if ()
            {

            }
        }
    }
}