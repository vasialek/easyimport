namespace EasyImport
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatasource = new System.Windows.Forms.TextBox();
            this.btnReadData = new System.Windows.Forms.Button();
            this.lstTables = new System.Windows.Forms.ListView();
            this.headerCheckbox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerTableName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerQnt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.btnAnalizeTables = new System.Windows.Forms.Button();
            this.btnCreateTableSql = new System.Windows.Forms.Button();
            this.btnSqlTableFromCsv = new System.Windows.Forms.Button();
            this.btnWriteData = new System.Windows.Forms.Button();
            this.btnCreateModel = new System.Windows.Forms.Button();
            this.btnCreateMapping = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripClock = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(12, 368);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(616, 189);
            this.txtOutput.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose datasource";
            // 
            // txtDatasource
            // 
            this.txtDatasource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatasource.Location = new System.Drawing.Point(129, 12);
            this.txtDatasource.Name = "txtDatasource";
            this.txtDatasource.Size = new System.Drawing.Size(499, 20);
            this.txtDatasource.TabIndex = 2;
            this.txtDatasource.Click += new System.EventHandler(this.txtDatasource_Click);
            // 
            // btnReadData
            // 
            this.btnReadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadData.Enabled = false;
            this.btnReadData.Location = new System.Drawing.Point(634, 10);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(132, 23);
            this.btnReadData.TabIndex = 3;
            this.btnReadData.Text = "Read data";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // lstTables
            // 
            this.lstTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerCheckbox,
            this.headerTableName,
            this.headerQnt});
            this.lstTables.Location = new System.Drawing.Point(12, 58);
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size(616, 304);
            this.lstTables.TabIndex = 4;
            this.lstTables.UseCompatibleStateImageBehavior = false;
            this.lstTables.View = System.Windows.Forms.View.Details;
            this.lstTables.DoubleClick += new System.EventHandler(this.lstTables_DoubleClick);
            // 
            // headerCheckbox
            // 
            this.headerCheckbox.Text = "Valid";
            this.headerCheckbox.Width = 40;
            // 
            // headerTableName
            // 
            this.headerTableName.Text = "Table";
            this.headerTableName.Width = 500;
            // 
            // headerQnt
            // 
            this.headerQnt.Text = "Records";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database structure";
            // 
            // btnAnalizeTables
            // 
            this.btnAnalizeTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalizeTables.Enabled = false;
            this.btnAnalizeTables.Location = new System.Drawing.Point(634, 58);
            this.btnAnalizeTables.Name = "btnAnalizeTables";
            this.btnAnalizeTables.Size = new System.Drawing.Size(132, 46);
            this.btnAnalizeTables.TabIndex = 3;
            this.btnAnalizeTables.Text = "Analize tables";
            this.btnAnalizeTables.UseVisualStyleBackColor = true;
            this.btnAnalizeTables.Click += new System.EventHandler(this.btnAnalizeTables_Click);
            // 
            // btnCreateTableSql
            // 
            this.btnCreateTableSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateTableSql.Enabled = false;
            this.btnCreateTableSql.Location = new System.Drawing.Point(786, 58);
            this.btnCreateTableSql.Name = "btnCreateTableSql";
            this.btnCreateTableSql.Size = new System.Drawing.Size(132, 46);
            this.btnCreateTableSql.TabIndex = 3;
            this.btnCreateTableSql.Text = "SQL - create tables";
            this.btnCreateTableSql.UseVisualStyleBackColor = true;
            this.btnCreateTableSql.Click += new System.EventHandler(this.btnCreateTableSql_Click);
            // 
            // btnSqlTableFromCsv
            // 
            this.btnSqlTableFromCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSqlTableFromCsv.Location = new System.Drawing.Point(786, 110);
            this.btnSqlTableFromCsv.Name = "btnSqlTableFromCsv";
            this.btnSqlTableFromCsv.Size = new System.Drawing.Size(132, 46);
            this.btnSqlTableFromCsv.TabIndex = 3;
            this.btnSqlTableFromCsv.Text = "SQL from FSCC text...";
            this.btnSqlTableFromCsv.UseVisualStyleBackColor = true;
            this.btnSqlTableFromCsv.Click += new System.EventHandler(this.btnSqlTableFromCsv_Click);
            // 
            // btnWriteData
            // 
            this.btnWriteData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteData.Location = new System.Drawing.Point(786, 10);
            this.btnWriteData.Name = "btnWriteData";
            this.btnWriteData.Size = new System.Drawing.Size(132, 23);
            this.btnWriteData.TabIndex = 3;
            this.btnWriteData.Text = "Write data...";
            this.btnWriteData.UseVisualStyleBackColor = true;
            this.btnWriteData.Click += new System.EventHandler(this.btnWriteData_Click);
            // 
            // btnCreateModel
            // 
            this.btnCreateModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateModel.Enabled = false;
            this.btnCreateModel.Location = new System.Drawing.Point(634, 536);
            this.btnCreateModel.Name = "btnCreateModel";
            this.btnCreateModel.Size = new System.Drawing.Size(159, 21);
            this.btnCreateModel.TabIndex = 3;
            this.btnCreateModel.Text = "C# Model from SQL...";
            this.btnCreateModel.UseVisualStyleBackColor = true;
            this.btnCreateModel.Click += new System.EventHandler(this.btnCreateModel_Click);
            // 
            // btnCreateMapping
            // 
            this.btnCreateMapping.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCreateMapping.Enabled = false;
            this.btnCreateMapping.Location = new System.Drawing.Point(634, 509);
            this.btnCreateMapping.Name = "btnCreateMapping";
            this.btnCreateMapping.Size = new System.Drawing.Size(159, 21);
            this.btnCreateMapping.TabIndex = 3;
            this.btnCreateMapping.Text = "Default mapping from SQL...";
            this.btnCreateMapping.UseVisualStyleBackColor = true;
            this.btnCreateMapping.Click += new System.EventHandler(this.btnCreateMapping_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(852, 509);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(66, 48);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripClock,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 560);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(930, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripClock
            // 
            this.toolStripClock.Name = "toolStripClock";
            this.toolStripClock.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 582);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCreateMapping);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnCreateModel);
            this.Controls.Add(this.btnSqlTableFromCsv);
            this.Controls.Add(this.btnCreateTableSql);
            this.Controls.Add(this.btnAnalizeTables);
            this.Controls.Add(this.lstTables);
            this.Controls.Add(this.btnWriteData);
            this.Controls.Add(this.btnReadData);
            this.Controls.Add(this.txtDatasource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOutput);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatasource;
        private System.Windows.Forms.Button btnReadData;
        private System.Windows.Forms.ListView lstTables;
        private System.Windows.Forms.ColumnHeader headerCheckbox;
        private System.Windows.Forms.ColumnHeader headerTableName;
        private System.Windows.Forms.ColumnHeader headerQnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAnalizeTables;
        private System.Windows.Forms.Button btnCreateTableSql;
        private System.Windows.Forms.Button btnSqlTableFromCsv;
        private System.Windows.Forms.Button btnWriteData;
        private System.Windows.Forms.Button btnCreateModel;
        private System.Windows.Forms.Button btnCreateMapping;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripClock;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

