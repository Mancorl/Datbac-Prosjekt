using System.Collections.Generic;

namespace Utlånssystem_Konvensjonell.SharedKernel;

public abstract class BaseEntity
{
	public List<BaseDomainEvent> Events = new();
}
