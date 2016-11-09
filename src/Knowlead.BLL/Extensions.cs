namespace Knowlead.BLL
{
    public static class Extensions
    {
        public static int LimitToRange(this int @value, int min, int max)
        {
            if (@value < min) { return min; }
            if (@value > max) { return max; }
            return @value;
        }
    }
}