/*
  
      file : QueryCriteriaGeneratorType.cs
description: Enumeration which describes the query generator types for QueryCriteria. 
    
    (c) 2004 - 2006 Marius Gheorghe. All rights reserved.
 
*/

namespace voidsoft.DataBlock
{
	/// <summary>
	///     Supported query generator types for QueryCriteria
	/// </summary>
	internal enum QueryCriteriaGeneratorType
	{
		Select,
		Delete,
		Update
	}
}