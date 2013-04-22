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
 
Imports BookManager.Mappings
Imports BookManager.BusinessFacade
Imports voidsoft.DataBlock
 
 
 
Public Partial Class search
	 Inherits System.Web.UI.Page
    Protected  Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
 
    End Sub
 
    Protected  Sub buttonSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Search(Me.textSearch.Text.Trim())
    End Sub
 
 
    Private  Sub Search(ByVal item As String)
        Me.labelTitleResults.Visible = True
        Me.labelResult.Visible = False
        Me.gridViewResults.Visible = False
 
 
        Dim bk As New Book()
        Dim bookPerst As New BookPersistentObject(bk)
 
        Dim qc As New QueryCriteria(bk)
        qc.Add(CriteriaOperator.Like, bk.GetField("Name"), item)
 
        Dim resultBooks() As Book = CType(bookPerst.GetTableMetadata(qc), Book())
 
        If resultBooks.Length = 0 Then
            Me.labelResult.Visible = True   
            Me.labelResult.Text = "No results found"
            Me.labelResult.Font.Bold = True
        Else 
 
            Application.Add("SearchResults", resultBooks)
 
            Me.gridViewResults.Visible = True
            Me.gridViewResults.DataSource = resultBooks
            Me.gridViewResults.DataBind()
        End If
    End Sub
 
 
    Protected  Sub gridViewResults_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
       Dim bookResults() As Book = CType(Application("SearchResults"), Book())
        Dim bk As Book = bookResults(Int32.Parse(0))
       Me.Response.Redirect("bookdetails.aspx?view=" + bk.BookId.ToString())
    End Sub
End Class