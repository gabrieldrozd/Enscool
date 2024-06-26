namespace Common.Utilities.Extensions;

public static class StringExtensions
{
    public static string ToTitleCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        unsafe
        {
            fixed (char* p = input) *p = char.ToUpper(*p);
        }

        return input;
    }
}