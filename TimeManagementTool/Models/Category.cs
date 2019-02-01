﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementTool.Models
{
    class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Category(int id, string title)
        {
            this.Id = id;
            this.Title = title;

        }

        public Category()
        {

        }
    }
}