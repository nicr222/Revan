using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MidStateShuttleService.Models
{
    [Index("RegistrationID", Name = "IX_RegistertionDaysModel_RegistrationID")]
    public partial class RegistertionDaysModel
    {
        [Key]
        public int RegistrationDayID { get; set; }

        public int RegistrationID { get; set; }

        [StringLength(10)]
        public string DayOfWeek { get; set; }

        [ForeignKey("RegistrationID")]
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
