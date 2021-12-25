using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReMouse.WPF.Core.CommandLine
{
    public abstract class CommandLineArgument
    {
        public bool Found { get; protected set; }

        protected readonly string[] names;

        protected CommandLineArgument(string[] names)
        {
            this.names = names;
        }

        /// <summary>
        /// If one of any names is found in args, return an array with first name index, second arg index else [-1, -1].
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected int[] Exist(string[] args)
        {
            for (int i = 0; i < names.Length; i++)
                for (int j = 0; j < args.Length; j++)
                    if (names[i] == args[j])
                        return new int[] { i, j };

            return new int[] { -1, -1 };
        }

        public abstract string GetHelp();
    }
}
