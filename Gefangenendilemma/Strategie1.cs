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
        public int runden;
        public int schwere;
        public List<int> enemyReactions = new List<int>();
        public List<int> ownReactions = new List<int>();

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
                
                if (letzteReaktion == Kooperieren)
                {
                    //eigene Reaktionen werden größtenteils nicht imitiert
                    if (enemyReactions.Except(ownReactions).ToList().Count < 5 && runden > 5 )
                    {
                        ownReactions.Add(Verrat);
                        return Verrat;
                    }
                    
                    ownReactions.Add(Kooperieren);
                    return Kooperieren;
                }
                
                ownReactions.Add(Verrat);
                return Verrat;
                
            }

            var unterschiede = enemyReactions.Except(ownReactions).ToList();

            if (unterschiede.Count < 5 && runden == 100 && enemyReactions.Count > 30)
            {
                Console.WriteLine("testorino brudi");
                ownReactions.Add(Verrat);
                return Kooperieren;
            } 

            ownReactions.Add(Verrat);
            return Verrat;
            
        }
        
    }
}