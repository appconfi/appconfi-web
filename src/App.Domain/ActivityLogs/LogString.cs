using System.Collections.Generic;
using System.Linq;

namespace App.Domain.ActivityLogs
{
    public static class LogString
    {
        public static string WithName(string resource, string status)
        {
            return $"{resource} {status}";
        }

        public static string WithDescription(IDictionary<string, string> args)
        {
            if (args.Count == 0)
                return string.Empty;
            return args.Aggregate("", (a, b) => $"{a}, {b.Key}:{b.Value}").Substring(2);
        }
    }
}
