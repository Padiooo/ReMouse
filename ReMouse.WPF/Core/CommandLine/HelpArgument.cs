
namespace ReMouse.WPF.Core.CommandLine
{
    public class HelpArgument : CommandLineArgument
    {
        public HelpArgument(string[] args)
            : base(new string[] { "--help", "-help", "-h", "-?" })
        {
            Found = Exist(args)[1] >= 0;
        }

        public override string GetHelp()
        {
            string help = "";

            for (int i = 0; i < names.Length - 1; i++)
                help += names[i] + ", ";
            help += names[^1] + "\t display this message";

            return help;
        }
    }
}
