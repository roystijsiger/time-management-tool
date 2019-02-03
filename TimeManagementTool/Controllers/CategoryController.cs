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
            _view = main;
            _context = new TimeManagementContext();
        }
        
        public void GetCategories()
        {
            List<Category> categories = this._context.Categories.ToList();
            _view.ShowCategories(categories);
        }

        public void AddCategory(string title)
        {
            Category category = new Category(title);
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                _view.UpdateCategories(category);
            }
            catch (Exception e)
            {
                _view.ShowError(e.Message);
            }
            
        }
        
    }
}
