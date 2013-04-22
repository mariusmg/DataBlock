/*

        file : JoinCriteriaCondition.cs
 description : struct used to create a join criteria condition.

 (c) 2004 - 2006 Marius Gheorghe. All rights reserved.  
  
*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Represents a Join Condition which is added to a QueryCriteria
	/// </summary>
	internal struct JoinCriteriaCondition
	{
		/// <summary>
		///     The QueryCriteria with which we join
		/// </summary>
		public QueryCriteria Criteria;

		/// <summary>
		///     The foreign key used in join
		/// </summary>
		public DatabaseField ForeignKey;

		/// <summary>
		///     Name of the table to which the foreign key belongs
		/// </summary>
		public string ForeignKeyFieldTableName;

		/// <summary>
		///     The Join type
		/// </summary>
		public JoinType Join;

		/// <summary>
		///     The primary key used in the join
		/// </summary>
		public DatabaseField PrimaryKey;

		/// <summary>
		///     Name of the table to which the primary key field belongs
		/// </summary>
		public string PrimaryKeyFieldTableName;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="joinType">The Join type</param>
		/// <param name="primaryKeyFieldTableName"> Name of the table to which the primary key field belongs</param>
		/// <param name="primaryKey">The primary key used in the join</param>
		/// <param name="foreignKeyFieldTableName">Name of the table to which the foreign key belongs</param>
		/// <param name="foreignKey">The foreign key used in join</param>
		/// <param name="criteria">The QueryCriteria with which we join</param>
		public JoinCriteriaCondition(JoinType joinType, string primaryKeyFieldTableName, DatabaseField primaryKey, string foreignKeyFieldTableName,
		                             DatabaseField foreignKey, QueryCriteria criteria)
		{
			Join = joinType;
			PrimaryKeyFieldTableName = primaryKeyFieldTableName;
			ForeignKeyFieldTableName = foreignKeyFieldTableName;
			PrimaryKey = primaryKey;
			ForeignKey = foreignKey;
			Criteria = criteria;
		}
	}
}