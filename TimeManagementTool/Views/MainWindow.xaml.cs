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
        private ProcessController processController;

        public MainWindow()
        {
            InitializeComponent();
            

            //initialize controller
            categoryController = new CategoryController(this);
            categoryController.GetCategories();

            processController = new ProcessController(this);
            processController.GetRunningProcesses();
        }
        
        //Display categories in the listview
        public void ShowCategories(List<Category> categories)
        {
            foreach (Category c in categories)
            {
                CategoryList.Items.Add(c);
            }

        }

        //add single category to the list
        public void UpdateCategories(Category c)
        {
            CategoryList.Items.Add(c);
        }
        
        //display error
        public void ShowError(string Error)
        {
            MessageBox.Show(Error);
        }

        //display proccesses by selected category
        public void ShowProcessesByCategory(List<ProcessCategory> categoryProcesses) 
        {
            ProcessCategoryList.Items.Clear();
            foreach(ProcessCategory pc in categoryProcesses)
            {
                ProcessCategoryList.Items.Add(pc); 
            }
        }

        //add a single process to selected category list
        public void AddSingleProcessToProcessByCategoryList(ProcessCategory categoryProcess)
        {
            ProcessCategoryList.Items.Add(categoryProcess);
        }

        //display processes in the lsitview
        public void ShowProcesses(Process[] processList)
        {
          
            foreach (Process process in processList)
            {
                ProcessList.Items.Add(process);
            }
        }
        

        //if the selected category is changed
        private void CategoryList_selectionChanged(object sender, SelectionChangedEventArgs args)
        {
            Category c = (Category)args.AddedItems[0];


            processController.GetProccessesByCategory(c);

        }

        //if add process is clicked this event is triggered.
        private void AddProcess_click(object sender, RoutedEventArgs e)
        {
            List<Process> selectedProcesses = ProcessList.SelectedItems.OfType<Process>().ToList();
            List<Category> selectedCategories = CategoryList.SelectedItems.OfType<Category>().ToList();

            var processName = selectedProcesses[0].ProcessName;
            processController.AddProcessToCategory(selectedCategories[0].Id, selectedProcesses[0].Id, selectedProcesses[0].ProcessName);
        }

        //if add category is clicked this event is triggered

        private void AddCategory_click(object sender, RoutedEventArgs e)
        {
            string title = txt_category_title.Text;
            categoryController.AddCategory(title);
        }
    }
}
