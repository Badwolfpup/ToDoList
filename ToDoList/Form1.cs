using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        private List<System.Timers.Timer> timers = new List<System.Timers.Timer>();

        public Form1()
        {
            InitializeComponent();
            LoadTasks();
            SetupTimers();
        }

        private void LoadTasks()
        {
            string fileName = @"C:\";
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    string[] taskDetails = line.Split('|');
                    Task task = new Task
                    {
                        Title = taskDetails[0],
                        Deadline = DateTime.Parse(taskDetails[1]),
                        Alarms = new List<DateTime>()
                    };
                    for (int i = 2; i < taskDetails.Length; i++)
                    {
                        task.Alarms.Add(DateTime.Parse(taskDetails[i]));
                    }
                    tasks.Add(task);
                    listBoxTasks.Items.Add(task.Title);
                }
            }
            SetupTimers();
        }

        private void SetupTimers()
        {
            // Clear previous timers
            foreach (var timer in timers)
            {
                timer.Stop();
                timer.Dispose();
            }
            timers.Clear();

            // Setup new timers for each task's alarms
            foreach (var task in tasks)
            {
                foreach (var alarm in task.Alarms)
                {
                    var timer = new System.Timers.Timer
                    {
                        Interval = (alarm - DateTime.Now).TotalMilliseconds,
                        AutoReset = false
                    };
                    timer.Elapsed += (sender, e) => ShowNotification(task.Title, alarm);
                    timer.Start();
                    timers.Add(timer);
                }
            }
        }

        private void ShowNotification(string taskTitle, DateTime alarmTime)
        {
            MessageBox.Show($"Reminder: Task '{taskTitle}' is due at {alarmTime}!");
        }
        private void SaveTasks()
        {
            string fileName = "tasks.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Task task in tasks)
                {
                    writer.Write(task.Title + "|" + task.Deadline);
                    foreach (DateTime alarm in task.Alarms)
                    {
                        writer.Write("|" + alarm);
                    }
                    writer.WriteLine();
                }
            }
        }

        private void AddTask()
        {
            DateTime deadline = dateTimePickerDeadline.Value;
            List<DateTime> alarms = new List<DateTime>
            {
                deadline.AddHours(-6),
                deadline.AddHours(-3),
                deadline.AddHours(-1)
            };

            Task newTask = new Task
            {
                Title = textBoxTask.Text,
                Deadline = deadline,
                Alarms = alarms
            };

            tasks.Add(newTask);
            listBoxTasks.Items.Add(newTask.Title);
            SaveTasks();
        }

        private void RemoveTask()
        {
            if (listBoxTasks.SelectedIndex != -1)
            {
                tasks.RemoveAt(listBoxTasks.SelectedIndex);
                listBoxTasks.Items.RemoveAt(listBoxTasks.SelectedIndex);
                SaveTasks();
            }
        }

        private void EditTask()
        {
            if (listBoxTasks.SelectedIndex != -1)
            {
                int selectedIndex = listBoxTasks.SelectedIndex;
                tasks[selectedIndex].Title = textBoxTask.Text;
                tasks[selectedIndex].Deadline = dateTimePickerDeadline.Value;

                // Update alarms
                tasks[selectedIndex].Alarms.Clear();
                DateTime deadline = tasks[selectedIndex].Deadline;
                tasks[selectedIndex].Alarms.AddRange(new List<DateTime>
                {
                    deadline.AddHours(-6),
                    deadline.AddHours(-3),
                    deadline.AddHours(-1)
                });

                listBoxTasks.Items[selectedIndex] = tasks[selectedIndex].Title;
                SaveTasks();
            }
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedIndex != -1)
            {
                textBoxTask.Text = tasks[listBoxTasks.SelectedIndex].Title;
                dateTimePickerDeadline.Value = tasks[listBoxTasks.SelectedIndex].Deadline;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddTask();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            RemoveTask();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        private class Task
        {
            public string Title { get; set; }
            public DateTime Deadline { get; set; }
            public List<DateTime> Alarms { get; set; }
        }
    }
}

