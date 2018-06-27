using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojoEvents.Models
{
    [Flags]
    public enum UserScopes
    {
        None = -1,
        CreateFestival = 0,
        EditFestival = 1,
        DeleteFestival = 2,
        SeeFestival = 3,
        CreateUsers = 4,
        SeeAllData = 5,
        PublishFestivals = 6
    }

}
