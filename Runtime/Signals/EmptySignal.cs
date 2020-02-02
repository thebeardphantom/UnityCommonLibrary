namespace BeardPhantom.UCL.Signals
{
    public struct EmptySignalData { }

    public class EmptySignal : Signal<EmptySignalData>
    {
        #region Fields

        private static readonly EmptySignalData _data = new EmptySignalData();

        #endregion

        #region Methods

        public void Publish()
        {
            Publish(_data);
        }

        #endregion
    }
}