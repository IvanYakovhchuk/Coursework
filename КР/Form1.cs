using System.Runtime.CompilerServices;
using System.Drawing;
using System.Windows.Forms;

namespace КР
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddButton("Start", 1700, 900, 35, 150);
            AddButton("Save", 1530, 900, 35, 150);
            ComboBox comboBox = new ComboBox();
            comboBox.Items.AddRange(new object[] { "Quick Sort", "Heap Sort", "Smooth Sort" });
            comboBox.SelectedIndex = 0;
            comboBox.Location = new Point(100, 175);
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Width = 150;
            this.Controls.Add(comboBox);
            AddLabel("Choose sorting method:", 100, 100, 50, 500);
            AddLabel("Create an array:", 100, 250, 50, 500);
            InitializeRadioButtons();
        }
        private void AddButton(string text, int x, int y, int Height, int Width)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Height = Height;
            button.Width = Width;
            this.Controls.Add(button);
        }
        private void AddLabel(string text, int x, int y, int Height, int Width)
        {
            Label label = new Label();
            label.Text = text;
            label.Width = Width;
            label.Height = Height;
            label.Font = new Font("Times", 18, FontStyle.Bold);
            label.Location = new Point(x, y);
            this.Controls.Add(label);
        }
        private RadioButton AddRadioButton(string text, int x, int y, int Height, int Width)
        {
            RadioButton radioButton = new RadioButton();
            radioButton.Text = text;
            radioButton.Location = new Point(x, y);
            radioButton.Height = Height;
            radioButton.Width = Width;
            return radioButton;
        }
        private void InitializeRadioButtons()
        {
            RadioButton manualInputRadioButton = AddRadioButton("Manually", 100, 300, 50, 150);
            manualInputRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            this.Controls.Add(manualInputRadioButton);
            RadioButton generatedInputRadioButton = AddRadioButton("Generate", 100, 335, 50, 150);
            generatedInputRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            this.Controls.Add(generatedInputRadioButton);
        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                if (radioButton.Text == "Manually")
                {
                    MessageBox.Show("Manual input was chosen!");
                }
                else if (radioButton.Text == "Generate")
                {
                    MessageBox.Show("Generated input was chosen!");
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
