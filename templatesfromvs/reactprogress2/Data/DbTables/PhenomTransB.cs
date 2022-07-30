using System;
using System.Collections.Generic;

namespace reactprogress2.Data.DbTables
{
    /// <summary>
    /// space phenomina transistion binding is so we know probablity and what phenomina a phenomina binds to
    /// </summary>
    public partial class PhenomTransB
    {
        public int Id { get; set; }
        public int PhenomenaId { get; set; }
        public int NextPhenomenaStageId { get; set; }
        public double ProbabilityOfTransistion { get; set; }

        public virtual PhenomSt NextPhenomenaStage { get; set; } = null!;
        public virtual PhenomSt Phenomena { get; set; } = null!;
    }
}
