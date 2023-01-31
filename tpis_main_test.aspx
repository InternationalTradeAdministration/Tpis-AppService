<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tpis_main_test.aspx.cs" Inherits="tpis_main_test" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
    table{border-collapse: collapse;}
    tr:hover {background-color: #f5f5f5;}
    th {background-color: lightblue}
    tr:nth-child(even) {background-color: #f2f2f2;}
    th, td { padding:5px;}
    </style>
    <title></title>
      <script language="JavaScript" type="text/javascript">
          function changeText(idElement, newValue) {
              document.getElementById('idElement').innerHTML = 'newValue';
          }
      </script>
    <script type = "text/javascript">
        function Confirm_Delete() {
            var confirm_delete_value = document.createElement("INPUT");
            confirm_delete_value.type = "hidden";
            confirm_delete_value.name = "confirm_delete_value";
            if (confirm("Do you want to delete the selected Lists (IDed by .L#...)?")) {
                confirm_delete_value.value = "Yes";
            } else {
                confirm_delete_value.value = "No";
            }
            document.forms[0].appendChild(confirm_delete_value);
        }
    </script>
<script language="JavaScript" type="text/javascript">
    function new_webpage() {
        window.open('', 'Selections');
        document.getElementById('FmtContent').submit();
    }

</script>
<script language="JavaScript" type="text/javascript">
    function GoToAnchor2() {
        document.getElementById('label2').focus();
    }
</script>

</head>
<body>
    <!--#include file="Prod/includes/html/header.html"-->
    
    <form id="form1" runat="server" >
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
     
<asp:UpdatePanel ID="Main_Update_Panel" runat="server" updatemode="Conditional">
<ContentTemplate>
<asp:UpdatePanel ID="Load_Update_Job_Panel" runat="server" updatemode="Conditional">
<ContentTemplate>

<asp:Panel ID="Load_Job_Panel" runat="server" Font-Size="Small" Height="600px" 
style="margin-top: 0px" BorderStyle="Groove" Width="790px" Visible="False">
<br />
        ERROR:<asp:TextBox ID="ERROR_TextBox" runat="server" Visible="True" Width="900px" ></asp:TextBox>
<br />
        <asp:TextBox ID="Misc_Databox1" Width="800px" runat="server" Visible="True"></asp:TextBox>
<br />
        <asp:TextBox ID="Product_Name_TextBox" Width="800px" runat="server" Visible="True"></asp:TextBox>
<br />
        <asp:TextBox ID="Misc_Textbox2" runat="server" Text="Misc2" Width="800px" Visible="True">NAICS</asp:TextBox>
<br />
        <asp:TextBox ID="Misc_Textbox3" runat="server" Width="800px" Text="Misc3" Visible="True">NAICS</asp:TextBox>
<br />
        <asp:Label ID="Get_Conc_Text" Width="100px" runat="server" Visible="True"></asp:Label>
&nbsp;&nbsp;        <asp:Label ID="Get_Digits_Text" Width="100px" runat="server" Visible="True"></asp:Label>
&nbsp;&nbsp;        <asp:Label ID="Get_Conc_Index" Width="100px" runat="server" Visible="True"></asp:Label>
&nbsp;&nbsp;        <asp:Label ID="Get_Digits_Index" Width="50px" runat="server" Visible="True"></asp:Label>
&nbsp;&nbsp;        <asp:Label ID="Get_Load_Flag" Text="DEFAULT" Width="70px" runat="server" Visible="True"></asp:Label>
	 <asp:Label ID="CONC_INFO" runat="server" Font-Bold="True" 
	   Font-Size="X-Small" Text="CONC" Visible="True"></asp:Label>
<br />
<br /><br />List BOX:
<br />
    <asp:ListBox ID="Show_Load_ListBox" runat="server" 
    SelectionMode="Multiple" style="margin-bottom: 0px" Height="190px" Width="400px"  Font-Size="Small" ></asp:ListBox>

<br />
	 <asp:Label ID="CMD_SMRY" runat="server" Font-Bold="True" 
	   Font-Size="X-Small" Text="CMD SUMMARY" Visible="True"></asp:Label>
	 <asp:Label ID="CMD_SMRY2" runat="server" Font-Bold="True" 
	   Font-Size="X-Small" Text="??" Visible="True"></asp:Label>
<br />
	 <asp:Label ID="state_SMRY" runat="server" Font-Bold="True" 
	   Font-Size="X-Small" Text="state SUMMARY" 
	   AutoPostBack="True"
	   Visible="True"></asp:Label>
<br />
<br />
	 <asp:Label ID="CTY_SMRY" runat="server" Font-Bold="True" 
	   Font-Size="X-Small" Text="CTY SUMMARY" Visible="True"></asp:Label>
	 <asp:Label ID="CTY_SMRY2" runat="server" Font-Bold="True" 
	   Font-Size="X-Small" Text="??" Visible="True"></asp:Label>
</asp:Panel> <!-- end Load_Job_Panel -->
 
</ContentTemplate>
</asp:UpdatePanel>
<asp:Panel id="Login_Panel_Std" Font-Size="Small" Height="100px" 
style="margin-top: 0px" BorderStyle="Groove" Width="790px" Visible="False">
<br />
 	     <asp:Label ID="Login_Label1" runat="server" Text="Logged In As: "  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
 	     &nbsp;
             &nbsp;
             <asp:Label ID="User_ID_Valid_Label1" runat="server" Text="ANONYMOUS"  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
             &nbsp;
             <asp:Label ID="User_ID_Email_Label1" runat="server" Text="ANONYMOUS"  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <a href="https://tpis.azurewebsites.net/tpis_ctyreport_dyn_ss.aspx" target="_blank_">TPIS COUNTRY REPORT</a>
             <br />

</asp:Panel> <!-- end Login_Panel_Std -->
        <asp:Panel ID="Login_Panel" Visible="false" runat="Server">
             <br />;
             <asp:Button ID="Hide_State_Reporter_Panels" runat="server" onclick="Hide_State_Rcty_Button_Click" 
	                 Text="Hide State and Reporter Panels" Width="119px" Font-Size="X-Small" Visible="False" />

             <asp:Label ID="Passed_Parm_Label1" runat="server" Text="CHECKLoadparm"  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>

                 <asp:DropDownList ID="User_ID_DropDownList" runat="server" 
                 DataSourceID="SqlDataSource32" DataTextField="User_Name" 
                 DataValueField="User_ID" Font-Size="X-Small" Visible="True" >
             </asp:DropDownList>
             <asp:Label ID="Login_Label2" runat="server" Text="Password: "  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
             &nbsp;
             <asp:TextBox ID="Login_Passwd_TextBox1" TextMode="Password" runat="server" Visible="True" Font-Size="X-Small"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            &nbsp;<asp:Button ID="Check_Passwd_Btn" runat="server" Font-Size="X-Small" 
                onclick="Check_Passwd_Btn_Click" Text="Login" Visible="True" 
                ToolTip="5 Chances before account is disabled"/>
            &nbsp;&nbsp;&nbsp;
<br />
 	     <asp:Label ID="ADUSER_Label1" runat="server" Text="ADUSER=NULL"  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
<br />
        </asp:Panel> <!--  end Login_Panel -->

        <asp:Panel ID="Data_Panel" runat="server" Font-Size="Small" Height="600px" 
            style="margin-top: 0px" BorderStyle="Groove" Width="790px" Visible="True">
            <asp:p Font-Size="Small" >DATA SOURCE, YEARS AND FLOW SELECTION: <asp:/p>

            <p><hr> </p>
<br /><asp:Label ID="Current_DBID_Label1" runat="server" Font-Size="Small" 
  Text="US" Visible="True"> </asp:Label>     
&nbsp;&nbsp;

         <asp:DropDownList ID="Database_Type_Dropdownlist" Font-Size="X-Small" runat="server"
             AutoPostback="true" OnSelectedIndexChanged="Database_Type_DropList_Changed">
            <asp:ListItem  Value="US"  Selected>U.S. Merchandise Trade</asp:ListItem>
            <asp:ListItem Value="USST">U.S. Foreign Trade By State</asp:ListItem>
            <asp:ListItem Value="USTB">U.S. Pre-HS Merchandise Trade (1978-1988)</asp:ListItem>
            <asp:ListItem Value="UNHS">U.N. Harmonomized COMTRADE</asp:ListItem>
            <asp:ListItem Value="UNS3">U.N. SITC Revision 3 COMTRADE (1989-present)</asp:ListItem>
            <asp:ListItem Value="UNS1">U.N. SITC Revision 1 COMTRADE (1962-present)</asp:ListItem>
         </asp:DropDownList>
            
             <asp:CheckBox ID="Display_years_chkbox" runat="server" Font-Size="Small" 
                 Text="Show data for years:"  Checked="True"
                 oncheckedchanged="Display_years_chkbox_CheckedChanged" Visible="False"/>
             <br />
 	            <asp:Label ID="Years_Label_Header1" runat="server" Font-Size="X-Small" 
 	    ForeColor="Red"
 	            Text="Note: Currently only one of imports or exports can be selected " Visible="False" ></asp:Label>
             <asp:Table HorizontalAlign="Left" Width="750" Style="display:block" runat="server">
 	    <asp:TableRow>
 	    <asp:TableCell HorizontalAligh="Left" Width="260">
 	            <asp:RadioButton ID="Flow_imports_ckbox" runat="server" Font-Size="Small" 
 		         GroupName="Flow" Text="Imports"  Checked="True" 
 		         OnCheckedChanged="Flow_ckbox_changed"
 		         AutoPostBack="True"/>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr1_LDP1" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr1_LDP2" runat="server" Text=" | "  style="margin-bottom: 0px"  Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell HorizontalAlign="Left"  Width="180">
 	            <asp:RadioButton ID="Flow_exports_ckbox" runat="server" Font-Size="Small" 
 		                GroupName="Flow" Text="Exports"  Checked="False"
 		         OnCheckedChanged="Flow_ckbox_changed"
 		         AutoPostBack="True"/>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr1_LDP3" runat="server" Text="| "  style="margin-bottom: 0px" Font-Size="Small"  Visible="False"></asp:Label>
 	            <asp:RadioButton ID="Flow_balance_ckbox" runat="server" Font-Size="Small" 
 		                GroupName="Flow" Text="Balance"  Checked="False" Visible="False"
 		         OnCheckedChanged="Flow_ckbox_changed"/>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr1_LDP4" runat="server" Text="| "  style="margin-bottom: 0px" Font-Size="Small"  Visible="False"></asp:Label>
 	            <asp:RadioButton ID="Flow_2way_ckbox" runat="server" Font-Size="Small" 
 		                GroupName="Flow" Text="2-Way Trade (X+M)"  Checked="False" Visible="False"
 		         OnCheckedChanged="Flow_ckbox_changed"/>
 	    </asp:TableCell>
 	    </asp:TableRow>
 	    </asp:Table>
 
         <asp:UpdatePanel ID="Data_Update_Panel_ushs" runat="server" updatemode="Conditional">
             <ContentTemplate>
 
         <asp:Panel ID="Data_Panel_ushs" runat="server" Font-Size="Small" Height="200px" 
             style="margin-top: 0px" BorderStyle="None" Width="790px" Visible="True">

             <asp:Table HorizontalAlign="Left" Width="700" Style="display:block" runat="server">
 	    
 	    <asp:TableRow>
 	    <asp:TableCell>
 	            <asp:CheckBox ID="Flow_GenImp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="General"  Checked="True"/>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:CheckBox ID="Flow_ConImp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Consumption"  Checked="False"/>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr2_LDP1" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell HorizontalAlign="Left">
 	            <asp:CheckBox ID="Flow_DomExp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Domestic"  Checked="False"/>
 	    </asp:TableCell>
 	    <asp:TableCell HorizontalAlign="Left">
 	            <asp:CheckBox ID="Flow_ForExp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Foreign"  Checked="False"/>
 	    </asp:TableCell>
 	    <asp:TableCell HorizontalAlign="Left">
 	            <asp:CheckBox ID="Flow_TotalExp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Total"  Checked="True"/>
 	    </asp:TableCell>
 	    </asp:TableRow>
 	   
 	    <asp:TableRow>
 	    <asp:TableCell>
 	            <asp:CheckBox ID="Flow_CustVal_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Customs Value"  Checked="True"/>
 		                    <br />
 	            <asp:CheckBox ID="Flow_CifVal_ckbox" runat="server" Font-Size="Small" 
 		                    Text="CIF Value"  Checked="False"/>
 		                    <br />
 	            <asp:CheckBox ID="Flow_Qty1_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Quantity 1"  Checked="False" Enabled="True"/>
 		                    <br />
 	            <asp:CheckBox ID="Flow_Qty2_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Quantity 2"  Checked="False" Enabled="True"/>
 		                    <br />
 	            <asp:CheckBox ID="UV1_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Unit Value 1"  Checked="False" Enabled="False"/>
 		                    <br />
 	            <asp:CheckBox ID="UV2_ckbox" runat="server" Font-Size="Small" 
 		                    Text="UV 2"  Checked="False" Enabled="False"/>
 		                    <br />
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:CheckBox ID="Flow_Duty_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Duties"  Checked="False"/>
 &nbsp;
          <asp:DropDownList ID="Display_Duties_Scale_Dropdown1" Font-Size="XX-Small" runat="server">
             <asp:ListItem  Value="B" >$Bil</asp:ListItem>
             <asp:ListItem Value="M" Selected="True">$Mil</asp:ListItem>
             <asp:ListItem Value="TH">$000</asp:ListItem>
             <asp:ListItem Value="$">$</asp:ListItem>
         </asp:DropDownList>
 		                    <br />
 	            <asp:CheckBox ID="Flow_DutiableVal_ckbox" runat="server"  
 		                    Text="Dutiable Value"  Checked="False"/>
 		                    <br />
 	            <asp:CheckBox ID="Flow_Charges_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Charges"  Checked="False"/>
 &nbsp;
          <asp:DropDownList ID="Display_Charges_Scale_Dropdown1" Font-Size="XX-Small" runat="server">
             <asp:ListItem  Value="B" >$Bil</asp:ListItem>
             <asp:ListItem Value="M" Selected="True">$Mil</asp:ListItem>
             <asp:ListItem Value="TH">$000</asp:ListItem>
             <asp:ListItem Value="$">$</asp:ListItem>
         </asp:DropDownList>
 		                    <br />
 	            <asp:CheckBox ID="Flow_LDP_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Landed Cost"  Checked="False"/>
 		                    <br />
 	            <asp:CheckBox ID="AVE1_ckbox" runat="server" Font-Size="X-Small" 
 		                    Text="AVE1"  Checked="False" Enabled="False"/>
 &nbsp;
          <asp:DropDownList ID="AVE1_Type_Dropdown1" Font-Size="XX-Small" runat="server">
             <asp:ListItem  Value="CUST" Selected="True">Customs</asp:ListItem>
             <asp:ListItem Value="CIF" >CIF</asp:ListItem>
             <asp:ListItem Value="CHG">Charges</asp:ListItem>
             <asp:ListItem Value="COST">Landed Cost</asp:ListItem>
             <asp:ListItem Value="DV">Dutiable</asp:ListItem>
         </asp:DropDownList>
 		                    <br />
 	            <asp:CheckBox ID="AVE2_ckbox" runat="server" Font-Size="Small" 
 		                    Text="AVE"  Checked="False" Enabled="False"/>
 		                    <br />
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowtypr1_LDP1a" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	            <br /><asp:Label ID="Flowtypr1_LDP1b" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	            <br /><asp:Label ID="Flowtypr1_LDP1c" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	            <br /><asp:Label ID="Flowtypr1_LDP1d" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	            <br /><asp:Label ID="Flowtypr1_LDP1d1" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	            <br /><asp:Label ID="Flowtypr1_LDP1d2" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	            <br /><asp:Label ID="Flowtypr1_LDP1d3" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:CheckBox ID="Flow_FASVal_ckbox" runat="server" Font-Size="Small" 
 		                    Text="FAS Value"  Checked="True"/>
 		                    <br />
 	            <asp:CheckBox ID="Flow_ExpQty1_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Quantity 1"  Checked="False" Enabled="True"/>
 		                    <br />
 	            <asp:CheckBox ID="Flow_ExpQty2_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Quantity 2"  Checked="False" Enabled="True"/>
 		                    <br />
 		                    <br />
 		                    <br />
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowtypr1_LDP3" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowtypr1_LDP4" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    </asp:TableRow>
 	    </asp:Table>

         </asp:Panel>  <!-- end Data_Panel_ushs -->
 
             </ContentTemplate>
         </asp:UpdatePanel> <!-- end Data_Update_Panel_ushs -->
         
         <asp:UpdatePanel ID="Data_Update_Panel_un_state" runat="server" updatemode="Conditional">
             <ContentTemplate>

         <asp:Panel ID="Data_Panel_un_state" runat="server" Font-Size="Small" Height="200px" 
             style="margin-top: 0px" BorderStyle="None" Width="790px" Visible="False">
         
             <asp:Table HorizontalAlign="Left" Width="700" Style="display:block" runat="server">
 	    
 	    <asp:TableRow>
 	    <asp:TableCell HorizontalAlign="Left" Width="129">
 	            <asp:CheckBox ID="UNFlow_GenImp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="General"  Checked="True"/>
 	    </asp:TableCell>
 	    <asp:TableCell HorizontalAlign="Left" Width="130">
 	            <asp:CheckBox ID="UNFlow_ConImp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Consumption"  Checked="False"  />
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="UNFlowr2_LDP1" runat="server" Text=" | "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell HorizontalAlign="Left">
 	            <asp:CheckBox ID="UNFlow_TotalExp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Total"  Checked="True"/>
 	    </asp:TableCell>
 	    <asp:TableCell HorizontalAlign="Left">
 	            <asp:CheckBox ID="UNFlow_DomExp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Domestic"  Checked="False"/>
 	    </asp:TableCell>
 	    <asp:TableCell HorizontalAlign="Left">
 	            <asp:CheckBox ID="UNFlow_ReExp_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Reexports"  Checked="False"/>
 	    </asp:TableCell>
 	    </asp:TableRow>
 	   
 	    <asp:TableRow>
 	    <asp:TableCell>
 	     <asp:CheckBox ID="UNFlow_Value_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Value"  Checked="True"/>
 		                    <br />
 		                    <br />
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:CheckBox ID="UNFlow_Volume_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Volume"  Checked="False" Enabled="False"/>
 		                    <br />
 	            <asp:CheckBox ID="UV_Volume_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Unit Value (Vol)"  Checked="False" Enabled="False"/>
 		                    <br />
 	            <asp:Label ID="UNFlowtypr1_LDP1a" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <br /><asp:Label ID="UNFlowtypr1_LDP1b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:CheckBox ID="UNFlow_Weight_ckbox" runat="server" Font-Size="Small" 
 		                    Text="Weight"  Checked="False" Enabled="False"/>
 		                    <br />
 	            <asp:CheckBox ID="UV_Weight_ckbox" runat="server" Font-Size="Small" 
 		                    Text="UV by Weight"  Checked="False" Enabled="False"/>
 		                    <br />
 	            <br /><asp:Label ID="UNFlowtypr1_LDP1e" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="UNFlowtypr1_LDP3" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="UNFlowtypr1_LDP4" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	            <br /><asp:Label ID="UNFlowtypr1_LDP1c" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	            <br /><asp:Label ID="UNFlowtypr1_LDP1d" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    </asp:TableRow>
 	    </asp:Table>

         
         </asp:Panel> <!-- end Data_Panel_un_state   -->
 
             </ContentTemplate>
         </asp:UpdatePanel>
 
          <asp:UpdatePanel ID="Data_Update_Panel_Years" runat="server" updatemode="Conditional">
             <ContentTemplate>
             
          <asp:Panel ID="Data_Panel_Years" runat="server" Font-Size="Small" Height="200px" 
             style="margin-top: 0px" BorderStyle="None" Width="790px" Visible="True">
 
             <asp:Table HorizontalAlign="Left" Width="700" Style="display:block" runat="server">
 
 	    <asp:TableRow>
 	    <asp:TableCell>
 	        <asp:Label ID="Scale_Val_LDP1" runat="server" Text="Scale for Values:"  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
  	    </asp:TableCell>
 	    <asp:TableCell>
               <asp:DropDownList ID="Display_Values_Scale_Dropdown1" Font-Size="Small" runat="server">
                  <asp:ListItem  Value="$BIL" >$Bil</asp:ListItem>
                  <asp:ListItem Value="$MIL" >$Mil</asp:ListItem>
                  <asp:ListItem Value="$000">$000</asp:ListItem>
                 <asp:ListItem Value="$" Selected >$</asp:ListItem>
               </asp:DropDownList>
 	    </asp:TableCell>
 	    </asp:TableRow>
 	   
 	   <asp:TableRow   >
 	    <asp:TableCell style="border-bottom:1px solid black;" span="5">
 	            
 	    </asp:TableCell>
 	    </asp:TableRow>


 	    <asp:TableRow>
 	    <asp:TableCell>
                 <asp:CheckBox ID="Year_ann_ckbox" runat="server" Font-Size="Small" 
                 Text="Annual:"  Checked="True"
                 Visible="True"/>
 	    </asp:TableCell>
 	    <asp:TableCell>
                 <asp:DropDownList ID="Show_Year1_DropDownList" runat="server" 
                 DataSourceID="SqlDataSource8" DataTextField="YEAR_LBL" 
                 DataValueField="YEAR_CODE" Font-Size="X-Small" >
             </asp:DropDownList>
 	    &nbsp;
 	            <asp:Label ID="Flowr3_LDP1" runat="server" Text="to"  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
 	        &nbsp;
                 <asp:DropDownList ID="Show_Year2_DropDownList" runat="server" 
                 DataSourceID="SqlDataSource8" DataTextField="YEAR_LBL" 
                 DataValueField="YEAR_CODE" Font-Size="X-Small"  >
             </asp:DropDownList>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr3_LDP2" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr3_LDP3" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr3_LDP4" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    <asp:TableCell>
 	            <asp:Label ID="Flowr3_LDP5" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	    </asp:TableCell>
 	    </asp:TableRow>
 	    <asp:TableRow>
 	   
 	        <asp:TableCell>
 	   	       <asp:CheckBox ID="Year_ytd_ckbox" runat="server" Font-Size="Small" 
 	   	       Text="Year-To-Date:"  Checked="True"
 	   	       Visible="True"/>
 	       </asp:TableCell>
 	       <asp:TableCell>
 	   	  <asp:DropDownList ID="Show_YTD1_DropDownList" runat="server" 
 	   	    		DataSourceID="SqlDataSource30" DataTextField="YEAR_LBL" 
 	   	    		DataValueField="YEAR_CODE" Font-Size="X-Small"  >
 	   	  </asp:DropDownList>
 	   	 &nbsp;
 	   	 <asp:Label ID="YTD_to_Label" runat="server" Font-Size="X-Small" Text="to"></asp:Label>
 	   	    	&nbsp;
 	   	    	<asp:DropDownList ID="Show_YTD2_DropDownList" runat="server" 
 	   	    	DataSourceID="SqlDataSource30" DataTextField="YEAR_LBL" 
 	   	    	DataValueField="YEAR_CODE" Font-Size="X-Small" >
 	   	 </asp:DropDownList>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	   <asp:Label ID="Flowr4_LDP2" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	            <asp:Label ID="Flowr4_LDP3" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	            <asp:Label ID="Flowr4_LDP4" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	   	    <asp:TableCell>
 	   	            <asp:Label ID="Flowr4_LDP5" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	   </asp:TableRow>
 
 	    <asp:TableRow>
 	   
 	        <asp:TableCell>
 	   	       <asp:CheckBox ID="Year_month_ckbox" runat="server" Font-Size="Small" 
 	   	       Text="Monthly:"  Checked="False"
 	   	       Visible="True"/>
 	       </asp:TableCell>
 	       <asp:TableCell>
 	   	  <asp:DropDownList ID="Show_Month1_DropDownList" runat="server" 
 	   	    		DataSourceID="SqlDataSource30" DataTextField="YEAR_LBL" 
 	   	    		DataValueField="YEAR_CODE" Font-Size="X-Small"  >
 	   	  </asp:DropDownList>
 	   	 &nbsp;
 	   	 <asp:Label ID="Month_to_Label" runat="server" Font-Size="X-Small" Text="to"></asp:Label>
 	   	    	&nbsp;
 	   	    	<asp:DropDownList ID="Show_Month2_DropDownList" runat="server" 
 	   	    	DataSourceID="SqlDataSource30" DataTextField="YEAR_LBL" 
 	   	    	DataValueField="YEAR_CODE" Font-Size="X-Small" >
 	   	 </asp:DropDownList>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	   <asp:Label ID="Month_LDP2" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	            <asp:Label ID="Month_LDP3" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	            <asp:Label ID="Month_LDP4" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	   	    <asp:TableCell>
 	   	            <asp:Label ID="Month_LDP5" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	   </asp:TableRow>
 
 	    <asp:TableRow>
 	   
 	        <asp:TableCell>
 	   	       <asp:CheckBox ID="Year_quarter_ckbox" runat="server" Font-Size="Small" 
 	   	       Text="Quarterly:"  Checked="False"
 	   	       Visible="True"/>
 	       </asp:TableCell>
 	       <asp:TableCell>
 	   	  <asp:DropDownList ID="Show_Quarter1_DropDownList" runat="server" 
 	   	    		DataSourceID="SqlDataSource30" DataTextField="YEAR_LBL" 
 	   	    		DataValueField="YEAR_CODE" Font-Size="X-Small"  >
 	   	  </asp:DropDownList>
 	   	 &nbsp;
 	   	 <asp:Label ID="Quarter_to_Label" runat="server" Font-Size="X-Small" Text="to"></asp:Label>
 	   	    	&nbsp;
 	   	    	<asp:DropDownList ID="Show_Quarter2_DropDownList" runat="server" 
 	   	    	DataSourceID="SqlDataSource30" DataTextField="YEAR_LBL" 
 	   	    	DataValueField="YEAR_CODE" Font-Size="X-Small" >
 	   	 </asp:DropDownList>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	   <asp:Label ID="Quarter_LDP2" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	            <asp:Label ID="Quarter_LDP3" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	            <asp:Label ID="Quarter_LDP4" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	   	    <asp:TableCell>
 	   	            <asp:Label ID="Quarter_LDP5" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	   </asp:TableRow>
 
 	    <asp:TableRow>
 	   
 	        <asp:TableCell span="5">
 	   	       <asp:CheckBox ID="Years_In_Rows_ckbox" runat="server" Font-Size="X-Small" 
 	   	       Text="Format Output One Year Per Row for > 2 Years<br />of Monthly and/or Quarterly Data."  Checked="False"
 	   	       Visible="True"
                       ToolTip="The same year ranges apply to each frequency CHECKED in one-year-per-row format:  For more than 10 Years of Monthly/Quarterly Data Output is Always 1 Year Per Row."
 	   	       />
 	       </asp:TableCell>
 	   </asp:TableRow>

 	    <asp:TableRow>
 	      <asp:TableCell span="5">
 	   	 <asp:Label ID="Calcs_LDP1" runat="server" Text="Time Calculations:  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	      <asp:TableCell >
                  <asp:CheckBox ID="Yr1_LastYr_CheckBox" runat="server" Text="1st Year-Last Year" />
                 <br /><asp:CheckBox ID="Last_2Yrs_CheckBox" runat="server" Text="Last 2 Years" />
                 <br /><asp:CheckBox ID="Last_2_YTD_CheckBox" runat="server" Text="Last 2 Years-to-Date" />
                 <br /><asp:CheckBox ID="All_Years_CheckBox" runat="server" Text="All Periods" Enabled="False"/>
                 
               </asp:TableCell>
 	      <asp:TableCell>
 	   	            <asp:Label ID="Calcs_LDP0" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	      <asp:TableCell >
                  <asp:CheckBox ID="PctChange_CheckBox" runat="server" Text="Percent Change" />
                   
                 <br /><asp:CheckBox ID="AbsChange_CheckBox" runat="server" Text="Change in Value" />
                 <br /><asp:CheckBox ID="GrRate_CheckBox" runat="server" Text="Growth Rate" Visible="True"
                 ToolTip="Average End Point Percent Change~=Nth root of percent change 1st-last"/>
               </asp:TableCell>
 	   </asp:TableRow>
 	   <asp:TableRow>
 	    <asp:TableCell >
                <asp:CheckBox ID="Value_Constraint_CheckBox" runat="server" Text="Value Constraints" />
                <br /><asp:Label ID="Value_Constraint_Label" runat="server" 
                 Text="(ex1: &gt;=5M, Ex2:&gt;1B)"></asp:Label>
               </asp:TableCell>
 	      <asp:TableCell >
                 <asp:TextBox ID="Value_Constraint_TextBox" runat="server" Width="200px" 
                 Font-Size="XX-Small"></asp:TextBox>
 
 	      </asp:TableCell>
 	      <asp:TableCell>
 	   	            <asp:Label ID="Calcs_LDP4" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	   	    <asp:TableCell>
 	   	            <asp:Label ID="Calcs_LDP5" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
 	      </asp:TableCell>
 	   </asp:TableRow>
 	   </asp:Table>


          </asp:Panel> <!-- end Data_Panel_Years -->

             </ContentTemplate>
         </asp:UpdatePanel>  <!-- end Data_Update_Panel_Years -->


 <asp:panel ID="panel_skip1" runat="server" Visible="False">
            <br/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &gt;=<asp:TextBox 
                 ID="PctChange_TextBox" runat="server" 
                 Width="44px">ALL</asp:TextBox>
             &nbsp;%<br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &gt;=<asp:TextBox 
                 ID="AbsChange_TextBox" runat="server" 
                 Height="19px" Width="44px">ALL</asp:TextBox>
             <br />
 	    <br/>
 	    <br/>
             <br />
          <asp:Label ID="Display_Format_lbl1" runat="server" Text="Column Headers"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="Display_Colheader_Format_Dropdown1" runat="server">
             <asp:ListItem  Value="FS" Selected="True">Formatted</asp:ListItem>
             <asp:ListItem Value="FNS">No Spanned Columns</asp:ListItem>
             <asp:ListItem Value="DB">Database Columns</asp:ListItem>
         </asp:DropDownList>
             <br />
 </asp:panel>

 
 
        </asp:Panel> <!--   end data_panel for years flows and database  -->
 
 
 
             <br /><br />
         <asp:UpdatePanel ID="CMD_Ajax_Panel1" runat="server" updatemode="Conditional">
           <ContentTemplate>
 
<a name="TPISCMDSETUP"></a>
         <asp:Panel ID="Data_CMD_Panel" runat="server" Font-Size="Small" Height="200px" 
             style="margin-top: 0px" BorderStyle="Groove" Width="790px" Visible="True"
             >
             <asp:p Font-Size="Small" >Select Commodities: <asp:/p>
 	     <asp:RadioButton ID="CMD_TOTAL_ALL_CkBox" runat="server" Font-Size="Small" 
 		GroupName="CMD_DETAIL_CK" Text="All Merchandise Trade"  Checked="True" 
 		OnCheckedChanged="CMD_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
 	     <asp:RadioButton ID="CMD_SELECTED_CkBox" runat="server" Font-Size="Small" 
 		GroupName="CMD_DETAIL_CK" Text="Selected Products"  Checked="False" 
 		OnCheckedChanged="CMD_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
   	     <asp:Label ID="Show_Conc_Digits_CK" Visible="False" runat="server" Font-Size="XX-Small" Text=""></asp:Label>
           
             <hr> 
             <asp:CheckBox ID="Display_CMDTOTAL_chkbox" runat="server" Font-Size="X-Small" 
                 Text="Total of Selected"  Checked="True" />
              <asp:CheckBox ID="Display_CMDSUB_chkbox" runat="server" Font-Size="X-Small" 
                 Text="Subgroups & Products Shown"  Checked="False" />
              <br />&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Display_CMDDETAIL_chkbox" runat="server" Font-Size="X-Small" 
                 Text="Detail for All Products at the Digit Level Shown"  Checked="False" AutoPostBack="True" />
              <asp:CheckBox ID="Display_CMDShare_chkbox" runat="server" Font-Size="X-Small" 
                 Text="% of Total"  Checked="False" />
                 
                 
 <br />
        <asp:Label ID="Product_Source_Label1" runat="server" Font-Size="X-Small" Text="Source:"></asp:Label>
	&nbsp;&nbsp;<asp:DropDownList ID="DataSource_DropDownList1" runat="server" Font-Size="X-Small" AutoPostBack="True" 
	            DataSourceID="SqlDataSource22" DataTextField="Group_Name" 
	            DataValueField="Group_Code" OnSelectedIndexChanged="DataSource_Change">
	</asp:DropDownList>
        &nbsp;&nbsp;<asp:Label>DIGITS (X,M):</asp:Label>&nbsp;
        <asp:DropDownList ID="DIGITS_ID_Dropdown1" runat="server" Font-Size="X-Small" AutoPostBack="True" 
            DataSourceID="SqlDataSource31" DataTextField="Digits_Desc" 
            DataValueField="Digits_Code"
            OnSelectedIndexChanged="Digits_ID_Dropdown_Change"
            >
        </asp:DropDownList>
<br />

        <asp:SqlDataSource ID="SqlDataSource22" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select product_label as group_name, source_code as group_code, display_order 
            from dbo.tpis_dyn_product_source_ids
            where source_type=UPPER(@T1)
            order by display_order, group_name ">
        <SelectParameters>
            <asp:ControlParameter ControlID="Database_Type_Dropdownlist" 
                DefaultValue="US" Name="T1" PropertyName="SelectedValue" />
        </SelectParameters>
        </asp:SqlDataSource>


        <asp:SqlDataSource ID="SqlDataSource31" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select conc, ref_digits as Digits_Code, ref_digits_desc as Digits_Desc
            , ref_digit_lov_box
            from dbo.V_TPIS32_PRODREPORT_DIGITS_LOV
            WHERE  CONC = @conc 
            ORDER BY ref_digits">
            <SelectParameters>
                <asp:ControlParameter ControlID="DataSource_DropDownList1" 
                    DefaultValue="US:NAICS" Name="conc" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>


<br />
                 

      <asp:Panel ID="Selected_CMD_Panel" runat="server" Font-Size="Small" Height="600px" 
             style="margin-top: 0px" BorderStyle="None" Width="790px" Visible="False">
             
              <br /><br /><asp:CheckBox ID="Display_CMD_TotalLabel_chkbox" runat="server" Font-Size="Small" 
                 Text="Your label for Total"  Checked="False" />
             &nbsp;&nbsp;<asp:TextBox ID="CMD_Total_TextBox" runat="server" Visible="True" Font-Size="X-Small" 
                 Width="50%" Text=""></asp:TextBox>
        <br />

        <asp:Label ID="Prod_Label_Header1" runat="server" Font-Size="X-Small" 
ForeColor="Red"
        Text="Product selection any of the following work: "></asp:Label>
        <br /><asp:Label ID="Prod_Label_Header2" runat="server" Font-Size="X-Small" 
ForeColor="Red"
        Text="Select a single item from dropdown lists, then set the digit level for the output level of detail "></asp:Label>
        <br /><asp:Label ID="Prod_Label_Header3" runat="server" Font-Size="X-Small" 
ForeColor="Red"
        Text="Type or paste product numbers into the Current User Selections Box, one product per line"></asp:Label>
        <br /><asp:Label ID="Prod_Label_Header4" runat="server" Font-Size="X-Small" 
ForeColor="Red"
        Text="Highlight Products in the Select from list, and Click the Select Button"></asp:Label>
        

<br /><br />
                

        <br/><asp:Label>FLOW (X,M):</asp:Label>&nbsp; 
        <asp:TextBox ID="Flow_TextBox1" Text="M" runat="server" Visible="True"></asp:TextBox>

<br />

        <asp:Label ID="Prod_Label1" runat="server" Font-Size="X-Small" Text="PRODUCT:"></asp:Label>
&nbsp;
        &nbsp;&nbsp;<asp:DropDownList ID="Product_DropDownList1" runat="server" Font-Size="X-Small" AutoPostBack="True" 
            DataSourceID="SqlDataSource21" DataTextField="PROD_CODENDESC" 
            DataValueField="PRODUCT_CODE" OnSelectedIndexChanged="Product_DropDownList1_Change">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Prod_Detail1_Label1" runat="server" Font-Size="X-Small" Text="..Product Detail:"></asp:Label>
&nbsp;
        &nbsp;&nbsp;<asp:DropDownList ID="Product_Detail1_DropDownList1" runat="server" Font-Size="X-Small" AutoPostBack="True" 
            DataSourceID="SqlDataSource24" DataTextField="PROD_CODENDESC" 
            DataValueField="PRODUCT_CODE" OnSelectedIndexChanged="Product_DropDownList_Detail1_Change">
        </asp:DropDownList>
&nbsp;
        <br />
        <asp:Label ID="Prod_Detail1_Label2" runat="server" Font-Size="X-Small" Text="..More Product Detail:"></asp:Label>
&nbsp;  <asp:DropDownList ID="Product_Detail1_DropDownList2" runat="server" Font-Size="X-Small" AutoPostBack="True" 
            DataSourceID="SqlDataSource25" DataTextField="PROD_CODENDESC" 
            DataValueField="PRODUCT_CODE" OnSelectedIndexChanged="Product_DropDownList_Detail2_Change">
        </asp:DropDownList>
<br />
                 



<!--   *************************************** -->

<br />
<asp:Table HorizontalAlign="Left" Style="display:block" Width="500px" runat="server">
<asp:TableRow>
<asp:TableCell>
    <asp:TextBox ID="Series_TextBox" runat="server" Visible="True" Font-Size="X-Small" 
                Width="200px"
            ToolTip="If you type a product here (no ranges), refresh by hitting SELECT button below."
            >TOTAL</asp:TextBox>
            <asp:TextBox ID="Series_Type_TextBox" runat="server" Visible="True" Font-Size="X-Small" 
                Width="50px" Text="US:HS"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;    
   <asp:Label ID="Select_CMD_From_List_Label1" runat="server" Font-Size="X-Small" Text="Select From List" Width="180px" ></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_CMD_Labela1" runat="server" Text="  "  style="margin-bottom: 0px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
   <asp:Label ID="Current_User_CMD_Selections_Label1" runat="server" Font-Size="X-Small" Text="Current User Selections:" Width="180px" ></asp:Label>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
        <asp:ListBox ID="Pick_CMD_ListBox" runat="server" DataSourceID="SqlDataSource29" 
            DataTextField="PRODUCT_NAME" DataValueField="PRODUCT_CODE" 
    SelectionMode="Multiple" style="margin-bottom: 0px" Height="190px" Width="400px"  Font-Size="Small" ></asp:ListBox>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_CMD_Label1" runat="server" Text="  "  style="margin-bottom: 0px" Height="190px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:TextBox ID="Selections_CMD_TextBox" runat="server" Height="190px" 
            style="margin-bottom: 0px" TextMode="MultiLine" Width="230px" Wrap="False" Font-Size="Small"
            ToolTip="Enter 1 product or group (=some name) per line, Use hyphen for ranges (31-33), ! as 1st character means NOT/do not include."
            ></asp:TextBox>
        <br />
</asp:TableCell>
<asp:TableCell>
        <asp:ListBox ID="Selected_CMD_ListBox" runat="server" 
            style="margin-bottom: 0px; display:none" Height="190px" SelectionMode="Multiple" 
            Width="180px" Visible="False" >
        </asp:ListBox>
</asp:TableCell>
<asp:TableCell >

</asp:TableCell>

</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="MoveSelected_CMD_Button" runat="server" 
                onclick="MoveSelected_CMD_Button_Click"  Text="Select &gt;" 
            Width="91px" Font-Size="X-Small" />
                <asp:Button ID="MoveNotSelected_CMD_Button" runat="server" Visible="True"
                    onclick="MoveNotSelected_CMD_Button_Click" Text="Select NOT &gt;" 
                    ToolTip="Copies Selected Items and Puts the NOT/EXCLUDE symbol ! in front"
            Width="91px" Font-Size="X-Small" />

</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="NewList_CMD_Button" runat="server" onclick="NewList_Button_Click" 
            Text="New List" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="MoveAll_CMD_Button" runat="server" 
            onclick="MoveAll_CMD_Button_Click" Text="Select All &gt;&gt;" Width="90px" 
            Font-Size="X-Small" />
                <asp:Button ID="DeleteLists_CMD_Button" runat="server" Visible="False"
                    ToolTip="Deletes only Session Lists Selected in the Select From List and/or Current User Selections Box"
            Width="91px" Font-Size="X-Small" />
            <!--  
            
            onclick="DeleteLists_CMD_Button_Click" Text="Delete My Lists" 
            OnClientClick = "Confirm_CMD_Delete()"
             -->

</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="RemoveAll_CMD_Button" runat="server" 
            onclick="RemoveAll_CMD_Button_Click" Text="Clear Selections" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell Span="5">
        <asp:Label ID="User_CMDsGroup_Name_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Name for Subgroup" >
        </asp:Label>
        &nbsp;<asp:Button ID="Create_CMDsGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="Create Subgroup" Width="120px" 
            onclick="Create_CMDsGroup_Click" 
            ToolTip="Must Enter a Group Label in Text Box Below"
            /> 
        &nbsp;<asp:Button ID="End_CMDsGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="End Subgroup" Width="120px" 
            onclick="End_CMDsGroup_Click" 
            ToolTip="Groups must be terminated with }"/> 
         <br />
         <asp:TextBox ID="User_CMDsGroup_Name_TextBox1" runat="server" Font-Size="XX-Small" Width="400px"></asp:TextBox>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell Span="5">
        <asp:Label ID="Load_CMDLists_Label1" Style="display:inline" Font-Size="XX-Small" 
        runat="server" Text="Your Saved Lists" ></asp:Label>
        &nbsp;<asp:Button ID="Load_Saved_CMDs_Button" runat="server" Font-Size="XX-Small" 
                Text="Load List" Width="70px" 
            onclick="Load_Saved_CMDs_Click" 
            ToolTip="Load list of saved commodities from the dropdown"/> 
        &nbsp;&nbsp;
        <asp:CheckBox ID="Append_CMD_Load_CkBox" runat="server" Font-Size="XX-Small" 
                 Text="Append List" ToolTip="Append do not replace current selections" Checked="False" />
        <br />
        <asp:DropDownList ID="Load_User_CMDList_DropDownList1" runat="server" Font-Size="XX-Small" 
        DataSourceID="SqlDataSource40"  DataTextField="LIST_NAME" 
            DataValueField="LIST_SEQ"
            AutoPostBack="True"  >
        </asp:DropDownList>
        &nbsp;
	<asp:Button ID="Delete_CMD_List_Button" runat="server" Font-Size="XX-Small" 
	Text="Delete List" Width="70px" 
	onclick="Button_Delete_CMD_List_Click" 
	ToolTip="Delete Your Saved List Shown;type delete in the box to verify"/> 
        <br />
        <asp:Label ID="User_CMDSel_Name_Label1" Style="display:inline" Font-Size="XX-Small" 
        runat="server" Text="Delete a List: Type DELETE then Press Delete Button:" ></asp:Label>
        <asp:TextBox ID="CMD_Verification_TextBox1" runat="server" Font-Size="XX-Small"  Width="70px" 
        ToolTip="Enter DELETE to verify you want to delete selected list"></asp:TextBox>
        <asp:Panel
     <br />
     <asp:Panel ID="Cmd_SaveNew_Panel" runat="server" Font-Size="Small" 
            style="margin-top: 0px; display:inline" BorderStyle="None" Width="790px" Visible="True">


        <asp:Label ID="User_ListSeq_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
        <asp:Label ID="List_NewSeq_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small" ></asp:Label>
        <br /><asp:Label ID="SubmitList_User_Processing_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
        <asp:Button ID="Save_CMDs_Button" runat="server" Font-Size="XX-Small" 
                Text="Save List As:" Width="65px" 
            onclick="Button_Save_CMDs_Click" 
            ToolTip="Save Selections as the name you provide"/> 
        <asp:TextBox ID="User_CMDsSel_Name_TextBox1" runat="server" Font-Size="XX-Small"  Width="300px" ></asp:TextBox>
        <br />
        <asp:CheckBox ID="Resave_CMD_Load_CkBox" runat="server" Font-Size="XX-Small" 
	Text="Replace Lists with Matching Name" ToolTip="Delete list with matching name before saving new list" Checked="False" />
        <br />
     </asp:Panel> 
     <br /><asp:Label ID="Save_CMDs_Status_Label" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Save List Not Loaded" Visible="True" ></asp:Label>
        <asp:Label ID="Out_label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="" Visible="False" ></asp:Label>


</asp:TableCell>
</asp:TableRow>

</asp:Table>


         <asp:SqlDataSource ID="SqlDataSource40" 
             runat="server" ConnectionString=
             "<%$ ConnectionStrings:TPISAZUS%>" 
             SelectCommand="SELECT LIST_SEQ
            , LIST_NAME 
            FROM dbo.V_TPIS_USER_LIST_HDR_CMD_GUI 
            where lower(TPIS_USER)= lower(@uid)
            and LIST_TYPE='COMMODITY'
            ORDER BY LIST_NAME">
            <SelectParameters>
                <asp:ControlParameter ControlID="User_ID_Valid_Label1" 
                    DefaultValue="pomeror" Name="uid" PropertyName="Text" />
            </SelectParameters>
         </asp:SqlDataSource>

<!--   *************************************** -->

      </asp:Panel> <!--   end Selected_CMD_Panel  -->

     <br />
     <br />
     </asp:Panel> <!--  end Data_CMD_Panel   -->
     
      </ContentTemplate>            
     </asp:UpdatePanel> <!--   end CMD_Ajax_Panel1 -->

   <asp:UpdatePanel ID="PCTY_Ajax_Panel1" runat="server" updatemode="Conditional">
           <ContentTemplate>

 
     <asp:Panel ID="Partner_Panel" runat="server" Font-Size="Small" Height="200px" 
            style="margin-top: 0px" BorderStyle="Groove" Width="790px" Visible="True">
     <a name="TPISPCTYSETUP"></a>
            
            <asp:p Font-Size="Small" >Partner Country Selection: <asp:/p>
 	     <asp:RadioButton ID="PCTY_TOTAL_ALL_CkBox" runat="server" Font-Size="Small" 
 		GroupName="PCTY_DETAIL_CK" Text="World (All Countries)"  Checked="True" 
 		OnCheckedChanged="PCTY_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
 	     <asp:RadioButton ID="PCTY_SELECTED_CkBox" runat="server" Font-Size="Small" 
 		GroupName="PCTY_DETAIL_CK" Text="Selected Countries"  Checked="False" 
 		OnCheckedChanged="PCTY_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
            <hr> 
            <asp:CheckBox ID="Display_PCTYTOTAL_chkbox" runat="server" Font-Size="Small" 
                Text="Total of Selected"  Checked="True" />
             <asp:CheckBox ID="Display_PCTYSUB_chkbox" runat="server" Font-Size="Small" 
                Text="Subgroups & Partners Shown"  Checked="False" />
             <br /><asp:CheckBox ID="Display_PCTYDETAIL_chkbox" runat="server" Font-Size="Small" 
                Text="Show All Partners Selected"  Checked="False" />
             &nbsp;&nbsp;<asp:CheckBox ID="Display_PCTYShare_chkbox" runat="server" Font-Size="X-Small" 
                Text="% of Total"  Checked="False" />
                


<asp:Panel ID="Selected_Partner_Panel" runat="server" Font-Size="Small" Height="500px" 
            style="margin-top: 0px" BorderStyle="None" Width="780px" Visible="False">
            
            
             <br /><asp:CheckBox ID="Display_PCTY_TotalLabel_chkbox" runat="server" Font-Size="Small" 
                Text="Your label for Total"  Checked="False" />
            &nbsp;&nbsp;<asp:TextBox ID="PCTY_Total_TextBox" runat="server" Visible="True" Font-Size="X-Small" 
                Width="50%" Text=""></asp:TextBox>
                <br /> <br />

<asp:Table HorizontalAlign="Left" Width="700" Style="display:block" runat="server">
<asp:TableRow>
<asp:TableCell>
        Source:&nbsp;&nbsp;
        <asp:DropDownList ID="Source_DropDownList1" runat="server" 
            DataSourceID="SqlDataSource3" DataTextField="SHARE_TYPE_NAME" 
            DataValueField="GROUP_TYPE" AutoPostBack="True"  >
        </asp:DropDownList>
        
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_Labelurlr2a" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_Labelurlr2b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
        <asp:DropDownList ID="CountryGroup_DropDownList" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="GRP_NAME" 
            DataValueField="GRP_CODE" AutoPostBack="True" Font-Size="X-Small"
            OnSelectedIndexChanged="CountryGroup_DropDownList_Change"
            >
        </asp:DropDownList>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_Labelurlr3a" runat="server" Text="  "  style="margin-bottom: 0px"  Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_Labelurlr3b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
</asp:TableRow>
</asp:Table>
        <br />

<asp:Table HorizontalAlign="Left" Style="display:block" Width="500px" runat="server">
<asp:TableRow>
<asp:TableCell>
   <asp:Label ID="Select_From_List_Label1" runat="server" Font-Size="X-Small" Text="Select From List" Width="180px" ></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_Labela1" runat="server" Text="  "  style="margin-bottom: 0px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
   <asp:Label ID="Current_User_Selections_Label1" runat="server" Font-Size="X-Small" Text="Current User Selections:" Width="180px" ></asp:Label>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
        <asp:ListBox ID="Pick_ListBox" runat="server" DataSourceID="SqlDataSource2" 
            DataTextField="CTY_CODE" DataValueField="CTY_CODE" 
    SelectionMode="Multiple" style="margin-bottom: 0px" Height="190px" Width="230px"  Font-Size="Small" ></asp:ListBox>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_Label1" runat="server" Text="  "  style="margin-bottom: 0px" Height="190px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:TextBox ID="Selections_TextBox" runat="server" Height="190px" 
            style="margin-bottom: 0px" TextMode="MultiLine" Width="230px" Wrap="False" Font-Size="Small"
            ToolTip="1 country or group per line, use ! as 1st character as a NOT/do not include indicator.  TPIS Groups start with a . and your saved session lists with .L#"
            ></asp:TextBox>
        <br />
</asp:TableCell>
<asp:TableCell>
        <asp:ListBox ID="Selected_ListBox" runat="server" 
            style="margin-bottom: 0px; display:none" Height="190px" SelectionMode="Multiple" 
            Width="180px" Visible="False" >
        </asp:ListBox>
</asp:TableCell>
<asp:TableCell  Width="300px">

</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="MoveSelected_Button" runat="server" 
                    onclick="MoveSelected_Button_Click" Text="Select &gt;" 
            Width="91px" Font-Size="X-Small" />
                <asp:Button ID="MoveNotSelected_Button" runat="server" Visible="True"
                    onclick="MoveNotSelected_Button_Click" Text="Select NOT &gt;" 
                    ToolTip="Copies Selected Items and Puts the NOT/EXCLUDE symbol ! in front"
            Width="91px" Font-Size="X-Small" />
            
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="NewList_Button" runat="server" onclick="NewList_Button_Click" 
            Text="New List" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="MoveAll_Button" runat="server" 
            Text="Select All &gt;&gt;" Width="90px" onclick="MoveAll_Button_Click" 
            Font-Size="X-Small" />
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="RemoveAll_Button" runat="server" onclick="RemoveAll_Button_Click" 
            Text="Clear Selections" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>



</asp:Table>



   <asp:Panel ID="Selections_Panel2" Visible="False" Width="500px" Style="display:block;clear" runat="server">
        <br />

        <br />
        <asp:Label ID="PCTY_Divide_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" 
           Text="--------------------------------------------" ></asp:Label>
       
        <br /><asp:Label ID="User_Subgroup_Name_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Name for Subgroup" ></asp:Label>
        &nbsp;<asp:Button ID="Create_PctyGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="Create Subgroup" Width="120px" 
            onclick="Create_PctyGroup_Click" 
            ToolTip="Must Enter a Group Label in Text Box Below"/> 
        &nbsp;<asp:Button ID="End_PctyGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="End Subgroup" Width="120px" 
            onclick="End_PctyGroup_Click" 
            ToolTip="Groups must be terminated with }"/> 
        &nbsp;&nbsp;<asp:TextBox ID="User_Subgroup_Name_TextBox1" runat="server" Font-Size="XX-Small" Width="400px"></asp:TextBox>

        <br />

        <br />
        <asp:Label ID="PCTY_Divide_Label2" Style="display:inline" Font-Size="XX-Small" runat="server" 
           Text="--------------------------------------------" ></asp:Label>
       <br />
<!---  add august 24 -->
        <asp:Label ID="Load_PCTYLists_Label1" Style="display:inline" Font-Size="XX-Small" 
        runat="server" Text="Your Saved Lists" ></asp:Label>
        &nbsp;<asp:Button ID="Load_Saved_PCTYs_Button" runat="server" Font-Size="XX-Small" 
                Text="Load List" Width="70px" 
            onclick="Load_Saved_PCTYs_Click" 
            ToolTip="Load list of saved countries from the dropdown"/> 
        &nbsp;&nbsp;
        <asp:CheckBox ID="Append_PCTY_Load_CkBox" runat="server" Font-Size="XX-Small" 
                 Text="Append List" ToolTip="Append do not replace current selections" Checked="False" />
        <br />
        <asp:DropDownList ID="Load_User_PCTYList_DropDownList1" runat="server" Font-Size="XX-Small" 
        DataSourceID="SqlDataSource6"  DataTextField="LIST_NAME" 
            DataValueField="LIST_SEQ"
            AutoPostBack="True"  >
        </asp:DropDownList>
        &nbsp;
	<asp:Button ID="Delete_PCTY_List_Button" runat="server" Font-Size="XX-Small" 
	Text="Delete List" Width="70px" 
	onclick="Button_Delete_PCTY_List_Click" 
	ToolTip="Delete Your Saved List Shown;type delete in the box to verify"/> 
        <br />
        <asp:Label ID="User_PCTYSel_Name_Label1" Style="display:inline" Font-Size="XX-Small" 
        runat="server" Text="Delete a List: Type DELETE then Press Delete Button:" ></asp:Label>
        <asp:TextBox ID="PCTY_Verification_TextBox1" runat="server" Font-Size="XX-Small"  Width="70px" 
        ToolTip="Enter DELETE to verify you want to delete selected list"></asp:TextBox>
        <asp:Panel
     <br />
     <asp:Panel ID="PCTY_SaveNew_Panel" runat="server" Font-Size="Small" 
            style="margin-top: 0px; display:inline" BorderStyle="None" Width="790px" Visible="True">


        <asp:Label ID="User_PCTY_ListSeq_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
        <asp:Label ID="List_PCTY_NewSeq_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small" ></asp:Label>
        <br /><asp:Label ID="SubmitList_User_PCTY_Processing_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
        <asp:Button ID="Save_PCTYs_Button" runat="server" Font-Size="XX-Small" 
                Text="Save List As:" Width="65px" 
            onclick="Button_Save_PCTYs_Click" 
            ToolTip="Save Selections as the name you provide"/> 
        <asp:TextBox ID="User_PCTYsSel_Name_TextBox1" runat="server" Font-Size="XX-Small"  Width="300px" ></asp:TextBox>
        <br />
        <asp:CheckBox ID="Resave_PCTY_Load_CkBox" runat="server" Font-Size="XX-Small" 
	Text="Replace Lists with Matching Name" ToolTip="Delete list with matching name before saving new list" Checked="False" />
        <br />
     </asp:Panel> 
     <br /><asp:Label ID="Save_PCTYs_Status_Label" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Save List Not Loaded" Visible="True" ></asp:Label>
        <asp:Label ID="Pcty_Out_label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="" Visible="False" ></asp:Label>
        <br /><asp:Label ID="Out_label_pcty1" Style="display:inline" runat="server" Text="" Visible="True" ></asp:Label>




<!--- end aug 24 2022 --->

    </asp:Panel> <!--   end Selections_Panel2  -->
   
            <asp:SqlDataSource ID="SqlDataSource6" 
                runat="server" ConnectionString=
                "<%$ ConnectionStrings:TPISAZUS%>" 
                SelectCommand="SELECT LIST_SEQ
               , LIST_NAME 
               FROM dbo.TPIS_USER_LIST_HDR_PROD_NEW 
               where lower(TPIS_USER)= lower(@uid)
               and LIST_TYPE='COUNTRY'
               ORDER BY LIST_NAME">
               <SelectParameters>
                   <asp:ControlParameter ControlID="User_ID_Valid_Label1" 
                       DefaultValue="pomeror" Name="uid" PropertyName="Text" />
               </SelectParameters>

         </asp:SqlDataSource>
   
   
    </asp:Panel> <!--   end Selected_Partner_panel  -->

   </asp:Panel> <!--  end Partner_Panel -->

   </ContentTemplate>            
   </asp:UpdatePanel>      <!--   end PCTY_Ajax_Panel1  -->

         <asp:UpdatePanel ID="STATE_Ajax_Panel1" runat="server" updatemode="Conditional">
           <ContentTemplate>
<br />
<br />
        &nbsp;&nbsp;<asp:Button ID="Enable_District_Btn" runat="server" Font-Size="X-Small" 
                onclick="Enable_District_Btn_Click"  
                Text="Choose Customs Districts" Visible="True" 
                ToolTip="Opens Panel to Select U.S. Customs Districts"/>
        &nbsp;&nbsp;<asp:Button ID="Enable_SPIs_Btn" runat="server" Font-Size="X-Small" 
                onclick="Enable_SPI_Btn_Click"  
                Text="Choose Special Tariff Programs (SPIs)" Visible="True" 
                ToolTip="NOT YET AVAIALABE!! Opens Panel to Select Import Special Program Indicators/SPIs"/>
 

<a name="TPISSTATESETUP"></a>
<asp:Panel ID="State_Panel" runat="server" Font-Size="Small" Height="150px" 
            style="margin-top: 0px" BorderStyle="Groove" Width="790px" Visible="False">
            
            <asp:Label ID="State_Header_Label" runat="server" Text="Customs District Selection: "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
            &nbsp;
 	     <asp:RadioButton ID="STATE_TOTAL_ALL_CkBox" runat="server" Font-Size="Small" 
 		GroupName="STATE_DETAIL_CK" Text="All Districts in the USA"  Checked="True" 
 		OnCheckedChanged="STATE_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
 	     <asp:RadioButton ID="STATE_SELECTED_CkBox" runat="server" Font-Size="Small" 
 		GroupName="STATE_DETAIL_CK" Text="Selected Districts"  Checked="False" 
 		OnCheckedChanged="STATE_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
            <hr /> 
            <br />


            <asp:CheckBox ID="Display_STATETOTAL_chkbox" runat="server" Font-Size="Small" 
                Text="Total of Selected"  Checked="True" />
             <asp:CheckBox ID="Display_STATESUB_chkbox" runat="server" Font-Size="Small" 
                Text="Subgroups & Districts Shown"  Checked="False" />
             <br /><asp:CheckBox ID="Display_STATEDETAIL_chkbox" runat="server" Font-Size="Small" 
                Text="Show All Districts Selected"  Checked="False" />
             &nbsp;&nbsp;<asp:CheckBox ID="Display_STATEShare_chkbox" runat="server" Font-Size="X-Small" 
                Text="% of Total"  Checked="False" />
                
             <br /><asp:CheckBox ID="Display_STATE_TotalLabel_chkbox" runat="server" Font-Size="Small" 
                Text="Your label for Total"  Checked="False" />
            &nbsp;&nbsp;<asp:TextBox ID="STATE_Total_TextBox" runat="server" Visible="True" Font-Size="X-Small" 
                Width="50%" Text=""></asp:TextBox>
                <br /> <br />
<asp:Panel ID="Selected_STATE_Panel" runat="server" Font-Size="Small" Height="300px" 
            style="margin-top: 0px" BorderStyle="None" Width="780px" Visible="False">
    
<asp:Table HorizontalAlign="Left" Width="700" Style="display:block" runat="server">
<asp:TableRow>
<asp:TableCell>
        Source:&nbsp;&nbsp;
        <asp:DropDownList ID="State_Source_DropDownList1" runat="server" 
            DataSourceID="SqlDataSource3" DataTextField="SHARE_TYPE_NAME" 
            DataValueField="GROUP_TYPE" AutoPostBack="True"  >
        </asp:DropDownList>
        
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="State_Filler_Labelurlr2a" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="State_Filler_Labelurlr2b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
        <asp:DropDownList ID="StateGroup_DropDownList" runat="server" 
            DataSourceID="SqlDataSource4" DataTextField="GRP_NAME" 
            DataValueField="GRP_CODE" AutoPostBack="True" Font-Size="X-Small"
            >
        </asp:DropDownList>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="State_Filler_Labelurlr3a" runat="server" Text="  "  style="margin-bottom: 0px"  Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="State_Filler_Labelurlr3b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
</asp:TableRow>
</asp:Table>

<asp:Table HorizontalAlign="Left" Style="display:block" Width="500px" runat="server">
<asp:TableRow>
<asp:TableCell>
   <asp:Label ID="State_Select_From_List_Label1" runat="server" Font-Size="X-Small" Text="Select From List" Width="180px" ></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="State_Filler_Labela1" runat="server" Text="  "  style="margin-bottom: 0px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
   <asp:Label ID="State_Current_User_Selections_Label1a" runat="server" Font-Size="X-Small" Text="Current User Selections:" Width="180px" ></asp:Label>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
        <asp:ListBox ID="States_Pick_ListBox" runat="server" DataSourceID="SqlDataSource5" 
            DataTextField="STATE_DIST_TEXT" DataValueField="STATE_DIST_CODE" 
    SelectionMode="Multiple" style="margin-bottom: 0px" Height="190px" Width="230px"  Font-Size="Small" ></asp:ListBox>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="State_Filler_Label1" runat="server" Text="  "  style="margin-bottom: 0px" Height="190px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>

<asp:TableCell>
        <asp:TextBox ID="States_Selections_TextBox" runat="server" Height="190px" 
            style="margin-bottom: 0px" TextMode="MultiLine" Width="230px" Wrap="False" Font-Size="Small"
            ToolTip="1 state or group per line, use ! as 1st character as a NOT/do not include indicator.  TPIS Groups start with a . and your saved session lists with .L#"
            ></asp:TextBox>
        <br />
</asp:TableCell>
<asp:TableCell>
        <asp:ListBox ID="States_Selected_ListBox" runat="server" 
            style="margin-bottom: 0px; display:none" Height="190px" SelectionMode="Multiple" 
            Width="180px" Visible="False" >
        </asp:ListBox>
</asp:TableCell>
<asp:TableCell  Width="300px">
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="State_MoveSelected_Button" runat="server" 
                    onclick="State_MoveSelected_Button_Click" Text="Select &gt;" 
            Width="91px" Font-Size="X-Small" />
                <asp:Button ID="State_MoveNotSelected_Button" runat="server" Visible="True"
                    onclick="State_MoveNotSelected_Button_Click" Text="Select NOT &gt;" 
                    ToolTip="Copies Selected Items and Puts the NOT/EXCLUDE symbol ! in front"
            Width="91px" Font-Size="X-Small" />
            
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="State_NewList_Button" runat="server" onclick="NewList_Button_Click" 
            Text="New List" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
             <asp:Button ID="State_MoveAll_Button" runat="server" 
            Text="Select All &gt;&gt;" Width="90px" onclick="State_MoveAll_Button_Click" 
            Font-Size="X-Small" />
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>

        <asp:Button ID="RemoveAll_States_Button" runat="server" 
        onclick="RemoveAll_States_Button_Click" 
            Text="Clear Selections" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>


        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="
            select region_code as grp_code
            , statedesc as grp_name
            , case when region_code='.USA' then 0
              when region_code='.REGIONS' then 1
              else 2 end as code_order
            from dbo.state_usa_regions_ref
            where len(statecode)>2
            and region_code=statecode
            group by region_code, statedesc
            ,case when region_code='.USA' then 0
              when region_code='.REGIONS' then 1
              else 2 end
            order by code_order, grp_code
            ">
            </asp:SqlDataSource>


        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="SELECT STATE_DIST_CODE + ' - ' + STATE_DISTRICT_NAME AS state_dist_text
            , STATE_DIST_CODE AS state_dist_code
            , STATE_DISTRICT_NAME as state_dist_name
            , state_code
            , orderby_seq
            , case when len(state_code)>2 then 0
              else 1 end as state_order
            FROM dbo.ustrade_state_district_region_ref
            WHERE  (REGION_CODE = @g1)
            and charindex(@db+';',valid_dbase)>0
            group by state_dist_code, state_district_name
                      , state_code, orderby_seq
            ,case when len(state_dist_code)>2 then 0
              else 1 end
            ORDER BY state_code, state_dist_name">
            <SelectParameters>
                <asp:ControlParameter ControlID="StateGroup_DropDownList" 
                    DefaultValue=".USA" Name="g1" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="Current_DBID_Label1" 
                    DefaultValue="US" Name="db" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>





        <br /><asp:Label ID="Out_label_state1" Style="display:inline" runat="server" Text="" Visible="True" ></asp:Label>
   <asp:Panel ID="State_Selections_Panel2" Visible="True" Width="500px" Style="display:block;clear" runat="server">
        <br />
        <asp:br />
        <asp:br />

        <br /><asp:Label ID="State_Divide_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" 
           Text="--------------------------------------------" ></asp:Label>
       
        <br /><asp:Label ID="States_User_Subgroup_Name_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Name for Subgroup" ></asp:Label>
        &nbsp;<asp:Button ID="Create_StateGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="Create Subgroup" Width="120px" 
            onclick="Create_PctyGroup_Click" 
            ToolTip="Must Enter a Group Label in Text Box Below"/> 
        &nbsp;<asp:Button ID="End_StateGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="End Subgroup" Width="120px" 
            onclick="End_PctyGroup_Click" 
            ToolTip="Groups must be terminated with }"/> 
        &nbsp;&nbsp;<asp:TextBox ID="State_User_Subgroup_Name_TextBox1" runat="server" Font-Size="XX-Small" Width="400px"></asp:TextBox>
       
        <br />
       
       
         <br />

        <br />
        <asp:Label ID="STATE_Divide_Label2" Style="display:inline" Font-Size="XX-Small" runat="server" 
           Text="--------------------------------------------" ></asp:Label>
       <br />
<!---  add august 24 -->
        <asp:Label ID="Load_STATELists_Label1" Style="display:inline" Font-Size="XX-Small" 
        runat="server" Text="Your Saved Lists" ></asp:Label>
        &nbsp;<asp:Button ID="Load_Saved_STATEs_Button" runat="server" Font-Size="XX-Small" 
                Text="Load List" Width="70px" 
            onclick="Load_Saved_STATEs_Click" 
            ToolTip="Load list of saved countries from the dropdown"/> 
        &nbsp;&nbsp;
        <asp:CheckBox ID="Append_STATE_Load_CkBox" runat="server" Font-Size="XX-Small" 
                 Text="Append List" ToolTip="Append do not replace current selections" Checked="False" />
        <br />
        <asp:DropDownList ID="Load_User_STATEList_DropDownList1" runat="server" Font-Size="XX-Small" 
        DataSourceID="SqlDataSource9"  DataTextField="LIST_NAME" 
            DataValueField="LIST_SEQ"
            AutoPostBack="True"  >
        </asp:DropDownList>
        &nbsp;
	<asp:Button ID="Delete_STATE_List_Button" runat="server" Font-Size="XX-Small" 
	Text="Delete List" Width="70px" 
	onclick="Button_Delete_STATE_List_Click" 
	ToolTip="Delete Your Saved List Shown;type delete in the box to verify"/> 
        <br />
        <asp:Label ID="User_STATESel_Name_Label1" Style="display:inline" Font-Size="XX-Small" 
        runat="server" Text="Delete a List: Type DELETE then Press Delete Button:" ></asp:Label>
        <asp:TextBox ID="STATE_Verification_TextBox1" runat="server" Font-Size="XX-Small"  Width="70px" 
        ToolTip="Enter DELETE to verify you want to delete selected list"></asp:TextBox>
        <asp:Panel
     <br />
     <asp:Panel ID="STATE_SaveNew_Panel" runat="server" Font-Size="Small" 
            style="margin-top: 0px; display:inline" BorderStyle="None" Width="790px" Visible="True">


        <asp:Label ID="User_STATE_ListSeq_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
        <asp:Label ID="List_STATE_NewSeq_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small" ></asp:Label>
        <br /><asp:Label ID="SubmitList_User_STATE_Processing_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
        <asp:Button ID="Save_STATEs_Button" runat="server" Font-Size="XX-Small" 
                Text="Save List As:" Width="65px" 
            onclick="Button_Save_STATEs_Click" 
            ToolTip="Save Selections as the name you provide"/> 
        <asp:TextBox ID="STATE_User_Sel_Name_TextBox1" runat="server" Font-Size="XX-Small"  Width="300px" ></asp:TextBox>
        <br />
        <asp:CheckBox ID="Resave_STATE_Load_CkBox" runat="server" Font-Size="XX-Small" 
	Text="Replace Lists with Matching Name" ToolTip="Delete list with matching name before saving new list" Checked="False" />
        <br />
     </asp:Panel> 
     <br /><asp:Label ID="Save_STATEs_Status_Label" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Save List Not Loaded" Visible="True" ></asp:Label>
        <asp:Label ID="STATE_Out_label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="" Visible="False" ></asp:Label>

            <asp:SqlDataSource ID="SqlDataSource9" 
                runat="server" ConnectionString=
                "<%$ ConnectionStrings:TPISAZUS%>" 
                SelectCommand="SELECT LIST_SEQ
               , LIST_NAME 
               FROM dbo.TPIS_USER_LIST_HDR_PROD_NEW 
               where lower(TPIS_USER)= lower(@uid)
               and LIST_TYPE in ('STATE','DIST') and upper(LIST_TYPE)=upper(@ltyp)
               ORDER BY LIST_NAME">
               <SelectParameters>
                   <asp:ControlParameter ControlID="User_ID_Valid_Label1" 
                       DefaultValue="pomeror" Name="uid" PropertyName="Text" />
                   <asp:ControlParameter ControlID="SubmitList_User_STATE_Processing_Label1" 
                       DefaultValue="STATE" Name="ltyp" PropertyName="Text" />
                       
               </SelectParameters>

         </asp:SqlDataSource>
   
   
        

    </asp:Panel>        <!--   end State_Selections_Panel2  -->


</asp:Panel> <!-- end Selected_STATE_Panel -->
</asp:Panel> <!-- end State_Panel -->
   </ContentTemplate>            
   </asp:UpdatePanel> <!-- end STATE_Ajax_Panel1 -->

<!-- end states panel -->

<!-- Start SPI Panel -->


<asp:UpdatePanel ID="SPIs_Update_Panel" runat="server" updatemode="Conditional">

<ContentTemplate>



<a name="TPISSPISETUP"></a>
<asp:Panel ID="SPI_Panel" runat="server" Font-Size="Small" Height="150px" 
            style="margin-top: 0px" BorderStyle="Groove" Width="790px" Visible="False">
            
            <asp:Label ID="SPI_Header_Label" runat="server" Text="Tariif Program (SPIs) Selection: "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
            &nbsp;
 	     <asp:RadioButton ID="SPI_TOTAL_ALL_CkBox" runat="server" Font-Size="Small" 
 		GroupName="SPI_DETAIL_CK" Text="All Tariff Programs"  Checked="True" 
 		OnCheckedChanged="SPI_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
 	     <asp:RadioButton ID="SPI_SELECTED_CkBox" runat="server" Font-Size="Small" 
 		GroupName="SPI_DETAIL_CK" Text="Show Selected Tariff Programs"  Checked="False" 
 		OnCheckedChanged="SPI_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
            <hr /> 
            <br />


            <asp:CheckBox ID="Display_SPITOTAL_chkbox" runat="server" Font-Size="Small" 
                Text="Total for Programs Selected"  Checked="True" />
             <asp:CheckBox ID="Display_SPISUB_chkbox" runat="server" Font-Size="Small" 
                Text="Subgroups & Programs Shown"  Checked="False" />
             <br /><asp:CheckBox ID="Display_SPIDETAIL_chkbox" runat="server" Font-Size="Small" 
                Text="Show Each Tariff Programs Selected"  Checked="False" />
             &nbsp;&nbsp;<asp:CheckBox ID="Display_SPIShare_chkbox" runat="server" Font-Size="X-Small" 
                Text="% of Total"  Checked="False" />
                
             <br /><asp:CheckBox ID="Display_SPI_TotalLabel_chkbox" runat="server" Font-Size="Small" 
                Text="Your label for Total"  Checked="False" />
            &nbsp;&nbsp;<asp:TextBox ID="SPI_Total_TextBox" runat="server" Visible="True" Font-Size="X-Small" 
                Width="50%" Text=""></asp:TextBox>
                <br /> <br />
<asp:Panel ID="Selected_SPI_Panel" runat="server" Font-Size="Small" Height="300px" 
            style="margin-top: 0px" BorderStyle="None" Width="780px" Visible="False">
    
<asp:Table HorizontalAlign="Left" Width="700" Style="display:block" runat="server">
<asp:TableRow>
<asp:TableCell>
        Source:&nbsp;&nbsp;
        <!-- 
        <asp:DropDownList ID="SPI_Source_DropDownList1" runat="server" 
            DataSourceID="SqlDataSource37" DataTextField="SPI_TEXT" 
            DataValueField="SPI_CODE" AutoPostBack="True"  >
        </asp:DropDownList>
         -->
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="SPI_Filler_Labelurlr2a" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="SPI_Filler_Labelurlr2b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
</asp:TableRow>

<asp:TableRow>
<asp:TableCell>
        <asp:Label ID="SPI_Filler_Labelurlr3adrpdwn" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="SPI_Filler_Labelurlr3a" runat="server" Text="  "  style="margin-bottom: 0px"  Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="SPI_Filler_Labelurlr3b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
</asp:TableRow>
</asp:Table>

<asp:Table HorizontalAlign="Left" Style="display:block" Width="500px" runat="server">
<asp:TableRow>
<asp:TableCell>
   <asp:Label ID="SPI_Select_From_List_Label1" runat="server" Font-Size="X-Small" Text="Select From List" Width="320px" ></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="SPI_Filler_Labela1" runat="server" Text="  "  style="margin-bottom: 0px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
   <asp:Label ID="SPI_Current_User_Selections_Label1a" runat="server" Font-Size="X-Small" Text="Current User Selections:" Width="320px" ></asp:Label>
</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
        <asp:ListBox ID="SPIs_Pick_ListBox" runat="server" DataSourceID="SqlDataSource37" 
            DataTextField="SPI_TEXT" DataValueField="SPI_CODE" 
    SelectionMode="Multiple" style="margin-bottom: 0px" Height="190px" Width="320px"  Font-Size="X-Small" ></asp:ListBox>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="SPI_Filler_Label1" runat="server" Text="  "  style="margin-bottom: 0px" Height="190px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>

<asp:TableCell>
        <asp:TextBox ID="SPIs_Selections_TextBox" runat="server" Height="190px" 
            style="margin-bottom: 0px" TextMode="MultiLine" Width="320px" Wrap="False" Font-Size="Small"
            ToolTip="1 spi or group per line, use ! as 1st character as a NOT/do not include indicator.  TPIS Groups start with a . and your saved session lists with .L#"
            ></asp:TextBox>
        <br />
</asp:TableCell>
<asp:TableCell>
        <asp:ListBox ID="SPIs_Selected_ListBox" runat="server" 
            style="margin-bottom: 0px; display:none" Height="320px" SelectionMode="Multiple" 
            Width="180px" Visible="False" >
        </asp:ListBox>
</asp:TableCell>
<asp:TableCell  Width="320px">
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="SPI_MoveSelected_Button" runat="server" 
                    onclick="SPI_MoveSelected_Button_Click" Text="Select &gt;" 
            Width="91px" Font-Size="X-Small" />
                <asp:Button ID="SPI_MoveNotSelected_Button" runat="server" Visible="True"
                    onclick="SPI_MoveNotSelected_Button_Click" Text="Select NOT &gt;" 
                    ToolTip="Copies Selected Items and Puts the NOT/EXCLUDE symbol ! in front"
            Width="91px" Font-Size="X-Small" />
            
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="SPI_NewList_Button" runat="server" onclick="SPI_NewList_Button_Click" 
            Text="New List" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
             <asp:Button ID="SPI_MoveAll_Button" runat="server" 
            Text="Select All &gt;&gt;" Width="90px" onclick="SPI_MoveAll_Button_Click" 
            Font-Size="X-Small" />
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>

        <asp:Button ID="RemoveAll_SPIs_Button" runat="server" 
        onclick="RemoveAll_SPIs_Button_Click" 
            Text="Clear Selections" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>
</asp:Table>


        <asp:SqlDataSource ID="SqlDataSource37" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="SELECT TPIS_SPI_SEQ AS SPI_CODE
            , FORMAT(TPIS_SPI_SEQ,'0')+' - ['+UPPER(SPI_CODE)+' used '+year_range+']: '+SPI_DESCRIPTION AS SPI_TEXT
            FROM DBO.TPIS_CSC_SPI_REF_MDS
            ORDER BY order_items, SPI_CODE ;
            ">
       </asp:SqlDataSource>

        <br /><asp:Label ID="Out_label_spi1" Style="display:inline" runat="server" Text="" Visible="True" ></asp:Label>
   <asp:Panel ID="SPI_Selections_Panel2" Visible="False" Width="500px" Style="display:block;clear" runat="server">
        <br />
        <asp:br />
        <asp:br />

        <br /><asp:Label ID="SPI_Divide_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" 
           Text="--------------------------------------------" ></asp:Label>
       
        <br /><asp:Label ID="SPIs_User_Subgroup_Name_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Name for Subgroup" ></asp:Label>
        &nbsp;<asp:Button ID="Create_SPIGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="Create Subgroup" Width="120px" 
            onclick="Create_PctyGroup_Click" 
            ToolTip="Must Enter a Group Label in Text Box Below"/> 
        &nbsp;<asp:Button ID="End_SPIGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="End Subgroup" Width="120px" 
            onclick="End_PctyGroup_Click" 
            ToolTip="Groups must be terminated with }"/> 
        &nbsp;&nbsp;<asp:TextBox ID="SPI_User_Subgroup_Name_TextBox1" runat="server" Font-Size="XX-Small" Width="400px"></asp:TextBox>

        <br />
       
       
       
        <br /><asp:Label ID="SPI_User_Sel_Name_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Name for User Selections List:" ></asp:Label>
        <br /><asp:TextBox ID="SPIs_User_Sel_Name_TextBox1" runat="server" Font-Size="XX-Small" Width="400px"></asp:TextBox>
        <br />

       
       <br />
        <asp:Label ID="SPIs_User_Restrict_Name_Label1" runat="server" Text="Describe SPI Selection Constraints:" Visible="False"></asp:Label>
        <br />
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="SPIs_TextBox1" runat="server" Height="50px" TextMode="MultiLine" Width="400px" Visible="False"></asp:TextBox>
        <br /><asp:Label ID="SPI_Save_Status_Label" Style="display:inline" runat="server" Text="NOT SAVED" Visible="True" ></asp:Label>
        <br /><asp:Label ID="SPI_Out_label_1" Style="display:inline" runat="server" Text="" Visible="True" ></asp:Label>


        

    </asp:Panel>        <!--   end SPI_Selections_Panel2  -->


</asp:Panel> <!-- end Selected_SPI_Panel -->
</asp:Panel> <!-- end SPI_Panel -->
   </ContentTemplate>            
   </asp:UpdatePanel> <!-- end SPI_Ajax_Panel1 -->


   </ContentTemplate>            
   </asp:UpdatePanel> <!-- end SPIs_Update_Panel -->


<!-- End SPI Panel  -->


<!-- start reporter panel -->
<asp:UpdatePanel ID="Reporter_Update_Panel" runat="server" updatemode="Conditional">

<ContentTemplate>

   <asp:Panel ID="Reporter_Panel" runat="server" Font-Size="Small" Height="200px" 
            style="margin-top: 0px" BorderStyle="Groove" Width="790px" Visible="False">
            
            <asp:p Font-Size="Small" >Reporter Country Selection: <asp:/p>
 	     <asp:RadioButton ID="RCTY_TOTAL_ALL_CkBox" runat="server" Font-Size="Small" 
 		GroupName="RCTY_DETAIL_CK" Text="World (All Countries)"  Checked="True" 
 		OnCheckedChanged="RCTY_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
 	     <asp:RadioButton ID="RCTY_SELECTED_CkBox" runat="server" Font-Size="Small" 
 		GroupName="RCTY_DETAIL_CK" Text="Selected Countries"  Checked="False" 
 		OnCheckedChanged="RCTY_DETAIL_ckbox_changed"
 		AutoPostBack="True"/>
            <hr /> 
            <asp:CheckBox ID="Display_RCTYTOTAL_chkbox" runat="server" Font-Size="Small" 
                Text="Total of Selected"  Checked="True" />
             <asp:CheckBox ID="Display_RCTYSUB_chkbox" runat="server" Font-Size="Small" 
                Text="Subgroups & Reporters Shown"  Checked="False" />
             <br /><asp:CheckBox ID="Display_RCTYDETAIL_chkbox" runat="server" Font-Size="Small" 
                Text="Show All Reporters Selected"  Checked="False" />
             &nbsp;&nbsp;<asp:CheckBox ID="Display_RCTYShare_chkbox" runat="server" Font-Size="X-Small" 
                Text="% of Total"  Checked="False" />
                


<asp:Panel ID="Selected_Reporter_Panel" runat="server" Font-Size="Small" Height="500px" 
            style="margin-top: 0px" BorderStyle="None" Width="780px" Visible="False">
            
             <br /><asp:CheckBox ID="Display_RCTY_TotalLabel_chkbox" runat="server" Font-Size="Small" 
                Text="Your label for Total"  Checked="False" />
            &nbsp;&nbsp;<asp:TextBox ID="RCTY_Total_TextBox" runat="server" Visible="True" Font-Size="X-Small" 
                Width="50%" Text=""></asp:TextBox>
                <br /> <br />

<asp:Table HorizontalAlign="Left" Width="700" Style="display:block" runat="server">
<asp:TableRow>
<asp:TableCell>
        Source:&nbsp;&nbsp;
        <asp:DropDownList ID="RCTY_Source_DropDownList1" runat="server" 
            DataSourceID="SqlDataSource3" DataTextField="SHARE_TYPE_NAME" 
            DataValueField="GROUP_TYPE" AutoPostBack="True"  >
        </asp:DropDownList>
        
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_RCTYLabelurlr2a" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_RCTYLabelurlr2b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
        <asp:DropDownList ID="RCTY_CountryGroup_DropDownList" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="GRP_NAME" 
            DataValueField="GRP_CODE" AutoPostBack="True" Font-Size="X-Small"
            OnSelectedIndexChanged="RCTY_CountryGroup_DropDownList_Change"
            >
        </asp:DropDownList>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_RCTY_Labelurlr3a" runat="server" Text="  "  style="margin-bottom: 0px"  Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_RCTY_Labelurlr3b" runat="server" Text="  "  style="margin-bottom: 0px" Font-Size="Small"></asp:Label>
</asp:TableCell>
</asp:TableRow>
</asp:Table>
        <br />

<asp:Table HorizontalAlign="Left" Style="display:block" Width="500px" runat="server">
<asp:TableRow>
<asp:TableCell>
   <asp:Label ID="RCTY_Select_From_List_Label1" runat="server" Font-Size="X-Small" Text="Select From List" Width="180px" ></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_RCTY_Labela1" runat="server" Text="  "  style="margin-bottom: 0px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
   <asp:Label ID="RCTY_Current_User_Selections_Label1" runat="server" Font-Size="X-Small" Text="Current User Selections:" Width="180px" ></asp:Label>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
        <asp:ListBox ID="RCTY_Pick_ListBox" runat="server" DataSourceID="SqlDataSource12" 
            DataTextField="CTY_CODE" DataValueField="CTY_CODE" 
    SelectionMode="Multiple" style="margin-bottom: 0px" Height="190px" Width="230px"  Font-Size="Small" ></asp:ListBox>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_RCTY_Label1" runat="server" Text="  "  style="margin-bottom: 0px" Height="190px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:TextBox ID="RCTY_Selections_TextBox" runat="server" Height="190px" 
            style="margin-bottom: 0px" TextMode="MultiLine" Width="230px" Wrap="False" Font-Size="Small"
            ToolTip="1 country or group per line, use ! as 1st character as a NOT/do not include indicator.  TPIS Groups start with a . and your saved session lists with .L#"
            ></asp:TextBox>
        <br />
</asp:TableCell>
<asp:TableCell>
        <asp:ListBox ID="RCTY_Selected_ListBox" runat="server" 
            style="margin-bottom: 0px; display:none" Height="190px" SelectionMode="Multiple" 
            Width="180px" Visible="False" >
        </asp:ListBox>
</asp:TableCell>
<asp:TableCell  Width="300px">

</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="RCTY_MoveSelected_Button" runat="server" 
                    onclick="RCTY_MoveSelected_Button_Click" Text="Select &gt;" 
            Width="91px" Font-Size="X-Small" />
                <asp:Button ID="RCTY_MoveNotSelected_Button" runat="server" Visible="True"
                    onclick="RCTY_MoveNotSelected_Button_Click" Text="Select NOT &gt;" 
                    ToolTip="Copies Selected Items and Puts the NOT/EXCLUDE symbol ! in front"
            Width="91px" Font-Size="X-Small" />
            
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="RCTY_NewList_Button" runat="server" onclick="RCTY_NewList_Button_Click" 
            Text="New List" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="RCTY_MoveAll_Button" runat="server" 
            Text="Select All &gt;&gt;" Width="90px" onclick="RCTY_MoveAll_Button_Click" 
            Font-Size="X-Small" />
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="RCTY_RemoveAll_Button" runat="server" onclick="RCTY_RemoveAll_Button_Click" 
            Text="Clear Selections" Width="119px" Font-Size="X-Small" />
</asp:TableCell>
</asp:TableRow>



</asp:Table>



   <asp:Panel ID="RCTY_Selections_Panel2" Visible="True" Width="500px" Style="display:block;clear" runat="server">
        <br />
        <asp:br />
        <asp:br />

        <br /><asp:Label ID="Divide_RCTY_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" 
           Text="--------------------------------------------" ></asp:Label>
       
        <br /><asp:Label ID="RCTY_User_Subgroup_Name_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Name for Subgroup" ></asp:Label>
        &nbsp;<asp:Button ID="Create_RctyGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="Create Subgroup" Width="120px" 
            onclick="Create_RctyGroup_Click" 
            ToolTip="Must Enter a Group Label in Text Box Below"/> 
        &nbsp;<asp:Button ID="End_RctyGroup_Button" runat="server" Font-Size="XX-Small" 
                Text="End Subgroup" Width="120px" 
            onclick="End_RctyGroup_Click" 
            ToolTip="Groups must be terminated with }"/> 
        &nbsp;&nbsp;<asp:TextBox ID="RCTY_User_Subgroup_Name_TextBox1" runat="server" Font-Size="XX-Small" Width="400px"></asp:TextBox>

        <br />
       
       
         <br />

        <br />
        <asp:Label ID="RCTY_Divide_Label2" Style="display:inline" Font-Size="XX-Small" runat="server" 
           Text="--------------------------------------------" ></asp:Label>
       <br />
<!---  add august 24 -->
        <asp:Label ID="Load_RCTYLists_Label1" Style="display:inline" Font-Size="XX-Small" 
        runat="server" Text="Your Saved Lists" ></asp:Label>
        &nbsp;<asp:Button ID="Load_Saved_RCTYs_Button" runat="server" Font-Size="XX-Small" 
                Text="Load List" Width="70px" 
            onclick="Load_Saved_RCTYs_Click" 
            ToolTip="Load list of saved countries from the dropdown"/> 
        &nbsp;&nbsp;
        <asp:CheckBox ID="Append_RCTY_Load_CkBox" runat="server" Font-Size="XX-Small" 
                 Text="Append List" ToolTip="Append do not replace current selections" Checked="False" />
        <br />
        <asp:DropDownList ID="Load_User_RCTYList_DropDownList1" runat="server" Font-Size="XX-Small" 
        DataSourceID="SqlDataSource6"  DataTextField="LIST_NAME" 
            DataValueField="LIST_SEQ"
            AutoPostBack="True"  >
        </asp:DropDownList>
        &nbsp;
	<asp:Button ID="Delete_RCTY_List_Button" runat="server" Font-Size="XX-Small" 
	Text="Delete List" Width="70px" 
	onclick="Button_Delete_RCTY_List_Click" 
	ToolTip="Delete Your Saved List Shown;type delete in the box to verify"/> 
        <br />
        <asp:Label ID="User_RCTYSel_Name_Label1" Style="display:inline" Font-Size="XX-Small" 
        runat="server" Text="Delete a List: Type DELETE then Press Delete Button:" ></asp:Label>
        <asp:TextBox ID="RCTY_Verification_TextBox1" runat="server" Font-Size="XX-Small"  Width="70px" 
        ToolTip="Enter DELETE to verify you want to delete selected list"></asp:TextBox>
        <asp:Panel
     <br />
     <asp:Panel ID="RCTY_SaveNew_Panel" runat="server" Font-Size="Small" 
            style="margin-top: 0px; display:inline" BorderStyle="None" Width="790px" Visible="True">


        <asp:Label ID="User_RCTY_ListSeq_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
        <asp:Label ID="List_RCTY_NewSeq_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small" ></asp:Label>
        <br /><asp:Label ID="SubmitList_User_RCTY_Processing_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
        <asp:Button ID="Save_RCTYs_Button" runat="server" Font-Size="XX-Small" 
                Text="Save List As:" Width="65px" 
            onclick="Button_Save_RCTYs_Click" 
            ToolTip="Save Selections as the name you provide"/> 
        <asp:TextBox ID="RCTY_User_Sel_Name_TextBox1" runat="server" Font-Size="XX-Small"  Width="300px" ></asp:TextBox>
        <br />
        <asp:CheckBox ID="Resave_RCTY_Load_CkBox" runat="server" Font-Size="XX-Small" 
	Text="Replace Lists with Matching Name" ToolTip="Delete list with matching name before saving new list" Checked="False" />
        <br />
     </asp:Panel> 
     <br /><asp:Label ID="Save_RCTYs_Status_Label" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Save List Not Loaded" Visible="True" ></asp:Label>
        <asp:Label ID="RCTY_Out_label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="" Visible="False" ></asp:Label>
        <br /><asp:Label ID="Out_label_RCTY1" Style="display:inline" runat="server" Text="" Visible="True" ></asp:Label>



        

    </asp:Panel>   <!--   end RCTY_Selections_Panel2  -->
    
    </asp:Panel>  <!--   end Selected_Reporter_Panel  -->

   </asp:Panel>  <!-- end Reporter_Panel -->
</ContentTemplate>
</asp:UpdatePanel>  <!--  end Reporter_Update_Panel  -->
<!-- 
 -->
   </ContentTemplate>            
   </asp:UpdatePanel>

</asp:Panel> 


</ContentTemplate>
</asp:UpdatePanel> <!--   close main update panel  -->



        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="SELECT GROUP_SEQ AS grp_code, GROUP_LABEL AS grp_name 
            , group_order
            FROM dbo.V_TPIS_GUI_CTYSEL_GROUPS 
            group by group_seq, group_label, group_order
            ORDER BY GROUP_ORDER, GROUP_LABEL">
            </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="SELECT CTY_NAME AS cty_text
            , CTY_NAME AS cty_code 
            FROM dbo.v_tpis_gui_cty_groups_roger
            WHERE  (GROUP_seq = @g1)
            ORDER BY cty_text">
            <SelectParameters>
                <asp:ControlParameter ControlID="CountryGroup_DropDownList" 
                    DefaultValue="401" Name="g1" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>


        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="SELECT share_type_id, group_type, share_type_name 
            FROM dbo.tpis_shared_types WHERE (share_category = 'COUNTRY') 
            AND GROUP_TYPE!='NOTSET'
            AND (share_type_id BETWEEN 100 AND 102 ) 
            ORDER BY order_id">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select year_lbl as year_lbl, year_code  as year_code
            , year_order
            from dbo.V_dyn_ctysel_years
            where series_type =upper(@d1) 
            and year_code between ann_start_yr and ann_end_yr
            group by year_lbl, year_code, year_order
            order by year_order desc
           ">
            <SelectParameters>
                <asp:ControlParameter ControlID="Database_Type_Dropdownlist" 
                    DefaultValue="US" Name="d1" PropertyName="SelectedValue" />
            </SelectParameters>
             </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="SELECT CTY_NAME AS cty_text
            , CTY_NAME AS cty_code 
            FROM dbo.v_tpis_gui_cty_groups_roger
            WHERE  (GROUP_seq = @g1)
            ORDER BY cty_text">
            <SelectParameters>
                <asp:ControlParameter ControlID="RCTY_CountryGroup_DropDownList" 
                    DefaultValue="401" Name="g1" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>


     <asp:SqlDataSource ID="SqlDataSource21" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select * 
            from dbo.v_tpis_dyn_prodlist_1_NEW
            where source_code=replace(upper(@dsrc1),'USST:','US:') and flow in ('XM',@flw1)
            order by display_order, product_code">
        <SelectParameters>
            <asp:ControlParameter ControlID="DataSource_DropDownList1" 
                DefaultValue="US:NAICS" Name="dsrc1" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="Flow_Textbox1" 
                DefaultValue="M" Name="flw1" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

        <br />
        <asp:SqlDataSource ID="SqlDataSource23" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select iso_3ch as CTY_CODE, ISO_CTY_NAME as Country 
            from dbo.hscen_iso_imf_cty_map
            where iso_3ch is not null and tpis_pcty_seq between 1 and 399
            order by Country 
            ">
            </asp:SqlDataSource>


        <asp:SqlDataSource ID="SqlDataSource24" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
        SelectCommand="select 'NONE' as Product_code, 
        'Not Selected' as PROD_CODENDESC, 
        0 as display_order, 'NONE' as child_group 
        union all
        select Product_code, PROD_CODENDESC
        , display_order, child_group 
        from dbo.v_tpis_dyn_prodlist_all_NEW
        where source_code=replace(upper(@dsrc1),'USST:','US:') and digits<5 
        and flow in ('XM',@flw1)
        and ((product_code like @prodcode1 + '%' and charindex(substring(@prodcode1,1,1),'0123456789')>0
        and child_group like '.TOTAL%')
        or (parent_group like @prodcode1 + '%' and product_code != @prodcode1)
        and child_group like parent_group)
        order by display_order, product_code">
        <SelectParameters>
            <asp:ControlParameter ControlID="DataSource_DropDownList1" 
                DefaultValue="US:NAICS" Name="dsrc1" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="Product_DropDownList1" 
                DefaultValue=".TOTAL" Name="prodcode1" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="Flow_Textbox1" 
                DefaultValue="M" Name="flw1" PropertyName="Text" />
        </SelectParameters>
       </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource25" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select 'NONE' as Product_code, 
        'Not Selected' as PROD_CODENDESC
        , 0 as display_order, 'NONE' as child_group 
        union all
        select Product_code, PROD_CODENDESC
        , display_order, child_group 
        from dbo.v_tpis_dyn_prodlist_all_NEW
        where source_code=replace(upper(@dsrc1),'USST:','US:') and flow in ('XM',@flw1)
        and ((product_code like @prodcode1 + '%' and charindex(substring(@prodcode1,1,1),'0123456789')>0
        and child_group like '.TOTAL%')
        or (parent_group like @prodcode1 + '%' and product_code != @prodcode1)
        and child_group like parent_group)
        order by display_order, product_code">
        <SelectParameters>
            <asp:ControlParameter ControlID="DataSource_DropDownList1" 
                DefaultValue="US:NAICS" Name="dsrc1" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="Product_Detail1_DropDownList1" 
                DefaultValue=".TOTAL" Name="prodcode1" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="Flow_Textbox1" 
                DefaultValue="M" Name="flw1" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource29" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="SELECT case 
               when PRODUCT_LBL is null then PRODUCT
               when digits>0 then PRODUCT + '--' + PRODUCT_LBL
               else PRODUCT_LBL end  AS PRODUCT_NAME
            , PRODUCT AS PRODUCT_CODE
            FROM dbo.V_ALLCONC_PRODUCT_LOOKUP
            WHERE  HS10_FLAG=0 and CONC = replace(upper(@conc),'USST:','US:') AND FLOW IN ('XM', @flow)
            and digits=@digits  
            and (upper(@cmd) in ('.TOTAL','TOTAL','ALL') or
            substring(PRODUCT, 1, len(@cmd))=@cmd
            or product in (
            select product from dbo.V_TPIS_CMD_GROUPS
            where conc=@conc and cmd_group=upper(@cmd) group by product
            ) 
            ) 
            UNION ALL
            SELECT PRODUCT_LBL AS PRODUCT_NAME
            , HS10_CODE AS PRODUCT_CODE
            FROM DBO.V_HS_CONC_10DIGIT_XREF
            WHERE HS10_FLAG=1 and HS10_CODE is not null
            and len(product_lbl)>1
            AND  CONC = @conc AND FLOW IN ('XM', @flow)
            and digits=@DIGITS
            AND PRODUCT_XREF LIKE @CMD+'%'
            ORDER BY Product_code">
            <SelectParameters>
                <asp:ControlParameter ControlID="DataSource_DropDownList1" 
                    DefaultValue="US:NAICS" Name="conc" PropertyName="SelectedValue" />
                 <asp:ControlParameter ControlID="Flow_Textbox1" 
                    DefaultValue="X" Name="flow" PropertyName="Text" />
                 <asp:ControlParameter ControlID="DIGITS_ID_DropDown1" 
                    DefaultValue="2" Name="digits" PropertyName="SelectedValue" />
                 <asp:ControlParameter ControlID="Series_TextBox" 
                    DefaultValue="ALL" Name="cmd" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>


        <asp:SqlDataSource ID="SqlDataSource30" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select year_lbl as year_lbl, year_code  as year_code
            from dbo.v_dyn_ctysel_years
            where series_type=UPPER(@dsrc1) and 
            (series_type='USTB' 
            or year_code between ann_start_yr and YTD_end_yr)            
            order by year_order desc
            ">
            <SelectParameters>
                <asp:ControlParameter ControlID="Database_Type_Dropdownlist" 
                DefaultValue="ANY" Name="dsrc1" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource32" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select upper(user_id) as User_ID,
            User_id + ': '+User_Name as User_Name
            from dbo.TPIS_USER_IDS 
            where upper(location) !='IBRC'
            ORDER BY case when upper(user_id) like 'POMEROR' then 1
            else 2 end, upper(User_Name)">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource34" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select Parm_Name, Parm_ID
            from dbo.TPIS_WebPage_Valid_Parms
            where upper(PARM_ID) in ('RCTY','CMD','PCTY','STATE')
            and (
            upper(Valid_Sources) ='ALL' 
            or charindex(upper(@SRCE_ID+';'),upper(VALID_SOURCES))>0
            )
            ORDER BY Parm_Order asc">
            <SelectParameters>
                <asp:ControlParameter ControlID="Database_Type_Dropdownlist" 
                    DefaultValue="US" Name="SRCE_ID" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource35" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select Parm_name
            , Parm_Value
            from dbo.TPIS_WebPage_Valid_Parms
            where upper(PARM_ID) ='TOP'
            and (
            upper(VALID_SOURCES)='ALL' 
            or charindex(upper(@PRM_ID), upper(VALID_SOURCES))>0
            )
            ORDER BY Parm_Order asc">
            <SelectParameters>
                <asp:ControlParameter ControlID="Rank_Parm_DropDownList1" 
                    DefaultValue="PCTY" Name="PRM_ID" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        
        <br />
     </div>
     <br />
     <div>
     

<asp:UpdatePanel ID="Run_Job_Update_Panel" runat="server" updatemode="Conditional">
<ContentTemplate>

<a name="TPISDataTable"></a>

<asp:Panel ID="Run_Job_Panel" runat="server" Font-Size="Small" >

        
        <br />
        <asp:Label ID="SubmitJob_User_Processing" Style="display:inline" Font-Size="XX-Small" runat="server" Text="User ID: .." Visible="False" ></asp:Label>
        &nbsp;<asp:Label ID="SubmitJob_User_ID_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="ANONYMOUS" Visible="False"></asp:Label>
        <asp:Label ID="SubmitJob_User_Email_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="; Email: ??" Visible="False" ></asp:Label>
        <br />
        <asp:CheckBox ID="Rank_CheckBox1" Checked="False" runat="server" 
            Font-Size="Small" Text="Rank" />
 	     &nbsp;<asp:DropDownList ID="Rank_Parm_DropDownList1" runat="server" 
                 DataSourceID="SqlDataSource34" DataTextField="Parm_Name" 
                 DataValueField="Parm_Id" Font-Size="X-Small"  >
             </asp:DropDownList>
 	     &nbsp;by&nbsp;<asp:DropDownList ID="Rank_Year_DropDownList1" runat="server" 
                 DataSourceID="SqlDataSource8" DataTextField="Year_Lbl" 
                 DataValueField="Year_Code" Font-Size="X-Small"  >
             </asp:DropDownList>
        &nbsp;Value and Show &nbsp;Top : &nbsp;<asp:TextBox ID="UserRank_TextBox1" runat="server" 
            Visible="True">5</asp:TextBox> &nbsp;Records

 	     &nbsp;<asp:DropDownList ID="Rank_Top_DropDownList1" runat="server" 
                 DataSourceID="SqlDataSource35" DataTextField="Parm_Name" 
                 Visible="False"
                 DataValueField="Parm_Value" Font-Size="X-Small"  >
             </asp:DropDownList>
        <asp:CheckBox ID="UserRank_CheckBox1" Checked="False" 
            runat="server" Font-Size="Small" runat="server" 
            Visible="False"
            Text="Top " ></asp:CheckBox>


            
        <br />
        &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="Show_Debug_CheckBox1" Checked="False" runat="server" 
            Font-Size="Small" Text="Show Debug Info" Visible="False" />

        <br />
        <br /><asp:Label ID="Job_Name_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Job Name (Defaults to Data Source Name + Time Submitted)" ></asp:Label>
        <br /><asp:TextBox ID="Job_Name_TextBox1" Text="USHS" runat="server" Font-Size="XX-Small" Width="400px"></asp:TextBox>
        <br />

        <asp:CheckBox ID="New_JobNum_chkbox" runat="server" Font-Size="Small" 
          Text="Create New Job Number for Each Job"  Checked="True" Visible="false" />&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="User_JobNum_TextBox1" runat="server" Visible="False">111001</asp:TextBox>
            
         
        <br /><asp:Label ID="Job_Number_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Job#" Visible="False"></asp:Label>
              <asp:Label ID="Last_JobNum_Label1" Style="display:inline" Font-Size="XX-Small" runat="server" Text="Job#" Visible="False"></asp:Label>
        <br />
            <asp:Button ID="Select_Load_HTML_Btn" runat="server" Font-Size="X-Small" 
                onclick="Load_HTML_Btn_Click" Text="Show Results" Visible="False" 
            ToolTip="Show Results Sends Form Data to Retrieve Output"
                />
            <asp:Button ID="Insert_Webform_Parms_Btn" runat="server" Font-Size="Small" 
                onclick="Insert_Webform_Data_Btn_Click" Text="Run Form" Visible="False" 
                ToolTip="Run Form Sends Form Data to the System but doesn't retrieve data"/>
            <asp:Button ID="Run_Job_Async_Btn" runat="server" Font-Size="Small" 
                onclick="Run_Job_Async_Btn_Click" Text="Run Job (submits your data request)" Visible="True" 
                OnClientClick="this.disabled=true;" UseSubmitBehavior="false"
                ToolTip="Submits a Request (Job) Retrieve data. Click Display HTML or the Job Table to View Output When Completed"/>

            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Show_Retrieved_HTML_Btn" runat="server" Font-Size="Small" 
                onclick="Show_Retrieved_HTML_Btn_Click" Text="Display HTML Table" Visible="True" 
                ToolTip="Shows HTML from finished job at the bottom of this page"/>
    <!--  
            &nbsp;&nbsp;<a href="https://tpisprod.blob.core.windows.net/$web/tpis_jobs_table/tpis_pomeror_job_list.html?sp=rl&st=2021-08-16T17:58:47Z&se=2024-01-01T04:01:00Z&sv=2020-08-04&sr=c&sig=8tuwRFlus8Cgawq2%2FnqqtmiQ%2F6XaDVpxJUaMN3hBhIs%3D" target="_blank_">TPIS JOB TABLE </a>
     -->
    &nbsp;&nbsp;<asp:Literal ID="Joblink_Literal" runat="server">       
                </asp:Literal>

            <script type="text/javascript">
                function SetTarget() {
                    document.format[0].target = "_blank";
                }
            </script>


            <br />
               <asp:Label ID="Run_Status_Label1" runat="server" Text=""  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
            <br />
            <asp:Label ID="Job_List_Label1" runat="server" Text="List of Your Submitted Requests (Jobs): "  style="margin-bottom: 0px" Font-Size="X-Small"></asp:Label>
            <br />

 	     &nbsp;<asp:DropDownList ID="Job_List2_DropDownList" runat="server" 
                 DataSourceID="SqlDataSource33" DataTextField="Job_Name" 
                 DataValueField="Job_Num" Font-Size="X-Small"  >
             </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource33" runat="server" ConnectionString=
            "<%$ ConnectionStrings:TPISAZUS%>" 
            SelectCommand="select Job_Num
            , format(job_num,'#')+'. '+Status+': '+DBASE_ID+' '
            +conc+' @'+digits
            +': '+Job_Name as JOB_NAME
            from dbo.TPIS_JOB_STATUS
            where upper(user_id)=@USER_ID 
            and (isnull(completion_flag,0)>=-1)
            ORDER BY job_num desc">
            <SelectParameters>
                <asp:ControlParameter ControlID="User_ID_Valid_Label1" 
                    DefaultValue="POMEROR" Name="USER_ID" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>

        <br /><br />

            &nbsp;&nbsp;<asp:Button ID="Refresh_Job_List_Btn" runat="server" Font-Size="X-Small" 
                onclick="Refresh_Job_List_Click" Text="Refresh Job List" Visible="True" 
                ToolTip="Refresh"/>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Delete_Job_Btn" runat="server" Font-Size="X-Small" 
                onclick="Delete_Job_Btn_Click"  
                Text="Select Jobs to Delete" Visible="True" 
                ToolTip="Opens a Panel to Select Jobs to Delete"/>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;<asp:Button ID="Load_Parms_Btn" runat="server" Font-Size="X-Small" 
                onclick="Load_Parms_Btn_Click" Text="Load Previous Job" Visible="True" 
                ToolTip="For the JOB Showing in Your Selection List Above, Reload Your Previous Inputs"/>
             &nbsp;&nbsp;   
<br />
<br /><asp:Label ID="LoadJob_Status_Label1" runat="server" Font-Size="Small" 
  Text="LOAD STATUS:" Visible="False"></asp:Label>  
<br />
<br /><asp:Label ID="LoadJob_Smry_Label1" runat="server" Font-Size="Small" 
  Text="LOAD SUMMARY:" Visible="False"></asp:Label>  
<asp:Label ID="LoadJob_Change_Label1" runat="server" Visible="False" Text="" />
<br />
<asp:Label ID="Parmload_Test_Label1" runat="server" Visible="False" Text="" />
<br />
<br />
<asp:Label ID="Parmload_Process_Label1" runat="server" Visible="False" Text="" />
<br />
<asp:Label ID="Parmload_Loadloop2_Label1" runat="server" Visible="False" Text="" />
<br />
<br /><asp:Label ID="LoadJob_Error_Label1" runat="server" Font-Size="Small" 
  Text="ERRORS SHOWN HERE" Visible="False"></asp:Label>  
<br />


<asp:Panel ID="Selected_JobDelete_Panel" runat="server" Font-Size="Small" Height="300px" 
            style="margin-top: 0px" BorderStyle="None" Width="780px" Visible="False">
<br />
<asp:CheckBox ID="Delete_Jobs_chkbox" runat="server" Font-Size="Small" 
  Text="Hide Job Deletion Panel"  Checked="False" Visible="True" 
  oncheckedchanged="JOBDEL_ckbox_changed"
  ToolTip="Check Hide Then Click Refresh Job List Button to Close Delete Jobs Panel"
  />&nbsp;&nbsp;&nbsp;
<asp:Label ID="Delete_Status_Label1" runat="server" Text="Delete Status Field "  style="margin-bottom: 0px" Width="250px" Font-Size="Small"></asp:Label>


<br />

<asp:Table HorizontalAlign="Left" Style="display:block" Width="500px" runat="server">
<asp:TableRow>
<asp:TableCell>
   <asp:Label ID="JOBDEL_Select_From_List_Label1" runat="server" Font-Size="X-Small" Text="Select Jobs From List" Width="180px" ></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_JOBDEL_Labela1" runat="server" Text="  "  style="margin-bottom: 0px" Width="10px" Font-Size="Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
   <asp:Label ID="JOBDEL_Current_User_Selections_Label1" runat="server" Font-Size="X-Small" Text="Jobs to Delete:" Width="180px" ></asp:Label>
</asp:TableCell>
</asp:TableRow>
<asp:TableRow>

<asp:TableCell>
        <asp:ListBox ID="JOBDEL_Pick_ListBox" runat="server" DataSourceID="SqlDataSource33" 
            DataTextField="Job_Name" DataValueField="Job_Num" 
    SelectionMode="Multiple" style="margin-bottom: 0px" Height="190px" Width="350px"  Font-Size="X-Small" ></asp:ListBox>
</asp:TableCell>
<asp:TableCell>
        <asp:Label ID="Filler_JOBDEL_Label1" runat="server" Text="  "  style="margin-bottom: 0px" Height="190px" Width="10px" Font-Size="X-Small"></asp:Label>
</asp:TableCell>
<asp:TableCell>
        <asp:TextBox ID="JOBDEL_Selections_TextBox" runat="server" Height="190px" 
            style="margin-bottom: 0px" TextMode="MultiLine" Width="350px" Wrap="False" Font-Size="X-Small"
            ToolTip="1 country or group per line, use ! as 1st character as a NOT/do not include indicator.  TPIS Groups start with a . and your saved session lists with .L#"
            ></asp:TextBox>
        <br />
</asp:TableCell>
<asp:TableCell>
</asp:TableCell>
<asp:TableCell  Width="300px">

</asp:TableCell>
</asp:TableRow>


<asp:TableRow>
<asp:TableCell>
                <asp:Button ID="JOBDEL_MoveSelected_Button" runat="server" 
                    onclick="JOBDEL_MoveSelected_Button_Click" Text="Select &gt;" 
            Width="91px" Font-Size="X-Small" />

                <asp:Button ID="JOBDEL_MoveAll_Button" runat="server" 
            Text="Select All &gt;&gt;" Width="90px" onclick="JOBDEL_MoveAll_Button_Click" 
            Font-Size="X-Small" />
            
</asp:TableCell>
<asp:TableCell>

</asp:TableCell>
<asp:TableCell>
        <asp:Button ID="JOBDEL_RemoveAll_Button" runat="server" onclick="JOBDEL_RemoveAll_Button_Click" 
            Text="Clear Selections" Width="119px" Font-Size="X-Small" />
        &nbsp;&nbsp;<asp:Button ID="Delete_Selected_Jobs_Btn" runat="server" Font-Size="X-Small" 
                onclick="Delete_Selected_Job_Btn_Click"  
                Text="Delete Selected Jobs" Visible="True" 
                ToolTip="Deletes Job Selected in Delete Jobs Box"/>

</asp:TableCell>
</asp:TableRow>


</asp:Table>


    
    </asp:Panel>
    <!--   end Selected_JobDelete_panel  -->

<asp:Panel ID="Show_Formatted_Panel" runat="server" Visible="True">

  <asp:Panel ID="TPIS_Staff_Panel2" Visible="True" Style="display:block" runat="server">
    <br />
       <asp:Label ID="View_SQL_Return_Label" runat="server" Text="SQL Returned from proc call goes here" Visible="False"></asp:Label>
    <br />
       
       <asp:Label ID="CtySeq_Selections_View" runat="server" Text="SQL To View_for Selections" Visible="False"></asp:Label>
    <br />
        <asp:Label ID="Table_SQL1_Literal" runat="server" Text="FORMATTED HTML SQL CMD GOES HERE" Visible="False"></asp:Label>
</asp:Panel> <!-- end TPIS_Staff_Panel2 -->
    <asp:Literal ID="Table_Fmt1_Literal" runat="server" Text="To Show Ouput HERE For the Job Selected Click the 'Display HTML Table' Button" Visible="True"></asp:Literal>
</asp:Panel> <!-- end Show_Formatted_Panel -->
<asp:Panel ID="JOB_FORM_DATA_PANEL" Visible="True" Style="display:block" runat="server">
    <br />
    <br />
   <asp:Label ID="Job_Data_Gridview_Label" runat="server" Text="Shown Below: User Inputs Sent To Request Data" Visible="False"></asp:Label>
    
   <asp:GridView ID="Job_Data_GridView" runat="server" Visible="True">
   </asp:GridView>
</asp:Panel> <!--  end JOB_FORM_DATA_PANEL  -->

</asp:Panel> <!-- end Run_Job_Panel -->
</ContentTemplate>
</asp:UpdatePanel> <!-- end Run_Job_Update_Panel -->
     </div>
    </form>


</body>
</html>
