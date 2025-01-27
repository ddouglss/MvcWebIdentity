using Microsoft.AspNetCore.Identity;

namespace MvcWebIdentityA.Areas.Admin.Models
{
    //Ira permitir saber quais são os Usarios que pertence a uma Role
    public class RoleEdit
    {
        public IdentityRole? Role { get; set; }
        public IEnumerable<IdentityUser>? Members { get; set; }
        public IEnumerable<IdentityUser>? NonMembers { get; set; }
    }
}
