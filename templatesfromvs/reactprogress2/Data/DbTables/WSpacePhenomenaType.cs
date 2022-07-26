using System;
using System.Collections.Generic;

namespace reactprogress2
{
    /// <summary>
    /// Natural and artificial lets says
    /// </summary>
    public partial class WSpacePhenomenaType
    {
        public WSpacePhenomenaType()
        {
            WSpacePhenominas = new HashSet<WSpacePhenomina>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string FriendlyName { get; set; } = null!;
        public string InGameDescription { get; set; } = null!;
        public bool Deleted { get; set; }

        public virtual ICollection<WSpacePhenomina> WSpacePhenominas { get; set; }
    }
}
