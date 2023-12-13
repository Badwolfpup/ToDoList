using System.Windows.Forms;
using System.Media;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        private List<System.Timers.Timer> timers = new List<System.Timers.Timer>();
        string fileName = @"C:\c#-projekt\todolist\todolist.txt";
        string folderPathSound = @"C:\c#-projekt\todolist\Sounds\";
        string folderPathTextfile = @"C:\c#-projekt\todolist\";
        private NotifyIcon notifyIcon;
        private ToolTip toolTip;


        public Form1()
        {
            InitializeComponent();
            LoadTasks();
            SetupTimers();
            InitializeNotifyIcon();
            CreateTooltip();
        }


        private void CreateTooltip()
        {
            toolTip = new ToolTip();
            toolTip.SetToolTip(buttonAdd, "Lägger till ett alarm");
            toolTip.ToolTipIcon = ToolTipIcon.Warning;
            toolTip.ToolTipTitle = "Det här är ett tooltip";
            toolTip.IsBalloon = true;
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Visible = false;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        private void NotifyIcon_DoubleClick(object? sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false; 
        }

        private void Form1_Resize(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (notifyIcon != null)
            {
                notifyIcon.Dispose();
            }
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
                if (Directory.Exists(folderPathTextfile)) 
                    using (FileStream fs = File.Create(fileName)) { } 
                else
                {
                    Directory.CreateDirectory(folderPathTextfile);
                    using (FileStream fs = File.Create(fileName)) { }
                }
            }
            folderCreated:
            if (Directory.Exists(folderPathSound))
            {
                try
                {
                    // Get all file names in the folder
                    string[] fileNames = Directory.GetFiles(folderPathSound);

                    foreach (string fileName in fileNames)
                    {
                        string file = Path.GetFileName(fileName);
                        file = file.Remove(file.Length - 4);
                        comboBoxSound.Items.Add(file);
                    }
                    if (comboBoxSound.Items.Count > 0) comboBoxSound.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                }
            }
            else { 
                Directory.CreateDirectory(folderPathSound);
                goto folderCreated;
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
            if ((dateTimePickerDeadline.Value - DateTime.Now).Minutes > 180) alarms.Add(deadline.AddHours(-3));
            if ((dateTimePickerDeadline.Value - DateTime.Now).Minutes > 60) alarms.Add(deadline.AddHours(-1));
            alarms.Add(deadline.AddMilliseconds(-1));

            Task newTask = new Task
            {
                Title = textBoxTask.Text,
                Deadline = deadline,
                Alarms = alarms,
                Sound = folderPathSound + comboBoxSound.Text + ".wav"
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
                dateTimePickerDeadline.Value = DateTime.Now;
                SaveTasks();
            }
            textBoxTask.Clear();
            //if (listBoxTasks.Items.Count > 0) listBoxTasks.SelectedIndex = 0;
            dateTimePickerDeadline.Value = DateTime.Now;
        }

        private void EditTask()
        {
            if (listBoxTasks.SelectedIndex != -1)
            {
                int selectedIndex = listBoxTasks.SelectedIndex;
                tasks[selectedIndex].Title = textBoxTask.Text;
                tasks[selectedIndex].Deadline = dateTimePickerDeadline.Value;
                tasks[selectedIndex].Sound = folderPathSound + comboBoxSound.Text;
                tasks[selectedIndex].Alarms.Clear();
                DateTime deadline = tasks[selectedIndex].Deadline;

                if ((dateTimePickerDeadline.Value - DateTime.Now).Minutes > 360) 
                    tasks[selectedIndex].Alarms.Add(deadline.AddHours(-6));
                if ((dateTimePickerDeadline.Value - DateTime.Now).Minutes > 180) 
                    tasks[selectedIndex].Alarms.Add(deadline.AddHours(-3));
                if ((dateTimePickerDeadline.Value - DateTime.Now).Minutes > 60) 
                    tasks[selectedIndex].Alarms.Add(deadline.AddHours(-1));
                tasks[selectedIndex].Alarms.Add(deadline.AddMilliseconds(-1));
                
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

