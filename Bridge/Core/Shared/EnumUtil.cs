using System;
using System.Collections.Generic;
using System.Text;

namespace AppZseroEF6.Util
{
    public enum ProductStatus
    {
        available,
        outstock
    }
    public enum OrderStatusId
    {
        start = 1,
        done = 5,
        shopCancel =7,
        customerCancel = 10,
        outStock =9,
    }

    public enum OrderCurrentStatus
    {
        received,
        doing,
        done,
        cancel,
    }
}
