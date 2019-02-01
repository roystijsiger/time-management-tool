using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeManagementTool.Data;

namespace TimeManagementTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Database database = new Database();
        
        public MainWindow()
        {
            InitializeComponent();
            getProcesses();

            SetListeners();
        }

        private Process[] getProcesses()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                ProcessList.Items.Add(theprocess);
            }
            

            return processlist;


        }

        private void SetListeners()
        {
            addProcessButton.Click += AddProcess_click;
            addCategoryButton.Click += AddCategory_click;
        }

        private void AddProcess_click(object sender, RoutedEventArgs e)
        {
            List<Process> selectedProcess = ProcessList.SelectedItems.OfType<Process>().ToList();
            

           var processName = selectedProcess[0].ProcessName;

            MessageBox.Show(processName);
        }

        private void AddCategory_click(object sender, RoutedEventArgs e)
        {
            if (database.InsertCategory(txt_category_title.Text))
            {
                MessageBox.Show("Inserted succesfully");
            }
            else
            {
                MessageBox.Show("Couldn't insert the category");
            }
        }
    }
}
