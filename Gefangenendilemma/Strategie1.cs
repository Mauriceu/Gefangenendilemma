using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{

    public class Strategie1 : BasisStrategie
    {
        private int runden;
        private int schwere;
        private List<int> enemyReactions = new List<int>();
        private List<int> ownReactions = new List<int>();

        public override string Name()
        {
            return "TFT";
        }

        public override string Autor()
        {
            return "Maurice Niedergesäß";
        }

        public override void Start(int runde, int schwere)
        {
            //Vorbereitungen für Start
            runden = runde;
            this.schwere = schwere;
            
            enemyReactions = new List<int>();
            ownReactions = new List<int>();
        }

        public override int Verhoer(int letzteReaktion)
        {
            //Strategie hier ergänzen
            if (letzteReaktion != -1) 
            {
                enemyReactions.Add(letzteReaktion);
            }


            if (schwere != VMittel)
            {
                if (ownReactions.Count + 1 == runden)
                {
                    ownReactions.Add(Verrat);
                    return Verrat;
                }
                
                if (letzteReaktion == Verrat)
                {
                    ownReactions.Add(Verrat);
                    return Verrat;
                }

                if (enemyReactions.FindAll(item => item == Verrat).Count == 0 && enemyReactions.Count > 1)
                {
                    ownReactions.Add(Kooperieren);
                    return Kooperieren;
                }
                
                ownReactions.Add(Verrat);
                return Verrat;
            }
            
            ownReactions.Add(Kooperieren);
            return Kooperieren;

        }
        
    }
}