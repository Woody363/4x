namespace reactprogress2.Data.DataModels
{
    public class PhenomTransTriggerInstruction
    {
        //this model can force a trigger but does not set which option if multiple are available is chosen
        public double? OverrideProbability { get; set; } = null;//Set this to 1 to force trigger
        public int PhenomId { get; set; }
        public PhenomTransTriggerInstruction(int phenomId, double? overrideProbability = null)
        {
            OverrideProbability = overrideProbability;
            PhenomId = phenomId;
        }

    }
}
