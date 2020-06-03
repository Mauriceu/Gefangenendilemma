using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    /// <summary>
    /// Nur nutzen, wenn es ein 3. Gruppenmitglied gibt.
    /// </summary>
    public class Strategie3 : BasisStrategie
    {
        public override string Name()
        {
            return "Anti-Verrat";
        }

        public override string Autor()
        {
            return "An Duc Hoang";
        }

        public override void Start(int runde, int schwere)
        {
            //Vorbereitungen für Start
        }

        public override int Verhoer(int letzteReaktion)
        {
            //Strategie hier ergänzen

            return Verrat;
        }
    }
}