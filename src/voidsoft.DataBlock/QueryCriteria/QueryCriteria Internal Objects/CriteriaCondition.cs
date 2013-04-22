/*

      file : CriteriaConditions.cs
description: Defines criteria conditions
    
 (c) 2004 - 2006 Marius Gheorghe. All rights reserved.

*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Represents a condition which is added to a QueryCriteria
	/// </summary>
	internal struct CriteriaCondition
	{
		/// <summary>
		///     Condition's criteria operator
		/// </summary>
		public CriteriaOperator CriteriaOperator;

		/// <summary>
		///     Condition's Field
		/// </summary>
		public DatabaseField Field;

		/// <summary>
		///     Condition's values
		/// </summary>
		public object[] Values;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="criteriaOperator">Condition's criteria operator</param>
		/// <param name="field">Condition's Field</param>
		/// <param name="values">Condition's values</param>
		public CriteriaCondition(CriteriaOperator criteriaOperator, DatabaseField field, params object[] values)
		{
			CriteriaOperator = criteriaOperator;
			Field = field;
			Values = values;
		}
	}
}