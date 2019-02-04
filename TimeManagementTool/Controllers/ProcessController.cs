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
    class ProcessController
    {
        public Process[] RunningProcesses { get; set; }
        private List<ProcessCategory> _watchedProcesses;
        private MainWindow _view;
        private TimeManagementContext _context;
        private ManagementEventWatcher processStartEvent = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");
        private ManagementEventWatcher processStopEvent = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");

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
            _watchedProcesses = _context.Processes.ToList();

            processStartEvent.EventArrived += new EventArrivedEventHandler(processStartEvent_EventArrived);
            processStartEvent.Start();
            processStopEvent.EventArrived += new EventArrivedEventHandler(processStopEvent_EventArrived);
            processStopEvent.Start();
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

        private void processStartEvent_EventArrived(object sender, EventArrivedEventArgs e)
        {
            //e.NewEvent.Properties["ProcessName"];
            //_watchedProcesses.Contains()
            Console.WriteLine("Process started");
            return;

        }

        private void processStopEvent_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("Proces gestops");
            return;
        }
    }
}
