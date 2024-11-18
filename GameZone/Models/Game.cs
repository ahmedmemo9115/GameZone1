
namespace GameZone.Models
{
    public class Game :BaseEntity   
    {
      
        [MaxLength(2500)]
        public String Description { get; set; } = string.Empty;


        [MaxLength(500)]
        public String Cover { get; set; } = string.Empty;


        public int CategoryId { get; set; } 


        public Category Category { get; set; } = default!;


        public ICollection<GameDevice> Device { get; set; } = new List<GameDevice>();


    }
}
