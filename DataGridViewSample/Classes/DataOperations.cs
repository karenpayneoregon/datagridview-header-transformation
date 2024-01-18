using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using DataGridViewSample.Models;

namespace DataGridViewSample.Classes;

internal class DataOperations
{

    public static async Task<DataTable> Books1()
    {
        await using var cn = new SqlConnection(ConnectionString());

        cn.Open();
        DataTable table = new();

        table.Load(await cn.ExecuteReaderAsync(SqlStatements.GetBooks));

        return table;
    }
    public static async Task<List<Book>> BooksContainer()
    {
        await using var cn = new SqlConnection(ConnectionString());
        return (List<Book>)await cn.QueryAsync<Book>(SqlStatements.GetBooks);
    }

    /// <summary>
    /// Get category for book
    /// </summary>
    /// <param name="book">Existing book</param>
    /// <remarks>
    /// For where the frontend does not always need a Book's category.
    /// </remarks>
    public static async Task GetCategory(Book book)
    {
        await using var cn = new SqlConnection(ConnectionString());
        SqlMapper.GridReader results = await cn.QueryMultipleAsync(SqlStatements.GetBookWithCategory, 
            new
            {
                book.CategoryId, 
                book.Id
            });

        Categories category = await results.ReadFirstAsync<Categories>();
        Book theBook = await results.ReadFirstAsync<Book>();
        
        book.Category = category;
    }

    public static async Task<Book> GetCategory(int id, int categoryId)
    {

        await using var cn = new SqlConnection(ConnectionString());
        SqlMapper.GridReader results = await cn.QueryMultipleAsync(SqlStatements.GetBookWithCategory,
            new
            {
                categoryId,
                id
            });

        Categories category = await results.ReadFirstAsync<Categories>();
        Book book = await results.ReadFirstAsync<Book>();
        book.Category = category;

        return book;
    }

    /// <summary>
    /// Get all books
    /// </summary>
    /// <returns>Books with category</returns>
    public static async Task<IEnumerable<Book>> Books()
    {
        await using var cn = new SqlConnection(ConnectionString());
        IEnumerable<Book> books = await cn.QueryAsync<Book, Categories, Book>(SqlStatements.GetBooksWithCategories,
            (book, category) =>
            {
                book.Category = category; return book;
            }, splitOn: nameof(Book.CategoryId));

        return books;
    }

    /// <summary>
    /// Get book by identifier with category
    /// </summary>
    /// <param name="id">Identifier to locate book with</param>
    /// <returns>Book</returns>
    public static async Task<Book> GetBook(int id)
    {
        await using var cn = new SqlConnection(ConnectionString());
        return cn.Query<Book, Categories, Book>(SqlStatements.GetBookWithCategories,
            (book, category) =>
            {
                book.Category = category; return book;
            }, 
            new { Id = id },
            splitOn: nameof(Book.CategoryId))
            .FirstOrDefault();
    }

    public static async Task<Book> GetBookAsync(int id)
    {
        await using var cn = new SqlConnection(ConnectionString());
        IEnumerable<Book> result= await cn.QueryAsync<Book, Categories, Book>(SqlStatements.GetBookWithCategories,
                (book, category) =>
                {
                    book.Category = category; 
                    return book;
                },
                new { Id = id },
                splitOn: nameof(Book.CategoryId));

        return result.FirstOrDefault();
    }

    public static async Task<Book> GetBookAsync1(int id)
    {
        await using var cn = new SqlConnection(ConnectionString());
        var result = await cn.QueryAsync<Book, Categories, Book>(SqlStatements.GetBookWithCategories,
            (book, category) =>
            {
                book.Category = category; return book;
            },
            new { Id = id },
            splitOn: nameof(Book.CategoryId));
        return result.FirstOrDefault();
    }
}