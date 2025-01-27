using Microsoft.Build.Framework;
namespace MvcWebIdentityA.Areas.Admin.Models
{
    //Permite alterar as informações de uma Role dos Usuarios vinculados as esse Roles ou não.
    public class RoleModification
    {
        [Required]
        public string? RoleName { get; set; }
        public string? RoleId { get; set; }
        public string[]? AddIds{ get; set; }
        public string[]? DeleteIds { get; set; }
    }
}
