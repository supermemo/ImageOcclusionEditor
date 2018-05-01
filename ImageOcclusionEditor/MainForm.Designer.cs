namespace ImageOcclusionEditor
{
  partial class MainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.wb = new System.Windows.Forms.WebBrowser();
      this.btnSaveExit = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // wb
      // 
      this.wb.AllowNavigation = false;
      this.wb.AllowWebBrowserDrop = false;
      this.wb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.wb.Location = new System.Drawing.Point(1, 1);
      this.wb.MinimumSize = new System.Drawing.Size(20, 20);
      this.wb.Name = "wb";
      this.wb.ScriptErrorsSuppressed = true;
      this.wb.ScrollBarsEnabled = false;
      this.wb.Size = new System.Drawing.Size(1062, 636);
      this.wb.TabIndex = 0;
      // 
      // btnSaveExit
      // 
      this.btnSaveExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveExit.Location = new System.Drawing.Point(939, 643);
      this.btnSaveExit.Name = "btnSaveExit";
      this.btnSaveExit.Size = new System.Drawing.Size(120, 33);
      this.btnSaveExit.TabIndex = 1;
      this.btnSaveExit.Text = "Save && E&xit  (Ctrl+S)";
      this.btnSaveExit.UseVisualStyleBackColor = true;
      this.btnSaveExit.Click += new System.EventHandler(this.btnSaveExit_Click);
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(813, 643);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(120, 33);
      this.btnSave.TabIndex = 2;
      this.btnSave.Text = "&Save  (Ctrl+Shift+S)";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(687, 643);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(120, 33);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.TabStop = false;
      this.btnCancel.Text = "&Cancel (ESC)";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1064, 681);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.btnSaveExit);
      this.Controls.Add(this.wb);
      this.DoubleBuffered = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MainForm";
      this.Text = "SuperMemo Image Occlusion Editor";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.WebBrowser wb;
    private System.Windows.Forms.Button btnSaveExit;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
  }
}

