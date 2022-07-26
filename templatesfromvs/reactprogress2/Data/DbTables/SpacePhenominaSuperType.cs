using System;
using System.Collections.Generic;

namespace reactprogress2
{
    public partial class SpacePhenominaSuperType
    {
        public SpacePhenominaSuperType()
        {
            SpacePhenominaTypes = new HashSet<SpacePhenominaType>();
        }

        public int Id { get; set; }
        public string FriendlyName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool Deleted { get; set; }

        public virtual ICollection<SpacePhenominaType> SpacePhenominaTypes { get; set; }
    }
}
