/*

       file: DatabaseServer.cs
description: Enum with the RDBMs supported by DataBlock.
    
   (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
 
*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Supported database servers
	/// </summary>
	public enum DatabaseServer
	{
		/// <summary>
		///     Microsoft Access 97 and higher
		/// </summary>
		Access = 1,

		/// <summary>
		///     Microsoft SQL Server 7.0 and higher
		/// </summary>
		SqlServer = 2,

		/// <summary>
		///     MySQL 4.0 or higher
		/// </summary>
		MySQL = 3,

		/// <summary>
		///     PostgreSql 8.0 and higher
		/// </summary>
		PostgreSql = 4,

		/// <summary>
		///     Oracle
		/// </summary>
		Oracle = 5,
	}
}