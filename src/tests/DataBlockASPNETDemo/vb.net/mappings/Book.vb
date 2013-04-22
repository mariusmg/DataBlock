Imports System
Imports System.Data
Imports voidsoft.DataBlock
Namespace BookManager.Mappings

	<Serializable()> _
	Public Class Book
	Inherits TableMetadata
		Private _fields As DatabaseField()

		Public Sub New()
            _fields = New DatabaseField(5) {}
			_fields(0) = New DatabaseField(DbType.Int32, "BookId", True, True, Nothing)
			_fields(1) = New DatabaseField(DbType.String, "Name", False, False, Nothing)
			_fields(2) = New DatabaseField(DbType.Int32, "Pages", False, False, Nothing)
			_fields(3) = New DatabaseField(DbType.String, "ISBN", False, False, Nothing)
			_fields(4) = New DatabaseField(DbType.String, "Genre", False, False, Nothing)
			_fields(5) = New DatabaseField(DbType.Int16, "Grade", False, False, Nothing)
			Me.currentTableName = "Book"
			Me.listRelations.Add(New ParentTableRelation("BookAuthors", "BookId", TableRelationCardinality.OneToMany, True))
			Me.listRelations.Add(New ManyToManyTableRelation("Author", "BookAuthors", "BookId", "AuthorId"))
		End Sub

		Public Overloads Overrides Property TableFields() As DatabaseField()
			Get
				Return _fields
			End Get
			Set
				_fields = value
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

		Public Property Name() As System.String
			Get
				Return CType((Me.GetField("Name")).fieldValue, System.String)
			End Get
			Set
				Me.SetFieldValue("Name", value)
			End Set
		End Property

		Public Property Pages() As System.Int32
			Get
				Return CType((Me.GetField("Pages")).fieldValue, System.Int32)
			End Get
			Set
				Me.SetFieldValue("Pages", value)
			End Set
		End Property

		Public Property ISBN() As System.String
			Get
				Return CType((Me.GetField("ISBN")).fieldValue, System.String)
			End Get
			Set
				Me.SetFieldValue("ISBN", value)
			End Set
		End Property

		Public Property Genre() As System.String
			Get
				Return CType((Me.GetField("Genre")).fieldValue, System.String)
			End Get
			Set
				Me.SetFieldValue("Genre", value)
			End Set
		End Property

		Public Property Grade() As System.Int16
			Get
				Return CType((Me.GetField("Grade")).fieldValue, System.Int16)
			End Get
			Set
				Me.SetFieldValue("Grade", value)
			End Set
		End Property

		Public Function GetBookAuthors() As Array
			Dim relatedType As Type = Type.GetType("BookManager.Mappings.BookAuthors")
			Return Me.GetRelatedTableData(relatedType)
		End Function

		Public Function GetAuthor() As Array
			Dim relatedType As Type = Type.GetType("BookManager.Mappings.Author")
			Dim intermediaryType As Type = Type.GetType("BookManager.Mappings.BookAuthors")
			Return Me.GetRelatedTableData(relatedType, intermediaryType)
		End Function
	End Class 
End Namespace
