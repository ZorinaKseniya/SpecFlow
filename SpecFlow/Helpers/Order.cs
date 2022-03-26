using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.Helpers
{
    public class Order
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int Quantity { get; set; }
        public DateTime ShipDate { get; set; }
        public string Status { get; set; }
        public bool Complete { get; set; }
        public Order (int id, int petId, int quantity, DateTime shipDate, string status, bool complete)
        {
            Id = id;
            PetId = PetId;
            Quantity = quantity;
            ShipDate = shipDate;
            Status = status;
            Complete = complete;
        }
    }
}
