using System;
using System.Runtime.CompilerServices;

namespace voidsoft.DataBlockModeler
{
    public class Utilies
    {
        /// <summary>
        /// Removes the empty spaces.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string RemoveEmptySpaces(string value)
        {
            try
            {
                int index = value.IndexOf(" ");

                while (index != -1)
                {
                    value = value.Remove(index, 1);

                    index = value.IndexOf(" ");
                }

                return value;
            }
            catch
            {
                return value;
            }
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GetEntityName(string name)
        {
            name = RemoveEmptySpaces(name);

            string[] parts = name.Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries);

            return parts[parts.Length - 1];
        }
    }
}