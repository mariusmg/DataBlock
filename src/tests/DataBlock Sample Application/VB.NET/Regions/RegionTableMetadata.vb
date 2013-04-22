Imports System
Imports System.Data
Imports voidsoft.DataBlock
Namespace ExtenderRegion

    Public Class RegionTableMetadata
        Inherits TableMetadata
        Private _fields As DatabaseField()

        Public Sub New()
            _fields = New DatabaseField(1) {}
            _fields(0) = New DatabaseField(DbType.Int32, "RegionID", True, False, Nothing)
            _fields(1) = New DatabaseField(DbType.String, "RegionDescription", False, False, Nothing)
            Me.tableName = "Region"
        End Sub

        Public Overloads Overrides Property TableFields() As DatabaseField()
            Get
                Return _fields
            End Get
            Set(ByVal Value As DatabaseField())
                _fields = value
            End Set
        End Property

        Public Property RegionID() As System.Int32
            Get
                Return CType((Me.GetField("RegionID")).fieldValue, System.Int32)
            End Get
            Set(ByVal Value As System.Int32)
                Me.SetFieldValue("RegionID", value)
            End Set
        End Property

        Public Property RegionDescription() As System.String
            Get
                Return CType((Me.GetField("RegionDescription")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("RegionDescription", value)
            End Set
        End Property
    End Class
End Namespace
