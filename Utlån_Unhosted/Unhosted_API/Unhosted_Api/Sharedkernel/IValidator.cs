using System.Collections.Generic;

namespace Unhosted_Api.SharedKernel;

public interface IValidator<T>
{
	(bool IsValid, string Error) IsValid(T item);
}
