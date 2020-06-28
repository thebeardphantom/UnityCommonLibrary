using System.Collections.Generic;
using System.Linq;
using BeardPhantom.UCL.Utility;
using UnityEngine;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// A stateful utility class for obtaining random items from a collection.
    /// </summary>
    public class CollectionSelector<T>
    {
        #region Fields

        /// <summary>
        /// A preshuffled list of indicies to use in order for <see cref="GetRandomRoundRobin" />
        /// </summary>
        private readonly List<int> _shuffledIndicies = new List<int>();

        /// <summary>
        /// Working list of items
        /// </summary>
        private IList<T> _collection;

        /// <summary>
        /// The last selected index from <see cref="GetRandomNew" />
        /// </summary>
        private int _lastSelected;

        #endregion

        #region Constructors

        /// <summary>
        /// Builds a collection selector given a collection.
        /// </summary>
        public CollectionSelector(IList<T> collection)
        {
            UpdateItems(collection);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a random item from the list.
        /// </summary>
        public T GetRandom()
        {
            return _collection[Random.Range(0, _collection.Count)];
        }

        /// <summary>
        /// Returns a random item that (if possible) isn't the previously selected item.
        /// </summary>
        public T GetRandomNew()
        {
            int index;
            do
            {
                index = Random.Range(0, _collection.Count - 1);
            }
            while (index == _lastSelected && _collection.Count > 1);

            _lastSelected = index;

            return _collection[index];
        }

        /// <summary>
        /// Returns a random item round-robin style.
        /// </summary>
        public T GetRandomRoundRobin()
        {
            if (_shuffledIndicies.Count == 0)
            {
                RefillShuffledList();
            }

            var value = _collection[_collection.Count - 1];
            _shuffledIndicies.RemoveAt(_shuffledIndicies.Count - 1);
            return value;
        }

        /// <summary>
        /// Updates the internal item collection.
        /// </summary>
        public void UpdateItems(IList<T> collection)
        {
            if (collection != null && collection.Count > 0)
            {
                _collection = collection;
                RefillShuffledList();
            }
            else
            {
                _shuffledIndicies.Clear();
            }
        }

        /// <summary>
        /// Refills the round-robin index source list.
        /// </summary>
        private void RefillShuffledList()
        {
            _shuffledIndicies.Clear();
            _shuffledIndicies.AddRange(Enumerable.Range(0, _collection.Count - 1));
            _shuffledIndicies.Shuffle();
        }

        #endregion
    }
}