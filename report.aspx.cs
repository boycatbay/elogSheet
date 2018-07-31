using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
 
namespace elogsheet
{
    public partial class report : System.Web.UI.Page
    {
        
        static DataTable resulttb;
        static String form_sk;
        DBA con = new DBA();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                start.Text = DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy");
                end.Text = DateTime.Today.ToString("MM/dd/yyyy");
                Area();
            }
        }

        protected void Area()
        {
            String q = "select * from a_new_process_step";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            marea.DataSource = ds.Tables[0];
            marea.DataMember = ds.Tables[0].TableName;
            marea.DataTextField = ds.Tables[0].Columns["PS_DESC"].ColumnName;
            marea.DataValueField = ds.Tables[0].Columns["PS_CODE"].ColumnName;
            marea.DataBind();
        }

        protected void marea_SelectedIndexChanged(object sender, EventArgs e)
        {
            formsel.Visible = false;
            String area = marea.SelectedValue;
            norGetdata(area);

        }
        protected void norGetdata(String area)
        {
            String q = "select form_sk,form_name from b_form where ps_code ='" + area + "'";
            DataSet ds = con.getData(q);
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataMember = ds.Tables[0].TableName;
            DropDownList1.DataTextField = ds.Tables[0].Columns["form_name"].ColumnName;
            DropDownList1.DataValueField = ds.Tables[0].Columns["form_sk"].ColumnName;
            DropDownList1.DataBind();
        }
        protected void formSelt_Onclick(object sender, EventArgs e)
        {
            formsel.Visible = true;
            form_sk = DropDownList1.SelectedValue;
            getReport(form_sk);
        }

        protected DataTable getrColReport(String form_sk)
        {
            String q = "select ls_item_desc from b_set_form where form_sk=" + form_sk;
            DataSet ds = con.getData(q);

            DataTable res = new DataTable();
            res.Columns.Add("Log NO.", typeof(String));
            res.Columns.Add("Date", typeof(String));
            res.Columns.Add("Initial", typeof(String));
            res.Columns.Add("Shift", typeof(String));
            res.Columns.Add("Machine NO.", typeof(String));
            res.Columns.Add("Package Code", typeof(String));
            res.Columns.Add("Lot", typeof(String));
            res.Columns.Add("Device", typeof(String));
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                res.Columns.Add(row["ls_item_desc"].ToString(), typeof(String));
            }
            res.Columns.Add("Upload", typeof(String));
            return res;
        }


        protected void getReport(String form_sk)
        {
            String strTime = start.Text;
            String endTime = end.Text;
            String q = "select log_sk,form_sk,log_no,ntacc,shift,m_id,to_char(datetime,'YYYY/MM/dd HH24:MI:SS') datetime,dev,lot,pkg_code,ls_item_desc,result from v_elog_report where form_sk =" + form_sk + " and datetime between to_date('" + strTime + "','MM/dd/yyyy') and  to_date('" + endTime + "','MM/dd/yyyy')";
            DataSet ds = con.getData(q);
            DataTable dt = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable resultTable = getrColReport(form_sk);

                for (int b = 0; b < dt.Rows.Count; b++)
                {
                    bool exit = false;
                    foreach (DataRow ro in resultTable.Rows)
                    {
                        if (ro["Log NO."].ToString() == dt.Rows[b]["log_no"].ToString())
                        {
                            exit = true;
                            break;
                        }
                    }
                    if (exit)
                    {
                        for (int a = 0; a < resultTable.Rows.Count; a++)
                        {
                            if (dt.Rows[b]["log_no"].ToString() == resultTable.Rows[a]["Log NO."].ToString())
                            {
                                if (!String.IsNullOrEmpty(dt.Rows[b]["ls_item_desc"].ToString()))
                                {
                                    String list = dt.Rows[b]["result"].ToString();
                                    resultTable.Rows[a]["" + dt.Rows[b]["ls_item_desc"].ToString() + ""] = list;
                                }
                                else
                                {
                                    String list = dt.Rows[b]["result"].ToString();
                                    resultTable.Rows[a]["Upload"] = list;
                                }
                                resultTable.AcceptChanges();

                            }

                        }
                    }
                    else
                    {


                        DataRow newrw = resultTable.NewRow();

                        newrw["Log NO."] = dt.Rows[b]["log_no"].ToString();
                        newrw["Date"] = dt.Rows[b]["datetime"].ToString();
                        newrw["Initial"] = dt.Rows[b]["ntacc"].ToString();
                        newrw["Shift"] = dt.Rows[b]["shift"].ToString();
                        newrw["Machine NO."] = dt.Rows[b]["m_id"].ToString();
                        newrw["Package Code"] = dt.Rows[b]["pkg_code"].ToString();
                        newrw["Lot"] = dt.Rows[b]["lot"].ToString();
                        newrw["Device"] = dt.Rows[b]["dev"].ToString();
                        resultTable.Rows.Add(newrw);
                        resultTable.AcceptChanges();
                        int index = resultTable.Rows.Count - 1;
                        if (!String.IsNullOrEmpty(dt.Rows[b]["ls_item_desc"].ToString()))
                        {
                            String list = dt.Rows[b]["result"].ToString();
                            // resultTable.Rows[index]["test"] = list;
                            resultTable.Rows[index]["" + dt.Rows[b]["ls_item_desc"].ToString() + ""] = list;
                        }
                        else
                        {
                            String list = dt.Rows[b]["result"].ToString();
                            resultTable.Rows[index]["Upload"] = list;
                        }
                        resultTable.AcceptChanges();
                    }
                }





                resulttb = resultTable.Copy();
                resulttb.AcceptChanges();
                GridView1.DataSource = resultTable;

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

        protected void exportExcel_Onclick(object sender, EventArgs e)
        {
            String filename = exportFilename.Text;
            myExcelHelper.ToExcel(resulttb, filename, Response);
        }
    }
}

