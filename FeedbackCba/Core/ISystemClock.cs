using System;

namespace FeedbackCba.Core
{
    public interface ISystemClock {
        DateTime Now { get; }
    }
}