Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
 
Imports voidsoft.DataBlock
Imports BookManager.Mappings
Imports BookManager.BusinessFacade
 
Public partial Class Books
	 Inherits System.Web.UI.Page
   Private books() As Book =  Nothing 
 
 
    Protected  Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.IsPostBack Then
           Me.LoadBooks()
        End If
    End Sub
 
 
 
 
 
    Private Sub LoadBooks()
            Dim bk As Book =  New Book() 
            Dim bookPerst As BookPersistentObject =  New BookPersistentObject(bk) 
 
            Me.books = CType(bookPerst.GetTableMetadata(), Book())
 
            Me.Application.Remove("BooksList")
 
            Me.Application.Add("BooksList", Me.books)
 
            Me.gridViewBooks.DataSource = Me.books
            Me.gridViewBooks.DataBind()
    End Sub
 
    Protected Sub New_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        'get the list of books from the app cache
        Me.books = CType(Me.Application("BooksList"), Book())

        'get the selected book
        Dim selectedBook As Book = Me.books(Convert.ToInt32(e.CommandArgument))


        'check for "view" command and redirect to list
        If e.CommandName.ToLower() = "view" Then
            Me.Response.Redirect("bookdetails.aspx?view=" + selectedBook.BookId.ToString())

            'check for "edit" command
        ElseIf (e.CommandName.ToLower() = "edit") Then
            Me.Response.Redirect("bookdetails.aspx?edit=" + selectedBook.BookId.ToString())

        ElseIf (e.CommandName.ToLower() = "delete") Then
            'delete the book. And because we have a PArent -> Child with a One to Many cardinality
            'and cascade delete , the related records from BookAuthors will also be deleted. 
            Dim perst As BookPersistentObject = New BookPersistentObject(selectedBook)
            perst.Delete(selectedBook)

            Me.Response.Redirect("books.aspx")
        End If
    End Sub

End Class

