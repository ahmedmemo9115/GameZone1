namespace GameZone.Services
{
	public interface IGamesService
	{
		IEnumerable<Game> GetAll();

        Game? GetByID(int id);

		Task Create(CreateGameFormViewModel model);

		Task<Game?> Update(EditGameFormViewModel model);
	}
}
