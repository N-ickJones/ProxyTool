

using System.Collections.Generic;

namespace ProxyWorldApi.Models
{
    public class Catalog
    {
        public int CatalogID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
