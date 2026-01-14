using System.Collections.Generic;

namespace Utlånssystem_Konvensjonell.SharedKernel;

public interface IValidator<T>
{
	(bool IsValid, string Error) IsValid(T item);
}
