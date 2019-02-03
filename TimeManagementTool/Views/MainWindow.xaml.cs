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
using TimeManagementTool.Controllers;
using TimeManagementTool.Data;
using TimeManagementTool.Models;

namespace TimeManagementTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CategoryController categoryController;
        public MainWindow()
        {
            InitializeComponent();

            //set all listeners
            SetListeners();

            //initialize controller
            categoryController = new CategoryController(this);
            categoryController.UpdateCategories();
            getProcesses(); 
        }
    
        public void ShowCategories(List<Category> categories)
        {
            foreach (Category c in categories)
            {
                CategoryList.Items.Add(c);
            }

        }

        public void UpdateCategories(Category c)
        {
            CategoryList.Items.Add(c);
        }
        
        public void ShowError(string Error)
        {
            MessageBox.Show(Error);
        }


        private List<Category> getCategories()
        {
            List<Category> categories = new List<Category>();
            
            using(TimeManagementContext tmc = new TimeManagementContext())
            {
                categories = tmc.Categories.ToList();
            }
            return categories;
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
            using(TimeManagementContext tmc = new TimeManagementContext())
            {
                Category c = new Category();
                c.Title = txt_category_title.Text;
                try { 
                    tmc.Categories.Add(c);
                    tmc.SaveChanges();
                    CategoryList.Items.Add(c);
                    MessageBox.Show("Successfully added category");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
