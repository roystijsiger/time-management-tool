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
        private TimeManagementContext _tmc;
        public ProcessController(MainWindow view)
        {
            _view = view;
            _tmc = new TimeManagementContext();
        }

        public void GetRunningProcesses()
        {
            Process[] processList = Process.GetProcesses();
            _view.ShowProcesses(processList);

        }

        public void GetProccessesByCategory(Category category)
        {
            List<ProcessCategory> categoryProcesses = _tmc.Processes.Where(cp => cp.Category.Id == category.Id).ToList();
            _view.ShowProcessesByCategory(categoryProcesses);

        }

        public void AddProcessToCategory(int categoryId, int processId, string processName)
        {
                Category c = _tmc.Categories.Find(categoryId);
                ProcessCategory processCategory = new ProcessCategory(processId, processName, c);
                _tmc.Processes.Add(processCategory);
                _tmc.SaveChanges();
                _view.AddSingleProcessToProcessByCategoryList(processCategory);
        }
    }
}
