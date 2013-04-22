using System;
using System.Data;
using voidsoft.DataBlock;
namespace ExtenderCustomers
{
	public class CategoryPersistentObject : voidsoft.DataBlock.PersistentObject
	{

		public CategoryPersistentObject(EDatabase database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable)
		{

		}


//		/// <returns></returns>
//		public virtual CategoryTableMetadata[] GetTableMetadata()
//		{
//			CategoryTableMetadata[] tables = null;
//
//			DataSet dsTemp = null;
//
//			try
//			{
//				dsTemp = new DataSet();
//
//				string selectQuery = SqlGenerator.GenerateSelectQuery(this.database, this.mainTable, false);
//				dsTemp = DataAccessLayer.ExecuteDataSet(this.database, this.connectionString, selectQuery);
//
//				tables = new CategoryTableMetadata[dsTemp.Tables[0].Rows.Count];
//
//
//				for (int i = 0; i < tables.Length; i++)
//				{
//					tables[i] = new CategoryTableMetadata();
//					tables[i].TableName = this.mainTable.TableName;
//					tables[i].TableFields = (DatabaseField[])this.mainTable.TableFields.Clone();
//				}
//
//				for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
//				{
//					this.MapDataReaderToTableMetadata(i, dsTemp.Tables[0], tables[i]);
//				}
//
//				return tables;
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}
//			finally
//			{
//				if (dsTemp != null)
//				{
//					dsTemp.Dispose();
//				}
//			}
//		}
//


	}
}
