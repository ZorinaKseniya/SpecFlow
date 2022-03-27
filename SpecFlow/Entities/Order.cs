using System;

namespace SpecFlowProject.Entities
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
            PetId = petId;
            Quantity = quantity;
            ShipDate = shipDate;
            Status = status;
            Complete = complete;
        }
    }
}
