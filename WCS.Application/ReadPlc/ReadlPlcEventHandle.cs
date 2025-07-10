using Common.Application.MediatR.Message;
using Common.Application.Net.S7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Application.ReadPlc
{
    /// <summary>
    /// 最终处理者
    /// </summary>
    /// <param name="service"></param>
    internal class ReadlPlcEventHandle(INetService service) : ICommandHandler<ReadPlcEventCommand, byte[]>
    {
        /// <summary>
        /// 进行一些操作
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<byte[]> Handle(ReadPlcEventCommand request, CancellationToken cancellationToken)
        {
            if(request is ReadPlcEventCommand readCommand)
            {

            }
            return service.ReadAsync();
        }
    }
}
