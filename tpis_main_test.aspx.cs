using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Collections;
//using System.Web.UI.DataVisualization.Charting ;
using System.Drawing;
using System.Net;

using System.Data.SqlClient;
using System.Xml.Linq;

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services;
//using System.DirectoryServices.AccountManagement;
//using System.DirectoryServices;

public partial class tpis_main_test : System.Web.UI.Page
{
    private DataTable wk_table = new DataTable();
    public string Data_ID = "USHS";
    public int Load_Complete_Flag = 0; // 1 means complete


    private DataTable wk_table_html = new DataTable();
    public string session_number;
    private Boolean PageLoad_Flag = true;
    private string sqlQuery2;
    private DataTable wk_table_scale = new DataTable();
    public string labelid = "";
    private Boolean ParmCheck_Flag = true;
    public Boolean flag_data_change = false;
    public Boolean flag_data_setup = false;
    public int flag_public_website = 1;// set to 1 for public web site
    public int flag_product_group = 0;
    public int flag_partner_group = 0;
    public string Job_Num_Str = "111001";
    public int loadparms_processed = 0;
    public string PASSED_USER_ID = "";
    public string PASSED_USER_EMAIL = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        //Job_Num_Str=User_JobNum_TextBox1.Text.ToString() ; 
        string wk_teststr = "";
        string wk_userid = "";
        string wk_sessionid = "";
        string wk_cookie;
        string wk_uname;
        string wk_str;
        string wk_str2;
        string wk_tpis_name;
        string wk_email;
        string wk_href;
        int wk_len = 0;
        int wk_offset = 0;
        int wk_offset2 = 0;
        string Jobload_User = "";
        wk_uname = "NOUID";

        var aduser = Environment.UserName;
        var adenv = Environment.UserDomainName;
        var adusername = User.Identity.Name.ToString();
        ADUSER_Label1.Text = "AD: =envuser |" + aduser.ToString()
            + "| DOM=|" + adenv + "| aduser=|" + adusername + "|";

        try
        {
            wk_str = User_ID_Email_Label1.Text.ToString().ToUpper() + ":TEST";

            if (wk_str.IndexOf("ROGER.POMEROY@TRADE.GOV") >= 0)
            {
                wk_str = "TESTFORROGER";
                SubmitJob_User_Processing.Text = " RTP email";


            }
            if (loadparms_processed != 1 && wk_str.IndexOf("@TRADE.GOV") < 0)
            {
                adusername = Request.QueryString["loaduser"].ToString().ToUpper();
                SubmitJob_User_Processing.Text = " asu=" + adusername.ToString();

                if (adusername.Equals("ROGER.POMEROY@TRADE.GOV", StringComparison.InvariantCultureIgnoreCase))
                {
                    User_ID_Valid_Label1.Text = "POMEROR".ToString();
                    User_ID_Email_Label1.Text = "ROGER.POMEROY@TRADE.GOV".ToString();
                    Login_Passwd_TextBox1.Text = "Roger$56";
                    User_ID_Valid_Label1.DataBind();
                    Login_Panel.Visible = true;
                    PASSED_USER_ID = "POMEROR";
                    PASSED_USER_EMAIL = "ROGER.POMEROY@TRADE.GOV";

                    SubmitJob_User_Processing.Text += " passed rtp";
                    SubmitJob_User_ID_Label1.Text = PASSED_USER_ID;
                    SubmitJob_User_Email_Label1.Text = PASSED_USER_EMAIL;

                }
                else if (adusername.Equals("ANNE.FLATNESS@TRADE.GOV", StringComparison.InvariantCultureIgnoreCase))
                {
                    User_ID_Valid_Label1.Text = "FLATNEA".ToString();
                    User_ID_Email_Label1.Text = "ANNE.FLATNESS@TRADE.GOV".ToString();
                    Login_Passwd_TextBox1.Text = "TPIS$otea";
                    User_ID_Valid_Label1.DataBind();
                    Login_Panel.Visible = true;
                    PASSED_USER_ID = "FLATNEA";
                    PASSED_USER_EMAIL = "ANNE.FLATNESS@TRADE.GOV";
                    SubmitJob_User_Processing.Text += " passed anne";
                    SubmitJob_User_ID_Label1.Text = PASSED_USER_ID;
                    SubmitJob_User_Email_Label1.Text = PASSED_USER_EMAIL;
                }

                loadparms_processed = 1;
            }
            else if (loadparms_processed == 0)
            {
                adusername = User.Identity.Name;
                if (adusername.ToUpper().IndexOf("@TRADE.GOV", 0, StringComparison.InvariantCultureIgnoreCase) <= 0)
                {
                    User_ID_Valid_Label1.Text = "ANONYMOUS".ToString();
                    User_ID_Email_Label1.Text = "ANONYMOUS".ToString();
                    // Login_Passwd_TextBox1.Text="TPIS$otea" ;
                    User_ID_Valid_Label1.DataBind();
                    Login_Panel.Visible = true;

                }

                loadparms_processed = 1;
            }
            else
            {
                loadparms_processed = 1;
                adusername = User_ID_Email_Label1.Text.ToString().ToUpper();

            }

        }
        catch
        {
            adusername = User.Identity.Name;
        }
        User_ID_Email_Label1.Text = adusername.ToString().ToUpper();
        wk_offset = adusername.ToString().ToUpper().IndexOf("@TRADE.GOV"
            , 0
            , StringComparison.CurrentCultureIgnoreCase);
        if (wk_offset > 0)
        {
            wk_offset2 = 0;
            wk_email = adusername.ToString().ToUpper();
            wk_uname = wk_email.Substring(0, wk_offset + 1);
            wk_offset2 = wk_uname.IndexOf(".", 0);
            wk_len = wk_uname.Length - wk_offset2;


            wk_str = wk_uname.Substring(wk_offset2, wk_len).ToUpper();
            wk_str = wk_str.Replace(".", "").Replace("@", "");
            wk_len = wk_str.Length;
            if (wk_len > 6)
            {
                wk_str = wk_str.Substring(0, 6);
                wk_len = wk_str.Length;

            }
            if (!string.IsNullOrEmpty(wk_str) && wk_len > 1)
            {


                wk_offset = 1;
                wk_str2 = wk_email.Substring(0, 3);
                if (wk_str.Length < 3)
                {
                    wk_str2 = wk_email.Substring(0, 3).ToUpper().Replace(".", "");
                }

                else
                {
                    wk_str2 = wk_email.Substring(0, 1).ToUpper();
                }

                wk_tpis_name = wk_str + wk_str2;
                if (wk_tpis_name.Length > 2)
                {

                    User_ID_Valid_Label1.Text = wk_tpis_name.ToUpper();

                    wk_href = "<a href=\"https://tpisprod.blob.core.windows.net/$web/tpis_jobs_table/tpis_"
                             + User_ID_Valid_Label1.Text.ToString().ToLower()
                             + "_job_list.html?sp=rl&st=2021-08-16T17:58:47Z&se=2024-01-01T04:01:00Z&sv=2020-08-04&sr=c&sig=8tuwRFlus8Cgawq2%2FnqqtmiQ%2F6XaDVpxJUaMN3hBhIs%3D\" target=\"_blank_\">"
                             + "Show Table of Links to Outputs from Your Completed Jobs</a>";
                    Joblink_Literal.Text = wk_href;
                    adusername = User.Identity.Name.ToString().ToUpper();

                    if (adusername.ToUpper().IndexOf("ROGER.POMEROY") >= 0)
                    {
                        User_JobNum_TextBox1.Visible = true;
                        New_JobNum_chkbox.Visible = true;
                        if (wk_email.ToUpper().IndexOf("ROGER.POMEROY") >= 0)
                        {
                            Login_Panel.Visible = true;

                        }
                        else
                        {
                            Login_Panel.Visible = false;
                        }

                    }
                    else
                    {
                        Login_Panel.Visible = false;
                        New_JobNum_chkbox.Visible = false;
                        Login_Panel.Visible = false;
                    }


                }
            }
        }
        else
        {
            User_ID_Valid_Label1.Text = "ANONYMOUS";
            User_ID_Valid_Label1.Text = "POMEROR";
        }
        wk_sessionid = "NOSID";
        //this.MaintainScrollPositionOnPostBack=true ;

        HttpBrowserCapabilities bc = Request.Browser;
        string test_domain = "";
        string is_tpis_staff = "";
        //is_tpis_staff=Cookie_UserID.Text.ToString().ToUpper() ;

        loadparms_processed = 1;


        // end handle passed jobparms


