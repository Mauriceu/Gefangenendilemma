using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    /// <summary>
    /// Nur nutzen, wenn es ein 3. Gruppenmitglied gibt.
    /// </summary>
    public class Strategie4 : BasisStrategie
    {
        int numberOfRounds;
        int currentRound = 0;
        int criminalOffence;

        /// <summary>
        /// Gibt den Namen der Strategie zurück, wichtig zum Anzeigen für die Auswahl
        /// </summary>
        /// <returns></returns>
        public override string Name()
        {
            return "Groundhog";
        }

        /// <summary>
        /// Gibt den Namen des Autors der Strategie zurück, wichtig für die Turnierpart um den Sieger zu ermitteln.
        /// </summary>
        /// <returns></returns>
        public override string Autor()
        {
            return "Andre Bayerbach";
        }

        /// <summary>
        /// Teilt mit, dass ein Verhoer jetzt startet
        /// </summary>
        /// <param name="runde">Anzahl der Runden, die verhört wird</param>
        /// <param name="schwere">Schwere des Verbrechen (VLeicht = 0, VMittel = 1, VSchwer = 2)</param>
        public override void Start(int runde, int schwere)
        {
            //Vorbereitungen für Start
            numberOfRounds = runde;
            currentRound = 0;
            criminalOffence = schwere;
        }

        /// <summary>
        /// Verhoert einen Gefangenen
        /// </summary>
        /// <param name="letzteReaktion">Reaktion des anderen Gefangenen, die Runde davor (NochNichtVerhoert = -1, Kooperieren = 0, Verrat = 1)</param>
        /// <returns>Gibt die eigene Reaktion für diese Runde zurück (Kooperieren = 0, Verrat = 1)</returns>
        public override int Verhoer(int letzteReaktion)
        {
            switch (criminalOffence)
            {
                case 0:
                    if (currentRound < (numberOfRounds - 2))
                    {
                        switch (letzteReaktion)
                        {
                            case 1:
                                currentRound++;
                                return Verrat;
                            default:
                                currentRound++;
                                return Kooperieren;
                        }
                    }
                    currentRound++;
                    return Verrat;

                case 1:
                    switch (letzteReaktion)
                    {
                        case 1:
                            currentRound++;
                            return Kooperieren;
                        default:
                            currentRound++;
                            return Verrat;
                    }

                default:
                    if (currentRound < (numberOfRounds - 1))
                    {
                        switch (letzteReaktion)
                        {
                            case 1:
                                currentRound++;
                                return Verrat;
                            default:
                                currentRound++;
                                return Kooperieren;
                        }
                    }
                    currentRound++;
                    return Verrat;
            }
        }
    }
}
