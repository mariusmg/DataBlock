using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Specialized;

using voidsoft.DataBlock;
using BookManager.Mappings;
using BookManager.BusinessFacade;


public partial class bookdetails : System.Web.UI.Page
{

    //private fields
    private bool isNew = false; 
    private int bookId = -1;
    private Book editableBook = null;





    #region event handling



    protected void Page_Load(object sender, EventArgs e)
    {
    
        if (this.IsPostBack)
        {

            //get the query strings
            NameValueCollection nc = this.Request.QueryString;

            string[] keys = nc.AllKeys;
            string[] values = nc.GetValues(0);


            //get the current action
            if (keys[0].ToLower() == "edit")
            {
                this.bookId = Convert.ToInt32(values[0]);
                isNew = false;

                //also get the current book

                 Book[] books = (Book[]) this.Application["BooksList"];

                 foreach (Book var in books)
                 {
                     if (var.BookId == this.bookId)
                     {
                         this.editableBook = var;
                         break;
                     }
                 }

            }
            else if (keys[0].ToLower() == "new")
            {
                isNew = true;
            }
            
        }
        else
        {
            NameValueCollection nc = this.Request.QueryString;

            string[] keys = nc.AllKeys;
           

            //get the current action
            if (keys[0].ToLower() == "edit")
            {
                string[] args = nc.GetValues(0);
                this.textBoxName.ReadOnly = false;
                this.textBoxPage.ReadOnly = false;
                this.listCheckboxes.Enabled = true;
                this.LoadAuthors();

                this.DisplayBook(Convert.ToInt32(args[0]));

            }
            else if (keys[0].ToLower() == "view")
            {
                this.LoadAuthors();
                string[] args = nc.GetValues(0);
                this.DisplayBook(Convert.ToInt32(args[0]));
                this.textBoxName.ReadOnly = true;
                this.textBoxPage.ReadOnly = true;
                this.buttonSubmit.Visible = false;
                this.listCheckboxes.Enabled = false;
               
            }
            else if (keys[0].ToLower() == "new")
            {
                this.labelAction.Text = "Add new book";
                this.textBoxName.ReadOnly = false;
                this.textBoxPage.ReadOnly = false;
                this.listCheckboxes.Enabled = true;
                this.LoadAuthors();
            }
        }
    }

    protected void buttonSubmit_Click(object sender, EventArgs e)
    {
        //validate the entered data
        if (this.ValidateData())
        {
            if (this.isNew)
            {

                //create new book

                Book newBook = new Book();
                newBook.Name = this.textBoxName.Text.Trim();

                if (this.textBoxPage.Text.Trim() != string.Empty)
                {
                    newBook.Pages = Convert.ToInt32(this.textBoxPage.Text.Trim());
                }
                else
                {
                    newBook.SetNullValue("Pages");
                }


                if (this.textBoxISBN.Text.Trim() != string.Empty)
                {
                    newBook.ISBN = this.textBoxISBN.Text;
                }
                else
                {
                    newBook.SetNullValue("ISBN");
                }


                if (this.textBoxGenre.Text.Trim() != string.Empty)
                {
                    newBook.Genre = this.textBoxGenre.Text;
                }
                else
                {
                    newBook.SetNullValue("Genre");
                }


                if (this.textBoxGrade.Text.Trim() != string.Empty)
                {
                    newBook.Grade = Int16.Parse(this.textBoxGrade.Text);
                }
                else
                {
                    newBook.SetNullValue("Grade");
                }




                //add the authors

                List<int> listId = new List<int>();

                foreach (ListItem var in this.listCheckboxes.Items)
                {

                    if (var.Selected)
                    {
                        listId.Add(Int32.Parse(var.Value));
                    }
                }

                //add the book authors
                BookAuthors[] bk = new BookAuthors[listId.Count];

                //set the author id
                for (int i = 0; i < bk.Length; i++)
                {
                    bk[i] = new BookAuthors();
                    bk[i].AuthorId = listId[i];
                }

                //attach the book authors to our 
                foreach (BookAuthors bauth in bk)
                {
                    newBook.AttachTableMetadata(bauth);
                }

                //create the book
                BookPersistentObject perst = new BookPersistentObject(newBook);
                perst.Create(newBook);

                //redirect to the main
                this.Response.Redirect("books.aspx");
            }
            else
            {
                //update
                Book bk = new Book();
                BookPersistentObject perst = new BookPersistentObject(bk);
                Book editBook = (Book)perst.GetTableMetadata(this.bookId);

                editBook.Name = this.textBoxName.Text.Trim();
                editBook.Pages = Int32.Parse(this.textBoxPage.Text.Trim());
                editBook.Grade = Int16.Parse(this.textBoxGrade.Text.Trim());
                editBook.ISBN = this.textBoxISBN.Text;
                editBook.Genre = this.textBoxGenre.Text;

                
                //get the attached authors
                BookAuthors[] booksAuthors = (BookAuthors[]) editBook.GetBookAuthors();

                //add the authors 
                foreach (ListItem var in this.listCheckboxes.Items)
                {
                    if (var.Selected)
                    {
                        //is new ?
                        bool isNewAddition = true;

                        foreach (BookAuthors bsk in booksAuthors)
                        {
                            if (bsk.AuthorId.ToString() == var.Value)
                            {
                                isNewAddition = false;
                                break;
                            }
                        }

                        if (isNewAddition)
                        {
                            BookAuthors bkAuth = new BookAuthors();
                            bkAuth.AuthorId = Int32.Parse(var.Value);

                            editBook.AttachTableMetadata(bkAuth);
                        }
                    }
                    else
                    {
                        foreach (BookAuthors bsk in booksAuthors)
                        {
                            if ((bsk.AuthorId.ToString() == var.Value) && (var.Selected == false))
                            {
                                editBook.RemoveTableMetadata(bsk);
                            }
                        }
                    }
                }

                //update the book
                perst.Update(editBook);

                //redirect to the main page
                this.Response.Redirect("books.aspx");
            }
        }
    }
    #endregion






