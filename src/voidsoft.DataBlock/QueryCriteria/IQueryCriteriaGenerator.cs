/*
    
      file: QueryCriteriaGenerator.cs
description: The interface defines operation(s) supported by the QueryCriteria implementors.   

  
   (c) 2004 - 2006 Marius Gheorghe. All rights reserved.

*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Interface which defines the operations supported by the
	///     QueryCriteriaGenerators
	/// </summary>
	public interface IQueryCriteriaGenerator
	{
		/// <summary>
		///     Generates a SELECT sql query based on the specified QueryCriteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which the query is generated</param>
		/// <returns>The resulting ExecutionQuery</returns>
		ExecutionQuery GenerateSelect(QueryCriteria criteria);

		/// <summary>
		///     Generates a UPDATE query based on the specified QueryCriteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which the query is generated</param>
		/// <returns>The resulting ExecutionQuery</returns>
		ExecutionQuery GenerateUpdate(QueryCriteria criteria);

		/// <summary>
		///     Generates a DELETE sql query based on the specified QueryCriteria
		/// </summary>
		/// <param name="criteria">QueryCriteria based upon which the query is generated</param>
		/// <returns>The resulting ExecutionQuery</returns>
		ExecutionQuery GenerateDelete(QueryCriteria criteria);
	}
}