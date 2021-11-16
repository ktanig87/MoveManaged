using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveManaged.Models
{
    public class DamageClaimEdit
    {
        public string Description { get; set; }
        public bool ClaimSubmitted { get; set; }
        public string ClaimNotes { get; set; }
        public bool ClaimResolved { get; set; }
        public int InventoryId { get; set; }
    }
}
