namespace BeardPhantom.UCL
{
    public interface IEventBusRegistrant<in T> where T : EventBusEvent
    {
        #region Methods

        void OnEventBusEvent(T data);

        #endregion
    }
}