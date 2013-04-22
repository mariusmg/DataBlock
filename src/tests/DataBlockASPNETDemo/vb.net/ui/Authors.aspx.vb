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
 
 
 
Public partial Class Authors
	 Inherits System.Web.UI.Page
 
    Dim authors() As Author =  Nothing 
 
    Protected  Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
 
        If Not Me.IsPostBack Then
            Me.LoadAuthors()
        End If
    End Sub
 
 
    Private Sub LoadAuthors()

        Dim aut As New Author()
        Dim autPerst As New AuthorPersistentObject(aut)

        authors = CType(autPerst.GetTableMetadata(), Author())

        'add them to view
        Me.Application.Add("AuthorsList", Me.authors)


        Me.gridViewAuthors.DataSource = authors
        Me.gridViewAuthors.DataBind()


    End Sub


    Protected Sub New_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        'get the selected index
        Me.authors = CType(Me.Application("AuthorsList"), Author())


        Dim selectedAuthor As Author = Me.authors(Convert.ToInt32(0))

        'check for "view" command and redirect to list
        If e.CommandName.ToLower() = "view" Then
            Me.Response.Redirect("authordetails.aspx?bookid=" + selectedAuthor.AuthorId.ToString())
        End If
    End Sub

End Class
