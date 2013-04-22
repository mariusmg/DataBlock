using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using voidsoft.DataBlock;
using BookManager.Mappings;
using BookManager.BusinessFacade;



public partial class Authors : System.Web.UI.Page
{

    Author[] authors = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            this.LoadAuthors();
        }
    }


    private void LoadAuthors()
    {
        try
        {
            Author aut = new Author();
            AuthorPersistentObject autPerst = new AuthorPersistentObject(aut);

            authors = (Author[])autPerst.GetTableMetadata();

            //add them to view
            this.Application.Add("AuthorsList", this.authors);


            this.gridViewAuthors.DataSource = authors;
            this.gridViewAuthors.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void gridViewAuthors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //get the selected index
        this.authors = (Author[])this.Application["AuthorsList"];


        Author selectedAuthor = this.authors[Convert.ToInt32(e.CommandArgument)];

        //check for "view" command and redirect to list
        if (e.CommandName.ToLower() == "view")
        {
            this.Response.Redirect("authordetails.aspx?bookid=" + selectedAuthor.AuthorId.ToString());
        }
    }

}
