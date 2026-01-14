using System;
using MediatR;

namespace Utlånssystem_Konvensjonell.SharedKernel;

public abstract record BaseDomainEvent : INotification
{
    public DateTimeOffset DateOccurred { get; protected set; }
        = DateTimeOffset.UtcNow;
}