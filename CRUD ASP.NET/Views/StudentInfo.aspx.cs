using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUD_ASP.NET
{
    public partial class StudentInfo : Page
    {

        protected static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseContext"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                GridViewBinding();
            }
        }

        private void GridViewBinding()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string sqlQuery = "SELECT ID, FirstName, LastName, DateOfBirth, PhoneNumber, Email From Students";

                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    SqlDataReader sudents = cmd.ExecuteReader();

                    dt.Columns.Add("ID", typeof(int));
                    int i = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        row["ID"] = i;
                        i++;
                    }
                    dt.Load(sudents);
                    grdStudentDetail.DataSource = dt;
                    grdStudentDetail.DataBind();

                }
            }
            catch (SqlException exception)
            {

                Response.Write(exception.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string firstName = txtFristName.Text;
            string lastName = txtLastName.Text;
            DateTime dateOfBirth = DateTime.Parse(txtDateOfBirth.Text);
            string phoneNumber = txtPhone.Text;
            string email = txtEmail.Text;
            DateTime createdDate = DateTime.Now;
            DateTime modifiedDate = DateTime.Now;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd.CommandText = "INSERT INTO Students (FirstName,LastName,PhoneNumber,Email,DateOfBirth,CreatedDate,ModifiedDate) VALUES (@FirstName,@LastName,@PhoneNumber,@Email,@DateOfBirth,@CreatedDate,@ModifiedDate)";
                    
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = firstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = dateOfBirth;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = createdDate;
                    cmd.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = modifiedDate;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    GridViewBinding();

                }
            }
            catch (SqlException exception)
            {
                Console.WriteLine("{0} Exception caught.", exception);
            }
            
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            lbtnAdd.Visible = false;
            pnlAdd.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lbtnAdd.Visible = true;
            pnlAdd.Visible = false;
        }

        protected void gvPerson_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            grdStudentDetail.PageIndex = e.NewPageIndex;

            GridViewBinding();
        }
        protected void gvPerson_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
            grdStudentDetail.EditIndex = e.NewEditIndex;

            
            GridViewBinding();

            lbtnAdd.Visible = false;
        }
        protected void gvPerson_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            
            grdStudentDetail.EditIndex = -1;

            
            GridViewBinding();

            lbtnAdd.Visible = true;
        }

        protected void gvPerson_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
           try
            {
                string ID = grdStudentDetail.Rows[e.RowIndex].Cells[3].Text;
                string firstName = ((TextBox)grdStudentDetail.Rows[e.RowIndex].FindControl("grdFirstName")).Text;
                string lastName = ((TextBox)grdStudentDetail.Rows[e.RowIndex].FindControl("grdLastName")).Text;
                string dateOfBirth = ((TextBox)grdStudentDetail.Rows[e.RowIndex].FindControl("grdDateOfBirth")).Text;
                string phoneNumber = ((TextBox)grdStudentDetail.Rows[e.RowIndex].FindControl("grdPhoneNumber")).Text;
                string email = ((TextBox)grdStudentDetail.Rows[e.RowIndex].FindControl("grdEmail")).Text;
                DateTime modifiedDate = DateTime.Now;

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd.CommandText = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, Email = @Email, DateOfBirth = @DateOfBirth, ModifiedDate = @ModifiedDate WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = firstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = phoneNumber;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = DateTime.Parse(dateOfBirth);
                    cmd.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = modifiedDate;
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    GridViewBinding();

                }
                grdStudentDetail.EditIndex = -1;

                
                GridViewBinding();

                
                lbtnAdd.Visible = true;
            }
            catch (SqlException exception)
            {
                Console.WriteLine("{0} Exception caught.", exception);
            }
        }

        protected void gvPerson_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    ((LinkButton)e.Row.Cells[1].Controls[0]).Attributes["onclick"] = "if(!confirm('Are you certain you want to delete this person ?')) return false;";
                }
            }
        }

        protected void gvPerson_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                string ID = grdStudentDetail.Rows[e.RowIndex].Cells[3].Text;

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = conn;

                    cmd.CommandText = "DELETE FROM Students WHERE ID = @ID";

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                GridViewBinding();
            }
            catch (SqlException exception)
            {
                Console.WriteLine("{0} Exception caught.", exception);
            }
        }

        // GridView.Sorting Event
        protected void gvPerson_Sorting(object sender, GridViewSortEventArgs e)
        {
            
        }
       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            grdStudentDetail.AllowPaging = false;
            grdStudentDetail.DataBind();
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            count = arr.Count;
            for (int i = 0; i < grdStudentDetail.Rows.Count; i++)
            {
                if (arr.Contains(grdStudentDetail.DataKeys[i].Value))
                {
                    DeleteRecord(grdStudentDetail.DataKeys[i].Value.ToString());
                    arr.Remove(grdStudentDetail.DataKeys[i].Value);
                }
            }
            ViewState["SelectedRecords"] = arr;
            hfCount.Value = "0";
            grdStudentDetail.AllowPaging = true;
            GridViewBinding();
            ShowMessage(count);
        }

        private void DeleteRecord(string CustomerID)
        {
            string constr = ConfigurationManager
                        .ConnectionStrings["conString"].ConnectionString;
            string query = "delete from TestCustomers " +
                            "where CustomerID=@CustomerID";
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void ShowMessage(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("alert('");
            sb.Append(count.ToString());
            sb.Append(" records deleted.');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(),
                            "script", sb.ToString());
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            try
            {
                bool isSelected = false;
                foreach (GridViewRow i in grdStudentDetail.Rows)
                {
                    CheckBox cb = (CheckBox)i.FindControl("chkSelect");
                    if (cb != null && cb.Checked)
                    {
                        isSelected = true;
                        break;
                    }
                }
                if (isSelected)
                {
                    GridView gvExport = grdStudentDetail;
                    gvExport.Columns[0].Visible = false;
                    foreach (GridViewRow i in grdStudentDetail.Rows)
                    {
                        gvExport.Rows[i.RowIndex].Visible = false;
                        CheckBox cb = (CheckBox)i.FindControl("chkSelect");
                        if (cb != null && cb.Checked)
                        {
                            gvExport.Rows[i.RowIndex].Visible = true;
                        }
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Charset = "";
                    string FileName = "StudentsInfo" + DateTime.Now + ".xls";
                    using (StringWriter strwritter = new StringWriter())
                    {
                        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                        grdStudentDetail.GridLines = GridLines.Both;
                        grdStudentDetail.HeaderStyle.Font.Bold = true;
                        grdStudentDetail.RenderControl(htmltextwrtter);
                        Response.Write(strwritter.ToString());
                        Response.End();
                    }
                }
                else
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Charset = "";
                    string FileName = "StudentsInfo" + DateTime.Now + ".xls";
                    using ( StringWriter strwritter = new StringWriter())
                    {
                        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                        grdStudentDetail.AllowPaging = false;
                        GridViewBinding();
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                        grdStudentDetail.GridLines = GridLines.Both;
                        grdStudentDetail.HeaderStyle.Font.Bold = true;
                        grdStudentDetail.RenderControl(htmltextwrtter);
                        Response.Write(strwritter.ToString());
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("{0} exception caught.", ex);
            }
        }
    }
}