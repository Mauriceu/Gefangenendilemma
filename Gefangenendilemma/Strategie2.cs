using System;
using System.Collections.Generic;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{

    public class Strategie2 : BasisStrategie
    {
        private int schwere;
        private int runden;

        public override string Name()
        {
            return "Anti-Groll-und-Verrate-Immer";
        }

        public override string Autor()
        {
            return "Hikmet Özer";
        }

        public override void Start(int runde, int schwere)
        {
            //Vorbereitungen für Start
        }

        public override int Verhoer(int letzteReaktion)
        {
            //Strategie hier ergänzen
            Random num = new Random();
            int ran = num.Next(0, 100);
            
            if (ran > 50)
            {
                return Verrat;
            }
            return Kooperieren;
        }
    }
}