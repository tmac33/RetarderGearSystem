// Assembly: Gear, Version=1.0.5619.28247, Culture=neutral, PublicKeyToken=null
// MVID: 37313088-B81D-4F89-988D-7DF444CCA003
// Assembly location: F:\Tmac\Gear.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WuXiaoYangGraduationDesign
{
  public class FormGear : Form
  {
    private IContainer components = (IContainer) null;
    public double yaLiJiao;
    public double thickness;
    public double moShu;
    public int chiShu;
    private Button buttonOk;
    private ComboBox comboBoxMoShu;
    private ComboBox comboBoxYaliJiao;
    private ComboBox comboBoxChiShu;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label lb_thinkness;
    private ComboBox comboBox1thickness;
    private Button button1;

    public FormGear()
    {
      this.InitializeComponent();
      this.MyInitialize();
    }

    private void MyInitialize()
    {
      this.comboBox1thickness.SelectedIndex = 0;
      this.comboBoxChiShu.SelectedIndex = 0;
      this.comboBoxMoShu.SelectedIndex = 0;
      this.comboBoxYaliJiao.SelectedIndex = 0;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.chiShu = (int) Convert.ToInt16(this.comboBoxChiShu.Text);
        this.moShu = Convert.ToDouble(this.comboBoxMoShu.Text);
        this.yaLiJiao = Convert.ToDouble(this.comboBoxYaliJiao.Text);
        this.thickness = Convert.ToDouble(this.comboBox1thickness.Text);
      }
      catch
      {
        this.chiShu = 24;
        this.moShu = 8.0;
        this.yaLiJiao = 20.0;
        this.thickness = 50.0;
      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void FormGear_Load(object sender, EventArgs e)
    {
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.buttonOk = new Button();
      this.comboBoxMoShu = new ComboBox();
      this.comboBoxYaliJiao = new ComboBox();
      this.comboBoxChiShu = new ComboBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.lb_thinkness = new Label();
      this.comboBox1thickness = new ComboBox();
      this.button1 = new Button();
      this.SuspendLayout();
      this.buttonOk.Location = new Point(78, 246);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new Size(89, 26);
      this.buttonOk.TabIndex = 0;
      this.buttonOk.Text = "确定";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonOk.Click += new EventHandler(this.button1_Click);
      this.comboBoxMoShu.FormattingEnabled = true;
      this.comboBoxMoShu.Items.AddRange(new object[1]
      {
        (object) "8"
      });
      this.comboBoxMoShu.Location = new Point(133, 54);
      this.comboBoxMoShu.Name = "comboBoxMoShu";
      this.comboBoxMoShu.Size = new Size(121, 20);
      this.comboBoxMoShu.TabIndex = 4;
      this.comboBoxYaliJiao.FormattingEnabled = true;
      this.comboBoxYaliJiao.Items.AddRange(new object[1]
      {
        (object) "20"
      });
      this.comboBoxYaliJiao.Location = new Point(133, 97);
      this.comboBoxYaliJiao.Name = "comboBoxYaliJiao";
      this.comboBoxYaliJiao.Size = new Size(121, 20);
      this.comboBoxYaliJiao.TabIndex = 5;
      this.comboBoxChiShu.FormattingEnabled = true;
      this.comboBoxChiShu.Items.AddRange(new object[4]
      {
        (object) "24",
        (object) "42",
        (object) "72",
        (object) "84"
      });
      this.comboBoxChiShu.Location = new Point(133, 149);
      this.comboBoxChiShu.Name = "comboBoxChiShu";
      this.comboBoxChiShu.Size = new Size(121, 20);
      this.comboBoxChiShu.TabIndex = 6;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(64, 62);
      this.label1.Name = "label1";
      this.label1.Size = new Size(47, 12);
      this.label1.TabIndex = 7;
      this.label1.Text = "模  数m";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(64, 105);
      this.label2.Name = "label2";
      this.label2.Size = new Size(47, 12);
      this.label2.TabIndex = 8;
      this.label2.Text = "压力角a";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(64, 152);
      this.label3.Name = "label3";
      this.label3.Size = new Size(47, 12);
      this.label3.TabIndex = 9;
      this.label3.Text = "齿  数z";
      this.lb_thinkness.AutoSize = true;
      this.lb_thinkness.Location = new Point(64, 194);
      this.lb_thinkness.Name = "lb_thinkness";
      this.lb_thinkness.Size = new Size(41, 12);
      this.lb_thinkness.TabIndex = 10;
      this.lb_thinkness.Text = "齿轮厚";
      this.comboBox1thickness.FormattingEnabled = true;
      this.comboBox1thickness.Items.AddRange(new object[4]
      {
        (object) "24",
        (object) "42",
        (object) "72",
        (object) "84"
      });
      this.comboBox1thickness.Location = new Point(133, 191);
      this.comboBox1thickness.Name = "comboBox1thickness";
      this.comboBox1thickness.Size = new Size(121, 20);
      this.comboBox1thickness.TabIndex = 11;
      this.button1.Location = new Point(183, 246);
      this.button1.Name = "button1";
      this.button1.Size = new Size(89, 26);
      this.button1.TabIndex = 12;
      this.button1.Text = "取消\r\n";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click_1);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(350, 316);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.comboBox1thickness);
      this.Controls.Add((Control) this.lb_thinkness);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.comboBoxChiShu);
      this.Controls.Add((Control) this.comboBoxYaliJiao);
      this.Controls.Add((Control) this.comboBoxMoShu);
      this.Controls.Add((Control) this.buttonOk);
      this.Name = "FormGear";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "齿轮参数输入";
      this.Load += new EventHandler(this.FormGear_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
