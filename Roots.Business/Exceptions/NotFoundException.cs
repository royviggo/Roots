using System;

namespace Roots.Business.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(object entity, object key)
            : base($"Entity [{entity.GetType()}] value [{key}] not found.")
        {
        }
    }
}
