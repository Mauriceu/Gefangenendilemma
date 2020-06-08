using System;
using System.Collections.Generic;
using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{

    public class Strategie2 : BasisStrategie
    {
        // Setzen von Attributen der Klasse
        private int rundencounter;
        private int runden;
        private int schwere;

        /// <summary>
        /// Enthält den Namen der Strategie
        /// </summary>
        /// <returns>Strategiename</returns>
        public override string Name()
        {
            return "Anti-Groll";
        }

        /// <summary>
        /// Enthält den Namen des Autors
        /// </summary>
        /// <returns>Autorname</returns>
        public override string Autor()
        {
            return "Hikmet Özer";
        }

        /// <summary>
        /// Übergibt der Klasse die Anzahl der Runden und den Schwierigkeitsgrad
        /// vor dem Verhör
        /// </summary>
        /// <param name="runden"></param>
        /// <param name="schwere"></param>
        public override void Start(int runden, int schwere)
        {
            Console.Clear();
            this.rundencounter = 0;
            this.runden = runden;
            this.schwere = schwere;
        }

        /// <summary>
        /// Gibt die Reaktion der Strategie zurück
        /// </summary>
        /// <param name="letzteReaktion"></param>
        /// <returns>Strategie</returns>
        public override int Verhoer(int letzteReaktion)
        {
            this.rundencounter++;

            // Bestimmt Reaktion je nach Schwierigkeitsgrad
            switch (this.schwere)
            {
                case 0:
                    if (this.rundencounter == this.runden)
                    {
                        return Verrat;
                    }

                    return Kooperieren;

                case 1:
                    if (this.runden == 1)
                    {
                        return Kooperieren;
                    }

                    if(this.rundencounter == 1)
                    {
                        return Verrat;
                    }

                    return Kooperieren;

                case 2:
                    if(this.rundencounter == this.runden)
                    {
                        return Verrat;
                    }

                    return Kooperieren;
            }

            return Kooperieren;
        }
    }
}