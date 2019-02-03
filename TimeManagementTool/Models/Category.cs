using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementTool.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ProcessCategory> Processes;

        public Category(int id, string title)
        {
            this.Id = id;
            this.Title = title;

        }
        
        public Category(string title)
        {
            this.Title = title;
        }

        public Category()
        {

        }
    }
}
