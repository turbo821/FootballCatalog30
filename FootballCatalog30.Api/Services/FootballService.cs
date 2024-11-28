using FootballCatalog30.Api.Interfaces;

namespace FootballCatalog30.Api.Services
{
    public class FootballService : IFootballService
    {
        private IFootballRepository _repo;

        public FootballService(IFootballRepository repository)
        {
            _repo = repository;
        }
    }
}
