using System.ComponentModel;
using DataGridViewSample.Classes;
using DataGridViewSample.Models;
using SqlServerLibrary.Classes;

namespace DataGridViewSample
{
    public partial class Form1 : Form
    {
        private readonly BindingSource _bindingSource = new();
        private BindingList<Book> _bindingList = new();
        public Form1()
        {
            InitializeComponent();
            Shown += OnShown;
        }

        private async void OnShown(object sender, EventArgs e)
        {
            _bindingList = new BindingList<Book>(await DataOperations.BooksContainer());
            _bindingSource.DataSource = _bindingList;
            dataGridView1.DataSource = _bindingSource;
            dataGridView1.ExpandColumns();
            var test = await DataOperations.GetBookAsync2(1);

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

        private async void GetCurrentFromTableButton_Click(object sender, EventArgs e)
        {
            Book book = _bindingList[_bindingSource.Position];

            await DataOperations.GetCategory(book);

            var book1 = await DataOperations.GetCategory(book.Id, book.CategoryId);
        }
    }
}