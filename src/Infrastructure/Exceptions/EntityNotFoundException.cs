using System;

namespace Athena.Infrastructure.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityType { get; }
        public string EntityName { get; }

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
        
        public EntityNotFoundException(string message, Exception inner) : 
            base(message, inner)
        {
        }
        
        public EntityNotFoundException(string message, string entityType, string entityName)
            : this(message)
        {
            EntityType = entityType;
            EntityName = entityName;
        }
    }
}