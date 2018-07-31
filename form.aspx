<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="form.aspx.cs" Inherits="elogsheet.form" %>

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
                e-Log Sheet Setting
            </h1>
        </div>
        <div class="row">
            <asp:GridView  align="center" ID="GridView2" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" GridLines="Vertical" DataKeyNames="form_sk,ps_code,form_name"
                Height="233px" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging"
                OnRowCommand="GridView2_RowCommand">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="PS_CODE" HeaderText="Process Step" SortExpression="PS_CODE" />
                    <asp:BoundField DataField="form_name" HeaderText="Form Name" SortExpression="form_name" />
                    <asp:ButtonField CommandName="Selete" HeaderText="Edit" ShowHeader="True" Text="Edit"
                        ButtonType="Button" />
                    <asp:ButtonField CommandName="Del" HeaderText="Delete" ShowHeader="True" Text="Delete"
                        ButtonType="Button" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    PageButtonCount="15" />
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
        <!-- Config -->
        <div class="row">
            <div class="row">
                <div class="col-25">
                    <label for="pscodeIn">
                        Process Step :</label>
                </div>
                <div class="col-75">
                    <asp:DropDownList ID="pscodeInSet" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-25">
                    <label for="lgdesc">
                        Name of Log :</label>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="lgdescSet" runat="server">
                    </asp:TextBox>
                </div>
            </div>
            <div class="row">
                <asp:Button ID="setNewForm" runat="server" Text="Set New Form" OnClick="setNewForm_Click"
                    Visible="false" />
                <asp:Button ID="addNewForm" runat="server" Text="Save" OnClick="addNewForm_Click" />
            </div>
			<br /><br /><br />
            <div id="addItemtoform" runat="server" visible="false">
                <div class="row">
                    <h2 style="font-style: normal; font-family: @Arial Unicode MS;">
                     Checklist Add</h2>
                </div>
                <div class="row">
                    <div class="col-25">
                        <label for="Item">
                            Check-item :</label>
                    </div>
                    <div class="col-75">
                        <asp:TextBox ID="ItemSet" runat="server">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-25">
                        <label for="freq">
                            Frequency :</label>
                    </div>
                    <div class="col-75">
                        <asp:TextBox ID="freqSet"  runat="server">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-25">
                        <label for="spsizeSet">
                            Sample Size :</label>
                    </div>
                    <div class="col-75">
                        <asp:TextBox ID="spsizeSet" TextMode="Number" runat="server">
                        </asp:TextBox>
                    </div>
					</div>
					<div class="row">
                    <div class="col-25">
                        <label for="spunit">
                            Sample Unit :</label></div>
                    <div class="col-75">
                        <asp:TextBox ID="spunitSet" runat="server">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <h2 style="text-align: center; font-style: normal; font-family: @Arial Unicode MS;">
                        Spec</h2>
                    <div class="row">
                        <div class="col-25">
                            <label for="minspec">
                                Minimal :</label>
                        </div>
                        <div class="col-75">
                            <asp:TextBox ID="minspecSet" TextMode="Number" runat="server">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-25">
                            <label for="maxspec">
                                Maximum :</label>
                        </div>
                        <div class="col-75">
                            <asp:TextBox ID="maxspecSet" TextMode="Number" runat="server">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-25">
                        <label for="unit">
                            Unit :</label>
                    </div>
                    <div class="col-75">
                        <asp:TextBox ID="unitSet" runat="server">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-25">
                        <label for="dropTexSel">
                            Result Input Style :</label>
                    </div>
                    <div class="col-75">
                        <asp:RadioButtonList ID="dropTexSet" runat="server" OnSelectedIndexChanged="dropTexSel_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem>  TextBox</asp:ListItem>
                            <asp:ListItem>  Dropdown</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
				<br />
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="col-25">
                            <label for="droplist">
                              Dropdrop Values(Seperate by &#39; ; &#39; ):</label>
                        </div>
                        <div class="col-75">
                            <textarea id="droplist" runat="server" cols="20" rows="2"></textarea>
                        </div>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                       
                    </asp:View>
                </asp:MultiView>
                <div class="row">
                    <asp:Button ID="add" runat="server" Text="Save" OnClick="add_Click" />
                </div>
            </div>
        </div>
        <br />
        <!-- Example -->
        <div class="row" >
            <asp:Panel runat="server" ID="example" Visible="false" CssClass="panel">
                <div class="row" runat="server">
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
                                E#</label></div>
                        <div class="col-75" style="text-align: center;">
                            <asp:TextBox ID="eno" TextMode="Number" runat="server" Enabled="False"></asp:TextBox></div>
                    </div>
                    <div class="row">
                        <div class="col-30">
                            <div class="col-25" style="text-align: center;">
                                <label for="shift">
                                    Shift</label></div>
                            <div class="col-75" style="text-align: center;">
                                <asp:TextBox ID="shift" TextMode="Number" runat="server" Enabled="False"></asp:TextBox></div>
                        </div>
                        <div class="col-30">
                            <div class="col-25" style="text-align: center;">
                                <label for="pscode">
                                    PS</label></div>
                            <div class="col-75" style="text-align: center;">
                                <asp:TextBox ID="pscode" TextMode="Number" runat="server" Enabled="False"></asp:TextBox></div>
                        </div>
                        <div class="col-30">
                            <div class="col-25" style="text-align: center;">
                                <label for="mid">
                                    M#</label></div>
                            <div class="col-75" style="text-align: center;">
                                <asp:TextBox ID="mid" TextMode="Number" runat="server" Enabled="False"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-30">
                            <div class="col-25" style="text-align: center;">
                                <label for="lot">
                                    Lot</label></div>
                            <div class="col-75" style="text-align: center;">
                                <asp:TextBox ID="lot" TextMode="Number" runat="server" Enabled="False"></asp:TextBox></div>
                        </div>
                        <div class="col-30">
                            <div class="col-25" style="text-align: center;">
                                <label for="pkgcode">
                                    PKG</label></div>
                            <div class="col-75" style="text-align: center;">
                                <asp:TextBox ID="pkgcode" TextMode="Number" runat="server" Enabled="False"></asp:TextBox></div>
                        </div>
                        <div class="col-30">
                            <div class="col-25" style="text-align: center;">
                                <label for="device">
                                    Dev</label></div>
                            <div class="col-75" style="text-align: center;">
                                <asp:TextBox ID="device" TextMode="Number" runat="server" Enabled="False"></asp:TextBox></div>
                        </div>
                    </div>
                </div>
				<br />
				<br />
              
                    <asp:GridView CssClass="mytable" align="center" ID="GridView1" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" GridLines="Vertical" DataKeyNames="form_sk,ls_items_sk,freq,sp_size,minspec,maxspec,specunit,input_cont,input_type,droplist,ls_item_desc,sp_size_unit"
                        AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ViewStateMode="Enabled"
                        OnRowCreated="GridView1_RowCreated" onrowcommand="GridView1_RowCommand">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="Del" HeaderText="Delete" ShowHeader="True"
                                Text="Delete" />
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
                        <h2 style="font-family: Arial">
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
            </asp:Panel>
        </div>
    </div>
</asp:Content>
