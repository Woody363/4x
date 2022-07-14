using System;
using System.Collections.Generic;

namespace reactprogress2
{
    /// <summary>
    /// if we had two outcomes for a sun e.g neutron star, black hole
    /// </summary>
    public partial class SpacePhenominaStateNextStateBinding
    {
        public int Id { get; set; }
        public int SpacePhenominaStartStateId { get; set; }
        public int? SpacePhenominaEndStateId { get; set; }
        public double? DefaultStartProbabilityOfChange { get; set; }
        public double? DefaultStartTurnCountDown { get; set; }
        public double? DefaultProbabilityToOccurOverOtherChangeOutcome { get; set; }
    }
}
