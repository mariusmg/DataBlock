/*

	   file : ISqlGenerators.cs

description : The IsqlGenerator interface defines operation used by the QueryGenerator.
              This interface operations are implemented by the specific database implementation.  

	 author : Marius Gheorghe.


*/


using System;
using System.Data;


namespace voidsoft.DataBlock
{

	/// <summary>
	/// Interface implemented by the database specific SQL generators
	/// </summary>
	internal interface ISqlGenerator
	{
		/// <summary>
		/// Returns the value of the specified dataType.
		/// </summary>
		/// <param name="dataType"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		string GetValue(DbType dataType, object value);

		/// <summary>
		/// Returns the value of the specified dataType along
		/// with the attribution operator.
		/// </summary>
		/// <param name="dataType"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		string GetValueWithAttributionOperator(DbType dataType, object value);

		/// <summary>
		/// Returns the value of the specified dataType
		/// along with the comparation operator.
		/// </summary>
		/// <param name="dataType"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		string GetValueWithComparationOperator(DbType dataType, object value);


	}


}
