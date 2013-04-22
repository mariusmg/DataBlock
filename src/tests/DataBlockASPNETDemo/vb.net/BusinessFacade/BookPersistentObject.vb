Imports System
Imports System.Data
Imports voidsoft.DataBlock
Namespace BookManager.BusinessFacade

	Public Class BookPersistentObject
	Inherits voidsoft.DataBlock.PersistentObject

		Public Sub New(ByVal database As DatabaseServer, ByVal connectionString As String, ByVal mainTable As TableMetadata)
			MyBase.New(database, connectionString, mainTable)
		End Sub

		Public Sub New(ByVal session As Session, ByVal mainTable As TableMetadata)
			MyBase.New(session, mainTable)
		End Sub

		Public Sub New(ByVal mainTable As TableMetadata)
			MyBase.New(mainTable)
		End Sub
	End Class 
End Namespace
