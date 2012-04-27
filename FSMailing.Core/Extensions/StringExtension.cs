using System;

namespace FSMailing.Core.Extensions
{
    public static class StringExtension
    {
        public static string FormatWith(this string s, params object[] p)
        {
            return String.Format(s, p);
        }
    }
}
