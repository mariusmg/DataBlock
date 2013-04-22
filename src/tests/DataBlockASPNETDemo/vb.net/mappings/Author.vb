Imports System
Imports System.Data
Imports voidsoft.DataBlock
Namespace BookManager.Mappings

	<Serializable()> _
	Public Class Author
	Inherits TableMetadata
		Private _fields As DatabaseField()

		Public Sub New()
            _fields = New DatabaseField(3) {}
			_fields(0) = New DatabaseField(DbType.Int32, "AuthorId", True, True, Nothing)
			_fields(1) = New DatabaseField(DbType.String, "Name", False, False, Nothing)
			_fields(2) = New DatabaseField(DbType.Int32, "Age", False, False, Nothing)
			_fields(3) = New DatabaseField(DbType.String, "Location", False, False, Nothing)
			Me.currentTableName = "Author"
			Me.listRelations.Add(New ParentTableRelation("BookAuthors", "AuthorId", TableRelationCardinality.OneToMany, True))
			Me.listRelations.Add(New ManyToManyTableRelation("Book", "BookAuthors", "AuthorId", "BookId"))
		End Sub

		Public Overloads Overrides Property TableFields() As DatabaseField()
			Get
				Return _fields
			End Get
			Set
				_fields = value
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

		Public Property Name() As System.String
			Get
				Return CType((Me.GetField("Name")).fieldValue, System.String)
			End Get
			Set
				Me.SetFieldValue("Name", value)
			End Set
		End Property

		Public Property Age() As System.Int32
			Get
				Return CType((Me.GetField("Age")).fieldValue, System.Int32)
			End Get
			Set
				Me.SetFieldValue("Age", value)
			End Set
		End Property

		Public Property Location() As System.String
			Get
				Return CType((Me.GetField("Location")).fieldValue, System.String)
			End Get
			Set
				Me.SetFieldValue("Location", value)
			End Set
		End Property

		Public Function GetBookAuthors() As Array
			Dim relatedType As Type = Type.GetType("BookManager.Mappings.BookAuthors")
			Return Me.GetRelatedTableData(relatedType)
		End Function

		Public Function GetBook() As Array
			Dim relatedType As Type = Type.GetType("BookManager.Mappings.Book")
			Dim intermediaryType As Type = Type.GetType("BookManager.Mappings.BookAuthors")
			Return Me.GetRelatedTableData(relatedType, intermediaryType)
		End Function
	End Class 
End Namespace
