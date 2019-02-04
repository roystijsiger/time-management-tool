using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementTool.Data;
using TimeManagementTool.Models;

namespace TimeManagementTool.Controllers
{
    class ProcessController
    {
        private MainWindow _view;
        private TimeManagementContext _context;
        public ProcessController(MainWindow view)
        {
            _view = view;
            _context = new TimeManagementContext();
        }

        public void GetRunningProcesses()
        {
            Process[] processList = Process.GetProcesses();
            _view.ShowProcesses(processList);

        }

        public void GetProccessesByCategory(Category category)
        {
            List<ProcessCategory> categoryProcesses = _context.Processes.Where(cp => cp.Category.Id == category.Id).ToList();
            _view.ShowProcessesByCategory(categoryProcesses);

        }

        public void AddProcessToCategory(int categoryId, int processId, string processName)
        {
                Category c = _context.Categories.Find(categoryId);
                ProcessCategory processCategory = new ProcessCategory(processId, processName, c);
                _context.Processes.Add(processCategory);
                _context.SaveChanges();
                _view.AddSingleProcessToProcessByCategoryList(processCategory);
        }
    }
}
