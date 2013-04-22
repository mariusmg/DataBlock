using System;
using System.Collections.Generic;
using System.IO;

namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class AspnetCodeGenerator
    {
        struct CodeGeneratorParameters
        {
            private string entityName;
            private bool generateContentPlaceholder;
            private DatabaseColumn[] columns;
            private string namespaceName;
            private string outputPath;
            private List<TableRelation> relation;

            public string EntityName
            {
                get
                {
                    return this.entityName;
                }
                set
                {
                    this.entityName = value;
                }
            }

            public bool GenerateContentPlaceholder
            {
                get
                {
                    return this.generateContentPlaceholder;
                }
                set
                {
                    this.generateContentPlaceholder = value;
                }
            }

            public DatabaseColumn[] Columns
            {
                get
                {
                    return this.columns;
                }
                set
                {
                    this.columns = value;
                }
            }

            public string NamespaceName
            {
                get
                {
                    return this.namespaceName;
                }
                set
                {
                    this.namespaceName = value;
                }
            }

            public string OutputPath
            {
                get
                {
                    return this.outputPath;
                }
                set
                {
                    this.outputPath = value;
                }
            }

            public List<TableRelation> Relations
            {
                get
                {
                    return this.relation;
                }
                set
                {
                    this.relation = value;
                }
            }
        }

        #region private fields
        private CodeGeneratorParameters parameters;
        #endregion


        /// <summary>
        /// Generates the edit page.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="generateContentPlaceholder">if set to <c>true</c> [generate content placeholder].</param>
        /// <param name="columns">The columns.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="relations">The relations.</param>
        private void GenerateEditPage(string entityName, bool generateContentPlaceholder, DatabaseColumn[] columns, string namespaceName, string outputPath, List<TableRelation> relations)
        {
            #region generate edit page

            FileStream fileStreamEditPage = null;
            StreamWriter writeEditPage = null;

            FileStream fileStreamEditCodeBehind = null;
            StreamWriter writerEditCodeBehind = null;


            FileStream fileStreamEditControldCodeBehind = null;
            StreamWriter writerControlsCodeBehind = null;

            Dictionary<string, string> dict = null;


            FileStream fileStreamPresenter = null;
            StreamWriter writerPresenter = null;

            try
            {

                parameters = new CodeGeneratorParameters();
                parameters.Columns = columns;
                parameters.EntityName = entityName;
                parameters.GenerateContentPlaceholder = generateContentPlaceholder;
                parameters.NamespaceName = namespaceName;
                parameters.OutputPath = outputPath;
                parameters.Relations = relations;


                fileStreamEditPage = new FileStream(outputPath + @"\" + entityName + "Edit.aspx", this.GetStreamOpenMode(outputPath + @"\" + entityName + "Edit.aspx"));
                writeEditPage = new StreamWriter(fileStreamEditPage);

                dict = new Dictionary<string, string>();

                this.WriteEditPageHeader(writeEditPage);
                this.WriteEditPageContent(writeEditPage, out dict);
                this.WriteEditPageFooter(writeEditPage);

                //write edit page code behind
                writeEditPage.Flush();

                fileStreamEditCodeBehind = new FileStream(outputPath + @"\" + entityName + "Edit.aspx.cs", this.GetStreamOpenMode(outputPath + @"\" + entityName + "Edit.aspx.cs"));
                writerEditCodeBehind = new StreamWriter(fileStreamEditCodeBehind);

                this.WriteEditPageCodeBehind(writerEditCodeBehind);

                writerEditCodeBehind.Flush();

                fileStreamEditControldCodeBehind = new FileStream(outputPath + @"\" + entityName + "Edit.aspx.designer.cs", this.GetStreamOpenMode(outputPath + @"\" + entityName + "Edit.aspx.designer.cs"));
                writerControlsCodeBehind = new StreamWriter(fileStreamEditControldCodeBehind);

                this.WriteEditPageDesignerCode(writerControlsCodeBehind, dict);

                writerControlsCodeBehind.Flush();

                fileStreamPresenter = new FileStream(outputPath + @"\" + entityName + "EditPresenter.cs", this.GetStreamOpenMode(outputPath + @"\" + entityName + "Presenter.cs"));
                writerPresenter = new StreamWriter(fileStreamPresenter);

                this.WriteEditPagePresenter(writerPresenter);

                writerPresenter.Flush();
            }
            finally
            {
                if (fileStreamEditPage != null)
                {
                    fileStreamEditPage.Close();
                }

                if (fileStreamEditCodeBehind != null)
                {
                    fileStreamEditCodeBehind.Close();
                }

                if (fileStreamEditControldCodeBehind != null)
                {
                    fileStreamEditControldCodeBehind.Close();
                }

                if (fileStreamPresenter != null)
                {
                    fileStreamPresenter.Close();
                }
            }
            #endregion
        }

        #region Edit page


        /// <summary>
        /// Writes the edit page header.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void WriteEditPageHeader(StreamWriter writer)
        {
            if (this.parameters.GenerateContentPlaceholder)
            {
                writer.WriteLine("  <%@ Page Language='C#' MasterPageFile='Master.Master' AutoEventWireup='true' Codebehind='" + this.parameters.EntityName + "Edit.aspx.cs' Inherits='" + this.parameters.NamespaceName + "." + this.parameters.EntityName + "Edit' %>");
                writer.WriteLine(" <asp:Content Id='Content1' ContentPlaceHolderID='ContentPlaceHolder1' runat='server' >");
            }
            else
            {
                //generate complete page
            }
        }


        /// <summary>
        /// Writes the content of the edit page.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="dictionaryControls">The dictionary controls.</param>
        private void WriteEditPageContent(StreamWriter writer, out Dictionary<string, string> dictionaryControls)
        {
            writer.WriteLine("<table width=" + "\"" + "100%" + "\"" + ">");

            writer.WriteLine("<tr>");
            writer.WriteLine("<td colspan='2' align='center'>");
//            writer.WriteLine("<asp:Label ID='labelTitle' runat='server' Text=" + "\"" + this.parameters.EntityName + "\"" + "/>");
            writer.WriteLine("<h3>" + this.parameters.EntityName + "</h3>");

			writer.WriteLine("</td>");
            writer.WriteLine("</tr>");


            List<string> listEndOutput = new List<string>();


            //key - control name
            //value - control type
            dictionaryControls = new Dictionary<string, string>();


            foreach (DatabaseColumn ci in this.parameters.Columns)
            {
                //skip editing the primary key
                if (ci.isPrimaryKey)
                {
                    continue;
                }

                writer.WriteLine("<tr>");

                writer.WriteLine("  <td align='right'>");
                writer.WriteLine("         <asp:Label id='label" + ci.Name + "' runat='server' Text='" + ci.Name + "'></asp:Label>");
                writer.WriteLine("  </td>");

                dictionaryControls.Add("label" + ci.Name, "Label");

                writer.WriteLine(" <td width='50%' valign='top' align='left'>");


                ChildTableRelation relation = null;
                string controlType, controlId, markupCode;

                if (this.IsLookupColumn(ci, this.parameters.Relations, out relation))
                {
                    this.GetOutputHtmlLookoupByType(ci, relation, this.parameters.EntityName, this.parameters.NamespaceName, ref listEndOutput, out controlId, out controlType, out markupCode);
                }
                else
                {
                    this.GetOutputHtmlByType(ci, out controlId, out controlType, out markupCode);
                }

                writer.WriteLine(markupCode);

                dictionaryControls.Add(controlId, controlType);

                if (ci.allowsNull == false && controlType == "TextBox")
                {
                    //also add field validators 
					writer.WriteLine("<br/>");
                    writer.WriteLine("<asp:RequiredFieldValidator ID=" + "\"" + "requiredField" + ci.Name + "\"" + " ControlToValidate='" + controlId + "' runat='server' ErrorMessage='Required field'></asp:RequiredFieldValidator>");
                }


                writer.WriteLine(" </td>");
                writer.WriteLine("</tr>");
            }

            writer.WriteLine("<tr>");
            writer.WriteLine("<td colspan='2' height='20px'></td>");
            writer.WriteLine("</tr>");

            writer.WriteLine("<tr>");
            writer.WriteLine("<td align='right'>");
            writer.WriteLine("<asp:Button runat='server' CausesValidation='true'  ID='buttonOK' Text='OK' OnClick='buttonOk_Click' />  ");
            writer.WriteLine("</td>");

            dictionaryControls.Add("buttonOk", "Button");

            writer.WriteLine("<td align='left'>");
            writer.WriteLine("<asp:Button runat='server' CausesValidation='false' ID='buttonCancel' OnClick='buttonCancel_Click' Text='Cancel' /> ");
            writer.WriteLine("</td>");

            dictionaryControls.Add("buttonCancel", "Button");

            writer.WriteLine("</tr>");

            writer.WriteLine("</table>");


            foreach (string data in listEndOutput)
            {
                writer.WriteLine(data);
            }

        }


        /// <summary>
        /// Writes the edit page footer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void WriteEditPageFooter(StreamWriter writer)
        {
            if (this.parameters.GenerateContentPlaceholder)
            {
                writer.WriteLine("</asp:Content>");
            }
            else
            {
            }
        }


        /// <summary>
        /// Writes the edit page code behind.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void WriteEditPageCodeBehind(StreamWriter writer)
        {
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Data;");
            writer.WriteLine("using System.Web.UI;");
            writer.WriteLine("using " + this.parameters.NamespaceName + ".Presenters;");

            writer.WriteLine("namespace " + this.parameters.NamespaceName);
            writer.WriteLine("{");

            writer.WriteLine("  public partial class " + this.parameters.EntityName + "Edit : Page ");
            writer.WriteLine("  {");

            writer.WriteLine("");
            writer.WriteLine(" private " + this.parameters.EntityName + "EditPresenter presenter = null;");
            writer.WriteLine("");


            writer.WriteLine("  public void Page_Load(object sender, EventArgs e)");
            writer.WriteLine("{");

            writer.WriteLine("  presenter = new " + this.parameters.EntityName + "EditPresenter();");


            writer.WriteLine(" if(! this.IsPostBack)");
            writer.WriteLine(" { ");
            writer.WriteLine("if(this.Request[" + "\"" + "id" + "\"" + "] != null)");
            writer.WriteLine("     {");
            writer.WriteLine(" this.BindData(Convert.ToInt32(this.Request[" + "\"" + "id" + "\"" + "]));");
            writer.WriteLine("     }");
            writer.WriteLine(" }");


            writer.WriteLine("}");

            writer.WriteLine("protected void buttonOk_Click(object sender, EventArgs e)");
            writer.WriteLine("{");
            writer.WriteLine("this.DoAction();");
            writer.WriteLine("}");


            writer.WriteLine("protected void buttonCancel_Click(object sender, EventArgs e)");
            writer.WriteLine("{");
            writer.WriteLine("    this.Response.Redirect(" + "\"" + this.parameters.EntityName + "View.aspx" + "\"" + ",true);");
            writer.WriteLine("}");


            writer.WriteLine("private void BindData(int id)");
            writer.WriteLine("{");
            writer.WriteLine(" " + this.parameters.EntityName + " entity = this.presenter.Get" + this.parameters.EntityName + "( Convert.ToInt32(this.Request[" + "\"" + "id" + "\"" + "]));");
            writer.WriteLine("");
            writer.WriteLine("");

            foreach (DatabaseColumn columnInfo in this.parameters.Columns)
            {
                if (columnInfo.isPrimaryKey)
                {
                    continue;
                }



                ChildTableRelation relation = null;
                bool isLookupColumns = this.IsLookupColumn(columnInfo, this.parameters.Relations, out relation);

                List<string> listOutput = new List<string>();

                string controlId, controlType, markupCode;


                if (isLookupColumns)
                {

                    this.GetOutputHtmlLookoupByType(columnInfo, relation, parameters.EntityName, parameters.NamespaceName, ref listOutput, out controlId, out controlType, out markupCode);

                    writer.WriteLine(" string " + relation.ForeignKeyName + " = entity." + columnInfo.Name + ".ToString();");

                    writer.WriteLine(controlId + ".DataBind();");

                    writer.WriteLine("for(int i = 0; i < this." + controlId + ".Items.Count; i++)");
                    writer.WriteLine("{");
                    writer.WriteLine("  if( " + controlId + ".Items[i].Value ==" + relation.ForeignKeyName + ")");
                    writer.WriteLine("{");
                    writer.WriteLine("     this. " + controlId + ".SelectedIndex = i;");
                    writer.WriteLine(" break;     ");
                    writer.WriteLine("}");
                    writer.WriteLine("}");

                }
                else
                {
                    GetOutputHtmlByType(columnInfo, out controlId, out controlType, out markupCode);


                    //add null checks
                    if (columnInfo.allowsNull)
                    {
                        writer.WriteLine("if(entity." + columnInfo.Name + " != null)");
                        writer.WriteLine("{");
                        writer.WriteLine("");
                    }


                    switch (controlType)
                    {
                        case "TextBox":
                            writer.WriteLine("this." + controlId + ".Text = entity. " + columnInfo.Name + ";");
                            break;

                        case "CheckBox":
                            writer.WriteLine("this." + controlId + ".Checked = Convert.ToBoolean(entity." + columnInfo.Name + ");");
                            break;

                        case "Calendar":
                            writer.WriteLine("this." + controlId + ".SelectedDate= Convert.ToDateTime(entity." + columnInfo.Name + ");");
                            break;

                        case "FileUpload":
                            break;
                    }
                }

                if (columnInfo.allowsNull)
                {
                    writer.WriteLine("}");
                }
            }

            writer.WriteLine("}");

            writer.WriteLine(" private void DoAction()");
            writer.WriteLine(" {");


            writer.WriteLine("  bool isCreate = (this.Request[" + "\"" + "id" + "\"" + "] == null);");

            writer.WriteLine("    " + this.parameters.EntityName + " entity = new " + this.parameters.EntityName + "();");


            DatabaseColumn primaryKeyInfo = null;

            //generate create    
            foreach (DatabaseColumn columnInfo in this.parameters.Columns)
            {
                if (columnInfo.isPrimaryKey)
                {
                    primaryKeyInfo = columnInfo;
                    continue;
                }

                ChildTableRelation relation = null;
                bool isLookupColumns = this.IsLookupColumn(columnInfo, this.parameters.Relations, out relation);

                List<string> listOutput = new List<string>();

                string controlId, controlType, markupCode;


                if (!isLookupColumns)
                {
                    GetOutputHtmlByType(columnInfo, out controlId, out controlType, out markupCode);

                    string columnType = this.GetElementType(columnInfo);

                    switch (controlType)
                    {
                        case "TextBox":

                            if (columnType == STRING_TYPE)
                            {
                                writer.WriteLine("entity." + columnInfo.Name + "= this." + controlId + ".Text;");
                            }
                            else
                            {
                                writer.WriteLine("entity." + columnInfo.Name + "= Convert.ToInt32(this." + controlId + ".Text);");
                            }
                            break;


                        case "CheckBox":
                            writer.WriteLine("entity." + columnInfo.Name + "= this." + controlId + ".Checked;");
                            break;

                        case "Calendar":
                            writer.WriteLine("entity." + columnInfo.Name + "= this." + controlId + ".SelectedDate;");
                            break;

                        case "FileUpload":
                            break;
                    }
                }
                else
                {
                    this.GetOutputHtmlLookoupByType(columnInfo, relation, this.parameters.EntityName, this.parameters.NamespaceName, ref listOutput, out controlId, out controlType, out markupCode);

                    writer.WriteLine("entity." + columnInfo.Name + "= Convert.ToInt32(this." + controlId + ".SelectedValue);");
                }
            }

            if (primaryKeyInfo == null)
            {
                throw new ArgumentException("The entity does not have a primary key defined");
            }

            writer.WriteLine(" if(isCreate)");
            writer.WriteLine("{");
            writer.WriteLine("   presenter.Create(entity);");
            writer.WriteLine("}");
            writer.WriteLine("else");
            writer.WriteLine("{");
            writer.WriteLine("    entity." + primaryKeyInfo.Name + "= Convert.ToInt32(this.Request[" + "\"" + "id" + "\"" + "]);");
            writer.WriteLine("    presenter.Update(entity);");
            writer.WriteLine("}");

            writer.WriteLine(" this.Response.Redirect(" + "\"" + this.parameters.EntityName + "View.aspx" + "\"" + ", true);");

            writer.WriteLine("}");

            writer.WriteLine("}");
            writer.WriteLine("}");
        }


        /// <summary>
        /// Writes the edit page designer code.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="dict">The dict.</param>
        private void WriteEditPageDesignerCode(StreamWriter writer, Dictionary<string, string> dict)
        {
            writer.WriteLine("namespace  " + this.parameters.NamespaceName);
            writer.WriteLine("{");

            writer.WriteLine(" public partial class " + this.parameters.EntityName + "Edit");
            writer.WriteLine("{");

            foreach (KeyValuePair<string, string> entry in dict)
            {
                writer.WriteLine("protected global::System.Web.UI.WebControls." + entry.Value + " " + entry.Key + ";");
            }

            writer.WriteLine("}");
            writer.WriteLine("}");
        }


        /// <summary>
        /// Writes the edit page presenter.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void WriteEditPagePresenter(StreamWriter writer)
        {
            writer.WriteLine("using System;");
            writer.WriteLine("using BusinessObjects;");
            writer.WriteLine("using System.Data;");

            writer.WriteLine("namespace " + this.parameters.NamespaceName + ".Presenters");
            writer.WriteLine("{");

            writer.WriteLine("public class " + this.parameters.EntityName + "EditPresenter");
            writer.WriteLine("{");


            writer.WriteLine(" private " + this.parameters.EntityName + "BusinessObject businessObject;");

            writer.WriteLine("");

            writer.WriteLine(" public " + this.parameters.EntityName + "EditPresenter()");
            writer.WriteLine("{");
            writer.WriteLine("   businessObject = new " + this.parameters.EntityName + "BusinessObject();");
            writer.WriteLine("}");


            writer.WriteLine(" public " + this.parameters.EntityName + " Get" + this.parameters.EntityName + "(int id)");
            writer.WriteLine("{");
            writer.WriteLine("   return businessObject.Get" + this.parameters.EntityName + "(id);");
            writer.WriteLine("}");


            writer.WriteLine(" public void Create(" + this.parameters.EntityName + " entity)");
            writer.WriteLine("{");
            writer.WriteLine("    businessObject.Create(entity);");
            writer.WriteLine("}");

            writer.WriteLine(" public void Update(" + this.parameters.EntityName + " entity)");
            writer.WriteLine("{");
            writer.WriteLine("    businessObject.Update(entity);");
            writer.WriteLine("}");


            foreach (TableRelation tableRelation in this.parameters.Relations)
            {
                if (tableRelation is ChildTableRelation)
                {
                    ChildTableRelation currentRelation = (ChildTableRelation) tableRelation;

                    writer.WriteLine("public DataTable Get" + tableRelation.RelatedTableName + "()");
                    writer.WriteLine(" {");
                    writer.WriteLine(tableRelation.RelatedTableName + " entity = new " + tableRelation.RelatedTableName + "();");
                    writer.WriteLine(currentRelation.RelatedTableName + "BusinessObject businessObject = new " + currentRelation.RelatedTableName + "BusinessObject();");
                    writer.WriteLine("    QueryCriteria qc = new QueryCriteria(entity.TableName, entity[" + currentRelation.RelatedTableName + "Fields." + currentRelation.ForeignKeyName + "], entity[" + currentRelation.RelatedTableName + "Fields.A]);");
                    writer.WriteLine("    return businessObject.GetDataTable(qc);");
                    writer.WriteLine(" }");
                }
            }



            writer.WriteLine("}");
            writer.WriteLine("}");
        }

        #endregion

        #region internal implementation


        /// <summary>
        /// Gets the stream open mode.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private FileMode GetStreamOpenMode(string filePath)
        {
            if (File.Exists(filePath))
            {
                return FileMode.Truncate;
            }

            return FileMode.CreateNew;
        }



        /// <summary>
        /// Gets HTML output for lookup entity.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="relation">The relation.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="listOutput">The list output.</param>
        /// <param name="controlId">The control id.</param>
        /// <param name="controlType">Type of the control.</param>
        /// <param name="markupOutput">The markup output.</param>
        private void GetOutputHtmlLookoupByType(DatabaseColumn info, ChildTableRelation relation, string entityName, string namespaceName, ref List<string> listOutput, out string controlId, out string controlType, out string markupOutput)
        {
            controlId = "dropDownList" + info.Name;
            controlType = "DropDownList";
            markupOutput = "<asp:DropDownList runat='server' DataTextField='A' DataValueField='" + relation.ForeignKeyName + "' DataSourceID='objectDataSource" + relation.RelatedTableName + "' id='dropDownList" + info.Name + "' ></asp:DropDownList>";

            listOutput.Add("<asp:ObjectDataSource ID='objectDataSource" + relation.RelatedTableName + "' runat='server' TypeName='" + namespaceName + ".Presenters." + entityName + "EditPresenter" + "' SelectMethod='Get" + relation.RelatedTableName + "'></asp:ObjectDataSource>");
        }

        /// <summary>
        /// Gets the type of the output HTML by.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="controlId">The control id.</param>
        /// <param name="controlType">Type of the control.</param>
        /// <param name="markupOutput">The markup output.</param>
        private void GetOutputHtmlByType(DatabaseColumn info, out string controlId, out string controlType, out string markupOutput)
        {
            string type = this.GetElementType(info);


            controlType = string.Empty;
            controlId = string.Empty;
            markupOutput = string.Empty;


            switch (type)
            {
                case NUMERIC_TYPE:
                case STRING_TYPE:
                    controlId = "textBox" + info.Name;
                    controlType = "TextBox";
                    markupOutput = "<asp:TextBox runat='server' ID='textBox" + info.Name + "' />";
                    return;

                case BOOLEAN_TYPE:
                    controlId = "checkBox" + info.Name;
                    controlType = "CheckBox";
                    markupOutput = "<asp:CheckBox runat='server' ID='checkBox" + info.Name + "' />";
                    return;

                case DATETIME_TYPE:
                    controlId = "calendar" + info.Name;
                    controlType = "Calendar";
                    markupOutput = "<asp:Calendar runat='server' ID=" + "\"" + "calendar" + info.Name + "\"" + "></asp:Calendar>";
                    return;

                case BYTEARRAY_TYPE:
                    controlId = "fileUpload" + info.Name;
                    controlType = "FileUpload";
                    markupOutput = "<asp:FileUpload runat='server' ID='fileUpload" + info.Name + "' />";
                    return;

                default:
                    return;
            }
        }


        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns></returns>
        private string GetElementType(DatabaseColumn info)
        {
            if (info.columnDataType == "System.String")
            {
                return STRING_TYPE;
            }
            else if (info.columnDataType == "System.Byte[]")
            {
                return BYTEARRAY_TYPE;
            }
            else if (info.columnDataType == "System.Boolean")
            {
                return BOOLEAN_TYPE;
            }
            else if (info.columnDataType == "System.DateTime")
            {
                return DATETIME_TYPE;
            }
            else
            {
                return NUMERIC_TYPE;
            }
        }


        /// <summary>
        /// Determines whether [is lookup column] [the specified info].
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="relations">The relations.</param>
        /// <returns>
        /// 	<c>true</c> if [is lookup column] [the specified info]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsLookupColumn(DatabaseColumn info, List<TableRelation> relations, out ChildTableRelation selectedRelation)
        {
            string name = info.Name;
            selectedRelation = null;


            foreach (TableRelation rel in relations)
            {
                if (rel is ChildTableRelation)
                {
                    ChildTableRelation myRelation = (ChildTableRelation) rel;

                    if (info.Name == myRelation.ForeignKeyName)
                    {
                        selectedRelation = myRelation;
                        return true;
                    }
                }
            }

            return false;
        }


        #endregion
    }
}