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

using BookManager.Mappings;
using BookManager.BusinessFacade;
using voidsoft.DataBlock;



public partial class search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        this.Search(this.textSearch.Text.Trim());
    }
    
    
    private void Search(string item)
    {
        this.labelTitleResults.Visible = true;
        this.labelResult.Visible = false;
        this.gridViewResults.Visible = false;

      
        Book bk = new Book();
        BookPersistentObject bookPerst = new BookPersistentObject(bk);

        QueryCriteria qc = new QueryCriteria(bk);
        qc.Add(CriteriaOperator.Like, bk.GetField("Name"), item);

        Book[] resultBooks = (Book[])  bookPerst.GetTableMetadata(qc);

        if (resultBooks.Length == 0)
        {
            this.labelResult.Visible = true;   
            this.labelResult.Text = "No results found";
            this.labelResult.Font.Bold = true;
        }
        else
        {

            Application.Add("SearchResults", resultBooks);

            this.gridViewResults.Visible = true;
            this.gridViewResults.DataSource = resultBooks;
            this.gridViewResults.DataBind();
        }
    }


    protected void gridViewResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       Book[] bookResults = (Book[])  Application["SearchResults"];
       Book bk = bookResults[Int32.Parse(e.CommandArgument.ToString())];
       this.Response.Redirect("bookdetails.aspx?view=" + bk.BookId.ToString());
    }
}
