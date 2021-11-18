using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveManaged.Models
{
    public class MoveEdit
    {
        public int Id { get; set; }
        public string MoverName { get; set; }
        public int DriverPhone { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int TSPPhone { get; set; }
    }
}
