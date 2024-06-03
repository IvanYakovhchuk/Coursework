using Classes;

namespace КР
{
    public partial class Form1 : Form
    {
        public string? SortingOrder { get; set; } //Chosen order of sorting (ascending or descending)
        public string? SortingMethod { get; set; } //Chosen method of sorting (quicksort, heapsort, smoothsort)
        public string? ArrayCreation {  get; set; } //Type of array you want to create (organised, reversed, random)
        public TextBox LengthOfArrayTextBox { get; set; } //Gets the text from textbox to parse it to the ArrayLength property
        public int ArrayLength { get; set; } //Length of the created array
        public TextBox MinBorderTextBox { get; set; } //Gets the text from textbox to parse it to the MinBorder property
        public int MinBorder { get; set; } //Minimal border of generated numbers
        public TextBox MaxBorderTextBox { get; set; } //Gets the text from textbox to parse it to the MaxBorder property
        public int MaxBorder { get; set; } //Maximal border of generated numbers
        public int[]? MyArray { get; set; } //Created array
        public int[]? VisualiseArray {  get; set; } //Array the program will visualise sorting on (if the length of the source array is over 300)
        public int[]? SortedArray { get; set; } //Sorted array (created to write both source and sorted arrays in the file)
        private Panel? BlocksPanel {  get; set; } //The panel of blocks which will visualise sorting by animation of swapping places

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle; //Makes the border of the form fixed in the same position as created
            Size = Screen.PrimaryScreen.WorkingArea.Size;  //Sets the size of the form as the size of the screen
            StartPosition = FormStartPosition.CenterScreen; //Rendering the form on the center of the screen
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Button startButton = AddButton("Start", 1700, 900, 35, 150);
            Button saveButton = AddButton("Save", 1530, 900, 35, 150);
            //Adding buttons to communicate with the form

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
            //Creating user interface

