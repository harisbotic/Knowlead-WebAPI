using System;
using static Knowlead.Common.Constants;

namespace Knowlead.Common.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public string EntityName { get; set; }
        public string FormName { get; set; }
        
        private const string staticMessage = ErrorCodes.EntityNotFound;

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string entityName)
            : base(staticMessage)
        {
            EntityName = entityName;
        }

        public EntityNotFoundException(string formName, string entityName)
            : base(staticMessage)
        {
            FormName = formName;
            EntityName = entityName;
        }

        public EntityNotFoundException(string entityName, Exception inner)
            : base(staticMessage, inner)
        {
            EntityName = entityName;
        }
    }
}
