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
using System.Globalization;
using System.Collections;


namespace elogsheet
{
    public partial class usingForm : System.Web.UI.Page
    {
        private static System.Globalization.Calendar cal = CultureInfo.InvariantCulture.Calendar;
        DBA con = new DBA();
        static String form_sk, rs, ps_code, logskin;
        static ArrayList result = new ArrayList();
        String lognoin, datein, enoin, shiftin, macin, lotin, pkgCoin, devin, freqin, spsizein, minspecin, maxspecin, unitin;
        int itemin;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                example.Visible = false;
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

        protected void pscodeInSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ps_code = pscodeInSet.SelectedValue;
            formSelect(ps_code);



        }

        protected void formSelect(String ps_code)
        {
            String q = "select form_sk,form_name from b_form where ps_code ='" + ps_code + "' and del_flag<>'Y'";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            selectForm.DataSource = ds.Tables[0];
            selectForm.DataMember = ds.Tables[0].TableName;
            selectForm.DataTextField = ds.Tables[0].Columns["form_name"].ColumnName;
            selectForm.DataValueField = ds.Tables[0].Columns["form_sk"].ColumnName;
            selectForm.DataBind();
        }

        protected void getFormItem(String form_sk)
        {
            String q = "select  to_char(sysdate,'YYYYMMIW')||lpad(b_commom_result_seq.nextval,5,'0') logno , b_commom_result_seq.nextval logsk,form_sk,ls_items_sk,freq,sp_size,minspec,maxspec,specunit,input_cont,droplist,ls_item_desc,sp_size_unit,input_type from b_set_form where form_sk=" + form_sk + "and del_flag<>'Y'";
            DataSet ds = con.getData(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                lotno.Text = ds.Tables[0].Rows[0]["logno"].ToString();
                logskin = ds.Tables[0].Rows[0]["logsk"].ToString();
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

        protected void selectForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            form_sk = selectForm.SelectedValue;
            example.Visible = true;
            pscodef.Text = ps_code;
            date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            getFormItem(form_sk);



        }
      






        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = cal.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return cal.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        protected static string replaceChar(string input)
            {
                
                input = input.Replace("\"", "");
                input = input.Replace("'", "");
                return input;
            }
        protected void Button2_Click(object sender, EventArgs e)
        {
            this.lognoin = lotno.Text;
            this.datein = date.Text;
            this.enoin = eno.Text;

            this.shiftin = shift.Text;
            this.macin = mid.Text;
            this.lotin = lot.Text;
            this.pkgCoin = pkgcode.Text;
            this.devin = device.Text;
            
            foreach (GridViewRow row in GridView1.Rows)
            {
                int lsitemsk = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values["ls_items_sk"]);
                if (GridView1.DataKeys[row.RowIndex].Values["input_cont"].Equals("droplist"))
                {
                    for (int i = 0; i < Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values["sp_size"]); i++)
                    {
                        DropDownList ddl = row.FindControl("ddl" + row.RowIndex.ToString() + i) as DropDownList;
                        result.Add(ddl.SelectedValue);
                    }
                }
                else if (GridView1.DataKeys[row.RowIndex].Values["input_cont"].Equals("textbox"))
                {
                    for (int i = 0; i < Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values["sp_size"]); i++)
                    {
                        TextBox tb = row.FindControl("text" + row.RowIndex.ToString() + i) as TextBox;
                        if (tb.Text != null)
                        {
                            String tex = tb.Text;
                            tex = replaceChar(tex);
                            result.Add(tex);
                        }
                        else
                        {
                            result.Add(0);
                        }
                    }
                }
               /*
                    for (int i = 0; i < Request.Files.Count; i++)
                    {

                        HttpPostedFile PostedFile = Request.Files[i];

                        if (PostedFile.ContentLength > 0)
                        {

                            string FileName = System.IO.Path.GetFileName(PostedFile.FileName);

                            PostedFile.SaveAs("C:/Users/ppin/Desktop/test/" + FileName);
                            result.Add("C:/Users/ppin/Desktop/test/" + FileName);
                        }

                    }
                */
                
                //Console.WriteLine(result.Count);
                String q = "insert into b_commom_result(log_sk,log_no,datetime,form_sk,ntacc,shift,m_id,pkg_code,lot,dev) values" +
                    "(" + logskin + ",'" + lognoin + "',sysdate," + form_sk + ",'" + enoin + "','" + shiftin + "','" + macin + "','" + pkgCoin + "','" + lotin + "','" + devin + "')";
                String rs = con.querytoDB(q);
           
                // Console.WriteLine(result.Count);
                for (int a = 0; a < result.Count; a++)
                {
                    q = "insert into b_item_result(log_sk,ls_item_sk,result,read_no) values(" + logskin + "," + lsitemsk + ",'" + result[a] as String + "'," + a + ")";
                    rs = con.querytoDB(q);
                }
                result.Clear();
            }
            result.Clear();
            for (int i = 0; i < Request.Files.Count; i++)
            {

                HttpPostedFile PostedFile = Request.Files[i];

                if (PostedFile.ContentLength > 0)
                {

                    string FileName = System.IO.Path.GetFileName(PostedFile.FileName);

                    PostedFile.SaveAs("\\\\thbkkfsprd00\\GrpDB12\\eLogSheet\\" + pscodeInSet.SelectedValue.ToString() + "\\Inbox\\" + FileName);
                    result.Add(FileName);
                }

            }
            

                // Console.WriteLine(result.Count);
                for (int a = 0; a < result.Count; a++)
                {
                   String qs = "insert into b_item_result(log_sk,ls_item_sk,result,read_no) values(" + logskin + "," + 0 + ",'" + result[a] as String + "'," + a + ")";
                    rs = con.querytoDB(qs);
                }
                result.Clear();

            Response.Redirect(Request.RawUrl);

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int index = e.Row.RowIndex;
                int contcolindex = getColId("Result");

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
              /*  else if (GridView1.DataKeys[index].Values["input_cont"].Equals("upl"))
                {

                    Button upadd = new Button();
                    upadd.ID = "Button"+index;
                        
                    upadd.Attributes.Add("onclick", "AddFileUpload("+index+");return false;");
                    upadd.Attributes.Add("value", "add");
                    upadd.Text = "Upload New Item(s)";
                    upadd.Attributes.Add("runat", "server");
                    e.Row.Cells[contcolindex].Controls.Add(upadd);
                    HtmlGenericControl div = new HtmlGenericControl("DIV");
                    div.Attributes.Add("ID", "FileUploadContainer"+index);
                    div.Attributes.Add("class", "col-75");
                    div.Attributes.Add("runat", "server");
                    e.Row.Cells[contcolindex].Controls.Add(div);
                }*/
            }
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


        //Boycatbay !!!
        //Fighto!!!!!


    }
}