using System.ComponentModel.DataAnnotations;

namespace RiverStateReporting.Data.Model
{
    /// <summary>
    /// Model class for stations
    /// </summary>
    public class Station
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string River { get; set; }

        public double RiverKm { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Range(1, 999)]
        public int FloodLevel { get; set; }

        [Required]
        [Range(1, 999)]
        public int DroughtLevel { get; set; }

        [Required]
        public int TimeOutInMinutes { get; set; } // Timeout to decide whether the station is alive or not

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [EmailAddress(ErrorMessage = "Invalid e-mail address.")]
        public string? AlertEmail1 { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [EmailAddress(ErrorMessage = "Invalid e-mail address.")]
        public string? AlertEmail2 { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [EmailAddress(ErrorMessage = "Invalid e-mail address.")]
        public string? AlertEmail3 { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? CreatedByUser { get; set; }

        public bool WarningSent { get; set; } = false; // Flag used in FloodMonitoringService class

        public virtual ICollection<Value>? Values { get; set; }
    }
}
