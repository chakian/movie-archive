using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MArchive.Domain.Message
{
    public enum MessageCodes
    {
        TheseUsersAreAlreadyFriends = 101,
        ThereIsNoRequestToAccept = 102,
        ThereIsNoRequestToReject = 103
    }
}
