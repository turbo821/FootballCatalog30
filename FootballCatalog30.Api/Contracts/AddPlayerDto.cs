using FootballCatalog30.Api.Models;

namespace FootballCatalog30.Api.Contracts
{
    public record AddPlayerDto(
        string Name,
        string Surname,
        Sex Sex,
        DateTime BirthDate,
        string CommandTitle,
        int CountryId
    );
}
