﻿namespace Common.Extensions.Net.Http;

public sealed record HttpOptions
{
    /// <summary>
    ///     每一次重试都调用
    /// </summary>
    public Action<HttpResponseMessage>? policCallback;

    public string BaseAddress { get; set; }

    public ushort Timeout { get; set; }

    public string Name { get; set; }

    public bool EnablePolicy { get; set; }

    /// <summary>
    ///     重试次数
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    ///     重试休眠事件间隔
    /// </summary>
    public int SleepDuration { get; set; }

    /// <summary>
    ///     获取鉴权Jwt
    /// </summary>
    public HttpRequestMessage GetAuthorization { get; set; }
}