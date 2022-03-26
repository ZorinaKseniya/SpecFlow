
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.Helpers
{
    public class Category
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class OneTag
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public OneTag(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
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
            PhotoUrls = new [] {photoUrls};
            Status = status;
            Category = category;
            Tags = tags;
        }
    }
}
