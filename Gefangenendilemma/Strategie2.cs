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

        public override void Start(int runden, int schwere)
        {
            this.schwere = schwere;
            this.runden = runden;
        }

        public override int Verhoer(int letzteReaktion)
        {
            if (schwere == 1)
            {
                return Kooperieren;
            }

            return Verrat;
        }
    }
}