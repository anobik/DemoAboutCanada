/* Filename: ItemData.cs
 * Description: model for the main page to hold buisness logic 
 * */

using System.Collections.Generic;

namespace AboutCanada
{
    /// <summary>
    /// Row element
    /// </summary>
    public class Row
    {
        public string title { get; set; }
        public string description { get; set; }
        public string imageHref { get; set; }
    }

    /// <summary>
    /// Items data class
    /// </summary>
    public class ItemData
    {
        public string title { get; set; }
        public List<Row> rows { get; set; }
    }
}
