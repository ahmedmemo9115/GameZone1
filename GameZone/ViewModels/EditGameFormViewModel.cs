using GameZone.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameZone.ViewModels
{
    public class EditGameFormViewModel : GameFormViewModel
    {
        public int id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;

    }
}
