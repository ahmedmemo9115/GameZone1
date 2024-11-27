namespace GameZone.Services
{
	public class GamesService : IGamesService
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly string _imagePath;

		public GamesService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
			_imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
		}

		public IEnumerable<Game> GetAll()
		{
			return _context.Games
				.Include(g => g.Category)
				.Include(g => g.Devices)
				.ThenInclude(d => d.Device)
				.AsNoTracking()
				.ToList();
		}
		public async Task Create(CreateGameFormViewModel model)
		{
			if (model.Cover == null || model.Cover.Length == 0)
				throw new InvalidOperationException("Cover file is missing or empty.");

			if (!Directory.Exists(_imagePath))
				Directory.CreateDirectory(_imagePath);

			var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
			var path = Path.Combine(_imagePath, coverName);

			try
			{
				using var stream = File.Create(path);
				await model.Cover.CopyToAsync(stream);
			}
			catch (Exception ex)
			{
				// Log and handle the error
				throw new Exception("An error occurred while saving the file.", ex);
			}


			Game game = new()
			{
				Name = model.Name,
				Description = model.Description,
				CategoryId = model.CategoryId,
				Cover = coverName,
				Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
			};

			_context.Add(game);
			await _context.SaveChangesAsync();

		}

	}
}
