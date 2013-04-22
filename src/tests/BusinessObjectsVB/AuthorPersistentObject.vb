Imports System
Imports System.Data
Imports voidsoft.DataBlock
Namespace Extender
 
Public Class AuthorPersistentObject
								Inherits PersistentObject 
 
      Public Sub New (database as DatabaseServer,connectionString as String,mainTable as TableMetadata)
               MyBase.New(database, connectionString, mainTable)
      End Sub
	
      Public Sub New (s as Session, mainTable as TableMetadata)
               MyBase.New(s, mainTable)
      End Sub
      Public Sub New (mainTable as TableMetadata)
               MyBase.New(mainTable)
      End Sub
	
End Class
End Namespace
