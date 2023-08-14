using DataGridViewSample.Classes;
using SqlServerLibrary.Classes;

namespace DataGridViewSample
{
    public partial class Form1 : Form
    {
        private readonly BindingSource _bindingSource = new();
        public Form1()
        {
            InitializeComponent();
            Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            _bindingSource.DataSource = DataOperations.Books();
            dataGridView1.DataSource = _bindingSource;
            dataGridView1.ExpandColumns();
        }


        private void SetDescriptionsButton_Click(object sender, EventArgs e)
        {
            SetDataGridViewColumnHeaders();
        }

        /// <summary>
        /// Currently called in the above click event so before using you can
        /// see the natural column names. For a real app this method is called after
        /// setting the DataGridView DataSource
        /// </summary>
        private void SetDataGridViewColumnHeaders()
        {
            var columns = ColumnService.ColumnDetails(ConnectionString(), "Books");

            foreach (var column in columns)
            {
                dataGridView1.Columns[column.Name]!.HeaderText = column.Description;
            }
        }
    }
}