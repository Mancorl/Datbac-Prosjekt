using System.Collections.Generic;

namespace Unhosted_Api.SharedKernel;

public abstract class BaseEntity
{
	public List<BaseDomainEvent> Events = new();
}
