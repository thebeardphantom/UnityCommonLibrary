namespace BeardPhantom.UCL.Signals
{
    public interface ISignal
    {
        #region Properties

        string SubscriberList { get; }

        #endregion

        #region Methods

        void UnsubscribeTarget(object target);

        #endregion
    }
}