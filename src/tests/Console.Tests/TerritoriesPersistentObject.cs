using System;
using System.Data;
using voidsoft.DataBlock;
namespace ExtenderTerritories
{
	public class TerritoriesPersistentObject : voidsoft.DataBlock.PersistentObject
	{

		public TerritoriesPersistentObject(EDatabase database, string connectionString, TableMetadata mainTable) : base (database, connectionString, mainTable)
		{
		}

		
	}
}