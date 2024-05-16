using System.Runtime.CompilerServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Classes;
using System;

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
        public int[]? VisualiseArray {  get; set; }
        public int[]? SortedArray { get; set; }
        private Panel? BlocksPanel {  get; set; }

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
            AddLabel("Choose sorting order:", 100, 620, 50, 500, "Times", 18, FontStyle.Bold);
            InitializeComboBoxes();
            startButton.Click += StrtBtnClick_Click;
            saveButton.Click += SvBtnClick_Click;
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
                ArrayCreation = radioButton.Text;
            }
        }
        private ComboBox AddComboBox(object[] items, int x, int y)
        {
            ComboBox ComboBox = new ComboBox();
            ComboBox.Items.AddRange(items);
            ComboBox.SelectedIndex = 0;
            ComboBox.Location = new Point(x, y);
            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox.Width = 150;
            return ComboBox;
        }
        private void InitializeComboBoxes()
        {
            ComboBox methodComboBox = AddComboBox(new object[] { "Quick Sort", "Heap Sort", "Smooth Sort" }, 100, 550);
            SortingMethod = methodComboBox.SelectedItem.ToString();
            methodComboBox.SelectedIndexChanged += methodComboBox_Checked;
            this.Controls.Add(methodComboBox);
            ComboBox orderComboBox = AddComboBox(new object[] { "Ascending", "Descending" }, 100, 700);
            SortingOrder = orderComboBox.SelectedItem.ToString();
            orderComboBox.SelectedIndexChanged += orderComboBox_Checked;
            this.Controls.Add(orderComboBox);
        }
        private void methodComboBox_Checked(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                SortingMethod = comboBox.SelectedItem.ToString();
            }
        }
        private void orderComboBox_Checked(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                SortingOrder = comboBox.SelectedItem.ToString();
            }
        }
        private void StrtBtnClick_Click(object sender, EventArgs e)
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
            if (BlocksPanel != null)
            {
                BlocksPanel.Controls.Clear();
                this.Controls.Remove(BlocksPanel);
                BlocksPanel.Dispose();
                BlocksPanel = null;
            }
            BlocksPanel = new Panel
            {
                Location = new Point(620, 50),
                Size = new Size(1250, 800),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(BlocksPanel);
            CreateBlocks(BlocksPanel);
            SortedArray = new int[ArrayLength];
            MyArray.CopyTo(SortedArray, 0);
            PerformSorting();
        }
        private void SvBtnClick_Click(object sender, EventArgs e)
        {
            if (MyArray == null || SortedArray == null)
            {
                MessageBox.Show("You have to create and sort an array in order to save the results!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*";
            saveFileDialog.Title = "Зберегти файл";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("Generated array:");
                        writer.WriteLine(string.Join(", ", MyArray));
                        writer.WriteLine("Sorted array:");
                        writer.WriteLine(string.Join(", ", SortedArray));
                        writer.WriteLine("Date and time of saving: " + DateTime.Now.ToString());
                    }

                    MessageBox.Show("File has been successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occured while saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void CreateBlocks(Panel blocksPanel)
        {
            int panelHeight = blocksPanel.ClientSize.Height;
            int panelWidth = blocksPanel.ClientSize.Width;

            if (MyArray.Length <= blocksPanel.ClientSize.Width)
            {
                int blockWidth = panelWidth / MyArray.Length;
                if (blockWidth < 1)
                {
                    blockWidth = 1;
                }
                int minHeight = MyArray.Min();
                int maxHeight = MyArray.Max();

                for (int i = 0; i < MyArray.Length; i++)
                {
                    int height = MyArray[i];
                    int scaledHeight = 1 + (height - minHeight) * (panelHeight - 1) / (maxHeight - minHeight);
                    int x = i * panelWidth / MyArray.Length;
                    int y = panelHeight - scaledHeight;

                    Panel block = new Panel
                    {
                        Size = new Size(blockWidth, scaledHeight),
                        Location = new Point(x, y),
                        BackColor = Color.FromArgb(255, Math.Abs((height * 2) % 255), Math.Abs((height * 3) % 255))
                    };

                    blocksPanel.Controls.Add(block);
                }
            }
            else
            {
                VisualiseArray = new int[blocksPanel.ClientSize.Width];
                for (int i = 0; i < blocksPanel.ClientSize.Width; i++)
                {
                    VisualiseArray[i] = MyArray[i];
                }
                int blockWidth = panelWidth / VisualiseArray.Length;
                if (blockWidth < 1)
                {
                    blockWidth = 1;
                }
                int minHeight = VisualiseArray.Min();
                int maxHeight = VisualiseArray.Max();

                for (int i = 0; i < VisualiseArray.Length; i++)
                {
                    int height = VisualiseArray[i];
                    int scaledHeight = 1 + (height - minHeight) * (panelHeight - 1) / (maxHeight - minHeight);
                    int x = i * blockWidth;
                    int y = panelHeight - scaledHeight;

                    Panel block = new Panel
                    {
                        Size = new Size(blockWidth, scaledHeight),
                        Location = new Point(x, y),
                        BackColor = Color.FromArgb(255, Math.Abs((height * 2) % 255), Math.Abs((height * 3) % 255))
                    };

                    blocksPanel.Controls.Add(block);
                }
            }
        }
        public void SwapBars(Panel panel, int i, int j)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new Action(() => SwapBars(panel, i, j)));
                return;
            }
            if (i < 0 || i >= panel.Controls.Count || j < 0 || j >= panel.Controls.Count)
            {
                return;
            }
            Panel block1 = panel.Controls[i] as Panel;
            Panel block2 = panel.Controls[j] as Panel;
            if (block1 == null || block2 == null)
            {
                return;
            }
            int tempX = block1.Location.X;
            block1.Location = new Point(block2.Location.X, block1.Location.Y);
            block2.Location = new Point(tempX, block2.Location.Y);
            panel.Controls.SetChildIndex(block1, j);
            panel.Controls.SetChildIndex(block2, i);
            System.Threading.Thread.Sleep(1);
        }
        private void Sorting_BlocksSwapped(object sender, BlockSwapEventArgs e)
        {
            SwapBars(BlocksPanel, e.Index1, e.Index2);
        }
        private void PerformSorting()
        {
            if (SortingMethod == "Quick Sort")
            {
                if (SortingOrder == "Ascending")
                {
                    QuickSort.BlocksSwapped += Sorting_BlocksSwapped;
                    QuickSort.QuicksortAscending(SortedArray, 0, SortedArray.Length - 1);
                    QuickSort.BlocksSwapped -= Sorting_BlocksSwapped;
                }
                if (SortingOrder == "Descending")
                {
                    QuickSort.BlocksSwapped += Sorting_BlocksSwapped;
                    QuickSort.QuicksortDescending(SortedArray, 0, SortedArray.Length - 1);
                    QuickSort.BlocksSwapped -= Sorting_BlocksSwapped;
                }
            }
            else if (SortingMethod == "Heap Sort")
            {
                if (SortingOrder == "Ascending")
                {
                    HeapSort.BlocksSwapped += Sorting_BlocksSwapped;
                    SortedArray = HeapSort.HeapsortAscending(SortedArray, SortedArray.Length);
                    HeapSort.BlocksSwapped -= Sorting_BlocksSwapped;
                }
                if (SortingOrder == "Descending")
                {
                    HeapSort.BlocksSwapped += Sorting_BlocksSwapped;
                    SortedArray = HeapSort.HeapsortDescending(SortedArray, SortedArray.Length);
                    HeapSort.BlocksSwapped -= Sorting_BlocksSwapped;
                }
            }
            else if (SortingMethod == "Smooth Sort")
            {
                if (SortingOrder == "Ascending")
                {
                    SmoothSort.BlocksSwapped += Sorting_BlocksSwapped;
                    SortedArray = SmoothSort.SmoothSortAscending(SortedArray);
                    SmoothSort.BlocksSwapped -= Sorting_BlocksSwapped;
                }
                if (SortingOrder == "Descending")
                {
                    SmoothSort.BlocksSwapped += Sorting_BlocksSwapped;
                    SortedArray = SmoothSort.SmoothSortDescending(SortedArray);
                    SmoothSort.BlocksSwapped -= Sorting_BlocksSwapped;
                }
            }
        }
    }
}
