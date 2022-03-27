using System.Linq;

namespace SpecFlowProject.Entities
{
    public class Pet
    {
        public int? Id { get; set; }
        public Category? Category { get; set; }
        public string Name { get; set; }
        public string [] PhotoUrls { get; set; }
        public OneTag[]? Tags { get; set; }
        public string? Status { get; set; }

        public Pet(int? id, string name, string photoUrls, string status, Category category, OneTag[] tags)
        {
            Id = id;
            Name = name;
            PhotoUrls = photoUrls.Split(",").ToArray();
            Status = status;
            Category = category;
            Tags = tags;
        }
    }
}
