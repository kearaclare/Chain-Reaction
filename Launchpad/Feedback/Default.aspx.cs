using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fullFeedbackView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ddlType.DataSource = GetAllFeedbackType();
            ddlType.DataValueField = "ID";
            ddlType.DataTextField = "Type";
            ddlType.DataBind();
            ddlType.SelectedValue = "0";
            //ddlType.Items.Insert(0, new ListItem("Select Type", "0"));

            LoadGrid();


        }
    }

    protected void LoadGrid()
    {
        GV1.DataSource = GetAll(ddlType.SelectedValue);
        GV1.DataBind();
    }

    protected DataSet GetAll(string typeID)
    {
        string strConString;
        SqlConnection conJobs;
        SqlDataAdapter dadItems;
        DataSet dstItems;
        string SelectCmd;
        if (typeID == "0")
        {
            SelectCmd = "select * from feedbackLog order by ID DESC;";
        }
        else
        {
            SelectCmd = "select * from feedbackLog where feedbackType=" + typeID + " order by ID DESC;";
        }
        strConString = ConfigurationManager.ConnectionStrings["feedback"].ConnectionString;
        conJobs = new SqlConnection(strConString);
        dstItems = new DataSet();
        dadItems = new SqlDataAdapter(SelectCmd, conJobs);
        dadItems.Fill(dstItems, "NewItems");
        return dstItems;
    }

    protected string ReadFeedbackType(object ID)
    {
        string strID = ID.ToString();
        SqlConnection con1;
        SqlCommand cmdSelect;
        SqlDataReader dtrDataReader;
        string strReturn = "";
        string strSQL = "select Type from feedbackType where ID=@id";

        con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["feedback"].ConnectionString);
        con1.Open();
        cmdSelect = new SqlCommand(strSQL, con1);
        cmdSelect.Parameters.AddWithValue("@id", strID);
        dtrDataReader = cmdSelect.ExecuteReader();

        if (dtrDataReader.Read())
        {
            strReturn = dtrDataReader["Type"].ToString();
        }
        dtrDataReader.Close();
        con1.Close();

        return strReturn;
    }

    protected static DataSet GetAllFeedbackType()
    {
        string strConString;
        SqlConnection conJobs;
        SqlDataAdapter dadItems;
        DataSet dstItems;
        string SelectCmd;
        SelectCmd = "select * from feedbackType";
        strConString = ConfigurationManager.ConnectionStrings["feedback"].ConnectionString;
        conJobs = new SqlConnection(strConString);
        dstItems = new DataSet();
        dadItems = new SqlDataAdapter(SelectCmd, conJobs);
        dadItems.Fill(dstItems, "NewItems");
        return dstItems;
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }

    protected void GV1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string FeedbackID = GV1.DataKeys[e.RowIndex].Value.ToString();

        DeleteFeedback(FeedbackID);
        LoadGrid();
    }

    public static void DeleteFeedback(string ID)
    {
        SqlConnection con1;
        string strConString;
        SqlCommand cmdDelete;
        string strCommand = "delete from feedbackLog where ID=@id";

        strConString = ConfigurationManager.ConnectionStrings["feedback"].ConnectionString;
        con1 = new SqlConnection(strConString);
        cmdDelete = new SqlCommand(strCommand, con1);
        cmdDelete.Parameters.AddWithValue("@id", ID);


        con1.Open();
        cmdDelete.ExecuteNonQuery();
        con1.Close();
    }
}