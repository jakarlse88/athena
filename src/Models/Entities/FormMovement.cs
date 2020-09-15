using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class FormMovement
    {
        public int FormId { get; set; }
        public int MovementId { get; set; }

        public virtual Form Form { get; set; }
        public virtual Movement Movement { get; set; }
    }
}
