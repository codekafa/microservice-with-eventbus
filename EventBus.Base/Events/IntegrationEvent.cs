using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Base.Events
{
    public class IntegrationEvent
    {

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            ID = id;
            CreateDate = createDate;
        }

     
        public IntegrationEvent()
        {
            ID = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }

        [JsonProperty]
        public Guid ID { get; private set; }

        [JsonProperty]
        public DateTime CreateDate { get; private set; }

    }
}
