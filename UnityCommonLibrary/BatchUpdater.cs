using System.Collections.Generic;

namespace BeardPhantom.UCL
{
    public static class BatchUpdater<T> where T : IBatchUpdatable
    {
        #region Fields

        private static readonly HashSet<T> _entries = new HashSet<T>();

        #endregion

        #region Constructors

        static BatchUpdater()
        {
            PlayerLoopHook.Register(DoBatchUpdate);
        }

        #endregion

        #region Methods

        internal static void Register(T entry)
        {
            _entries.Add(entry);
        }

        internal static void Unregister(T entry)
        {
            _entries.Remove(entry);
        }

        private static void DoBatchUpdate()
        {
            foreach (var entry in _entries)
            {
                entry.BatchUpdate();
            }
        }

        #endregion
    }

    public static class BatchUpdater
    {
        #region Methods

        public static void Register<T>(T entry) where T : IBatchUpdatable
        {
            BatchUpdater<T>.Register(entry);
        }

        public static void Unregister<T>(T entry) where T : IBatchUpdatable
        {
            BatchUpdater<T>.Unregister(entry);
        }

        #endregion
    }
}