namespace Common.Domain;

/// <summary>
///     公共实体接口
/// </summary>
public interface IEventDomain
{
    DateTime OccurredOnUtc { get; }
}