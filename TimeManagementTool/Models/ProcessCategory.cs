using System.ComponentModel.DataAnnotations;

namespace TimeManagementTool.Models
{
    public class ProcessCategory
    {
        [Key]
        public string Name { get; set; }
        public Category Category { get; set; }

        public ProcessCategory(string name, Category category)
        {
            Name = name;
            Category = category;
        }

        public ProcessCategory()
        {

        }
    }
}