using System;
using System.Collections.Generic;

namespace BeardPhantom.UCL.Utility
{
    public static class WeightedChoiceUtility
    {
        #region Fields

        public static readonly Random Rng = new Random();

        #endregion

        #region Methods

        public static T ChooseFromWeighted<T>(this IReadOnlyList<T> choices) where T : IWeightedChoice
        {
            var index = ChooseIndexFromWeighted(choices);
            return index < 0 ? default : choices[index];
        }

        public static int ChooseIndexFromWeighted<T>(this IReadOnlyList<T> choices) where T : IWeightedChoice
        {
            var weightSum = 0;
            for (var i = 0; i < choices.Count; i++)
            {
                weightSum += choices[i].Weight;
            }

            var rng = Rng.Next(0, weightSum);
            for (var i = 0; i < choices.Count; i++)
            {
                var choice = choices[i];
                if (rng < choice.Weight)
                {
                    return i;
                }

                rng -= choice.Weight;
            }

            return -1;
        }

        #endregion
    }
}