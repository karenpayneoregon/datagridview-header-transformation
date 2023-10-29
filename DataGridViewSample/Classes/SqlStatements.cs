namespace DataGridViewSample.Classes;
public class SqlStatements
{
    /// <summary>
    /// Get all books without category
    /// </summary>
    public static string GetBooks =>
        """
        SELECT Id,Title,Price,CategoryId
        FROM dbo.Books;
        """;
    /// <summary>
    /// Return all books with category
    /// </summary>
    public static string GetBooksWithCategories =>
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

    public static string GetBookWithCategories =>
        """
        SELECT Id,
                Title,
                Price,
                Books.CategoryId,
                Description
        FROM dbo.Books
            INNER JOIN dbo.Categories
                ON Books.CategoryId = Categories.CategoryId
        WHERE dbo.Books.Id = @Id
        """;
}
