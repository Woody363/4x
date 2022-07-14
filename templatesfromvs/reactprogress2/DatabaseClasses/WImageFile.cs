using System;
using System.Collections.Generic;

namespace reactprogress2
{
    /// <summary>
    /// this is the table
    /// </summary>
    public partial class WImageFile
    {
        public WImageFile()
        {
            WSpacePhenominas = new HashSet<WSpacePhenomina>();
        }

        public int Id { get; set; }
        /// <summary>
        /// this is so we know what it is when looking at the db its not for querying
        /// </summary>
        public string? FriendlyName { get; set; }
        public string? FileLocation { get; set; }
        public string CssRef { get; set; } = null!;
        public DateTime AddedOn { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<WSpacePhenomina> WSpacePhenominas { get; set; }
    }
}
