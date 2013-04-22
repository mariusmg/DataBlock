Imports System
Imports System.Data
Imports voidsoft.DataBlock
Namespace ExtenderRegion

    Public Class RegionPersistentObject
        Inherits voidsoft.DataBlock.PersistentObject

        Public Sub New(ByVal database As EDatabase, ByVal connectionString As String, ByVal mainTable As TableMetadata)
            MyBase.New(database, connectionString, mainTable)
        End Sub

        Public Sub New(ByVal session As Session, ByVal mainTable As TableMetadata)
            MyBase.New(session, mainTable)
        End Sub
    End Class
End Namespace
