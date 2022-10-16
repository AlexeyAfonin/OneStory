namespace OneStory.Core.Utils
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsNull(this int i)
        {
            return i.Equals(0);
        }

        public static bool IsNull(this float f)
        {
            return f.Equals(0f);
        }
    }
}
