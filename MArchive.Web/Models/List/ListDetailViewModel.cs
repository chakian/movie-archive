using MArchive.Domain.Movie;
using System.Collections.Generic;

namespace MArchive.Web.Models.List
{
    public class ListDetailViewModel
    {
        public UserListDO List { get; set; }
        public List<UserListDetailDO> Movies { get; set; }
    }
}