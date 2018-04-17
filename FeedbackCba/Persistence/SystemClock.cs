using System;
using FeedbackCba.Core;

namespace FeedbackCba.Persistence
{
    public class SystemClock : ISystemClock
    {

        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}