            startButton.Click += StrtBtnClick_Click;
            saveButton.Click += SvBtnClick_Click;
            //Adding events that will happen when one of the button is clicked
        }
        private Button AddButton(string text, int x, int y, int Height, int Width)
        {
            Button button = new() // Creating the button with given parameters
            {
                Text = text,
                Location = new Point(x, y),
                Height = Height,
                Width = Width
            };
            Controls.Add(button); //Adding the botton to the control's panel (form)
            return button;
        }
        private void AddLabel(string text, int x, int y, int Height, int Width, string Font, int FontSize, FontStyle fontStyle)
        {
            Label label = new() //Creating the text label with given parameters
            {
                Text = text,
                Width = Width,
                Height = Height,
                Font = new Font(Font, FontSize, fontStyle),
                Location = new Point(x, y)
            };
            Controls.Add(label); //Adding this label to the control's panel (form)
        }
        private TextBox InitializeTextBox(int  x, int y, int Height, int Width)
        {
            TextBox textBox = new() //Creating the textbox with given parameters
            {
                Height = Height,
                Width = Width,
                Location = new Point(x, y),
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(textBox); //Adding this textbox to the control's panel (form)
            return textBox;
        }
        private static RadioButton AddRadioButton(string text, int x, int y, int Height, int Width)
        {
            RadioButton radioButton = new() //Creating radiobutton with given parameters
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
            //Creating three radiobuttons and grouping them by checking which one was chosen
            Controls.Add(RandomArrayRadioButton); //Adding this group of radiobuttons to the control's panel (form)

        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Event that is checking what radiobutton was checked and sets the property Array Creation to this radiobutton's text
            RadioButton? radioButton = sender as RadioButton; //This line giets an object that caused an event and sets it as radioButton variable
            if (radioButton != null && radioButton.Checked)
            {
                ArrayCreation = radioButton.Text;
            }
        }
        private static ComboBox AddComboBox(object[] items, int x, int y)
        {
            ComboBox ComboBox = new(); //Creating dropdown list with given parameters
            ComboBox.Items.AddRange(items); //Adds the items that you can choose to the dropdown list
            ComboBox.SelectedIndex = 0; //Sets the index which is chosen by default
            ComboBox.Location = new Point(x, y);
            ComboBox.DropDownStyle = ComboBoxStyle.DropDownList; //Remove an option of writing anything in the list
            ComboBox.Width = 150;
            return ComboBox;
        }
        private void InitializeComboBoxes()
        {
            //Creating two dropdown lists that allows user to choose the method and order of sorting

            ComboBox methodComboBox = AddComboBox(["Quick Sort", "Heap Sort", "Smooth Sort"], 100, 675);
            SortingMethod = methodComboBox.SelectedItem.ToString();
            //Sets the item of combobox by default (quicksort) to the property SortingMethod

            methodComboBox.SelectedIndexChanged += MethodComboBox_Checked;
            //If the item is changed, event MethodComboBox_Checked is called

            Controls.Add(methodComboBox); //Adding list to the control's panel (form)

            ComboBox orderComboBox = AddComboBox(["Ascending", "Descending"], 100, 825);
            SortingOrder = orderComboBox.SelectedItem.ToString();
            //Sets the item of combobox by default (ascending) to the property SortingMOrder

            orderComboBox.SelectedIndexChanged += OrderComboBox_Checked;
            //If the item is changed, event OrderComboBox_Checked is called

            Controls.Add(orderComboBox); //Adding list to the control's panel (form)
        }
        private void MethodComboBox_Checked(object sender, EventArgs e)
        {
            //Event that sets the SortingMethod property to the text of chosen item of combobox
            ComboBox? comboBox = sender as ComboBox; //This line gets the object that caused an event and sets it as comboBox variable
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                SortingMethod = comboBox.SelectedItem.ToString();
            }
        }
        private void OrderComboBox_Checked(object sender, EventArgs e)
        {
            //Event that sets the SortingOrder property to the text of chosen item of combobox
            ComboBox? comboBox = sender as ComboBox; //This line gets the object that caused an event and sets it as comboBox variable
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                SortingOrder = comboBox.SelectedItem.ToString();
            }
        }
        private void StrtBtnClick_Click(object sender, EventArgs e)
        {
            //Event that is caused when the user clicks button Start

            try
            {
                ArrayLength = int.Parse(LengthOfArrayTextBox.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Entered length is not a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Parsing entered length of an array

            if (ArrayLength > 50000 || ArrayLength < 100)
            {
                MessageBox.Show("Entered length is not in range of possible lengths!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Checking if the length is valid

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
            //Parsing entered range of generated numbers

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
            //Checking if the range is valid

            if (!string.IsNullOrEmpty(ArrayCreation)) //checking if user chose the type of array
            {
                if (ArrayCreation == "Organised")
                {
                    //User wants to create organised array
                    MyArray = GetArray.GetOrganisedArray(ArrayLength, MinBorder, MaxBorder);
                }
                if (ArrayCreation == "Reversed")
                {
                    //User wants to create reversed array
                    MyArray = GetArray.GetReversedArray(ArrayLength, MinBorder, MaxBorder);
                }
                if (ArrayCreation == "Random")
                {
                    //User wants to create random array
                    MyArray = GetArray.GetRandomArray(ArrayLength, MinBorder, MaxBorder);
                }
            }
            else
            {
                MessageBox.Show("Please, choose what array you want to create!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if user hasn't chosen the type of array, return an error

            if (MaxBorder < MinBorder + MyArray.Length)
            {
                MessageBox.Show("Enter the borders that are at least equal to the length of an array!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //checking if there is enough numbers in the range to create an array

            if (BlocksPanel != null)
            {
                BlocksPanel.Controls.Clear(); //clears all the elements (blocks) that was in this panel
                Controls.Remove(BlocksPanel); //removes panel from the control's panel (form)
                BlocksPanel.Dispose(); //deletes all the information about this panel
                BlocksPanel = null; //sets the pointer to it as null
            }
            //if the start button is clicked more than once and there already is the panel of blocks, removes it

            BlocksPanel = new Panel
            {
                Location = new Point(620, 50),
                Size = new Size(1250, 800),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            }; //creating new panel of blocks

            Controls.Add(BlocksPanel); //Adding this panel to the control's panel (form)
            CreateBlocks(BlocksPanel); //Adding blocks to the panel
            SortedArray = new int[ArrayLength];
            MyArray.CopyTo(SortedArray, 0);
            //Creating new array which will be sorted

            PerformSorting(); //Sorting an array
        }
        private void SvBtnClick_Click(object sender, EventArgs e)
        {
            //Event that is caused when the user clicks button Save

            if (MyArray == null || SortedArray == null)
            {
                MessageBox.Show("You have to create and sort an array in order to save the results!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if array is not created or sorted, returns an error

            SaveFileDialog saveFileDialog = new() //Calls the save window
            {
                Filter = "Текстові файли (*.txt)|*.txt|Всі файли (*.*)|*.*",
                Title = "Зберегти файл",
                DefaultExt = "txt",
                AddExtension = true
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName; //creates new file with entered name
                try
                {
                    using (StreamWriter writer = new(filePath)) //opens the file
                    {
                        writer.WriteLine($"Generated array (length - {MyArray.Length}):");
                        writer.WriteLine(string.Join(", ", MyArray));
                        writer.WriteLine("Sorted array:");
                        writer.WriteLine(string.Join(", ", SortedArray));
                        writer.WriteLine("Date and time of saving: " + DateTime.Now.ToString());
                        //writes an information in the file
                    }
                    //the file is closed
                    //the information about successful creation of the file
                    MessageBox.Show("File has been successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occured while saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } //if something goes wrong woth saving user's file, returns an error
            }
            //if the user clicks OK on the save window, creates a new file with results
        }
        private void CreateBlocks(Panel blocksPanel)
        {
            //this function creates blocks on the panel which is given by parameters

            int panelHeight = blocksPanel.ClientSize.Height;
            int panelWidth = blocksPanel.ClientSize.Width;
            //Configuration of the panel's size

            if (MyArray.Length <= 300) //if the length of an array is less than 300
            {
                int blockWidth = panelWidth / MyArray.Length; //Sets the one block's width
                int minHeight = MyArray.Min(); //the minimal height of the block sets as the lowest element in array
                int maxHeight = MyArray.Max(); //the maximal height of the block sets as the biggest element in array

                for (int i = 0; i < MyArray.Length; i++)
                {
                    //Calculate the height and the position of every block depending on it's value
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
                    //Creating the block and adding it to the control's panel
                    blocksPanel.Controls.Add(block);
                }
            }
            else //if the length of an array is more than 300, creates the new array that contains first 300 elements of the source array
            {
                //all this code performs the same action as the one higher
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
            } //checking if the bar indexes are valid

            Panel? block1 = panel.Controls[i] as Panel;
            Panel? block2 = panel.Controls[j] as Panel;

            if (block1 == null || block2 == null)
            {
                return;
            } //checking if the bars are not null

            int tempX = block1.Location.X;
            block1.Location = new Point(block2.Location.X, block1.Location.Y);
            block2.Location = new Point(tempX, block2.Location.Y);
            //swapping the bars location

            panel.Controls.SetChildIndex(block1, j);
            panel.Controls.SetChildIndex(block2, i);
            //swapping the indexes of the bars

            Thread.Sleep(1); //stops the process for 1 ms
        }
        private void Sorting_BlocksSwapped(object sender, BlockSwapEventArgs e)
        {
            //Event that is caused when two elements in array are swapped
            SwapBars(BlocksPanel, e.Index1, e.Index2);
        }
        private void PerformSorting()
        {
            int timeComplexity = 0; //counter od practical time complexity (calculates number of swaps! in array)

            if (ArrayLength <= 300)
            {
                //Sorts the SortedArray depending on user's choice

                if (SortingMethod == "Quick Sort")
                {
                    if (SortingOrder == "Ascending")
                    {
                        QuickSort.BlocksSwapped += Sorting_BlocksSwapped; //calling an event when elements are swapped inside an array
                        QuickSort.QuicksortAscending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        QuickSort.BlocksSwapped -= Sorting_BlocksSwapped; //event is no longer active even if the elements are swapped inside an array
                    }
                    if (SortingOrder == "Descending")
                    {
                        QuickSort.BlocksSwapped += Sorting_BlocksSwapped;
                        QuickSort.QuicksortDescending(SortedArray, 0, SortedArray.Length - 1, ref timeComplexity);
                        QuickSort.BlocksSwapped -= Sorting_BlocksSwapped;
                    }
                    MessageBox.Show($"Practical time complexity of this sorting is: {timeComplexity}");
                    //Showing the practical complexity of sorting
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
                //if ArrayLength is more than 300, performs sorting on the VisualiseArray
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
