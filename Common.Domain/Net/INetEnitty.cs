namespace Common.Domain;

/// <summary>
/// 网络基础类
/// </summary>
public abstract class INetEnitty:IEntity
{
    protected INetEnitty(Guid id) : base(id)
    {
    }
    
    public string Ip { get; init; }
    
    public int Port { get; init; }
}