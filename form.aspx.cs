using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;

namespace elogsheet
{
    public partial class form : System.Web.UI.Page
    {
        DBA con = new DBA();
        static int round = 0;
        static String form_sk, rs;
        String pscodein, lognamein, itemname, freqin, datein, spunitin, spsizein, minspecin, maxspecin, unitin;
        protected void Page_Load(object sender, EventArgs e)
        {
            getForm();
            if (!IsPostBack)
            {
                pslistSelect();
            }
        }





        protected void pslistSelect()
        {
            String q = "SELECT PS_CODE,PS_DESC FROM A_NEW_PROCESS_STEP";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            pscodeInSet.DataSource = ds.Tables[0];
            pscodeInSet.DataMember = ds.Tables[0].TableName;
            pscodeInSet.DataTextField = ds.Tables[0].Columns["PS_DESC"].ColumnName;
            pscodeInSet.DataValueField = ds.Tables[0].Columns["PS_CODE"].ColumnName;
            pscodeInSet.DataBind();

        }

        #region gv1

        protected void getFormItem(String form_sk)
        {
            String q = "select form_sk,ls_items_sk,freq,sp_size,minspec,maxspec,specunit,input_cont,droplist,ls_item_desc,sp_size_unit,input_type from b_set_form where form_sk=" + form_sk + "and del_flag<>'Y'";

            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "No Records Found";
            }

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                int contcolindex = getColId("Result");

