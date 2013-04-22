using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace voidsoft.DataBlockModeler
{
    internal partial class AspnetCodeGenerator
    {
        private const string NUMERIC_TYPE = "NUMERIC";
        private const string STRING_TYPE = "STRING";
        private const string BOOLEAN_TYPE = "BOOLEAN";
        private const string BYTEARRAY_TYPE = "BYTEARRAY";
        private const string DATETIME_TYPE = "DATETIME";

        /// <summary>
        /// Generates the specified info.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="generateContentPlaceholder">if set to <c>true</c> [generate content placeholder].</param>
        /// <param name="outputPath">The output path.</param>
        public void Generate(DatabaseColumn[] info, string namespaceName, string entityName, bool generateContentPlaceholder, string outputPath, List<TableRelation> relations)
        {
            GenerateViewPage(entityName, generateContentPlaceholder, info, namespaceName, outputPath);

            GenerateEditPage(entityName, generateContentPlaceholder, info, namespaceName, outputPath, relations);
        }

        /// <summary>
        /// Generates the view page.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="generateContentPlaceholder">if set to <c>true</c> [generate content placeholder].</param>
        /// <param name="info">The info.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="outputPath">The output path.</param>
        private void GenerateViewPage(string entityName, bool generateContentPlaceholder, DatabaseColumn[] info, string namespaceName, string outputPath)
        {
            #region generate view page

            FileStream fileStreamViewPage = null;
            StreamWriter writer = null;

            FileStream fileStreamViewPageCodeBehind = null;
            StreamWriter writerCodeBehind = null;

            FileStream fileStreamDesigner = null;
            StreamWriter writerDesigner = null;


            FileStream fileStreamPresenter = null;
            StreamWriter writerPresenter = null;

            try
            {
                fileStreamViewPage = new FileStream(outputPath + @"\" + entityName + "View.aspx", this.GetStreamOpenMode(outputPath + @"\" + entityName + "View.aspx"));
                writer = new StreamWriter(fileStreamViewPage);

                fileStreamViewPageCodeBehind = new FileStream(outputPath + @"\" + entityName + "View.aspx.cs", this.GetStreamOpenMode(outputPath + @"\" + entityName + "View.aspx.cs"));
                writerCodeBehind = new StreamWriter(fileStreamViewPageCodeBehind);

                fileStreamDesigner = new FileStream(outputPath + @"\" + entityName + "View.aspx.designer.cs", this.GetStreamOpenMode(outputPath + @"\" + entityName + "View.aspx.designer.cs"));
                writerDesigner = new StreamWriter(fileStreamDesigner);

                fileStreamPresenter = new FileStream(outputPath + @"\" + entityName + "ViewPresenter.cs", this.GetStreamOpenMode(outputPath + @"\" + entityName + "ViewPresenter.cs"));
                writerPresenter = new StreamWriter(fileStreamPresenter);

                //write view aspx page
                this.WriteViewPageHeader(writer, entityName, namespaceName, generateContentPlaceholder);
                this.WriteViewPageContent(writer, info, entityName, namespaceName);
                this.WriteViewPageFooter(writer, generateContentPlaceholder);

                //write code behind file
                this.WriteViewCodeBehindFile(writerCodeBehind, entityName, namespaceName);

                //write the designer code
                this.WriteViewPageDesignerCode(writerDesigner, entityName, namespaceName);

                //write the view presenter class
                this.WriteViewPresenterClass(writerPresenter, entityName, namespaceName);

                writer.Flush();
                writerCodeBehind.Flush();
                writerDesigner.Flush();
                writerPresenter.Flush();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (fileStreamViewPage != null)
                {
                    fileStreamViewPage.Close();
                }

                if (fileStreamViewPageCodeBehind != null)
                {
                    fileStreamViewPageCodeBehind.Close();
                }

                if (fileStreamDesigner != null)
                {
                    fileStreamDesigner.Close();
                }


                if (fileStreamPresenter != null)
                {
                    fileStreamPresenter.Close();
                }
            }
            #endregion
        }



        #region View page content generator

        /// <summary>
        /// Writers the view page header.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="generateContentPlaceholder">if set to <c>true</c> [generate content placeholder].</param>
        private void WriteViewPageHeader(StreamWriter writer, string entityName, string namespaceName, bool generateContentPlaceholder)
        {
            if (generateContentPlaceholder)
            {
                writer.WriteLine("  <%@ Page Language='C#' MasterPageFile='Master.Master' AutoEventWireup='true' Codebehind='" + entityName + "View.aspx.cs' Inherits='" + namespaceName + "." + entityName + "View' %>");
                writer.WriteLine(" <asp:Content Id='Content1' ContentPlaceHolderID='ContentPlaceHolder1' runat='server' >");
            }
            else
            {
                //generate complete page
            }
        }

        /// <summary>
        /// Writes the content of the view page.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="entityName">Name of the entity.</param>
        private void WriteViewPageContent(StreamWriter writer, DatabaseColumn[] info, string entityName, string namespaceName)
        {
            writer.WriteLine("<table width=" + "\"" + "100%" + "\"" + ">");

            writer.WriteLine("<tr>");
            writer.WriteLine("<td align='center'>");
//            writer.WriteLine("<asp:Label ID='labelTitle' runat='server' Text=" + "\"" + entityName + "\"" + "/>");
  
             writer.WriteLine("<h3>" + entityName + "</h3>");

			writer.WriteLine("</td>");
			writer.WriteLine("</tr>");


            writer.WriteLine(" <tr>");
            writer.WriteLine("      <td align=" + "\"" + "center" + "\"" + ">");

            writer.WriteLine(" <asp:GridView   OnRowCommand='gridView_RowCommand' ID='gridView' runat='server' AllowSorting='True' AllowPaging='True' AutoGenerateColumns='False' DataSourceId='objectDataSource" + entityName + "'>");


            writer.WriteLine("<Columns>");


            bool generateLink = false;
            DatabaseColumn primaryKeyColumn = null;

            for (int i = 0; i < info.Length; i++)
            {
                if (info[i].isPrimaryKey)
                {
                    primaryKeyColumn = info[i];
                    continue;
                }

                if (primaryKeyColumn == null)
                {
                    throw new ArgumentException("No primary key found");
                }


                if (generateLink == false)
                {
                    writer.WriteLine("<asp:TemplateField ItemStyle-HorizontalAlign='Left'>");


                    //generate delete button
                    writer.WriteLine("<ItemTemplate>");
                    writer.WriteLine("  <asp:LinkButton CommandArgument='<%# DataBinder.Eval(Container.DataItem, " + "\"" + primaryKeyColumn.Name + "\"" + ") %>'");
                    writer.WriteLine("   CommandName='linkDelete' runat='server' Text='Delete' ></asp:LinkButton>");
                    writer.WriteLine("</ItemTemplate>");

                    writer.WriteLine("</asp:TemplateField>");


                    writer.WriteLine("<asp:TemplateField ItemStyle-HorizontalAlign='Left' HeaderText='" + info[i].Name + "' SortExpression='" + info[i].Name + "'>");


                    writer.WriteLine("<ItemTemplate>");
                    writer.WriteLine("  <asp:LinkButton CommandArgument='<%# DataBinder.Eval(Container.DataItem, " + "\"" + primaryKeyColumn.Name + "\"" + ") %>'");
                    writer.WriteLine("   CommandName='linkSelect' runat='server' Text='<%# DataBinder.Eval(Container.DataItem, " + "\"" + info[i].Name + "\"" + ") %>' ></asp:LinkButton>");
                    writer.WriteLine("</ItemTemplate>");
                    writer.WriteLine("</asp:TemplateField>");





                    generateLink = true;
                }
                else
                {
                    //generate bound columns
                    writer.WriteLine("<asp:BoundField DataField='" + info[i].Name + "' HeaderText='" + info[i].Name + "' SortExpression='" + info[i].Name + "' ItemStyle-HorizontalAlign='Center' />");
                }
            }

            writer.WriteLine("</Columns>");

            writer.WriteLine(" </asp:GridView>");

            writer.WriteLine(" <asp:ObjectDataSource ID='objectDataSource" + entityName + "' runat='server' TypeName='" + namespaceName + ".Presenters." + entityName + "ViewPresenter" + "' SelectMethod='Get" + entityName + "'></asp:ObjectDataSource>");

            writer.WriteLine("");
            writer.WriteLine("     </td>");
            writer.WriteLine("</tr>");

            writer.WriteLine("<tr>");
            writer.WriteLine("<td align='center'>");
            writer.WriteLine("<br/>");
            writer.WriteLine("<br/>");
            writer.WriteLine("<asp:Button runat='server' OnClick='buttonNew_Click' ID='buttonNew' Text='New' />");
            writer.WriteLine("</td>");
            writer.WriteLine("</tr>");

            //end page header here
            writer.WriteLine("</table>");
        }


        /// <summary>
        /// Writes the view page footer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="generateContentPlaceholder">if set to <c>true</c> [generate content placeholder].</param>
        private void WriteViewPageFooter(StreamWriter writer, bool generateContentPlaceholder)
        {
            if (generateContentPlaceholder)
            {
                writer.WriteLine("</asp:Content>");
            }
            else
            {
            }
        }


        /// <summary>
        /// Writes the code behind file.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="entityName">Name of the entity.</param>
        private void WriteViewCodeBehindFile(StreamWriter writer, string entityName, string namespaceName)
        {
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Data;");
            writer.WriteLine("using System.Web.UI;");
            writer.WriteLine("using " + namespaceName + ".Presenters;");

            writer.WriteLine("namespace " + namespaceName);
            writer.WriteLine("{");

            writer.WriteLine("  public partial class " + entityName + "View : Page ");
            writer.WriteLine("  {");
            writer.WriteLine("");

            writer.WriteLine("    private " + entityName + "ViewPresenter presenter = null;");
            writer.WriteLine("");


            writer.WriteLine("   protected void Page_Load(object sender, EventArgs e)");
            writer.WriteLine("   {");
            writer.WriteLine("       presenter = new " + entityName + "ViewPresenter();");
            writer.WriteLine("   }");

            writer.WriteLine(" protected void gridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)");
            writer.WriteLine(" {");


            writer.WriteLine(" string id = e.CommandArgument.ToString(); ");

            writer.WriteLine("  if (e.CommandName ==" + "\"" + "linkSelect" + "\"" + ")");
            writer.WriteLine("{");
            writer.WriteLine("      this.Response.Redirect(" + "\"" + entityName + "Edit.aspx?id=" + "\"" + " + id, true);");
            writer.WriteLine("}");

            writer.WriteLine(" else if(e.CommandName ==" + "\"" + "linkDelete" + "\"" + ")");
            writer.WriteLine("{");
            writer.WriteLine("     presenter.Delete(Convert.ToInt32(id));");
            writer.WriteLine("     this.Response.Redirect(this.Request.RawUrl, true);  ");
            writer.WriteLine("}");

            writer.WriteLine(" }");


            writer.WriteLine("");
            writer.WriteLine("");

            writer.WriteLine(" protected void buttonNew_Click(object sender, EventArgs e)");
            writer.WriteLine("{");
            writer.WriteLine("  this.Response.Redirect(" + "\"" + entityName + "Edit.aspx" + "\"" + ",true);");
            writer.WriteLine("}");

            writer.WriteLine(" }");

            writer.WriteLine("}");
        }


        /// <summary>
        /// Writes the view page designer code.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        private void WriteViewPageDesignerCode(StreamWriter writer, string entityName, string namespaceName)
        {
            writer.WriteLine("namespace " + namespaceName);
            writer.WriteLine("{");

            writer.WriteLine(" public partial class " + entityName + "Edit");
            writer.WriteLine("{");

            writer.WriteLine("protected global::System.Web.UI.WebControls.GridView gridView;");

            writer.WriteLine("}");
            writer.WriteLine("}");
        }



        /// <summary>
        /// Writes the presenter class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        private void WriteViewPresenterClass(StreamWriter writer, string entityName, string namespaceName)
        {
            writer.WriteLine("using System;");
            writer.WriteLine("using BusinessObjects;");
            writer.WriteLine("using System.Data;");

            writer.WriteLine("namespace " + namespaceName + ".Presenters");
            writer.WriteLine("{");

            writer.WriteLine("public class " + entityName + "ViewPresenter");
            writer.WriteLine("{");


            writer.WriteLine(" private " + entityName + "BusinessObject businessObject;");

            writer.WriteLine("");

            writer.WriteLine(" public " + entityName + "ViewPresenter()");
            writer.WriteLine("{");
            writer.WriteLine("   businessObject = new " + entityName + "BusinessObject();");
            writer.WriteLine("}");


            writer.WriteLine(" public DataTable Get" + entityName + "()");
            writer.WriteLine("{");
            writer.WriteLine("   return businessObject.GetDataTable();");
            writer.WriteLine("}");


            writer.WriteLine(" public void Delete(int id)");
            writer.WriteLine("{");
            writer.WriteLine("    businessObject.Delete(id);");
            writer.WriteLine("}");


            writer.WriteLine("}");
            writer.WriteLine("}");
        }





        #endregion
    }
}