using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementTool;
using TimeManagementTool.Data;
using TimeManagementTool.Models;

namespace TimeManagementTool.Controllers
{
    class CategoryController
    {
        private  MainWindow _view;
        private TimeManagementContext _context;

        public CategoryController(MainWindow main){
            this._view = main;
            this._context = new TimeManagementContext();
        }
        
        public void UpdateCategories()
        {
            List<Category> categories = this._context.Categories.ToList();
            this._view.ShowCategories(categories);
        }

        public void AddCategory(string title)
        {
            Category category = new Category(title);
            try
            {
                this._context.Categories.Add(category);
                this._view.UpdateCategories(category);
            }
            catch (Exception e)
            {
                this._view.ShowError(e.Message);
            }
            
        }
        
    }
}
