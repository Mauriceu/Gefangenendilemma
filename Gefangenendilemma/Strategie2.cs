using System;
using System.Collections.Generic;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{

    public class Strategie2 : BasisStrategie
    {

        public override string Name()
        {
            return "Benutzereingabe";
        }

        public override string Autor()
        {
            return "Hikmet Özer";
        }

        public override void Start(int runden, int schwere)
        {
            Console.Clear();
        }

        public override int Verhoer(int letzteReaktion)
        {
            return Kooperieren;
        }
    }
}