using MArchive.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace MArchive.Domain.Movie
{
    public class UserListDO : BaseDO
    {
        public int UserID { get; set; }
        
        [StringLength(250)]
        public string Name { get; set; }
    }
}