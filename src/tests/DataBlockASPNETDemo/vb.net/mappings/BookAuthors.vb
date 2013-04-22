Imports System
Imports System.Data
Imports voidsoft.DataBlock
Namespace BookManager.Mappings

	<Serializable()> _
	Public Class BookAuthors
	Inherits TableMetadata
		Private _fields As DatabaseField()

		Public Sub New()
            _fields = New DatabaseField(2) {}
			_fields(0) = New DatabaseField(DbType.Int32, "Id", True, True, Nothing)
			_fields(1) = New DatabaseField(DbType.Int32, "AuthorId", False, False, Nothing)
			_fields(2) = New DatabaseField(DbType.Int32, "BookId", False, False, Nothing)
			Me.currentTableName = "BookAuthors"
		End Sub

		Public Overloads Overrides Property TableFields() As DatabaseField()
			Get
				Return _fields
			End Get
			Set
				_fields = value
			End Set
		End Property

		Public Property Id() As System.Int32
			Get
				Return CType((Me.GetField("Id")).fieldValue, System.Int32)
			End Get
			Set
				Me.SetFieldValue("Id", value)
			End Set
		End Property

		Public Property AuthorId() As System.Int32
			Get
				Return CType((Me.GetField("AuthorId")).fieldValue, System.Int32)
			End Get
			Set
				Me.SetFieldValue("AuthorId", value)
			End Set
		End Property

		Public Property BookId() As System.Int32
			Get
				Return CType((Me.GetField("BookId")).fieldValue, System.Int32)
			End Get
			Set
				Me.SetFieldValue("BookId", value)
			End Set
		End Property
	End Class 
End Namespace
