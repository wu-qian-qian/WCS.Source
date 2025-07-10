using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Application.ReadPlc
{
    /// <summary>
    /// PLC 读取数据模型
    /// </summary>
    public sealed class ReadPlcEventCommand:PlcEventCommand
    {

    }

    /// <summary>
    /// 上层的 模型
    /// </summary>
    public class PlcEventCommand : ICommand<byte[]>
    {
        public string Ip { get; set; }
    }
}