                if (!Convert.IsDBNull(GridView1.DataKeys[index].Values["sp_size"]))
                {
                    int samplesize = Convert.ToInt32(GridView1.DataKeys[index].Values["sp_size"]);
                    if (GridView1.DataKeys[index].Values["input_cont"].Equals("droplist"))
                    {

                        for (int i = 0; i < samplesize; i++)
                        {
                            DropDownList ddl = new DropDownList();
                            String list = GridView1.DataKeys[index].Values["droplist"].ToString();
                            String[] itemlist = list.Split(';');
                            foreach (String k in itemlist)
                            {
                                ddl.Items.Add(k);
                            }

                            ddl.ID = "ddl" + index + i;
                            e.Row.Cells[contcolindex].Controls.Add(ddl);

                        }
                    }
                    else if (GridView1.DataKeys[index].Values["input_cont"].Equals("textbox"))
                    {
                        for (int i = 0; i < samplesize; i++)
                        {
                            TextBox tb = new TextBox();
                            tb.ID = "text" + index + i;
                            e.Row.Cells[contcolindex].Controls.Add(tb);
                        }
                    }
                    else if (GridView1.DataKeys[index].Values["input_cont"].Equals("upl"))
                    {

                        Button upadd = new Button();
                        upadd.ID = "Button1";
                        upadd.Attributes.Add("onclick", "AddFileUpload();return false;");
                        upadd.Attributes.Add("value", "add");
                        upadd.Text = "Upload New Item(s)";
                        upadd.Attributes.Add("runat", "server");
                        e.Row.Cells[contcolindex].Controls.Add(upadd);
                        HtmlGenericControl div = new HtmlGenericControl("DIV");
                        div.Attributes.Add("ID", "FileUploadContainer");
                        div.Attributes.Add("class", "col-75");
                        div.Attributes.Add("runat", "server");
                        e.Row.Cells[contcolindex].Controls.Add(div);
                    }
                }

            }         
    }
        protected void AddNewFileUpload(object sender, EventArgs e)
        {
            

        }
        private int getColId(string colName)
        {
            int index = -1;
            foreach (DataControlField col in GridView1.Columns)
            {
                if (col.HeaderText == colName)
                {
                    index = GridView1.Columns.IndexOf(col);
                    break;
                }
            }
            return index;
        }
        #endregion

        protected void clearAllForm() {
            pscodeInSet.ClearSelection();
            lgdescSet.Text = String.Empty;
            ItemSet.Text = String.Empty;
            freqSet.Text = String.Empty;
            spsizeSet.Text = String.Empty;
            spunitSet.Text = String.Empty;
            minspecSet.Text = String.Empty;
            maxspecSet.Text = String.Empty;
            unitSet.Text = String.Empty;
            dropTexSet.ClearSelection();
            //dropTexSel0.ClearSelection();
            droplist.InnerText = String.Empty;
            MultiView1.ActiveViewIndex = -1;
        }
        protected void setNewForm_Click(object sender, EventArgs e)
        {

            clearAllForm();
           
            setNewForm.Visible = false;
            addNewForm.Visible = true;
            pscodeInSet.Enabled = true;
            lgdescSet.Enabled = true;
            example.Visible = false;
            addItemtoform.Visible = false;
        }

        /*protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
  
            formDelete(form_sk);
           
        }*/

        protected void addNewForm_Click(object sender, EventArgs e)
        {
            this.pscodein = pscodeInSet.SelectedValue;
            this.lognamein = lgdescSet.Text;
            String q = "insert into b_form(form_sk,ps_code,form_name,del_flag) values(B_FORM_SEQ.nextval,'" + pscodein + "','" + lognamein + "','N')";
            String rs = con.querytoDB(q);
            q = "select max(form_sk) form_sk from b_form";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {

                form_sk = ds.Tables[0].Rows[0]["form_sk"].ToString();
            }
         /*    q = "insert into b_set_form(form_sk,ls_items_sk,ls_item_desc,input_cont,del_flag) values" +
                        " (" + form_sk + ", 0 , 'upload' ,'upl','N')";
            rs = con.querytoDB(q);*/
            //Label for example
            getForm();
			getFormItem(form_sk);
            setNewForm.Visible = true;
            addNewForm.Visible = false;
            pscodeInSet.Enabled = false;
            lgdescSet.Enabled = false;
            example.Visible = true;
            addItemtoform.Visible = true;
        }

        protected void dropTexSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropTexSet.SelectedIndex.Equals(0))
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else if (dropTexSet.SelectedIndex.Equals(1))
            {
                MultiView1.ActiveViewIndex = 0;
            }
         /*   else if (dropTexSet.SelectedIndex.Equals(2))
            {
                MultiView1.ActiveViewIndex = 2;
            }*/
        }

     /*   protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            delFormItem(index);

        }*/

        


        protected void add_Click(object sender, EventArgs e)
        {
            this.itemname = ItemSet.Text;
            this.freqin = freqSet.Text;
            this.spsizein = spsizeSet.Text;
            this.spunitin = spunitSet.Text;
            this.minspecin = minspecSet.Text;
            this.maxspecin = maxspecSet.Text;
            this.unitin = unitSet.Text;
            int x = dropTexSet.SelectedIndex;
            GridView1.Visible = true;
            if (MultiView1.ActiveViewIndex.Equals(1))
            {
               /* if (dropTexSel0.SelectedValue.Equals("text"))
                {
                    String q = "insert into b_set_form(form_sk,ls_items_sk,ls_item_desc,freq,sp_size,sp_size_unit,minspec,maxspec,specunit,input_cont,input_type,del_flag) values" +
                        " (" + form_sk + ",b_set_form_seq.nextval,'" + itemname + "','" + freqin + "'," + spsizein + ",'" + spunitin + "'," + minspecin + "," + maxspecin + ",'" + unitin + "','textbox','text','N')";
                   rs= con.querytoDB(q);
                }
                else if (dropTexSel0.SelectedValue.Equals("number"))
                {
                    String q = "insert into b_set_form(form_sk,ls_items_sk,ls_item_desc,freq,sp_size,sp_size_unit,minspec,maxspec,specunit,input_cont,input_type,del_flag) values" +
                        " (" + form_sk + ",b_set_form_seq.nextval,'" + itemname + "','" + freqin + "'," + spsizein + ",'" + spunitin + "'," + minspecin + "," + maxspecin + ",'" + unitin + "','textbox','numb','N')";
                    rs =con.querytoDB(q);
                }*/
                String q = "insert into b_set_form(form_sk,ls_items_sk,ls_item_desc,freq,sp_size,sp_size_unit,minspec,maxspec,specunit,input_cont,del_flag) values" +
                        " (" + form_sk + ",b_set_form_seq.nextval,'" + itemname + "','" + freqin + "'," + spsizein + ",'" + spunitin + "'," + minspecin + "," + maxspecin + ",'" + unitin + "','textbox','N')";
                rs = con.querytoDB(q);
            }
            else if (MultiView1.ActiveViewIndex.Equals(0))
            {
                String dropdwlist = droplist.Value.ToString();
                String q = "insert into b_set_form(form_sk,ls_items_sk,ls_item_desc,freq,sp_size,sp_size_unit,minspec,maxspec,specunit,input_cont,droplist,del_flag) values" +
                        " (" + form_sk + ",b_set_form_seq.nextval,'" + itemname + "','" + freqin + "','" + spsizein + "','" + spunitin + "','" + minspecin + "','" + maxspecin + "','" + unitin + "','droplist','" + dropdwlist + "','N')";
               rs = con.querytoDB(q);

            }
           /* else if (MultiView1.ActiveViewIndex.Equals(2)) 
            {
                String q = "insert into b_set_form(form_sk,ls_items_sk,ls_item_desc,freq,sp_size,sp_size_unit,minspec,maxspec,specunit,input_cont,del_flag) values" +
                        " (" + form_sk + ",b_set_form_seq.nextval,'" + itemname + "','" + freqin + "'," + spsizein + ",'" + spunitin + "'," + minspecin + "," + maxspecin + ",'" + unitin + "','upl','N')";
                rs = con.querytoDB(q);
            }*/
            this.MessageBox(rs.ToString());
            getFormItem(form_sk);
            clearAllItemAdd();

            


        }

        protected void clearAllItemAdd()
        {
            ItemSet.Text = String.Empty;
            freqSet.Text = String.Empty;
            spsizeSet.Text = String.Empty;
            spunitSet.Text = String.Empty;
            minspecSet.Text = String.Empty;
            maxspecSet.Text = String.Empty;
            unitSet.Text = String.Empty;
            dropTexSet.ClearSelection();
           // dropTexSel0.ClearSelection();
            MultiView1.ActiveViewIndex = -1;
            droplist.InnerText = String.Empty;
        }



        protected void getForm()
        {

            String q = "SELECT * from b_form where del_flag<>'Y'";

                DataSet ds = con.getData(q);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    int columncount = GridView2.Rows[0].Cells.Count;
                    GridView2.Rows[0].Cells.Clear();
                    GridView2.Rows[0].Cells.Add(new TableCell());
                    GridView2.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView2.Rows[0].Cells[0].Text = "No Records Found";
                }
            }
        

        protected void MessageBox(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'> window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            getForm();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Selete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                pscodeInSet.SelectedValue = GridView2.DataKeys[index].Values["ps_code"].ToString();
                lgdescSet.Text = GridView2.DataKeys[index].Values["form_name"].ToString();
                form_sk = GridView2.DataKeys[index].Values["form_sk"].ToString();
                getFormItem(form_sk);
                setNewForm.Visible = true;
                addNewForm.Visible = false;
                addItemtoform.Visible = true;
                example.Visible = true;
                pscodeInSet.Enabled = false;
                lgdescSet.Enabled = false;
            }else if(e.CommandName == "Del")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                form_sk = GridView2.DataKeys[index].Values["form_sk"].ToString();
                formDelete(form_sk);
            }

        }

        protected void formDelete(String form_sk)
        {
            //form_sk = GridView2.DataKeys[index].Values["form_sk"].ToString();
            //String q = "delete from b_set_form where form_sk =" + form_sk;
            String x = "update b_set_form set del_flag='Y' where form_sk=" + form_sk;
            rs = con.querytoDB(x);
           // q = "delete from b_form where form_sk =" + form_sk;
           x = "update b_form set del_flag='Y' where form_sk=" + form_sk;
            rs = con.querytoDB(x);
            clearAllForm();
            setNewForm.Visible = false;
            addNewForm.Visible = true;
            pscodeInSet.Enabled = true;
            lgdescSet.Enabled = true;
            example.Visible = false;
            addItemtoform.Visible = false;
            getForm();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            getFormItem(form_sk);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                delFormItem(index);
                

            }
        }

        protected void delFormItem(int index)
        {
            int itemsk = Convert.ToInt32( GridView1.DataKeys[index].Values["ls_items_sk"]);
            String q = "update b_set_form set del_flag='Y' where form_sk =" + form_sk + " and ls_items_sk=" + itemsk;
            rs = con.querytoDB(q);
            
            clearAllItemAdd();
            getFormItem(form_sk);
        }

    }
}