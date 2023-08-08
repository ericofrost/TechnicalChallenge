using System.Reflection;
using CLI.Services.Constants;

namespace CLI.Services.Extentions
{
    public static class CommandExtensions
    {
        /// <summary>
        /// Returns the command line argument action command
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Command(this string[] args)
        {
            var type = typeof(Commands);

            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.GetValue(Activator.CreateInstance(type)).Equals(args[0]))?.Name;
        }
    }
}
