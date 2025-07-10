using Common.Domain;
using WCS.Shared;

namespace Communication.Domain.S7;

public class S7NetConfig:INetEnitty
{
    public S7NetConfig(Guid id,string ip,string port) : base(id)
    {
    }
    public S7TypeEnum S7Type { get; set; } 
    
    /// <summary>
    /// 槽号
    /// </summary>
    public short Solt { get; set; } 
    
    /// <summary>
    /// 机架
    /// </summary>
    public short Rack { get; set; }
    
    /// <summary>
    /// 读取超时
    /// </summary>
    public int ReadTimeOut { get; set; }
    /// <summary>
    /// 写入超时
    /// </summary>
    public int WriteTimeOut { get; set; }

}