using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackHub.Model.Contracts
{
    internal interface IHasCreatedAt
    {
        public DateTime? CreatedAt { get; set; }
    }
}
