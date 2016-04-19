using System;

namespace MArchiveLibrary.Attributes
{
    public class RoleAttribute : Attribute
    {
        public RoleEnum Role { get; set; }

        public RoleAttribute()
        {
        }

        public RoleAttribute(RoleEnum Role)
        {
            this.Role = Role;
        }
    }

    public enum RoleEnum
    {
        LoggedInUser,
        SuperDuperUser,
        SKIP_AUTHORIZATION
    }
}
