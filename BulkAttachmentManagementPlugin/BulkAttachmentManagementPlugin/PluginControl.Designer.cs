namespace BulkAttachmentManagementPlugin
{
    partial class PluginControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupStep2 = new System.Windows.Forms.GroupBox();
            this.RadioReportOnly = new System.Windows.Forms.RadioButton();
            this.lbCSVLocation = new System.Windows.Forms.Label();
            this.RadioDownloadSpecific = new System.Windows.Forms.RadioButton();
            this.RadioDownloadAll = new System.Windows.Forms.RadioButton();
            this.ButtonCsvBrowse = new System.Windows.Forms.Button();
            this.TextBoxCsvLocation = new System.Windows.Forms.TextBox();
            this.GroupStep3 = new System.Windows.Forms.GroupBox();
            this.ButtonRun = new System.Windows.Forms.Button();
            this.ListViewMainOutput = new System.Windows.Forms.ListView();
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDownloadLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRegardingEntity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRegardID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chErrorMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelMainOutput = new System.Windows.Forms.Label();
            this.OpenFileDianlogCvsFile = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialogCsvFile = new System.Windows.Forms.SaveFileDialog();
            this.FolderBrowserDialogMainFile = new System.Windows.Forms.FolderBrowserDialog();
            this.GroupStep1 = new System.Windows.Forms.GroupBox();
            this.RadioFiles = new System.Windows.Forms.RadioButton();
            this.RadioAll = new System.Windows.Forms.RadioButton();
            this.RadioNotes = new System.Windows.Forms.RadioButton();
            this.RadioEmail = new System.Windows.Forms.RadioButton();
            this.GroupStep4 = new System.Windows.Forms.GroupBox();
            this.ButtonExport = new System.Windows.Forms.Button();
            this.GroupStep2.SuspendLayout();
            this.GroupStep3.SuspendLayout();
            this.GroupStep1.SuspendLayout();
            this.GroupStep4.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupStep2
            // 
            this.GroupStep2.Controls.Add(this.RadioReportOnly);
            this.GroupStep2.Controls.Add(this.lbCSVLocation);
            this.GroupStep2.Controls.Add(this.RadioDownloadSpecific);
            this.GroupStep2.Controls.Add(this.RadioDownloadAll);
            this.GroupStep2.Controls.Add(this.ButtonCsvBrowse);
            this.GroupStep2.Controls.Add(this.TextBoxCsvLocation);
            this.GroupStep2.Enabled = false;
            this.GroupStep2.Location = new System.Drawing.Point(209, 23);
            this.GroupStep2.Name = "GroupStep2";
            this.GroupStep2.Size = new System.Drawing.Size(725, 88);
            this.GroupStep2.TabIndex = 1;
            this.GroupStep2.TabStop = false;
            this.GroupStep2.Text = "Step 2: (What To Download.)";
            // 
            // RadioReportOnly
            // 
            this.RadioReportOnly.AutoSize = true;
            this.RadioReportOnly.Location = new System.Drawing.Point(250, 19);
            this.RadioReportOnly.Name = "RadioReportOnly";
            this.RadioReportOnly.Size = new System.Drawing.Size(89, 17);
            this.RadioReportOnly.TabIndex = 5;
            this.RadioReportOnly.Text = "Report ONLY";
            this.RadioReportOnly.UseVisualStyleBackColor = true;
            this.RadioReportOnly.CheckedChanged += new System.EventHandler(this.RadioReportOnly_CheckedChanged);
            // 
            // lbCSVLocation
            // 
            this.lbCSVLocation.AutoSize = true;
            this.lbCSVLocation.Location = new System.Drawing.Point(7, 44);
            this.lbCSVLocation.Name = "lbCSVLocation";
            this.lbCSVLocation.Size = new System.Drawing.Size(0, 13);
            this.lbCSVLocation.TabIndex = 4;
            // 
            // RadioDownloadSpecific
            // 
            this.RadioDownloadSpecific.AutoSize = true;
            this.RadioDownloadSpecific.Location = new System.Drawing.Point(119, 20);
            this.RadioDownloadSpecific.Name = "RadioDownloadSpecific";
            this.RadioDownloadSpecific.Size = new System.Drawing.Size(125, 17);
            this.RadioDownloadSpecific.TabIndex = 3;
            this.RadioDownloadSpecific.Text = "Specific Attachments";
            this.RadioDownloadSpecific.UseVisualStyleBackColor = true;
            this.RadioDownloadSpecific.CheckedChanged += new System.EventHandler(this.RadioDownloadSpecific_CheckedChanged);
            // 
            // RadioDownloadAll
            // 
            this.RadioDownloadAll.AutoSize = true;
            this.RadioDownloadAll.Location = new System.Drawing.Point(7, 20);
            this.RadioDownloadAll.Name = "RadioDownloadAll";
            this.RadioDownloadAll.Size = new System.Drawing.Size(98, 17);
            this.RadioDownloadAll.TabIndex = 2;
            this.RadioDownloadAll.Text = "All Attachments";
            this.RadioDownloadAll.UseVisualStyleBackColor = true;
            this.RadioDownloadAll.CheckedChanged += new System.EventHandler(this.RadioDownloadAll_CheckedChanged);
            // 
            // ButtonCsvBrowse
            // 
            this.ButtonCsvBrowse.Enabled = false;
            this.ButtonCsvBrowse.Location = new System.Drawing.Point(644, 61);
            this.ButtonCsvBrowse.Name = "ButtonCsvBrowse";
            this.ButtonCsvBrowse.Size = new System.Drawing.Size(75, 20);
            this.ButtonCsvBrowse.TabIndex = 1;
            this.ButtonCsvBrowse.Text = "Browse";
            this.ButtonCsvBrowse.UseVisualStyleBackColor = true;
            this.ButtonCsvBrowse.Click += new System.EventHandler(this.ButtonCsvBrowse_Click);
            // 
            // TextBoxCsvLocation
            // 
            this.TextBoxCsvLocation.Enabled = false;
            this.TextBoxCsvLocation.Location = new System.Drawing.Point(7, 62);
            this.TextBoxCsvLocation.Name = "TextBoxCsvLocation";
            this.TextBoxCsvLocation.ReadOnly = true;
            this.TextBoxCsvLocation.Size = new System.Drawing.Size(631, 20);
            this.TextBoxCsvLocation.TabIndex = 0;
            // 
            // GroupStep3
            // 
            this.GroupStep3.Controls.Add(this.ButtonRun);
            this.GroupStep3.Enabled = false;
            this.GroupStep3.Location = new System.Drawing.Point(940, 23);
            this.GroupStep3.Name = "GroupStep3";
            this.GroupStep3.Size = new System.Drawing.Size(132, 88);
            this.GroupStep3.TabIndex = 2;
            this.GroupStep3.TabStop = false;
            this.GroupStep3.Text = "Step 3: (Run)";
            // 
            // ButtonRun
            // 
            this.ButtonRun.Location = new System.Drawing.Point(26, 38);
            this.ButtonRun.Name = "ButtonRun";
            this.ButtonRun.Size = new System.Drawing.Size(75, 23);
            this.ButtonRun.TabIndex = 0;
            this.ButtonRun.Text = "Run";
            this.ButtonRun.UseVisualStyleBackColor = true;
            this.ButtonRun.Click += new System.EventHandler(this.ButtonRun_Click);
            // 
            // ListViewMainOutput
            // 
            this.ListViewMainOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewMainOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDateTime,
            this.chGUID,
            this.chFileName,
            this.chFileSize,
            this.chDownloadLocation,
            this.chRegardingEntity,
            this.chRegardID,
            this.chErrorMessage});
            this.ListViewMainOutput.HideSelection = false;
            this.ListViewMainOutput.Location = new System.Drawing.Point(27, 160);
            this.ListViewMainOutput.Name = "ListViewMainOutput";
            this.ListViewMainOutput.Size = new System.Drawing.Size(1253, 620);
            this.ListViewMainOutput.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListViewMainOutput.TabIndex = 4;
            this.ListViewMainOutput.UseCompatibleStateImageBehavior = false;
            this.ListViewMainOutput.View = System.Windows.Forms.View.Details;
            // 
            // chDateTime
            // 
            this.chDateTime.Text = "Date Time Processed";
            this.chDateTime.Width = 150;
            // 
            // chGUID
            // 
            this.chGUID.Text = "Record GUID";
            this.chGUID.Width = 150;
            // 
            // chFileName
            // 
            this.chFileName.Text = "File Name";
            this.chFileName.Width = 150;
            // 
            // chFileSize
            // 
            this.chFileSize.Text = "File Size (Bytes)";
            this.chFileSize.Width = 150;
            // 
            // chDownloadLocation
            // 
            this.chDownloadLocation.Text = "Download Location";
            this.chDownloadLocation.Width = 150;
            // 
            // chRegardingEntity
            // 
            this.chRegardingEntity.Text = "Regrading Entity";
            this.chRegardingEntity.Width = 150;
            // 
            // chRegardID
            // 
            this.chRegardID.Text = "Regarding ID";
            this.chRegardID.Width = 150;
            // 
            // chErrorMessage
            // 
            this.chErrorMessage.Text = "Error Message";
            this.chErrorMessage.Width = 150;
            // 
            // LabelMainOutput
            // 
            this.LabelMainOutput.AutoSize = true;
            this.LabelMainOutput.Location = new System.Drawing.Point(29, 144);
            this.LabelMainOutput.Name = "LabelMainOutput";
            this.LabelMainOutput.Size = new System.Drawing.Size(65, 13);
            this.LabelMainOutput.TabIndex = 5;
            this.LabelMainOutput.Text = "Main Output";
            // 
            // SaveFileDialogCsvFile
            // 
            this.SaveFileDialogCsvFile.DefaultExt = "csv";
            this.SaveFileDialogCsvFile.Filter = "CSV Files | *.csv";
            this.SaveFileDialogCsvFile.Title = "Export Results To CSV File";
            // 
            // GroupStep1
            // 
            this.GroupStep1.Controls.Add(this.RadioFiles);
            this.GroupStep1.Controls.Add(this.RadioAll);
            this.GroupStep1.Controls.Add(this.RadioNotes);
            this.GroupStep1.Controls.Add(this.RadioEmail);
            this.GroupStep1.Location = new System.Drawing.Point(27, 23);
            this.GroupStep1.Name = "GroupStep1";
            this.GroupStep1.Size = new System.Drawing.Size(162, 118);
            this.GroupStep1.TabIndex = 8;
            this.GroupStep1.TabStop = false;
            this.GroupStep1.Text = "Step 1: (Choose One)";
            // 
            // RadioFiles
            // 
            this.RadioFiles.AutoSize = true;
            this.RadioFiles.Location = new System.Drawing.Point(5, 67);
            this.RadioFiles.Name = "RadioFiles";
            this.RadioFiles.Size = new System.Drawing.Size(46, 17);
            this.RadioFiles.TabIndex = 3;
            this.RadioFiles.TabStop = true;
            this.RadioFiles.Text = "Files";
            this.RadioFiles.UseVisualStyleBackColor = true;
            this.RadioFiles.CheckedChanged += new System.EventHandler(this.Step1RadioButtons);
            // 
            // RadioAll
            // 
            this.RadioAll.AutoSize = true;
            this.RadioAll.Location = new System.Drawing.Point(6, 90);
            this.RadioAll.Name = "RadioAll";
            this.RadioAll.Size = new System.Drawing.Size(36, 17);
            this.RadioAll.TabIndex = 2;
            this.RadioAll.TabStop = true;
            this.RadioAll.Text = "All";
            this.RadioAll.UseVisualStyleBackColor = true;
            this.RadioAll.CheckedChanged += new System.EventHandler(this.Step1RadioButtons);
            // 
            // RadioNotes
            // 
            this.RadioNotes.AutoSize = true;
            this.RadioNotes.Location = new System.Drawing.Point(6, 21);
            this.RadioNotes.Name = "RadioNotes";
            this.RadioNotes.Size = new System.Drawing.Size(53, 17);
            this.RadioNotes.TabIndex = 1;
            this.RadioNotes.TabStop = true;
            this.RadioNotes.Text = "Notes";
            this.RadioNotes.UseVisualStyleBackColor = true;
            this.RadioNotes.CheckedChanged += new System.EventHandler(this.Step1RadioButtons);
            // 
            // RadioEmail
            // 
            this.RadioEmail.AutoSize = true;
            this.RadioEmail.Location = new System.Drawing.Point(5, 44);
            this.RadioEmail.Name = "RadioEmail";
            this.RadioEmail.Size = new System.Drawing.Size(54, 17);
            this.RadioEmail.TabIndex = 0;
            this.RadioEmail.TabStop = true;
            this.RadioEmail.Text = "E-Mail";
            this.RadioEmail.UseVisualStyleBackColor = true;
            this.RadioEmail.CheckedChanged += new System.EventHandler(this.Step1RadioButtons);
            // 
            // GroupStep4
            // 
            this.GroupStep4.Controls.Add(this.ButtonExport);
            this.GroupStep4.Location = new System.Drawing.Point(1078, 23);
            this.GroupStep4.Name = "GroupStep4";
            this.GroupStep4.Size = new System.Drawing.Size(200, 88);
            this.GroupStep4.TabIndex = 9;
            this.GroupStep4.TabStop = false;
            this.GroupStep4.Text = "Step 4: (Export Results)";
            // 
            // ButtonExport
            // 
            this.ButtonExport.Location = new System.Drawing.Point(36, 39);
            this.ButtonExport.Name = "ButtonExport";
            this.ButtonExport.Size = new System.Drawing.Size(128, 23);
            this.ButtonExport.TabIndex = 8;
            this.ButtonExport.Text = "Export Results";
            this.ButtonExport.UseVisualStyleBackColor = true;
            this.ButtonExport.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupStep4);
            this.Controls.Add(this.GroupStep1);
            this.Controls.Add(this.LabelMainOutput);
            this.Controls.Add(this.ListViewMainOutput);
            this.Controls.Add(this.GroupStep3);
            this.Controls.Add(this.GroupStep2);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(1293, 826);
            this.GroupStep2.ResumeLayout(false);
            this.GroupStep2.PerformLayout();
            this.GroupStep3.ResumeLayout(false);
            this.GroupStep1.ResumeLayout(false);
            this.GroupStep1.PerformLayout();
            this.GroupStep4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GroupStep2;
        private System.Windows.Forms.Button ButtonCsvBrowse;
        private System.Windows.Forms.TextBox TextBoxCsvLocation;
        private System.Windows.Forms.GroupBox GroupStep3;
        private System.Windows.Forms.Button ButtonRun;
        private System.Windows.Forms.ListView ListViewMainOutput;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ColumnHeader chGUID;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ColumnHeader chDownloadLocation;
        private System.Windows.Forms.ColumnHeader chErrorMessage;
        private System.Windows.Forms.Label LabelMainOutput;
        private System.Windows.Forms.OpenFileDialog OpenFileDianlogCvsFile;
        private System.Windows.Forms.SaveFileDialog SaveFileDialogCsvFile;
        private System.Windows.Forms.RadioButton RadioDownloadSpecific;
        private System.Windows.Forms.RadioButton RadioDownloadAll;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialogMainFile;
        private System.Windows.Forms.Label lbCSVLocation;
        private System.Windows.Forms.ColumnHeader chRegardingEntity;
        private System.Windows.Forms.ColumnHeader chRegardID;
        private System.Windows.Forms.GroupBox GroupStep1;
        private System.Windows.Forms.RadioButton RadioNotes;
        private System.Windows.Forms.RadioButton RadioEmail;
        private System.Windows.Forms.RadioButton RadioAll;
        private System.Windows.Forms.GroupBox GroupStep4;
        private System.Windows.Forms.Button ButtonExport;
        private System.Windows.Forms.RadioButton RadioReportOnly;
        private System.Windows.Forms.ColumnHeader chFileSize;
        private System.Windows.Forms.RadioButton RadioFiles;
    }
}
