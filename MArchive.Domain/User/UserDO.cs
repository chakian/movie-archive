﻿using MArchive.Domain.Base;

namespace MArchive.Domain.User {
    public class UserDO : BaseDO {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}