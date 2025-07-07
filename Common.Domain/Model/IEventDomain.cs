using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    /// <summary>
    /// 公共实体接口
    /// </summary>
    public interface IEventDomain
    {
        DateTime OccurredOnUtc { get; }
    }
}
