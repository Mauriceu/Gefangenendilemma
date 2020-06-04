using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    /// <summary>
    /// Nur nutzen, wenn es ein 3. Gruppenmitglied gibt.
    /// </summary>
    public class Strategie3 : BasisStrategie
    {
        private bool antiVerrat;
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
            antiVerrat = false;
        }

        public override int Verhoer(int letzteReaktion)
        {
            
        }
    }
}