using System;
using System.Collections.Generic;

namespace reactprogress2
{
    public partial class SpacePhenominaType
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; } = null!;
        public string? Name { get; set; }
        public bool Deleted { get; set; }
        public int SuperTypeId { get; set; }

        public virtual SpacePhenominaSuperType SuperType { get; set; } = null!;
    }
}
