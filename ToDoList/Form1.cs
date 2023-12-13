using System.Windows.Forms;
using System.Media;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        private List<System.Timers.Timer> timers = new List<System.Timers.Timer>();
        string fileName = @"C:\c#-projekt\todolist\todolist.txt";
        string folderPath = @"C:\c#-projekt\todolist\Sounds\";


        public Form1()
        {
            InitializeComponent();
            LoadTasks();
            SetupTimers();
        }

        private void LoadTasks()
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string s in lines)
                {
                    string[] taskdetails = s.Split('|');
                    Task task = new Task
                    {
                        Title = taskdetails[0],
                        Deadline = DateTime.Parse(taskdetails[1]),
                        Sound = taskdetails[2],
                        Alarms = new List<DateTime>()
                    };
                    for (int i = 3; i < taskdetails.Length; i++)
                    {
                        task.Alarms.Add(DateTime.Parse(taskdetails[i]));
                    }
                    tasks.Add(task);
                    listBoxTasks.Items.Add(task.Title);
                }

            }
            else
            {
                using (FileStream fs = File.Create(fileName)) { } 
            }
            if (Directory.Exists(folderPath))
            {
                try
                {
                    // Get all file names in the folder
                    string[] fileNames = Directory.GetFiles(folderPath);

                    foreach (string fileName in fileNames)
                    {
                        string file = Path.GetFileName(fileName);
                        comboBoxSound.Items.Add(file);
                    }
                    if (comboBoxSound.Items.Count > 0) comboBoxSound.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                }
            }
            SetupTimers();
            if (listBoxTasks.Items.Count > 0) listBoxTasks.SelectedIndex = 0;
        } 

        private void SetupTimers()
        {
            foreach (var timer in timers)
            {
                timer.Stop();
                timer.Dispose();
            }

            foreach (var task in tasks)
            {
                foreach (var alarm in task.Alarms) 
                {
                    TimeSpan timeUntilAlarm = alarm - DateTime.Now;

                    var timer = new System.Timers.Timer();

                    if (timeUntilAlarm.Microseconds > 0) timer.Interval = timeUntilAlarm.TotalMilliseconds;
                    timer.AutoReset = false;

                    timer.Elapsed += (sender, e) =>
                    ShowNotification(task.Title, alarm, task.Sound);
                    timer.Start();
                    timers.Add(timer);

                }
            }
           

        }

        private void ShowNotification(string taskTitle, DateTime alarmTime, string sound)
        {
            if (File.Exists(sound))
            {
                using (SoundPlayer s  = new SoundPlayer(sound))
                {
                    s.Play();
                }
            }
            MessageBox.Show($"Reminder: Task '{taskTitle}' is due at {alarmTime}");
            
        }
        private void SaveTasks()
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach(Task task in tasks)
                {
                    sw.Write(task.Title + "|" + task.Deadline + "|" + task.Sound);
                    foreach (DateTime alarm in task.Alarms)
                    {
                        sw.Write("|" + alarm);
                    }
                    sw.WriteLine();
                }
            }
        }

        private void AddTask()
        {
            DateTime deadline = dateTimePickerDeadline.Value;
            List<DateTime> alarms = new List<DateTime>();

            if ((dateTimePickerDeadline.Value - DateTime.Now).Minutes > 360) alarms.Add(deadline.AddHours(-6));
            if ((dateTimePickerDeadline.Value - DateTime.Now).Minutes > 360) alarms.Add(deadline.AddHours(-3));
            if ((dateTimePickerDeadline.Value - DateTime.Now).Minutes > 360) alarms.Add(deadline.AddHours(-1));
            alarms.Add(deadline.AddMilliseconds(-1));

            Task newTask = new Task
            {
                Title = textBoxTask.Text,
                Deadline = deadline,
                Alarms = alarms,
                Sound = folderPath + comboBoxSound.Text
            };
            tasks.Add(newTask);
            listBoxTasks.Items.Add(newTask);
            listBoxTasks.Items[listBoxTasks.Items.Count - 1] = tasks[listBoxTasks.Items.Count - 1].Title;
            SaveTasks();
            SetupTimers();
        }

        private void RemoveTask()
        {
            if(listBoxTasks.SelectedIndex != -1)
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
                tasks[selectedIndex].Sound = folderPath + comboBoxSound.Text;
                tasks[selectedIndex].Alarms.Clear();
                DateTime deadline = tasks[selectedIndex].Deadline;
                tasks[selectedIndex].Alarms.AddRange(new List<DateTime>()
                {
                    deadline.AddHours(-6),
                    deadline.AddHours(-3),
                    deadline.AddHours(-1)
                }); ;
                listBoxTasks.Items[selectedIndex] =
                    tasks[selectedIndex].Title;
                SaveTasks();
            }
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedIndex != -1)
            {
                textBoxTask.Text = tasks[listBoxTasks.SelectedIndex].Title; 
                dateTimePickerDeadline.Value = 
                    tasks[listBoxTasks.SelectedIndex].Deadline;
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

            public string Sound { get; set; }
            public DateTime Deadline { get; set; }
            public List<DateTime> Alarms { get; set; }
        } 
    }
}

