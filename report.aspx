<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="report.aspx.cs" Inherits="elogsheet.report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="Scripts/jquery-1.4.1.js">
        $(function () {
            var startDate = $("#start");
            var endDate = $("#end");
            startDate.datetimepicker({
                addSliderAccess: true,
                sliderAccessArgs: { touchonly: false },
                onClose: function (dateText, inst) {
                    if (endDate.val() != '') {
                        var testStartDate = startDate.datetimepicker('getDate');
                        var testEndDate = endDate.datetimepicker('getDate');
                        if (testStartDate > testEndDate)
                            endDate.datetimepicker('setDate', testStartDate);
                    }
                    else {
                        endDate.val(dateText);
                    }
                },
                onSelect: function (selectedDateTime) {
                    endDate.datetimepicker('option', 'minDate', startDate.datetimepicker('getDate'));
                }
            });
            endDate.datetimepicker({
                addSliderAccess: true,
                sliderAccessArgs: { touchonly: false },
                onClose: function (dateText, inst) {
                    if (startDate.val() != '') {
                        var testStartDate = startDate.datetimepicker('getDate');
                        var testEndDate = endDate.datetimepicker('getDate');
                        if (testStartDate > testEndDate)
                            startDate.datetimepicker('setDate', testEndDate);
                    }
                    else {
                        startDate.val(dateText);
                    }
                },
                onSelect: function (selectedDateTime) {
                    startDate.datetimepicker('option', 'maxDate', endDate.datetimepicker('getDate'));
                }
            });
        });   
    </script>





    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div align="center">
            <h2>
                Report of e-Log Sheet</h2>
        </div>
        <br />
        <div id="dataShow" align="center" runat="server">
            <div class="row" align="center">
                <label for="start">
                    Date from :
                </label>
                &nbsp;
                <asp:TextBox ID="start" TextMode="Date" runat="server" Style="width: 20%; text-align: center;"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <label for="end">
                    To :
                </label>
                
                <asp:TextBox ID="end" TextMode="Date" runat="server" Style="width: 20%; text-align: center;"></asp:TextBox>
            </div>
            <br />
            <div class="row" align="center">
                <label for="marea">
                    Area :
                </label>&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="marea" runat="server" Width="20%" DataField="PS_DESC" AutoPostBack="true"
                    OnSelectedIndexChanged="marea_SelectedIndexChanged">
                </asp:DropDownList>
                
            </div>
            <div class="row" align="center">
                <label for="DropDownList1">
                    Log Sheet :
                </label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="20%" 
                    DataField="PS_DESC"
                   >
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </div>
			 <div class="row">
               
              
                  <asp:Button ID="formSelt" runat="server" Text="Select" 
                      OnClick="formSelt_Onclick" />
				  </div>
         
               
            <br />
            <div class="row" id="formsel" runat="server" visible = "false">
                <div style="overflow:auto;width:100%;height:400px">
                <asp:GridView CssClass="mytable" align="center" ID="GridView1" runat="server"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical" ViewStateMode="Enabled">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Bold="True" 
                        HorizontalAlign="Center" VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
                    </div>
            
            <br />
            <div class="row">
                <div class="col-75">
                
                <asp:TextBox ID="exportFilename" visible="false" runat="server" Style="width: 60%; text-align: center;"></asp:TextBox>
                </div>
                <div class="col-25">
                  <asp:Button ID="exportExcel" runat="server" Text="Export Report" OnClick="exportExcel_Onclick" /></div>
            </div>
                </div>
            <div class="row">
                </div>
        </div>
    </div>
</asp:Content>
