using System.Collections.Generic;

namespace Udemy.Identity.Models
{
    public class RoleAssingListModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool Exist { get; set; }
    }
    public class RoleAssingSendModel
    {
        public List<RoleAssingListModel> Roles { get; set; }
        public int UserId { get; set; }
    }
}
