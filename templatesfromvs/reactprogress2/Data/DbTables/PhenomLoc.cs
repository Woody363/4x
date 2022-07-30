using System;
using System.Collections.Generic;

namespace reactprogress2.Data.DbTables
{
    public partial class PhenomLoc
    {
        public int Id { get; set; }
        public int Xcoord { get; set; }
        public int Ycoord { get; set; }
        public int PhenominaId { get; set; }
        public bool Deleted { get; set; }

        public virtual PhenomSt Phenomina { get; set; } = null!;
    }
}
