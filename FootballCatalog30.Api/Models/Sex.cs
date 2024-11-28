using Microsoft.OpenApi.Attributes;

namespace FootballCatalog30.Api.Models
{
    public enum Sex
    {
        [Display("муж")]
        Male = 0,
        [Display("жен")]
        Female = 1
    }
}
