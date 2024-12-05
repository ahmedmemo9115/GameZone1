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

        public Game? GetByID(int id)
        {
			return _context.Games
				 .Include(g => g.Category)
				 .Include(g => g.Devices)
				 .ThenInclude(d => d.Device)
				 .AsNoTracking()
				 .SingleOrDefault(g => g.Id == id);
        }

        public async Task Create(CreateGameFormViewModel model)
		{
			var coverName = await SaveCover(model.Cover);

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

		public async Task<Game?> Update(EditGameFormViewModel model)
		{
			var Game = _context.Games.
				Include(g => g.Devices)
				.SingleOrDefault(g => g.Id == model.id);

			if (Game is null)
				return null;

			var hasnewcover = model.Cover is not null;
			var oldcover = Game.Cover;

			Game.Name = model.Name;
			Game.Description = model.Description;
			Game.CategoryId = model.CategoryId;
			Game.Devices = model.SelectedDevices.Select(d => new GameDevice {DeviceId = d}).ToList() ;

			if (hasnewcover)
			{
				Game.Cover = await SaveCover(model.Cover!);
			}
			var effrows = _context.SaveChanges();

			if (effrows >0)
			{
				if (hasnewcover)
				{
					var cover = Path.Combine(_imagePath,oldcover);
					File.Delete(cover);
				}
				return Game;
			}
			else { 
				var cover = Path.Combine(_imagePath, Game.Cover);
				File.Delete(cover);
				return null;
			}
		}
		private async Task<string> SaveCover(IFormFile Cover)
		{

			if (Cover == null || Cover.Length == 0)
				throw new InvalidOperationException("Cover file is missing or empty.");

			if (!Directory.Exists(_imagePath))
				Directory.CreateDirectory(_imagePath);

			var coverName = $"{Guid.NewGuid()}{Path.GetExtension(Cover.FileName)}";
			var path = Path.Combine(_imagePath, coverName);

			try
			{
				using var stream = File.Create(path);
				await Cover.CopyToAsync(stream);
			}
			catch (Exception ex)
			{
				// Log and handle the error
				throw new Exception("An error occurred while saving the file.", ex);
			}
			return coverName;
		}
	}
}
