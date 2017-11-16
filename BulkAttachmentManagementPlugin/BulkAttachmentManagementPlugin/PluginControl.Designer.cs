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
            this.gbStep2 = new System.Windows.Forms.GroupBox();
            this.butCSVBrowse = new System.Windows.Forms.Button();
            this.tbCSVLocation = new System.Windows.Forms.TextBox();
            this.gbStep3 = new System.Windows.Forms.GroupBox();
            this.butRun = new System.Windows.Forms.Button();
            this.pbMain = new System.Windows.Forms.ProgressBar();
            this.lvMainOutput = new System.Windows.Forms.ListView();
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDownloadLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chErrorMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbMainOutput = new System.Windows.Forms.Label();
            this.lbProgress = new System.Windows.Forms.Label();
            this.butExport = new System.Windows.Forms.Button();
            this.ofdCVSFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdCSVFile = new System.Windows.Forms.SaveFileDialog();
            this.gbStep1.SuspendLayout();
            this.gbStep2.SuspendLayout();
            this.gbStep3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbStep1
            // 
            this.gbStep1.Controls.Add(this.rbUpload);
            this.gbStep1.Controls.Add(this.rbDownload);
            this.gbStep1.Location = new System.Drawing.Point(27, 23);
            this.gbStep1.Name = "gbStep1";
            this.gbStep1.Size = new System.Drawing.Size(130, 73);
            this.gbStep1.TabIndex = 0;
            this.gbStep1.TabStop = false;
            this.gbStep1.Text = "Step 1: (Choose One)";
            // 
            // rbUpload
            // 
            this.rbUpload.AutoSize = true;
            this.rbUpload.Location = new System.Drawing.Point(7, 44);
            this.rbUpload.Name = "rbUpload";
            this.rbUpload.Size = new System.Drawing.Size(59, 17);
            this.rbUpload.TabIndex = 1;
            this.rbUpload.TabStop = true;
            this.rbUpload.Text = "Upload";
            this.rbUpload.UseVisualStyleBackColor = true;
            // 
            // rbDownload
            // 
            this.rbDownload.AutoSize = true;
            this.rbDownload.Location = new System.Drawing.Point(7, 20);
            this.rbDownload.Name = "rbDownload";
            this.rbDownload.Size = new System.Drawing.Size(73, 17);
            this.rbDownload.TabIndex = 0;
            this.rbDownload.TabStop = true;
            this.rbDownload.Text = "Download";
            this.rbDownload.UseVisualStyleBackColor = true;
            // 
            // gbStep2
            // 
            this.gbStep2.Controls.Add(this.butCSVBrowse);
            this.gbStep2.Controls.Add(this.tbCSVLocation);
            this.gbStep2.Location = new System.Drawing.Point(174, 23);
            this.gbStep2.Name = "gbStep2";
            this.gbStep2.Size = new System.Drawing.Size(462, 73);
            this.gbStep2.TabIndex = 1;
            this.gbStep2.TabStop = false;
            this.gbStep2.Text = "Step 2: (Location of CSV)";
            // 
            // butCSVBrowse
            // 
            this.butCSVBrowse.Location = new System.Drawing.Point(374, 29);
            this.butCSVBrowse.Name = "butCSVBrowse";
            this.butCSVBrowse.Size = new System.Drawing.Size(75, 23);
            this.butCSVBrowse.TabIndex = 1;
            this.butCSVBrowse.Text = "Browse";
            this.butCSVBrowse.UseVisualStyleBackColor = true;
            this.butCSVBrowse.Click += new System.EventHandler(this.butCSVBrowse_Click);
            // 
            // tbCSVLocation
            // 
            this.tbCSVLocation.Location = new System.Drawing.Point(6, 31);
            this.tbCSVLocation.Name = "tbCSVLocation";
            this.tbCSVLocation.Size = new System.Drawing.Size(362, 20);
            this.tbCSVLocation.TabIndex = 0;
            // 
            // gbStep3
            // 
            this.gbStep3.Controls.Add(this.butRun);
            this.gbStep3.Location = new System.Drawing.Point(656, 23);
            this.gbStep3.Name = "gbStep3";
            this.gbStep3.Size = new System.Drawing.Size(132, 73);
            this.gbStep3.TabIndex = 2;
            this.gbStep3.TabStop = false;
            this.gbStep3.Text = "Step 3: (Run)";
            // 
            // butRun
            // 
            this.butRun.Location = new System.Drawing.Point(26, 29);
            this.butRun.Name = "butRun";
            this.butRun.Size = new System.Drawing.Size(75, 23);
            this.butRun.TabIndex = 0;
            this.butRun.Text = "Run";
            this.butRun.UseVisualStyleBackColor = true;
            this.butRun.Click += new System.EventHandler(this.butRun_Click);
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(27, 133);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(761, 23);
            this.pbMain.TabIndex = 3;
            // 
            // lvMainOutput
            // 
            this.lvMainOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDateTime,
            this.chGUID,
            this.chFileName,
            this.chDownloadLocation,
            this.chErrorMessage});
            this.lvMainOutput.Location = new System.Drawing.Point(27, 185);
            this.lvMainOutput.Name = "lvMainOutput";
            this.lvMainOutput.Size = new System.Drawing.Size(761, 603);
            this.lvMainOutput.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvMainOutput.TabIndex = 4;
            this.lvMainOutput.UseCompatibleStateImageBehavior = false;
            this.lvMainOutput.View = System.Windows.Forms.View.Details;
            // 
            // chDateTime
            // 
            this.chDateTime.Text = "Date Time Processed";
            this.chDateTime.Width = 152;
            // 
            // chGUID
            // 
            this.chGUID.Text = "GUID";
            this.chGUID.Width = 152;
            // 
            // chFileName
            // 
            this.chFileName.Text = "File Name";
            this.chFileName.Width = 152;
            // 
            // chDownloadLocation
            // 
            this.chDownloadLocation.Text = "Download Location";
            this.chDownloadLocation.Width = 152;
            // 
            // chErrorMessage
            // 
            this.chErrorMessage.Text = "Error Message";
            this.chErrorMessage.Width = 152;
            // 
            // lbMainOutput
            // 
            this.lbMainOutput.AutoSize = true;
            this.lbMainOutput.Location = new System.Drawing.Point(28, 169);
            this.lbMainOutput.Name = "lbMainOutput";
            this.lbMainOutput.Size = new System.Drawing.Size(65, 13);
            this.lbMainOutput.TabIndex = 5;
            this.lbMainOutput.Text = "Main Output";
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(27, 114);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(48, 13);
            this.lbProgress.TabIndex = 6;
            this.lbProgress.Text = "Progress";
            // 
            // butExport
            // 
            this.butExport.Location = new System.Drawing.Point(328, 805);
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
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.butExport);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.lbMainOutput);
            this.Controls.Add(this.lvMainOutput);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.gbStep3);
            this.Controls.Add(this.gbStep2);
            this.Controls.Add(this.gbStep1);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(805, 853);
            this.gbStep1.ResumeLayout(false);
            this.gbStep1.PerformLayout();
            this.gbStep2.ResumeLayout(false);
            this.gbStep2.PerformLayout();
            this.gbStep3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbStep1;
        private System.Windows.Forms.RadioButton rbUpload;
        private System.Windows.Forms.RadioButton rbDownload;
        private System.Windows.Forms.GroupBox gbStep2;
        private System.Windows.Forms.Button butCSVBrowse;
        private System.Windows.Forms.TextBox tbCSVLocation;
        private System.Windows.Forms.GroupBox gbStep3;
        private System.Windows.Forms.Button butRun;
        private System.Windows.Forms.ProgressBar pbMain;
        private System.Windows.Forms.ListView lvMainOutput;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ColumnHeader chGUID;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ColumnHeader chDownloadLocation;
        private System.Windows.Forms.ColumnHeader chErrorMessage;
        private System.Windows.Forms.Label lbMainOutput;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.Button butExport;
        private System.Windows.Forms.OpenFileDialog ofdCVSFile;
        private System.Windows.Forms.SaveFileDialog sfdCSVFile;
    }
}
