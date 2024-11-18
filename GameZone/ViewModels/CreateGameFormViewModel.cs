
using GameZone.Attributes;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel
    {

        [MaxLength(2500), Required] 
        public String Name { get; set; } = string.Empty;

        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categoriess { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [MaxLength(2500)]
        public String Description { get; set; } = string.Empty;

        [AllowedExtensions(FileSettings.AllowedExtensions) ,MaxFileSize(FileSettings.MaxFileSizeInBytes) ] 
        
        public IFormFile Cover { get; set; } = default!;



    }
}
