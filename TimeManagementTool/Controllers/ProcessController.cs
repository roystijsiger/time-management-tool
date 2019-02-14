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
        public Process[] _runningProcesses { get; set; }
        private List<ProcessCategory> _watchedProcesses;
        private MainWindow _view;
        private TimeManagementContext _context;

        public ProcessController(MainWindow view)
        {
            _view = view;

            _context = new TimeManagementContext();
            _watchedProcesses = _context.Processes.ToList();

            watchProcesses();
            listenForCreate();
        }

        public void GetRunningProcesses()
        {
            //List<Process> processList = Process.GetProcesses().ToList();
            //Process[] a = Process.GetProcesses().ToList();

            Process[] processes = Process.GetProcesses().Distinct(new ProcessEqualityComparer()).ToArray();
            //var b = processList.GroupBy(g => g.ProcessName, p => p).Select(s => s);
            //var processListB = b.OfType<Process>().ToList();


            //Process[] processes 
            //return; 
            _view.ShowProcesses(processes);
        }

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
                _watchedProcesses.Add(processCategory);
                _view.AddSingleProcessToProcessByCategoryList(processCategory);
        }

        private void listenForCreate()
        {
            ManagementEventWatcher startWatch = new ManagementEventWatcher(
            new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            startWatch.EventArrived
                                += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();
        }

        private void listenForExit(string processName)
        {
            var process = Process.GetProcessesByName(processName)[0];
            Console.WriteLine("Process {0} is now being watched for close event", process.ProcessName.ToString());
            var handle = process.SafeHandle;
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(onExit_event);
        }

        private void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Process[] runningProcesses = Process.GetProcesses();

            if (isProcessWatched(e.NewEvent.Properties["ProcessName"].Value.ToString()))
            {
                Console.WriteLine("Process started: {0}"
                         , e.NewEvent.Properties["ProcessName"].Value);
                watchProcess(removeDotExe(e.NewEvent.Properties["ProcessName"].Value.ToString()));
            }
            
        }

        private void onExit_event(object sender, EventArgs e)
        {

            Process p = (Process)sender;


            TimeSpan processDuration = p.ExitTime -  p.StartTime;
            Console.WriteLine("Process stopped: {0} was running for {1}", p.ProcessName.ToString(), processDuration.ToString());

            return;
        }

        private void watchProcesses()
        {
            _watchedProcesses = _context.Processes.ToList();
            foreach (ProcessCategory pc in _watchedProcesses)
            {

                //check if the process is running
                if (isProcessRunning(pc.Name))
                {
                    listenForExit(pc.Name);

                }
            }
        }

        private void watchProcess(string processName)
        {
            if (isProcessRunning(processName))
            {
                listenForExit(processName);
            }
        }

        private Boolean isProcessRunning(string processName)
        {
            if (Process.GetProcessesByName(processName).Length > 0)
            {
                return true;
            }
            return false;
        }

        private Boolean isProcessWatched(string processName)
        {
            foreach(ProcessCategory pc in _watchedProcesses){
                if(pc.Name == removeDotExe(processName))
                {
                    return true;
                }
            }
            return false;
        }

        private string removeDotExe(string processName)
        {
            String[] divided=  processName.Split('.');
            if(divided.Length > 1)
            {
                return divided[0];
            }
            return processName;
        }
        
    }

    class ProcessEqualityComparer : IEqualityComparer<Process>
    {
        public bool Equals(Process x, Process y)
        {
            return x.ProcessName.Equals(y.ProcessName);
        }

        public int GetHashCode(Process obj)
        {
            return obj.ProcessName.GetHashCode();
        }
    }
}
