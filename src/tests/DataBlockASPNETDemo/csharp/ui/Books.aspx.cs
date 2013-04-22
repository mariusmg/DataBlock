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

public partial class Books : System.Web.UI.Page
{
   private Book[] books = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
           this.LoadBooks();
        }
    }




    /// <summary>
    /// Load the list of books and bind them to the grid
    /// </summary>
    private void LoadBooks()
    {
            Book bk = new Book();
            BookPersistentObject bookPerst = new BookPersistentObject(bk);

            this.books = (Book[])bookPerst.GetTableMetadata();

            this.Application.Remove("BooksList");

            this.Application.Add("BooksList", this.books);

            this.gridViewBooks.DataSource = this.books;
            this.gridViewBooks.DataBind();
    }
    
    protected void gridViewBooks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //get the list of books from the app cache
        this.books = (Book[])this.Application["BooksList"];

        //get the selected book
        Book selectedBook = this.books[ Convert.ToInt32(e.CommandArgument)];


        //check for "view" command and redirect to list
        if (e.CommandName.ToLower() == "view")
        {
            this.Response.Redirect("bookdetails.aspx?view=" + selectedBook.BookId.ToString());
        }
        //check for "edit" command
        else if (e.CommandName.ToLower() == "edit")
        {
            this.Response.Redirect("bookdetails.aspx?edit=" + selectedBook.BookId.ToString());
        }
        //check for delete command
        else if(e.CommandName.ToLower() == "delete")
        {
            //delete the book. And because we have a PArent -> Child with a One to Many cardinality
            //and cascade delete , the related records from BookAuthors will also be deleted. 
            BookPersistentObject perst = new BookPersistentObject(selectedBook);
            perst.Delete(selectedBook);

            this.Response.Redirect("books.aspx");
        }
    }

}
