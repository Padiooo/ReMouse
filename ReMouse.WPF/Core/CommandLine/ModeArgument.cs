using System;

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
                    throw new Exception($"Invalid argument value for argument {names[indexes[0]]}.");
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
