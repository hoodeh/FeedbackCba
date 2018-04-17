namespace FeedbackCba.Core
{
    public interface IFeedbackRecorder
    {
        bool CanProvideFeedback(string customerId, string pageUrl);
        void RecordFeedback(string customerId, string pageUrl);
    }
}