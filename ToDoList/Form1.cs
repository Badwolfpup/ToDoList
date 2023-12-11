using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        private List<System.Timers.Timer> timers = new List<System.Timers.Timer>();
        string fileName = @"C:\c#-projekt\todolist\todolist.txt";


        public Form1()
        {
            InitializeComponent();
            LoadTasks();
            SetupTimers();
        }

        private void LoadTasks()
        {
 
        }

        private void SetupTimers()
        {
 
        }

        private void ShowNotification(string taskTitle, DateTime alarmTime)
        {
           
        }
        private void SaveTasks()
        {

        }

        private void AddTask()
        {
 
        }

        private void RemoveTask()
        {
 
        }

        private void EditTask()
        {
 
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
   
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
        }

        private class Task
        {

        }
    }
}

