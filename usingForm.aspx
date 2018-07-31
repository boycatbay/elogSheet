<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="usingForm.aspx.cs" Inherits="elogsheet.usingForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script type = "text/javascript">

     var counter = 0;

     function AddFileUpload() {

         var div = document.createElement('DIV');

         div.innerHTML = '<input id="file' + counter + '" name = "file' + counter +

                     '" type="file" />' +

                     '<input id="Button' + counter + '" type="button" ' +

                     'value="Remove" onclick = "RemoveFileUpload(this)" />';

         document.getElementById("FileUploadContainer").appendChild(div);

         counter++;

     }

     function RemoveFileUpload(div) {

         document.getElementById("FileUploadContainer").removeChild(div.parentNode);


     }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <h1 style="text-align: center; font-style: italic; font-family: @Arial Unicode MS;">
                e-Log Sheet</h1>
        </div>
        <div class="row">
        </div>
        <!-- Config -->
        <div class="row">
            <div class="row">
                <div class="col-25">
                    <label for="pscodeIn">
                        Process Step :</label>
                </div>
                <div class="col-75">
                    <asp:DropDownList ID="pscodeInSet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="pscodeInSet_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
			<br />
            <div class="row">
                <div class="col-25">
                    <label for="lgdesc">
                        Name of Log :</label>
                </div>
                <div class="col-75">
                    <asp:DropDownList ID="selectForm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="selectForm_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <!-- Example -->
        <asp:Panel runat="server" ID="example" CssClass="panel">
            <div id="Div1" class="row" runat="server">
                <div class="row">
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="lotno">
                                Log#</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="lotno" runat="server" Enabled="False"></asp:TextBox></div>
                    </div>
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="date">
                                Date</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="date" runat="server" Enabled="False"></asp:TextBox></div>
                    </div>
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="eno">
                                Initial</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="eno" runat="server"></asp:TextBox></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="shift">
                                Shift</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="shift" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="pscode">
                                PS</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="pscodef" runat="server" Enabled="False"></asp:TextBox></div>
                    </div>
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="mid">
                                M#</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="mid" runat="server"></asp:TextBox></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="lot">
                                Lot</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="lot" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="pkgcode">
                                PKG</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="pkgcode" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="col-30">
                        <div class="col-25" style="text-align: center;">
                            <label for="device">
                                Dev</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="device" runat="server"></asp:TextBox></div>
                    </div>
                </div>
            </div>
            <br />
            <br />
                <asp:GridView CssClass="mytable" align="center" ID="GridView1" runat="server" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical" 
                    DataKeyNames="form_sk,ls_items_sk,freq,sp_size,minspec,maxspec,specunit,input_cont,input_type,droplist,ls_item_desc,sp_size_unit" ViewStateMode="Enabled"
                    OnRowCreated="GridView1_RowCreated" PageSize="50">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="LS_ITEM_DESC" HeaderText="Items" SortExpression="LS_ITEM_DESC" />
                        <asp:BoundField DataField="freq" HeaderText="Frequency" SortExpression="freq" />
                        <asp:BoundField DataField="sp_size" HeaderText="Sample Size" SortExpression="sp_size" />
                        <asp:BoundField DataField="minspec" HeaderText="MinSpec" SortExpression="minspec" />
                        <asp:BoundField DataField="maxspec" HeaderText="MaxSpec" SortExpression="maxspec" />
                        <asp:BoundField DataField="specunit" HeaderText="Unit" SortExpression="specunit" />
                        <asp:TemplateField HeaderText="Result" ItemStyle-Width="45%">
                            <ItemTemplate>
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
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
           
            <br />
            <br />
            <div class="row" style="background-color: White; border-width: thin;border-color:Red;border-style:solid;">
                <div class="col-75">
                    <h2 >
                        Click to add files</h2>
                    &nbsp;&nbsp;
                    <div id="FileUploadContainer">
                        <!--FileUpload Controls will be added here -->
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <div class="col-25">
                    <input id="Button1" type="button" value="add" onclick="AddFileUpload()" />
                </div>
                <br />
                <br />
                <br />
            </div>
            <br />
            <br />
            <div class="row">
                <asp:Button ID="Button2" runat="server" Text="Save" OnClick="Button2_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
