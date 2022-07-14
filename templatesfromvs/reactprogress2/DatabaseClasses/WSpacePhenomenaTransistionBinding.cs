using System;
using System.Collections.Generic;

namespace reactprogress2
{
    public partial class WSpacePhenomenaTransistionBinding
    {
        public int Id { get; set; }
        public int PhenomenaId { get; set; }
        public int NextPhenomenaStageId { get; set; }
        public double ProbabilityOfTransistion { get; set; }

        public virtual WSpacePhenomina NextPhenomenaStage { get; set; } = null!;
        public virtual WSpacePhenomina Phenomena { get; set; } = null!;
    }
}
