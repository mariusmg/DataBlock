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
 
 
Public partial Class _Default
	 Inherits System.Web.UI.Page
    Protected  Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
 
        'read the config info
        voidsoft.DataBlock.Configuration.ReadConfigurationFromConfigFile()
 
        Me.Response.Redirect("books.aspx")
    End Sub
End Class

