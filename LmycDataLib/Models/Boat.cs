using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmycDataLib.Models
{
    public class Boat
    {
        public int BoatId { get; set; }
        public string BoatName { get; set; }
        public string Picture { get; set; }
        public int LengthInFeet { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public DateTime RecordCreationDate { get; set; }


        [ForeignKey("User")]
        [ScaffoldColumn(false)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public ApplicationUser User { get; set; }
    }
}
