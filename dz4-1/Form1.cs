using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dz4_1
{
    public partial class Form1 : Form
    {
        //ToolBar toolBar;
        //ToolStrip ts;
        ImageList list;
        
        string filename;
        private ToolStripContainer toolStripContainer1;
        public string fontColor, backgroundColor, fontfamily;

        bool cut = false;
        public Form1()
        {
            InitializeComponent();
            

            //toolBar = new ToolBar();
            //ts = new ToolStrip();
            toolStripContainer1 = new ToolStripContainer();

            list = new ImageList();
            list.ImageSize = new Size(30,30);
            list.Images.Add(new Bitmap("new.png"));
            list.Images.Add(new Bitmap("open.png"));
            list.Images.Add(new Bitmap("cansel.png"));
            list.Images.Add(new Bitmap("save.png"));

            list.Images.Add(new Bitmap("copy.png"));
            list.Images.Add(new Bitmap("cut.png"));
            list.Images.Add(new Bitmap("paste.png"));
            list.Images.Add(new Bitmap("config.png"));

            //toolBar.ImageList = list;
            toolStrip1.ImageList = list;

            ToolStripButton toolBarButton1 = new ToolStripButton();
            toolBarButton1.ImageIndex = 0;
            toolBarButton1.Click += new EventHandler(tsClick);
            ToolStripButton toolBarButton2 = new ToolStripButton();
            toolBarButton2.ImageIndex = 1;
            toolBarButton2.Click += new EventHandler(tsClick);
            ToolStripButton toolBarButton3 = new ToolStripButton();
            toolBarButton3.ImageIndex = 2;
            toolBarButton3.Click += new EventHandler(tsClick);
            ToolStripButton toolBarButton4 = new ToolStripButton();
            toolBarButton4.ImageIndex = 3;
            toolBarButton4.Click += new EventHandler(tsClick);

            ToolStripSeparator separator1 = new ToolStripSeparator();

            ToolStripButton toolBarButton5 = new ToolStripButton();
            toolBarButton5.ImageIndex = 4;
            toolBarButton5.Click += new EventHandler(tsClick);
            ToolStripButton toolBarButton6 = new ToolStripButton();
            toolBarButton6.ImageIndex = 5;
            toolBarButton6.Click += new EventHandler(tsClick);
            ToolStripButton toolBarButton7 = new ToolStripButton();
            toolBarButton7.ImageIndex = 6;
            toolBarButton7.Click += new EventHandler(tsClick);
            ToolStripSeparator separator2 = new ToolStripSeparator();
            
            ToolStripButton toolBarButton8 = new ToolStripButton();
            toolBarButton8.ImageIndex = 7;
            toolBarButton8.Click += new EventHandler(tsClick);

            toolStrip1.Items.Add(toolBarButton1);
            toolStrip1.Items.Add(toolBarButton2);
            toolStrip1.Items.Add(toolBarButton3);
            toolStrip1.Items.Add(toolBarButton4);
            toolStrip1.Items.Add(separator1);
            toolStrip1.Items.Add(toolBarButton5);
            toolStrip1.Items.Add(toolBarButton6);
            toolStrip1.Items.Add(toolBarButton7);
            toolStrip1.Items.Add(separator2);
            toolStrip1.Items.Add(toolBarButton8);
            toolStrip1.Location = new Point();

            //Controls.Add(toolStrip1);
            textBox1.ContextMenuStrip = contextMenuStrip1;
            //
        }

        public void changeFontColor(System.Drawing.Color color)
        {
            textBox1.ForeColor = color;
        }

        public void changeBackgroundColor(System.Drawing.Color color)
        {
            this.BackColor = color;
            textBox1.BackColor = color;
            
        }

        private void tsClick(object sender, System.EventArgs e)
        {
            ToolStripButton toolBarButton = sender as ToolStripButton;
            switch (toolBarButton.ImageIndex)
            {
                case 0:
                    this.createNewDocument();
                    break;
                case 1:
                    this.openNewDocument();
                    break;
                case 2:
                    this.undoText();
                    break;
                case 3:
                    if (this.filename != null)
                    {
                        StreamWriter w = new StreamWriter(this.filename);
                        w.WriteLine(textBox1.Text);
                        w.Close();
                    }
                    else
                    {
                        this.saveFile();
                    }
                    break;
                case 4:
                    this.copyText();
                    break;
                case 5:
                    this.cutText();
                    break;
                case 6:
                    this.pasteText();
                    break;
                case 7:
                    Form2 form2 = new Form2(this);
                    form2.Show();
                    break;

            }
        }

        public RichTextBox getTextBox()
        {
            return textBox1;
        }

        private void undoText()
        {
            if(textBox1.CanUndo == true)
            {
                textBox1.Undo();
                //textBox1.ClearUndo();
            }
        }

        private void pasteText()
        {    
            textBox1.Paste();
            if(this.cut)
            {
                Clipboard.Clear();
                this.cut = false;
            }
            
        }

        private void cutText()
        {

            if (textBox1.SelectedText != "")
            {
                textBox1.Cut();
                this.cut = true;
            }

        }

        private void copyText()
        {
            if(textBox1.SelectionLength > 0)
            {
                textBox1.Copy();
            }
        }
        void tBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.createNewDocument();
        }
        private void createNewDocument()
        {
            if(textBox1.Text != "")
            {
                if(MessageBox.Show("Сохранить изменения?","Новый документ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (this.filename != null)
                    {
                        StreamWriter w = new StreamWriter(this.filename);
                        w.WriteLine(textBox1.Text);
                        w.Close();
                    }
                    else
                    {
                        this.saveFile();
                    }
                }
                this.filename = null;
                textBox1.Text = "";
                
            }
            this.Text = "Новый файл";
        }
        private void saveFile()
        {
            SaveFileDialog f2;
            f2 = new SaveFileDialog();
            f2.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (f2.ShowDialog() == DialogResult.OK)
            {
                this.filename = f2.FileName;
                StreamWriter w = new StreamWriter (this.filename);
                w.WriteLine(textBox1.Text);
                w.Close();
            }
            this.Text = this.filename;
        }
        private void openNewDocument()
        {
            OpenFileDialog f1;
            
            f1 = new OpenFileDialog();
            f1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            f1.FilterIndex = 1;
            if (f1.ShowDialog() == DialogResult.OK)
            {
                StreamReader r = File.OpenText(f1.FileName);
                this.filename = f1.FileName;
                textBox1.Text = r.ReadToEnd();
                r.Close();
            }
            this.Text = this.filename;
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFile();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.filename != null)
            {
                StreamWriter w = new StreamWriter(this.filename);
                w.WriteLine(textBox1.Text);
                w.Close();
            }
            else
            {
                this.saveFile();
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openNewDocument();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.copyText();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cutText();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pasteText();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.undoText();
        }

        private void отменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.copyText();
        }

        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.cutText();
        }

        private void вставитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.pasteText();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.undoText();
        }

        private void настройкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
        }


    }
}
