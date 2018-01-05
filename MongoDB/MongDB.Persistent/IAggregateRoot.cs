using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongDB.Persistent
{
    public interface IAggregateRoot
    {
        string Id { get; set; }
    }
}