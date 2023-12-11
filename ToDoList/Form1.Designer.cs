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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
            this.textBoxTask = new System.Windows.Forms.TextBox();
            this.dateTimePickerDeadline = new System.Windows.Forms.DateTimePicker();
            this.labelTask = new System.Windows.Forms.Label();
            this.labelDeadline = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.listBoxTasks = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // textBoxTask
            this.textBoxTask.Location = new System.Drawing.Point(150, 30);
            this.textBoxTask.Size = new System.Drawing.Size(200, 20);

            // dateTimePickerDeadline
            this.dateTimePickerDeadline.Location = new System.Drawing.Point(150, 70);
            this.dateTimePickerDeadline.Size = new System.Drawing.Size(200, 20);

            // labelTask
            this.labelTask.AutoSize = true;
            this.labelTask.Location = new System.Drawing.Point(50, 30);
            this.labelTask.Text = "Task:";

            // labelDeadline
            this.labelDeadline.AutoSize = true;
            this.labelDeadline.Location = new System.Drawing.Point(50, 70);
            this.labelDeadline.Text = "Deadline:";

            // buttonAdd
            this.buttonAdd.Location = new System.Drawing.Point(50, 110);
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.Text = "Add";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);

            // buttonRemove
            this.buttonRemove.Location = new System.Drawing.Point(150, 110);
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);

            // buttonEdit
            this.buttonEdit.Location = new System.Drawing.Point(250, 110);
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);

            // listBoxTasks
            this.listBoxTasks.FormattingEnabled = true;
            this.listBoxTasks.Location = new System.Drawing.Point(50, 150);
            this.listBoxTasks.Size = new System.Drawing.Size(300, 200);
            this.listBoxTasks.SelectedIndexChanged += new System.EventHandler(this.listBoxTasks_SelectedIndexChanged);




            // ToDoListForm
            this.ClientSize = new System.Drawing.Size(400, 380);
            this.Controls.Add(this.textBoxTask);
            this.Controls.Add(this.dateTimePickerDeadline);
            this.Controls.Add(this.labelTask);
            this.Controls.Add(this.labelDeadline);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.listBoxTasks);
            this.Name = "ToDoListForm";
            this.Text = "To-Do List Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

            this.dateTimePickerDeadline.CustomFormat = "dd MMM HH:mm";
            this.dateTimePickerDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDeadline.Location = new System.Drawing.Point(150, 70);
            this.dateTimePickerDeadline.Size = new System.Drawing.Size(200, 20);
        }
        private System.Windows.Forms.TextBox textBoxTask;
        private System.Windows.Forms.DateTimePicker dateTimePickerDeadline;
        private System.Windows.Forms.Label labelTask;
        private System.Windows.Forms.Label labelDeadline;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.ListBox listBoxTasks;
        #endregion
    }
}
