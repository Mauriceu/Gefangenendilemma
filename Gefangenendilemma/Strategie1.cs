using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{

    public class Strategie1 : BasisStrategie
    {
        public int schwere;
        public int runden;
        public int enemyP; 
        public int ownP; 
        public List<int> enemyReactions = new List<int>();
        public List<int> ownReactions = new List<int>();

        public override string Name()
        {
            return "Maurice";
        }

        public override string Autor()
        {
            return "Maurice Niedergesäß";
        }

        public override void Start(int runde, int schwere)
        {
            //Vorbereitungen für Start
            this.schwere = schwere;
            runden = runde;
        }

        public override int Verhoer(int letzteReaktion)
        {
            //Strategie hier ergänzen
            if (letzteReaktion != -1) 
            {
                enemyReactions.Add(letzteReaktion);
            }
            
            
            if (letzteReaktion == 1)
            {
                ownReactions.Add(1);
                return Verrat;
            }
            ownReactions.Add(0);
            return Kooperieren;
            
        }
        
    }
}