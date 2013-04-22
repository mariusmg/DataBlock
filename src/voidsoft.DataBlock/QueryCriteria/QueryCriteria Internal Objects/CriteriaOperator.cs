/*

       file: CriteriaOperator.cs
description: Defines criteria operators.
    
 (c) 2004 - 2006 Marius Gheorghe. All rights reserved.

*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     QueryCriteria operators
	/// </summary>
	public enum CriteriaOperator
	{
		/// <summary>
		///     Order By operator
		/// </summary>
		OrderBy, //Ordery BY

		/// <summary>
		///     DISTINCT operator
		/// </summary>
		Distinct, //DISTINCT

		/// <summary>
		///     BETWEEN operator
		/// </summary>
		Between, //BETWEEN

		/// <summary>
		///     NOT operator
		/// </summary>
		Not, //NOT

		/// <summary>
		///     LIKE operator. This generated the code : Field = %value%
		/// </summary>
		Like, //LIKE

		/// <summary>
		///     Like operator. This generates the code : Field = value%
		/// </summary>
		LikeStart,

		/// <summary>
		///     Like operator. This generates the code : Field = %value
		/// </summary>
		LikeEnd,

		/// <summary>
		///     Equality operator
		/// </summary>
		Equality, // =

		/// <summary>
		///     Different operator
		/// </summary>
		Different, // <>

		/// <summary>
		///     IsNull operator
		/// </summary>
		IsNull,

		/// <summary>
		///     IsnotNull operator
		/// </summary>
		IsNotNull,

		/// <summary>
		///     Or operator
		/// </summary>
		Or, //OR

		/// <summary>
		///     Smaller operator
		/// </summary>
		Smaller, // <

		/// <summary>
		///     SmallerOrEqual operator
		/// </summary>
		SmallerOrEqual, // <=

		/// <summary>
		///     Higher operator
		/// </summary>
		Higher, //>

		/// <summary>
		/// </summary>
		HigherOrEqual, //>=

		/// <summary>
		///     MAX operator
		/// </summary>
		Max, //MAX

		/// <summary>
		///     MIN operator
		/// </summary>
		Min, //MIN

		/// <summary>
		///     Count operator
		/// </summary>
		Count //COUNT
	}
}