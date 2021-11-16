using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveManaged.Data
{
    public class Box
    {
        [Key]
        public int BoxId { get; set; }
        public string BoxSize { get; set; }

        public virtual MoveDetails MoveDetails { get; set; }
        [ForeignKey(nameof(MoveDetails))]
        public int MoveId { get; set; }

        public virtual Room Room { get; set; }
        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }

    }
}
