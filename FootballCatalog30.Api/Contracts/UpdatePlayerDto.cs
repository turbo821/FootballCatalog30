using FootballCatalog30.Api.Models;

namespace FootballCatalog30.Api.Contracts
{
    public record UpdatePlayerDto(
        int Id,
        string Name,
        string Surname,
        Sex Sex,
        DateTime BirthDate,
        string CommandTitle,
        int CountryId
    );
}
