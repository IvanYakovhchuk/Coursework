using System.Runtime.CompilerServices;
using System.Drawing;
using System.Windows.Forms;
using Classes;

namespace КР
{
    public partial class Form1 : Form
    {
        public string? SortingOrder { get; set; }
        public string? SortingMethod { get; set; }
        public string? ArrayCreation {  get; set; }
        public TextBox LengthOfArrayTextBox { get; set; }
        public int ArrayLength { get; set; }
        public int[]? MyArray { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Button startButton = AddButton("Start", 1700, 900, 35, 150);
            Button saveButton = AddButton("Save", 1530, 900, 35, 150);

            AddLabel("Enter the length of an array:", 100, 100, 50, 500, "Times", 18, FontStyle.Bold);
            LengthOfArrayTextBox = InitializeTextBox(100, 175, 60, 100);
            AddLabel("(from 100 to 50 000)", 210, 175, 50, 400, "Times", 14, FontStyle.Regular);

            AddLabel("Create an array:", 100, 250, 50, 500, "Times", 18, FontStyle.Bold);
            InitializeRadioButtons();

            AddLabel("Choose sorting method:", 100, 475, 50, 500, "Times", 18, FontStyle.Bold);
            ComboBox methodComboBox = InitializeComboBox(new object[] { "Quick Sort", "Heap Sort", "Smooth Sort" }, 100, 550);

            AddLabel("Choose sorting order:", 100, 620, 50, 500, "Times", 18, FontStyle.Bold);
            ComboBox orderComboBox = InitializeComboBox(new object[] { "Ascending", "Descending" }, 100, 700);

            SortingMethod = GetSortingMethod(methodComboBox);
            SortingOrder = GetSortingOrder(orderComboBox);

            startButton.Click += BtnClick_Click;
        }

        private Button AddButton(string text, int x, int y, int Height, int Width)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Height = Height;
            button.Width = Width;
            this.Controls.Add(button);
            return button;
        }
        private void AddLabel(string text, int x, int y, int Height, int Width, string Font, int FontSize, FontStyle fontStyle)
        {
            Label label = new Label();
            label.Text = text;
            label.Width = Width;
            label.Height = Height;
            label.Font = new Font(Font, FontSize, fontStyle);
            label.Location = new Point(x, y);
            this.Controls.Add(label);
        }
        private ComboBox InitializeComboBox(object[] items, int x, int y)
        {
            ComboBox ComboBox = new ComboBox();
            ComboBox.Items.AddRange(items);
            ComboBox.SelectedIndex = 0;
            ComboBox.Location = new Point(x, y);
            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox.Width = 150;
            this.Controls.Add(ComboBox);
            return ComboBox;
        }
        private TextBox InitializeTextBox(int  x, int y, int Height, int Width)
        {
            TextBox textBox = new TextBox();
            textBox.Height = Height;
            textBox.Width = Width;
            textBox.Location = new Point(x, y);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(textBox);
            return textBox;
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
            RadioButton OrganisedArrayRadioButton = AddRadioButton("Organised", 100, 300, 50, 150);
            OrganisedArrayRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            this.Controls.Add(OrganisedArrayRadioButton);
            RadioButton BackOrganisedArrayRadioButton = AddRadioButton("Reversed", 100, 350, 50, 150);
            BackOrganisedArrayRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            this.Controls.Add(BackOrganisedArrayRadioButton);
            RadioButton RandomArrayRadioButton = AddRadioButton("Random", 100, 400, 50, 150);
            RandomArrayRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            this.Controls.Add(RandomArrayRadioButton);

        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                if (radioButton.Text == "Organised")
                {
                    ArrayCreation = radioButton.Text;
                }
                else if (radioButton.Text == "Reversed")
                {
                    ArrayCreation = radioButton.Text;
                }
                else if (radioButton.Text == "Random")
                {
                    ArrayCreation = radioButton.Text;
                }
            }
        }
        private void BtnClick_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayLength = int.Parse(LengthOfArrayTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Entered length is not a number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ArrayLength > 50000 || ArrayLength < 100)
            {
                MessageBox.Show("Entered length is not in range of possible lengths!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(ArrayCreation))
            {
                if (ArrayCreation == "Organised")
                {
                    MyArray = GetArray.GetOrganisedArray(ArrayLength);
                }
                if (ArrayCreation == "Reversed")
                {
                    MyArray = GetArray.GetReversedArray(ArrayLength);
                }
                if (ArrayCreation == "Random")
                {
                    MyArray = GetArray.GetRandomArray(ArrayLength);
                }
            }
            else
            {
                MessageBox.Show("Please, choose what array you want to create!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }
        private string GetSortingOrder(ComboBox combobox)
        {
            string sortingOrder = combobox.SelectedItem.ToString();
            return sortingOrder;
        }
        private string GetSortingMethod(ComboBox combobox)
        {
            string sortingMethod = combobox.SelectedItem.ToString();
            return sortingMethod;
        }
    }
}
