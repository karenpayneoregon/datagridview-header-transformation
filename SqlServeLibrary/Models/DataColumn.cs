
namespace SqlServerLibrary.Models
{
    public class DataColumn
    {
        /// <summary>
        /// Name of column
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// Ordinal position of column
        /// </summary>
        /// <returns></returns>
        public int Ordinal { get; set; }
        /// <summary>
        /// Description of column
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// May be NULL
        /// </remarks>
        public string Description { get; set; }
        /// <summary>
        /// Determines if this column should be visible in the user interface.
        /// </summary>
        /// <returns></returns>
        public bool Visible => Description != null;
    }

}
