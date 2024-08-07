using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiverStateReporting.Data.Model
{
    /// <summary>
    /// Model class representing structure of data sent by each river station.
    /// </summary>
    public class Value
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public virtual Station? Station { get; set; }

        [Required]
        public int Val { get; set; } // Water level in cm

        [Required]
        public DateTime TimeStamp { get; set; }

        
    }
}
