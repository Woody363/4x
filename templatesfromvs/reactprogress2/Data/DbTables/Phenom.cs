using System;
using System.Collections.Generic;

namespace reactprogress2.Data.DbTables
{
    /// <summary>
    /// for suns etc
    /// phenomina state
    /// </summary>
    public partial class Phenom
    {
        public int Id { get; set; }
        public string FriendlyName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime AddedOn { get; set; }
        public bool Deleted { get; set; }
        public short OrderInSubType { get; set; }
        public short Size { get; set; }
        public int? VisualsTableId { get; set; }
        /// <summary>
        /// space phenomina super type id
        /// </summary>
        public int SpstId { get; set; }
        /// <summary>
        /// space phenomina type
        /// </summary>
        public int SptId { get; set; }
        /// <summary>
        /// sub type id
        /// </summary>
        public int SpsubTypeId { get; set; }
    }
}
