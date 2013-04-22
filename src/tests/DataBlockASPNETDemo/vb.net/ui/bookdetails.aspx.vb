 Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
 
Imports System.Collections.Specialized
 
Imports voidsoft.DataBlock
Imports BookManager.Mappings
Imports BookManager.BusinessFacade
 
 
Public partial Class bookdetails
	 Inherits System.Web.UI.Page
 
    'private fields
    Private isNew As Boolean =  False 
    Private bookId As Integer =  -1 
    Private editableBook As Book =  Nothing 
 
 
 
 

 
    Protected  Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
 
		 if me.IsPostBack Then
		 
            'get the query strings
            Dim nc As NameValueCollection =  Me.Request.QueryString 
 
            Dim keys() As String =  nc.AllKeys 
            Dim values() As String =  nc.GetValues(0) 
 
 
            'get the current action
            If keys(0).ToLower() = "edit" Then
                Me.bookId = Convert.ToInt32(values(0))
                isNew = False
 
                'also get the current book
 
                 Dim books() As Book = CType(Me.Application("BooksList"), Book())
 
                 Dim var As Book
                 For Each var In books
                     If var.BookId = Me.bookId Then
                         Me.editableBook = var
                         Exit For
                     End If
                 Next
 
            Else If keys(0).ToLower() = "new" Then 
                isNew = True
            End If
 
     else
    
			Dim nc As NameValueCollection =  Me.Request.QueryString 
 
            Dim keys() As String =  nc.AllKeys 
 
 
            'get the current action
            If keys(0).ToLower() = "edit" Then
                Dim args() As String =  nc.GetValues(0) 
                Me.textBoxName.ReadOnly = False
                Me.textBoxPage.ReadOnly = False
                Me.listCheckboxes.Enabled = True
                Me.LoadAuthors()
 
                Me.DisplayBook(Convert.ToInt32(args(0)))
 
            Else If keys(0).ToLower() = "view" Then 
                Me.LoadAuthors()
                Dim args() As String =  nc.GetValues(0) 
                Me.DisplayBook(Convert.ToInt32(args(0)))
                Me.textBoxName.ReadOnly = True
                Me.textBoxPage.ReadOnly = True
                Me.buttonSubmit.Visible = False
                Me.listCheckboxes.Enabled = False
 
            Else If keys(0).ToLower() = "new" Then 
                Me.labelAction.Text = "Add new book"
                Me.textBoxName.ReadOnly = False
                Me.textBoxPage.ReadOnly = False
                Me.listCheckboxes.Enabled = True
                Me.LoadAuthors()
            End If
    
    
    end if
    
 
    End Sub

    Protected  Sub buttonSubmit_Click(ByVal sender As Object, ByVal e As EventArgs)
        'validate the entered data
        If Me.ValidateData() Then
            If Me.isNew Then
 
                'create new book
 
                Dim NewBook As Book =  New Book() 
                NewBook.Name = Me.textBoxName.Text.Trim()
 
                If Me.textBoxPage.Text.Trim() <> String.Empty Then
                    NewBook.Pages = Convert.ToInt32(Me.textBoxPage.Text.Trim())
                Else 
                    NewBook.SetNullValue("Pages")
                End If
 
 
                If Me.textBoxISBN.Text.Trim() <> String.Empty Then
                    NewBook.ISBN = Me.textBoxISBN.Text
                Else 
                    NewBook.SetNullValue("ISBN")
                End If
 
 
                If Me.textBoxGenre.Text.Trim() <> String.Empty Then
                    NewBook.Genre = Me.textBoxGenre.Text
                Else 
                    NewBook.SetNullValue("Genre")
                End If
 
 
                If Me.textBoxGrade.Text.Trim() <> String.Empty Then
                    NewBook.Grade = Int16.Parse(Me.textBoxGrade.Text)
                Else 
                    NewBook.SetNullValue("Grade")
                End If
 
 
 
 
                'add the authors
 
                Dim listId As New List(Of Int32)()
 
 
                Dim var As ListItem
                For Each var In Me.listCheckboxes.Items
 
                    If var.Selected Then
                        listId.Add(Int32.Parse(var.Value))
                    End If
                Next
 
                'add the book authors
                Dim bk() As BookAuthors = New BookAuthors(listId.Count - 1) {}
 
                'set the author id
                Dim i As Integer
                For  i = 0 To  bk.Length- 1  Step  i + 1
                    bk(i) = New BookAuthors()
                    bk(i).AuthorId = listId(i)
                Next
 
                'attach the book authors to our 
                Dim bauth As BookAuthors
                For Each bauth In bk
                    NewBook.AttachTableMetadata(bauth)
                Next
 
                'create the book
                Dim perst As BookPersistentObject =  New BookPersistentObject(NewBook) 
                perst.Create(NewBook)
 
                'redirect to the main
                Me.Response.Redirect("books.aspx")
            Else 
                'update
                Dim bk As Book =  New Book() 
                Dim perst As BookPersistentObject =  New BookPersistentObject(bk) 
                Dim editBook As Book = CType(perst.GetTableMetadata(Me.bookId), Book)
 
                editBook.Name = Me.textBoxName.Text.Trim()
                editBook.Pages = Int32.Parse(Me.textBoxPage.Text.Trim())
                editBook.Grade = Int16.Parse(Me.textBoxGrade.Text.Trim())
                editBook.ISBN = Me.textBoxISBN.Text
                editBook.Genre = Me.textBoxGenre.Text
 
 
                'get the attached authors
                Dim booksAuthors() As BookAuthors = CType(editBook.GetBookAuthors(), BookAuthors())
 
                'add the authors 
                Dim var As ListItem
                For Each var In Me.listCheckboxes.Items
                    If var.Selected Then
                        'is new ?
                        Dim isNewAddition As Boolean =  True 
 
                        Dim bsk As BookAuthors
                        For Each bsk In booksAuthors
                            If bsk.AuthorId.ToString() = var.Value Then
                                isNewAddition = False
                                Exit For
                            End If
                        Next
 
                        If isNewAddition Then
                            Dim bkAuth As BookAuthors =  New BookAuthors() 
                            bkAuth.AuthorId = Int32.Parse(var.Value)
 
                            editBook.AttachTableMetadata(bkAuth)
                        End If
                    Else 
                        Dim bsk As BookAuthors
                        For Each bsk In booksAuthors
                            If (bsk.AuthorId.ToString() = var.Value) And (var.Selected = False) Then
                                editBook.RemoveTableMetadata(bsk)
                            End If
                        Next
                    End If
                Next
 
                'update the book
                perst.Update(editBook)
 
                'redirect to the main page
                Me.Response.Redirect("books.aspx")
            End If
        End If
    End Sub



    Private  Sub DisplayBook(ByVal id As Integer)
 
        Dim books() As Book = CType(Me.Application("BooksList"), Book())
 
        Dim var As Book
        For Each var In books
            If var.BookId = id Then
 
                Me.editableBook = var
 
                Me.labelAction.Text = "View book details : " + var.Name
 
 
                Me.textBoxName.Text = var.Name
 
                If Not var.IsNull("Pages") Then
                    Me.textBoxPage.Text = var.Pages.ToString()
                End If
 
                If Not var.IsNull("ISBN") Then
                    Me.textBoxISBN.Text = var.ISBN
                End If
 
                If Not var.IsNull("Genre") Then
                    Me.textBoxGenre.Text = var.Genre
                End If
 
                If Not var.IsNull("Grade") Then
                    Me.textBoxGrade.Text = var.Grade.ToString()
                End If
 
 
                'load the authors
                Dim authors() As Author = CType(var.GetAuthor(), Author())
                Dim auth As Author
                For Each auth In authors
                    Dim lst As ListItem
                    For Each lst In Me.listCheckboxes.Items
                        If lst.Text = auth.Name Then
                            lst.Selected = True
                        End If
                    Next
                Next
 
                Exit For
            End If
        Next
    End Sub



    Private  Sub LoadAuthors()
 
        Dim aut As Author =  New Author() 
        Dim authorPerst As AuthorPersistentObject =  New AuthorPersistentObject(aut) 
 
        'get all the authors
        Dim authors() As Author = CType(authorPerst.GetTableMetadata(), Author())
 
        'bind the author list to the check box list
        Me.listCheckboxes.DataSource = authors
        Me.listCheckboxes.DataTextField = "Name"
        Me.listCheckboxes.DataValueField = "AuthorId"
        Me.listCheckboxes.DataBind()
    End Sub


 Private Function ValidateData() As Boolean
 
        'validate the user name
        If Me.textBoxName.Text.Trim() = String.Empty Then
            Me.labelError.Text = "Please enter the book's name"
            Me.labelError.Visible = True
            Return False
        End If
 
 
  
        'check if the number of pages is numeric
        If Me.textBoxPage.Text.Trim()<> String.Empty Then
 
           Dim result As Integer
            If Int32.TryParse(Me.textBoxPage.Text.Trim(), result) = False Then
                Me.labelError.Text = "The book's nr of pages must be numeric"
                Me.labelError.Visible = True
                Return False
            End If
        End If
 
 
 
 
        'check if the grade is numeric
        If Me.textBoxGrade.Text.Trim() <> String.Empty Then
 
            Dim result As Short
            If Int16.TryParse(Me.textBoxGrade.Text.Trim(), result) = False Then
                Me.labelError.Text = "The book's grade must be numeric"
                Me.labelError.Visible = True
                Return False
            End If
        End If
 
        'check if the book's name is unique
 
        Dim bk As Book =  New Book() 
        Dim perst As BookPersistentObject =  New BookPersistentObject(bk) 
        Dim field As DatabaseField =  bk.GetField("Name") 
 
        'check if the name is unique
 
 
        'check if the name has changes
        If Me.isNew = True Then  '//And Me.editableBook.Name = Me.textBoxName.Text.Trim() Then


            Dim isUnique As Boolean = perst.IsUnique(field, Me.textBoxName.Text.Trim())


            If isUnique = False Then
                Me.labelError.Text = "The book's name is not unique. Please insert another name"
                Me.labelError.Visible = True
                Return False
            End If

            'the name was not changed
            Return True

        ElseIf Me.isNew = False And Me.editableBook.Name = Me.textBoxName.Text.Trim() Then


            Return True

        Else

            Dim isUnique As Boolean = perst.IsUnique(field, Me.textBoxName.Text.Trim())


            If isUnique = False Then
                Me.labelError.Text = "The book's name is not unique. Please insert another name"
                Me.labelError.Visible = True
                Return False
            End If

        End If
 
 
       
 
        'everything seems fine
        Return True
 End Function



End Class
