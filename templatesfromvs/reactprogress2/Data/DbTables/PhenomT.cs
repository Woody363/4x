using System;
using System.Collections.Generic;

namespace reactprogress2.Data.DbTables
{
    /// <summary>
    /// Natural and artificial lets says for phenomena types
    /// </summary>
    public partial class PhenomT
    {
        public PhenomT()
        {
            PhenomSts = new HashSet<PhenomSt>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string FriendlyName { get; set; } = null!;
        public string InGameDescription { get; set; } = null!;
        public bool Deleted { get; set; }

        public virtual ICollection<PhenomSt> PhenomSts { get; set; }
    }
}
