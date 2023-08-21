using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class OkulTur
    {
        [Key]
        public int Id { get; set; }
        public string Tip { get; set; }
    }
}
