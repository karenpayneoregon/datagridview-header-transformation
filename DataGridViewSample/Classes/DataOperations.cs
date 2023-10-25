using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using DataGridViewSample.Models;

namespace DataGridViewSample.Classes;

        internal class DataOperations
        {
            public static string SelectStatement =>
                """
                    SELECT Id,Title,Price,CategoryId 
                    FROM dbo.Books;
                    """;
            public static string SelectIncludeStatement =>
                """
                    SELECT Id,
                           Title,
                           Price,
                           Books.CategoryId,
                           Description
                    FROM dbo.Books
                        INNER JOIN dbo.Categories
                            ON Books.CategoryId = Categories.CategoryId;
                    """;
            public static async Task<DataTable> Books1()
            {
                await using var cn = new SqlConnection(ConnectionString());

                cn.Open();
                DataTable table = new();

                table.Load(await cn.ExecuteReaderAsync(SelectStatement));

                return table;
            }
            public static async Task<IEnumerable<Book>> BooksContainer()
            {
                await using var cn = new SqlConnection(ConnectionString());
                return await cn.QueryAsync<Book>(SelectStatement);
            }
            public static async Task Books()
            {
                await using var cn = new SqlConnection(ConnectionString());
                var books = await cn.QueryAsync<Book, Categories, Book>(
                    SelectIncludeStatement, 
                    (book, category) => { book.Category = category; return book; }, splitOn: nameof(Book.CategoryId));
            }
        }
