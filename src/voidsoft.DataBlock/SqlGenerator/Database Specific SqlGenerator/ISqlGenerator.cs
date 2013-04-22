/*

	   file : ISqlGenerators.cs
description : The IsqlGenerator interface defines operation used by the QueryGenerator.
              This interface operations are implemented by the specific database implementation.  
	
   (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
 
  
*/

using System.Data;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Interface which defines operation that must be implemented by the
	///     database specific SQL generators
	/// </summary>
	internal interface ISqlGenerator
	{
		/// <summary>
		///     Returns the value of the specified dataType.
		/// </summary>
		/// <param name="dataType"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		string GetValue(DbType dataType, object value);

		/// <summary>
		///     Gets the value from the specified IDataParameter
		/// </summary>
		/// <param name="param"></param>
		/// <returns></returns>
		string GetValue(IDataParameter param);

		/// <summary>
		///     Returns the value of the specified dataType along
		///     with the attribution operator.
		/// </summary>
		/// <param name="dataType"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		string GetValueWithAttributionOperator(DbType dataType, object value);

		/// <summary>
		///     Gets the value of the specified IDataParameter along with the
		///     attribution operator
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		string GetValueWithAttributionOperator(IDataParameter parameter);

		/// <summary>
		///     Gets the value of the specified IDataParameter along with the
		///     comparasion operator
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		string GetValueWithComparationOperator(IDataParameter parameter);

		/// <summary>
		///     Returns the value of the specified dataType along with the
		///     comparation operator.
		/// </summary>
		/// <param name="dataType"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		string GetValueWithComparationOperator(DbType dataType, object value);

		/// <summary>
		///     Generates a query using paging
		/// </summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="numberOfItems">The number of items.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <returns></returns>
		string GeneratePaginatedQuery(TableMetadata metadata, int numberOfItems, int pageNumber);
	}
}