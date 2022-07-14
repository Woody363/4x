using System;
using System.Collections.Generic;

namespace reactprogress2
{
    /// <summary>
    /// for suns etc
    /// </summary>
    public partial class SpacePhenominaState
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
