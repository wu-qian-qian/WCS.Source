using Common.Infrastructure.Log;
using Communication.Domain.S7;
using S7.Net;
using S7.Net.Types;
using Serilog;
using WCS.Shared;

namespace Communication.Infrastructure.S7Net;

public class S7NetToken : Common.Infrastructure.Net.S7.S7Net
{
    public S7NetToken(S7NetConfig netConfig)
    {
        CpuType s7Cpu = netConfig.S7Type switch
        {
            S7TypeEnum.S71200 => CpuType.S71200,
            S7TypeEnum.S71500 => CpuType.S71500,
            _ => throw new ArgumentException("无可用类型"),
        };
        _plc = new Plc(s7Cpu, netConfig.Ip, netConfig.Port, netConfig.Rack, netConfig.Solt);
        _plc.ReadTimeout = netConfig.ReadTimeOut;
        _plc.WriteTimeout = netConfig.WriteTimeOut;
    }

    public override void Connect()
    {
        try
        {
            _plc.Open();
            Log.Logger.ForCategory(Common.Shared.LogCategory.Net).Information($"{_plc.IP}--链接成功");
        }
        catch (PlcException ex)
        {
            Log.Logger.ForCategory(Common.Shared.LogCategory.Net).Information($"{_plc.IP}--链接失败，{ex.Message}");
        }
    }

    public override void ReConnect()
    {
        Log.Logger.ForCategory(Common.Shared.LogCategory.Net).Information($"{_plc.IP}--开始重新链接");
        Connect();
    }

    public override void Close()
    {
        _plc.Close();
        Log.Logger.ForCategory(Common.Shared.LogCategory.Net).Information($"{_plc.IP}--关闭链接");
    }

    public async Task<byte[]> ReadAsync(DataItem bulkItem)
    {
        byte[] bufferBlock = default;
        if (_plc.IsConnected)
        {
            var result = _plc.ReadBytes(DataType.DataBlock, bulkItem.DB, bulkItem.StartByteAdr, bulkItem.Count);
            bufferBlock = result ?? Array.Empty<byte>();
            Log.Logger.ForCategory(Common.Shared.LogCategory.Net).Information($"{_plc.IP}--读取数据成功");
        }
        else
        {
            Log.Logger.ForCategory(Common.Shared.LogCategory.Net).Information($"{_plc.IP}--PLC未连接");
        }
        return bufferBlock;
    }
}