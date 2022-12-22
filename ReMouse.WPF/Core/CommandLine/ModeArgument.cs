using System;
using System.Linq;

namespace ReMouse.WPF.Core.CommandLine
{
    public abstract class ModeArgument<T> : CommandLineArgument where T : struct, Enum
    {
        public T Option { get; }

        protected ModeArgument(string[] args, string[] names)
            : base(names)
        {
            int[] indexes = Exist(args);
            Found = indexes[1] >= 0;

            if (Found)
            {
                string option = args[indexes[1] + 1];
                try
                {
                    Option = Enum.Parse<T>(option.ToUpper());
                }
                catch (Exception)
                {
                    string error = $"Invalid argument value ({args[indexes[1] + 1]}) for argument {names[indexes[0]]}.";
                    var values = string.Join(", ", Enum.GetValues<T>().Select(v => v.ToString().ToLowerInvariant()));
                    string tooltip = $"Expected: {values}.";
                    throw new ArgumentException(string.Format("{0}\n{1}", error, tooltip));
                }
            }
        }

        public override string GetHelp()
        {
            string help = "";

            for (int i = 0; i < names.Length - 1; i++)
                help += names[i] + ", ";
            help += names[^1] + " <value> \t value:[";

            T[] options = Enum.GetValues<T>();
            for (int i = 0; i < options.Length - 1; i++)
                help += options[i].ToString().ToLower() + ", ";
            help += options[^1].ToString().ToLower() + "]";

            return help;
        }
    }
}
