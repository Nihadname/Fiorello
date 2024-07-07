using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication11.Models.Common;

namespace WebApplication11.Models
{
    public class Setting:BaseEntity
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}
