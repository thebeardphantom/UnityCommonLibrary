using System;
using UnityEngine;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// A reference to a System.Type
    /// </summary>
    [Serializable]
    public class TypeReference
    {
        #region Fields

        [SerializeField]
        private string _typeString;

        private Type _type;

        #endregion

        #region Properties

        public Type Type
        {
            get
            {
                if (_type == null)
                {
                    _type = Type.GetType(_typeString);
                }

                return _type;
            }
        }

        #endregion
    }
}