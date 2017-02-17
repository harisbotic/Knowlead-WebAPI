using System;

namespace Knowlead.Common
{
    public static class Utils
    {
        public static int LimitToRange(this int @value, int min, int max)
        {
            if (@value < min) { return min; }
            if (@value > max) { return max; }
            return @value;
        }

        public static Tuple<Guid, Guid> GetBiggerSmallerGuidTuple(Guid guidOne, Guid guidTwo)
        {
            var biggerGuid = (guidOne.CompareTo(guidTwo) >= 0)? guidOne : guidTwo;
            var smallerGuid = (guidOne.CompareTo(guidTwo) <= 0)? guidOne : guidTwo;

            return new Tuple<Guid,Guid> (biggerGuid, smallerGuid);
        }

        public static String GenerateChatMessagePartitionKey(Guid guidOne, Guid guidTwo)
        { 
            var bsTuple = GetBiggerSmallerGuidTuple(guidOne, guidTwo);
            return $"{bsTuple.Item1}{bsTuple.Item2}";
        }
    }
}