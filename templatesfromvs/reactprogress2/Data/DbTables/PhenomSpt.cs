using System;
using System.Collections.Generic;

namespace reactprogress2.Data.DbTables
{
    public partial class PhenomSpt
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool Deleted { get; set; }
    }
}
