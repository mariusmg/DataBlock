using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace voidsoft.DataBlock
{
	internal class MetadataManager
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static List<ChildTableRelation> GetChildRelations(TableMetadata entity)
		{
			List<ChildTableRelation> list = new List<ChildTableRelation>();

			foreach (TableRelation relation in entity.Relations)
			{
				if (relation is ChildTableRelation)
				{
					list.Add((ChildTableRelation) relation);
				}
			}

			return list;
		}

		/// <summary>
		///     Returns a list with the relations in which the specified table is Parent.
		/// </summary>
		/// <param name="mainTable">The table from which we return the relations</param>
		/// <returns>A list with the Parent table relations</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static List<ParentTableRelation> GetParentRelations(TableMetadata mainTable)
		{
			List<ParentTableRelation> parentRelations = new List<ParentTableRelation>();

			TableRelation[] relations = mainTable.Relations;
			parentRelations = new List<ParentTableRelation>();

			for (int i = 0; i < relations.Length; i++)
			{
				if (relations[i] is ParentTableRelation)
				{
					parentRelations.Add((ParentTableRelation) relations[i]);
				}
			}

			return parentRelations;
		}

		/// <summary>
		///     Returns the foreignKey DatabaseField from the child table based
		///     on the relations between 2 tables
		/// </summary>
		/// <param name="parentEntity">ParentTable which contains the list of relations.</param>
		/// <param name="childEntity">Child table from which the DatabaseField is returned</param>
		/// <returns>DatabaseField foreign key</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static DatabaseField GetForeignKeyField(TableMetadata parentEntity, TableMetadata childEntity)
		{
			TableRelation[] relations = parentEntity.Relations;

			foreach (TableRelation relation in relations)
			{
				if (relation is ManyToManyTableRelation)
				{
					continue;
				}

				if (relation.RelatedTableName == childEntity.TableName)
				{
					return childEntity.GetField(((ParentTableRelation) relation).ForeignKeyName);
				}
			}

			throw new ArgumentException("No relation found between the specified tables");
		}

		/// <summary>
		///     Returns the name of the foreign key from the child table based on the relation between the 2 tables.
		/// </summary>
		/// <param name="parentTable">Parent TableMetadata</param>
		/// <param name="childTable">Child TableMetadata</param>
		/// <returns>Name of the foreign key field</returns>
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static string GetForeignKeyName(TableMetadata parentTable, TableMetadata childTable)
		{
			TableRelation[] relations = parentTable.Relations;

			foreach (TableRelation relation in relations)
			{
				if (relation is ManyToManyTableRelation)
				{
					continue;
				}

				if (relation.RelatedTableName == childTable.TableName)
				{
					if (relation is ParentTableRelation)
					{
						return ((ParentTableRelation) relation).ForeignKeyName;
					}
					else
					{
						return ((ChildTableRelation) relation).ForeignKeyName;
					}
				}
			}

			throw new ArgumentException("No relation found between the specified tables");
		}
	}
}