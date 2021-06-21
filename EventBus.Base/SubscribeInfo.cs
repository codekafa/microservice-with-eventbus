using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Base
{
    public class SubscribeInfo
    {
        public Type HandlerType { get; private set; }


        public SubscribeInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }


        public static SubscribeInfo Typed(Type handlerType)
        {
            return new SubscribeInfo(handlerType);
        }
    }
}
