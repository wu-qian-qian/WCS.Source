using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Domain.S7
{
    public class S7EntityItem : IEntity
    {
        public S7EntityItem(Guid id) : base(id)
        {
        }
    }
}
