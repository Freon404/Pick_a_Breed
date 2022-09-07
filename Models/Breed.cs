using System.Drawing;
using System.Xml.Linq;

namespace Pick_a_Breed.Models
{
    public class Breed
    {
        public Guid id { get; set; }
        public string Name { get; set; }

        public string Size { get; set; }
        public string Description { get; set; }

        public Breed()
        {

        }

        public Breed(string name, string size, string description)
        {
            this.Name = name;
            this.Size = size;
            this.Description = description;
        }

    }
}
