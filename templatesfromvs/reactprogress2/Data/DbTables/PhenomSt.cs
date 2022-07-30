using System;
using System.Collections.Generic;

namespace reactprogress2.Data.DbTables
{
    public partial class PhenomSt
    {
        public PhenomSt()
        {
            PhenomLocs = new HashSet<PhenomLoc>();
            PhenomTransBNextPhenomenaStages = new HashSet<PhenomTransB>();
            PhenomTransBPhenomena = new HashSet<PhenomTransB>();
        }

        public int Id { get; set; }
        public string FriendlyName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int SpacePhenominaTypeId { get; set; }
        public int ImageFilesId { get; set; }

        public virtual Image ImageFiles { get; set; } = null!;
        public virtual PhenomT SpacePhenominaType { get; set; } = null!;
        public virtual ICollection<PhenomLoc> PhenomLocs { get; set; }
        public virtual ICollection<PhenomTransB> PhenomTransBNextPhenomenaStages { get; set; }
        public virtual ICollection<PhenomTransB> PhenomTransBPhenomena { get; set; }
    }
}
