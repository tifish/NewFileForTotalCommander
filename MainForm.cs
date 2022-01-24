using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NewFileForTotalCommander
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;

            _ini = new IniFile();

            encodingComboBox.SelectedIndex = 0;

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                var path = args[1];
                if (Directory.Exists(path) && !path.EndsWith(@"\"))
                    path += @"\";
                fileTextBox.Text = path;
            }

            fileTextBox.SelectionStart = fileTextBox.Text.Length;
        }

        private readonly IniFile _ini;

        private void okButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(fileTextBox.Text))
            {
                MessageBox.Show("File already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Directory.Exists(fileTextBox.Text))
            {
                MessageBox.Show("Directory already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var encoding = encodingComboBox.Text switch
                {
                    "ASCII" => Encoding.ASCII,
                    "UTF-8 with BOM" => Encoding.UTF8,
                    "Unicode" => Encoding.Unicode,
                    "UTF-32" => Encoding.UTF32,
                    "Big Endian Unicode" => Encoding.BigEndianUnicode,
                    "UTF-7" => Encoding.UTF7,
                    _ => throw new ArgumentOutOfRangeException(),
                };

                File.WriteAllText(fileTextBox.Text, string.Empty, encoding);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (encodingComboBox.Text != (string) encodingComboBox.Items[0])
            {
                var ext = Path.GetExtension(fileTextBox.Text);
                _ini.Write(ext, encodingComboBox.Text, "Encodings");
            }

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fileTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var ext = Path.GetExtension(fileTextBox.Text.Trim());
                var encoding = _ini.Read(ext, "Encodings");
                encodingComboBox.Text = string.IsNullOrEmpty(encoding)
                    ? (string) encodingComboBox.Items[0]
                    : encoding;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
