/*

	   file: DataBlockException.cs
description: Top level exception which should be caught from the client code when
             doing DataBlock persistence operation(s).   

     (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
*/

using System;

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Exception thrown by the persistent object operations
	/// </summary>
	[Serializable]
	public class DataBlockException : Exception
	{
		/// <summary>
		///     PersistentObjectException constructor
		/// </summary>
		public DataBlockException()
		{
		}

		/// <summary>
		///     PersistentObjectException constructor
		/// </summary>
		/// <param name="message"></param>
		public DataBlockException(string message) : base(message)
		{
		}

		/// <summary>
		///     PersistentObjectException constructor
		/// </summary>
		/// <param name="message"></param>
		/// <param name="ex"></param>
		public DataBlockException(string message, Exception ex) : base(message, ex)
		{
		}
	}
}