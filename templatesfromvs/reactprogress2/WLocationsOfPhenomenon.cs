using System;
using System.Collections.Generic;

namespace reactprogress2
{
    public partial class WLocationsOfPhenomenon
    {
        public int Id { get; set; }
        public int Xcoord { get; set; }
        public int Ycoord { get; set; }
        public int PhenominaId { get; set; }
        public bool Deleted { get; set; }

        public virtual WSpacePhenomina Phenomina { get; set; } = null!;
    }
}
