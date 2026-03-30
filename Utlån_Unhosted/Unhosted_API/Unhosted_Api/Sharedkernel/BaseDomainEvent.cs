using System;
using MediatR;

namespace Unhosted_Api.SharedKernel;

public abstract record BaseDomainEvent : INotification
{
    public DateTimeOffset DateOccurred { get; protected set; }
        = DateTimeOffset.UtcNow;
}