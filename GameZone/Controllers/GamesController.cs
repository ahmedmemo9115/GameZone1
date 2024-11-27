
namespace GameZone.Controllers
{
    public class GamesController : Controller
    {


        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesService _gamesService;



        public GamesController( ICategoriesService categoriesService,
            IDevicesService devicesService, 
            IGamesService gamesService)
        {
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gamesService = gamesService;
        }

		public IActionResult Index()
		{
			var games = _gamesService.GetAll();
			return View(games);
		}
        public IActionResult Details(int id)
        {
            var game = _gamesService.GetByID(id);

            if (game is null)
                return NotFound();
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categoriess = _categoriesService.GetSelectLists(),

                Devices = _devicesService.GetSelectLists()

            };
            return View(viewModel);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public  IActionResult Create(CreateGameFormViewModel model)
        {
            model.Categoriess = _categoriesService.GetSelectLists();
            model.Devices = _devicesService.GetSelectLists();
            if (!ModelState.IsValid)
            {
                return View("Create",model);
            }

           
          _gamesService.Create(model).GetAwaiter().GetResult();

            return RedirectToAction(nameof(Index));
        }

    }
}
