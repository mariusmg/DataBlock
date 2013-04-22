/*

       file : ParentTableRelation.cs
description :  Describes a Parent - Child table relation
    
  
  (c) 2004 - 2006 Marius Gheorghe. All rights reserved. 
*/

using System;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     This is a Parent -> Child relationship. Note that the type of the fields on which
	///     the 2 tables are related must be the same so the type of the foreign key is inferred
	///     from the Primary Key of the parent table.
	/// </summary>
	[Serializable]
	public class ParentTableRelation : TableRelation
	{
		private bool cascadeDeleteChildren;
		private string foreignKeyName;
		private string relatedTableName;
		private TableRelationCardinality relationCardinality;

		/// <summary>
		///     Constructor. Creates a new instance of ChildTableRealtion
		/// </summary>
		public ParentTableRelation(string relatedTableName, string foreignKeyName, TableRelationCardinality relationCardinality, bool cascadeDeleteChildren)
		{
			this.relatedTableName = relatedTableName;
			this.foreignKeyName = foreignKeyName;
			this.relationCardinality = relationCardinality;
			this.cascadeDeleteChildren = cascadeDeleteChildren;
		}

		/// <summary>
		///     The name of the related table.
		/// </summary>
		public override string RelatedTableName
		{
			get
			{
				return relatedTableName;
			}
		}

		/// <summary>
		///     The cardinality with the related table.
		/// </summary>
		public override TableRelationCardinality RelationCardinality
		{
			get
			{
				return relationCardinality;
			}
		}

		/// <summary>
		///     The name of the foreign key.
		/// </summary>
		public string ForeignKeyName
		{
			get
			{
				return foreignKeyName;
			}
		}

		/// <summary>
		///     Flag to know if the data from the related tables is deleted when
		///     we delete data from the main table.
		/// </summary>
		public bool CascadeDeleteChildren
		{
			get
			{
				return cascadeDeleteChildren;
			}
		}
	}
}