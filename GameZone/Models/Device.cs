namespace GameZone.Models
{
    public class Device : BaseEntity
    {

        [MaxLength(250)]
        public string Icon { get; set; } = string.Empty;
    }
}
