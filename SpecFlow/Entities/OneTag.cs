namespace SpecFlowProject.Entities
{
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
}