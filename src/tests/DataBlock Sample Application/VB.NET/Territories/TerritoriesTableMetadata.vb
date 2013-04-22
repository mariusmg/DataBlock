


Imports System
Imports System.Data
Imports voidsoft.DataBlock






Namespace ExtenderTerritories

    Public Class TerritoriesTableMetadata
        Inherits TableMetadata
        Private _fields As DatabaseField()

        Public Sub New()
            _fields = New DatabaseField(2) {}
            _fields(0) = New DatabaseField(DbType.String, "TerritoryID", True, False, Nothing)
            _fields(1) = New DatabaseField(DbType.String, "TerritoryDescription", False, False, Nothing)
            _fields(2) = New DatabaseField(DbType.Int32, "RegionID", False, False, Nothing)
            Me.TableName = "Territories"
        End Sub

        Public Overloads Overrides Property TableFields() As DatabaseField()
            Get
                Return _fields
            End Get
            Set(ByVal Value As DatabaseField())
                _fields = Value
            End Set
        End Property

        Public Property TerritoryID() As System.String
            Get
                Return CType((Me.GetField("TerritoryID")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("TerritoryID", Value)
            End Set
        End Property

        Public Property TerritoryDescription() As System.String
            Get
                Return CType((Me.GetField("TerritoryDescription")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("TerritoryDescription", Value)
            End Set
        End Property

        Public Property RegionID() As System.Int32
            Get
                Return CType((Me.GetField("RegionID")).fieldValue, System.Int32)
            End Get
            Set(ByVal Value As System.Int32)
                Me.SetFieldValue("RegionID", Value)
            End Set
        End Property
    End Class
End Namespace
