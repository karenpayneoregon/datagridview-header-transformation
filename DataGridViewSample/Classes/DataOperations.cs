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
    public static async Task<IEnumerable<Book>> BooksContainer()
    {
        await using var cn = new SqlConnection(ConnectionString());
        return await cn.QueryAsync<Book>(SqlStatements.GetBooks);
    }

    /// <summary>
    /// Get all books
    /// </summary>
    /// <returns>Books with category</returns>
    public static async Task<IEnumerable<Book>> Books()
    {
        await using var cn = new SqlConnection(ConnectionString());
        IEnumerable<Book> books = await cn.QueryAsync<Book, Categories, Book>(
            SqlStatements.GetBooksWithCategories,
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
}
