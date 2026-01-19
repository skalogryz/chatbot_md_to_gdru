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
using static Md2ChatToGd.MdToGdRu;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace ChatBotMdToGdRu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxMarkdown_TextChanged(object sender, EventArgs e)
        {
            ConvertText();
        }

        public void ConvertText()
        {
            var text = Convert(textBoxMarkdown.Text);
            textBoxGdru.Text = text;
            if (chkAutoCopy.Checked)
            {
                // the clipboard might be busy this event
                this.BeginInvoke(
                    new Action(() => { CopyToBuffer(false); })
                    );
            }
        }

        public void CopyToBuffer(bool showError)
        {
            try
            {
                Clipboard.SetText(textBoxGdru.Text);
            }
            catch (Exception x)
            {
                if (showError)
                    MessageBox.Show($"не смог скоировать в буффер: {x.Message}");
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyToBuffer(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }


        public string GetConfigFileName()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(path, "ChatBotMdToGdRu", "config.json");
        }
        public void LoadConfig()
        {
            var path = GetConfigFileName();
            if (!File.Exists(path))
                return;
            var json = File.ReadAllText(path);
            try
            {
                var cfg = JsonConvert.DeserializeObject<UIConfig>(json);
                if (cfg != null)
                    UIConfig.cfg = cfg;

                this.Bounds = cfg.bounds;
                this.containerMain.SplitterDistance = cfg.offset;
                this.chkAutoCopy.Checked = cfg.isAutoCopy;
            }
            catch
            {

            }
        }

        public void SaveConfig()
        {
            try
            {
                if (UIConfig.cfg == null) 
                    UIConfig.cfg = new UIConfig();

                var cfg = UIConfig.cfg;
                cfg.bounds = this.Bounds;
                cfg.offset = this.containerMain.SplitterDistance;
                cfg.isAutoCopy = this.chkAutoCopy.Checked;

                var json = JsonConvert.SerializeObject(cfg, Formatting.Indented);
                if (string.IsNullOrWhiteSpace(json)) return;
                var path = GetConfigFileName();
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText(path, json);
            }
            catch
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }
    }
}
