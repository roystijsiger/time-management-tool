using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementTool.Data;
using TimeManagementTool.Models;
using System.Management;

namespace TimeManagementTool.Controllers
{
    public class ProcessController
    {
        public Process[] RunningProcesses { get; set; }
        private List<ProcessCategory> _watchedProcesses;
        private MainWindow _view;
        private TimeManagementContext _context;

        public ProcessController(MainWindow view)
        {
            RunningProcesses = Process.GetProcesses();
            _view = view;
            _context = new TimeManagementContext();
        }

        public void GetRunningProcesses()
        {
            _view.ShowProcesses(RunningProcesses);
        }

        public void WatchProcesses()
        {
            List<ProcessCategory> processCategories = _context.Processes.ToList();
            foreach(ProcessCategory pc in processCategories)
            {
                //check if the process is running
                if(Process.GetProcessesByName(pc.Name).Length > 0)
                {
                    var process = Process.GetProcessesByName(pc.Name)[0];
                    var handle = process.SafeHandle;
                    process.EnableRaisingEvents = true;
                    process.Exited += new EventHandler(OnExit_event);
                }
                

            }
        }

        /*public void WatchProcess(string ProcessName)
        {
            Process process = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            DateTime startedAt = process.StartTime;

            var handle = process.SafeHandle;
            process.WaitForExit();
            DateTime closedAt = process.ExitTime;

            TimeSpan timeDifference = closedAt - startedAt;

        }*/

        public void GetProccessesByCategory(Category category)
        {
            List<ProcessCategory> categoryProcesses = _context.Processes.Where(cp => cp.Category.Id == category.Id).ToList();
            _view.ShowProcessesByCategory(categoryProcesses);

        }

        public void AddProcessToCategory(int categoryId, string processName)
        {
                Category c = _context.Categories.Find(categoryId);
                ProcessCategory processCategory = new ProcessCategory( processName, c);
                _context.Processes.Add(processCategory);
                _context.SaveChanges();
                _view.AddSingleProcessToProcessByCategoryList(processCategory);
        }

        private void OnExit_event(object sender, EventArgs e)
        {

            Process p = (Process)sender;
            TimeSpan userProcessorTime = p.UserProcessorTime;

            return;
        }
    }
}
