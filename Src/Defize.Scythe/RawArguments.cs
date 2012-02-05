namespace Defize.Scythe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class RawArguments
    {
        private static readonly Regex FlagMatcher = new Regex("^/(?<name>[a-zA-Z][a-zA-Z0-9]*)$");
        private static readonly Regex NamedArgumentMatcher = new Regex("^/(?<name>[a-zA-Z][a-zA-Z0-9]*)=(?<value>.*)$");

        private readonly ISet<string> _flags = new HashSet<string>();
        private readonly IDictionary<string, string> _namedArguments = new Dictionary<string, string>();
        private readonly IList<string> _positionalArguments = new List<string>();

        private RawArguments()
        { }

        public static RawArguments Parse(string[] arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException();
            }

            return ParseCore(arguments);
        }

        public bool HasFlag(string name)
        {
            return _flags.Contains(name);
        }

        public bool TryGetNamedArgument(string name, out string value)
        {
            return _namedArguments.TryGetValue(name, out value);
        }

        public bool TryGetPositionalArgument(int position, out string value)
        {
            if (position < 1 || position > _positionalArguments.Count)
            {
                value = null;
                return false;
            }

            value = _positionalArguments[position];
            return true;
        }

        private static RawArguments ParseCore(string[] arguments)
        {
            var raw = new RawArguments();

            foreach (var argument in arguments)
            {
                ParseArgument(argument, raw);
            }

            return raw;
        }

        private static void ParseArgument(string argument,RawArguments raw)
        {
            var flagMatch = FlagMatcher.Match(argument);
            if (flagMatch.Success)
            {
                var name = flagMatch.Groups["name"].Value;

                if (raw._flags.Add(name))
                {
                    return;
                }

                throw new ArgumentException();
            }

            var namedArgumentMatch = NamedArgumentMatcher.Match(argument);
            if (namedArgumentMatch.Success)
            {
                var name = namedArgumentMatch.Groups["name"].Value;
                var value = namedArgumentMatch.Groups["value"].Value;

                if (raw._namedArguments.ContainsKey(name))
                {
                    throw new ArgumentException();
                }

                raw._namedArguments.Add(name, value);

                return;
            }

            raw._positionalArguments.Add(argument);
        }
    }
}
