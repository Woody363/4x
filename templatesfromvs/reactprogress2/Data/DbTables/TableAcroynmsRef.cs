using System;
using System.Collections.Generic;

namespace reactprogress2.Data.DbTables
{
    public partial class TableAcroynmsRef
    {
        public string Acronym { get; set; } = null!;
        public string Meaning { get; set; } = null!;
        public string? Comment { get; set; }
    }
}
