using System;
using System.Text;
using BeardPhantom.UCL.Pooling;

namespace BeardPhantom.UCL.Utility
{
    public struct Flags32
    {
        #region Properties

        public int Data { get; private set; }

        #endregion

        #region Constructors

        public Flags32(int data)
        {
            Data = data;
        }

        #endregion

        #region Methods

        public bool this[int index]
        {
            get
            {
                var mask = 1 << index;
                return (Data & mask) == mask;
            }
            set
            {
                var mask = 1 << index;
                if (value)
                {
                    Data |= mask;
                }
                else
                {
                    Data &= ~mask;
                }
            }
        }

        public Flags32 GetRandomFlag()
        {
            var result = new Flags32(Data);
            var enabledFlagCount = 0;
            for (var i = 0; i < 32; i++)
            {
                if (result[i])
                {
                    enabledFlagCount++;
                }
            }

            int rng;
            using (var randomHandle = PocoPool<Random>.Obtain())
            {
                rng = randomHandle.Object.Next(enabledFlagCount + 1);
            }

            for (var i = 0; i < 32; i++)
            {
                if (result[i])
                {
                    rng--;
                    if (rng <= 0)
                    {
                        result.Data = 1 << i;
                        break;
                    }
                }
            }

            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            using (var stringBuilderHandle = PocoPool<StringBuilder>.Obtain())
            {
                var stringBuilder = stringBuilderHandle.Object;
                for (var i = 31; i >= 0; i--)
                {
                    stringBuilder.Append(
                        this[i]
                            ? '1'
                            : '0');
                }

                return stringBuilder.ToString();
            }
        }

        #endregion
    }
}