    /// <summary>
    /// Displays data about the book with the specified id
    /// </summary>
    /// <param name="id"></param>
    private void DisplayBook(int id)
    {

        Book[] books = (Book[]) this.Application["BooksList"];

        foreach (Book var in books)
        {
            if (var.BookId == id)
            {

                this.editableBook = var;

                this.labelAction.Text = "View book details : " + var.Name;


                this.textBoxName.Text = var.Name;

                if (!var.IsNull("Pages"))
                {
                    this.textBoxPage.Text = var.Pages.ToString();
                }

                if (!var.IsNull("ISBN"))
                {
                    this.textBoxISBN.Text = var.ISBN;
                }

                if (!var.IsNull("Genre"))
                {
                    this.textBoxGenre.Text = var.Genre;
                }

                if (!var.IsNull("Grade"))
                {
                    this.textBoxGrade.Text = var.Grade.ToString();
                }


                //load the authors
                Author[] authors = (Author[])var.GetAuthor();
                foreach (Author auth in authors)
                {
                    foreach (ListItem lst in this.listCheckboxes.Items)
                    {
                        if (lst.Text == auth.Name)
                        {
                            lst.Selected = true;
                        }
                    }
                }

                break;
            }
        }
    }


    /// <summary>
    /// Loads the authors
    /// </summary>
    private void LoadAuthors()
    {

        Author aut = new Author();
        AuthorPersistentObject authorPerst = new AuthorPersistentObject(aut);

        //get all the authors
        Author[] authors = (Author[])authorPerst.GetTableMetadata();

        //bind the author list to the check box list
        this.listCheckboxes.DataSource = authors;
        this.listCheckboxes.DataTextField = "Name";
        this.listCheckboxes.DataValueField = "AuthorId";
        this.listCheckboxes.DataBind();
    }





    #region internal implementation

    private bool ValidateData()
    {
        //validate

        //validate the user name
        if (this.textBoxName.Text.Trim() == string.Empty)
        {
            this.labelError.Text = "Please enter the book's name";
            this.labelError.Visible = true;
            return false;
        }








        //check if the number of pages is numeric
        if (this.textBoxPage.Text.Trim()!= string.Empty)
        {

           int result;
            if (Int32.TryParse(this.textBoxPage.Text.Trim(), out result) == false)
            {
                this.labelError.Text = "The book's nr of pages must be numeric";
                this.labelError.Visible = true;
                return false;
            }
        }




        //check if the grade is numeric
        if (this.textBoxGrade.Text.Trim() != string.Empty)
        {

            short result;
            if (Int16.TryParse(this.textBoxGrade.Text.Trim(), out result) == false)
            {
                this.labelError.Text = "The book's grade must be numeric";
                this.labelError.Visible = true;
                return false;
            }
        }

        //check if the book's name is unique

        Book bk = new Book();
        BookPersistentObject perst = new BookPersistentObject(bk);
        DatabaseField field = bk.GetField("Name");

        //check if the name is unique


        //check if the name has changes
        if(this.isNew == false && this.editableBook.Name == this.textBoxName.Text.Trim())
        {
            
            //the name was not changed
            return true;

        }


        bool isUnique = perst.IsUnique(field, this.textBoxName.Text.Trim());


        if (isUnique == false)
        {
            this.labelError.Text = "The book's name is not unique. Please insert another name";
            this.labelError.Visible = true;
            return false;
        }



        //everything seems fine
        return true;



    }
    #endregion

}
