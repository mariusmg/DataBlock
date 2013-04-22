/*
  
       file : Operation.cs
description : Enumeration which describes database operations.
    
  
  (c) 2004 - 2006 Marius Gheorghe. All rights reserved.

*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Represents a list of operations supported by the
	///     PersistentObject
	/// </summary>
	public enum Operation
	{
		/// <summary>
		///     Create operation
		/// </summary>
		Create,

		/// <summary>
		///     Delete operation
		/// </summary>
		Delete,

		/// <summary>
		///     Update operation
		/// </summary>
		Update,

		/// <summary>
		///     Custom business operation
		/// </summary>
		Custom
	}
}