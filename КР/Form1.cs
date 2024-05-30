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
        public TextBox MinBorderTextBox { get; set; }
        public int MinBorder { get; set; }
        public TextBox MaxBorderTextBox { get; set; }
        public int MaxBorder { get; set; }
        public int[]? MyArray { get; set; }
        public int[]? VisualiseArray {  get; set; }
        public int[]? SortedArray { get; set; }
        private Panel? BlocksPanel {  get; set; }

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Button startButton = AddButton("Start", 1700, 900, 35, 150);
            Button saveButton = AddButton("Save", 1530, 900, 35, 150);

            AddLabel("Enter the length of an array:", 100, 50, 50, 500, "Times", 18, FontStyle.Bold);
            LengthOfArrayTextBox = InitializeTextBox(100, 125, 60, 100);
            AddLabel("(from 100 to 50 000)", 210, 125, 50, 400, "Times", 14, FontStyle.Regular);

            AddLabel("Range of generated numbers:", 100, 200, 50, 500, "Times", 18, FontStyle.Bold);
            AddLabel("from (-100 000 to 100 000):", 100, 250, 50, 500, "Times", 12, FontStyle.Regular);
            MinBorderTextBox = InitializeTextBox(172, 315, 60, 100);
            AddLabel("Min:", 100, 315, 50, 75, "Times", 12, FontStyle.Italic);
            MaxBorderTextBox = InitializeTextBox(375, 315, 60, 100);
            AddLabel("Max:", 300, 315, 50, 75, "Times", 12, FontStyle.Italic);

            AddLabel("Create an array:", 100, 375, 50, 500, "Times", 18, FontStyle.Bold);
            InitializeRadioButtons();

            AddLabel("Choose sorting method:", 100, 600, 50, 500, "Times", 18, FontStyle.Bold);
            AddLabel("Choose sorting order:", 100, 745, 50, 500, "Times", 18, FontStyle.Bold);
            InitializeComboBoxes();
            startButton.Click += StrtBtnClick_Click;
            saveButton.Click += SvBtnClick_Click;
        }
        private Button AddButton(string text, int x, int y, int Height, int Width)
        {
            Button button = new()
            {
                Text = text,
                Location = new Point(x, y),
                Height = Height,
                Width = Width
            };
            Controls.Add(button);
            return button;
        }
        private void AddLabel(string text, int x, int y, int Height, int Width, string Font, int FontSize, FontStyle fontStyle)
        {
            Label label = new()
            {
                Text = text,
                Width = Width,
                Height = Height,
                Font = new Font(Font, FontSize, fontStyle),
                Location = new Point(x, y)
            };
            Controls.Add(label);
        }
        private TextBox InitializeTextBox(int  x, int y, int Height, int Width)
        {
            TextBox textBox = new()
            {
                Height = Height,
                Width = Width,
                Location = new Point(x, y),
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(textBox);
            return textBox;
        }
        private static RadioButton AddRadioButton(string text, int x, int y, int Height, int Width)
        {
            RadioButton radioButton = new()
            {
                Text = text,
                Location = new Point(x, y),
                Height = Height,
                Width = Width
            };
            return radioButton;
        }
        private void InitializeRadioButtons()
        {
            RadioButton OrganisedArrayRadioButton = AddRadioButton("Organised", 100, 425, 50, 150);
            OrganisedArrayRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            Controls.Add(OrganisedArrayRadioButton);
            RadioButton BackOrganisedArrayRadioButton = AddRadioButton("Reversed", 100, 475, 50, 150);
            BackOrganisedArrayRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            Controls.Add(BackOrganisedArrayRadioButton);
            RadioButton RandomArrayRadioButton = AddRadioButton("Random", 100, 525, 50, 150);
            RandomArrayRadioButton.CheckedChanged += RadioButton_CheckedChanged;
            Controls.Add(RandomArrayRadioButton);

        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton? radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                ArrayCreation = radioButton.Text;
            }
        }
        private static ComboBox AddComboBox(object[] items, int x, int y)
        {
            ComboBox ComboBox = new();
            ComboBox.Items.AddRange(items);
            ComboBox.SelectedIndex = 0;
            ComboBox.Location = new Point(x, y);
            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox.Width = 150;
            return ComboBox;
        }
        private void InitializeComboBoxes()
        {
            ComboBox methodComboBox = AddComboBox(["Quick Sort", "Heap Sort", "Smooth Sort"], 100, 675);
            SortingMethod = methodComboBox.SelectedItem.ToString();
            methodComboBox.SelectedIndexChanged += MethodComboBox_Checked;
            Controls.Add(methodComboBox);
            ComboBox orderComboBox = AddComboBox(["Ascending", "Descending"], 100, 825);
            SortingOrder = orderComboBox.SelectedItem.ToString();
            orderComboBox.SelectedIndexChanged += OrderComboBox_Checked;
            Controls.Add(orderComboBox);
        }
        private void MethodComboBox_Checked(object sender, EventArgs e)
        {
            ComboBox? comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                SortingMethod = comboBox.SelectedItem.ToString();
            }
        }
        private void OrderComboBox_Checked(object sender, EventArgs e)
        {
            ComboBox? comboBox = sender as ComboBox;
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
                MessageBox.Show("Entered length is not a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ArrayLength > 50000 || ArrayLength < 100)
            {
                MessageBox.Show("Entered length is not in range of possible lengths!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                MaxBorder = int.Parse(MaxBorderTextBox.Text);
                MinBorder = int.Parse(MinBorderTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Entered borders of the range of generated numbers are not a numbers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
;           }
            if (MaxBorder > 100000 || MinBorder < -100000)
            {
                MessageBox.Show("Entered borders of the range of generated numbers are out of max range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MaxBorder <= MinBorder)
            {
                MessageBox.Show("Minimum border of the range of generated numbers can't be larger than or equal to the maximum one!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(ArrayCreation))
            {
                if (ArrayCreation == "Organised")
                {
                    MyArray = GetArray.GetOrganisedArray(ArrayLength, MinBorder, MaxBorder);
                }
                if (ArrayCreation == "Reversed")
                {
                    MyArray = GetArray.GetReversedArray(ArrayLength, MinBorder, MaxBorder);
                }
                if (ArrayCreation == "Random")
                {
                    MyArray = GetArray.GetRandomArray(ArrayLength, MinBorder, MaxBorder);
                }
            }
            else
            {
                MessageBox.Show("Please, choose what array you want to create!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MaxBorder < MinBorder + MyArray.Length)
            {
                MessageBox.Show("Enter the borders that are at least equal to the length of an array!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (BlocksPanel != null)
            {
                BlocksPanel.Controls.Clear();
                Controls.Remove(BlocksPanel);
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
            Controls.Add(BlocksPanel);
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
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*",
                Title = "Зберегти файл",
                DefaultExt = "txt",
                AddExtension = true
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    using (StreamWriter writer = new(filePath))
                    {
                        writer.WriteLine($"Generated array (length - {MyArray.Length}):");
                        writer.WriteLine(string.Join(", ", MyArray));
                        writer.WriteLine("Sorted array:");
                        writer.WriteLine(string.Join(", ", SortedArray));
                        writer.WriteLine("Date and time of saving: " + DateTime.Now.ToString());
                    }

                    MessageBox.Show("File has been successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (MyArray.Length <= 300)
            {
                int blockWidth = panelWidth / MyArray.Length;
                int minHeight = MyArray.Min();
                int maxHeight = MyArray.Max();

                for (int i = 0; i < MyArray.Length; i++)
                {
                    int height = MyArray[i];
                    int scaledHeight = 1 + (height - minHeight) * (panelHeight - 1) / (maxHeight - minHeight);
                    int x = i * panelWidth / MyArray.Length;
                    int y = panelHeight - scaledHeight;

                    Panel block = new()
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
                VisualiseArray = new int[300];
                for (int i = 0; i < VisualiseArray.Length; i++)
                {
                    VisualiseArray[i] = MyArray[i];
                }
                int blockWidth = panelWidth / VisualiseArray.Length;
                int minHeight = VisualiseArray.Min();
                int maxHeight = VisualiseArray.Max();

                for (int i = 0; i < VisualiseArray.Length; i++)
                {
                    int height = VisualiseArray[i];
                    int scaledHeight = 1 + (height - minHeight) * (panelHeight - 1) / (maxHeight - minHeight);
                    int x = (i * panelWidth) / VisualiseArray.Length;
                    int y = panelHeight - scaledHeight;

                    Panel block = new()
                    {
                        Size = new Size(blockWidth, scaledHeight),
                        Location = new Point(x, y),
                        BackColor = Color.FromArgb(255, Math.Abs((height * 2) % 255), Math.Abs((height * 3) % 255))
                    };

                    blocksPanel.Controls.Add(block);
                }
            }
        }
        public static void SwapBars(Panel panel, int i, int j)
        {
            if (i < 0 || i >= panel.Controls.Count || j < 0 || j >= panel.Controls.Count)
            {
                return;
            }
            Panel? block1 = panel.Controls[i] as Panel;
            Panel? block2 = panel.Controls[j] as Panel;
            if (block1 == null || block2 == null)
            {
                return;
            }
            int tempX = block1.Location.X;
            block1.Location = new Point(block2.Location.X, block1.Location.Y);
            block2.Location = new Point(tempX, block2.Location.Y);
            panel.Controls.SetChildIndex(block1, j);
            panel.Controls.SetChildIndex(block2, i);
            Thread.Sleep(1);
        }
        private void Sorting_BlocksSwapped(object sender, BlockSwapEventArgs e)
        {
            SwapBars(BlocksPanel, e.Index1, e.Index2);
        }
        private void PerformSorting()
        {
            int timeComplexity = 0;
            if (ArrayLength <= 300)
            {
                if (SortingMethod == "Quick Sort")
                {
                    if (SortingOrder == "Ascending")
                    {
                        QuickSort.BlocksSwapped += Sorting_BlocksSwapped;
                        QuickSort.QuicksortAscending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        QuickSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    if (SortingOrder == "Descending")
                    {
                        QuickSort.BlocksSwapped += Sorting_BlocksSwapped;
                        QuickSort.QuicksortDescending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        QuickSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    MessageBox.Show($"Practical time complexity of this sorting is: {timeComplexity}");
                }
                else if (SortingMethod == "Heap Sort")
                {
                    if (SortingOrder == "Ascending")
                    {
                        HeapSort.BlocksSwapped += Sorting_BlocksSwapped;
                        SortedArray = HeapSort.HeapsortAscending(SortedArray, SortedArray.Length, ref timeComplexity);
                        HeapSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    if (SortingOrder == "Descending")
                    {
                        HeapSort.BlocksSwapped += Sorting_BlocksSwapped;
                        SortedArray = HeapSort.HeapsortDescending(SortedArray, SortedArray.Length, ref timeComplexity);
                        HeapSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    MessageBox.Show($"Practical time complexity of this sorting is: {timeComplexity}");
                }
                else if (SortingMethod == "Smooth Sort")
                {
                    if (SortingOrder == "Ascending")
                    {
                        SmoothSort.BlocksSwapped += Sorting_BlocksSwapped;
                        SmoothSort.SmoothSortAscending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        SmoothSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                   if (SortingOrder == "Descending")
                    {
                        SmoothSort.BlocksSwapped += Sorting_BlocksSwapped;
                        SmoothSort.SmoothSortDescending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        SmoothSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    MessageBox.Show($"Practical time complexity of this sorting is: {timeComplexity}");
                }
            }
            else
            {
                int timeComplexityVis = 0;
                for (int i = 0; i < VisualiseArray.Length; i++)
                {
                    VisualiseArray[i] = MyArray[i];
                }
                if (SortingMethod == "Quick Sort")
                {
                    if (SortingOrder == "Ascending")
                    {
                        QuickSort.QuicksortAscending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        QuickSort.BlocksSwapped += Sorting_BlocksSwapped;
                        QuickSort.QuicksortAscending(VisualiseArray, 0, VisualiseArray.Length - 1, ref timeComplexityVis);
                        QuickSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    if (SortingOrder == "Descending")
                    {
                        QuickSort.QuicksortDescending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        QuickSort.BlocksSwapped += Sorting_BlocksSwapped;
                        QuickSort.QuicksortDescending(VisualiseArray, 0, VisualiseArray.Length - 1, ref timeComplexityVis);
                        QuickSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    MessageBox.Show($"Practical time complexity of this sorting is: {timeComplexity}");
                }
                else if (SortingMethod == "Heap Sort")
                {
                    if (SortingOrder == "Ascending")
                    {
                        SortedArray = HeapSort.HeapsortAscending(SortedArray, SortedArray.Length, ref timeComplexity);
                        HeapSort.BlocksSwapped += Sorting_BlocksSwapped;
                        VisualiseArray = HeapSort.HeapsortAscending(VisualiseArray, VisualiseArray.Length, ref timeComplexityVis);
                        HeapSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    if (SortingOrder == "Descending")
                    {
                        SortedArray = HeapSort.HeapsortDescending(SortedArray, SortedArray.Length, ref timeComplexity);
                        HeapSort.BlocksSwapped += Sorting_BlocksSwapped;
                        VisualiseArray = HeapSort.HeapsortDescending(VisualiseArray, VisualiseArray.Length, ref timeComplexityVis);
                        HeapSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    MessageBox.Show($"Practical time complexity of this sorting is: {timeComplexity}");
                }
                else if (SortingMethod == "Smooth Sort")
                {
                    if (SortingOrder == "Ascending")
                    {
                        SmoothSort.SmoothSortAscending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        SmoothSort.BlocksSwapped += Sorting_BlocksSwapped;
                        SmoothSort.SmoothSortAscending(VisualiseArray, 0, VisualiseArray.Length - 1, ref timeComplexityVis);
                        SmoothSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    if (SortingOrder == "Descending")
                    {
                        SmoothSort.SmoothSortDescending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        SmoothSort.BlocksSwapped += Sorting_BlocksSwapped;
                        SmoothSort.SmoothSortDescending(VisualiseArray, 0, VisualiseArray.Length - 1, ref timeComplexityVis);
                        SmoothSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    MessageBox.Show($"Practical time complexity of this sorting is: {timeComplexity}");
                }
            }
        }
    }
}
