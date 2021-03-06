using Gefangenendilemma.Basis;

namespace Gefangenendilemma
{
    public class VerrateImmerStrategie : BasisStrategie
    {
        public override string Name()
        {
            return "Verrate immer";
        }

        public override string Autor()
        {
            return "Steffen Trutz";
        }

        public override void Start(int runde, int schwere)
        {
            //ignore
        }

        public override int Verhoer(int letzteReaktion)
        {
            //immer verweigern
            return Verrat;
        }
    }
}