using FootballCatalog30.Api.Models;

namespace FootballCatalog30.Api.Contracts
{
    public record GetPlayerCatalogDto(
        int Id,
        string Name,
        string Surname,
        string Sex,
        DateTime BirthDate,
        string? CommandTitle,
        string? CountryTitle
    );
}
