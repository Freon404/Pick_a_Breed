using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Xml.Linq;

namespace Pick_a_Breed.Models
{   public enum SizeEnum
    {
        Small,
        Medium,
        Large,
        Giant
    }
    public class Breed
    {
        public Guid id { get; set; }
        public string Name { get; set; }

        [EnumDataType(typeof(SizeEnum))]
        public SizeEnum Size { get; set; }
        public string Description { get; set; }
        public bool Favourite { get; set; }

        public List<Feature> ? Features { get; set; }
        public Breed(string name, SizeEnum size, string description, List<Feature> features)
        {
            Name = name;
            Size = size;
            Description = description;
            Features = features;
        }

        public Breed()
        {

        }

    }
}
