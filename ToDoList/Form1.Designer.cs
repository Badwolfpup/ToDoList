namespace ToDoList
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxTask = new TextBox();
            dateTimePickerDeadline = new DateTimePicker();
            labelTask = new Label();
            labelDeadline = new Label();
            buttonAdd = new Button();
            buttonRemove = new Button();
            buttonEdit = new Button();
            listBoxTasks = new ListBox();
            SuspendLayout();
            // 
            // textBoxTask
            // 
            textBoxTask.Location = new Point(150, 30);
            textBoxTask.Name = "textBoxTask";
            textBoxTask.Size = new Size(200, 31);
            textBoxTask.TabIndex = 0;
            // 
            // dateTimePickerDeadline
            // 
            dateTimePickerDeadline.CustomFormat = "dd MMM HH:mm";
            dateTimePickerDeadline.Format = DateTimePickerFormat.Custom;
            dateTimePickerDeadline.Location = new Point(150, 70);
            dateTimePickerDeadline.Name = "dateTimePickerDeadline";
            dateTimePickerDeadline.Size = new Size(200, 31);
            dateTimePickerDeadline.TabIndex = 1;
            // 
            // labelTask
            // 
            labelTask.AutoSize = true;
            labelTask.Location = new Point(50, 30);
            labelTask.Name = "labelTask";
            labelTask.Size = new Size(49, 25);
            labelTask.TabIndex = 2;
            labelTask.Text = "Task:";
            // 
            // labelDeadline
            // 
            labelDeadline.AutoSize = true;
            labelDeadline.Location = new Point(50, 70);
            labelDeadline.Name = "labelDeadline";
            labelDeadline.Size = new Size(85, 25);
            labelDeadline.TabIndex = 3;
            labelDeadline.Text = "Deadline:";
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(50, 110);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 35);
            buttonAdd.TabIndex = 4;
            buttonAdd.Text = "Add";
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new Point(150, 110);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(75, 35);
            buttonRemove.TabIndex = 5;
            buttonRemove.Text = "Remove";
            buttonRemove.Click += buttonRemove_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(250, 110);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(75, 35);
            buttonEdit.TabIndex = 6;
            buttonEdit.Text = "Edit";
            buttonEdit.Click += buttonEdit_Click;
            // 
            // listBoxTasks
            // 
            listBoxTasks.FormattingEnabled = true;
            listBoxTasks.ItemHeight = 25;
            listBoxTasks.Location = new Point(50, 168);
            listBoxTasks.Name = "listBoxTasks";
            listBoxTasks.Size = new Size(300, 179);
            listBoxTasks.TabIndex = 7;
            listBoxTasks.SelectedIndexChanged += listBoxTasks_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 380);
            Controls.Add(textBoxTask);
            Controls.Add(dateTimePickerDeadline);
            Controls.Add(labelTask);
            Controls.Add(labelDeadline);
            Controls.Add(buttonAdd);
            Controls.Add(buttonRemove);
            Controls.Add(buttonEdit);
            Controls.Add(listBoxTasks);
            Name = "Form1";
            Text = "To-Do List Manager";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox textBoxTask;
        private DateTimePicker dateTimePickerDeadline;
        private Label labelTask;
        private Label labelDeadline;
        private Button buttonAdd;
        private Button buttonRemove;
        private Button buttonEdit;
        private ListBox listBoxTasks;
        #endregion
    }
}
