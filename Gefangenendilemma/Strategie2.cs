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

            Console.Write("Ihr Gegner hat in der letzten Runde ");
            switch(letzteReaktion)
            {
                case -1:
                    Console.WriteLine("noch nichts ausgewählt.");
                    break;
                case 0:
                    Console.WriteLine("kooperieren ausgewählt.");
                    break;
                case 1:
                    Console.WriteLine("verrat ausgewählt.");
                    break;
            }
            Console.WriteLine("Was möchten Sie als nächstes tun? (0 = kooperieren; 1 = verraten)");

            while ( true )
            {
                switch (Console.ReadLine())
                {
                    case "0":
                        return Kooperieren;
                    case "1":
                        return Verrat;
                    default:
                        Console.WriteLine("Ungültige Eingabe. Bitte geben Sie 0 [kooperieren] oder 1 [verrat] als Antwort ein.");
                        break;
                }
            }
        }
    }
}