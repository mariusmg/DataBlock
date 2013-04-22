using System;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     QueryCriteria generator utilities
	/// </summary>
	public static class QueryCriteriaGeneratorUtilities
	{
		/// <summary>
		///     Checks for malformed SQL.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public static string CheckForMalformedSql(string query)
		{
			int index = query.IndexOf("FROM", StringComparison.CurrentCultureIgnoreCase);

			if (index == -1)
			{
				return query;
			}

			string newValue = string.Empty;

			--index;
			bool found = false;

			while (index > -1)
			{
				if (query[index] == ' ')
				{
					--index;
					continue;
				}
				else if (query[index] != ' ' && query[index] != ',')
				{
					break;
				}

				else if (query[index] == ',')
				{
					newValue = query.Remove(index, 1);
					break;
				}
			}

			return newValue != string.Empty ? newValue : query;
		}
	}
}