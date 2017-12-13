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
            this.gbStep1 = new System.Windows.Forms.GroupBox();
            this.rbUpload = new System.Windows.Forms.RadioButton();
            this.rbDownload = new System.Windows.Forms.RadioButton();
            this.gbStep3 = new System.Windows.Forms.GroupBox();
            this.lbCSVLocation = new System.Windows.Forms.Label();
            this.rbSpecificAttachments = new System.Windows.Forms.RadioButton();
            this.rbAllAttachments = new System.Windows.Forms.RadioButton();
            this.butCSVBrowse = new System.Windows.Forms.Button();
            this.tbCSVLocation = new System.Windows.Forms.TextBox();
            this.gbStep4 = new System.Windows.Forms.GroupBox();
            this.butRun = new System.Windows.Forms.Button();
            this.lvMainOutput = new System.Windows.Forms.ListView();
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDownloadLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRegardingEntity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRegardID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chErrorMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbMainOutput = new System.Windows.Forms.Label();
            this.butExport = new System.Windows.Forms.Button();
            this.ofdCVSFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdCSVFile = new System.Windows.Forms.SaveFileDialog();
            this.fbdMainFile = new System.Windows.Forms.FolderBrowserDialog();
            this.gbStep2 = new System.Windows.Forms.GroupBox();
            this.rbEMail = new System.Windows.Forms.RadioButton();
            this.Notes = new System.Windows.Forms.RadioButton();
            this.rbBoth = new System.Windows.Forms.RadioButton();
            this.gbStep1.SuspendLayout();
            this.gbStep3.SuspendLayout();
            this.gbStep4.SuspendLayout();
            this.gbStep2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbStep1
            // 
            this.gbStep1.Controls.Add(this.rbUpload);
            this.gbStep1.Controls.Add(this.rbDownload);
            this.gbStep1.Location = new System.Drawing.Point(27, 23);
            this.gbStep1.Name = "gbStep1";
            this.gbStep1.Size = new System.Drawing.Size(130, 88);
            this.gbStep1.TabIndex = 0;
            this.gbStep1.TabStop = false;
            this.gbStep1.Text = "Step 1: (Choose One)";
            // 
            // rbUpload
            // 
            this.rbUpload.AutoSize = true;
            this.rbUpload.Enabled = false;
            this.rbUpload.Location = new System.Drawing.Point(7, 44);
            this.rbUpload.Name = "rbUpload";
            this.rbUpload.Size = new System.Drawing.Size(59, 17);
            this.rbUpload.TabIndex = 1;
            this.rbUpload.Text = "Upload";
            this.rbUpload.UseVisualStyleBackColor = true;
            this.rbUpload.CheckedChanged += new System.EventHandler(this.rbUpload_CheckedChanged);
            // 
            // rbDownload
            // 
            this.rbDownload.AutoSize = true;
            this.rbDownload.Location = new System.Drawing.Point(7, 20);
            this.rbDownload.Name = "rbDownload";
            this.rbDownload.Size = new System.Drawing.Size(73, 17);
            this.rbDownload.TabIndex = 0;
            this.rbDownload.Text = "Download";
            this.rbDownload.UseVisualStyleBackColor = true;
            this.rbDownload.CheckedChanged += new System.EventHandler(this.rbDownload_CheckedChanged);
            // 
            // gbStep3
            // 
            this.gbStep3.Controls.Add(this.lbCSVLocation);
            this.gbStep3.Controls.Add(this.rbSpecificAttachments);
            this.gbStep3.Controls.Add(this.rbAllAttachments);
            this.gbStep3.Controls.Add(this.butCSVBrowse);
            this.gbStep3.Controls.Add(this.tbCSVLocation);
            this.gbStep3.Enabled = false;
            this.gbStep3.Location = new System.Drawing.Point(355, 23);
            this.gbStep3.Name = "gbStep3";
            this.gbStep3.Size = new System.Drawing.Size(562, 88);
            this.gbStep3.TabIndex = 1;
            this.gbStep3.TabStop = false;
            this.gbStep3.Text = "Step 3:";
            // 
            // lbCSVLocation
            // 
            this.lbCSVLocation.AutoSize = true;
            this.lbCSVLocation.Location = new System.Drawing.Point(7, 44);
            this.lbCSVLocation.Name = "lbCSVLocation";
            this.lbCSVLocation.Size = new System.Drawing.Size(0, 13);
            this.lbCSVLocation.TabIndex = 4;
            // 
            // rbSpecificAttachments
            // 
            this.rbSpecificAttachments.AutoSize = true;
            this.rbSpecificAttachments.Location = new System.Drawing.Point(119, 20);
            this.rbSpecificAttachments.Name = "rbSpecificAttachments";
            this.rbSpecificAttachments.Size = new System.Drawing.Size(125, 17);
            this.rbSpecificAttachments.TabIndex = 3;
            this.rbSpecificAttachments.Text = "Specific Attachments";
            this.rbSpecificAttachments.UseVisualStyleBackColor = true;
            this.rbSpecificAttachments.CheckedChanged += new System.EventHandler(this.rbSpecificAttachments_CheckedChanged);
            // 
            // rbAllAttachments
            // 
            this.rbAllAttachments.AutoSize = true;
            this.rbAllAttachments.Location = new System.Drawing.Point(7, 20);
            this.rbAllAttachments.Name = "rbAllAttachments";
            this.rbAllAttachments.Size = new System.Drawing.Size(98, 17);
            this.rbAllAttachments.TabIndex = 2;
            this.rbAllAttachments.Text = "All Attachments";
            this.rbAllAttachments.UseVisualStyleBackColor = true;
            this.rbAllAttachments.CheckedChanged += new System.EventHandler(this.rbAllAttachments_CheckedChanged);
            // 
            // butCSVBrowse
            // 
            this.butCSVBrowse.Enabled = false;
            this.butCSVBrowse.Location = new System.Drawing.Point(481, 62);
            this.butCSVBrowse.Name = "butCSVBrowse";
            this.butCSVBrowse.Size = new System.Drawing.Size(75, 20);
            this.butCSVBrowse.TabIndex = 1;
            this.butCSVBrowse.Text = "Browse";
            this.butCSVBrowse.UseVisualStyleBackColor = true;
            this.butCSVBrowse.Click += new System.EventHandler(this.butCSVBrowse_Click);
            // 
            // tbCSVLocation
            // 
            this.tbCSVLocation.Enabled = false;
            this.tbCSVLocation.Location = new System.Drawing.Point(7, 62);
            this.tbCSVLocation.Name = "tbCSVLocation";
            this.tbCSVLocation.ReadOnly = true;
            this.tbCSVLocation.Size = new System.Drawing.Size(468, 20);
            this.tbCSVLocation.TabIndex = 0;
            // 
            // gbStep4
            // 
            this.gbStep4.Controls.Add(this.butRun);
            this.gbStep4.Enabled = false;
            this.gbStep4.Location = new System.Drawing.Point(940, 23);
            this.gbStep4.Name = "gbStep4";
            this.gbStep4.Size = new System.Drawing.Size(132, 88);
            this.gbStep4.TabIndex = 2;
            this.gbStep4.TabStop = false;
            this.gbStep4.Text = "Step 4: (Run)";
            // 
            // butRun
            // 
            this.butRun.Location = new System.Drawing.Point(26, 38);
            this.butRun.Name = "butRun";
            this.butRun.Size = new System.Drawing.Size(75, 23);
            this.butRun.TabIndex = 0;
            this.butRun.Text = "Run";
            this.butRun.UseVisualStyleBackColor = true;
            this.butRun.Click += new System.EventHandler(this.butRun_Click);
            // 
            // lvMainOutput
            // 
            this.lvMainOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDateTime,
            this.chGUID,
            this.chFileName,
            this.chDownloadLocation,
            this.chRegardingEntity,
            this.chRegardID,
            this.chErrorMessage});
            this.lvMainOutput.Location = new System.Drawing.Point(27, 139);
            this.lvMainOutput.Name = "lvMainOutput";
            this.lvMainOutput.Size = new System.Drawing.Size(1059, 641);
            this.lvMainOutput.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvMainOutput.TabIndex = 4;
            this.lvMainOutput.UseCompatibleStateImageBehavior = false;
            this.lvMainOutput.View = System.Windows.Forms.View.Details;
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
            // lbMainOutput
            // 
            this.lbMainOutput.AutoSize = true;
            this.lbMainOutput.Location = new System.Drawing.Point(28, 123);
            this.lbMainOutput.Name = "lbMainOutput";
            this.lbMainOutput.Size = new System.Drawing.Size(65, 13);
            this.lbMainOutput.TabIndex = 5;
            this.lbMainOutput.Text = "Main Output";
            // 
            // butExport
            // 
            this.butExport.Location = new System.Drawing.Point(378, 786);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(128, 23);
            this.butExport.TabIndex = 7;
            this.butExport.Text = "Export Results";
            this.butExport.UseVisualStyleBackColor = true;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // sfdCSVFile
            // 
            this.sfdCSVFile.DefaultExt = "csv";
            this.sfdCSVFile.Filter = "CSV Files | *.csv";
            this.sfdCSVFile.Title = "Export Results To CSV File";
            // 
            // gbStep2
            // 
            this.gbStep2.Controls.Add(this.rbBoth);
            this.gbStep2.Controls.Add(this.Notes);
            this.gbStep2.Controls.Add(this.rbEMail);
            this.gbStep2.Enabled = false;
            this.gbStep2.Location = new System.Drawing.Point(174, 23);
            this.gbStep2.Name = "gbStep2";
            this.gbStep2.Size = new System.Drawing.Size(162, 88);
            this.gbStep2.TabIndex = 8;
            this.gbStep2.TabStop = false;
            this.gbStep2.Text = "Step 2: (Choose One)";
            // 
            // rbEMail
            // 
            this.rbEMail.AutoSize = true;
            this.rbEMail.Enabled = false;
            this.rbEMail.Location = new System.Drawing.Point(5, 44);
            this.rbEMail.Name = "rbEMail";
            this.rbEMail.Size = new System.Drawing.Size(54, 17);
            this.rbEMail.TabIndex = 0;
            this.rbEMail.TabStop = true;
            this.rbEMail.Text = "E-Mail";
            this.rbEMail.UseVisualStyleBackColor = true;
            // 
            // Notes
            // 
            this.Notes.AutoSize = true;
            this.Notes.Location = new System.Drawing.Point(6, 21);
            this.Notes.Name = "Notes";
            this.Notes.Size = new System.Drawing.Size(53, 17);
            this.Notes.TabIndex = 1;
            this.Notes.TabStop = true;
            this.Notes.Text = "Notes";
            this.Notes.UseVisualStyleBackColor = true;
            // 
            // rbBoth
            // 
            this.rbBoth.AutoSize = true;
            this.rbBoth.Location = new System.Drawing.Point(6, 67);
            this.rbBoth.Name = "rbBoth";
            this.rbBoth.Size = new System.Drawing.Size(47, 17);
            this.rbBoth.TabIndex = 2;
            this.rbBoth.TabStop = true;
            this.rbBoth.Text = "Both";
            this.rbBoth.UseVisualStyleBackColor = true;
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbStep2);
            this.Controls.Add(this.butExport);
            this.Controls.Add(this.lbMainOutput);
            this.Controls.Add(this.lvMainOutput);
            this.Controls.Add(this.gbStep4);
            this.Controls.Add(this.gbStep3);
            this.Controls.Add(this.gbStep1);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(1099, 826);
            this.gbStep1.ResumeLayout(false);
            this.gbStep1.PerformLayout();
            this.gbStep3.ResumeLayout(false);
            this.gbStep3.PerformLayout();
            this.gbStep4.ResumeLayout(false);
            this.gbStep2.ResumeLayout(false);
            this.gbStep2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbStep1;
        private System.Windows.Forms.RadioButton rbUpload;
        private System.Windows.Forms.RadioButton rbDownload;
        private System.Windows.Forms.GroupBox gbStep3;
        private System.Windows.Forms.Button butCSVBrowse;
        private System.Windows.Forms.TextBox tbCSVLocation;
        private System.Windows.Forms.GroupBox gbStep4;
        private System.Windows.Forms.Button butRun;
        private System.Windows.Forms.ListView lvMainOutput;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ColumnHeader chGUID;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ColumnHeader chDownloadLocation;
        private System.Windows.Forms.ColumnHeader chErrorMessage;
        private System.Windows.Forms.Label lbMainOutput;
        private System.Windows.Forms.Button butExport;
        private System.Windows.Forms.OpenFileDialog ofdCVSFile;
        private System.Windows.Forms.SaveFileDialog sfdCSVFile;
        private System.Windows.Forms.RadioButton rbSpecificAttachments;
        private System.Windows.Forms.RadioButton rbAllAttachments;
        private System.Windows.Forms.FolderBrowserDialog fbdMainFile;
        private System.Windows.Forms.Label lbCSVLocation;
        private System.Windows.Forms.ColumnHeader chRegardingEntity;
        private System.Windows.Forms.ColumnHeader chRegardID;
        private System.Windows.Forms.GroupBox gbStep2;
        private System.Windows.Forms.RadioButton Notes;
        private System.Windows.Forms.RadioButton rbEMail;
        private System.Windows.Forms.RadioButton rbBoth;
    }
}
