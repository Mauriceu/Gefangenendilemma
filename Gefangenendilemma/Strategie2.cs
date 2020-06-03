using System;
using System.Collections.Generic;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{

    public class Strategie2 : BasisStrategie
    {
        public override string Name()
        {
            return "Testeroni";
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