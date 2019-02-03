namespace TimeManagementTool.Models
{
    public class ProcessCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }

        public ProcessCategory(int id, string name, Category category)
        {
            Id = id;
            Name = name;
            Category = category;
        }

        public ProcessCategory()
        {

        }
    }
}