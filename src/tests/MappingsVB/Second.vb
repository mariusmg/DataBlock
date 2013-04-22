Imports System
Imports System.Data
Imports voidsoft.DataBlock

Namespace Extender
 


		Public Class Second
		             Inherits TableMetadata
			Private _fields() as DatabaseField

			Sub New() 
			
					_fields = new DatabaseField(4) {}
 _fields(0) = new DatabaseField(DbType.Int32,"Id",True,True,Nothing)
 _fields(1) = new DatabaseField(DbType.Int32,"FirstId",False,False,Nothing)
 _fields(2) = new DatabaseField(DbType.Int32,"Age",False,False,Nothing)
 _fields(3) = new DatabaseField(DbType.Int32,"Quantity",False,False,Nothing)
 
me.currentTableName = "Second"

Me.listRelations.Add(new ParentTableRelation("Third", "SecondId",TableRelationCardinality.OneToMany,False))
Me.listRelations.Add(new ChildTableRelation("First",TableRelationCardinality.OneToOne,"Id","FirstId"))

End Sub



			Public Overrides Property TableFields() as DatabaseField() 
		   	   Get
				  Return _fields
			   End Get
			   Set (ByVal Value as DatabaseField())
			      _fields = value
			   End Set
			End Property

     Public Property Id As System.Int32
        Get
           Return  CType((Me.GetField("Id")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue(Id, Value)
        End Set
      End Property

     Public Property FirstId As System.Int32
        Get
           Return  CType((Me.GetField("FirstId")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue(FirstId, Value)
        End Set
      End Property

     Public Property Age As System.Int32
        Get
           Return  CType((Me.GetField("Age")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue(Age, Value)
        End Set
      End Property

     Public Property Quantity As System.Int32
        Get
           Return  CType((Me.GetField("Quantity")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue(Quantity, Value)
        End Set
      End Property
Public Function GetThird() As Array
    Dim relatedType As Type = Type.GetType("Extender.Third")
    Return Me.GetRelatedTableData(relatedType)
End Function
Public Function GetFirst() As TableMetadata
    Dim relatedType As Type = Type.GetType("Extender.First")
    Dim result() As TableMetadata = Me.GetRelatedTableData(relatedType)
   If result.Length > 0 Then
      Return result(0)
   Else
      Return Nothing
   End If
End Function
End Class
End Namespace