        string wk_tpis_home_url = "";
        string PageLoad_Path = Request.Path.ToString().ToUpper();
        if (PageLoad_Path.IndexOf("TSE_PUBLIC") >= 0)
        {
            wk_tpis_home_url = "http://tse.export.gov/tse/TSEHomeTPIS.aspx";
            flag_public_website = 2;
        }
        else if (PageLoad_Path.IndexOf("TPIS_PUBLIC") >= 0)
        {
            wk_tpis_home_url = "http://tpisrtp.roger.net";
            flag_public_website = 1;
        }
        else if (PageLoad_Path.IndexOf("TPIS_GREPORT") >= 0)
        {
            wk_tpis_home_url = "http://tpisrtp.ROGER.NET/cgi-bin/wtpis/prod";
            flag_public_website = 0;
        }
        else
        {
            wk_tpis_home_url = "https://tpis.trade.gov";

        }
    } // end page load

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        //This will fire when page is loaded 

        // handle passed parms

        string Jobload_Parm = "";
        int Jobload_num = 0;
        Jobload_Parm = Passed_Parm_Label1.Text.ToString().ToUpper();
        Passed_Parm_Label1.Text = " pc";
        Main_Update_Panel.Update();
        if (String.IsNullOrEmpty(Jobload_Parm))
        {
            Jobload_Parm = "NONE";
        }
        Jobload_num = 0;
        if (Jobload_Parm.IndexOf("PASSED") < 0) // if passed then already checked
        {

            try
            {
                Jobload_Parm = Request.QueryString["loadjob"].ToString();

                if (!string.IsNullOrEmpty(Jobload_Parm))
                {
                    if (!Int32.TryParse(Jobload_Parm, out Jobload_num))
                    {
                        Jobload_num = -1;
                    }
                }



            }
            catch (Exception ldprm)
            {
                Jobload_Parm = "NONE";
                Jobload_num = -1;
            }
            Passed_Parm_Label1.Text = "PASSED :" + Jobload_Parm + ":";
            if (Jobload_num > 111000 && Jobload_num < 1000000)
            {

                Passed_Parm_Label1.Text = "PASSED Job Load=" + Jobload_Parm;
                User_JobNum_TextBox1.Text = Jobload_Parm;
                User_JobNum_TextBox1.DataBind();
                if (Show_Debug_CheckBox1.Checked)
                {
                    Load_Job_Panel.Visible = true;
                    Load_Update_Job_Panel.Update();
                }
                Load_Complete_Flag = 0;
                Change_Database(sender, e);
                Main_Update_Panel.Update();


                Load_Parmfile(sender, e);
                Main_Update_Panel.Update();


                if (Load_Complete_Flag != 1)
                {
                    LoadJob_Status_Label1.Text = "JOB " + Job_Num_Str + " LOAD INCOMPLETE--CLICK LOAD PREVIOUS JOB BUTTON AGAIN";
                    Load_Parmfile(sender, e);
                }
                else
                {
                    //LoadJob_Status_Label1.Text = "LOAD COMPLETE FOR JOB " + Job_Num_Str;
                    LoadJob_Status_Label1.Text = "JOB " + Job_Num_Str + " LOAD INCOMPLETE--CLICK LOAD PREVIOUS JOB BUTTON AGAIN";
                    LoadJob_Status_Label1.ForeColor = System.Drawing.Color.Red;
                }
                if (!LoadJob_Status_Label1.Visible)
                {
                    LoadJob_Status_Label1.ForeColor = System.Drawing.Color.Red;
                    LoadJob_Status_Label1.Visible = true;

                }
                int seljob_indx = 0;

                Job_List2_DropDownList.DataBind();


                seljob_indx = Job_List2_DropDownList.Items.IndexOf(Job_List2_DropDownList.Items.FindByValue(Jobload_num.ToString()));
                try
                {
                    Job_List2_DropDownList.SelectedIndex = seljob_indx;
                    Job_List2_DropDownList.DataBind();
                    LoadJob_Error_Label1.Text = "Job number load dropdown reset to=" + Jobload_num.ToString() + " Index=" + seljob_indx.ToString();
                    //Run_Job_Update_Panel.Update() ;

                }
                catch (Exception jlisterror)
                {
                    LoadJob_Error_Label1.Text = "Job number load dropdown failed=" + Jobload_num.ToString() + " ERROR: " + jlisterror.ToString();
                    LoadJob_Error_Label1.Visible = true;
                    LoadJob_Error_Label1.ForeColor = System.Drawing.Color.Red;

                }


                Run_Job_Update_Panel.Update();
                //Load_Parmfile(sender, e);
                GoToAnchor(this.Page, "TPISDataTable");

            }

        }

    } // end page_loadcompleted


    public static void GoToAnchor(System.Web.UI.Page page, string anchor)
    {
        string script = "<SCRIPT language = 'javascript'>window.location.href='#" + anchor + "'</SCRIPT>";

        page.RegisterStartupScript("TPISDataTable", script);
    }

    protected void MoveSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (Selected_ListBox.Visible)
                {
                    Selected_ListBox.Items.Add(li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append(li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (Selections_TextBox.Text.Length > 0)
                Selections_TextBox.Text = Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
        }

    }
    protected void MoveNotSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (Selected_ListBox.Visible)
                {
                    Selected_ListBox.Items.Add("!" + li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append("!" + li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (Selections_TextBox.Text.Length > 0)
                Selections_TextBox.Text = Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
        }

    }
    protected void MoveAll_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        foreach (ListItem li in Pick_ListBox.Items)
        {
            if (Selected_ListBox.Visible)
            {
                Selected_ListBox.Items.Add(li.Text);
            }
            if (li.Text.Length > 0)
            {
                builder.Append(li.Text + "\r\n");
            }
        }
        if (builder.Length > 0)
        {
            if (Selections_TextBox.Text.Length > 0)
                Selections_TextBox.Text += "\r\n" +
                    builder.ToString();
            else
                Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;

        }
    }
    protected void RemoveAll_Button_Click(object sender, EventArgs e)
    {
        Selected_ListBox.Items.Clear();
        Selections_TextBox.Text = "";
        //User_Sel_Name_TextBox1.Text = "";
        //Select_List_Num_DropDownList.Items.Clear() ;   
        // aug24 2022:  TextBox1.Text = "";

        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;

    }

    protected void NewList_Button_Click(object sender, EventArgs e)
    {

        int num_ctyselections = 0;

        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }

    protected void Create_PctyGroup_Click(object sender, EventArgs e)
    {
        string subgroup_name = "";
        string listtype = "PCTY";
        subgroup_name = User_Subgroup_Name_TextBox1.Text.ToString();
        if (String.IsNullOrEmpty(subgroup_name))
        {
            subgroup_name = "ERROR--MUST ENTER NAME FOR PARTNER LIST";
        }
        else
        {
            if (Selections_TextBox.Text.Length > 0)
                Selections_TextBox.Text = Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                              + "\r\n=" + subgroup_name + "\r\n";
            else
                Selections_TextBox.Text = "=" + subgroup_name + "\r\n";
        }

        Selections_TextBox.DataBind();
        //Save_Status_Label.Text = "Saving File to " + subgroup_name;
        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
        // Save_UserLists(listtype) ;
    } // end Button_Save_pctys
    protected void End_PctyGroup_Click(object sender, EventArgs e)
    {
        string pctygroup_name = "";
        pctygroup_name = User_Subgroup_Name_TextBox1.Text.ToString();
        if (Selections_TextBox.Text.Length > 0)
        {
            User_Subgroup_Name_TextBox1.Text = "";
            Selections_TextBox.Text = Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                          + "\r\n}\r\n";
            User_Subgroup_Name_TextBox1.Text = "";


            // Save_Status_Label.Text = "Ended Partner Group: " + pctygroup_name;

        }
        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    } // end End_PctyGroup_Click

    //*************************************
    protected void Database_Type_DropList_Changed(object sender, EventArgs e)
    {
        string wk_str = "";
        flag_data_change = true;
        DateTime cur_time = DateTime.Now;
        string cur_time_str = "";
        cur_time_str = cur_time.ToString("G");
        try
        {


            wk_str = Database_Type_Dropdownlist.SelectedValue.ToString().ToUpper();
            Get_Load_Flag.Text = "NO";
            Get_Digits_Text.Text = "";
            Get_Conc_Text.Text = "";
            string wk_region = "State";

            if (wk_str.Equals("US", StringComparison.InvariantCultureIgnoreCase))
            {
                Data_Panel_ushs.Visible = true;
                Current_DBID_Label1.Text = wk_str.ToUpper();
                //Data_Panel_ushs.Style["display"]="block";
                Data_Update_Panel_ushs.Update();
                Data_Panel_un_state.Visible = false;
                Data_Update_Panel_un_state.Update();

                Enable_District_Btn.Visible = true;
                Enable_SPIs_Btn.Visible = true;

                State_Header_Label.Text = "Customs Districts Selection:";
                STATE_TOTAL_ALL_CkBox.Text = "All Districts in the USA";
                STATE_SELECTED_CkBox.Text = "Selected Customs Districts";
                STATE_SELECTED_CkBox.Checked = false;
                STATE_TOTAL_ALL_CkBox.Checked = true;
                State_Selections_Panel2.Visible = false;
                Selected_STATE_Panel.Visible = false;
                State_Panel.Height = 150;
                State_Panel.Visible = false;
                STATE_Ajax_Panel1.Update();

                Selected_SPI_Panel.Visible = false;
                SPI_Selections_Panel2.Visible = false;
                SPI_SELECTED_CkBox.Checked = false;
                SPI_TOTAL_ALL_CkBox.Checked = true;
                SPI_Panel.Height = 150;
                SPI_Panel.Visible = false;
                SPIs_Update_Panel.Update();

                Selected_Reporter_Panel.Visible = false;
                RCTY_SELECTED_CkBox.Checked = false;
                RCTY_Selections_Panel2.Visible = false;
                RCTY_TOTAL_ALL_CkBox.Checked = true;
                Reporter_Panel.Height = 200;
                Reporter_Panel.Visible = false;
                Reporter_Update_Panel.Update();

                Data_ID = "USHS";
                Year_month_ckbox.Enabled = true;
                Year_quarter_ckbox.Enabled = true;
                Year_ytd_ckbox.Enabled = true;

                DataSource_DropDownList1.DataBind();
                DIGITS_ID_Dropdown1.DataBind();
            }
            else
            {
                if (wk_str.Equals("USTB", StringComparison.InvariantCultureIgnoreCase))
                {
                    Data_Panel_ushs.Visible = true;
                    Current_DBID_Label1.Text = wk_str.ToUpper();
                    Data_Update_Panel_ushs.Update();
                    Data_Panel_un_state.Visible = false;
                    Data_Update_Panel_un_state.Update();


                    State_Header_Label.Text = "Customs Districts Selection:";
                    STATE_TOTAL_ALL_CkBox.Text = "All Districts in the USA";
                    STATE_SELECTED_CkBox.Text = "Selected Customs Districts";
                    STATE_SELECTED_CkBox.Checked = false;
                    STATE_TOTAL_ALL_CkBox.Checked = true;
                    State_Selections_Panel2.Visible = false;
                    Selected_STATE_Panel.Visible = false;
                    State_Panel.Height = 150;
                    State_Panel.Visible = true;
                    STATE_Ajax_Panel1.Update();

                    Selected_SPI_Panel.Visible = false;
                    SPI_Selections_Panel2.Visible = false;
                    SPI_SELECTED_CkBox.Checked = false;
                    SPI_TOTAL_ALL_CkBox.Checked = true;
                    SPI_Panel.Height = 150;
                    SPI_Panel.Visible = false;
                    SPIs_Update_Panel.Update();

                    Selected_Reporter_Panel.Visible = false;
                    RCTY_SELECTED_CkBox.Checked = false;
                    RCTY_Selections_Panel2.Visible = false;
                    RCTY_TOTAL_ALL_CkBox.Checked = true;
                    Reporter_Panel.Height = 200;
                    Reporter_Panel.Visible = false;
                    Reporter_Update_Panel.Update();

                    Data_ID = "USTB";
                    Year_month_ckbox.Enabled = true;
                    Year_quarter_ckbox.Enabled = true;
                    Year_ytd_ckbox.Enabled = false;
                    Year_ytd_ckbox.Checked = false;
                    DataSource_DropDownList1.DataBind();
                    DIGITS_ID_Dropdown1.DataBind();

                }
                else
                {
                    Data_Panel_ushs.Visible = false;
                    //Data_Panel_ushs.Style["display"]="none";
                    Data_Update_Panel_ushs.Update();
                    if (wk_str.Equals("USST", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Current_DBID_Label1.Text = wk_str.ToUpper();
                        UNFlow_ConImp_ckbox.Enabled = true;
                        UNFlow_DomExp_ckbox.Visible = false;
                        UNFlow_ReExp_ckbox.Visible = false;
                        UNFlow_Volume_ckbox.Visible = false;
                        UNFlow_Weight_ckbox.Visible = true;
                        UV_Volume_ckbox.Visible = false;
                        UV_Weight_ckbox.Visible = true;
                        //State_Panel.Visible = true;
                        SPI_Panel.Visible = false;
                        Enable_District_Btn.Visible = false;
                        Enable_SPIs_Btn.Visible = false;

                        State_Header_Label.Text = "State Selection:";
                        STATE_TOTAL_ALL_CkBox.Text = "All States in the USA";
                        STATE_SELECTED_CkBox.Text = "Selected States";
                        STATE_SELECTED_CkBox.Checked = false;
                        STATE_TOTAL_ALL_CkBox.Checked = true;
                        State_Selections_Panel2.Visible = false;
                        Selected_STATE_Panel.Visible = false;
                        State_Panel.Height = 150;
                        State_Panel.Visible = true;
                        STATE_Ajax_Panel1.Update();

                        Selected_SPI_Panel.Visible = false;
                        SPI_Selections_Panel2.Visible = false;
                        SPI_SELECTED_CkBox.Checked = false;
                        SPI_TOTAL_ALL_CkBox.Checked = true;
                        SPI_Panel.Height = 150;
                        SPI_Panel.Visible = false;
                        SPIs_Update_Panel.Update();

                        Selected_Reporter_Panel.Visible = false;
                        RCTY_SELECTED_CkBox.Checked = false;
                        RCTY_Selections_Panel2.Visible = false;
                        RCTY_TOTAL_ALL_CkBox.Checked = true;
                        Reporter_Panel.Height = 200;
                        Reporter_Panel.Visible = false;
                        Reporter_Update_Panel.Update();


                        Data_ID = "USST";
                        UNFlow_Value_ckbox.Checked = true;
                        Year_month_ckbox.Enabled = true;
                        Year_quarter_ckbox.Enabled = true;
                        Year_ytd_ckbox.Enabled = true;
                        DataSource_DropDownList1.DataBind();
                        DIGITS_ID_Dropdown1.DataBind();

                    }
                    else if (wk_str.IndexOf("UN") >= 0)
                    {
                        Current_DBID_Label1.Text = wk_str.ToUpper();

                        UNFlow_ConImp_ckbox.Enabled = false;
                        UNFlow_DomExp_ckbox.Visible = true;
                        UNFlow_ReExp_ckbox.Visible = true;
                        UNFlow_Volume_ckbox.Visible = true;
                        UNFlow_Weight_ckbox.Visible = true;
                        UV_Volume_ckbox.Visible = true;
                        UV_Weight_ckbox.Visible = true;
                        Reporter_Panel.Visible = true;

                        Enable_District_Btn.Visible = false;
                        Enable_SPIs_Btn.Visible = false;

                        STATE_SELECTED_CkBox.Checked = false;
                        STATE_TOTAL_ALL_CkBox.Checked = true;
                        Selected_STATE_Panel.Visible = false;
                        State_Panel.Height = 150;
                        State_Panel.Visible = false;
                        STATE_Ajax_Panel1.Update();

                        Selected_SPI_Panel.Visible = false;
                        SPI_Selections_Panel2.Visible = false;
                        SPI_SELECTED_CkBox.Checked = false;
                        SPI_TOTAL_ALL_CkBox.Checked = true;
                        SPI_Panel.Height = 150;
                        SPI_Panel.Visible = false;
                        SPIs_Update_Panel.Update();

                        Selected_Reporter_Panel.Visible = false;
                        RCTY_SELECTED_CkBox.Checked = false;
                        RCTY_Selections_Panel2.Visible = false;
                        RCTY_TOTAL_ALL_CkBox.Checked = true;
                        Reporter_Panel.Height = 200;
                        Reporter_Panel.Visible = true;
                        Reporter_Update_Panel.Update();



                        UNFlow_Value_ckbox.Checked = true;
                        Data_ID = "UNHS";
                        Year_month_ckbox.Enabled = false;
                        Year_quarter_ckbox.Enabled = false;
                        Year_ytd_ckbox.Enabled = false;
                        Year_month_ckbox.Checked = false;
                        Year_quarter_ckbox.Checked = false;
                        Year_ytd_ckbox.Checked = false;

                        if (wk_str.IndexOf("S1") >= 0)
                            Data_ID = "UNS1";
                        else if (wk_str.IndexOf("S3") >= 0)
                            Data_ID = "UNS3";
                    }




                    Data_Panel_un_state.Visible = true;
                    Data_Update_Panel_un_state.Update();
                    DataSource_DropDownList1.DataBind();
                    DIGITS_ID_Dropdown1.DataBind();
                }
                // Job_Name_TextBox1.Text = Data_ID + " @ " + cur_time_str;
            }

        }
        catch (Exception jobchg_error)
        {
            LoadJob_Error_Label1.Text = "@chdb: wk_str=" + wk_str
            + "||; Data_id=" + Data_ID + "||; catch error=" + jobchg_error.ToString();
            LoadJob_Error_Label1.DataBind();
            LoadJob_Error_Label1.Visible = true;
            Run_Job_Update_Panel.Update();
        }


        GoToAnchor(this.Page, "TPISDataTable");
    }


    protected void Display_years_chkbox_CheckedChanged(object sender, EventArgs e)
    {
        if (Display_years_chkbox.Checked)
        {
            //Show_Year1_DropDownList.Visible = true;
            //Year_to_Label.Visible = true;
            //Show_Year2_DropDownList.Visible = true;
            //Show_Year1_DropDownList.DataBind();
            //if (Analyze_Year.SelectedIndex>=0)
            //{
            //   Show_Year2_DropDownList.SelectedValue = Analyze_Year.SelectedValue;
            //   Show_Year1_DropDownList.SelectedValue = Analyze_Year.SelectedValue;
            //}
            //else
            //{
            //   if (Show_Year1_DropDownList.SelectedIndex<0) 
            //   {
            //     Show_Year2_DropDownList.SelectedValue = "2011" ;
            //     Show_Year1_DropDownList.SelectedValue = "2011" ;
            //   }
            //}

            if (Show_Year1_DropDownList.SelectedIndex < 0)
            {
                Show_Year2_DropDownList.SelectedValue = "2014";
                Show_Year1_DropDownList.SelectedValue = "2014";
            }


        }

    } // Display_years_chkbox_CheckedChanged

    protected void Flow_ckbox_changed(object sender, EventArgs e)
    {
        Flow_TextBox1.Text = "M";
        if (Flow_imports_ckbox.Checked)
            Flow_TextBox1.Text = "M";
        else if (Flow_exports_ckbox.Checked)
            Flow_TextBox1.Text = "X";

    } // end Flow_ckbox_changed


    protected void Product_DropDownList1_Change(object sender, EventArgs e)
    {
        flag_data_setup = true;
        Product_Detail1_DropDownList1.DataBind();
        try
        {
            Series_TextBox.Text = this.Product_DropDownList1.SelectedValue.ToString();
            Series_Type_TextBox.Text = DataSource_DropDownList1.SelectedValue.ToString();
        }
        catch (Exception ex)
        {
        }

        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Product_DropDownList1_Change


    protected void Product_DropDownList_Detail1_Change(object sender, EventArgs e)
    {
        //Product_Detail1_DropDownList21.DataBind() ;
        flag_data_setup = true;
        string wk_str = "";
        try
        {
            if (this.Product_Detail1_DropDownList1.SelectedIndex > 0)
            {
                wk_str = this.Product_Detail1_DropDownList1.SelectedValue.ToString();
                if (String.IsNullOrEmpty(wk_str))
                    return;
                else if (wk_str.ToUpper() == "NOT SELECTED")
                    Series_TextBox.Text = this.Product_DropDownList1.SelectedValue.ToString();
                else
                    Series_TextBox.Text = wk_str;
            }

            //Series_Type_TextBox.Text=DataSource_DropDownList1.SelectedValue.ToString() ;
        }
        catch (Exception ex)
        {
        }

        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Product_DropDownList_Detail1_Change

    protected void Product_DropDownList_Detail2_Change(object sender, EventArgs e)
    {
        flag_data_setup = true;
        string wk_str = "";
        try
        {
            if (this.Product_Detail1_DropDownList1.SelectedIndex > 0)
            {
                wk_str = this.Product_Detail1_DropDownList2.SelectedValue.ToString();
                if (String.IsNullOrEmpty(wk_str))
                    return;
                else if (wk_str.ToUpper() == "NOT SELECTED")
                    Series_TextBox.Text = this.Product_Detail1_DropDownList2.SelectedValue.ToString();
                else
                    Series_TextBox.Text = wk_str;
            }

            //Series_Type_TextBox.Text=DataSource_DropDownList1.SelectedValue.ToString() ;
        }
        catch (Exception ex)
        {
        }
        // GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Product_DropDownList_Detail2_Change


    protected void DataSource_Change(object sender, EventArgs e)
    {

        Get_Load_Flag.Text = "NO";
        Get_Digits_Text.Text = "";
        Get_Conc_Text.Text = "";

        try
        {
            this.Product_DropDownList1.SelectedIndex = 0;
            this.Product_Detail1_DropDownList1.SelectedIndex = 0;
            this.Product_Detail1_DropDownList2.SelectedIndex = 0;
            try
            {
                Series_TextBox.Text = "TOTAL";
                Series_Type_TextBox.Text = DataSource_DropDownList1.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
            }
        }
        catch (Exception ex)
        {
            Product_Name_TextBox.Text = "Merchandise Trade";
        }
        Product_DropDownList1.DataBind();
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
        // Load_Oracle_Table(sender, e) ;

    } // end DataSource_Change


    protected void MoveSelected_CMD_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in Pick_CMD_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (Selected_CMD_ListBox.Visible)
                {
                    Selected_CMD_ListBox.Items.Add(li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append(li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (Selections_CMD_TextBox.Text.Length > 0)
                Selections_CMD_TextBox.Text = Selections_CMD_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                Selections_CMD_TextBox.Text = builder.ToString();
            //GoToAnchor(this.Page, "TPISCMDSETUP") ;

        }

    } // END MoveSelected_CMD_Button_Click

    protected void MoveNotSelected_CMD_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in Pick_CMD_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (Selected_CMD_ListBox.Visible)
                {
                    Selected_CMD_ListBox.Items.Add("!" + li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append("!" + li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (Selections_CMD_TextBox.Text.Length > 0)
                Selections_CMD_TextBox.Text = Selections_CMD_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                Selections_CMD_TextBox.Text = builder.ToString();
            //GoToAnchor(this.Page, "TPISCMDSETUP") ;
        }

    } // END MoveNotSelected_CMD_Button_Click
    protected void MoveAll_CMD_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        foreach (ListItem li in Pick_CMD_ListBox.Items)
        {
            if (Selected_CMD_ListBox.Visible)
            {
                Selected_CMD_ListBox.Items.Add(li.Text);
            }
            if (li.Text.Length > 0)
            {
                builder.Append(li.Text + "\r\n");
            }
        }
        if (builder.Length > 0)
        {
            if (Selections_CMD_TextBox.Text.Length > 0)
                Selections_CMD_TextBox.Text += "\r\n" +
                    builder.ToString();
            else
                Selections_CMD_TextBox.Text = builder.ToString();
            //GoToAnchor(this.Page, "TPISCMDSETUP") ;

        }
    } // END MoveAll_CMD_Button_Click

    protected void RemoveAll_CMD_Button_Click(object sender, EventArgs e)
    {
        Selected_CMD_ListBox.Items.Clear();
        Selections_CMD_TextBox.Text = "";
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
        // User_Sel_Name_TextBox1.Text="" ;
        //Select_List_Num_DropDownList.Items.Clear() ;   
        // TextBox1.Text="" ;


    } // end RemoveAll_CMD_Button_Click


    protected void Create_CMDsGroup_Click(object sender, EventArgs e)
    {
        string cmdgroup_name = "";
        string listtype = "CMDs";
        cmdgroup_name = "";
        cmdgroup_name = User_CMDsGroup_Name_TextBox1.Text.ToString();
        if (String.IsNullOrEmpty(cmdgroup_name))
        {
            Save_CMDs_Status_Label.Text = "ERROR--MUST ENTER NAME FOR Product Group";
        }
        else
        {
            if (Selections_CMD_TextBox.Text.Length > 0)
                Selections_CMD_TextBox.Text = Selections_CMD_TextBox.Text.ToString().TrimEnd('\r', '\n')
                              + "\r\n=" + cmdgroup_name + "\r\n";
            else
                Selections_CMD_TextBox.Text = "=" + cmdgroup_name + "\r\n";
            Save_CMDs_Status_Label.Text = "Creating Product Group: " + cmdgroup_name;
            flag_product_group = 1;
        }

        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
        // Save_UserLists(listtype) ;
    } // end Button_Save_CMDs
    protected void End_CMDsGroup_Click(object sender, EventArgs e)
    {
        string group_name = "";
        if (Selections_CMD_TextBox.Text.Length > 0)
        {
            group_name = User_CMDsGroup_Name_TextBox1.Text.ToString();
            if (!String.IsNullOrEmpty(group_name))
            {

                Selections_CMD_TextBox.Text = Selections_CMD_TextBox.Text.ToString().TrimEnd('\r', '\n')
                           + "\r\n}\r\n";
                User_CMDsGroup_Name_TextBox1.Text = "";
                Save_CMDs_Status_Label.Text = "Ended Product Group: " + group_name;
            }
        }
        flag_product_group = 0;
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;

    }

    protected void Button_Save_PCTYs_Click(object sender, EventArgs e)
    {
        string wk_return = "";
        string save_name = "";
        string listtype = "PCTY";
        int resave_list = 0;
        if (Resave_PCTY_Load_CkBox.Checked)
            resave_list = 1;

        // Save_UserLists...;
        Save_PCTYs_Status_Label.Text = "Calling Save_User_List with " + listtype;
        Save_PCTYs_Status_Label.DataBind();
        Save_User_List(listtype, resave_list);

    } // end Save_PCTYs_UserLists
    protected void Button_Save_RCTYs_Click(object sender, EventArgs e)
    {
        string wk_return = "";
        string save_name = "";
        string listtype = "RCTY";
        int resave_list = 0;
        if (Resave_RCTY_Load_CkBox.Checked)
            resave_list = 1;

        // Save_UserLists...;
        Save_RCTYs_Status_Label.Text = "Calling Save_User_List with " + listtype;
        Save_RCTYs_Status_Label.DataBind();
        Save_User_List(listtype, resave_list);

    } // end Save_PCTYs_UserLists

    protected void Button_Save_CMDs_Click(object sender, EventArgs e)
    {
        string listtype = "CMD";
        int resave_list = 0;
        // Save_UserLists...;
        Save_CMDs_Status_Label.Text = "Calling Save_User_List with " + listtype;
        Save_CMDs_Status_Label.DataBind();
        Save_User_List(listtype, resave_list);
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Button_Save_CMDs

    protected void Button_Delete_CMD_List_Click(object sender, EventArgs e)
    {
        string listtype = "CMD";
        string wk_str = "";

        int list_seq = 1;
        list_seq = Load_User_CMDList_DropDownList1.SelectedIndex;
        if (list_seq < 0)
        {
            Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Red;
            Save_CMDs_Status_Label.Text = "NO LIST TO DELETE";
        }
        else
        {
            wk_str = "!" + CMD_Verification_TextBox1.Text.ToString().ToUpper().Trim();
            if (!wk_str.Equals("!DELETE"))
            {
                Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Red;

                Save_CMDs_Status_Label.Text = "REJECTED:  MUST TYPE DELETE IN BOX TO VERIFY";

            }
            else
            {
                Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Black;

                wk_str = Load_User_CMDList_DropDownList1.SelectedValue.ToString()
                + ": " + Load_User_CMDList_DropDownList1.SelectedItem.ToString();
                // Save_UserLists...;
                Save_CMDs_Status_Label.Text = "Deleting List " + listtype + " " + wk_str;
                Delete_Saved_User_List(listtype);

            }
        }

        Save_CMDs_Status_Label.DataBind();
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Button_Delete_CMD_List_Click

    protected void Button_Delete_PCTY_List_Click(object sender, EventArgs e)
    {
        string listtype = "PCTY";
        string wk_str = "";

        int list_seq = 1;
        list_seq = Load_User_PCTYList_DropDownList1.SelectedIndex;
        if (list_seq < 0)
        {
            Save_PCTYs_Status_Label.ForeColor = System.Drawing.Color.Red;
            Save_PCTYs_Status_Label.Text = "NO LIST TO DELETE";
        }
        else
        {
            wk_str = "!" + PCTY_Verification_TextBox1.Text.ToString().ToUpper().Trim();
            if (!wk_str.Equals("!DELETE"))
            {
                Save_PCTYs_Status_Label.ForeColor = System.Drawing.Color.Red;

                Save_PCTYs_Status_Label.Text = "REJECTED:  MUST TYPE DELETE IN BOX TO VERIFY";

            }
            else
            {
                Save_PCTYs_Status_Label.ForeColor = System.Drawing.Color.Black;

                wk_str = Load_User_PCTYList_DropDownList1.SelectedValue.ToString()
                + ": " + Load_User_PCTYList_DropDownList1.SelectedItem.ToString();
                // Save_UserLists...;
                Save_PCTYs_Status_Label.Text = "Deleting List " + listtype + " " + wk_str;
                Delete_Saved_User_List(listtype);

            }
        }

        Save_PCTYs_Status_Label.DataBind();
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Button_Delete_PCTY_List_Click

    protected void Button_Delete_RCTY_List_Click(object sender, EventArgs e)
    {
        string listtype = "RCTY";
        string wk_str = "";

        int list_seq = 1;
        list_seq = Load_User_RCTYList_DropDownList1.SelectedIndex;
        if (list_seq < 0)
        {
            Save_RCTYs_Status_Label.ForeColor = System.Drawing.Color.Red;
            Save_RCTYs_Status_Label.Text = "NO LIST TO DELETE";
        }
        else
        {
            wk_str = "!" + RCTY_Verification_TextBox1.Text.ToString().ToUpper().Trim();
            if (!wk_str.Equals("!DELETE"))
            {
                Save_RCTYs_Status_Label.ForeColor = System.Drawing.Color.Red;

                Save_RCTYs_Status_Label.Text = "REJECTED:  MUST TYPE DELETE IN BOX TO VERIFY";

            }
            else
            {
                Save_RCTYs_Status_Label.ForeColor = System.Drawing.Color.Black;

                wk_str = Load_User_RCTYList_DropDownList1.SelectedValue.ToString()
                + ": " + Load_User_RCTYList_DropDownList1.SelectedItem.ToString();
                // Save_UserLists...;
                Save_RCTYs_Status_Label.Text = "Deleting List " + listtype + " " + wk_str;
                Delete_Saved_User_List(listtype);

            }
        }

        Save_RCTYs_Status_Label.DataBind();
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Button_Delete_RCTY_List_Click


    protected void Button_Delete_STATE_List_Click(object sender, EventArgs e)
    {
        string listtype = "STATE";
        string wk_str = "";

        int list_seq = 1;
        list_seq = Load_User_STATEList_DropDownList1.SelectedIndex;
        if (list_seq < 0)
        {
            Save_STATEs_Status_Label.ForeColor = System.Drawing.Color.Red;
            Save_STATEs_Status_Label.Text = "NO LIST TO DELETE";
        }
        else
        {
            wk_str = "!" + STATE_Verification_TextBox1.Text.ToString().ToUpper().Trim();
            if (!wk_str.Equals("!DELETE"))
            {
                Save_STATEs_Status_Label.ForeColor = System.Drawing.Color.Red;

                Save_STATEs_Status_Label.Text = "REJECTED:  MUST TYPE DELETE IN BOX TO VERIFY";

            }
            else
            {
                Save_STATEs_Status_Label.ForeColor = System.Drawing.Color.Black;

                wk_str = Load_User_STATEList_DropDownList1.SelectedValue.ToString()
                + ": " + Load_User_STATEList_DropDownList1.SelectedItem.ToString();
                // Save_UserLists...;
                Save_STATEs_Status_Label.Text = "Deleting List " + listtype + " " + wk_str;
                Delete_Saved_User_List(listtype);

            }
        }

        Save_STATEs_Status_Label.DataBind();
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Button_Delete_STATE_List_Click


    protected void Button_Save_STATEs_Click(object sender, EventArgs e)
    {
        string listtype = "STATE";
        listtype = SubmitList_User_STATE_Processing_Label1.Text.ToString().ToUpper();
        int resave_list = 0;
        if (Resave_STATE_Load_CkBox.Checked)
            resave_list = 1;
        // Save_UserLists...;
        Save_STATEs_Status_Label.Text = "Calling Save_User_List with " + listtype;
        Save_STATEs_Status_Label.DataBind();
        Save_User_List(listtype, resave_list);
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Button_Save_CMDs
    protected void Button_Save_SPIs_Click(object sender, EventArgs e)
    {
        string listtype = "SPI";
        int resave_list = 0;
        // Save_UserLists...;
        Save_User_List(listtype, resave_list);
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;
    } // end Button_Save_CMDs

    protected void Run_Job_Async_Btn_Click(object sender, EventArgs e)
    {
        string wkuser = User_ID_Valid_Label1.Text.ToUpper();
        Job_Data_Gridview_Label.Visible = true;
        if (wkuser.Equals("ANONYMOUS", StringComparison.InvariantCultureIgnoreCase))
        {
            Run_Status_Label1.Text = User_ID_Valid_Label1.Text.ToString() + ": !!! ERROR MUST LOGIN, ANONYMOUS CANNOT SUBMIT JOBS !!!";
            Run_Status_Label1.ForeColor = System.Drawing.Color.Red;
            Run_Job_Async_Btn.Enabled = true;
            Run_Status_Label1.DataBind();


        }
        else
        {
            Run_Status_Label1.Text = "PLEASE WAIT--PROCESSING REQUEST";
            Run_Status_Label1.ForeColor = System.Drawing.Color.Red;
            Run_Status_Label1.DataBind();
            Write_Parmfile(sender, e);
            Process_SS_Async(sender, e);
            Last_JobNum_Label1.Text = Job_Num_Str;
            GoToAnchor(this.Page, "TPISDataTable");
            Job_List2_DropDownList.DataBind();

            Run_Status_Label1.Text = "JOB " + Job_Num_Str + " SUBMITTED";
            Run_Status_Label1.ForeColor = System.Drawing.Color.Green;
            Run_Job_Async_Btn.Enabled = true;
            Run_Status_Label1.DataBind();

        }
        Run_Job_Async_Btn.Enabled = true;
    }


    protected void Load_HTML_Btn_Click(object sender, EventArgs e)
    {
        //User_JobNum_TextBox1.Text=Last_JobNum_Label1.Text.ToSring() ;
        Write_Parmfile(sender, e);
        Process_SSAnalytics(sender, e);
        GoToAnchor(this.Page, "TPISDataTable");
        Job_List2_DropDownList.DataBind();

    }


    protected void Show_Retrieved_HTML_Btn_Click(object sender, EventArgs e)
    {
        string wk_job_str = "";
        int ck_jnum = 0;

        wk_job_str = Job_List2_DropDownList.SelectedValue.ToString();
        if (Int32.TryParse(wk_job_str, out ck_jnum))
        {
            if (ck_jnum > 100000 && ck_jnum < 999999)
            {
                Job_Num_Str = ck_jnum.ToString();

                User_JobNum_TextBox1.Text = Job_Num_Str;
            }

        }
        try
        {
            string SAS_KEY = "?sp=rl&st=2021-08-16T17:58:47Z&se=2024-01-01T04:01:00Z&sv=2020-08-04&sr=c&sig=8tuwRFlus8Cgawq2%2FnqqtmiQ%2F6XaDVpxJUaMN3hBhIs%3D";
            string wk_url = "https://tpis1.trade.gov/TPIS_JOBS/j133621.html";
            WebClient client = new WebClient();
            string wk_url2 = "https://tpisprod.blob.core.windows.net/$web/job_data/j" + wk_job_str + "_ts.html";
            wk_url = wk_url2 + SAS_KEY;

            string html_job = client.DownloadString(wk_url);
            Table_Fmt1_Literal.Text = html_job.ToString();

        }
        catch
        {
            Show_Retrieved_HTML(sender, e);

        }

        //GoToAnchor(this.Page, "TPISDataTable") ;

    }
    protected void Refresh_Job_List_Click(object sender, EventArgs e)
    {
        Job_List2_DropDownList.DataBind();
        //GoToAnchor(this.Page, "TPISDataTable") ;
        Run_Job_Update_Panel.Update();

    }
    protected void Delete_Job_Btn_Click(object sender, EventArgs e)
    {
        string wk_job_str = "";
        int ck_jnum = 0;
        if (Selected_JobDelete_Panel.Visible == false)
        {
            Selected_JobDelete_Panel.Visible = true;
            Selected_JobDelete_Panel.Height = 700;

            // 
            //Delete_Job_Btn.Text = "Delete Selected Jobs";
            //Delete_Job_Btn.DataBind();
            Delete_Jobs_chkbox.Checked = false;
            Refresh_Job_List_Click(sender, e);

        }
        else if (Delete_Jobs_chkbox.Checked)
        {
            Selected_JobDelete_Panel.Visible = false;
            Selected_JobDelete_Panel.Height = 30;
            //Delete_Jobs_chkbox.Checked = false;
            //Delete_Job_Btn.Text = "Select Jobs to Delete";
            Refresh_Job_List_Click(sender, e);
        }

        wk_job_str = Job_List2_DropDownList.SelectedValue.ToString();
        if (Int32.TryParse(wk_job_str, out ck_jnum))
        {
            if (ck_jnum > 100000 && ck_jnum < 999999)
            {
                Job_Num_Str = ck_jnum.ToString();

                // User_JobNum_TextBox1.Text = Job_Num_Str;
            }

        }
        // Show_Retrieved_HTML(sender, e);

        //GoToAnchor(this.Page, "TPISDataTable") ;

    }

    protected void Show_TPIS_Jobs_Btn_Click(object sender, EventArgs e)
    {
        string wk_user = "";
        string wk_redirect = "";
        string wk_SASKEY = "";
        string wk_jobtable_name = "";
        string wk_source = "DEV";
        string wk_url = "";
        // dev sas key read list only expires 2024/01/01
        wk_SASKEY = "https://tpisprod.blob.core.windows.net/$web?sp=rl&st=2021-08-16T17:58:47Z&se=2024-01-01T04:01:00Z&sv=2020-08-04&sr=c&sig=8tuwRFlus8Cgawq2%2FnqqtmiQ%2F6XaDVpxJUaMN3hBhIs%3D";

        wk_SASKEY = "sp=rl&st=2021-08-16T17:58:47Z&se=2024-01-01T04:01:00Z&sv=2020-08-04&sr=c&sig=8tuwRFlus8Cgawq2%2FnqqtmiQ%2F6XaDVpxJUaMN3hBhIs%3D";
        if (String.Equals(wk_source, "DEV"
                   , StringComparison.OrdinalIgnoreCase))
        {
            wk_SASKEY = "https://tpisdev.blob.core.windows.net/$web?sp=rl&st=2021-10-20T21:06:03Z&se=2024-01-01T06:06:03Z&spr=https&sv=2020-08-04&sr=c&sig=O670IBs5wYrPo67SJNkDgyrFp6M6gLi2eYjWnrIJqmE%3D";

            wk_SASKEY = "sp=UNKNOWNrl&st=2021-10-20T21:06:03Z&se=2024-01-01T06:06:03Z&spr=https&sv=2020-08-04&sr=c&sig=O670IBs5wYrPo67SJNkDgyrFp6M6gLi2eYjWnrIJqmE%3D";
        }
        wk_SASKEY = "sp=rl&st=2021-08-16T17:58:47Z&se=2024-01-01T04:01:00Z&sv=2020-08-04&sr=c&sig=8tuwRFlus8Cgawq2%2FnqqtmiQ%2F6XaDVpxJUaMN3hBhIs%3D";

        wk_user = User_ID_Valid_Label1.Text.ToLower();

        wk_jobtable_name = "tpis_" + wk_user.ToLower() + "_job_list.html";

        wk_redirect = "https://tpisprod.blob.core.windows.net/$web/tpis_jobs_table/" + wk_jobtable_name + "?" + wk_SASKEY;


        Response.Redirect(wk_redirect);

        // Job_List2_DropDownList.DataBind() ;
        //GoToAnchor(this.Page, "TPISDataTable") ;

    }


    //***********************************  process formdata

    protected void Insert_Webform_Data_Btn_Click(object sender, EventArgs e)
    {

        Write_Parmfile(sender, e);
        //GoToAnchor(this.Page,"TPISCMDSETUP") ;
        Job_List2_DropDownList.DataBind();

    }

    public void Get_TPIS_User_Name(String user_email)
    {

        SqlConnection Conn2;
        string connString;
        string strSQL;
        string wk_user = "POMEROR";
        //int i=0 ;

        string wk_str2 = "";

        //"UID=TPISGUI;Password=Log$me#in2020;" +


        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=US_DATA;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";
        //Job_Num_Str = User_JobNum_TextBox1.Text.ToString();
        //Job_Number_Label1.Text = "Old JOB #: " + Job_Num_Str;
        strSQL = "";
        strSQL = "select Top next value for dbo.tpis_job_seq ";
        SqlCommand sqlcmd = new SqlCommand(strSQL, sqlconn);
        sqlconn.Open();
        Job_Num_Str = Convert.ToString(sqlcmd.ExecuteScalar());

        sqlconn.Close();

        wk_user = User_ID_Valid_Label1.Text.ToString();
        if (String.IsNullOrEmpty(wk_user))
        {
            wk_user = User_ID_DropDownList.SelectedValue.ToString();

        }

    } // end    Get_TPIS_User_Name

    protected void Write_Parmfile(object sender, EventArgs e)
    {
        SqlConnection Conn2;
        string seq_num;
        string connString;
        string strSQL;
        string wk_user = "POMEROR";
        string wk_user_email = "ROGER.POMEROY@TRADE.GOV";
        int Line_Num = 0;
        int J_NUM = 111001;
        int misc_flag = 0;
        string wk_str = "";
        int wk_yr1_int = 0;
        int wk_yr2_int = 0;
        string wk_yr1 = "";
        string wk_yr2 = "";
        int flag_time = 0;

        int cnt_selected_cmds = 0;
        int cnt_subgroups = 0;
        int digit_level = 0;
        int total_all = 0;
        int cnt_loop = 0;
        int num_items = 0;
        int flag_conc_ck = 0;
        int flag_values = 0;
        string conc_ck = "";
        string digits_ck = "";
        string wk_job_name = "";
        string passed_user_id = "";
        string passed_user_email = "";
        //int i=0 ;

        string wk_str2 = "";

        //"UID=TPISGUI;Password=Log$me#in2020;" +


        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=US_DATA;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";
        Job_Num_Str = User_JobNum_TextBox1.Text.ToString();
        Job_Number_Label1.Text = "Old JOB #: " + Job_Num_Str;

        wk_user = User_ID_Valid_Label1.Text.ToString();
        wk_user_email = User_ID_Email_Label1.Text.ToString().ToUpper();

        passed_user_id = SubmitJob_User_ID_Label1.Text.ToString().ToUpper();
        passed_user_email = SubmitJob_User_Email_Label1.Text.ToString().ToUpper();

        if (!String.Equals(passed_user_id, "ANONYMOUS", StringComparison.OrdinalIgnoreCase)
            && !String.IsNullOrEmpty(passed_user_id))
        {
            if (passed_user_email.IndexOf("@TRADE.GOV", 0, StringComparison.OrdinalIgnoreCase) > 0)
            {
                wk_user = passed_user_id;
                wk_user_email = passed_user_email;
            }

        }
        if (String.IsNullOrEmpty(wk_user))
        {
            wk_user = User_ID_DropDownList.SelectedValue.ToString();

        }
        if (Load_Job_Panel.Visible)
        {
            if (!Show_Debug_CheckBox1.Checked)
            {
                Load_Job_Panel.Visible = false;
                Load_Update_Job_Panel.Update();
            }
        }


        if (New_JobNum_chkbox.Checked)
        {
            strSQL = "select next value for dbo.tpis_job_seq ";
            SqlCommand sqlcmd = new SqlCommand(strSQL, sqlconn);
            sqlconn.Open();
            Job_Num_Str = Convert.ToString(sqlcmd.ExecuteScalar());

            sqlconn.Close();
        }
        if (!Int32.TryParse(Job_Num_Str, out J_NUM))
        {
            Job_Num_Str = "111001";
            J_NUM = 111001;
            User_JobNum_TextBox1.Text = Job_Num_Str;

        }


        User_JobNum_TextBox1.Text = Job_Num_Str;
        Job_Number_Label1.Text += " New JOB #: " + Job_Num_Str
        + " J_NUM=" + J_NUM.ToString();
        wk_str = Get_Load_Flag.Text.ToUpper();
        if (wk_str.IndexOf("LOAD") >= 0)
        {
            flag_conc_ck = 1;
            conc_ck = Get_Conc_Text.Text.ToString().ToUpper();
            digits_ck = Get_Digits_Text.Text.ToString().ToUpper();
            if (DataSource_DropDownList1.SelectedIndex >= 0)
            {
                wk_str = DataSource_DropDownList1.SelectedValue.ToString().ToUpper();
            }
            else if (!String.IsNullOrEmpty(conc_ck))
            {

                DataSource_DropDownList1.Items.FindByValue(conc_ck).Selected = true;
            }
        }
        wk_str = "NA";
        wk_str = "insert into dbo.session_gui_fields (job_num, in_type, in_text1, in_text2)";


        strSQL = "select html_text from [US_DATA].[dbo].[job_html) order by line_num ";
        Misc_Textbox2.Text = strSQL;
        Table_SQL1_Literal.Text = strSQL.ToString();
        SubmitJob_User_Processing.Text = "Run " + Job_Num_Str
        + "; user=" + wk_user + " (" + passed_user_id + " : " + passed_user_email
        + "; email=" + wk_user_email;
        try
        {
            DataTable dt = new DataTable();
            //Add columns  
            dt.Columns.Add(new DataColumn("JOB_NUM", typeof(int)));
            dt.Columns.Add(new DataColumn("in_line", typeof(int)));
            dt.Columns.Add(new DataColumn("in_type", typeof(string)));
            dt.Columns.Add(new DataColumn("in_text1", typeof(string)));
            dt.Columns.Add(new DataColumn("in_text2", typeof(string)));
            //Add rows  
            string wk_db_id_str = "US:HS";
            wk_job_name = "US:HS";
            string wk_db_id_name = "U.S. Merchandise Trade";
            wk_db_id_str = Database_Type_Dropdownlist.SelectedValue.ToString();
            wk_db_id_name = Database_Type_Dropdownlist.SelectedItem.Text.ToString();
            Line_Num += 1;
            dt.Rows.Add(J_NUM, Line_Num, "DATAID", wk_db_id_str, null);
            Line_Num += 1;
            dt.Rows.Add(J_NUM, Line_Num, "DATA", wk_db_id_name, null);
            Line_Num += 1;
            dt.Rows.Add(J_NUM, Line_Num, "USER_ID", wk_user, null);
            Line_Num += 1;
            dt.Rows.Add(J_NUM, Line_Num, "USER_EMAIL", wk_user_email, null);

            DateTime time = DateTime.Now;
            string strtime = time.ToString("yyyy/MM/dd HH:mm:ss");
            Line_Num += 1;
            dt.Rows.Add(J_NUM, Line_Num, "DATETIME", strtime, null);

            wk_job_name = Job_Name_TextBox1.Text.ToString();
            if (String.IsNullOrEmpty(wk_job_name))
            {
                wk_job_name = wk_db_id_str + " at " + strtime;
            }
            if (wk_job_name.Length > 100)
            {
                wk_job_name = wk_job_name.Substring(1, 99);
            }


            Line_Num += 1;
            dt.Rows.Add(J_NUM, Line_Num, "DESCRIPTION", wk_job_name, null);
            Job_Name_TextBox1.Text = wk_db_id_str + " @ " + time.ToString("G");

            if (Flow_imports_ckbox.Checked)
            {
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "FLOW", "IMPORTS", null);
                if (wk_db_id_str.IndexOf("UN") >= 0)
                {
                    if (UNFlow_Value_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE", "VALUE", null);
                    }
                    if (UNFlow_Volume_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE", "QUANTITY", null);
                    }

                } /** un flows ***/
                else
                if (wk_db_id_str.IndexOf("USST") >= 0)
                {
                    misc_flag = 0;
                    if (Flow_GenImp_ckbox.Checked) /**|| !Flow_ConImp_ckbox.Checked)**/
                    {
                        misc_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "GENERAL", null);
                    }
                    if (Flow_ConImp_ckbox.Checked)
                    {
                        misc_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "CONSUMPTION", null);
                    }
                    if (misc_flag == 0)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "GENERAL", null);
                        Flow_GenImp_ckbox.Checked = true;
                    }

                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE", "VALUE", null);
                    Line_Num += 1;
                    flag_values = 1;
                    dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "CUSTOMS", null);
                    if (UNFlow_Weight_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "WEIGHT", null);
                    }

                } /** state flows ***/
                else
                { /** us flows ***/


                    misc_flag = 0;
                    if (Flow_GenImp_ckbox.Checked) /**|| !Flow_ConImp_ckbox.Checked)**/
                    {
                        misc_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "GENERAL", null);
                    }
                    if (Flow_ConImp_ckbox.Checked)
                    {
                        misc_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "CONSUMPTION", null);
                    }
                    if (misc_flag == 0)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "GENERAL", null);
                        Flow_GenImp_ckbox.Checked = true;
                    }


                    flag_values = 0;
                    if (Flow_CustVal_ckbox.Checked)
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "CUSTOMS", null);
                    }
                    if (Flow_CifVal_ckbox.Checked)
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "CIF", null);
                    }
                    if (Flow_Qty1_ckbox.Checked)
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "QUANTITY1", null);
                    }
                    if (Flow_Qty2_ckbox.Checked)
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "QUANTITY2", null);
                    }
                    if (Flow_Charges_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "CHARGES", null);
                    }
                    if (Flow_ConImp_ckbox.Checked)
                    {
                        if (Flow_Duty_ckbox.Checked)
                        {
                            Line_Num += 1;
                            dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "DUTY", null);
                        }
                        if (Flow_DutiableVal_ckbox.Checked)
                        {
                            Line_Num += 1;
                            dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "DUTIABLE", null);
                        }
                        if (Flow_LDP_ckbox.Checked)
                        {
                            Line_Num += 1;
                            dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "LANDED COST", null);
                        }
                        if (AVE1_ckbox.Checked) //Flow_ImpUV1_ckbox.Checked
                        {
                            Line_Num += 1;
                            flag_values = 1;
                            dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "AVE1", null);
                        }
                        if (AVE2_ckbox.Checked) //Flow_ImpUV2_ckbox.Checked
                        {
                            Line_Num += 1;
                            flag_values = 1;
                            dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "AVE2", null);
                        }
                    }
                    if (UV1_ckbox.Checked) //Flow_ImpUV1_ckbox.Checked
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "UV1", null);
                    }
                    if (UV1_ckbox.Checked) //Flow_ImpUV2_ckbox.Checked
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "UV2", null);
                    }
                } /** us flows ***/
            } // END IMPORTS
            else
            {
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "FLOW", "EXPORTS", null);
                if (wk_db_id_str.IndexOf("UN") >= 0)
                {
                    if (UNFlow_Value_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE", "VALUE", null);
                    }
                    if (UNFlow_Volume_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE", "QUANTITY", null);
                    }
                    if (UNFlow_Weight_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE", "WEIGHT", null);
                    }

                } /** un flows ***/
                else
                if (wk_db_id_str.IndexOf("USST") >= 0)
                {
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "TOTAL", null);
                    if (UNFlow_Value_ckbox.Checked)
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "FAS/FOB", null);
                    }
                    if (UNFlow_Weight_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "WEIGHT", null);
                    }

                } /** state flows ***/
                else
                { /** us flows ***/
                    if (Flow_DomExp_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "DOMESTIC", null);
                    }
                    if (Flow_ForExp_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "FOREIGN", null);
                    }
                    if (Flow_TotalExp_ckbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOW_US_TYPE", "TOTAL", null);
                    }
                    flag_values = 0;
                    // un and state flows
                    if (Flow_FASVal_ckbox.Checked)
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "FAS/FOB", null);
                    }
                    if (Flow_ExpQty1_ckbox.Checked)
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "QUANTITY1", null);
                    }
                    if (Flow_ExpQty2_ckbox.Checked)
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "QUANTITY2", null);
                    }
                    if (1 == 2) //Flow_ExpUV1_ckbox.Checked
                    {
                        Line_Num += 1;
                        flag_values = 1;
                        dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_DETAIL", "UV1", null);
                    }
                } /** us flows ***/


            } // END EXPORTS
            wk_str = Display_Values_Scale_Dropdown1.SelectedValue;
            if (String.IsNullOrEmpty(wk_str))
            {
                wk_str = "$";
            }
            Line_Num += 1;
            dt.Rows.Add(J_NUM, Line_Num, "FLOWTYPE_VALUE_SCALE", wk_str, null);


            if (Year_ytd_ckbox.Checked)
            {
                flag_time = 1;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS_FREQ", "YEAR-TO-DATE", null);
                if (Show_YTD1_DropDownList.SelectedIndex < 0)
                    Show_YTD1_DropDownList.SelectedIndex = 0;
                if (Show_YTD2_DropDownList.SelectedIndex < 0)
                    Show_YTD2_DropDownList.SelectedIndex = 0;
                wk_yr1 = Show_YTD1_DropDownList.SelectedValue;
                wk_yr2 = Show_YTD2_DropDownList.SelectedValue;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS_Y", wk_yr1, wk_yr2);
                if (int.TryParse(wk_yr1, out wk_yr1_int))
                {
                    if (wk_yr1_int > 1988 && int.TryParse(wk_yr2, out wk_yr2_int))
                    {
                        if (wk_yr2_int > wk_yr1_int)
                        {
                            if (wk_yr2_int - 1 > wk_yr1_int)
                            {
                                wk_yr1_int = wk_yr2_int - 1;
                                wk_yr1 = wk_yr1_int.ToString();
                            }
                            if (Last_2_YTD_CheckBox.Checked)
                            {
                                if (PctChange_CheckBox.Checked)
                                {
                                    wk_str = "PCT CHANGE(" + wk_yr1 + "," + wk_yr2 + ",YTD)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "TIME_CALC", wk_str, null);
                                }
                                if (AbsChange_CheckBox.Checked)
                                {
                                    wk_str = "CHANGE(" + wk_yr1 + "," + wk_yr2 + ",YTD)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "TIME_CALC", wk_str, null);
                                }
                            }

                        } // end yr2> yr1
                    } // end both years are numeric and greater than 1961
                } // end yr1 is numeric
            }
            if (Year_month_ckbox.Checked)
            {
                flag_time = 1;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS_FREQ", "MONTHLY", null);
                if (Show_Month1_DropDownList.SelectedIndex < 0)
                    Show_Month1_DropDownList.SelectedIndex = 0;
                if (Show_Month2_DropDownList.SelectedIndex < 0)
                    Show_Month2_DropDownList.SelectedIndex = 0;
                wk_yr1 = Show_Month1_DropDownList.SelectedValue;
                wk_yr2 = Show_Month2_DropDownList.SelectedValue;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS_M", wk_yr1, wk_yr2);
            }
            if (Year_quarter_ckbox.Checked)
            {
                flag_time = 1;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS_FREQ", "QUARTERLY", null);
                if (Show_Quarter1_DropDownList.SelectedIndex < 0)
                    Show_Quarter1_DropDownList.SelectedIndex = 0;
                if (Show_Quarter2_DropDownList.SelectedIndex < 0)
                    Show_Quarter2_DropDownList.SelectedIndex = 0;
                wk_yr1 = Show_Quarter1_DropDownList.SelectedValue;
                wk_yr2 = Show_Quarter2_DropDownList.SelectedValue;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS_Q", wk_yr1, wk_yr2);
            }
            if (Year_ann_ckbox.Checked || flag_time == 0)
            {

                flag_time = 1;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS_FREQ", "ANNUAL", null);
                if (Show_Year1_DropDownList.SelectedIndex < 0)
                    Show_Year1_DropDownList.SelectedIndex = 0;
                if (Show_Year2_DropDownList.SelectedIndex < 0)
                    Show_Year2_DropDownList.SelectedIndex = 0;
                wk_yr1 = Show_Year1_DropDownList.SelectedValue;
                wk_yr2 = Show_Year2_DropDownList.SelectedValue;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS", wk_yr1, wk_yr2);
                if (int.TryParse(wk_yr1, out wk_yr1_int))
                {
                    if (wk_yr1_int > 1961 && int.TryParse(wk_yr2, out wk_yr2_int))
                    {
                        if (wk_yr2_int <= wk_yr1_int)
                        {
                            Value_Constraint_TextBox.Text = "raange error " + wk_yr1_int.ToString()
                               + "-" + wk_yr2_int.ToString();
                        }
                        else
                        {
                            if (Yr1_LastYr_CheckBox.Checked)
                            {
                                Value_Constraint_TextBox.Text = "1STLAST " + wk_yr1_int.ToString()
                                + "-" + wk_yr2_int.ToString();
                                if (PctChange_CheckBox.Checked)
                                {
                                    wk_str = "PCT CHANGE(" + wk_yr1 + "," + wk_yr2 + ",ANN)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "TIME_CALC", wk_str, null);
                                    Value_Constraint_TextBox.Text = Line_Num.ToString() + ":" + wk_str;
                                }
                                if (AbsChange_CheckBox.Checked)
                                {
                                    wk_str = "CHANGE(" + wk_yr1 + "," + wk_yr2 + ",ANN)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "TIME_CALC", wk_str, null);
                                    Value_Constraint_TextBox.Text = Line_Num.ToString() + ":" + wk_str;
                                }
                                if (wk_yr2_int - 1 > wk_yr1_int && GrRate_CheckBox.Checked)
                                {
                                    wk_str = "GROWTH RATE(" + wk_yr1 + "," + wk_yr2 + ",ANN)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "TIME_CALC", wk_str, null);
                                    Value_Constraint_TextBox.Text = Line_Num.ToString() + ":" + wk_str;
                                }
                            }
                            if (wk_yr2_int - 1 > wk_yr1_int && Last_2Yrs_CheckBox.Checked)
                            {
                                Value_Constraint_TextBox.Text = "LAST 2 " + wk_yr1_int.ToString()
                                + "-" + wk_yr2_int.ToString();
                                wk_yr1_int = wk_yr2_int - 1;
                                wk_yr1 = wk_yr1_int.ToString();
                                if (PctChange_CheckBox.Checked)
                                {
                                    wk_str = "PCT CHANGE(" + wk_yr1 + "," + wk_yr2 + ",ANN)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "TIME_CALC", wk_str, null);
                                    Value_Constraint_TextBox.Text = Line_Num.ToString() + ":" + wk_str;
                                }
                                if (AbsChange_CheckBox.Checked)
                                {
                                    wk_str = "CHANGE(" + wk_yr1 + "," + wk_yr2 + ",ANN)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "TIME_CALC", wk_str, null);
                                    Value_Constraint_TextBox.Text = Line_Num.ToString() + ":" + wk_str;
                                }
                            }
                            if (All_Years_CheckBox.Checked)
                            {
                                Value_Constraint_TextBox.Text = "LAST 2 " + wk_yr1_int.ToString()
                                + "-" + wk_yr2_int.ToString();
                                if (PctChange_CheckBox.Checked)
                                {
                                    wk_str = "PCT CHANGE(" + wk_yr1 + "," + wk_yr2 + ",ALLYRS)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "ALLYRS_CALC", wk_str, null);
                                    Value_Constraint_TextBox.Text = Line_Num.ToString() + ":" + wk_str;
                                }
                                if (AbsChange_CheckBox.Checked)
                                {
                                    wk_str = "CHANGE(" + wk_yr1 + "," + wk_yr2 + ",ALLYRS)";
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, Line_Num, "ALLYRS_CALC", wk_str, null);
                                    Value_Constraint_TextBox.Text = Line_Num.ToString() + ":" + wk_str;
                                }
                            }

                        } // end yr2> yr1
                    } // end both years are numeric and greater than 1961
                    else
                    {
                        Value_Constraint_TextBox.Text = "yr2 error " + wk_yr1 + "-" + wk_yr2;
                    }
                } // end yr1 is numeric
                else
                {
                    Value_Constraint_TextBox.Text = "yr1 error " + wk_yr1 + "-" + wk_yr2;

                }


            } // end annual
            if (Years_In_Rows_ckbox.Checked)
            {
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "YEARS_FMT", "YEARS_IN_ROWS", null);

            }

            if (Rank_CheckBox1.Checked)
            {
                string wk_rankval = "";
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "RANK", "Y", null);
                if (Rank_Year_DropDownList1.SelectedIndex < 0)
                    Rank_Year_DropDownList1.SelectedIndex = 0;
                wk_rankval = Rank_Year_DropDownList1.SelectedValue.ToString();
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "RANK_YEAR", wk_rankval, null);
                if (Rank_Parm_DropDownList1.SelectedIndex < 0)
                    Rank_Parm_DropDownList1.SelectedIndex = 0;
                wk_rankval = Rank_Parm_DropDownList1.SelectedValue.ToString();
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "RANK_VAR", wk_rankval, null);
                wk_rankval = UserRank_TextBox1.Text.ToString();
                if (1 == 2)
                {

                    if (Rank_Top_DropDownList1.SelectedIndex < 0)
                        Rank_Top_DropDownList1.SelectedIndex = 0;
                    wk_rankval = Rank_Top_DropDownList1.SelectedValue.ToString();
                    if (wk_rankval.ToUpper() == "ENTER" && UserRank_CheckBox1.Checked)
                    {
                        wk_rankval = UserRank_TextBox1.Text.ToString();
                    }
                }

                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "RANK_TOP", wk_rankval, null);
            }  // end ranked


            bool product_flag = false;
            bool product_share = false;
            bool total_flag = false;
            bool all_products_flag = false;
            string wk_conc = "";
            int i = 0;

            if (DataSource_DropDownList1.SelectedIndex < 0)
                DataSource_DropDownList1.SelectedIndex = 0;
            wk_conc = DataSource_DropDownList1.SelectedValue.ToString().ToUpper();
            if (flag_conc_ck == 1)
            {
                if (!String.IsNullOrEmpty(conc_ck))
                {
                    wk_conc = conc_ck.ToString().ToUpper();
                }
            }
            CONC_INFO.Text = '<' + wk_conc;
            if (String.IsNullOrEmpty(wk_conc))
                wk_conc = "HS";
            else
            {
                i = wk_conc.IndexOf("US:");
                if (i < 0)
                {
                    i = wk_conc.IndexOf("UN");
                    if (i >= 0)
                    {
                        if (wk_conc.IndexOf("HS") > 0)
                        {
                            wk_conc = "UN:HS";
                        }
                        else if (wk_conc.IndexOf("S1") > 0)
                        {
                            wk_conc = "UN:S1";
                        }
                        else if (wk_conc.IndexOf("S3") > 0)
                        {
                            wk_conc = "UN:S3";
                        }
                        else i = -1;
                    }
                } /*** end un concordance handling ***/
                else
                {
                    if (i < 0)
                        i = wk_conc.IndexOf("TB:");

                    if (i >= 0)
                    {
                        wk_conc = wk_conc.Substring(i + 3);
                    }
                }
                if (String.IsNullOrEmpty(wk_conc))
                    wk_conc = "HS";
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "CMD_CONC", wk_conc, null);
            }
            CONC_INFO.Text = CONC_INFO.Text.ToString() + '>' + wk_conc;

            product_flag = false;
            product_share = false;
            total_flag = false;
            all_products_flag = false;
            if (CMD_TOTAL_ALL_CkBox.Checked)
            {
                all_products_flag = true;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "CMD_ZIN_V", "0", "9");
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "CMD_SHOW", "TOTAL LABEL", null);
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "CMD_OBJ_LABEL", "Total All Merchandise", null);

            }
            if (Display_CMDDETAIL_chkbox.Checked)
            {
                product_flag = true;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "CMD_SHOW", "FULL_DETAIL", null);

            } // end CMD INDIVIDUAL DETAIL
            if (Display_CMDSUB_chkbox.Checked && !all_products_flag)
            {
                product_flag = true;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "CMD_SHOW", "ITEMS BELOW", null);

            } // end commodity checks
            product_share = false;
            if (Display_CMDShare_chkbox.Checked && product_flag)
            {
                product_share = true;
                total_flag = true;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "CMD_SHOW", "SHARE", null);

            } // end commodity checks

            string wk_digits_str = "4";

            if (DIGITS_ID_Dropdown1.SelectedIndex < 0)
                DIGITS_ID_Dropdown1.SelectedIndex = 0;
            wk_digits_str = DIGITS_ID_Dropdown1.SelectedValue.ToString();
            Line_Num += 1;
            dt.Rows.Add(J_NUM, Line_Num, "CMD_DIGITS", wk_digits_str, null);

            //if (Display_CMDTOTAL_chkbox.Checked || product_flag!=1 || product_share==1)
            if (Display_CMDTOTAL_chkbox.Checked)
            {
                total_flag = true;
            }
            if (!product_flag)
            {
                total_flag = true;
            }
            if (total_flag)
            {
                product_flag = true;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "CMD_SHOW", "TOTAL", null);

                if (Display_CMD_TotalLabel_chkbox.Checked)
                {
                    wk_str = CMD_Total_TextBox.Text.ToString();
                    if (!String.IsNullOrEmpty(wk_str))
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "CMD_SHOW", "TOTAL LABEL", null);
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "CMD_OBJ_LABEL", wk_str, null);
                    }

                } // end total lable check

            } // end commodity total check




            cnt_selected_cmds = 0;
            //num_items=Selections_CMD_TextBox.Lines.Length;

            //    for (int line = 0; line < lineCount; line++)
            //        // GetLineText takes a zero-based line index.
            //        lines.Add(textBox.GetLineText(line));

            //string[] strArr = Selections_CMD_TextBox.Text.Split (new string[]{System.Environment.NewLine}, StringSplitOptions.None );
            string[] strArr = Regex.Split(Selections_CMD_TextBox.Text.Trim(), "\n");
            i = 0;
            num_items = 0;
            if (!all_products_flag)
            {  // begin user product selections
                num_items = strArr.Length;
                wk_str = "";
                wk_str2 = "";
                if (num_items == 1)
                {
                    wk_str = strArr[0].ToString();
                    if (string.IsNullOrEmpty(wk_str))
                    {
                        num_items = 0;
                    }
                }
                wk_str2 = "num_items=" + num_items.ToString();
                CMD_SMRY2.Text = "cmd count:" + num_items.ToString();
                Out_label1.Text = wk_str2 + " str1=" + wk_str;
                if (num_items < 1)
                {
                    wk_str = Series_TextBox.Text.ToString().ToUpper();
                    if (String.IsNullOrEmpty(wk_str))
                        num_items = -1;
                    else
                    {
                        Out_label1.Text = wk_str2;
                        cnt_selected_cmds = cnt_selected_cmds + 1;
                        Line_Num += 1;
                        wk_str2 = "";
                        dt.Rows.Add(J_NUM, Line_Num, "CMD_ZIN_V", wk_str, wk_str2);
                        wk_str = "";
                        num_items = 0;

                    }
                } // end no items selected so check Series_TextBox for a value 
                if (num_items < 0)
                {

                    i = Product_Detail1_DropDownList2.SelectedIndex;
                    if (i >= 0)
                    {
                        wk_str = Product_Detail1_DropDownList2.SelectedValue;
                        if (string.IsNullOrEmpty(wk_str))
                            i = -1;
                        else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                            i = -1;
                        if (i == -1)
                        {
                            wk_str2 = wk_str2 + " no detail 2;";
                        }
                        else wk_str2 = wk_str2 + ": " + wk_str;
                    }
                    if (i == -1)
                    {
                        i = Product_Detail1_DropDownList1.SelectedIndex;
                        if (i >= 0)
                        {
                            wk_str = Product_Detail1_DropDownList1.SelectedValue;
                            if (string.IsNullOrEmpty(wk_str))
                                i = -1;
                            else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                i = -1;
                        }
                        if (i == -1)
                        {
                            wk_str2 = wk_str2 + " no detail 1;";
                        }
                        else wk_str2 = wk_str2 + ": " + wk_str;
                    }
                    if (i == -1)
                    {
                        i = Product_DropDownList1.SelectedIndex;
                        if (i < 0)
                        {
                            i = 0;
                            Product_DropDownList1.SelectedIndex = i;
                        }
                        if (i >= 0)
                        {
                            wk_str = Product_DropDownList1.SelectedValue;
                            if (string.IsNullOrEmpty(wk_str))
                                i = -1;
                            else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                i = -1;
                        }
                        if (i == -1)
                        {
                            wk_str2 = wk_str2 + " no product selected 1;";
                        }
                        else wk_str2 = wk_str2 + ": " + wk_str;
                    }
                    Out_label1.Text = wk_str2;
                    cnt_selected_cmds = cnt_selected_cmds + 1;
                    Line_Num += 1;
                    wk_str2 = "";
                    dt.Rows.Add(J_NUM, Line_Num, "CMD_ZIN_V", wk_str, wk_str2);
                    wk_str = "";


                }
                wk_str2 = "";
                wk_str = "";

                //for (int i = 0; i < num_items; i++)
                if (num_items > 0)
                    foreach (string s in strArr)
                    {
                        cnt_loop = cnt_loop + 1;

                        wk_str2 = "";
                        wk_str = "";
                        wk_str = s.ToString().Replace("\n", "").Replace("\r", "");

                        //wk_str=Selected_CMD_ListBox.Items.ToString() ;
                        if (!String.IsNullOrEmpty(wk_str))
                        {

                            if (wk_str.IndexOf("=") == 0)
                            {
                                cnt_subgroups = cnt_subgroups + 1;
                            }
                            else if (String.Equals(wk_str, ".TOTAL"
                                     , StringComparison.OrdinalIgnoreCase))
                            {
                                total_all = 1;
                            }
                            else
                            { // commodities
                                if (wk_str.IndexOf("--") >= 1)
                                {
                                    wk_str = wk_str.Substring(0, wk_str.IndexOf("--"));
                                }
                                else if (wk_str.IndexOf("-") > 1)
                                {
                                    wk_str2 = wk_str.Substring(wk_str.IndexOf("-") + 1);
                                    wk_str = wk_str.Substring(0, wk_str.IndexOf("-"));
                                } // END RANGES
                                else if (wk_str.IndexOf("..") > 1)
                                {
                                    wk_str2 = wk_str.Substring(wk_str.IndexOf("..") + 2);
                                    wk_str = wk_str.Substring(0, wk_str.IndexOf(".."));
                                } // END RANGES
                            } // end selected commodities



                            cnt_selected_cmds = cnt_selected_cmds + 1;
                            Line_Num += 1;
                            dt.Rows.Add(J_NUM, Line_Num, "CMD_ZIN_V", wk_str, wk_str2);
                        } // string not empty

                    } // end loop over products


                Out_label1.Text = "num_items " + num_items.ToString()
                  + " cnt_loop" + cnt_loop.ToString()
                  + " subgroup " + cnt_subgroups.ToString()
                  + " total flag " + total_all.ToString();
            } // end not all_products_flag

            int partner_flag = 0;
            int partner_share = 0;
            bool world_flag = false;
            partner_flag = 0;
            partner_share = 0;
            if (PCTY_TOTAL_ALL_CkBox.Checked)
            {
                world_flag = true;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "PCTY_ZIN_V", ".WORLD", null);
            }

            if (Display_PCTYDETAIL_chkbox.Checked)
            {
                partner_flag = 1;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "PCTY_SHOW", "FULL_DETAIL", null);

            } // end PCTY INDIVIDUAL DETAIL
            if (Display_PCTYSUB_chkbox.Checked)
            {
                partner_flag = 1;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "PCTY_SHOW", "ITEMS BELOW", null);

            } // end partner checks
            if (Display_PCTYShare_chkbox.Checked && partner_flag == 1)
            {
                partner_flag = 1;
                partner_share = 1;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "PCTY_SHOW", "SHARE", null);

            } // end commodity checks

            if (Display_PCTYTOTAL_chkbox.Checked || partner_flag != 1 || partner_share == 1)
            {
                partner_flag = 1;
                Line_Num += 1;
                dt.Rows.Add(J_NUM, Line_Num, "PCTY_SHOW", "TOTAL", null);

                if (Display_PCTY_TotalLabel_chkbox.Checked || partner_flag != 1)
                {
                    wk_str = PCTY_Total_TextBox.Text.ToString();
                    if (!String.IsNullOrEmpty(wk_str))
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "PCTY_SHOW", "TOTAL LABEL", null);
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, "PCTY_OBJ_LABEL", wk_str, null);
                    }

                } // end total lable check

            } // end partner total check

            wk_str2 = "";
            int cnt_selected_pctys = 0;
            cnt_subgroups = 0;
            total_all = 0;

            //string[] pctyArr = Selections_TextBox.Text.Split (new string[]{System.Environment.NewLine}, StringSplitOptions.None );
            if (!PCTY_TOTAL_ALL_CkBox.Checked)
            {

                string[] pctyArr = Regex.Split(Selections_TextBox.Text.Trim(), "\n");
                num_items = pctyArr.Length;
                CTY_SMRY2.Text = "country count:" + num_items.ToString();
                cnt_loop = 0;
                i = 0;
                num_items = pctyArr.Length;
                wk_str = "";
                wk_str2 = "";
                if (num_items == 1)
                {
                    wk_str = pctyArr[0].ToString();
                    if (string.IsNullOrEmpty(wk_str))
                    {
                        num_items = 0;
                    }
                }
                wk_str2 = "num_items=" + num_items.ToString();
                Out_label_pcty1.Text = wk_str2 + " str1=" + wk_str;
                if (num_items < 1)
                {
                    i = CountryGroup_DropDownList.SelectedIndex;
                    if (i < 0) CountryGroup_DropDownList.SelectedIndex = 0;
                    if (i >= 0)
                    {
                        wk_str = CountryGroup_DropDownList.SelectedValue.ToString();
                        if (wk_str.IndexOf("401") >= 0)
                            wk_str = ".WORLD";
                        if (string.IsNullOrEmpty(wk_str))
                            i = -1;
                        else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                            i = -1;
                        if (i == -1)
                        {
                            wk_str = ".WORLD";
                            wk_str2 = wk_str2 + " cty group selected; world assumed " + wk_str;
                        }
                        else wk_str2 = wk_str2 + ": " + wk_str;
                    }
                    Out_label1.Text = wk_str2;
                    cnt_selected_cmds = cnt_selected_cmds + 1;
                    Line_Num += 1;
                    wk_str2 = "";
                    dt.Rows.Add(J_NUM, Line_Num, "PCTY_ZIN_V", wk_str, wk_str2);
                    wk_str = "";
                }
                wk_str2 = "";
                wk_str = "";

                //for (int i = 0; i < num_items; i++)
                if (num_items > 0)
                {
                    foreach (string s2 in pctyArr)
                    {
                        cnt_loop = cnt_loop + 1;

                        wk_str2 = "";
                        wk_str = "";
                        wk_str = s2.ToString();
                        //wk_str=Selected_ListBox.Items.ToString() ;
                        if (!String.IsNullOrEmpty(wk_str))
                        {

                            if (wk_str.IndexOf("=") == 0 || wk_str.IndexOf(".") == 0)
                            {
                                cnt_subgroups = cnt_subgroups + 1;
                            }
                            else if (String.Equals(wk_str, ".WORLD"
                                      , StringComparison.OrdinalIgnoreCase))
                            {
                                total_all = 1;
                            }

                            cnt_selected_pctys = cnt_selected_pctys + 1;
                            Line_Num += 1;
                            dt.Rows.Add(J_NUM, Line_Num, "PCTY_ZIN_V", wk_str, null);
                        } // string not empty


                    }  // end loop writing pctys selections

                } // end num_items >0
                Out_label_pcty1.Text = "num_items " + num_items.ToString()
                  + " cnt_loop" + cnt_loop.ToString()
                  + " subgroup " + cnt_subgroups.ToString()
                  + " total flag " + total_all.ToString();
            } // end partner country selections (NOT .WORLD)

            //*****************************************************
            //*** REPORTER COUNTRY PROCESSING 
            //*****************************************************

            int reporter_flag = 0;
            int reporter_share = 0;
            world_flag = false;
            reporter_flag = 0;
            reporter_share = 0;
            if (Reporter_Panel.Visible)
            {
                if (RCTY_TOTAL_ALL_CkBox.Checked)
                {
                    world_flag = true;
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, "RCTY_ZIN_V", ".WORLD", null);
                }

                if (Display_RCTYDETAIL_chkbox.Checked)
                {
                    reporter_flag = 1;
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, "RCTY_SHOW", "FULL_DETAIL", null);

                } // end RCTY INDIVIDUAL DETAIL
                if (Display_RCTYSUB_chkbox.Checked)
                {
                    reporter_flag = 1;
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, "RCTY_SHOW", "ITEMS BELOW", null);

                } // end reporter checks
                if (Display_RCTYShare_chkbox.Checked && reporter_flag == 1)
                {
                    reporter_flag = 1;
                    reporter_share = 1;
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, "RCTY_SHOW", "SHARE", null);

                } // end commodity checks

                if (Display_RCTYTOTAL_chkbox.Checked || reporter_flag != 1 || reporter_share == 1)
                {
                    reporter_flag = 1;
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, "RCTY_SHOW", "TOTAL", null);

                    if (Display_RCTY_TotalLabel_chkbox.Checked || reporter_flag != 1)
                    {
                        wk_str = RCTY_Total_TextBox.Text.ToString();
                        if (!String.IsNullOrEmpty(wk_str))
                        {
                            Line_Num += 1;
                            dt.Rows.Add(J_NUM, Line_Num, "RCTY_SHOW", "TOTAL LABEL", null);
                            Line_Num += 1;
                            dt.Rows.Add(J_NUM, Line_Num, "RCTY_OBJ_LABEL", wk_str, null);
                        }

                    } // end total lable check

                } // end reporter total check

                wk_str2 = "";
                int cnt_selected_rctys = 0;
                cnt_subgroups = 0;
                total_all = 0;

                //string[] rctyArr = Selections_TextBox.Text.Split (new string[]{System.Environment.NewLine}, StringSplitOptions.None );
                if (!RCTY_TOTAL_ALL_CkBox.Checked)
                {

                    string[] rctyArr = Regex.Split(RCTY_Selections_TextBox.Text.Trim(), "\n");
                    num_items = rctyArr.Length;
                    CTY_SMRY2.Text = "country count:" + num_items.ToString();
                    cnt_loop = 0;
                    i = 0;
                    num_items = rctyArr.Length;
                    wk_str = "";
                    wk_str2 = "";
                    if (num_items == 1)
                    {
                        wk_str = rctyArr[0].ToString();
                        if (string.IsNullOrEmpty(wk_str))
                        {
                            num_items = 0;
                        }
                    }
                    wk_str2 = "num_items=" + num_items.ToString();
                    Out_label_RCTY1.Text = wk_str2 + " str1=" + wk_str;
                    if (num_items < 1)
                    {
                        i = RCTY_CountryGroup_DropDownList.SelectedIndex;
                        if (i < 0) RCTY_CountryGroup_DropDownList.SelectedIndex = 0;
                        if (i >= 0)
                        {
                            wk_str = RCTY_CountryGroup_DropDownList.SelectedValue.ToString();
                            if (wk_str.IndexOf("401") >= 0)
                                wk_str = ".WORLD";
                            if (string.IsNullOrEmpty(wk_str))
                                i = -1;
                            else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                i = -1;
                            if (i == -1)
                            {
                                wk_str = ".WORLD";
                                wk_str2 = wk_str2 + " cty group selected; world assumed " + wk_str;
                            }
                            else wk_str2 = wk_str2 + ": " + wk_str;
                        }
                        Out_label1.Text = wk_str2;
                        cnt_selected_cmds = cnt_selected_cmds + 1;
                        Line_Num += 1;
                        wk_str2 = "";
                        dt.Rows.Add(J_NUM, Line_Num, "RCTY_ZIN_V", wk_str, wk_str2);
                        wk_str = "";
                    }
                    wk_str2 = "";
                    wk_str = "";

                    //for (int i = 0; i < num_items; i++)
                    if (num_items > 0)
                    {
                        foreach (string s2 in rctyArr)
                        {
                            cnt_loop = cnt_loop + 1;

                            wk_str2 = "";
                            wk_str = "";
                            wk_str = s2.ToString();
                            //wk_str=RCTY_Selected_ListBox.Items.ToString() ;
                            if (!String.IsNullOrEmpty(wk_str))
                            {

                                if (wk_str.IndexOf("=") == 0 || wk_str.IndexOf(".") == 0)
                                {
                                    cnt_subgroups = cnt_subgroups + 1;
                                }
                                else if (String.Equals(wk_str, ".WORLD"
                                          , StringComparison.OrdinalIgnoreCase))
                                {
                                    total_all = 1;
                                }

                                cnt_selected_rctys = cnt_selected_rctys + 1;
                                Line_Num += 1;
                                dt.Rows.Add(J_NUM, Line_Num, "RCTY_ZIN_V", wk_str, null);
                            } // string not empty


                        }  // end loop writing rctys selections

                    } // end num_items >0
                    Out_label_RCTY1.Text = "num_items " + num_items.ToString()
                      + " cnt_loop" + cnt_loop.ToString()
                      + " subgroup " + cnt_subgroups.ToString()
                      + " total flag " + total_all.ToString();
                } // end reporter country selections (NOT .WORLD)

                //*********************************************
                //*********** END REPORTER COUNTRY PROCESSING 
                //*********************************************
            } // end reporter panel visible

            int i_len = 0;
            int cnt_selected_states = 0;
            cnt_subgroups = 0;
            total_all = 0;
            int state_flag = 0;
            int state_share = 0;
            int district_flag = -1;
            string wk_region_code = "STATE";
            string wk_out_zin_v = "";
            string wk_out_show = "";
            string wk_out_obj = "";
            string wk_out_other = "";
            string wk_cur_db = "USST";
            wk_cur_db = Current_DBID_Label1.Text.ToString().ToUpper();
            bool usa_flag = false;
            bool show_states = false;
            state_SMRY.Text = "State: False";
            wk_region_code = "STATE";
            wk_out_zin_v = "STATE_ZIN_V";
            wk_out_show = "STATE_SHOW";
            wk_out_obj = "STATE_OBJ_LABEL";
            if (State_Panel.Visible && wk_cur_db.IndexOf("USST") >= 0)
            {
                show_states = true;
                STATE_Out_label1.Text = Data_ID + " State:: True";
                wk_region_code = "STATE";
                wk_out_zin_v = "STATE_ZIN_V";
                wk_out_show = "STATE_SHOW";
                wk_out_obj = "STATE_OBJ_LABEL";
            }
            else if (State_Panel.Visible && wk_cur_db.IndexOf("US") >= 0)
            {
                show_states = true;
                STATE_Out_label1.Text = Data_ID + " District:: True";
                wk_region_code = "DIST";
                wk_out_zin_v = "DIST_ZIN_V";
                wk_out_show = "DIST_SHOW";
                wk_out_obj = "DIST_OBJ_LABEL";
                if (Display_STATEDETAIL_chkbox.Checked)
                {
                    district_flag = 1;
                }
                else if (!STATE_TOTAL_ALL_CkBox.Checked)
                {
                    district_flag = 1;
                }
                else
                {
                    district_flag = -1;
                    show_states = false;
                }

            }

            else
            {
                STATE_Out_label1.Text = Data_ID + " " + wk_region_code + ":: False";
                show_states = false;
            }
            STATE_Out_label1.DataBind();
            state_flag = 0;
            state_share = 0;

            if (show_states)
            {
                STATE_Out_label1.Text = Data_ID + " " + wk_region_code + " b: True";

                if (STATE_TOTAL_ALL_CkBox.Checked)
                {
                    STATE_Out_label1.Text = Data_ID + " " + wk_region_code + " c: total all";
                    usa_flag = true;
                    state_share = 0;

                    if (Display_STATEShare_chkbox.Checked)
                    {
                        state_flag = 1;
                        state_share = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "SHARE", null);

                    } // end state share check
                    Line_Num += 1;

                    dt.Rows.Add(J_NUM, Line_Num, wk_out_zin_v, ".USA", null);
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "TOTAL LABEL", null);
                    Line_Num += 1;
                    if (district_flag == 1)
                    {
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_obj, "USA (All Districts)", null);
                    }
                    else
                    {
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_obj, "USA (All States)", null);
                    }
                    if (Display_STATEShare_chkbox.Checked || Display_STATEDETAIL_chkbox.Checked)
                    {
                        state_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "FULL_DETAIL", null);

                    } // end STATE INDIVIDUAL DETAIL
                    if (Display_STATEShare_chkbox.Checked
                        || Display_STATETOTAL_chkbox.Checked
                        || !Display_STATEDETAIL_chkbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "TOTAL", null);
                    }
                }
                else
                {

                    STATE_Out_label1.Text = Data_ID + " State d: not all button checked";

                    if (Display_STATEDETAIL_chkbox.Checked)
                    {
                        STATE_Out_label1.Text = Data_ID + " State e: detail checked";
                        state_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "FULL_DETAIL", null);

                    } // end STATE INDIVIDUAL DETAIL
                    if (Display_STATESUB_chkbox.Checked)
                    {
                        state_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "ITEMS BELOW", null);

                    } // end state checks
                    if (Display_STATEShare_chkbox.Checked && partner_flag == 1)
                    {
                        state_flag = 1;
                        state_share = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "SHARE", null);

                    } // end commodity checks

                    if (Display_STATETOTAL_chkbox.Checked || state_flag != 1 || state_share == 1)
                    {
                        state_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "TOTAL", null);

                        if (Display_STATE_TotalLabel_chkbox.Checked || state_flag != 1)
                        {
                            wk_str = STATE_Total_TextBox.Text.ToString();
                            if (!String.IsNullOrEmpty(wk_str))
                            {
                                Line_Num += 1;
                                dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "TOTAL LABEL", null);
                                Line_Num += 1;
                                dt.Rows.Add(J_NUM, Line_Num, wk_out_obj, wk_str, null);
                            }

                        } // end total lable check

                    } // end state total check




                } // end not total all states checked


                if (!STATE_TOTAL_ALL_CkBox.Checked)
                {
                    STATE_Out_label1.Text = state_SMRY.Text.ToString() + "; selections ";

                    string[] stateArr = Regex.Split(States_Selections_TextBox.Text.Trim(), "\n");
                    num_items = stateArr.Length;
                    //CTY_SMRY2.Text="country count:" +num_items.ToString() ;
                    cnt_loop = 0;
                    i = 0;
                    num_items = stateArr.Length;
                    wk_str = "";
                    wk_str2 = "";
                    if (num_items == 1)
                    {
                        wk_str = stateArr[0].ToString();
                        if (string.IsNullOrEmpty(wk_str))
                        {
                            num_items = 0;
                        }
                    }
                    wk_str2 = "num_items=" + num_items.ToString();
                    Out_label_state1.Text = wk_str2 + " str1=" + wk_str;
                    if (num_items < 1 && 1 == 2)
                    {
                        i = CountryGroup_DropDownList.SelectedIndex;
                        if (i < 0) CountryGroup_DropDownList.SelectedIndex = 0;
                        if (i >= 0)
                        {
                            wk_str = CountryGroup_DropDownList.SelectedValue.ToString();
                            if (wk_str.IndexOf("401") >= 0)
                                wk_str = ".WORLD";
                            if (string.IsNullOrEmpty(wk_str))
                                i = -1;
                            else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                i = -1;
                            if (i == -1)
                            {
                                wk_str = ".WORLD";
                                wk_str2 = wk_str2 + " cty group selected; world assumed " + wk_str;
                            }
                            else wk_str2 = wk_str2 + ": " + wk_str;
                        }
                        Out_label1.Text = wk_str2;
                        cnt_selected_cmds = cnt_selected_cmds + 1;
                        Line_Num += 1;
                        wk_str2 = "";
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_zin_v, wk_str, wk_str2);
                        wk_str = "";
                    }
                    wk_str2 = "";
                    wk_str = "";

                    //for (int i = 0; i < num_items; i++)
                    if (num_items > 0)
                    {
                        foreach (string s2 in stateArr)
                        {
                            cnt_loop = cnt_loop + 1;

                            wk_str2 = "";
                            wk_str = "";
                            wk_str = s2.ToString();
                            i_len = wk_str.IndexOf(" - ");

                            if (i_len > 1)
                            {
                                wk_str = wk_str.Substring(0, i_len);
                            }
                            //wk_str=Selected_ListBox.Items.ToString() ;
                            if (!String.IsNullOrEmpty(wk_str))
                            {

                                if (wk_str.IndexOf("=") == 0 || wk_str.IndexOf(".") == 0)
                                {
                                    cnt_subgroups = cnt_subgroups + 1;
                                }
                                else if (String.Equals(wk_str, ".WORLD"
                                          , StringComparison.OrdinalIgnoreCase))
                                {
                                    total_all = 1;
                                }

                                cnt_selected_states = cnt_selected_states + 1;
                                Line_Num += 1;
                                dt.Rows.Add(J_NUM, Line_Num, wk_out_zin_v, wk_str, null);
                            } // string not empty


                        } // end loop writing states selections
                    } // end num_items >0
                    Out_label_state1.Text = "num_items " + num_items.ToString()
                      + " cnt_loop" + cnt_loop.ToString()
                      + " subgroup " + cnt_subgroups.ToString()
                      + " total flag " + total_all.ToString();
                } // end state selections (NOT .ALL or .USA)
            } // end if show_state = true

            // spis start

            i_len = 0;
            int cnt_selected_spis = 0;
            cnt_subgroups = 0;
            total_all = 0;
            int spi_share = 0;
            int spi_flag = 0;
            bool allspi_flag = false;
            bool show_spis = false;
            wk_cur_db = Current_DBID_Label1.Text.ToString().ToUpper();
            wk_out_zin_v = "SPI_ZIN_V";
            wk_out_show = "SPI_SHOW";
            wk_out_obj = "SPI_OBJ_LABEL";
            show_spis = false;
            if (SPI_Panel.Visible && wk_cur_db.IndexOf("US") >= 0)
            {
                if (wk_cur_db.IndexOf("USST") >= 0)
                {
                    show_spis = false;
                }
                else
                {
                    show_spis = true;
                }
            }

            if (show_spis)
            {

                if (SPI_TOTAL_ALL_CkBox.Checked)
                {
                    usa_flag = true;
                    spi_share = 0;

                    if (Display_SPIShare_chkbox.Checked)
                    {
                        spi_share = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "SHARE", null);

                    } // end spi share check
                    Line_Num += 1;

                    dt.Rows.Add(J_NUM, Line_Num, wk_out_zin_v, ".ALL", null);
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "TOTAL LABEL", null);
                    Line_Num += 1;
                    dt.Rows.Add(J_NUM, Line_Num, wk_out_obj, "All SPIs", null);
                    if (Display_SPIShare_chkbox.Checked || Display_SPIDETAIL_chkbox.Checked)
                    {
                        spi_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "FULL_DETAIL", null);

                    } // end SPI INDIVIDUAL DETAIL
                    if (Display_SPIShare_chkbox.Checked
                        || Display_SPITOTAL_chkbox.Checked
                        || !Display_SPIDETAIL_chkbox.Checked)
                    {
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "TOTAL", null);
                    }
                }
                else
                {

                    STATE_Out_label1.Text = Data_ID + " State d: not all button checked";

                    if (Display_SPIDETAIL_chkbox.Checked)
                    {
                        STATE_Out_label1.Text = Data_ID + " State e: detail checked";
                        spi_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "FULL_DETAIL", null);

                    } // end SPI INDIVIDUAL DETAIL
                    if (Display_SPISUB_chkbox.Checked)
                    {
                        spi_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "ITEMS BELOW", null);

                    } // end spi checks
                    if (Display_SPIShare_chkbox.Checked && partner_flag == 1)
                    {
                        spi_flag = 1;
                        spi_share = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "SHARE", null);

                    } // end commodity checks

                    if (Display_SPITOTAL_chkbox.Checked || spi_flag != 1 || spi_share == 1)
                    {
                        spi_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "TOTAL", null);

                        if (Display_SPI_TotalLabel_chkbox.Checked || spi_flag != 1)
                        {
                            wk_str = SPI_Total_TextBox.Text.ToString();
                            if (!String.IsNullOrEmpty(wk_str))
                            {
                                Line_Num += 1;
                                dt.Rows.Add(J_NUM, Line_Num, wk_out_show, "TOTAL LABEL", null);
                                Line_Num += 1;
                                dt.Rows.Add(J_NUM, Line_Num, wk_out_obj, wk_str, null);
                            }

                        } // end total lable check

                    } // end spi total check




                } // end not total all spis checked


                if (!SPI_TOTAL_ALL_CkBox.Checked)
                {
                    string[] spiArr = Regex.Split(SPIs_Selections_TextBox.Text.Trim(), "\n");
                    num_items = spiArr.Length;
                    //SPI_SMRY2.Text="spi count:" +num_items.ToString() ;
                    cnt_loop = 0;
                    i = 0;
                    num_items = spiArr.Length;
                    wk_str = "";
                    wk_str2 = "";
                    if (num_items == 1)
                    {
                        wk_str = spiArr[0].ToString();
                        if (string.IsNullOrEmpty(wk_str))
                        {
                            num_items = 0;
                        }
                    }
                    wk_str2 = "num_items=" + num_items.ToString();
                    Out_label_spi1.Text = wk_str2 + " str1=" + wk_str;
                    wk_str2 = "";
                    wk_str = "";

                    //for (int i = 0; i < num_items; i++)
                    if (num_items > 0)
                    {
                        foreach (string s2 in spiArr)
                        {
                            cnt_loop = cnt_loop + 1;

                            wk_str2 = "";
                            wk_str = "";
                            wk_str = s2.ToString();
                            i_len = wk_str.IndexOf(" - ");

                            if (i_len > 0 && i_len < 5)
                            {
                                wk_str = wk_str.Substring(0, i_len);
                            }
                            //wk_str=Selected_ListBox.Items.ToString() ;
                            if (!String.IsNullOrEmpty(wk_str))
                            {

                                if (wk_str.IndexOf("=") == 0 || wk_str.IndexOf(".") == 0)
                                {
                                    cnt_subgroups = cnt_subgroups + 1;
                                }
                                else if (String.Equals(wk_str, ".WORLD"
                                          , StringComparison.OrdinalIgnoreCase))
                                {
                                    total_all = 1;
                                }

                                cnt_selected_spis = cnt_selected_spis + 1;
                                Line_Num += 1;
                                dt.Rows.Add(J_NUM, Line_Num, wk_out_zin_v, wk_str, null);
                            } // string not empty


                        } // end loop writing spis selections
                    } // end num_items >0
                    Out_label_spi1.Text = "num_items " + num_items.ToString()
                      + " cnt_loop" + cnt_loop.ToString()
                      + " subgroup " + cnt_subgroups.ToString()
                      + " total flag " + total_all.ToString();
                } // end spi selections (NOT .ALL )
            } // end if show_spi = true

            // spis end 

            //sqlcon as SqlConnection  
            if (1 == 1)
            {
                Job_Data_GridView.Visible = true;
                Job_Data_GridView.DataSource = dt;
                Job_Data_GridView.DataBind();
            }

            SqlCommand sqlcom = new SqlCommand("dbo.spt_tpis_jobparms", sqlconn);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.AddWithValue("@insertjobdata", dt);
            sqlcom.Parameters.AddWithValue("@JOBNUM", J_NUM);
            sqlconn.Open();
            sqlcom.ExecuteNonQuery();
            sqlconn.Close();
            // return "Hello" ;
        } // end try
        catch (Exception ex)
        {
            if (wk_user.ToUpper() == "POMEROR" || wk_user.ToUpper() == "TPISTEST")
            {
                Table_Fmt1_Literal.Text = strSQL + " loading error " + ex.Message;
                Table_Fmt1_Literal.Visible = true;
                //Show_Table_Panel.Visible=true ;                       
                //Show_Table_Panel.Style["display"]="block" ;
                //flag_table_errors=1 ;
            }
            else
            {
                Table_Fmt1_Literal.Text = "ERROR CREATING FORMATTED DATA TABLE-TRY REFRESHING PAGE OR SELECTING OTHER INFORMATION";

            }
            sqlconn.Close();
        } // end catch


        // Process_SSAnalytics(sender, e);
    }  // End Write_Parm

    protected void Process_SSAnalytics(object sender, EventArgs e)
    {
        SqlConnection Conn2;
        string seq_num;
        string connString;
        string strSQL;
        string wk_user = "POMEROR";
        string wk_user_email = "ROGER.POMEROY@TRADE.GOV";

        int J_NUM = 111001;
        Job_Num_Str = User_JobNum_TextBox1.Text.ToString();
        if (!Int32.TryParse(Job_Num_Str, out J_NUM))
        {
            Job_Num_Str = "111001";
            J_NUM = 111001;
            User_JobNum_TextBox1.Text = Job_Num_Str;

        }

        // "UID=TPISGUI;Password=Log$me#in2020;" +

        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=tcp:ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=US_DATA;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";

        string wk_str = "NA";

        strSQL = "select html_text from dbo.job_html "
        + " where job_num=" + Job_Num_Str + " order by line_num "
        + " order by line_num ";
        Misc_Textbox2.Text = strSQL;
        Table_SQL1_Literal.Text = strSQL.ToString();
        if (1 == 2)
        {
            try
            {


                sqlconn.Open();
                if (1 == 2)
                {
                    using (SqlCommand jobcmd = new SqlCommand("dbo.spt_build_parms_iuPROD_qty_AZ", sqlconn))
                    {
                        jobcmd.CommandType = CommandType.StoredProcedure;
                        jobcmd.Parameters.Add("@JOBNUM", SqlDbType.Int).Value = J_NUM;
                        jobcmd.Parameters.Add("@PRINTYN", SqlDbType.Int).Value = 0;
                        jobcmd.ExecuteNonQuery();


                    }
                }

                string query = "select html_text from dbo.job_html where job_num=" + Job_Num_Str + " order by line_num ";

                SqlCommand cmd = new SqlCommand(query, sqlconn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // DataTable dt = new DataTable();
                wk_table_html.Rows.Clear();
                da.Fill(wk_table_html);
                Load_HTML_DataTable();
                Show_Formatted_Panel.Visible = true;
                Show_Formatted_Panel.Style["display"] = "block";
                //wk_table_html.Columns.Clear();
                da.Fill(wk_table_html);
                sqlconn.Close();
                // return "Hello" ;
            }
            catch (Exception ex)
            {
                if (wk_user.ToUpper() == "POMEROR" || wk_user.ToUpper() == "TPISTEST")
                {
                    Table_Fmt1_Literal.Text = strSQL + " loading error " + ex.Message;
                    Table_Fmt1_Literal.Visible = true;
                    //Show_Table_Panel.Visible=true ;                       
                    //Show_Table_Panel.Style["display"]="block" ;
                    //flag_table_errors=1 ;
                }
                else
                {
                    Table_Fmt1_Literal.Text = "ERROR CREATING FORMATTED DATA TABLE-TRY REFRESHING PAGE OR SELECTING OTHER INFORMATION";

                }
                sqlconn.Close();
            }
        } // end skipping
    } // end SS_Get_Session_ID

    protected void Process_SS_Async(object sender, EventArgs e)
    {
        SqlConnection Conn2;
        string seq_num;
        string connString;
        string strSQL;
        string wk_user = "POMEROR";
        string wk_user_email = "ROGER.POMEROY@TRADE.GOV";

        int J_NUM = 111001;
        Job_Num_Str = User_JobNum_TextBox1.Text.ToString();
        if (!Int32.TryParse(Job_Num_Str, out J_NUM))
        {
            Job_Num_Str = "111001";
            J_NUM = 111001;
            User_JobNum_TextBox1.Text = Job_Num_Str;

        }

        // "UID=TPISGUI;Password=Log$me#in2020;" +

        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=tcp:ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=XAGENT;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";

        string wk_str = "NA";

        strSQL = "select html_text from dbo.job_html "
        + " where job_num=" + Job_Num_Str + " order by line_num "
        + " order by line_num ";
        Misc_Textbox2.Text = strSQL;
        Table_SQL1_Literal.Text = strSQL.ToString();
        try
        {


            sqlconn.Open();
            if (1 == 1)
            {
                using (SqlCommand jobcmd = new SqlCommand("dbo.spt_tpis_job_start", sqlconn))
                {
                    jobcmd.CommandType = CommandType.StoredProcedure;
                    jobcmd.Parameters.Add("@JOB_NUM", SqlDbType.Int).Value = J_NUM;
                    jobcmd.ExecuteNonQuery();

                    Table_Fmt1_Literal.Text = "JOB SUBMITTED TO RUN ASYNCRONOUSLY = " + J_NUM.ToString();
                }
            }
            if (1 == 2)
            {
                using (SqlCommand jobcmd = new SqlCommand("dbo.spt_async_run_tpis_null", sqlconn))
                {
                    jobcmd.CommandType = CommandType.StoredProcedure;
                    jobcmd.Parameters.Add("@JOB_NUM", SqlDbType.Int).Value = J_NUM;
                    jobcmd.ExecuteNonQuery();


                }
            }
            sqlconn.Close();
            // return "Hello" ;
        }
        catch (Exception ex)
        {
            if (wk_user.ToUpper() == "POMEROR" || wk_user.ToUpper() == "TPISTEST")
            {
                Table_Fmt1_Literal.Text = strSQL + " loading error " + ex.Message;
                Table_Fmt1_Literal.Visible = true;
                //Show_Table_Panel.Visible=true ;                       
                //Show_Table_Panel.Style["display"]="block" ;
                //flag_table_errors=1 ;
            }
            else
            {
                Table_Fmt1_Literal.Text = "ERROR SUBMITTING JOB TO RUN ASYNCHRONOUSLY";

            }
            sqlconn.Close();
        }


    } // end process_async


    protected void Show_Retrieved_HTML(object sender, EventArgs e)
    {
        SqlConnection Conn2;
        string seq_num;
        string connString;
        string strSQL;
        string wk_user = "POMEROR";

        int J_NUM = 111001;


        Job_Num_Str = User_JobNum_TextBox1.Text.ToString();
        if (!Int32.TryParse(Job_Num_Str, out J_NUM))
        {
            Job_Num_Str = User_JobNum_TextBox1.Text.ToString();
            if (!Int32.TryParse(Job_Num_Str, out J_NUM))
            {
                Job_Num_Str = "111001";
                J_NUM = 111001;
                User_JobNum_TextBox1.Text = Job_Num_Str;
            }

        }

        // "UID=TPISGUI;Password=Log$me#in2020;" +

        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=tcp:ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=US_DATA;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";

        string wk_str = "NA";

        strSQL = "select html_text from dbo.job_html "
        + " where job_num=" + Job_Num_Str + " order by line_num "
        + " order by line_num ";
        Misc_Textbox2.Text = strSQL;
        Table_SQL1_Literal.Text = strSQL.ToString();
        try
        {


            sqlconn.Open();

            string query = "select html_text from dbo.job_html where job_num=" + Job_Num_Str + " order by line_num ";

            SqlCommand cmd = new SqlCommand(query, sqlconn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // DataTable dt = new DataTable();
            wk_table_html.Rows.Clear();
            da.Fill(wk_table_html);
            Load_HTML_DataTable();
            Show_Formatted_Panel.Visible = true;
            Show_Formatted_Panel.Style["display"] = "block";
            //wk_table_html.Columns.Clear();
            da.Fill(wk_table_html);
            sqlconn.Close();
            // return "Hello" ;
        }
        catch (Exception ex)
        {
            if (wk_user.ToUpper() == "POMEROR" || wk_user.ToUpper() == "TPISTEST")
            {
                Table_Fmt1_Literal.Text = strSQL + " loading error " + ex.Message;
                Table_Fmt1_Literal.Visible = true;
                //Show_Table_Panel.Visible=true ;                       
                //Show_Table_Panel.Style["display"]="block" ;
                //flag_table_errors=1 ;
            }
            else
            {
                Table_Fmt1_Literal.Text = "ERROR CREATING FORMATTED DATA TABLE-TRY REFRESHING PAGE OR SELECTING OTHER INFORMATION";

            }
            sqlconn.Close();
        }

    } // end Show_Retrieved_HTML

    protected void Load_HTML_DataTable()
    {
        string str = string.Empty;
        int i = 0;
        str = "<p>load html begin " + "div id=\"FmtContent\" " + "</p>";
        str = "";
        str = str + "<div id=\"FmtContent\" >";

        try
        {
            foreach (DataRow dRow in wk_table_html.Rows)
            {
                i++;
                if (i > 0)
                {
                    str += dRow["HTML_TEXT"];
                }
                else
                {
                    str = " " + dRow["HTML_TEXT"];
                }
            }
            str = str + "</div>";
            if (i > 0)
            {
                Table_Fmt1_Literal.Text = str;
                //Cty_Grid_Label1.Visible=false ;    
                //Prod_Grid_Label1.Visible=false ;             
                //Show_UnFormatted_Panel.Visible=false ;
                //Show_UnFormatted_Panel.Style["display"]="none" ;
                ;
            }
            else
                Table_Fmt1_Literal.Text = str + "<br> No formatted lines to display <br>";


        }
        catch (Exception ex)
        {
            str = "catch" +
            " ERROR OCCURRED LOADING FORMATTED HTML " + ex.Message
            + "</div"
            ;
            Table_Fmt1_Literal.Text = str;
        }

    }



    //*************************************  end data processing

    protected void Check_Passwd_Btn_Click(object sender, EventArgs e)
    {
        string cur_password = "";
        string wk_user = "";
        cur_password = Login_Passwd_TextBox1.Text.ToString();
        Login_Label2.Text = cur_password;
        string wk_str = cur_password;
        string wk_href = " ";
        wk_user = User_ID_DropDownList.SelectedValue.ToString();

        if (wk_user.Equals("POMEROR", StringComparison.InvariantCultureIgnoreCase))
        {
            if (cur_password.Equals("Roger$56", StringComparison.InvariantCultureIgnoreCase))
            {
                User_ID_Valid_Label1.Text = User_ID_DropDownList.SelectedValue.ToString();
                wk_href = "<a href=\"https://tpisprod.blob.core.windows.net/$web/tpis_jobs_table/tpis_pomeror_job_list.html?sp=rl&st=2021-08-16T17:58:47Z&se=2024-01-01T04:01:00Z&sv=2020-08-04&sr=c&sig=8tuwRFlus8Cgawq2%2FnqqtmiQ%2F6XaDVpxJUaMN3hBhIs%3D\" target=\"_blank_\">"
                      + "Show Table of Links to Outputs from Your Completed Jobs</a>";
                Joblink_Literal.Text = wk_href.ToString();
                New_JobNum_chkbox.Visible = true;
                User_JobNum_TextBox1.Visible = true;

            }
            else
            {
                Login_Label2.Text = cur_password + " Invalid Password";
            }

        }
        else
        {
            if (cur_password.Equals("TPIS$otea", StringComparison.InvariantCultureIgnoreCase))
            {
                User_ID_Valid_Label1.Text = User_ID_DropDownList.SelectedValue.ToString();
                wk_href = "<a href=\"https://tpisprod.blob.core.windows.net/$web/tpis_jobs_table/tpis_"
                         + User_ID_Valid_Label1.Text.ToString().ToLower()
                         + "_job_list.html?sp=rl&st=2021-08-16T17:58:47Z&se=2024-01-01T04:01:00Z&sv=2020-08-04&sr=c&sig=8tuwRFlus8Cgawq2%2FnqqtmiQ%2F6XaDVpxJUaMN3hBhIs%3D\" target=\"_blank_\">"
                         + "Show Table of Links to Outputs from Your Completed Jobs</a>";
                Joblink_Literal.Text = wk_href;

            }
            else
            {
                Login_Label2.Text = cur_password + " wrong, try TPIS$otea";
            }

        }
    }


    protected void RemoveAll_States_Button_Click(object sender, EventArgs e)
    {
        //States_Selected_ListBox.Items.Clear();
        States_Selections_TextBox.Text = "";
        //STATE_User_Sel_Name_TextBox1.Text="" ;
        //Select_List_Num_DropDownList.Items.Clear() ;   
        //TextBox1.Text="" ;

        GoToAnchor(this.Page, "TPISSTATESETUP");

    }

    protected void Digits_ID_Dropdown_Change(object sender, EventArgs e)
    {

        int wk_tidx = DIGITS_ID_Dropdown1.SelectedIndex;
        string wk_digits = "";
        string wk_str = "";
        Get_Digits_Text.Text = "";
        if (wk_tidx >= 0)
        {
            Get_Digits_Text.Text = DIGITS_ID_Dropdown1.SelectedValue.ToString();
            //wk_tidx = DIGITS_ID_Dropdown1.Items.IndexOf(DIGITS_ID_Dropdown1.Items.FindByValue(wk_digits));
            wk_digits = DIGITS_ID_Dropdown1.SelectedValue.ToString();
            if (wk_tidx >= 0 && wk_tidx < DIGITS_ID_Dropdown1.Items.Count)
            {
                if (wk_digits == "10")
                {
                    try
                    {

                        if (Product_Detail1_DropDownList2.SelectedIndex + Product_Detail1_DropDownList1.SelectedIndex <= 0)
                        {
                            if (Product_DropDownList1.SelectedIndex <= 0)
                            {
                                Product_DropDownList1.SelectedIndex = 1;
                                Product_DropDownList1.DataBind();
                                wk_str = Product_DropDownList1.SelectedValue.ToString();
                                Series_TextBox.Text = wk_str;
                            } // if total and 10-digits then set to 1st product
                        }
                    }
                    catch (Exception ext10)
                    {
                        Series_TextBox.Text = "1";

                    }
                    Series_TextBox.DataBind();
                } // end special handling 10-digits
                  //DIGITS_ID_Dropdown1.SelectedIndex = wk_tidx;
                  //DIGITS_ID_Dropdown1.DataBind();
                CMD_Ajax_Panel1.Update();
            } // end digits in valid range

        }
        //GoToAnchor(this.Page, "TPISCMDSETUP") ;

    }

    protected void CMD_DETAIL_ckbox_changed(object sender, EventArgs e)
    {
        if (CMD_TOTAL_ALL_CkBox.Checked)
        {
            Selected_CMD_Panel.Visible = false;
            Display_CMDSUB_chkbox.Visible = false;
            Data_CMD_Panel.Height = 200;
            Display_CMDDETAIL_chkbox.Text = "Detail for All Products at the Digit Level Shown";
            Display_CMDTOTAL_chkbox.Text = "Total All Merchandise";
        }
        else if (CMD_SELECTED_CkBox.Checked)
        {
            Selected_CMD_Panel.Visible = true;
            Display_CMDSUB_chkbox.Visible = true;
            Data_CMD_Panel.Height = 830;
            Display_CMDDETAIL_chkbox.Text = "Detail for the Selected Products at the Digit Level Shown";
            Display_CMDTOTAL_chkbox.Text = "Total of Selected Products";
        }

        // GoToAnchor(this.Page, "TPISCMDSETUP") ;
    }

    protected void PCTY_DETAIL_ckbox_changed(object sender, EventArgs e)
    {
        if (PCTY_TOTAL_ALL_CkBox.Checked)
        {
            Selected_Partner_Panel.Visible = false;
            Display_PCTYSUB_chkbox.Visible = false;
            Partner_Panel.Height = 200;
            Display_PCTYTOTAL_chkbox.Text = "World Total";
            Display_PCTYDETAIL_chkbox.Text = "Show All Partners";
        }
        else if (PCTY_SELECTED_CkBox.Checked)
        {
            Selected_Partner_Panel.Visible = true;
            Display_PCTYSUB_chkbox.Visible = true;
            Selections_Panel2.Visible = true;
            PCTY_SaveNew_Panel.Visible = true;
            Partner_Panel.Height = 730;
            Display_PCTYTOTAL_chkbox.Text = "Total of Selected";
            Display_PCTYDETAIL_chkbox.Text = "Show All Partners Selected";
        }

        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }

    protected void STATE_DETAIL_ckbox_changed(object sender, EventArgs e)
    {
        string wk_region = "States";
        string wk_db = Current_DBID_Label1.Text.ToString().ToUpper();
        wk_region = "Districts";
        SubmitList_User_STATE_Processing_Label1.Text = "DIST";
        if (wk_db.IndexOf("USST") >= 0)
        {
            wk_region = "States";
            SubmitList_User_STATE_Processing_Label1.Text = "STATE";
        }

        if (STATE_TOTAL_ALL_CkBox.Checked)
        {
            Selected_STATE_Panel.Visible = false;
            Display_STATESUB_chkbox.Visible = false;
            State_Panel.Height = 150;
            Display_STATEDETAIL_chkbox.Text = "Show All " + wk_region;
            Display_STATETOTAL_chkbox.Text = "Total for USA (All " + wk_region + ")";
        }
        else if (STATE_SELECTED_CkBox.Checked)
        {
            Display_STATESUB_chkbox.Visible = true;
            State_Selections_Panel2.Visible = true;
            Selected_STATE_Panel.Visible = true;
            State_Panel.Height = 730;
            Display_STATEDETAIL_chkbox.Text = "Show All " + wk_region + " Selected";
            Display_STATETOTAL_chkbox.Text = "Total of Selected " + wk_region;
            SubmitList_User_STATE_Processing_Label1.Text = "STATE";

        }
        STATE_Ajax_Panel1.Update();

        // GoToAnchor(this.Page, "TPISCMDSETUP") ;
    }

    protected void CountryGroup_DropDownList_Change(object sender, EventArgs e)
    {
        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }




    protected void StateGroup_DropDownList_Change(object sender, EventArgs e)
    {
        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }


    protected void State_MoveSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in States_Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (States_Selected_ListBox.Visible)
                {
                    States_Selected_ListBox.Items.Add(li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append(li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (States_Selections_TextBox.Text.Length > 0)
                States_Selections_TextBox.Text = States_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                States_Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
        }

    }
    protected void State_MoveNotSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in States_Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (States_Selected_ListBox.Visible)
                {
                    States_Selected_ListBox.Items.Add("!" + li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append("!" + li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (States_Selections_TextBox.Text.Length > 0)
                States_Selections_TextBox.Text = States_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                States_Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
        }

    }
    protected void State_MoveAll_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        foreach (ListItem li in States_Pick_ListBox.Items)
        {
            if (States_Selected_ListBox.Visible)
            {
                States_Selected_ListBox.Items.Add(li.Text);
            }
            if (li.Text.Length > 0)
            {
                builder.Append(li.Text + "\r\n");
            }
        }
        if (builder.Length > 0)
        {
            if (States_Selections_TextBox.Text.Length > 0)
                States_Selections_TextBox.Text += "\r\n" +
                    builder.ToString();
            else
                States_Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;

        }
    }
    protected void State_RemoveAll_Button_Click(object sender, EventArgs e)
    {
        States_Selected_ListBox.Items.Clear();
        States_Selections_TextBox.Text = "";
        STATE_User_Sel_Name_TextBox1.Text = "";
        //Select_List_Num_DropDownList.Items.Clear() ;   
        //States_TextBox1.Text = "";

        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;

    }

    protected void State_NewList_Button_Click(object sender, EventArgs e)
    {

        int num_ctyselections = 0;

        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }



    protected void RCTY_MoveSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in RCTY_Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (RCTY_Selected_ListBox.Visible)
                {
                    RCTY_Selected_ListBox.Items.Add(li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append(li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (RCTY_Selections_TextBox.Text.Length > 0)
                RCTY_Selections_TextBox.Text = RCTY_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                RCTY_Selections_TextBox.Text = builder.ToString();
        }

    }
    protected void RCTY_MoveNotSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+RCTY_ListBox1.Items.Count+"<br />";
        foreach (ListItem li in RCTY_Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (RCTY_Selected_ListBox.Visible)
                {
                    RCTY_Selected_ListBox.Items.Add("!" + li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append("!" + li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (RCTY_Selections_TextBox.Text.Length > 0)
                RCTY_Selections_TextBox.Text = RCTY_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                RCTY_Selections_TextBox.Text = builder.ToString();
        }

    }
    protected void RCTY_MoveAll_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        foreach (ListItem li in RCTY_Pick_ListBox.Items)
        {
            if (RCTY_Selected_ListBox.Visible)
            {
                RCTY_Selected_ListBox.Items.Add(li.Text);
            }
            if (li.Text.Length > 0)
            {
                builder.Append(li.Text + "\r\n");
            }
        }
        if (builder.Length > 0)
        {
            if (RCTY_Selections_TextBox.Text.Length > 0)
                RCTY_Selections_TextBox.Text += "\r\n" +
                    builder.ToString();
            else
                RCTY_Selections_TextBox.Text = builder.ToString();

        }
    }
    protected void RCTY_RemoveAll_Button_Click(object sender, EventArgs e)
    {
        RCTY_Selected_ListBox.Items.Clear();
        RCTY_Selections_TextBox.Text = "";
        RCTY_User_Sel_Name_TextBox1.Text = "";
        //Select_List_Num_DropDownList.Items.Clear() ;   
        //RCTY_TextBox1.Text = "";


    }

    protected void RCTY_NewList_Button_Click(object sender, EventArgs e)
    {

        int num_ctyselections = 0;

    }

    protected void Create_RctyGroup_Click(object sender, EventArgs e)
    {
        string subgroup_name = "";
        string listtype = "RCTY";
        subgroup_name = RCTY_User_Subgroup_Name_TextBox1.Text.ToString();
        if (String.IsNullOrEmpty(subgroup_name))
        {
            subgroup_name = "ERROR--MUST ENTER NAME FOR REPORTER LIST";
        }
        else
        {
            if (RCTY_Selections_TextBox.Text.Length > 0)
                RCTY_Selections_TextBox.Text = RCTY_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                              + "\r\n=" + subgroup_name + "\r\n";
            else
                RCTY_Selections_TextBox.Text = "=" + subgroup_name + "\r\n";
        }

        RCTY_Selections_TextBox.DataBind();
        //Save_RCTYs_Status_Label.Text = "Saving File to " + subgroup_name;
        // Save_UserLists(listtype) ;
    } // end Button_Save_rctys

    protected void End_RctyGroup_Click(object sender, EventArgs e)
    {
        string rctygroup_name = "";
        rctygroup_name = RCTY_User_Subgroup_Name_TextBox1.Text.ToString();
        if (RCTY_Selections_TextBox.Text.Length > 0)
        {
            RCTY_User_Subgroup_Name_TextBox1.Text = "";
            RCTY_Selections_TextBox.Text = RCTY_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                          + "\r\n}\r\n";
            RCTY_User_Subgroup_Name_TextBox1.Text = "";


            Save_RCTYs_Status_Label.Text = "Ended Reporter Group: " + rctygroup_name;

        }
    } // end End_RctyGroup_Click

    protected void RCTY_DETAIL_ckbox_changed(object sender, EventArgs e)
    {
        if (RCTY_TOTAL_ALL_CkBox.Checked)
        {
            Selected_Reporter_Panel.Visible = false;
            Display_RCTYSUB_chkbox.Visible = false;
            Reporter_Panel.Height = 200;
            Display_RCTYTOTAL_chkbox.Text = "World Total";
            Display_RCTYDETAIL_chkbox.Text = "Show All Reporters";
        }
        else if (RCTY_SELECTED_CkBox.Checked)
        {
            Selected_Reporter_Panel.Visible = true;
            RCTY_Selections_Panel2.Visible = true;
            RCTY_SaveNew_Panel.Visible = true;

            Display_RCTYSUB_chkbox.Visible = true;
            Reporter_Panel.Height = 730;
            Display_RCTYTOTAL_chkbox.Text = "Total of Selected";
            Display_RCTYDETAIL_chkbox.Text = "Show All Reporters Selected";
        }
        Reporter_Update_Panel.Update();

        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }

    protected void RCTY_CountryGroup_DropDownList_Change(object sender, EventArgs e)
    {
        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }




    protected void Load_Parms_Btn_Click(object sender, EventArgs e)
    {
        int conc_index = 0;
        string wk_job_str = "";
        int ck_jnum = 0;

        wk_job_str = Job_List2_DropDownList.SelectedValue.ToString();
        if (Int32.TryParse(wk_job_str, out ck_jnum))
        {
            if (ck_jnum > 100000 && ck_jnum < 999999)
            {
                Job_Num_Str = ck_jnum.ToString();

                User_JobNum_TextBox1.Text = Job_Num_Str;
            }

        }
        if (!LoadJob_Smry_Label1.Visible)
        {
            // LoadJob_Smry_Label1.Visible=true ;
            LoadJob_Status_Label1.Visible = true;
        }
        if (Show_Debug_CheckBox1.Checked)
        {
            Load_Job_Panel.Visible = true;
            Load_Update_Job_Panel.Update();
        }
        Load_Complete_Flag = 0;
        Change_Database(sender, e);

        Load_Parmfile(sender, e);

        if (Load_Complete_Flag != 1)
        {
            LoadJob_Status_Label1.Text = "JOB " + Job_Num_Str + " LOAD INCOMPLETE--CLICK LOAD PREVIOUS JOB BUTTON AGAIN";
            Load_Parmfile(sender, e);
        }
        else
        {
            LoadJob_Status_Label1.Text = "LOAD COMPLETE FOR JOB " + Job_Num_Str;

        }

        if (Int32.TryParse(Get_Conc_Index.Text, out conc_index))
        {
            if (conc_index >= 0)
            {

                //DataSource_DropDownList1.SelectedIndex=conc_index ;
                Get_Conc_Index.Text += "::" + conc_index.ToString();
                //DataSource_DropDownList1.DataBind() ;
            }
        }
        LoadJob_Error_Label1.Text = "";

        Run_Job_Update_Panel.Update();
        GoToAnchor(this.Page, "TPISDataTable");
        //Process_SSAnalytics(sender, e) ;

    }

    protected void Change_Database(object sender, EventArgs e)
    {
        string connString;
        string strSQL = "";
        string strSQL2 = "";
        int J_NUM = 111001;
        string wk_str = "";
        string wk_data_id = "";
        string wk_data_name = "";
        wk_data_id = Current_DBID_Label1.Text.ToString().ToUpper();


        //int i=0 ;

        string wk_str2 = "";
        LoadJob_Change_Label1.Text = "L1:Load ";

        // "UID=TPISGUI;Password=Log$me#in2020;" +

        using (SqlConnection sqlconn = new SqlConnection())
        {

            sqlconn.ConnectionString =
            "Data Source=tcp:ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
            "Initial Catalog=US_DATA;" +
            "UID=tpisgui;Password=Log$mein2021;" +
            "Integrated Security=False;";
            Job_Num_Str = User_JobNum_TextBox1.Text.ToString();
            if (!Int32.TryParse(Job_Num_Str, out J_NUM))
            {
                Job_Num_Str = "111001";
                J_NUM = 111001;
                User_JobNum_TextBox1.Text = Job_Num_Str;

            }
            wk_str = "NA";

            LoadJob_Change_Label1.Text = "L1:Load job " + Job_Num_Str + ": ";

            Parmload_Process_Label1.Text = "L1:LP1-- ";


            try
            {
                sqlconn.Open();
                strSQL2 = "SELECT TOP 1 upper(in_text1) as dbaseid "
                + " from dbo.tpis_jobdata where job_num=" + Job_Num_Str
                + " and upper(in_type) = 'DATAID' "
                + " and in_text1 is not null and "
                + " upper(in_text1) in ('US','UNHS','USST','USTB','USS3','USS1') ";

                using (SqlCommand sqlcomDB = new SqlCommand(strSQL2, sqlconn))
                {
                    sqlcomDB.CommandType = CommandType.Text;
                    sqlcomDB.CommandText = strSQL2;
                    using (var reader = sqlcomDB.ExecuteReader())
                    {
                        int ord_dataid = 0;
                        if (!reader.Read())
                        {
                            LoadJob_Smry_Label1.Text += " Read Error";
                            throw new Exception("Read Count Error!");
                        }
                        ord_dataid = reader.GetOrdinal("dbaseid");
                        Data_ID = reader.GetString(ord_dataid);
                    } // using reader

                } // end sqlcomDB
                LoadJob_Change_Label1.Text = "L1:Load job " + Job_Num_Str + "DB CK: |" + Data_ID + "| from "
                  + Current_DBID_Label1.Text.ToString();
                if (!String.IsNullOrEmpty(Data_ID))
                {
                    try
                    {
                        if (!Current_DBID_Label1.Text.ToString().Equals(Data_ID, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ListItem selectedListItem = Database_Type_Dropdownlist.Items.FindByValue(Data_ID);
                            if (selectedListItem != null)
                            {
                                selectedListItem.Selected = true;
                                Current_DBID_Label1.Text = Data_ID.ToString().ToUpper();
                                LoadJob_Change_Label1.Text += " changed to " + Data_ID;
                                Database_Type_Dropdownlist.DataBind();
                                //Database_Type_DropList_Changed(sender, e) ;
                                Main_Update_Panel.Update();
                                Load_New_DB_Env(Data_ID, Job_Num_Str);

                            }
                        }
                    }
                    catch (Exception chg_db)
                    {
                        LoadJob_Change_Label1.Text = "ERROR CHANGE DB TO "
                        + Data_ID + " IS : " + chg_db.Message.ToString();

                    }
                }
            } // end main try
            catch (Exception ljtry)
            {
                LoadJob_Change_Label1.Text += " Error:" + ljtry.Message.ToString();
            }
        } // end sqlconn using

    } // end Change_Database procedure 
    protected void Load_New_DB_Env(String p_dbid, String p_jobnum)
    {
        string wk_str = "";
        wk_str = p_dbid.ToUpper();
        if (String.IsNullOrEmpty(wk_str))
        {
            return;
        }
        if (wk_str.Equals("US", StringComparison.InvariantCultureIgnoreCase))
        {
            Data_Panel_ushs.Visible = true;
            Current_DBID_Label1.Text = wk_str.ToUpper();
            //Data_Panel_ushs.Style["display"]="block";
            Data_Update_Panel_ushs.Update();
            Data_Panel_un_state.Visible = false;
            Data_Update_Panel_un_state.Update();

            Enable_District_Btn.Visible = true;
            Enable_SPIs_Btn.Visible = true;

            State_Header_Label.Text = "Customs Districts Selection:";
            STATE_TOTAL_ALL_CkBox.Text = "All Districts in the USA";
            STATE_SELECTED_CkBox.Text = "Selected Customs Districts";
            SubmitList_User_STATE_Processing_Label1.Text = "DIST";
            STATE_SELECTED_CkBox.Checked = false;
            STATE_TOTAL_ALL_CkBox.Checked = true;
            State_Selections_Panel2.Visible = false;
            Selected_STATE_Panel.Visible = false;
            State_Panel.Height = 150;
            State_Panel.Visible = false;
            STATE_Ajax_Panel1.Update();

            Selected_SPI_Panel.Visible = false;
            SPI_Selections_Panel2.Visible = false;
            SPI_SELECTED_CkBox.Checked = false;
            SPI_TOTAL_ALL_CkBox.Checked = true;
            SPI_Panel.Height = 150;
            SPI_Panel.Visible = false;
            SPIs_Update_Panel.Update();

            Selected_Reporter_Panel.Visible = false;
            RCTY_SELECTED_CkBox.Checked = false;
            RCTY_Selections_Panel2.Visible = false;
            RCTY_TOTAL_ALL_CkBox.Checked = true;
            Reporter_Panel.Height = 200;
            Reporter_Panel.Visible = false;
            Reporter_Update_Panel.Update();

            Data_ID = "USHS";
            Year_month_ckbox.Enabled = true;
            Year_quarter_ckbox.Enabled = true;
            Year_ytd_ckbox.Enabled = true;

            DataSource_DropDownList1.DataBind();
            DIGITS_ID_Dropdown1.DataBind();
            Load_Update_Job_Panel.Update();
        }
        else
        {
            if (wk_str.Equals("USTB", StringComparison.InvariantCultureIgnoreCase))
            {
                Data_Panel_ushs.Visible = true;
                Current_DBID_Label1.Text = wk_str.ToUpper();
                Data_Update_Panel_ushs.Update();
                Data_Panel_un_state.Visible = false;
                Data_Update_Panel_un_state.Update();


                State_Header_Label.Text = "Customs Districts Selection:";
                STATE_TOTAL_ALL_CkBox.Text = "All Districts in the USA";
                STATE_SELECTED_CkBox.Text = "Selected Customs Districts";
                SubmitList_User_STATE_Processing_Label1.Text = "DIST";
                STATE_SELECTED_CkBox.Checked = false;
                STATE_TOTAL_ALL_CkBox.Checked = true;
                State_Selections_Panel2.Visible = false;
                Selected_STATE_Panel.Visible = false;
                State_Panel.Height = 150;
                State_Panel.Visible = false;
                STATE_Ajax_Panel1.Update();

                Selected_SPI_Panel.Visible = false;
                SPI_Selections_Panel2.Visible = false;
                SPI_SELECTED_CkBox.Checked = false;
                SPI_TOTAL_ALL_CkBox.Checked = true;
                SPI_Panel.Height = 150;
                SPI_Panel.Visible = false;
                SPIs_Update_Panel.Update();

                Selected_Reporter_Panel.Visible = false;
                RCTY_SELECTED_CkBox.Checked = false;
                RCTY_Selections_Panel2.Visible = false;
                RCTY_TOTAL_ALL_CkBox.Checked = true;
                Reporter_Panel.Height = 200;
                Reporter_Panel.Visible = false;
                Reporter_Update_Panel.Update();

                Data_ID = "USTB";
                Year_month_ckbox.Enabled = true;
                Year_quarter_ckbox.Enabled = true;
                Year_ytd_ckbox.Enabled = false;
                Year_ytd_ckbox.Checked = false;
                DataSource_DropDownList1.DataBind();
                DIGITS_ID_Dropdown1.DataBind();
                Load_Update_Job_Panel.Update();

            }
            else
            {
                Data_Panel_ushs.Visible = false;
                //Data_Panel_ushs.Style["display"]="none";
                Data_Update_Panel_ushs.Update();
                if (wk_str.Equals("USST", StringComparison.InvariantCultureIgnoreCase))
                {
                    Current_DBID_Label1.Text = wk_str.ToUpper();
                    UNFlow_ConImp_ckbox.Enabled = true;
                    UNFlow_DomExp_ckbox.Visible = false;
                    UNFlow_ReExp_ckbox.Visible = false;
                    UNFlow_Volume_ckbox.Visible = false;
                    UNFlow_Weight_ckbox.Visible = true;
                    UV_Volume_ckbox.Visible = false;
                    UV_Weight_ckbox.Visible = true;
                    State_Panel.Visible = true;
                    SPI_Panel.Visible = false;
                    Enable_District_Btn.Visible = false;
                    Enable_SPIs_Btn.Visible = false;

                    State_Header_Label.Text = "State Selection:";
                    STATE_TOTAL_ALL_CkBox.Text = "All States in the USA";
                    STATE_SELECTED_CkBox.Text = "Selected States";
                    SubmitList_User_STATE_Processing_Label1.Text = "STATE";
                    STATE_SELECTED_CkBox.Checked = false;
                    STATE_TOTAL_ALL_CkBox.Checked = true;
                    State_Selections_Panel2.Visible = false;
                    Selected_STATE_Panel.Visible = false;
                    State_Panel.Height = 150;
                    State_Panel.Visible = true;
                    STATE_Ajax_Panel1.Update();

                    Selected_SPI_Panel.Visible = false;
                    SPI_Selections_Panel2.Visible = false;
                    SPI_SELECTED_CkBox.Checked = false;
                    SPI_TOTAL_ALL_CkBox.Checked = true;
                    SPI_Panel.Height = 150;
                    SPI_Panel.Visible = false;
                    SPIs_Update_Panel.Update();

                    Selected_Reporter_Panel.Visible = false;
                    RCTY_SELECTED_CkBox.Checked = false;
                    RCTY_Selections_Panel2.Visible = false;
                    RCTY_TOTAL_ALL_CkBox.Checked = true;
                    Reporter_Panel.Height = 200;
                    Reporter_Panel.Visible = false;
                    Reporter_Update_Panel.Update();


                    Data_ID = "USST";
                    UNFlow_Value_ckbox.Checked = true;
                    Year_month_ckbox.Enabled = true;
                    Year_quarter_ckbox.Enabled = true;
                    Year_ytd_ckbox.Enabled = true;
                    DataSource_DropDownList1.DataBind();
                    DIGITS_ID_Dropdown1.DataBind();
                    Load_Update_Job_Panel.Update();

                }
                else if (wk_str.IndexOf("UN") >= 0)
                {
                    Current_DBID_Label1.Text = wk_str.ToUpper();

                    UNFlow_ConImp_ckbox.Enabled = false;
                    UNFlow_DomExp_ckbox.Visible = true;
                    UNFlow_ReExp_ckbox.Visible = true;
                    UNFlow_Volume_ckbox.Visible = true;
                    UNFlow_Weight_ckbox.Visible = true;
                    UV_Volume_ckbox.Visible = true;
                    UV_Weight_ckbox.Visible = true;
                    Reporter_Panel.Visible = true;

                    Enable_District_Btn.Visible = false;
                    Enable_SPIs_Btn.Visible = false;

                    STATE_SELECTED_CkBox.Checked = false;
                    STATE_TOTAL_ALL_CkBox.Checked = true;
                    Selected_STATE_Panel.Visible = false;
                    State_Panel.Height = 150;
                    State_Panel.Visible = false;
                    STATE_Ajax_Panel1.Update();

                    Selected_SPI_Panel.Visible = false;
                    SPI_Selections_Panel2.Visible = false;
                    SPI_SELECTED_CkBox.Checked = false;
                    SPI_TOTAL_ALL_CkBox.Checked = true;
                    SPI_Panel.Height = 150;
                    SPI_Panel.Visible = false;
                    SPIs_Update_Panel.Update();

                    Selected_Reporter_Panel.Visible = false;
                    RCTY_SELECTED_CkBox.Checked = false;
                    RCTY_Selections_Panel2.Visible = false;
                    RCTY_TOTAL_ALL_CkBox.Checked = true;
                    Reporter_Panel.Height = 200;
                    Reporter_Panel.Visible = true;
                    Reporter_Update_Panel.Update();



                    UNFlow_Value_ckbox.Checked = true;
                    Data_ID = "UNHS";
                    Year_month_ckbox.Enabled = false;
                    Year_quarter_ckbox.Enabled = false;
                    Year_ytd_ckbox.Enabled = false;
                    Year_month_ckbox.Checked = false;
                    Year_quarter_ckbox.Checked = false;
                    Year_ytd_ckbox.Checked = false;

                    if (wk_str.IndexOf("S1") >= 0)
                        Data_ID = "UNS1";
                    else if (wk_str.IndexOf("S3") >= 0)
                        Data_ID = "UNS3";
                    Load_Update_Job_Panel.Update();
                }




                Data_Panel_un_state.Visible = true;
                Data_Update_Panel_un_state.Update();
                DataSource_DropDownList1.DataBind();
                DIGITS_ID_Dropdown1.DataBind();
            }
            // Job_Name_TextBox1.Text = Data_ID + " @ " + cur_time_str;
        }
        Main_Update_Panel.Update();



    } // end Load_New_DB_Env

    protected void Load_Parmfile(object sender, EventArgs e)
    {
        SqlConnection Conn2;
        string seq_num;
        string connString;
        string strSQL = "";
        string strSQL2 = "";
        string wk_user = "POMEROR";
        int Line_Num = 0;
        int J_NUM = 111001;
        int Rank_Flag = 0;
        string Rank_Var = "";
        string Rank_Topx_Str = "";
        string Rank_Year_Str = "";
        string wk_str = "";
        int wk_yr1_int = 0;
        int wk_yr2_int = 0;
        string wk_yr1 = "";
        string wk_yr2 = "";
        int flag_time = 0;
        string wk_conc = "";
        string wk_digits = "";
        string wk_data_id = "";
        string wk_data_name = "";
        int cmd_show = 0;
        int pcty_show = 0;
        int rcty_show = 0;
        int state_show = 0;
        int dist_show = 0;
        int flow_check = 0;
        string wk_pcty = "";
        string wk_rcty = "";
        int flag_db = 1; // 1 or 2 is ushs or ustb 3 is usst >3 is UN
        int i_ck = 0;
        int i_sel = 0;
        string wk_conc_ck = wk_conc;

        string flows_active = ":";
        string flowtypes_active = ":";
        string dbase_active = ":";
        string cmdtotal_found = ":";
        string cmdconc_found = ":";
        string cmdoptions_active = ":";
        string yearfields_active = ":";
        string ctysettings_active = ":";
        string cmddigits_active = ":";
        string cmdconc_active = ":";
        int wk_cnt_rows = 0;

        int cnt_selected_cmds = 0;
        int cnt_subgroups = 0;
        int digit_level = 0;
        int total_all = 0;
        int cnt_loop = 0;
        int num_items = 0;

        int cmds_sel_cnt = 0;
        int pctys_sel_cnt = 0;
        int rctys_sel_cnt = 0;
        int dists_sel_cnt = 0;
        int states_sel_cnt = 0;
        int spis_sel_cnt = 0;
        int cmd_total_flag = 0;
        int pcty_world_flag = 0;
        int rcty_world_flag = 0;
        int state_total_flag = 0;

        int us_flows_cnt = 0;
        int un_flows_cnt = 0;
        int cnt_rcty_zin_v = 0;
        string flows_found = "";
        string dbase_found = "";
        string conc_found = "";
        Data_ID = Current_DBID_Label1.Text.ToString();


        //int i=0 ;

        string wk_str2 = "";
        LoadJob_Smry_Label1.Text = "Load ";

        // "UID=TPISGUI;Password=Log$me#in2020;" +

        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=tcp:ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=US_DATA;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";
        Job_Num_Str = User_JobNum_TextBox1.Text.ToString();
        if (!Int32.TryParse(Job_Num_Str, out J_NUM))
        {
            Job_Num_Str = "111001";
            J_NUM = 111001;
            User_JobNum_TextBox1.Text = Job_Num_Str;

        }
        wk_str = "NA";
        wk_str = "insert into dbo.session_gui_fields (job_num, in_type, in_text1, in_text2)";

        LoadJob_Smry_Label1.Text = "Load job " + Job_Num_Str + ": ";

        DataTable dt = new DataTable();
        DataTable dt_smry = new DataTable();
        Parmload_Loadloop2_Label1.Text = "L1-- ";
        Parmload_Process_Label1.Text = "LP1-- ";


        try
        {
            strSQL = "SELECT JOB_NUM, IN_LINE, IN_TYPE, IN_TEXT1, IN_TEXT2 "
            + " from dbo.tpis_jobdata where job_num=" + Job_Num_Str
            + " order by in_line ";
            //SqlDataAdapter sda = new SqlDataAdapter();
            //  try {

            SqlCommand sqlcom = new SqlCommand(strSQL, sqlconn);
            sqlcom.CommandType = CommandType.Text;
            sqlcom.CommandText = strSQL;
            sqlconn.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(sqlcom))
            {
                sda.Fill(dt);
            }

            strSQL2 = "SELECT JOB_NUM, IN_LINE, IN_TYPE, IN_TEXT1, IN_TEXT2 "
            + " from dbo.tpis_jobdata where job_num=" + Job_Num_Str
            + " and upper(in_type) in ('DESCRIPTION','DATAID','CMD_CONC','CMD_DIGITS','DATA','CMD_ZIN_V') "
            + " order by in_line ";
            SqlCommand sqlcom2 = new SqlCommand(strSQL2, sqlconn);
            sqlcom2.CommandType = CommandType.Text;
            sqlcom2.CommandText = strSQL2;


            using (SqlDataAdapter sdb = new SqlDataAdapter(sqlcom2))
            {
                sdb.Fill(dt_smry);
            }


            strSQL2 = "SELECT job_num "
            + ", sum(case when in_type ='CMD_ZIN_V' then 1 else 0 end) as num_cmds "
            + ", sum(case when in_type ='CMD_ZIN_V' and (upper(in_text1) in ('.TOTAL','TOTAL','ALL') or in_text1+'-'+in_text2='0-9') then 1 else 0 end) as cmd_total "
            + ", sum(case when in_type ='PCTY_ZIN_V' then 1 else 0 end) as num_pctys  "
            + ", sum(case when in_type ='PCTY_ZIN_V' and upper(in_text1) in ('.WORLD','WORLD') then 1 else 0 end) as pcty_world "
            + ", sum(case when in_type ='RCTY_ZIN_V' then 1 else 0 end) as num_rctys  "
            + ", sum(case when in_type ='RCTY_ZIN_V' and upper(in_text1) in ('.WORLD','WORLD') then 1 else 0 end) as rcty_world "
            + ", sum(case when in_type ='STATE_ZIN_V' then 1 else 0 end) as num_states "
            + ", sum(case when in_type ='STATE_ZIN_V' and upper(in_text1) like '%USA%' or upper(in_text1) in ('USA','ALL','.ALL') then 1 else 0 end) as states_usa "
            + ", sum(case when in_type ='SPI_ZIN_V' then 1 else 0 end) as num_spis  "
            + ", sum(case when in_type ='DIST_ZIN_V' then 1 else 0 end) as num_dists  "
            + ", sum(case when in_type ='DIST_ZIN_V' and upper(in_text1) in ('.ALL','ALL') then 1 else 0 end) as all_dists "
            + " from dbo.tpis_jobdata  "
            + " where job_num = " + Job_Num_Str
            + " and upper(in_type) "
            + " in ('PCTY_ZIN_V','RCTY_ZIN_V','STATE_ZIN_V','CMD_ZIN_V', 'DIST_ZIN_V','SPI_ZIN_V') "
            + " and in_text1 is not null "
            + " and substring(in_text1,1,1) not in ('{','}','=') "
            + " group by job_num "
            ;

            using (SqlCommand sqlcom3 = new SqlCommand(strSQL2, sqlconn))
            {
                using (var reader = sqlcom3.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        LoadJob_Smry_Label1.Text += " Read Error";
                        throw new Exception("Read Count Error!");
                    }
                    int ordcmd = reader.GetOrdinal("num_cmds");
                    int ordpcty = reader.GetOrdinal("num_pctys");
                    int ordrcty = reader.GetOrdinal("num_rctys");
                    int ordstate = reader.GetOrdinal("num_states");
                    int ordspi = reader.GetOrdinal("num_spis");
                    int orddist = reader.GetOrdinal("num_dists");
                    int ordcmdtot = reader.GetOrdinal("cmd_total");
                    int ordpctywld = reader.GetOrdinal("pcty_world");
                    int ordrctywld = reader.GetOrdinal("rcty_world");
                    int ordallstates = reader.GetOrdinal("states_usa");

                    cmds_sel_cnt = reader.GetInt32(ordcmd);
                    pctys_sel_cnt = reader.GetInt32(ordpcty);
                    rctys_sel_cnt = reader.GetInt32(ordrcty);
                    states_sel_cnt = reader.GetInt32(ordstate);
                    dists_sel_cnt = reader.GetInt32(orddist);
                    spis_sel_cnt = reader.GetInt32(ordspi);
                    cmd_total_flag = reader.GetInt32(ordcmdtot);
                    pcty_world_flag = reader.GetInt32(ordpctywld);
                    rcty_world_flag = reader.GetInt32(ordrctywld);
                    state_total_flag = reader.GetInt32(ordallstates);

                } // using reader

            } // end sqlcom3 
            if (1 == 2)
            {
                strSQL2 = "SELECT TOP 1 upper(in_text1) as dbaseid "
                + " from dbo.tpis_jobdata where job_num=" + Job_Num_Str
                + " and upper(in_type) = 'DATAID' "
                + " and in_text1 is not null and "
                + " upper(in_text1) in ('US','UNHS','USST','USTB','USS3','USS1') ";

                using (SqlCommand sqlcom4 = new SqlCommand(strSQL2, sqlconn))
                {
                    sqlcom4.CommandType = CommandType.Text;
                    sqlcom4.CommandText = strSQL2;
                    using (var reader = sqlcom4.ExecuteReader())
                    {
                        int ord_dataid = 0;
                        if (!reader.Read())
                        {
                            LoadJob_Smry_Label1.Text += " Read Error";
                            throw new Exception("Read Count Error!");
                        }
                        ord_dataid = reader.GetOrdinal("dbaseid");
                        Data_ID = reader.GetString(ord_dataid);
                    } // using reader

                } // end sqlcom4
                LoadJob_Change_Label1.Text = "Load job " + Job_Num_Str + "DB CK: |" + Data_ID + "| from "
                  + Current_DBID_Label1.Text.ToString();
                if (!String.IsNullOrEmpty(Data_ID))
                {
                    try
                    {
                        if (!Current_DBID_Label1.Text.ToString().Equals(Data_ID, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ListItem selectedListItem = Database_Type_Dropdownlist.Items.FindByValue(Data_ID);
                            if (selectedListItem != null)
                            {
                                selectedListItem.Selected = true;
                                Current_DBID_Label1.Text = Data_ID.ToString().ToUpper();
                                LoadJob_Change_Label1.Text += " changed to " + Data_ID;
                                Database_Type_Dropdownlist.DataBind();
                                Database_Type_DropList_Changed(sender, e);
                                Main_Update_Panel.Update();
                            }
                        }
                    }
                    catch (Exception chg_db)
                    {
                        LoadJob_Change_Label1.Text = "ERROR CHANGE DB TO "
                        + Data_ID + " IS : " + chg_db.Message.ToString();

                    }
                }
            } // end load job change
            LoadJob_Smry_Label1.Text += " (Data_ID=" + Data_ID + ") cmds=" + cmds_sel_cnt.ToString()
               + " (" + cmd_total_flag.ToString() + "=TOT) "
               + " pctys=" + pctys_sel_cnt.ToString()
               + " (" + pcty_world_flag.ToString() + "=wld) "
               ;
            if (rctys_sel_cnt > 0)
            {

                LoadJob_Smry_Label1.Text += " rctys=" + rctys_sel_cnt.ToString()
                + " (" + rcty_world_flag.ToString() + "=wld) ";
            };
            if (states_sel_cnt > 0)
            {

                LoadJob_Smry_Label1.Text += " states=" + states_sel_cnt.ToString()
                + " (" + state_total_flag.ToString() + "=USA) ";
            };
            if (spis_sel_cnt + dists_sel_cnt > 0)
            {
                LoadJob_Smry_Label1.Text += " spis=" + spis_sel_cnt.ToString()
                + " dists=" + dists_sel_cnt.ToString();
            };




            //sqlconn.Close();

            //sda.Dispose();
            //sdb.Dispose();

            Show_Load_ListBox.Items.Clear();
            CMD_TOTAL_ALL_CkBox.Checked = true;
            CMD_SELECTED_CkBox.Checked = false;
            Display_CMDTOTAL_chkbox.Checked = false;
            Display_CMDSUB_chkbox.Checked = false;
            Display_CMDDETAIL_chkbox.Checked = false;
            Display_CMDShare_chkbox.Checked = false;
            Display_CMD_TotalLabel_chkbox.Checked = false;

            PCTY_TOTAL_ALL_CkBox.Checked = true;
            PCTY_SELECTED_CkBox.Checked = false;
            Display_PCTYTOTAL_chkbox.Checked = false;
            Display_PCTYSUB_chkbox.Checked = false;
            Display_PCTYDETAIL_chkbox.Checked = false;
            Display_PCTYShare_chkbox.Checked = false;

            Flow_imports_ckbox.Checked = false;
            Flow_exports_ckbox.Checked = false;
            Flow_balance_ckbox.Checked = false;
            Flow_2way_ckbox.Checked = false;

            if (Data_ID.Equals("US", StringComparison.InvariantCultureIgnoreCase))
                flag_db = 1;
            else if (Data_ID.Equals("USTB", StringComparison.InvariantCultureIgnoreCase))
                flag_db = 2;
            else if (Data_ID.Equals("USST", StringComparison.InvariantCultureIgnoreCase))
                flag_db = 3;
            else if (Data_ID.IndexOf("UN", 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
                flag_db = 4;

            if (flag_db == 3)
            {
                UNFlow_ConImp_ckbox.Enabled = true;
                UNFlow_ConImp_ckbox.Checked = false;
                UNFlow_GenImp_ckbox.Checked = false;
                UNFlow_DomExp_ckbox.Visible = false;
                UNFlow_TotalExp_ckbox.Checked = false;
                UNFlow_ReExp_ckbox.Visible = false;
                UNFlow_Volume_ckbox.Visible = false;
                UNFlow_Weight_ckbox.Visible = true;
                UV_Volume_ckbox.Visible = false;
                UV_Weight_ckbox.Visible = true;

                Flow_imports_ckbox.Checked = false;
                Flow_exports_ckbox.Checked = false;
                Flow_balance_ckbox.Checked = false;
                Flow_2way_ckbox.Checked = false;
                Flow_exports_ckbox.Checked = true;
                Flow_imports_ckbox.Checked = false;
                wk_data_id = "USST";
            }
            else if (flag_db == 4)
            {
                UNFlow_ConImp_ckbox.Enabled = false;
                UNFlow_DomExp_ckbox.Visible = true;
                UNFlow_ReExp_ckbox.Visible = true;
                UNFlow_Volume_ckbox.Visible = true;
                UNFlow_Weight_ckbox.Visible = true;
                UV_Volume_ckbox.Visible = true;
                UV_Weight_ckbox.Visible = true;
                UNFlow_DomExp_ckbox.Checked = false;
                UNFlow_ReExp_ckbox.Checked = false;
                UNFlow_Volume_ckbox.Checked = false;
                UNFlow_Weight_ckbox.Checked = false;
                UV_Volume_ckbox.Checked = false;
                UV_Weight_ckbox.Checked = false;
                Flow_imports_ckbox.Checked = false;
                Flow_exports_ckbox.Checked = false;
                Flow_balance_ckbox.Checked = false;
                Flow_2way_ckbox.Checked = false;
                wk_data_id = Data_ID;
                UNFlow_TotalExp_ckbox.Checked = false;
                Flow_exports_ckbox.Checked = false;
                Flow_imports_ckbox.Checked = true;


            }
            else
            {
                Flow_GenImp_ckbox.Checked = false;
                Flow_ConImp_ckbox.Checked = false;
                Flow_DomExp_ckbox.Checked = false;
                Flow_ForExp_ckbox.Checked = false;
                Flow_TotalExp_ckbox.Checked = false;
                Flow_CustVal_ckbox.Checked = false;
                Flow_CifVal_ckbox.Checked = false;
                Flow_Qty1_ckbox.Checked = false;
                Flow_Qty2_ckbox.Checked = false;
                UV1_ckbox.Checked = false;
                UV2_ckbox.Checked = false;
                Flow_Duty_ckbox.Checked = false;
                Flow_FASVal_ckbox.Checked = false;
                Flow_ExpQty1_ckbox.Checked = false;
                Flow_ExpQty2_ckbox.Checked = false;
                wk_data_id = "US";
                wk_conc = "HS";
                if (Reporter_Panel.Visible)
                {
                    Reporter_Panel.Visible = false;
                    Reporter_Update_Panel.Update();
                }
                Flow_exports_ckbox.Checked = false;
                Flow_imports_ckbox.Checked = false;
                //Flow_imports_ckbox.Checked=true ;
                //Flow_GenImp_ckbox.Checked = true ;
                //Flow_CustVal_ckbox.Checked = true ;



            }

            Year_month_ckbox.Checked = false;
            Year_quarter_ckbox.Checked = false;
            Year_ytd_ckbox.Checked = false;
            Year_ann_ckbox.Checked = false;

            wk_digits = "4";
            Misc_Databox1.Text = "LOAD DB " + Job_Num_Str + ": ";
            Parmload_Test_Label1.Text += " dt rowsmry=" + dt_smry.Rows.Count.ToString();
            if (1 == 1)
            {
                Job_Data_GridView.Visible = true;
                Job_Data_GridView.DataSource = dt_smry;
                Job_Data_GridView.DataBind();
            }

            wk_cnt_rows = 0;
            Parmload_Process_Label1.Text = "smrycnt= " + dt_smry.Rows.Count.ToString() + ": ";
            wk_str = "";


            foreach (DataRow rowb in dt_smry.Rows)
            {
                wk_cnt_rows += 1;
                if (wk_cnt_rows < 10)
                {
                    Parmload_Process_Label1.Text = Parmload_Process_Label1.Text.ToString() + " #" + wk_cnt_rows.ToString() + ". " + wk_str;
                }
                wk_str = rowb[2].ToString().Trim().ToUpper();
                Parmload_Process_Label1.Text = Parmload_Process_Label1.Text.ToString() + wk_str;
                try
                {
                    if (wk_str.Equals("CMD_CONC", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_conc = rowb[3].ToString().ToUpper();
                        cmdconc_active += wk_conc;
                        LoadJob_Smry_Label1.Text += " conc=" + wk_conc;
                    }
                    else if (wk_str.Equals("CMD_DIGITS", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_digits = rowb[3].ToString().ToUpper();
                        cmddigits_active += wk_digits;
                        LoadJob_Smry_Label1.Text += " @digits=" + wk_digits;

                    }
                    else if (wk_str.Equals("DESCRIPTION", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Job_Name_TextBox1.Text = rowb[3].ToString();
                        User_JobNum_TextBox1.Text = Job_Num_Str.ToString();
                    }

                    else if (1 == 2 && wk_str.Equals("DATAID", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_data_id = rowb[3].ToString().ToUpper();
                        if (wk_data_id.Equals(Data_ID, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Misc_Databox1.Text += " DB NO CHANGE=" + wk_data_id;
                        }
                        if (wk_data_id.Equals("USTB", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Data_Panel_ushs.Visible = true;
                            Data_Update_Panel_ushs.Update();
                            Data_Panel_un_state.Visible = false;
                            Year_month_ckbox.Enabled = true;
                            Year_quarter_ckbox.Enabled = true;
                            Year_ytd_ckbox.Enabled = false;
                            Year_month_ckbox.Checked = true;
                            //Year_quarter_ckbox.Checked=false ;
                            Year_ytd_ckbox.Checked = false;
                            Data_Update_Panel_un_state.Update();
                            State_Panel.Visible = false;
                            Reporter_Panel.Visible = false;
                            Data_ID = "USTB";
                        }
                        else if (wk_data_id.Equals("USST", StringComparison.InvariantCultureIgnoreCase))
                        {
                            UNFlow_ConImp_ckbox.Enabled = true;
                            UNFlow_DomExp_ckbox.Visible = false;
                            UNFlow_ReExp_ckbox.Visible = false;
                            UNFlow_Volume_ckbox.Visible = false;
                            UNFlow_Weight_ckbox.Visible = true;
                            UV_Volume_ckbox.Visible = false;
                            UV_Weight_ckbox.Visible = true;
                            State_Panel.Visible = true;
                            Year_month_ckbox.Enabled = true;
                            Year_quarter_ckbox.Enabled = true;
                            Year_ytd_ckbox.Enabled = true;
                            Year_month_ckbox.Checked = false;
                            Year_quarter_ckbox.Checked = false;
                            //Year_ytd_ckbox.Checked=false ;

                            STATE_Ajax_Panel1.Update();
                            Reporter_Panel.Visible = false;
                            Data_ID = "USST";
                        }
                        else if (wk_data_id.IndexOf("UN") >= 0)
                        {
                            UNFlow_ConImp_ckbox.Enabled = false;
                            UNFlow_DomExp_ckbox.Visible = true;
                            UNFlow_ReExp_ckbox.Visible = true;
                            UNFlow_Volume_ckbox.Visible = true;
                            UNFlow_Weight_ckbox.Visible = true;
                            UV_Volume_ckbox.Visible = true;
                            UV_Weight_ckbox.Visible = true;
                            Reporter_Panel.Visible = true;

                            State_Panel.Visible = false;
                            SPI_Panel.Visible = false;
                            Enable_District_Btn.Visible = false;
                            Enable_SPIs_Btn.Visible = false;
                            STATE_Ajax_Panel1.Update();
                            Reporter_Update_Panel.Update();

                            Data_ID = "UNHS";
                            Year_month_ckbox.Enabled = false;
                            Year_quarter_ckbox.Enabled = false;
                            Year_ytd_ckbox.Enabled = false;
                            Year_month_ckbox.Checked = false;
                            Year_quarter_ckbox.Checked = false;
                            Year_ytd_ckbox.Checked = false;
                            if (wk_str.IndexOf("S1") >= 0)
                                Data_ID = "UNS1";
                            else if (wk_str.IndexOf("S1") >= 0)
                                Data_ID = "UNS2";
                        }
                        else if (wk_data_id.Equals("US", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Data_Panel_ushs.Visible = true;
                            //Data_Panel_ushs.Style["display"]="block";
                            Year_month_ckbox.Enabled = true;
                            Year_quarter_ckbox.Enabled = true;
                            Year_ytd_ckbox.Enabled = true;

                            Data_Update_Panel_ushs.Update();
                            Data_Panel_un_state.Visible = false;
                            Data_Update_Panel_un_state.Update();
                            State_Panel.Visible = false;
                            Reporter_Panel.Visible = false;
                            Enable_District_Btn.Visible = false;
                            Enable_SPIs_Btn.Visible = false;
                            SPI_Panel.Visible = false;
                            STATE_Ajax_Panel1.Update();
                            Reporter_Update_Panel.Update();


                            Data_ID = "USHS";

                        }
                        dbase_active += Data_ID;
                        Misc_Databox1.Text += " end wk_data_id=" + wk_data_id;
                        if (Database_Type_Dropdownlist.Items.FindByValue(Data_ID) != null)
                        {
                            Database_Type_Dropdownlist.SelectedValue = Data_ID;
                            Current_DBID_Label1.Text = Data_ID;
                            Database_Type_Dropdownlist.DataBind();
                        };
                    }
                    else if (wk_str.Equals("DATA", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_data_name = rowb[3].ToString();
                    }
                    wk_str = "";
                } // end try
                catch (Exception smryloop)
                {
                    Parmload_Process_Label1.Text = "ERROR dt_sumry line " + wk_cnt_rows.ToString() + " wk_str=" + wk_str
                    + smryloop.Message.ToString();

                }

                DataSource_DropDownList1.DataBind();
                Show_Conc_Digits_CK.Text = wk_conc;

                int wk_tidx = 0;
                wk_tidx = DataSource_DropDownList1.Items.IndexOf(DataSource_DropDownList1.Items.FindByValue(wk_conc));
                if (wk_tidx < 0)
                {
                    wk_str = wk_data_id + ':' + wk_conc;
                    wk_tidx = DataSource_DropDownList1.Items.IndexOf(DataSource_DropDownList1.Items.FindByValue(wk_str));
                }
                if (wk_tidx < 0)
                {

                    Show_Conc_Digits_CK.Text += " idxerr=" + wk_tidx.ToString();
                    if (wk_conc == "HS")
                        wk_tidx = 0;
                    if (wk_conc == "NAICS")
                        wk_tidx = 1;
                }
                if (wk_tidx >= 0 && wk_tidx < DataSource_DropDownList1.Items.Count)
                {
                    DataSource_DropDownList1.SelectedIndex = wk_tidx;
                    Database_Type_Dropdownlist.DataBind();
                    DIGITS_ID_Dropdown1.DataBind();
                    Series_Type_TextBox.Text = DataSource_DropDownList1.SelectedValue.ToString();
                    Series_Type_TextBox.DataBind();
                    CMD_Ajax_Panel1.Update();
                }

                Show_Conc_Digits_CK.Text += " idx=" + wk_tidx.ToString() + " cmdsel="
                + DataSource_DropDownList1.SelectedValue.ToString();

                DIGITS_ID_Dropdown1.DataBind();
                Show_Conc_Digits_CK.Text = wk_digits;

                wk_tidx = 0;
                wk_tidx = DIGITS_ID_Dropdown1.Items.IndexOf(DIGITS_ID_Dropdown1.Items.FindByValue(wk_digits));
                if (wk_tidx < 0)
                {
                    wk_str = wk_data_id + ':' + wk_digits;
                    wk_tidx = DIGITS_ID_Dropdown1.Items.IndexOf(DIGITS_ID_Dropdown1.Items.FindByValue(wk_str));
                }
                if (wk_tidx < 0)
                {

                    Show_Conc_Digits_CK.Text += " digidxerr=" + wk_tidx.ToString();
                    wk_tidx = 0;
                }
                if (wk_tidx >= 0 && wk_tidx < DIGITS_ID_Dropdown1.Items.Count)
                {
                    if (wk_digits == "10")
                    {
                        try
                        {

                            if (Product_Detail1_DropDownList2.SelectedIndex + Product_Detail1_DropDownList1.SelectedIndex <= 0)
                            {
                                if (Product_DropDownList1.SelectedIndex <= 0)
                                {
                                    Product_DropDownList1.SelectedIndex = 1;
                                    Product_DropDownList1.DataBind();
                                    wk_str = Product_DropDownList1.SelectedValue.ToString();
                                    Series_TextBox.Text = wk_str;
                                }
                            } // if total and 10-digits then set to 1st product
                        }
                        catch (Exception ext10)
                        {
                            Series_TextBox.Text = "1";

                        }
                        Series_TextBox.DataBind();
                    } // end special handling 10-digits
                    DIGITS_ID_Dropdown1.SelectedIndex = wk_tidx;
                    DIGITS_ID_Dropdown1.DataBind();
                    CMD_Ajax_Panel1.Update();
                }

            } // end loop over summary load sql

            Parmload_Test_Label1.Text += " end loop";
            try
            { // ljouter
                LoadJob_Error_Label1.Text = " 1 ";

                int wk_dbindex = 0;
                wk_dbindex = Database_Type_Dropdownlist.Items.IndexOf(Database_Type_Dropdownlist.Items.FindByValue(Data_ID));
                if (wk_dbindex >= 0 && wk_dbindex < Database_Type_Dropdownlist.Items.Count)
                {
                    Database_Type_Dropdownlist.SelectedIndex = wk_dbindex;
                    Database_Type_Dropdownlist.DataBind();
                    CMD_Ajax_Panel1.Update();
                }
                int wk_conc_index = 0;
                wk_conc_index = DataSource_DropDownList1.Items.IndexOf(DataSource_DropDownList1.Items.FindByValue(wk_conc));
                LoadJob_Error_Label1.Text += " 2";
                if (wk_conc_index >= 0 && wk_conc_index < DataSource_DropDownList1.Items.Count)
                {
                    DataSource_DropDownList1.SelectedIndex = wk_conc_index;
                    DataSource_DropDownList1.DataBind();
                    CMD_Ajax_Panel1.Update();
                }
                LoadJob_Error_Label1.Text += " 3";
                Parmload_Test_Label1.Text = "Load " + Job_Num_Str + " ";
                Parmload_Test_Label1.Text += " dt rowcntA=" + dt.Rows.Count.ToString();
                Parmload_Test_Label1.DataBind();

                string wk_loc = "db";
                Selections_CMD_TextBox.Text = "";
                Selections_TextBox.Text = "";
                Misc_Textbox2.Text = "src |" + wk_str + "| digits=" + wk_digits;
                Misc_Textbox3.Text = "end loop 1 cnt=" + wk_cnt_rows.ToString();
                wk_str = "NONE";
                LoadJob_Error_Label1.Text += " 4";
                try
                {
                    if (Database_Type_Dropdownlist.SelectedIndex >= 0)
                    {
                        wk_str = Database_Type_Dropdownlist.SelectedValue.ToString().ToUpper();
                    }
                    wk_loc = "db2";
                    Misc_Databox1.Text += " wk_str=|" + wk_str + "| wk_data_id=|" + wk_data_id + "| ";

                    if (!wk_str.Equals(wk_data_id, StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_loc = "db3";
                        Database_Type_Dropdownlist.SelectedIndex = -1;
                        Database_Type_Dropdownlist.Items.FindByValue(wk_data_id).Selected = true;
                        Database_Type_Dropdownlist.DataBind();
                        DataSource_DropDownList1.DataBind();
                        wk_loc = "db4";
                        DIGITS_ID_Dropdown1.DataBind();
                    }
                    CMD_Ajax_Panel1.Update();
                    wk_conc_ck = wk_conc.ToUpper();
                    i_ck = wk_conc_ck.IndexOf(":");
                    if (i_ck < 0)
                    {
                        wk_conc_ck = wk_data_id + ':' + wk_conc;
                    }
                    wk_str = "NONE";
                    wk_loc = "conc1";
                    if (DataSource_DropDownList1.SelectedIndex >= 0)
                        wk_str = DataSource_DropDownList1.SelectedValue.ToString().ToUpper();
                    wk_loc = "conc2";
                    if (!wk_str.Equals(wk_conc_ck, StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_loc = "conc3";
                        DataSource_DropDownList1.SelectedIndex = -1;
                        DataSource_DropDownList1.Items.FindByValue(wk_data_id).Selected = true;
                        DataSource_DropDownList1.DataBind();
                        wk_loc = "conc4";
                    }
                    CMD_Ajax_Panel1.Update();
                    wk_str = "NONE";
                    wk_loc = "digits1";
                    if (DIGITS_ID_Dropdown1.SelectedIndex >= 0)
                        wk_str = DIGITS_ID_Dropdown1.SelectedValue.ToString().ToUpper();
                    wk_loc = "digits2";
                    if (!wk_str.Equals(wk_digits, StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_loc = "digits3";
                        DIGITS_ID_Dropdown1.SelectedIndex = -1;
                        DIGITS_ID_Dropdown1.Items.FindByValue(wk_data_id).Selected = true;
                        DIGITS_ID_Dropdown1.DataBind();
                        wk_loc = "digits4";
                    }
                    CMD_Ajax_Panel1.Update();
                    wk_loc = "end";

                }
                catch (Exception eeloop2)
                {
                    ERROR_TextBox.Text = wk_loc + " Error: " + wk_data_id + ", " + wk_conc + ", " + wk_digits + " E:"
                    + eeloop2.Message;
                }
                LoadJob_Error_Label1.Text += " 5";

                wk_str = "start dt";
                wk_cnt_rows = 0;
                wk_str = wk_conc.ToUpper();
                if (wk_conc.IndexOf(":") < 0)
                    wk_str = wk_data_id.ToUpper() + ":" + wk_conc.ToUpper();

                Misc_Textbox2.Text = wk_data_id + " src |" + wk_conc + "| digits=" + wk_digits;
                LoadJob_Error_Label1.Text += " 5.1";

                if (wk_conc_ck.IndexOf(wk_conc) < 0)
                {
                    LoadJob_Error_Label1.Text += " 5.2";

                    ListItem concListItem = DataSource_DropDownList1.Items.FindByValue(wk_conc);
                    if (concListItem == null)
                    {
                        concListItem = DataSource_DropDownList1.Items.FindByValue(wk_str);
                    }
                    if (concListItem != null)
                    {
                        concListItem.Selected = true;
                        DataSource_DropDownList1.DataBind();

                    }

                    //DataSource_DropDownList1.SelectedIndex = -1;
                    CMD_Ajax_Panel1.Update();

                }
                LoadJob_Error_Label1.Text += " 5.3";

                //cmds_sel_cnt
                //pctys_sel_cnt
                //rctys_sel_cnt
                //states_sel_cnt
                //dists_sel_cnt
                //spis_sel_cnt
                //cmd_total_flag
                //pcty_world_flag
                //rcty_world_flag
                //state_total_flag

                LoadJob_Error_Label1.Text += " 6";
                if (cmd_total_flag > 0 && cmd_total_flag >= cmds_sel_cnt || cmds_sel_cnt < 1)
                {
                    CMD_SELECTED_CkBox.Checked = false;
                    CMD_TOTAL_ALL_CkBox.Checked = true;
                    if (Selected_CMD_Panel.Visible)
                    {
                        Selected_CMD_Panel.Visible = false;
                        Data_CMD_Panel.Height = 200;
                        Selections_CMD_TextBox.Text = "";
                    }
                    cmd_total_flag = 9999; // total only
                    CMD_Ajax_Panel1.Update();
                } // end just cmd total
                else if (cmds_sel_cnt > 0 && cmd_total_flag < cmds_sel_cnt)
                {  // selections make individual panel visible

                    CMD_TOTAL_ALL_CkBox.Checked = false;
                    CMD_SELECTED_CkBox.Checked = true;
                    if (!Selected_CMD_Panel.Visible)
                    {
                        Selected_CMD_Panel.Visible = true;
                        Display_CMDSUB_chkbox.Visible = true;
                        Data_CMD_Panel.Height = 830;
                        Selections_CMD_TextBox.Text = "";
                        Display_CMDDETAIL_chkbox.Text = "Show All Products Selected";
                        Display_CMDTOTAL_chkbox.Text = "Total of Selected Products";

                    }
                    CMD_Ajax_Panel1.Update();

                } // end individual selections panel open

                Parmload_Test_Label1.Text += " end cmd";
                if (pcty_world_flag > 0 && pcty_world_flag >= pctys_sel_cnt || pctys_sel_cnt < 1)
                {
                    PCTY_SELECTED_CkBox.Checked = false;
                    PCTY_TOTAL_ALL_CkBox.Checked = true;
                    if (Selected_Partner_Panel.Visible)
                    {
                        Selected_Partner_Panel.Visible = false;
                        Partner_Panel.Height = 200;
                        Selections_TextBox.Text = "";
                    }
                    pcty_world_flag = 9999; // total only
                    PCTY_Ajax_Panel1.Update();
                } // end just cmd total
                else if (pctys_sel_cnt > 0 && pcty_world_flag < pctys_sel_cnt)
                {  // selections make individual panel visible

                    PCTY_TOTAL_ALL_CkBox.Checked = false;
                    PCTY_SELECTED_CkBox.Checked = true;
                    if (!Selected_Partner_Panel.Visible)
                    {
                        Selected_Partner_Panel.Visible = true;
                        Selections_TextBox.Text = "";
                        Display_PCTYSUB_chkbox.Visible = true;
                        Partner_Panel.Height = 730;
                        //Display_PCTYTOTAL_chkbox.Text = "Total of Selected";
                        //Display_PCTYDETAIL_chkbox.Text = "Show All Partners Selected";

                    }
                    PCTY_Ajax_Panel1.Update();

                } // end individual pcty selections panel open
                Parmload_Test_Label1.Text += " end pcty; Data_ID=" + Data_ID
                + " UN idx=" + Data_ID.IndexOf("UN").ToString() + ". ";
                LoadJob_Error_Label1.Text += " 7";

                if (Data_ID.IndexOf("UN") >= 0)
                {
                    try
                    { // un
                        LoadJob_Smry_Label1.Text += " (Rc wld=" + rcty_world_flag.ToString() + " cnt=" + rctys_sel_cnt.ToString() + ")";
                        Reporter_Panel.Visible = true;
                        Selected_Reporter_Panel.Visible = true;
                        Reporter_Panel.Height = 730;

                        RCTY_Selections_TextBox.Text = "";
                        //RCTY_Selections_TextBox.DataBind() ;
                        RCTY_User_Sel_Name_TextBox1.Text = "";
                        //Select_List_Num_DropDownList.Items.Clear() ;   
                        //RCTY_TextBox1.Text = "";
                        //Reporter_Update_Panel.Update();

                        if (rcty_world_flag > 0 && rcty_world_flag >= rctys_sel_cnt || rctys_sel_cnt < 1)
                        {

                            LoadJob_Smry_Label1.Text += " WT";
                            RCTY_SELECTED_CkBox.Checked = false;
                            RCTY_TOTAL_ALL_CkBox.Checked = true;
                            if (Selected_Reporter_Panel.Visible)
                            {
                                Selected_Reporter_Panel.Visible = false;
                                Reporter_Panel.Height = 200;
                                RCTY_Selections_TextBox.Text = "";
                            }
                            rcty_world_flag = 9999; // total only
                            Reporter_Update_Panel.Update();
                        } // end just world rcty total
                        else if (rctys_sel_cnt > 0 && rcty_world_flag < rctys_sel_cnt)
                        {  // selections make individual panel visible

                            RCTY_TOTAL_ALL_CkBox.Checked = false;
                            RCTY_SELECTED_CkBox.Checked = true;
                            RCTY_Selections_TextBox.Text = "";
                            LoadJob_Smry_Label1.Text += " RS";

                            if (!Selected_Reporter_Panel.Visible)
                            {
                                Selected_Reporter_Panel.Visible = true;
                                Display_RCTYSUB_chkbox.Visible = true;
                                Reporter_Panel.Height = 730;
                                //Display_RCTYTOTAL_chkbox.Text = "Total of Selected";
                                //Display_RCTYDETAIL_chkbox.Text = "Show All Reporters Selected";

                            }
                            Reporter_Update_Panel.Update();

                        } // end individual rcty selections panel open
                    } // end un try
                    catch (Exception unsetup)
                    {
                        LoadJob_Smry_Label1.Text += " Error:" + unsetup.Message.ToString();
                    }
                } // end un summary processing

                LoadJob_Error_Label1.Text += " 8";
                try
                {

                    Product_Name_TextBox.Text = DataSource_DropDownList1.SelectedValue.ToString()
                    + " " + DataSource_DropDownList1.SelectedIndex.ToString()
                    + ". " + DataSource_DropDownList1.SelectedItem.Text.ToString()
                    + " :: db" + Database_Type_Dropdownlist.SelectedValue.ToString()
                    + " i_sel" + i_sel.ToString();
                    ;

                    Misc_Textbox2.Text += " Start Loop 2";
                    wk_cnt_rows = 0;
                    Parmload_Loadloop2_Label1.Text = "Start Loop 2 row cnt=" + dt.Rows.Count.ToString();
                    Parmload_Test_Label1.Text += " dt rowcntB=" + dt.Rows.Count.ToString();
                    Job_Data_GridView.Visible = true;
                    Job_Data_GridView.DataSource = dt;
                    Job_Data_GridView.DataBind();
                }
                catch (Exception gvdterror)
                {
                    Parmload_Loadloop2_Label1.Text += " Error: "
                    + gvdterror.Message.ToString();

                }
                Parmload_Loadloop2_Label1.Text += " :: ";
                wk_cnt_rows = 0;
                wk_str = "";


                LoadJob_Error_Label1.Text += " 9";

                foreach (DataRow row in dt.Rows)
                {
                    wk_cnt_rows = wk_cnt_rows + 1;
                    wk_str = row[0].ToString() + ":"
                    + row[1].ToString() + ". type=" + row[2].ToString()
                    + " t1=" + row[3].ToString() + " t2=" + row[4].ToString();
                    Show_Load_ListBox.Items.Add(wk_str);
                    wk_str = row[2].ToString().Trim().ToUpper();
                    if (cmd_total_flag != 9999 && wk_str.Equals("CMD_ZIN_V", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString();
                        if (CMD_TOTAL_ALL_CkBox.Checked)
                        {
                            CMD_SELECTED_CkBox.Checked = true;
                            if (!Selected_CMD_Panel.Visible)
                            {
                                Selected_CMD_Panel.Visible = true;
                                Display_CMDSUB_chkbox.Visible = true;
                                Data_CMD_Panel.Height = 830;
                                Display_CMDDETAIL_chkbox.Text = "Show All Products Selected";
                                Display_CMDTOTAL_chkbox.Text = "Total of Selected Products";
                                CMD_Ajax_Panel1.Update();

                            }

                        }
                        if (String.IsNullOrEmpty(row[4].ToString()))
                        {
                            wk_str = row[3].ToString();
                        }
                        else
                        {
                            wk_str = row[3].ToString() + "-" + row[4].ToString();
                        }
                        Selections_CMD_TextBox.Text += wk_str + "\r\n";
                    }
                    else if (wk_str.Equals("CMD_SHOW", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        wk_str = wk_str.Replace(" ", "_");
                        wk_str = wk_str.Replace("PCT_OF_TOTAL", "SHARE");
                        if (wk_str.Equals("TOTAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_CMDTOTAL_chkbox.Checked = true;
                            if (cmd_show < 1000)
                            {
                                cmd_show += 1000;
                            }
                        }
                        else if (wk_str.Equals("FULL_DETAIL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_CMDDETAIL_chkbox.Checked = true;
                            if (cmd_show / 10 < 1)
                            {
                                cmd_show += 1;
                            }
                        }
                        else if (wk_str.Equals("ITEMS_BELOW", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_CMDSUB_chkbox.Checked = true;
                            if (cmd_show / 100 < 10)
                            {
                                cmd_show += 10;
                            }
                        }
                        else if (wk_str.Equals("SHARE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_CMDShare_chkbox.Checked = true;
                            if (cmd_show / 1000 < 100)
                            {
                                cmd_show += 100;
                            }
                        }
                        else if (wk_str.Equals("TOTAL_LABEL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_CMD_TotalLabel_chkbox.Checked = true;
                        }
                    }
                    else if (wk_str.Equals("CMD_OBJ_LABEL", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        CMD_Total_TextBox.Text = wk_str;
                    }
                    else if (wk_str.Equals("RANK", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        if (wk_str.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Rank_Flag = 1;
                        }
                    }
                    else if (wk_str.Equals("RANK_TOP", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        Rank_Topx_Str = wk_str;
                    }
                    else if (wk_str.Equals("RANK_YEAR", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        Rank_Year_Str = wk_str;
                    }
                    else if (wk_str.Equals("RANK_VAR", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        Rank_Var = wk_str;
                    }
                    else if (pcty_world_flag != 9999 && wk_str.Equals("PCTY_ZIN_V", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString();
                        if (PCTY_TOTAL_ALL_CkBox.Checked)
                        {
                            PCTY_SELECTED_CkBox.Checked = true;
                            if (!Selected_Partner_Panel.Visible)
                                Selected_Partner_Panel.Visible = true;

                        }
                        if (String.IsNullOrEmpty(row[4].ToString()))
                        {
                            wk_str = row[3].ToString();
                        }
                        else
                        {
                            wk_str = row[3].ToString() + "-" + row[4].ToString();
                        }
                        Selections_TextBox.Text += wk_str + "\r\n";
                    }
                    else if (wk_str.Equals("PCTY_SHOW", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        wk_str = wk_str.Replace(" ", "_");
                        wk_str = wk_str.Replace("PCT_OF_TOTAL", "SHARE");
                        if (wk_str.Equals("TOTAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_PCTYTOTAL_chkbox.Checked = true;
                            if (pcty_show < 1000)
                            {
                                pcty_show += 1000;
                            }
                        }
                        else if (wk_str.Equals("FULL_DETAIL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_PCTYDETAIL_chkbox.Checked = true;
                            if (pcty_show / 10 < 1)
                            {
                                pcty_show += 1;
                            }
                        }
                        else if (wk_str.Equals("ITEMS_BELOW", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_PCTYSUB_chkbox.Checked = true;
                            if (pcty_show / 100 < 10)
                            {
                                pcty_show += 10;
                            }
                        }
                        else if (wk_str.Equals("SHARE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_PCTYShare_chkbox.Checked = true;
                            if (pcty_show / 1000 < 100)
                            {
                                pcty_show += 100;
                            }
                        }
                        else if (wk_str.Equals("TOTAL_LABEL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_PCTY_TotalLabel_chkbox.Checked = true;
                        }
                    }
                    else if (wk_str.Equals("PCTY_OBJ_LABEL", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        PCTY_Total_TextBox.Text = wk_str;
                    }
                    else if (wk_str.Equals("FLOW", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        if (wk_str.Equals("IMPORTS", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_imports_ckbox.Checked = true;
                            Flow_exports_ckbox.Checked = false;
                            Flow_balance_ckbox.Checked = false;
                            Flow_2way_ckbox.Checked = false;
                            flow_check = 1;
                            if (wk_data_id.IndexOf("USST") >= 0)
                            {
                                UNFlow_TotalExp_ckbox.Checked = true;
                            }
                            else
                            if (wk_data_id.IndexOf("UN") >= 0)
                            {
                                UNFlow_TotalExp_ckbox.Checked = true;
                            }

                        }
                        else if (wk_str.Equals("EXPORTS", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_exports_ckbox.Checked = true;
                            Flow_imports_ckbox.Checked = false;
                            Flow_balance_ckbox.Checked = false;
                            Flow_2way_ckbox.Checked = false;
                            flow_check = 2;
                            if (wk_data_id.IndexOf("USST") >= 0)
                            {
                                UNFlow_GenImp_ckbox.Checked = true;
                            }
                            else
                            if (wk_data_id.IndexOf("UN") >= 0)
                            {
                                UNFlow_GenImp_ckbox.Checked = true;
                            }

                        }


                    } // end flow
                    else if (wk_str.Equals("FLOW_US_TYPE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        if (wk_str.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_GenImp_ckbox.Checked = true;
                            Flow_TotalExp_ckbox.Checked = true;
                            Flow_FASVal_ckbox.Checked = true;
                            if (wk_data_id.IndexOf("USST") >= 0)
                            {
                                UNFlow_GenImp_ckbox.Checked = true;
                            }
                        }
                        else if (wk_str.Equals("CONSUMPTION", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_ConImp_ckbox.Checked = true;
                            Flow_TotalExp_ckbox.Checked = true;
                            Flow_FASVal_ckbox.Checked = true;
                            if (wk_data_id.IndexOf("USST") >= 0)
                            {
                                UNFlow_ConImp_ckbox.Checked = true;
                            }

                        }
                        else if (wk_str.Equals("DOMESTIC", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_DomExp_ckbox.Checked = true;
                            Flow_CustVal_ckbox.Checked = true;
                            Flow_GenImp_ckbox.Checked = true;

                        }
                        else if (wk_str.Equals("FOREIGN", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_ForExp_ckbox.Checked = true;
                            Flow_CustVal_ckbox.Checked = true;
                            Flow_GenImp_ckbox.Checked = true;

                        }
                        else if (wk_str.Equals("TOTAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_TotalExp_ckbox.Checked = true;
                            Flow_CustVal_ckbox.Checked = true;
                            Flow_GenImp_ckbox.Checked = true;

                            if (wk_data_id.IndexOf("USST") >= 0)
                            {
                                UNFlow_TotalExp_ckbox.Checked = true;
                            }
                        }



                    } // end flow
                    else if (wk_str.Equals("FLOWTYPE_DETAIL", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        wk_str = wk_str.Replace(" ", "_");
                        if (wk_str.Equals("CUSTOMS", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_CustVal_ckbox.Checked = true;
                        }
                        else if (wk_str.IndexOf("FAS") >= 0)
                        {
                            Flow_FASVal_ckbox.Checked = true;
                            if (wk_data_id.IndexOf("USST") >= 0)
                            {
                                UNFlow_Value_ckbox.Checked = true;
                            }
                            else
                            if (wk_data_id.IndexOf("UN") >= 0)
                            {
                                UNFlow_Value_ckbox.Checked = true;
                            }


                        }
                        else if (wk_str.IndexOf("WEIGHT") >= 0)
                        {
                            Flow_CustVal_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("CIF", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_CifVal_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("DUTIABLE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_DutiableVal_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("DUTY", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_Duty_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("CHARGES", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_Charges_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("QUANTITY1", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (flow_check == 1)
                            {
                                Flow_Qty1_ckbox.Checked = true;
                            }
                            else if (flow_check == 2)
                            {
                                Flow_ExpQty1_ckbox.Checked = true;
                            }
                            else if (flow_check == 0)
                            {
                                Flow_Qty1_ckbox.Checked = true;
                                Flow_ExpQty1_ckbox.Checked = true;
                            }

                        }
                        else if (wk_str.Equals("QUANTITY2", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (flow_check == 1)
                            {
                                Flow_Qty2_ckbox.Checked = true;
                            }
                            else if (flow_check == 2)
                            {
                                Flow_ExpQty2_ckbox.Checked = true;
                            }
                            else if (flow_check == 0)
                            {
                                Flow_Qty2_ckbox.Checked = true;
                                Flow_ExpQty2_ckbox.Checked = true;
                            }
                        }
                        else if (wk_str.Equals("LANDED_COST", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Flow_LDP_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("UV1", StringComparison.InvariantCultureIgnoreCase))
                        {
                            UV1_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("UV2", StringComparison.InvariantCultureIgnoreCase))
                        {
                            UV2_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("AVE1", StringComparison.InvariantCultureIgnoreCase))
                        {
                            AVE1_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("AVE2", StringComparison.InvariantCultureIgnoreCase))
                        {
                            AVE2_ckbox.Checked = true;
                        }

                    } // end flowtype_detail
                    else if (wk_str.Equals("YEARS_FREQ", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        if (wk_str.Equals("ANNUAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Year_ann_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("YEAR-TO-DATE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Year_ytd_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("MONTHLY", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Year_month_ckbox.Checked = true;
                        }
                        else if (wk_str.Equals("QUARTERLY", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Year_quarter_ckbox.Checked = true;
                        }
                    }
                    else if (wk_str.Equals("YEARS", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Show_Year1_DropDownList.SelectedIndex = -1;
                        wk_str = row[3].ToString().ToUpper();
                        Show_Year1_DropDownList.SelectedIndex = -1;
                        Show_Year1_DropDownList.Items.FindByValue(wk_str).Selected = true;
                        wk_str = row[4].ToString().ToUpper();
                        Show_Year2_DropDownList.SelectedIndex = -1;
                        Show_Year2_DropDownList.Items.FindByValue(wk_str).Selected = true;
                    } // ann years
                    else if (wk_str.Equals("YEARS_Y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        Show_YTD1_DropDownList.SelectedIndex = -1;
                        Show_YTD1_DropDownList.Items.FindByValue(wk_str).Selected = true;
                        wk_str = row[4].ToString().ToUpper();
                        Show_YTD2_DropDownList.SelectedIndex = -1;
                        Show_YTD2_DropDownList.Items.FindByValue(wk_str).Selected = true;
                    } // YTD years
                    else if (wk_str.Equals("YEARS_M", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        Show_Month1_DropDownList.SelectedIndex = -1;
                        Show_Month1_DropDownList.Items.FindByValue(wk_str).Selected = true;
                        wk_str = row[4].ToString().ToUpper();
                        Show_Month2_DropDownList.SelectedIndex = -1;
                        Show_Month2_DropDownList.Items.FindByValue(wk_str).Selected = true;
                    } // MTH years
                    else if (wk_str.Equals("YEARS_Q", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        Show_Quarter1_DropDownList.SelectedIndex = -1;
                        Show_Quarter1_DropDownList.Items.FindByValue(wk_str).Selected = true;
                        wk_str = row[4].ToString().ToUpper();
                        Show_Quarter2_DropDownList.SelectedIndex = -1;
                        Show_Quarter2_DropDownList.Items.FindByValue(wk_str).Selected = true;
                    } // MTH years
                    else if (wk_str.Equals("STATE_ZIN_V", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString();
                        if (wk_str.ToUpper().Trim().Equals(".USA") && states_sel_cnt <= state_total_flag)
                        {
                            STATE_TOTAL_ALL_CkBox.Checked = true;
                            State_Panel.Height = 150;
                            State_Panel.Visible = true;
                            Selected_STATE_Panel.Visible = false;
                            State_Selections_Panel2.Visible = false;
                        }
                        else if (STATE_TOTAL_ALL_CkBox.Checked)
                        {
                            STATE_SELECTED_CkBox.Checked = true;
                            if (!Selected_STATE_Panel.Visible)
                            {
                                Selected_STATE_Panel.Visible = true;
                                State_Selections_Panel2.Visible = true;
                                Selected_STATE_Panel.Visible = true;
                                State_Panel.Height = 730;
                                State_Selections_Panel2.Visible = true;
                            }
                            if (String.IsNullOrEmpty(row[4].ToString()))
                            {
                                wk_str = row[3].ToString();
                            }
                            else
                            {
                                wk_str = row[3].ToString() + "-" + row[4].ToString();
                            }

                            States_Selections_TextBox.Text += wk_str + "\r\n";
                        }
                    }
                    else if (wk_str.Equals("STATE_SHOW", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        wk_str = wk_str.Replace(" ", "_");
                        wk_str = wk_str.Replace("PCT_OF_TOTAL", "SHARE");
                        if (wk_str.Equals("TOTAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATETOTAL_chkbox.Checked = true;
                            if (state_show < 1000)
                            {
                                state_show += 1000;
                            }
                        }
                        else if (wk_str.Equals("FULL_DETAIL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATEDETAIL_chkbox.Checked = true;
                            if (state_show / 10 < 1)
                            {
                                state_show += 1;
                            }
                        }
                        else if (wk_str.Equals("ITEMS_BELOW", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATESUB_chkbox.Checked = true;
                            if (state_show / 100 < 10)
                            {
                                state_show += 10;
                            }
                        }
                        else if (wk_str.Equals("SHARE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATEShare_chkbox.Checked = true;
                            if (state_show / 1000 < 100)
                            {
                                state_show += 100;
                            }
                        }
                        else if (wk_str.Equals("TOTAL_LABEL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATE_TotalLabel_chkbox.Checked = true;
                        }
                    }
                    else if (wk_str.Equals("STATE_OBJ_LABEL", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        STATE_Total_TextBox.Text = wk_str;
                    }
                    else if (wk_str.Equals("DIST_ZIN_V", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString();
                        if (STATE_TOTAL_ALL_CkBox.Checked)
                        {
                            STATE_SELECTED_CkBox.Checked = true;
                            if (!Selected_STATE_Panel.Visible)
                            {
                                State_Selections_Panel2.Visible = true;
                                Selected_STATE_Panel.Visible = true;
                                STATE_Ajax_Panel1.Update();
                            }

                        }
                        if (String.IsNullOrEmpty(row[4].ToString()))
                        {
                            wk_str = row[3].ToString();
                        }
                        else
                        {
                            wk_str = row[3].ToString() + "-" + row[4].ToString();
                        }
                        States_Selections_TextBox.Text += wk_str + "\r\n";
                    }
                    else if (wk_str.Equals("DIST_SHOW", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        wk_str = wk_str.Replace(" ", "_");
                        wk_str = wk_str.Replace("PCT_OF_TOTAL", "SHARE");
                        if (wk_str.Equals("TOTAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATETOTAL_chkbox.Checked = true;
                            if (dist_show < 1000)
                            {
                                dist_show += 1000;
                            }
                        }
                        else if (wk_str.Equals("FULL_DETAIL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATEDETAIL_chkbox.Checked = true;
                            if (dist_show / 10 < 1)
                            {
                                dist_show += 1;
                            }
                        }
                        else if (wk_str.Equals("ITEMS_BELOW", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATESUB_chkbox.Checked = true;
                            if (dist_show / 100 < 10)
                            {
                                dist_show += 10;
                            }
                        }
                        else if (wk_str.Equals("SHARE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATEShare_chkbox.Checked = true;
                            if (dist_show / 1000 < 100)
                            {
                                dist_show += 100;
                            }
                        }
                        else if (wk_str.Equals("TOTAL_LABEL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATE_TotalLabel_chkbox.Checked = true;
                        }
                    }
                    else if (wk_str.Equals("DIST_OBJ_LABEL", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        STATE_Total_TextBox.Text = wk_str;
                    }

                    else if (wk_str.Equals("SPI_ZIN_V", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString();
                        if (SPI_TOTAL_ALL_CkBox.Checked)
                        {
                            SPI_SELECTED_CkBox.Checked = true;
                            if (!Selected_SPI_Panel.Visible)
                            {
                                Selected_SPI_Panel.Visible = true;
                                SPIs_Update_Panel.Update();
                            }

                        }
                        if (String.IsNullOrEmpty(row[4].ToString()))
                        {
                            wk_str = row[3].ToString();
                        }
                        else
                        {
                            wk_str = row[3].ToString() + "-" + row[4].ToString();
                        }
                        States_Selections_TextBox.Text += wk_str + "\r\n";
                    }
                    else if (wk_str.Equals("SPI_SHOW", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        wk_str = wk_str.Replace(" ", "_");
                        wk_str = wk_str.Replace("PCT_OF_TOTAL", "SHARE");
                        if (wk_str.Equals("TOTAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_SPITOTAL_chkbox.Checked = true;
                            if (dist_show < 1000)
                            {
                                dist_show += 1000;
                            }
                        }
                        else if (wk_str.Equals("FULL_DETAIL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_SPIDETAIL_chkbox.Checked = true;
                            if (dist_show / 10 < 1)
                            {
                                dist_show += 1;
                            }
                        }
                        else if (wk_str.Equals("ITEMS_BELOW", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_SPISUB_chkbox.Checked = true;
                            if (dist_show / 100 < 10)
                            {
                                dist_show += 10;
                            }
                        }
                        else if (wk_str.Equals("SHARE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_SPIShare_chkbox.Checked = true;
                            if (dist_show / 1000 < 100)
                            {
                                dist_show += 100;
                            }
                        }
                        else if (wk_str.Equals("TOTAL_LABEL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_STATE_TotalLabel_chkbox.Checked = true;
                        }
                    }
                    else if (wk_str.Equals("SPI_OBJ_LABEL", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        SPI_Total_TextBox.Text = wk_str;
                    }

                    else if (wk_str.Equals("RCTY_ZIN_V", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (Data_ID.IndexOf("UN") >= 0 && rcty_world_flag != 9999)
                        {
                            wk_str = "RT";
                        }
                        cnt_rcty_zin_v += 1;
                        wk_str = row[3].ToString();
                        Parmload_Test_Label1.Text += " " + wk_str.Substring(0, 1);
                        Parmload_Test_Label1.DataBind();

                        if (RCTY_TOTAL_ALL_CkBox.Checked)
                        {
                            RCTY_SELECTED_CkBox.Checked = true;
                            if (!Reporter_Panel.Visible)
                            {
                                Reporter_Panel.Visible = true;
                                Selected_Reporter_Panel.Visible = true;
                                Display_RCTYSUB_chkbox.Visible = true;
                                Reporter_Panel.Height = 730;
                            }

                        }
                        if (String.IsNullOrEmpty(row[4].ToString()))
                        {
                            wk_str = row[3].ToString();
                        }
                        else
                        {
                            wk_str = row[3].ToString() + "-" + row[4].ToString();
                        }
                        RCTY_Selections_TextBox.Text += wk_str + "\r\n";
                    }
                    else if (Reporter_Panel.Visible
                    && wk_str.Equals("RCTY_SHOW", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wk_str = row[3].ToString().ToUpper();
                        wk_str = wk_str.Replace(" ", "_");
                        wk_str = wk_str.Replace("PCT_OF_TOTAL", "SHARE");
                        if (wk_str.Equals("TOTAL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_RCTYTOTAL_chkbox.Checked = true;
                            if (rcty_show < 1000)
                            {
                                rcty_show += 1000;
                            }
                        }
                        else if (wk_str.Equals("FULL_DETAIL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_RCTYDETAIL_chkbox.Checked = true;
                            if (rcty_show / 10 < 1)
                            {
                                rcty_show += 1;
                            }
                        }
                        else if (wk_str.Equals("ITEMS_BELOW", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_RCTYSUB_chkbox.Checked = true;
                            if (rcty_show / 100 < 10)
                            {
                                rcty_show += 10;
                            }
                        }
                        else if (wk_str.Equals("SHARE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_RCTYShare_chkbox.Checked = true;
                            if (rcty_show / 1000 < 100)
                            {
                                rcty_show += 100;
                            }
                        }
                        else if (wk_str.Equals("TOTAL_LABEL", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Display_RCTY_TotalLabel_chkbox.Checked = true;
                        }
                    }
                    else if (Reporter_Panel.Visible
                    && wk_str.Equals("RCTY_OBJ_LABEL", StringComparison.InvariantCultureIgnoreCase))
                    {

                        wk_str = row[3].ToString().ToUpper();
                        RCTY_Total_TextBox.Text = wk_str;
                    }

                    wk_str = "";



                } // end for each

                if (!Int32.TryParse(Job_Num_Str, out J_NUM))
                {
                    Job_Num_Str = "111001";
                    J_NUM = 111001;
                    User_JobNum_TextBox1.Text = Job_Num_Str;

                }
                else if (Job_Num_Str.Length == 6)
                {
                    User_JobNum_TextBox1.Text = Job_Num_Str.ToString();
                }


                Misc_Textbox2.Text += " end loop 2 cnt=" + wk_cnt_rows.ToString();
                if (Selected_Partner_Panel.Visible)
                {
                    Display_PCTYSUB_chkbox.Visible = true;
                    Partner_Panel.Height = 730;
                    Display_PCTYTOTAL_chkbox.Text = "Total of Selected";
                    Display_PCTYDETAIL_chkbox.Text = "Show All Partners Selected";
                    PCTY_Ajax_Panel1.Update();
                }
                if (Reporter_Panel.Visible)
                {
                    Display_RCTYSUB_chkbox.Visible = true;
                    Reporter_Panel.Height = 730;
                    Display_RCTYTOTAL_chkbox.Text = "Total of Selected";
                    Display_RCTYDETAIL_chkbox.Text = "Show All Reporters Selected";
                    Reporter_Update_Panel.Update();
                }
                int wk_i = 0;
                if (Rank_Flag == 1)
                {
                    wk_i = Rank_Parm_DropDownList1.Items.IndexOf(Rank_Parm_DropDownList1.Items.FindByValue(Rank_Var));
                    if (wk_i >= 0)
                    {
                        Rank_Parm_DropDownList1.SelectedIndex = wk_i;
                        wk_i = Rank_Year_DropDownList1.Items.IndexOf(Rank_Year_DropDownList1.Items.FindByValue(Rank_Year_Str));
                        if (wk_i >= 0)
                        {
                            Rank_Year_DropDownList1.SelectedIndex = wk_i;
                            Rank_CheckBox1.Checked = true;
                            UserRank_TextBox1.Text = Rank_Topx_Str;

                        }
                    }
                }

                CMD_Ajax_Panel1.Update();
                DIGITS_ID_Dropdown1.SelectedIndex = -1;
                DIGITS_ID_Dropdown1.Items.FindByValue(wk_digits).Selected = true;
                wk_str = DIGITS_ID_Dropdown1.SelectedValue;
                Get_Load_Flag.Text = "LOAD";
                Get_Conc_Text.Text = wk_conc;
                Get_Digits_Text.Text = wk_conc;
                Get_Conc_Index.Text = i_sel.ToString();
                Get_Digits_Index.Text = wk_digits;

            } // end try
            catch (Exception db1_err)
            {
                Misc_Databox1.Text = "CONNECTION ERROR:" + db1_err.Message;
                sqlconn.Close();
                //sda.Dispose() ;
                //sdb.Dispose();

            }
            finally
            {
                sqlconn.Close();
                //sda.Dispose();
                //sdb.Dispose();

            }
            Data_Update_Panel_Years.Update();
            CMD_Ajax_Panel1.Update();
            Main_Update_Panel.Update();
            if (wk_cnt_rows > 1)
            {
                Load_Complete_Flag = 1;
            }

            Parmload_Test_Label1.Text += " (rcty_zin_v=" + cnt_rcty_zin_v.ToString() + ")";
            LoadJob_Error_Label1.Text += " 10";

        } // ljouter
        catch (Exception ljouter)
        {
            LoadJob_Error_Label1.Text = "ERROR: " + ljouter.Message.ToString();
        }


    } // end Load_Parmfile

    // new code job deletion feb15 2022

    protected void JOBDEL_MoveSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in JOBDEL_Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (li.Text.Length > 0)
                {
                    builder.Append(li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (JOBDEL_Selections_TextBox.Text.Length > 0)
                JOBDEL_Selections_TextBox.Text = JOBDEL_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                JOBDEL_Selections_TextBox.Text = builder.ToString();
        }

    }
    protected void JOBDEL_MoveAll_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        foreach (ListItem li in JOBDEL_Pick_ListBox.Items)
        {
            if (li.Text.Length > 0)
            {
                builder.Append(li.Text + "\r\n");
            }
        }
        if (builder.Length > 0)
        {
            if (JOBDEL_Selections_TextBox.Text.Length > 0)
                JOBDEL_Selections_TextBox.Text += "\r\n" +
                    builder.ToString();
            else
                JOBDEL_Selections_TextBox.Text = builder.ToString();

        }
    }
    protected void JOBDEL_RemoveAll_Button_Click(object sender, EventArgs e)
    {
        JOBDEL_Selections_TextBox.Text = "";
        //Select_List_Num_DropDownList.Items.Clear() ;   


    }

    protected void JOBDEL_ckbox_changed(object sender, EventArgs e)
    {
        if (Delete_Jobs_chkbox.Checked)
        {
            Selected_JobDelete_Panel.Visible = false;
            Selected_JobDelete_Panel.Height = 30;
            Delete_Job_Btn.Text = "Select Jobs to Delete";
            Delete_Jobs_chkbox.DataBind();
            Refresh_Job_List_Click(sender, e);
        }
        else
        {
            Selected_JobDelete_Panel.Visible = true;
            Selected_JobDelete_Panel.Height = 700;
            Refresh_Job_List_Click(sender, e);
        }


        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }


    // end new code job deletion feb 15 2022

    // del job proc feb16 2022
    protected void Delete_Selected_Job_Btn_Click(object sender, EventArgs e)
    {
        Write_DeleteJobs(sender, e);
    }
    protected void Enable_District_Btn_Click(object sender, EventArgs e)
    {
        if (!State_Panel.Visible)
        {
            State_Panel.Visible = true;
            Selected_STATE_Panel.Visible = false;
            State_Panel.Height = 150;
            STATE_Ajax_Panel1.Update();
        }
        else
        {
            State_Panel.Visible = false;
            Selected_STATE_Panel.Visible = false;
            State_Panel.Height = 150;

            STATE_Ajax_Panel1.Update();
        }
    }
    protected void Enable_SPI_Btn_Click(object sender, EventArgs e)
    {
        if (!SPI_Panel.Visible)
        {
            SPI_Panel.Visible = true;
            Selected_SPI_Panel.Visible = false;
            SPI_TOTAL_ALL_CkBox.Checked = true;
            SPI_SELECTED_CkBox.Checked = false;
            Display_SPIDETAIL_chkbox.Checked = false;
            SPI_Panel.Height = 150;
            SPIs_Update_Panel.Update();
        }
        else
        {
            SPI_Panel.Visible = false;
            Selected_SPI_Panel.Visible = false;
            SPI_Panel.Height = 150;
            SPIs_Update_Panel.Update();
        }
    }
    protected void Write_DeleteJobs(object sender, EventArgs e)
    {
        SqlConnection Conn2;
        string seq_num;
        string connString;
        string strSQL;
        string wk_user_id = "POMEROR";
        string wk_user_email = "ROGER.POMEROY@TRADE.GOV";
        int Line_Num = 0;
        int J_NUM = 111001;
        int cur_job_num = 0;
        int misc_flag = 0;
        string wk_str = "";
        int wk_yr1_int = 0;
        int wk_yr2_int = 0;
        string wk_yr1 = "";
        string wk_yr2 = "";
        int flag_time = 0;

        int cnt_selected_jobs = 0;
        int cnt_loop = 0;
        int num_items = 0;
        //int i=0 ;

        string wk_str2 = "";

        //"UID=TPISGUI;Password=Log$me#in2020;" +


        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=US_DATA;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";
        Job_Num_Str = User_JobNum_TextBox1.Text.ToString();

        wk_user_id = User_ID_Valid_Label1.Text.ToString();
        wk_user_email = User_ID_Email_Label1.Text.ToString().ToUpper();
        if (Selected_JobDelete_Panel.Visible)
        {



            if (String.IsNullOrEmpty(wk_user_id) || String.IsNullOrEmpty(wk_user_email))
            {
                Delete_Status_Label1.Text = "USER NOT FOUND ERROR!";
                Delete_Status_Label1.DataBind();
                //Selected_JobDelete_Panel.Update();
                return;

            }


            strSQL = "select next value for dbo.tpis_deletejob_seq ";
            SqlCommand sqlcmd = new SqlCommand(strSQL, sqlconn);
            sqlconn.Open();
            Job_Num_Str = Convert.ToString(sqlcmd.ExecuteScalar());

            sqlconn.Close();
            if (!Int32.TryParse(Job_Num_Str, out J_NUM))
            {
                Job_Num_Str = "10001";
                J_NUM = 10001;
                // User_JobNum_TextBox1.Text = Job_Num_Str;

            }

            //Job_Delete_Status_Label1.Text = Job_Num_Str;




            cnt_selected_jobs = 0;
            //num_items=JOBDEL_Selections_TextBox.Lines.Length;

            //    for (int line = 0; line < lineCount; line++)
            //        // GetLineText takes a zero-based line index.
            //        lines.Add(textBox.GetLineText(line));

            //string[] strArr = JOBDEL_Selections_TextBox.Text.Split (new string[]{System.Environment.NewLine}, StringSplitOptions.None );
            string[] strArr = Regex.Split(JOBDEL_Selections_TextBox.Text.Trim(), "\n");
            cur_job_num = 0;
            num_items = 0;
            num_items = strArr.Length;
            wk_str = "";
            wk_str2 = "";

            //for (int i = 0; i < num_items; i++)
            if (num_items > 0)
            {
                DataTable dt = new DataTable();
                try
                {
                    //Add columns  
                    dt.Columns.Add(new DataColumn("JOB_NUM", typeof(int)));
                    dt.Columns.Add(new DataColumn("in_line", typeof(int)));
                    dt.Columns.Add(new DataColumn("in_type", typeof(string)));
                    dt.Columns.Add(new DataColumn("in_text1", typeof(string)));
                    dt.Columns.Add(new DataColumn("in_text2", typeof(string)));
                    //Add rows  

                    foreach (string s in strArr)
                    {
                        cnt_loop = cnt_loop + 1;

                        wk_str2 = "";
                        wk_str = "";
                        wk_str = s.ToString().Replace("\n", "").Replace("\r", "");

                        //wk_str=Selected_CMD_ListBox.Items.ToString() ;
                        if (!String.IsNullOrEmpty(wk_str))
                        {

                            if (wk_str.IndexOf(".") >= 1)
                            {
                                wk_str = wk_str.Substring(0, wk_str.IndexOf("."));
                            }
                            else if (wk_str.IndexOf(" ") > 1)
                            {
                                wk_str = wk_str.Substring(0, wk_str.IndexOf(" "));
                            } // END RANGES
                            if (wk_str.Length > 6)
                            {
                                wk_str = wk_str.Substring(0, 6).Replace(".", "");
                            }
                            else
                            {
                                wk_str = wk_str.Replace(".", "");

                            }
                            if (Int32.TryParse(wk_str, out cur_job_num))
                            {
                                if (cur_job_num > 100000 && cur_job_num < 1000000)
                                {
                                    cnt_selected_jobs = cnt_selected_jobs + 1;
                                    Line_Num += 1;
                                    dt.Rows.Add(J_NUM, cur_job_num, "DELETE_JOB", wk_user_email, wk_user_id);
                                }
                            }
                        } // string not empty

                    } // end loop over jobs
                    if (1 == 1 && cnt_selected_jobs > 0)
                    {
                        Job_Data_GridView.Visible = true;
                        Job_Data_GridView.DataSource = dt;
                        Job_Data_GridView.DataBind();
                    }

                    //SqlCommand sqlcom = new SqlCommand("dbo.spt_tpis_jobparms", sqlconn);
                    SqlCommand sqlcom = new SqlCommand("dbo.spt_tpis_deletejobs", sqlconn);
                    sqlcom.CommandType = CommandType.StoredProcedure;
                    sqlcom.Parameters.AddWithValue("@insertjobdata", dt);
                    sqlcom.Parameters.AddWithValue("@JOBNUM", J_NUM);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                    dt.Dispose();
                    // return "Hello" ;
                } // end try
                catch (Exception ex)
                {
                    if (wk_user_id.ToUpper() == "POMEROR" || wk_user_id.ToUpper() == "TPISTEST")
                    {
                        Table_Fmt1_Literal.Text = strSQL + " job delete loading error " + ex.Message;
                        Table_Fmt1_Literal.Visible = true;
                        //Show_Table_Panel.Visible=true ;
                        //Show_Table_Panel.Style["display"]="block" ;
                        //flag_table_errors=1 ;
                    }
                    else
                    {
                        Table_Fmt1_Literal.Text = "ERROR TRYING TO DELETE JOBS";

                    }
                    sqlconn.Close();
                    dt.Dispose();
                } // end catch

            } // num items > 0

            Delete_Status_Label1.Text = "Jobs Submitted to Delete " + cnt_selected_jobs.ToString();

        } // job panel Visible

    } // proc Write_DeleteJobs

    // end del job prod feb16 2022
    protected void Hide_State_Rcty_Button_Click(object sender, EventArgs e)
    {
        if (Reporter_Panel.Visible)
        {
            Reporter_Panel.Visible = false;
        }
        if (State_Panel.Visible)
        {
            State_Panel.Visible = false;
        }
        if (!Run_Job_Panel.Visible)
        {
            Run_Job_Panel.Visible = true;
        }

    }


    protected void SPI_DETAIL_ckbox_changed(object sender, EventArgs e)
    {
        string wk_region = "SPIs";
        string wk_db = Current_DBID_Label1.Text.ToString().ToUpper();
        if (wk_db.IndexOf("USST") >= 0)
        {
            wk_region = "SPIs";
        }

        if (SPI_TOTAL_ALL_CkBox.Checked)
        {
            Selected_SPI_Panel.Visible = false;
            Display_SPISUB_chkbox.Visible = false;
            SPI_Panel.Height = 150;
            Display_SPIDETAIL_chkbox.Text = "Show Every Tariff Program (SPI)";
            Display_SPITOTAL_chkbox.Text = "Total for Trade in All Programs";
        }
        else if (SPI_SELECTED_CkBox.Checked)
        {
            Selected_SPI_Panel.Visible = true;
            Display_SPISUB_chkbox.Visible = true;
            SPI_Panel.Height = 600;
            Display_SPIDETAIL_chkbox.Text = "Show Trade in Each Selected Tariff Program (SPI)";
            Display_SPITOTAL_chkbox.Text = "Calculate the Total for Your Selected Tariff Programs";
        }
        SPIs_Update_Panel.Update();

        // GoToAnchor(this.Page, "TPISSPISETUP") ;
    }



    protected void SPIGroup_DropDownList_Change(object sender, EventArgs e)
    {
        // GoToAnchor(this.Page, "TPISSPISETUP") ;
    }


    protected void SPI_MoveSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in SPIs_Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (SPIs_Selected_ListBox.Visible)
                {
                    SPIs_Selected_ListBox.Items.Add(li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append(li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (SPIs_Selections_TextBox.Text.Length > 0)
                SPIs_Selections_TextBox.Text = SPIs_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                SPIs_Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
        }

    }
    protected void SPI_MoveNotSelected_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        //lblMessage.Text += "item count: "+ListBox1.Items.Count+"<br />";
        foreach (ListItem li in SPIs_Pick_ListBox.Items)
        {
            if (li.Selected == true)
            {
                if (SPIs_Selected_ListBox.Visible)
                {
                    SPIs_Selected_ListBox.Items.Add("!" + li.Text);
                }
                if (li.Text.Length > 0)
                {
                    builder.Append("!" + li.Text + "\r\n");
                }
            }
        }
        if (builder.Length > 0)
        {
            if (SPIs_Selections_TextBox.Text.Length > 0)
                SPIs_Selections_TextBox.Text = SPIs_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                    + "\r\n" + builder.ToString();
            else
                SPIs_Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
        }

    }
    protected void SPI_MoveAll_Button_Click(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        foreach (ListItem li in SPIs_Pick_ListBox.Items)
        {
            if (SPIs_Selected_ListBox.Visible)
            {
                SPIs_Selected_ListBox.Items.Add(li.Text);
            }
            if (li.Text.Length > 0)
            {
                builder.Append(li.Text + "\r\n");
            }
        }
        if (builder.Length > 0)
        {
            if (SPIs_Selections_TextBox.Text.Length > 0)
                SPIs_Selections_TextBox.Text += "\r\n" +
                    builder.ToString();
            else
                SPIs_Selections_TextBox.Text = builder.ToString();
            // GoToAnchor(this.Page, "TPISPCTYSETUP") ;

        }
    }
    protected void RemoveAll_SPIs_Button_Click(object sender, EventArgs e)
    {
        SPIs_Selected_ListBox.Items.Clear();
        SPIs_Selections_TextBox.Text = "";
        SPIs_User_Sel_Name_TextBox1.Text = "";
        //Select_List_Num_DropDownList.Items.Clear() ;   
        SPIs_TextBox1.Text = "";

        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;

    }

    protected void SPI_NewList_Button_Click(object sender, EventArgs e)
    {

        int num_ctyselections = 0;

        // GoToAnchor(this.Page, "TPISPCTYSETUP") ;
    }

    protected void Load_Saved_CMDs_Click(object sender, EventArgs e)
    {

        int wk_cnt_rows = 0;
        string wk_str = "";
        string wk_str2 = "";
        string wk_conc = "";
        string wk_digits = "";
        string wk_flow = "";
        string List_Seq_Str = "";
        string List_Name_Str = "";
        string strSQL = "";
        string wk_conc_by_db = "";
        int strlen = 0;
        int wk_tidx = 0;


        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=tcp:ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=US_DATA;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";
        DataTable dt = new DataTable();
        wk_conc_by_db = Current_DBID_Label1.Text.ToString();

        if (wk_conc_by_db.Length < 2)
        {
            wk_conc_by_db = "US";
        }

        try
        {
            List_Seq_Str = Load_User_CMDList_DropDownList1.SelectedValue.ToString();
            List_Name_Str = Load_User_CMDList_DropDownList1.SelectedItem.Text.ToString();
            int wk_date_i = List_Name_Str.IndexOf(" (Date:");
            if (wk_date_i > 1)
            {
                List_Name_Str = List_Name_Str.Substring(0, wk_date_i).Trim();
            }
            strSQL = "SELECT Item_value, in_line, List_Seq, Conc, Digits, Flow "
            + "from dbo.TPIS_USER_LIST_VALUES_PROD where list_seq=" + List_Seq_Str
            + " order by in_line ";
            //SqlDataAdapter sda = new SqlDataAdapter();
            //  try {

            SqlCommand sqlcom = new SqlCommand(strSQL, sqlconn);
            sqlcom.CommandType = CommandType.Text;
            sqlcom.CommandText = strSQL;
            sqlconn.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(sqlcom))
            {
                sda.Fill(dt);
            }
            sqlconn.Close();

            wk_cnt_rows = 0;
            Parmload_Process_Label1.Text = "smrycnt= " + dt.Rows.Count.ToString() + ": ";
            wk_str = "";

            StringBuilder builder = new StringBuilder();
            wk_conc = "NA";



            foreach (DataRow row in dt.Rows)
            {
                wk_cnt_rows += 1;

                if (wk_conc.Equals("NA"))
                {
                    wk_conc = row[3].ToString().ToUpper().Trim();
                    if (wk_conc.IndexOf(":") > 1)
                    {
                        wk_conc = Current_DBID_Label1.Text.ToString()
                        + ":" + wk_conc.Substring(0, wk_conc.IndexOf(":"));
                    }
                    else
                    {
                        wk_conc = Current_DBID_Label1.Text.ToString()
                        + ":" + wk_conc;

                    }
                    wk_digits = row[4].ToString().ToUpper().Trim();
                    wk_flow = row[5].ToString().ToUpper().Trim();

                }
                wk_str = row[0].ToString().Trim();
                if (wk_str.Length > 0)
                {

                    wk_str.Replace("\r", "").Replace("\n", "");
                    if (".={}".IndexOf(wk_str.Substring(0, 1)) < 0)
                    {
                        if (wk_str.IndexOf("-") > 1)
                            strlen = wk_str.IndexOf("-");
                        else if (strlen < wk_str.Length && wk_str.Length <= 10)
                            strlen = wk_str.Length;
                    }
                    builder.Append(wk_str + "\r\n");


                }
            }
            if (wk_conc.IndexOf(":") < 1)
            {
                wk_conc = Current_DBID_Label1.Text.ToString()
                   + ":" + wk_conc;
            }


            wk_tidx = DataSource_DropDownList1.Items.IndexOf(DataSource_DropDownList1.Items.FindByValue(wk_conc));

            if (wk_tidx >= 0 && wk_tidx < DataSource_DropDownList1.Items.Count
               && wk_tidx != DataSource_DropDownList1.SelectedIndex)
            {

                DataSource_DropDownList1.SelectedIndex = wk_tidx;
                Database_Type_Dropdownlist.DataBind();
                DIGITS_ID_Dropdown1.DataBind();
                Series_Type_TextBox.Text = DataSource_DropDownList1.SelectedValue.ToString();
                Series_Type_TextBox.DataBind();
                CMD_Ajax_Panel1.Update();
            }
            if (wk_tidx < 0)
            {
                Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Red;
                Save_CMDs_Status_Label.Text = wk_conc_by_db + " ??: LOAD ERROR conc NOT FOUND=" + wk_conc
                + " IS DATABASE " + Current_DBID_Label1.Text.ToString() + " CORRECT?";
            }
            else
            {  // load table 
                if (builder.Length > 0)
                {
                    if (Append_CMD_Load_CkBox.Checked && Selections_CMD_TextBox.Text.Length > 0)
                        Selections_CMD_TextBox.Text =
                          Selections_CMD_TextBox.Text.ToString().TrimEnd('\r', '\n')
                          + "\r\n" + builder.ToString();
                    else
                        Selections_CMD_TextBox.Text = builder.ToString();

                }
            } // load table ok 

        } // end try
        catch (Exception excmd)
        {
            wk_str2 = excmd.Message;
            sqlconn.Close();
            dt.Dispose();
            wk_tidx = -1;

        } // end catch
        if (wk_tidx >= 0)
        {


            // new
            wk_tidx = 0;
            if (Int32.TryParse(wk_digits, out wk_tidx))
            {
                if (wk_tidx < strlen)
                {
                    wk_digits = strlen.ToString();
                }
            }
            wk_tidx = 0;
            wk_tidx = DIGITS_ID_Dropdown1.SelectedIndex;
            if (wk_tidx < 0 || (wk_tidx >= 0 && wk_tidx < strlen))
            {
                wk_tidx = DIGITS_ID_Dropdown1.Items.IndexOf(DIGITS_ID_Dropdown1.Items.FindByValue(wk_digits));

            }
            if (wk_tidx < 0)
            {
                Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Red;
                Save_CMDs_Status_Label.Text = "DIGIT LEVEL =" + wk_digits + " NOT FOUND--CHECK IF DATABASE IS CORRECT";
            }
            if (wk_tidx >= 0 && wk_tidx < DIGITS_ID_Dropdown1.Items.Count)
            {
                if (wk_digits == "10")
                {
                    try
                    {

                        if (Product_Detail1_DropDownList2.SelectedIndex + Product_Detail1_DropDownList1.SelectedIndex <= 0)
                        {
                            if (Product_DropDownList1.SelectedIndex <= 0)
                            {
                                Product_DropDownList1.SelectedIndex = 1;
                                Product_DropDownList1.DataBind();
                                wk_str = Product_DropDownList1.SelectedValue.ToString();
                                Series_TextBox.Text = wk_str;
                            }
                        } // if total and 10-digits then set to 1st product
                    }
                    catch (Exception ext10)
                    {
                        Series_TextBox.Text = "1";

                    }
                    Series_TextBox.DataBind();
                } // end special handling 10-digits
                DIGITS_ID_Dropdown1.SelectedIndex = wk_tidx;
                DIGITS_ID_Dropdown1.DataBind();
                CMD_Ajax_Panel1.Update();
            }

            // new
            if (wk_tidx >= 0)
            {
                Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Green;
                Save_CMDs_Status_Label.Text = List_Seq_Str + ":" + wk_str2 + " Num Rows=" + wk_cnt_rows.ToString()
                + " conc=" + wk_conc + " maxdigits=" + strlen
             ;
            }
        }
        if (List_Name_Str.Length > 0)
        {
            try
            {
                string wk_lstr = "";
                strSQL = "select list_name from dbo.TPIS_USER_LIST_HDR_PROD_NEW "
                    + " where LIST_SEQ=" + List_Seq_Str;
                SqlCommand sqlcmd = new SqlCommand(strSQL, sqlconn);
                sqlconn.Open();
                wk_lstr = Convert.ToString(sqlcmd.ExecuteScalar());

                sqlconn.Close();
                if (wk_lstr.Length > 0)
                    List_Name_Str = wk_lstr;
                strSQL = "select UPPER(isNull(flow,'NA')) as flow from dbo.TPIS_USER_LIST_HDR_PROD_NEW "
                    + " where LIST_SEQ=" + List_Seq_Str;
                SqlCommand sqlcmd2 = new SqlCommand(strSQL, sqlconn);
                sqlconn.Open();
                wk_lstr = Convert.ToString(sqlcmd2.ExecuteScalar());

                sqlconn.Close();
                if (wk_lstr.Length > 0)
                {
                    if (wk_lstr.Equals("EXP"))
                    {
                        Flow_exports_ckbox.Checked = true;
                        Flow_imports_ckbox.Checked = false;
                    }
                    else if (wk_lstr.Equals("EXP"))
                    {
                        Flow_exports_ckbox.Checked = false;
                        Flow_imports_ckbox.Checked = true;
                    }

                }

            }
            catch (Exception exlistname)
            {
                wk_tidx = 0;
            }

            User_CMDsSel_Name_TextBox1.Text = List_Name_Str;
            CMD_Total_TextBox.Text = List_Name_Str;
            Display_CMD_TotalLabel_chkbox.Checked = true;
        }
        Save_CMDs_Status_Label.DataBind();
        Selections_CMD_TextBox.DataBind();
        CMD_Ajax_Panel1.Update();

    } // end Load_Saved_CMDs_Click


    protected void Save_User_List(string p_prmtype, int p_resave)
    {
        // p_resave
        SqlConnection Conn2;
        int wk_resave = 0; // save a new list
        string List_Seq_Str = "";
        string connString;
        string strSQL;
        string wk_parmtype = p_prmtype.ToUpper();
        string wk_user = "POMEROR";
        string wk_user_email = "ROGER.POMEROY@TRADE.GOV";
        int Line_Num = 0;
        int LIST_NUM = 199001;
        int misc_flag = 0;
        string wk_str = "";
        string wk_str2 = "";
        int wk_yr1_int = 0;
        int wk_yr2_int = 0;
        string wk_yr1 = "";
        string wk_yr2 = "";
        int flag_time = 0;
        bool world_flag = false;
        int partner_flag = 0;
        int partner_share = 0;

        int cnt_selected_cmds = 0;
        int cnt_subgroups = 0;
        int digit_level = 0;
        int total_all = 0;
        int cnt_loop = 0;
        int num_items = 0;
        int flag_conc_ck = 0;
        int flag_values = 0;
        string conc_ck = "";
        string digits_ck = "";
        string wk_list_name = "";
        string list_type_id = "COMMODITY";
        string passed_user_id = "";
        string passed_user_email = "";
        string wk_listseq_type_str = "7";
        int wk_listseq_type_num = 7000000;
        SqlConnection sqlconn = new SqlConnection();
        int wk_i = 0;
        int i_len = 0;
        int cnt_selected_states = 0;
        cnt_subgroups = 0;
        total_all = 0;
        int state_flag = 0;
        int state_share = 0;
        int district_flag = -1;
        string wk_region_code = "STATE";
        string wk_out_zin_v = "";
        string wk_out_show = "";
        string wk_out_obj = "";
        string wk_out_other = "";
        string wk_cur_db = "USST";
        string wk_save_name = "";

        wk_cur_db = Current_DBID_Label1.Text.ToString().ToUpper();
        bool usa_flag = false;
        bool show_states = false;

        Save_CMDs_Status_Label.Visible = true;
        Save_CMDs_Status_Label.Text = "Starting USER LIST SAVE ";
        Save_CMDs_Status_Label.DataBind();

        if (p_resave == 1)
        {
            wk_resave = 1;
        }
        wk_parmtype = p_prmtype.ToUpper().Trim();
        Save_CMDs_Status_Label.Visible = true;
        Save_CMDs_Status_Label.Text = "Starting USER LIST SAVE for " + wk_parmtype;
        Save_CMDs_Status_Label.DataBind();
        // return ;
        try
        {
            if (wk_parmtype.Equals("CMD"))
            {
                wk_listseq_type_str = "1";
                wk_listseq_type_num = 1000000;
                list_type_id = "COMMODITY";
                wk_list_name = User_CMDsSel_Name_TextBox1.Text.ToString();
            }
            else if (wk_parmtype.Equals("CTY") || wk_parmtype.Equals("PCTY") || wk_parmtype.Equals("RCTY"))
            {
                wk_listseq_type_str = "2";
                wk_listseq_type_num = 2000000;
                list_type_id = "COUNTRY";
                wk_list_name = User_PCTYsSel_Name_TextBox1.Text.ToString().ToUpper();

            }
            else if (wk_parmtype.Equals("STATE"))
            {
                wk_listseq_type_str = "6";
                wk_listseq_type_num = 6000000;
                list_type_id = "STATE";
                wk_list_name = STATE_User_Sel_Name_TextBox1.Text.ToString().ToUpper();
            }
            else if (wk_parmtype.Equals("DISTRICT"))
            {
                wk_listseq_type_str = "3";
                wk_listseq_type_num = 3000000;
                list_type_id = "DISTRICT";
                wk_list_name = STATE_User_Sel_Name_TextBox1.Text.ToString().ToUpper();
            }
            else if (wk_parmtype.Equals("SPI"))
            {
                wk_listseq_type_str = "5";
                wk_listseq_type_num = 5000000;
                list_type_id = "SPI";
            }
            else if (wk_parmtype.Equals("SERIES"))
            {
                wk_listseq_type_str = "7";
                wk_listseq_type_num = 7000000;
                list_type_id = "SERIES";
            }






            //"UID=TPISGUI;Password=Log$me#in2020;" +


            sqlconn.ConnectionString =
            "Data Source=ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
            "Initial Catalog=US_DATA;" +
            "UID=tpisgui;Password=Log$mein2021;" +
            "Integrated Security=False;";
            List_Seq_Str = User_ListSeq_Label1.Text.ToString();
            List_NewSeq_Label1.Text = "Old Seq #: " + List_Seq_Str;

            wk_user = User_ID_Valid_Label1.Text.ToString();
            wk_user_email = User_ID_Email_Label1.Text.ToString().ToUpper();

            passed_user_id = SubmitJob_User_ID_Label1.Text.ToString().ToUpper();
            passed_user_email = SubmitJob_User_Email_Label1.Text.ToString().ToUpper();

            if (!String.Equals(passed_user_id, "ANONYMOUS", StringComparison.OrdinalIgnoreCase)
                && !String.IsNullOrEmpty(passed_user_id))
            {
                if (passed_user_email.IndexOf("@TRADE.GOV", 0, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    wk_user = passed_user_id;
                    wk_user_email = passed_user_email;
                }

            }
            if (String.IsNullOrEmpty(wk_user))
            {
                wk_user = User_ID_DropDownList.SelectedValue.ToString();

            }



            if (wk_resave < 1)
            {
                strSQL = "select next value for dbo.tpis_user_list_seq ; ";
                SqlCommand sqlcmd = new SqlCommand(strSQL, sqlconn);
                sqlconn.Open();
                List_Seq_Str = Convert.ToString(sqlcmd.ExecuteScalar());

                sqlconn.Close();
            }
            if (!Int32.TryParse(List_Seq_Str, out LIST_NUM))
            {
                List_Seq_Str = "999999";
                LIST_NUM = 999999;
                User_ListSeq_Label1.Text = List_Seq_Str;

            }

            Save_CMDs_Status_Label.Text = "After seq" + " (" + List_Seq_Str + ") " + wk_parmtype;
            Save_CMDs_Status_Label.DataBind();

            //return ;
        }
        catch (Exception extsv)
        {
            Save_CMDs_Status_Label.Text = "ERRROR " + extsv.Message.ToString();
            Save_CMDs_Status_Label.DataBind();
            return;

        }

        User_ListSeq_Label1.Text = List_Seq_Str;
        List_NewSeq_Label1.Text += " New List #: " + List_Seq_Str
        + " LIST_NUM=" + LIST_NUM.ToString();

        wk_str = Get_Load_Flag.Text.ToUpper();
        if (wk_str.IndexOf("LOAD") >= 0)
        {
            flag_conc_ck = 1;
            conc_ck = Get_Conc_Text.Text.ToString().ToUpper();
            digits_ck = Get_Digits_Text.Text.ToString().ToUpper();
            if (DataSource_DropDownList1.SelectedIndex >= 0)
            {
                wk_str = DataSource_DropDownList1.SelectedValue.ToString().ToUpper();
            }
            else if (!String.IsNullOrEmpty(conc_ck))
            {

                DataSource_DropDownList1.Items.FindByValue(conc_ck).Selected = true;
            }
        }
        wk_str = "NA";


        strSQL = "List_seq=" + List_Seq_Str;
        Misc_Textbox2.Text = strSQL;
        Table_SQL1_Literal.Text = strSQL.ToString();
        SubmitList_User_Processing_Label1.Text = "...";
        if (strSQL.Equals("ROGER"))
        {
            SubmitList_User_Processing_Label1.Text = "Run " + List_Seq_Str
            + "; user=" + wk_user + " (" + passed_user_id + " : " + passed_user_email
            + "; email=" + wk_user_email;
            SubmitList_User_Processing_Label1.Visible = true;
        }


        try
        {
            DataTable dt = new DataTable();
            //Add columns  
            dt.Columns.Add(new DataColumn("LIST_SEQ", typeof(int)));
            dt.Columns.Add(new DataColumn("in_line", typeof(int)));
            dt.Columns.Add(new DataColumn("in_type", typeof(string)));
            dt.Columns.Add(new DataColumn("in_text1", typeof(string)));
            dt.Columns.Add(new DataColumn("in_text2", typeof(string)));
            //Add rows  
            string wk_db_id_str = "US:HS";

            string wk_db_id_name = "U.S. Merchandise Trade";
            wk_db_id_str = Database_Type_Dropdownlist.SelectedValue.ToString();
            wk_db_id_name = Database_Type_Dropdownlist.SelectedItem.Text.ToString();
            Line_Num += 1;
            dt.Rows.Add(LIST_NUM, Line_Num, "DATAID", wk_db_id_str, null);
            Line_Num += 1;
            dt.Rows.Add(LIST_NUM, Line_Num, "DATA", wk_db_id_name, null);
            Line_Num += 1;
            dt.Rows.Add(LIST_NUM, Line_Num, "USER_ID", wk_user, null);
            Line_Num += 1;
            dt.Rows.Add(LIST_NUM, Line_Num, "USER_EMAIL", wk_user_email, null);

            DateTime time = DateTime.Now;
            string strtime = time.ToString("yyyy/MM/dd HH:mm:ss");
            Line_Num += 1;
            dt.Rows.Add(LIST_NUM, Line_Num, "DATETIME", strtime, null);


            if (String.IsNullOrEmpty(wk_list_name))
            {
                wk_list_name = wk_db_id_str + " at " + strtime;
            }
            if (wk_list_name.Length > 100)
            {
                wk_list_name = wk_list_name.Substring(1, 99);
            }


            Line_Num += 1;
            dt.Rows.Add(LIST_NUM, Line_Num, "DESCRIPTION", wk_list_name, null);
            Job_Name_TextBox1.Text = wk_db_id_str + " @ " + time.ToString("G");

            if (Flow_imports_ckbox.Checked)
            {
                Line_Num += 1;
                dt.Rows.Add(LIST_NUM, Line_Num, "FLOW", "IMPORTS", null);
            } // END IMPORTS
            else
            {
                Line_Num += 1;
                dt.Rows.Add(LIST_NUM, Line_Num, "FLOW", "EXPORTS", null);

            } // END EXPORTS

            if (wk_parmtype == "CMD")
            {

                bool product_flag = false;
                bool product_share = false;
                bool total_flag = false;
                bool all_products_flag = false;
                string wk_conc = "";
                wk_i = 0;

                if (DataSource_DropDownList1.SelectedIndex < 0)
                    DataSource_DropDownList1.SelectedIndex = 0;
                wk_conc = DataSource_DropDownList1.SelectedValue.ToString().ToUpper();
                if (flag_conc_ck == 1)
                {
                    if (!String.IsNullOrEmpty(conc_ck))
                    {
                        wk_conc = conc_ck.ToString().ToUpper();
                    }
                }
                CONC_INFO.Text = '<' + wk_conc;
                if (String.IsNullOrEmpty(wk_conc))
                    wk_conc = "HS";
                else
                {
                    wk_i = wk_conc.IndexOf("US:");
                    if (wk_i < 0)
                    {
                        wk_i = wk_conc.IndexOf("UN");
                        if (wk_i >= 0)
                        {
                            if (wk_conc.IndexOf("HS") > 0)
                            {
                                wk_conc = "UN:HS";
                            }
                            else if (wk_conc.IndexOf("S1") > 0)
                            {
                                wk_conc = "UN:S1";
                            }
                            else if (wk_conc.IndexOf("S3") > 0)
                            {
                                wk_conc = "UN:S3";
                            }
                            else wk_i = -1;
                        }
                    } /*** end un concordance handling ***/
                    else
                    {
                        if (wk_i < 0)
                            wk_i = wk_conc.IndexOf("TB:");

                        if (wk_i >= 0)
                        {
                            wk_conc = wk_conc.Substring(wk_i + 3);
                        }
                    }
                    if (String.IsNullOrEmpty(wk_conc))
                        wk_conc = "HS";
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "CMD_CONC", wk_conc, null);
                }
                CONC_INFO.Text = CONC_INFO.Text.ToString() + '>' + wk_conc;

                product_flag = false;
                product_share = false;
                total_flag = false;
                all_products_flag = false;
                if (CMD_TOTAL_ALL_CkBox.Checked)
                {
                    all_products_flag = true;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "CMD_ZIN_V", "0", "9");
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "CMD_SHOW", "TOTAL LABEL", null);
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "CMD_OBJ_LABEL", "Total All Merchandise", null);

                }
                if (Display_CMDDETAIL_chkbox.Checked)
                {
                    product_flag = true;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "CMD_SHOW", "FULL_DETAIL", null);

                } // end CMD INDIVIDUAL DETAIL
                if (Display_CMDSUB_chkbox.Checked && !all_products_flag)
                {
                    product_flag = true;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "CMD_SHOW", "ITEMS BELOW", null);

                } // end commodity checks
                product_share = false;
                if (Display_CMDShare_chkbox.Checked && product_flag)
                {
                    product_share = true;
                    total_flag = true;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "CMD_SHOW", "SHARE", null);

                } // end commodity checks

                string wk_digits_str = "4";

                if (DIGITS_ID_Dropdown1.SelectedIndex < 0)
                    DIGITS_ID_Dropdown1.SelectedIndex = 0;
                wk_digits_str = DIGITS_ID_Dropdown1.SelectedValue.ToString();
                Line_Num += 1;
                dt.Rows.Add(LIST_NUM, Line_Num, "CMD_DIGITS", wk_digits_str, null);

                //if (Display_CMDTOTAL_chkbox.Checked || product_flag!=1 || product_share==1)
                if (Display_CMDTOTAL_chkbox.Checked)
                {
                    total_flag = true;
                }
                if (!product_flag)
                {
                    total_flag = true;
                }
                if (total_flag)
                {
                    product_flag = true;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "CMD_SHOW", "TOTAL", null);

                    if (Display_CMD_TotalLabel_chkbox.Checked)
                    {
                        wk_str = CMD_Total_TextBox.Text.ToString();
                        if (!String.IsNullOrEmpty(wk_str))
                        {
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, "CMD_SHOW", "TOTAL LABEL", null);
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, "CMD_OBJ_LABEL", wk_str, null);
                        }

                    } // end total lable check

                } // end commodity total check




                cnt_selected_cmds = 0;
                //num_items=Selections_CMD_TextBox.Lines.Length;

                //    for (int line = 0; line < lineCount; line++)
                //        // GetLineText takes a zero-based line index.
                //        lines.Add(textBox.GetLineText(line));

                //string[] strArr = Selections_CMD_TextBox.Text.Split (new string[]{System.Environment.NewLine}, StringSplitOptions.None );
                string[] strArr = Regex.Split(Selections_CMD_TextBox.Text.Trim(), "\n");
                wk_i = 0;
                num_items = 0;
                if (!all_products_flag)
                {  // begin user product selections
                    num_items = strArr.Length;
                    wk_str = "";
                    wk_str2 = "";
                    if (num_items == 1)
                    {
                        wk_str = strArr[0].ToString();
                        if (string.IsNullOrEmpty(wk_str))
                        {
                            num_items = 0;
                        }
                    }
                    wk_str2 = "num_items=" + num_items.ToString();
                    CMD_SMRY2.Text = "cmd count:" + num_items.ToString();
                    Out_label1.Text = wk_str2 + " str1=" + wk_str;
                    if (num_items < 1)
                    {
                        wk_str = Series_TextBox.Text.ToString().ToUpper();
                        if (String.IsNullOrEmpty(wk_str))
                            num_items = -1;
                        else
                        {
                            Out_label1.Text = wk_str2;
                            cnt_selected_cmds = cnt_selected_cmds + 1;
                            Line_Num += 1;
                            wk_str2 = "";
                            dt.Rows.Add(LIST_NUM, Line_Num, "CMD_ZIN_V", wk_str, wk_str2);
                            wk_str = "";
                            num_items = 0;

                        }
                    } // end no items selected so check Series_TextBox for a value 
                    if (num_items < 0)
                    {

                        wk_i = Product_Detail1_DropDownList2.SelectedIndex;
                        if (wk_i >= 0)
                        {
                            wk_str = Product_Detail1_DropDownList2.SelectedValue;
                            if (string.IsNullOrEmpty(wk_str))
                                wk_i = -1;
                            else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                wk_i = -1;
                            if (wk_i == -1)
                            {
                                wk_str2 = wk_str2 + " no detail 2;";
                            }
                            else wk_str2 = wk_str2 + ": " + wk_str;
                        }
                        if (wk_i == -1)
                        {
                            wk_i = Product_Detail1_DropDownList1.SelectedIndex;
                            if (wk_i >= 0)
                            {
                                wk_str = Product_Detail1_DropDownList1.SelectedValue;
                                if (string.IsNullOrEmpty(wk_str))
                                    wk_i = -1;
                                else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                    wk_i = -1;
                            }
                            if (wk_i == -1)
                            {
                                wk_str2 = wk_str2 + " no detail 1;";
                            }
                            else wk_str2 = wk_str2 + ": " + wk_str;
                        }
                        if (wk_i == -1)
                        {
                            wk_i = Product_DropDownList1.SelectedIndex;
                            if (wk_i < 0)
                            {
                                wk_i = 0;
                                Product_DropDownList1.SelectedIndex = wk_i;
                            }
                            if (wk_i >= 0)
                            {
                                wk_str = Product_DropDownList1.SelectedValue;
                                if (string.IsNullOrEmpty(wk_str))
                                    wk_i = -1;
                                else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                    wk_i = -1;
                            }
                            if (wk_i == -1)
                            {
                                wk_str2 = wk_str2 + " no product selected 1;";
                            }
                            else wk_str2 = wk_str2 + ": " + wk_str;
                        }
                        Out_label1.Text = wk_str2;
                        cnt_selected_cmds = cnt_selected_cmds + 1;
                        Line_Num += 1;
                        wk_str2 = "";
                        dt.Rows.Add(LIST_NUM, Line_Num, "CMD_ZIN_V", wk_str, wk_str2);
                        wk_str = "";


                    }
                    wk_str2 = "";
                    wk_str = "";

                    //for (int i = 0; i < num_items; i++)
                    if (num_items > 0)
                        foreach (string s in strArr)
                        {
                            cnt_loop = cnt_loop + 1;

                            wk_str2 = "";
                            wk_str = "";
                            wk_str = s.ToString().Replace("\n", "").Replace("\r", "");

                            //wk_str=Selected_CMD_ListBox.Items.ToString() ;
                            if (!String.IsNullOrEmpty(wk_str))
                            {

                                if (wk_str.IndexOf("=") == 0)
                                {
                                    cnt_subgroups = cnt_subgroups + 1;
                                }
                                else if (String.Equals(wk_str, ".TOTAL"
                                         , StringComparison.OrdinalIgnoreCase))
                                {
                                    total_all = 1;
                                }
                                else
                                { // commodities
                                    if (wk_str.IndexOf("--") >= 1)
                                    {
                                        wk_str = wk_str.Substring(0, wk_str.IndexOf("--"));
                                    }
                                    else if (wk_str.IndexOf("-") > 1)
                                    {
                                        wk_str2 = wk_str.Substring(wk_str.IndexOf("-") + 1);
                                        wk_str = wk_str.Substring(0, wk_str.IndexOf("-"));
                                    } // END RANGES
                                    else if (wk_str.IndexOf("..") > 1)
                                    {
                                        wk_str2 = wk_str.Substring(wk_str.IndexOf("..") + 2);
                                        wk_str = wk_str.Substring(0, wk_str.IndexOf(".."));
                                    } // END RANGES
                                } // end selected commodities



                                cnt_selected_cmds = cnt_selected_cmds + 1;
                                Line_Num += 1;
                                dt.Rows.Add(LIST_NUM, Line_Num, "CMD_ZIN_V", wk_str, wk_str2);
                            } // string not empty

                        } // end loop over products


                    Out_label1.Text = "num_items " + num_items.ToString()
                      + " cnt_loop" + cnt_loop.ToString()
                      + " subgroup " + cnt_subgroups.ToString()
                      + " total flag " + total_all.ToString();
                } // end not all_products_flag

            } // end wk_parmtype=="CMD" for products

            if (wk_parmtype == "PCTY")
            {
                world_flag = false;
                partner_flag = 0;
                partner_share = 0;
                if (PCTY_TOTAL_ALL_CkBox.Checked)
                {
                    world_flag = true;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_ZIN_V", ".WORLD", null);
                }

                if (Display_PCTYDETAIL_chkbox.Checked)
                {
                    partner_flag = 1;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_SHOW", "FULL_DETAIL", null);

                } // end PCTY INDIVIDUAL DETAIL
                if (Display_PCTYSUB_chkbox.Checked)
                {
                    partner_flag = 1;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_SHOW", "ITEMS BELOW", null);

                } // end partner checks
                if (Display_PCTYShare_chkbox.Checked && partner_flag == 1)
                {
                    partner_flag = 1;
                    partner_share = 1;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_SHOW", "SHARE", null);

                } // end commodity checks

                if (Display_PCTYTOTAL_chkbox.Checked || partner_flag != 1 || partner_share == 1)
                {
                    partner_flag = 1;
                    Line_Num += 1;
                    dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_SHOW", "TOTAL", null);

                    if (Display_PCTY_TotalLabel_chkbox.Checked || partner_flag != 1)
                    {
                        wk_str = PCTY_Total_TextBox.Text.ToString();
                        if (!String.IsNullOrEmpty(wk_str))
                        {
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_SHOW", "TOTAL LABEL", null);
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_OBJ_LABEL", wk_str, null);
                        }

                    } // end total lable check

                } // end partner total check

                wk_str2 = "";
                int cnt_selected_pctys = 0;
                cnt_subgroups = 0;
                total_all = 0;

                //string[] pctyArr = Selections_TextBox.Text.Split (new string[]{System.Environment.NewLine}, StringSplitOptions.None );
                if (!PCTY_TOTAL_ALL_CkBox.Checked)
                {

                    string[] pctyArr = Regex.Split(Selections_TextBox.Text.Trim(), "\n");
                    num_items = pctyArr.Length;
                    CTY_SMRY2.Text = "country count:" + num_items.ToString();
                    cnt_loop = 0;
                    wk_i = 0;
                    num_items = pctyArr.Length;
                    wk_str = "";
                    wk_str2 = "";
                    if (num_items == 1)
                    {
                        wk_str = pctyArr[0].ToString();
                        if (string.IsNullOrEmpty(wk_str))
                        {
                            num_items = 0;
                        }
                    }
                    wk_str2 = "num_items=" + num_items.ToString();
                    Out_label_pcty1.Text = wk_str2 + " str1=" + wk_str;
                    if (num_items < 1)
                    {
                        wk_i = CountryGroup_DropDownList.SelectedIndex;
                        if (wk_i < 0) CountryGroup_DropDownList.SelectedIndex = 0;
                        if (wk_i >= 0)
                        {
                            wk_str = CountryGroup_DropDownList.SelectedValue.ToString();
                            if (wk_str.IndexOf("401") >= 0)
                                wk_str = ".WORLD";
                            if (string.IsNullOrEmpty(wk_str))
                                wk_i = -1;
                            else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                wk_i = -1;
                            if (wk_i == -1)
                            {
                                wk_str = ".WORLD";
                                wk_str2 = wk_str2 + " cty group selected; world assumed " + wk_str;
                            }
                            else wk_str2 = wk_str2 + ": " + wk_str;
                        }
                        Out_label1.Text = wk_str2;
                        cnt_selected_cmds = cnt_selected_cmds + 1;
                        Line_Num += 1;
                        wk_str2 = "";
                        dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_ZIN_V", wk_str, wk_str2);
                        wk_str = "";
                    }
                    wk_str2 = "";
                    wk_str = "";

                    //for (int wk_i = 0; wk_i < num_items; wk_i++)
                    if (num_items > 0)
                    {
                        foreach (string s2 in pctyArr)
                        {
                            cnt_loop = cnt_loop + 1;

                            wk_str2 = "";
                            wk_str = "";
                            wk_str = s2.ToString();
                            //wk_str=Selected_ListBox.Items.ToString() ;
                            if (!String.IsNullOrEmpty(wk_str))
                            {

                                if (wk_str.IndexOf("=") == 0 || wk_str.IndexOf(".") == 0)
                                {
                                    cnt_subgroups = cnt_subgroups + 1;
                                }
                                else if (String.Equals(wk_str, ".WORLD"
                                          , StringComparison.OrdinalIgnoreCase))
                                {
                                    total_all = 1;
                                }

                                cnt_selected_pctys = cnt_selected_pctys + 1;
                                Line_Num += 1;
                                dt.Rows.Add(LIST_NUM, Line_Num, "PCTY_ZIN_V", wk_str, null);
                            } // string not empty


                        }  // end loop writing pctys selections

                    } // end num_items >0
                    Out_label_pcty1.Text = "num_items " + num_items.ToString()
                      + " cnt_loop" + cnt_loop.ToString()
                      + " subgroup " + cnt_subgroups.ToString()
                      + " total flag " + total_all.ToString();
                } // end partner country selections (NOT .WORLD)
            }// end wk_parmtype=="P" for partners


            if (wk_parmtype == "RCTY")
            { // begin wk_parmtype=="RCTY" for Reporters


                //*****************************************************
                //*** REPORTER COUNTRY PROCESSING 
                //*****************************************************

                int reporter_flag = 0;
                int reporter_share = 0;
                world_flag = false;
                reporter_flag = 0;
                reporter_share = 0;
                if (Reporter_Panel.Visible)
                {
                    if (RCTY_TOTAL_ALL_CkBox.Checked)
                    {
                        world_flag = true;
                        Line_Num += 1;
                        dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_ZIN_V", ".WORLD", null);
                    }

                    if (Display_RCTYDETAIL_chkbox.Checked)
                    {
                        reporter_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_SHOW", "FULL_DETAIL", null);

                    } // end RCTY INDIVIDUAL DETAIL
                    if (Display_RCTYSUB_chkbox.Checked)
                    {
                        reporter_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_SHOW", "ITEMS BELOW", null);

                    } // end reporter checks
                    if (Display_RCTYShare_chkbox.Checked && reporter_flag == 1)
                    {
                        reporter_flag = 1;
                        reporter_share = 1;
                        Line_Num += 1;
                        dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_SHOW", "SHARE", null);

                    } // end commodity checks

                    if (Display_RCTYTOTAL_chkbox.Checked || reporter_flag != 1 || reporter_share == 1)
                    {
                        reporter_flag = 1;
                        Line_Num += 1;
                        dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_SHOW", "TOTAL", null);

                        if (Display_RCTY_TotalLabel_chkbox.Checked || reporter_flag != 1)
                        {
                            wk_str = RCTY_Total_TextBox.Text.ToString();
                            if (!String.IsNullOrEmpty(wk_str))
                            {
                                Line_Num += 1;
                                dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_SHOW", "TOTAL LABEL", null);
                                Line_Num += 1;
                                dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_OBJ_LABEL", wk_str, null);
                            }

                        } // end total lable check

                    } // end reporter total check

                    wk_str2 = "";
                    int cnt_selected_rctys = 0;
                    cnt_subgroups = 0;
                    total_all = 0;

                    //string[] rctyArr = Selections_TextBox.Text.Split (new string[]{System.Environment.NewLine}, StringSplitOptions.None );
                    if (!RCTY_TOTAL_ALL_CkBox.Checked)
                    {

                        string[] rctyArr = Regex.Split(RCTY_Selections_TextBox.Text.Trim(), "\n");
                        num_items = rctyArr.Length;
                        CTY_SMRY2.Text = "country count:" + num_items.ToString();
                        cnt_loop = 0;
                        wk_i = 0;
                        num_items = rctyArr.Length;
                        wk_str = "";
                        wk_str2 = "";
                        if (num_items == 1)
                        {
                            wk_str = rctyArr[0].ToString();
                            if (string.IsNullOrEmpty(wk_str))
                            {
                                num_items = 0;
                            }
                        }
                        wk_str2 = "num_items=" + num_items.ToString();
                        Out_label_RCTY1.Text = wk_str2 + " str1=" + wk_str;
                        if (num_items < 1)
                        {
                            wk_i = RCTY_CountryGroup_DropDownList.SelectedIndex;
                            if (wk_i < 0) RCTY_CountryGroup_DropDownList.SelectedIndex = 0;
                            if (wk_i >= 0)
                            {
                                wk_str = RCTY_CountryGroup_DropDownList.SelectedValue.ToString();
                                if (wk_str.IndexOf("401") >= 0)
                                    wk_str = ".WORLD";
                                if (string.IsNullOrEmpty(wk_str))
                                    wk_i = -1;
                                else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                    wk_i = -1;
                                if (wk_i == -1)
                                {
                                    wk_str = ".WORLD";
                                    wk_str2 = wk_str2 + " cty group selected; world assumed " + wk_str;
                                }
                                else wk_str2 = wk_str2 + ": " + wk_str;
                            }
                            Out_label1.Text = wk_str2;
                            cnt_selected_cmds = cnt_selected_cmds + 1;
                            Line_Num += 1;
                            wk_str2 = "";
                            dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_ZIN_V", wk_str, wk_str2);
                            wk_str = "";
                        }
                        wk_str2 = "";
                        wk_str = "";

                        //for (int i = 0; i < num_items; i++)
                        if (num_items > 0)
                        {
                            foreach (string s2 in rctyArr)
                            {
                                cnt_loop = cnt_loop + 1;

                                wk_str2 = "";
                                wk_str = "";
                                wk_str = s2.ToString();
                                //wk_str=RCTY_Selected_ListBox.Items.ToString() ;
                                if (!String.IsNullOrEmpty(wk_str))
                                {

                                    if (wk_str.IndexOf("=") == 0 || wk_str.IndexOf(".") == 0)
                                    {
                                        cnt_subgroups = cnt_subgroups + 1;
                                    }
                                    else if (String.Equals(wk_str, ".WORLD"
                                              , StringComparison.OrdinalIgnoreCase))
                                    {
                                        total_all = 1;
                                    }

                                    cnt_selected_rctys = cnt_selected_rctys + 1;
                                    Line_Num += 1;
                                    dt.Rows.Add(LIST_NUM, Line_Num, "RCTY_ZIN_V", wk_str, null);
                                } // string not empty


                            }  // end loop writing rctys selections

                        } // end num_items >0
                        Out_label_RCTY1.Text = "num_items " + num_items.ToString()
                          + " cnt_loop" + cnt_loop.ToString()
                          + " subgroup " + cnt_subgroups.ToString()
                          + " total flag " + total_all.ToString();
                    } // end reporter country selections (NOT .WORLD)

                    //*********************************************
                    //*********** END REPORTER COUNTRY PROCESSING 
                    //*********************************************
                } // end reporter panel visible
            }// end wk_parmtype=="RCTY" for Reporters

            if (wk_parmtype == "STATE")
            {

                state_SMRY.Text = "State: False";
                wk_region_code = "STATE";
                wk_out_zin_v = "STATE_ZIN_V";
                wk_out_show = "STATE_SHOW";
                wk_out_obj = "STATE_OBJ_LABEL";
                if (State_Panel.Visible && wk_cur_db.IndexOf("USST") >= 0)
                {
                    show_states = true;
                    STATE_Out_label1.Text = Data_ID + " State:: True";
                    wk_region_code = "STATE";
                    wk_out_zin_v = "STATE_ZIN_V";
                    wk_out_show = "STATE_SHOW";
                    wk_out_obj = "STATE_OBJ_LABEL";
                }
                else if (State_Panel.Visible && wk_cur_db.IndexOf("US") >= 0)
                {
                    show_states = true;
                    STATE_Out_label1.Text = Data_ID + " District:: True";
                    wk_region_code = "DIST";
                    wk_out_zin_v = "DIST_ZIN_V";
                    wk_out_show = "DIST_SHOW";
                    wk_out_obj = "DIST_OBJ_LABEL";
                    if (Display_STATEDETAIL_chkbox.Checked)
                    {
                        district_flag = 1;
                    }
                    else if (!STATE_TOTAL_ALL_CkBox.Checked)
                    {
                        district_flag = 1;
                    }
                    else
                    {
                        district_flag = -1;
                        show_states = false;
                    }

                }

                else
                {
                    STATE_Out_label1.Text = Data_ID + " " + wk_region_code + ":: False";
                    show_states = false;
                }
                STATE_Out_label1.DataBind();
                state_flag = 0;
                state_share = 0;

                if (show_states)
                {
                    STATE_Out_label1.Text = Data_ID + " " + wk_region_code + " b: True";

                    if (STATE_TOTAL_ALL_CkBox.Checked)
                    {
                        STATE_Out_label1.Text = Data_ID + " " + wk_region_code + " c: total all";
                        usa_flag = true;
                        state_share = 0;

                        if (Display_STATEShare_chkbox.Checked)
                        {
                            state_flag = 1;
                            state_share = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "SHARE", null);

                        } // end state share check
                        Line_Num += 1;

                        dt.Rows.Add(LIST_NUM, Line_Num, wk_out_zin_v, ".USA", null);
                        Line_Num += 1;
                        dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "TOTAL LABEL", null);
                        Line_Num += 1;
                        if (district_flag == 1)
                        {
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_obj, "USA (All Districts)", null);
                        }
                        else
                        {
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_obj, "USA (All States)", null);
                        }
                        if (Display_STATEShare_chkbox.Checked || Display_STATEDETAIL_chkbox.Checked)
                        {
                            state_flag = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "FULL_DETAIL", null);

                        } // end STATE INDIVIDUAL DETAIL
                        if (Display_STATEShare_chkbox.Checked
                            || Display_STATETOTAL_chkbox.Checked
                            || !Display_STATEDETAIL_chkbox.Checked)
                        {
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "TOTAL", null);
                        }
                    }
                    else
                    {

                        STATE_Out_label1.Text = Data_ID + " State d: not all button checked";

                        if (Display_STATEDETAIL_chkbox.Checked)
                        {
                            STATE_Out_label1.Text = Data_ID + " State e: detail checked";
                            state_flag = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "FULL_DETAIL", null);

                        } // end STATE INDIVIDUAL DETAIL
                        if (Display_STATESUB_chkbox.Checked)
                        {
                            state_flag = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "ITEMS BELOW", null);

                        } // end state checks
                        if (Display_STATEShare_chkbox.Checked && state_flag == 1)
                        {
                            state_flag = 1;
                            state_share = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "SHARE", null);

                        } // end commodity checks

                        if (Display_STATETOTAL_chkbox.Checked || state_flag != 1 || state_share == 1)
                        {
                            state_flag = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "TOTAL", null);

                            if (Display_STATE_TotalLabel_chkbox.Checked || state_flag != 1)
                            {
                                wk_str = STATE_Total_TextBox.Text.ToString();
                                if (!String.IsNullOrEmpty(wk_str))
                                {
                                    Line_Num += 1;
                                    dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "TOTAL LABEL", null);
                                    Line_Num += 1;
                                    dt.Rows.Add(LIST_NUM, Line_Num, wk_out_obj, wk_str, null);
                                }

                            } // end total lable check

                        } // end state total check




                    } // end not total all states checked


                    if (!STATE_TOTAL_ALL_CkBox.Checked)
                    {
                        STATE_Out_label1.Text = state_SMRY.Text.ToString() + "; selections ";

                        string[] stateArr = Regex.Split(States_Selections_TextBox.Text.Trim(), "\n");
                        num_items = stateArr.Length;
                        //CTY_SMRY2.Text="country count:" +num_items.ToString() ;
                        cnt_loop = 0;
                        wk_i = 0;
                        num_items = stateArr.Length;
                        wk_str = "";
                        wk_str2 = "";
                        if (num_items == 1)
                        {
                            wk_str = stateArr[0].ToString();
                            if (string.IsNullOrEmpty(wk_str))
                            {
                                num_items = 0;
                            }
                        }
                        wk_str2 = "num_items=" + num_items.ToString();
                        Out_label_state1.Text = wk_str2 + " str1=" + wk_str;
                        if (num_items < 1 && 1 == 2)
                        {
                            wk_i = CountryGroup_DropDownList.SelectedIndex;
                            if (wk_i < 0) CountryGroup_DropDownList.SelectedIndex = 0;
                            if (wk_i >= 0)
                            {
                                wk_str = CountryGroup_DropDownList.SelectedValue.ToString();
                                if (wk_str.IndexOf("401") >= 0)
                                    wk_str = ".WORLD";
                                if (string.IsNullOrEmpty(wk_str))
                                    wk_i = -1;
                                else if (wk_str.Equals("NONE", StringComparison.InvariantCultureIgnoreCase))
                                    wk_i = -1;
                                if (wk_i == -1)
                                {
                                    wk_str = ".WORLD";
                                    wk_str2 = wk_str2 + " cty group selected; world assumed " + wk_str;
                                }
                                else wk_str2 = wk_str2 + ": " + wk_str;
                            }
                            Out_label1.Text = wk_str2;
                            cnt_selected_cmds = cnt_selected_cmds + 1;
                            Line_Num += 1;
                            wk_str2 = "";
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_zin_v, wk_str, wk_str2);
                            wk_str = "";
                        }
                        wk_str2 = "";
                        wk_str = "";

                        //for (int i = 0; i < num_items; i++)
                        if (num_items > 0)
                        {
                            foreach (string s2 in stateArr)
                            {
                                cnt_loop = cnt_loop + 1;

                                wk_str2 = "";
                                wk_str = "";
                                wk_str = s2.ToString();
                                i_len = wk_str.IndexOf(" - ");

                                if (i_len > 1)
                                {
                                    wk_str = wk_str.Substring(0, i_len);
                                }
                                //wk_str=Selected_ListBox.Items.ToString() ;
                                if (!String.IsNullOrEmpty(wk_str))
                                {

                                    if (wk_str.IndexOf("=") == 0 || wk_str.IndexOf(".") == 0)
                                    {
                                        cnt_subgroups = cnt_subgroups + 1;
                                    }
                                    else if (String.Equals(wk_str, ".WORLD"
                                              , StringComparison.OrdinalIgnoreCase))
                                    {
                                        total_all = 1;
                                    }

                                    cnt_selected_states = cnt_selected_states + 1;
                                    Line_Num += 1;
                                    dt.Rows.Add(LIST_NUM, Line_Num, wk_out_zin_v, wk_str, null);
                                } // string not empty


                            } // end loop writing states selections
                        } // end num_items >0
                        Out_label_state1.Text = "num_items " + num_items.ToString()
                          + " cnt_loop" + cnt_loop.ToString()
                          + " subgroup " + cnt_subgroups.ToString()
                          + " total flag " + total_all.ToString();
                    } // end state selections (NOT .ALL or .USA)
                } // end if show_state = true
            }// end wk_parmtype=="STATE" for STATES/DISTRICTS

            if (wk_parmtype == "SPI")
            {

                // spis start

                i_len = 0;
                int cnt_selected_spis = 0;
                cnt_subgroups = 0;
                total_all = 0;
                int spi_share = 0;
                int spi_flag = 0;
                bool allspi_flag = false;
                bool show_spis = false;
                wk_cur_db = Current_DBID_Label1.Text.ToString().ToUpper();
                wk_out_zin_v = "SPI_ZIN_V";
                wk_out_show = "SPI_SHOW";
                wk_out_obj = "SPI_OBJ_LABEL";
                show_spis = false;
                if (SPI_Panel.Visible && wk_cur_db.IndexOf("US") >= 0)
                {
                    if (wk_cur_db.IndexOf("USST") >= 0)
                    {
                        show_spis = false;
                    }
                    else
                    {
                        show_spis = true;
                    }
                }

                if (show_spis)
                {

                    if (SPI_TOTAL_ALL_CkBox.Checked)
                    {
                        usa_flag = true;
                        spi_share = 0;

                        if (Display_SPIShare_chkbox.Checked)
                        {
                            spi_share = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "SHARE", null);

                        } // end spi share check
                        Line_Num += 1;

                        dt.Rows.Add(LIST_NUM, Line_Num, wk_out_zin_v, ".ALL", null);
                        Line_Num += 1;
                        dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "TOTAL LABEL", null);
                        Line_Num += 1;
                        dt.Rows.Add(LIST_NUM, Line_Num, wk_out_obj, "All SPIs", null);
                        if (Display_SPIShare_chkbox.Checked || Display_SPIDETAIL_chkbox.Checked)
                        {
                            spi_flag = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "FULL_DETAIL", null);

                        } // end SPI INDIVIDUAL DETAIL
                        if (Display_SPIShare_chkbox.Checked
                            || Display_SPITOTAL_chkbox.Checked
                            || !Display_SPIDETAIL_chkbox.Checked)
                        {
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "TOTAL", null);
                        }
                    }
                    else
                    {

                        STATE_Out_label1.Text = Data_ID + " State d: not all button checked";

                        if (Display_SPIDETAIL_chkbox.Checked)
                        {
                            STATE_Out_label1.Text = Data_ID + " State e: detail checked";
                            spi_flag = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "FULL_DETAIL", null);

                        } // end SPI INDIVIDUAL DETAIL
                        if (Display_SPISUB_chkbox.Checked)
                        {
                            spi_flag = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "ITEMS BELOW", null);

                        } // end spi checks
                        if (Display_SPIShare_chkbox.Checked && partner_flag == 1)
                        {
                            spi_flag = 1;
                            spi_share = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "SHARE", null);

                        } // end commodity checks

                        if (Display_SPITOTAL_chkbox.Checked || spi_flag != 1 || spi_share == 1)
                        {
                            spi_flag = 1;
                            Line_Num += 1;
                            dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "TOTAL", null);

                            if (Display_SPI_TotalLabel_chkbox.Checked || spi_flag != 1)
                            {
                                wk_str = SPI_Total_TextBox.Text.ToString();
                                if (!String.IsNullOrEmpty(wk_str))
                                {
                                    Line_Num += 1;
                                    dt.Rows.Add(LIST_NUM, Line_Num, wk_out_show, "TOTAL LABEL", null);
                                    Line_Num += 1;
                                    dt.Rows.Add(LIST_NUM, Line_Num, wk_out_obj, wk_str, null);
                                }

                            } // end total lable check

                        } // end spi total check




                    } // end not total all spis checked


                    if (!SPI_TOTAL_ALL_CkBox.Checked)
                    {
                        string[] spiArr = Regex.Split(SPIs_Selections_TextBox.Text.Trim(), "\n");
                        num_items = spiArr.Length;
                        //SPI_SMRY2.Text="spi count:" +num_items.ToString() ;
                        cnt_loop = 0;
                        wk_i = 0;
                        num_items = spiArr.Length;
                        wk_str = "";
                        wk_str2 = "";
                        if (num_items == 1)
                        {
                            wk_str = spiArr[0].ToString();
                            if (string.IsNullOrEmpty(wk_str))
                            {
                                num_items = 0;
                            }
                        }
                        wk_str2 = "num_items=" + num_items.ToString();
                        Out_label_spi1.Text = wk_str2 + " str1=" + wk_str;
                        wk_str2 = "";
                        wk_str = "";

                        //for (int i = 0; i < num_items; i++)
                        if (num_items > 0)
                        {
                            foreach (string s2 in spiArr)
                            {
                                cnt_loop = cnt_loop + 1;

                                wk_str2 = "";
                                wk_str = "";
                                wk_str = s2.ToString();
                                i_len = wk_str.IndexOf(" - ");

                                if (i_len > 0 && i_len < 5)
                                {
                                    wk_str = wk_str.Substring(0, i_len);
                                }
                                //wk_str=Selected_ListBox.Items.ToString() ;
                                if (!String.IsNullOrEmpty(wk_str))
                                {

                                    if (wk_str.IndexOf("=") == 0 || wk_str.IndexOf(".") == 0)
                                    {
                                        cnt_subgroups = cnt_subgroups + 1;
                                    }
                                    else if (String.Equals(wk_str, ".WORLD"
                                              , StringComparison.OrdinalIgnoreCase))
                                    {
                                        total_all = 1;
                                    }

                                    cnt_selected_spis = cnt_selected_spis + 1;
                                    Line_Num += 1;
                                    dt.Rows.Add(LIST_NUM, Line_Num, wk_out_zin_v, wk_str, null);
                                } // string not empty


                            } // end loop writing spis selections
                        } // end num_items >0
                        Out_label_spi1.Text = "num_items " + num_items.ToString()
                          + " cnt_loop" + cnt_loop.ToString()
                          + " subgroup " + cnt_subgroups.ToString()
                          + " total flag " + total_all.ToString();
                    } // end spi selections (NOT .ALL )
                } // end if show_spi = true

                // spis end 

            }// end wk_parmtype=="STATE" for STATES/DISTRICTS

            //sqlcon as SqlConnection  
            if (wk_parmtype == "ZZZ")
            {
                Job_Data_GridView.Visible = true;
                Job_Data_GridView.DataSource = dt;
                Job_Data_GridView.DataBind();
            }
            int resave_flag = 0;
            if (Resave_CMD_Load_CkBox.Checked)
            {
                resave_flag = 1;
            }
            SqlCommand sqlcom = new SqlCommand("dbo.spt_tpis_save_user_lists", sqlconn);
            sqlcom.CommandType = CommandType.StoredProcedure;
            sqlcom.Parameters.AddWithValue("@insertjobdata", dt);
            sqlcom.Parameters.AddWithValue("@JOBNUM", LIST_NUM);
            sqlcom.Parameters.AddWithValue("@LISTTYPE", wk_parmtype);
            sqlcom.Parameters.AddWithValue("@P_REPLACE", resave_flag);

            sqlconn.Open();
            sqlcom.ExecuteNonQuery();
            sqlconn.Close();
            if (wk_parmtype.Equals("CMD"))
            {
                Save_CMDs_Status_Label.Text = "Commodities Saved as " + wk_list_name;
                Load_User_CMDList_DropDownList1.DataBind();
                Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Black;
                Save_CMDs_Status_Label.DataBind();
                CMD_Ajax_Panel1.Update();
            }
            else if (wk_parmtype.Equals("PCTY"))
            {
                Save_CMDs_Status_Label.Text = "Partner Countries Saved as " + wk_list_name;
                Load_User_PCTYList_DropDownList1.DataBind();
                Save_PCTYs_Status_Label.ForeColor = System.Drawing.Color.Black;
                Save_PCTYs_Status_Label.DataBind();
                PCTY_Ajax_Panel1.Update();
            }
            else if (wk_parmtype.Equals("PCTY"))
            {
                Save_CMDs_Status_Label.Text = "Reporter Countries Saved as " + wk_list_name;
                Load_User_RCTYList_DropDownList1.DataBind();
                Save_RCTYs_Status_Label.ForeColor = System.Drawing.Color.Black;
                Save_RCTYs_Status_Label.DataBind();
                Reporter_Update_Panel.Update();
            }
            else if (wk_parmtype.Equals("STATE"))
            {
                Save_CMDs_Status_Label.Text = "States Saved as " + wk_list_name;
                Load_User_STATEList_DropDownList1.DataBind();
                Save_STATEs_Status_Label.ForeColor = System.Drawing.Color.Black;
                Save_STATEs_Status_Label.DataBind();
                STATE_Ajax_Panel1.Update();
            }
            else if (wk_parmtype.Equals("DIST"))
            {
                Save_CMDs_Status_Label.Text = "Districts Saved as " + wk_list_name;
                Load_User_STATEList_DropDownList1.DataBind();
                Save_STATEs_Status_Label.ForeColor = System.Drawing.Color.Black;
                Save_STATEs_Status_Label.DataBind();
                STATE_Ajax_Panel1.Update();
            }

            //Save_CMDs_Status_Label.Text = "Commodities Saved";
            //Load_User_CMDList_DropDownList1.DataBind();
            //CMD_Ajax_Panel1.Update();
            // return "Hello" ;
        } // end try
        catch (Exception ex)
        {
            if (wk_user.ToUpper() == "POMEROR" || wk_user.ToUpper() == "TPISTEST")
            {
                Table_Fmt1_Literal.Text = strSQL + " loading error " + ex.Message;
                Table_Fmt1_Literal.Visible = true;
                //Show_Table_Panel.Visible=true ;                       
                //Show_Table_Panel.Style["display"]="block" ;
                //flag_table_errors=1 ;
            }
            else
            {
                Table_Fmt1_Literal.Text = "ERROR SAVING FILE-TRY REFRESHING PAGE OR SELECTING OTHER INFORMATION";

            }
            Save_CMDs_Status_Label.Text = "Commodities SAVE ERROR";
            sqlconn.Close();
        } // end catch


    }  // End Save_User_List

    protected void Delete_Saved_User_List(string p_prmtype)
    {
        // p_resave
        SqlConnection Conn2;
        int error_flag = 0; // save a new list
        string List_Seq_Str = "";
        string List_Name = "";
        string connString;
        string strSQL = "";
        string wk_parmtype = p_prmtype.ToUpper();
        string wk_user = "POMEROR";
        string wk_user_email = "ROGER.POMEROY@TRADE.GOV";
        int Line_Num = 0;
        int LIST_NUM = 199001;
        int misc_flag = 0;
        string wk_str = "";
        string wk_str2 = "";
        string list_type_id = "";
        string wk_listseq_type_str = "7";
        int wk_listseq_type_num = 7000000;
        int test_print = 0;

        SqlConnection sqlconn = new SqlConnection();
        int wk_i = 0;
        int i_len = 0;
        Save_CMDs_Status_Label.Text = "Starting USER LIST DELETE ";
        Save_CMDs_Status_Label.DataBind();

        List_Seq_Str = Load_User_CMDList_DropDownList1.SelectedValue.ToString();

        List_Name = Load_User_CMDList_DropDownList1.SelectedItem.ToString();

        wk_parmtype = p_prmtype.ToUpper().Trim();
        Save_CMDs_Status_Label.Visible = true;
        Save_CMDs_Status_Label.Text = "Starting USER LIST DELETE for " + wk_parmtype + " " + List_Name;
        Save_CMDs_Status_Label.DataBind();
        if (test_print > 0)
        {
            Out_label1.Text = "List_Name deleting.";
            Out_label1.Visible = true;
            Out_label1.DataBind();

        }
        // return ;
        try
        {
            if (wk_parmtype.Equals("CMD"))
            {
                wk_listseq_type_str = "1";
                wk_listseq_type_num = 1000000;
                list_type_id = "COMMODITY";
            }
            else if (wk_parmtype.Equals("CTY") || wk_parmtype.Equals("PCTY") || wk_parmtype.Equals("RCTY"))
            {
                wk_listseq_type_str = "2";
                wk_listseq_type_num = 2000000;
                list_type_id = "COUNTRY";
            }
            else if (wk_parmtype.Equals("STATE"))
            {
                wk_listseq_type_str = "6";
                wk_listseq_type_num = 6000000;
                list_type_id = "STATE";
            }
            else if (wk_parmtype.Equals("DISTRICT"))
            {
                wk_listseq_type_str = "3";
                wk_listseq_type_num = 3000000;
                list_type_id = "DISTRICT";
            }
            else if (wk_parmtype.Equals("SPI"))
            {
                wk_listseq_type_str = "5";
                wk_listseq_type_num = 5000000;
                list_type_id = "SPI";
            }
            else if (wk_parmtype.Equals("SERIES"))
            {
                wk_listseq_type_str = "7";
                wk_listseq_type_num = 7000000;
                list_type_id = "SERIES";
            }

            if (test_print > 0)
            {

                Out_label1.Text += " type" + list_type_id;
                Out_label1.DataBind();
            }





            //"UID=TPISGUI;Password=Log$me#in2020;" +


            sqlconn.ConnectionString =
            "Data Source=ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
            "Initial Catalog=US_DATA;" +
            "UID=tpisgui;Password=Log$mein2021;" +
            "Integrated Security=False;";

            wk_user = User_ID_Valid_Label1.Text.ToString();
            wk_user_email = User_ID_Email_Label1.Text.ToString().ToUpper();


            if (!Int32.TryParse(List_Seq_Str, out LIST_NUM))
            {
                List_Seq_Str = "-999999";
                LIST_NUM = -999999;
                User_ListSeq_Label1.Text = List_Seq_Str;

                Save_CMDs_Status_Label.Text = "Illegal List Number=" + List_Seq_Str + "; list name=" + List_Name;
                error_flag = 1;

            }
            else
            {
                Save_CMDs_Status_Label.Text = "Deleting List Number=" + List_Seq_Str + "; list name=" + List_Name;

            }

            if (test_print > 0)
            {
                Out_label1.Text += " uid " + wk_user;
                Out_label1.DataBind();
            }
            Save_CMDs_Status_Label.DataBind();
            //return ;
        }
        catch (Exception extsv)
        {
            Save_CMDs_Status_Label.Text = "ERRROR Setting Up Delete: " + extsv.Message.ToString();
            Save_CMDs_Status_Label.DataBind();
            error_flag = 1;

        }

        if (test_print > 0)
        {

            Out_label1.Text += " eflag " + error_flag.ToString();
            Out_label1.DataBind();
        }

        if (error_flag == 0)
            try
            {
                string wk_db_id_str = "US:HS";
                string wk_db_id_name = "U.S. Merchandise Trade";
                wk_db_id_str = Database_Type_Dropdownlist.SelectedValue.ToString();
                wk_db_id_name = Database_Type_Dropdownlist.SelectedItem.Text.ToString();
                strSQL = "EXEC dbo.spt_tpis_delete_user_lists_new ";


                SqlCommand sqlcom = new SqlCommand("dbo.spt_tpis_delete_user_lists_new", sqlconn);
                sqlcom.CommandType = CommandType.StoredProcedure;
                sqlcom.Parameters.AddWithValue("@LIST_NUM", LIST_NUM);
                sqlcom.Parameters.AddWithValue("@P_LISTTYPE", wk_parmtype);
                sqlcom.Parameters.AddWithValue("@P_USERID", wk_user);
                sqlconn.Open();
                sqlcom.ExecuteNonQuery();
                sqlconn.Close();
                if (wk_parmtype.Equals("CMD"))
                {
                    Save_CMDs_Status_Label.Text = "LIST " + List_Seq_Str + "(" + LIST_NUM.ToString()
                    + ") DELETED uid=" + wk_user + " " + List_Name;
                    Load_User_CMDList_DropDownList1.DataBind();
                    Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Black;
                    Save_CMDs_Status_Label.DataBind();
                    CMD_Ajax_Panel1.Update();
                }
                else if (wk_parmtype.Equals("PCTY"))
                {
                    Save_PCTYs_Status_Label.Text = "LIST " + List_Seq_Str + "(" + LIST_NUM.ToString()
                    + ") DELETED uid=" + wk_user + " " + List_Name;
                    Load_User_PCTYList_DropDownList1.DataBind();
                    Save_PCTYs_Status_Label.ForeColor = System.Drawing.Color.Black;
                    Save_PCTYs_Status_Label.DataBind();
                    PCTY_Ajax_Panel1.Update();
                }

                // return "Hello" ;
            } // end try
            catch (Exception ex)
            {
                if (wk_user.ToUpper() == "POMEROR" || wk_user.ToUpper() == "TPISTEST")
                {
                    Table_Fmt1_Literal.Text = strSQL + " delete list error " + ex.Message;
                    Table_Fmt1_Literal.Visible = true;
                    //Show_Table_Panel.Visible=true ;                       
                    //Show_Table_Panel.Style["display"]="block" ;
                    //flag_table_errors=1 ;
                }
                else
                {
                    Table_Fmt1_Literal.Text = "ERROR DELETING LIST-TRY REFRESHING PAGE OR SELECTING OTHER INFORMATION";

                }
                if (wk_parmtype.Equals("CMD"))
                {
                    Save_CMDs_Status_Label.ForeColor = System.Drawing.Color.Red;
                    Save_CMDs_Status_Label.Text = "ERROR FOR DELETE LIST" + ex.Message;
                    Save_CMDs_Status_Label.DataBind();
                    CMD_Ajax_Panel1.Update();
                }
                else if (wk_parmtype.Equals("PCTY"))
                {
                    Save_PCTYs_Status_Label.Text = "ERROR FOR DELETE LIST" + ex.Message;
                    Save_PCTYs_Status_Label.ForeColor = System.Drawing.Color.Black;
                    Save_PCTYs_Status_Label.DataBind();
                    PCTY_Ajax_Panel1.Update();
                }

                error_flag = 1;
                sqlconn.Close();
            } // end catch


    }  // End Delete_Saved_User_List

    protected void Load_Saved_RCTYs_Click(object sender, EventArgs e)
    {
        string List_Seq_Str = Load_User_RCTYList_DropDownList1.SelectedValue.ToString();
        string List_Name_Str = Load_User_RCTYList_DropDownList1.SelectedItem.Text.ToString();
        Load_Saved_Parms("RCTY", List_Seq_Str, List_Name_Str);

        //string List_Seq_Str = Load_User_RCTYList_DropDownList1.SelectedValue.ToString();
        //string List_Name_Str = Load_User_RCTYList_DropDownList1.SelectedItem.Text.ToString();

        //Load_Saved_Parms("RCTY",List_Seq_Str, List_Name_Str);
    }
    protected void Load_Saved_PCTYs_Click(object sender, EventArgs e)
    {
        string List_Seq_Str = Load_User_PCTYList_DropDownList1.SelectedValue.ToString();
        string List_Name_Str = Load_User_PCTYList_DropDownList1.SelectedItem.Text.ToString();

        Load_Saved_Parms("PCTY", List_Seq_Str, List_Name_Str);
    }
    protected void Load_Saved_STATEs_Click(object sender, EventArgs e)
    {
        string List_Seq_Str = Load_User_STATEList_DropDownList1.SelectedValue.ToString();
        string List_Name_Str = Load_User_STATEList_DropDownList1.SelectedItem.Text.ToString();

        Load_Saved_Parms("STATE", List_Seq_Str, List_Name_Str);

        //string List_Seq_Str = Load_User_STATEList_DropDownList1.SelectedValue.ToString();
        //string List_Name_Str = Load_User_STATEList_DropDownList1.SelectedItem.Text.ToString();

        //Load_Saved_Parms("STATE",List_Seq_Str, List_Name_Str);
    }
    protected void Load_Saved_SPIs_Click(object sender, EventArgs e)
    {
        string List_Seq_Str = Load_User_PCTYList_DropDownList1.SelectedValue.ToString();
        string List_Name_Str = Load_User_PCTYList_DropDownList1.SelectedItem.Text.ToString();
        Load_Saved_Parms("PCTY", List_Seq_Str, List_Name_Str);

        //string List_Seq_Str = Load_User_PCTYList_DropDownList1.SelectedValue.ToString();
        //string List_Name_Str = Load_User_PCTYList_DropDownList1.SelectedItem.Text.ToString();

        //Load_Saved_Parms("PCTY",List_Seq_Str, List_Name_Str);
    }

    protected void Load_Saved_Parms(string prm_type, string prm_seq, string prm_name)
    {
        int wk_cnt_rows = 0;
        string wk_str = "";
        string wk_str2 = "";
        string wk_conc = "";
        string wk_digits = "";
        string wk_flow = "";
        string List_Seq_Str = "";
        string List_Name_Str = "";
        string strSQL = "";
        string wk_conc_by_db = "";
        string wk_parm_type = "";
        int strlen = 0;
        int wk_tidx = 0;

        wk_parm_type = prm_type.ToString().ToUpper();


        SqlConnection sqlconn = new SqlConnection();
        sqlconn.ConnectionString =
        "Data Source=tcp:ita-mds-tpisserver-prod-east1.database.windows.net,1433;" +
        "Initial Catalog=US_DATA;" +
        "UID=tpisgui;Password=Log$mein2021;" +
        "Integrated Security=False;";
        DataTable dt = new DataTable();
        List_Seq_Str = prm_seq.Trim();
        List_Name_Str = prm_name.ToUpper().Trim();

        try
        {
            int wk_date_i = List_Name_Str.IndexOf(" (Date:");
            if (wk_date_i > 1)
            {
                List_Name_Str = List_Name_Str.Substring(0, wk_date_i).Trim();
            }
            strSQL = "SELECT Item_value, in_line, List_Seq"
            + " from dbo.TPIS_USER_LIST_VALUES_PROD where list_seq=" + List_Seq_Str
            + " order by in_line ";
            //SqlDataAdapter sda = new SqlDataAdapter();
            //  try {

            SqlCommand sqlcom = new SqlCommand(strSQL, sqlconn);
            sqlcom.CommandType = CommandType.Text;
            sqlcom.CommandText = strSQL;
            sqlconn.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter(sqlcom))
            {
                sda.Fill(dt);
            }
            sqlconn.Close();

            wk_cnt_rows = 0;
            Parmload_Process_Label1.Text = "smrycnt= " + dt.Rows.Count.ToString() + ": ";
            wk_str = "";

            StringBuilder builder = new StringBuilder();



            foreach (DataRow row in dt.Rows)
            {
                wk_cnt_rows += 1;

                wk_str = row[0].ToString().Trim();
                if (wk_str.Length > 0)
                {

                    wk_str.Replace("\r", "").Replace("\n", "");
                    builder.Append(wk_str + "\r\n");


                }
            }
            if (wk_cnt_rows < 1)
            {
                Save_PCTYs_Status_Label.ForeColor = System.Drawing.Color.Red;
                Save_PCTYs_Status_Label.Text = " ??: LOAD ERROR NO RECORDS FOUND=List Empty?";
            }
            else
            {  // load table 
                if (builder.Length > 0)
                {
                    if (wk_parm_type.Equals("PCTY"))
                    {
                        if (Append_PCTY_Load_CkBox.Checked && Selections_TextBox.Text.Length > 0)
                            Selections_TextBox.Text =
                               Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                               + "\r\n" + builder.ToString();
                        else
                            Selections_TextBox.Text = builder.ToString();
                    }
                    else if (wk_parm_type.Equals("RCTY"))
                    {
                        if (Append_RCTY_Load_CkBox.Checked && RCTY_Selections_TextBox.Text.Length > 0)
                            RCTY_Selections_TextBox.Text =
                               RCTY_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                                + "\r\n" + builder.ToString();
                        else
                            RCTY_Selections_TextBox.Text = builder.ToString();

                    }
                    else if (wk_parm_type.Equals("STATE"))
                    {
                        //if (Append_PCTY_Load_CkBox.Checked && States_Selections_TextBox.Text.Length > 0)
                        //   States_Selections_TextBox.Text =
                        //      States_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                        //      + "\r\n" + builder.ToString();
                        //else
                        States_Selections_TextBox.Text = builder.ToString();

                    }
                    else if (wk_parm_type.Equals("SPI"))
                    {
                        //if (Append_SPI_Load_CkBox.Checked && SPIs_Selections_TextBox.Text.Length > 0)
                        //   SPIs_Selections_TextBox.Text =
                        //      SPIs_Selections_TextBox.Text.ToString().TrimEnd('\r', '\n')
                        //      + "\r\n" + builder.ToString();
                        //else
                        SPIs_Selections_TextBox.Text = builder.ToString();

                    }

                }
            } // load table ok 

        } // end try
        catch (Exception exPCTY)
        {
            wk_str2 = exPCTY.Message;
            sqlconn.Close();
            dt.Dispose();
            wk_tidx = -1;

        } // end catch
        if (List_Name_Str.Length > 0)
        {
            try
            {
                string wk_lstr = "";
                strSQL = "select list_name from dbo.TPIS_USER_LIST_HDR_PROD_NEW "
                    + " where LIST_SEQ=" + List_Seq_Str;
                SqlCommand sqlPCTY = new SqlCommand(strSQL, sqlconn);
                sqlconn.Open();
                wk_lstr = Convert.ToString(sqlPCTY.ExecuteScalar());

                sqlconn.Close();
                if (wk_lstr.Length > 0)
                    List_Name_Str = wk_lstr;
                strSQL = "select UPPER(isNull(flow,'NA')) as flow from dbo.TPIS_USER_LIST_HDR_PROD_NEW "
                    + " where LIST_SEQ=" + List_Seq_Str;
                SqlCommand sqlPCTY2 = new SqlCommand(strSQL, sqlconn);
                sqlconn.Open();
                wk_lstr = Convert.ToString(sqlPCTY2.ExecuteScalar());

                sqlconn.Close();

            }
            catch (Exception exlistname)
            {
                wk_tidx = 0;
            }

            User_PCTYsSel_Name_TextBox1.Text = List_Name_Str;
            PCTY_Total_TextBox.Text = List_Name_Str;
            Display_PCTY_TotalLabel_chkbox.Checked = true;
        }
        Save_PCTYs_Status_Label.DataBind();
        Selections_TextBox.DataBind();
        PCTY_Ajax_Panel1.Update();

    } // end Load_Saved_PCTYs_Click


} // END MAIN
