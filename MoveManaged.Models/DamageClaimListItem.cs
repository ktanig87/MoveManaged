﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveManaged.Models
{
    public class DamageClaimListItem
    {
        public int ClaimId { get; set; }
        public bool ClaimSubmitted { get; set; }
        public bool ClaimResolved { get; set; }
        public int InventoryId { get; set; }
    }
}
