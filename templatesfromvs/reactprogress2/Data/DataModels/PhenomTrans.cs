using reactprogress2.Data.DbTables;

namespace reactprogress2.Data.DataModels
{
    public class PhenomTrans
    {
        public PhenomLoc PhenomLoc {get;set;}
        public PhenomSt NewPhenomSt { get; set; }

        public PhenomTrans() { 
        //empty constructor for linq
        }
        public PhenomTrans(PhenomLoc phenomLoc, PhenomSt newPhenomSt)
        {
            PhenomLoc = phenomLoc;
            NewPhenomSt = newPhenomSt;
        }
    }
}
