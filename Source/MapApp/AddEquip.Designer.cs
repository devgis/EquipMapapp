namespace MapApp
{
    partial class AddEquip
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbEquipName = new System.Windows.Forms.TextBox();
            this.cbEquipState = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btAdd = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备名称：";
            // 
            // tbEquipName
            // 
            this.tbEquipName.Location = new System.Drawing.Point(110, 14);
            this.tbEquipName.MaxLength = 20;
            this.tbEquipName.Name = "tbEquipName";
            this.tbEquipName.Size = new System.Drawing.Size(189, 21);
            this.tbEquipName.TabIndex = 1;
            // 
            // cbEquipState
            // 
            this.cbEquipState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEquipState.FormattingEnabled = true;
            this.cbEquipState.Items.AddRange(new object[] {
            "在线",
            "离开",
            "隐身",
            "忙碌",
            "离线"});
            this.cbEquipState.Location = new System.Drawing.Point(110, 41);
            this.cbEquipState.Name = "cbEquipState";
            this.cbEquipState.Size = new System.Drawing.Size(189, 20);
            this.cbEquipState.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "设备状态：";
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(62, 155);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 4;
            this.btAdd.Text = "添加(&A)";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btExit
            // 
            this.btExit.Location = new System.Drawing.Point(167, 155);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(75, 23);
            this.btExit.TabIndex = 5;
            this.btExit.Text = "取消(&C)";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // tbRemarks
            // 
            this.tbRemarks.Location = new System.Drawing.Point(110, 67);
            this.tbRemarks.MaxLength = 200;
            this.tbRemarks.Multiline = true;
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRemarks.Size = new System.Drawing.Size(189, 78);
            this.tbRemarks.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "备注信息：";
            // 
            // AddEquip
            // 
            this.AcceptButton = this.btAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 190);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbRemarks);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbEquipState);
            this.Controls.Add(this.tbEquipName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEquip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加设备";
            this.Load += new System.EventHandler(this.AddEquip_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbEquipName;
        private System.Windows.Forms.ComboBox cbEquipState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.Label label3;
    }
}