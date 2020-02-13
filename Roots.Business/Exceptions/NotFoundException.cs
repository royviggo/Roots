using System;

namespace Roots.Business.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity [{name}] value [{key}] not found.")
        {
        }
    }
}
