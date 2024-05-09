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
        public int[]? array { get; set; }

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
            this.Controls.Add(startButton);
            Button saveButton = AddButton("Save", 1530, 900, 35, 150);
            this.Controls.Add(saveButton);
            ComboBox methodComboBox = new ComboBox();
            methodComboBox.Items.AddRange(new object[] { "Quick Sort", "Heap Sort", "Smooth Sort" });
            methodComboBox.SelectedIndex = 0;
            methodComboBox.Location = new Point(100, 175);
            methodComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            methodComboBox.Width = 150;
            this.Controls.Add(methodComboBox);
            SortingMethod = GetSortingMethod(methodComboBox);
            AddLabel("Choose sorting method:", 100, 100, 50, 500);
            AddLabel("Create an array:", 100, 250, 50, 500);
            InitializeRadioButtons();
            AddLabel("Choose sorting order:", 100, 475, 50, 500);
            ComboBox orderComboBox = new ComboBox();
            orderComboBox.Items.AddRange(new object[] { "Ascending", "Descending" });
            orderComboBox.SelectedIndex = 0;
            orderComboBox.Location = new Point(100, 550);
            orderComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            orderComboBox.Width = 150;
            this.Controls.Add(orderComboBox);
            SortingOrder = GetSortingOrder(orderComboBox);
            AddLabel("Enter the length of an array:", 100, 620, 50, 500);
            LengthOfArrayTextBox = new TextBox();
            LengthOfArrayTextBox.Height = 60;
            LengthOfArrayTextBox.Width = 100;
            LengthOfArrayTextBox.Location = new Point(100, 700);
            LengthOfArrayTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(LengthOfArrayTextBox);
            startButton.Click += BtnClick_Click;
        }

        private Button AddButton(string text, int x, int y, int Height, int Width)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Height = Height;
            button.Width = Width;
            return button;
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
            if (!string.IsNullOrEmpty(ArrayCreation))
            {
                if (ArrayCreation == "Organised")
                {
                    array = GetArray.GetOrganisedArray(ArrayLength);
                }
                if (ArrayCreation == "Reversed")
                {
                    array = GetArray.GetReversedArray(ArrayLength);
                }
                if (ArrayCreation == "Random")
                {
                    array = GetArray.GetRandomArray(ArrayLength);
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
