<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentInfo.aspx.cs" Inherits="CRUD_ASP.NET.StudentInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
		<h1>Student Info</h1>
		<asp:UpdatePanel ID="udtPanel1" runat="server">
			<ContentTemplate>
		<asp:Panel ID="pnlAdd" runat="server" Visible="False">
			<table style="width: 100%;">
				<tr>
					<td>
						<asp:Label ID="lblFirstName" runat="server" Text="First Name :"></asp:Label>

					</td>
					<td>
						<asp:TextBox ID="txtFristName" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="lblLastName" runat="server" Text="Last Name :"></asp:Label>
					</td>
					<td>
						<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>

					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth :"></asp:Label>

					</td>
					<td>
						<asp:TextBox ID="txtDateOfBirth" runat="server"></asp:TextBox>
						<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateOfBirth" 
							 Format="dd/MM/yyyy">
						</ajaxToolkit:CalendarExtender>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="lblPhone" runat="server" Text="Phone :"></asp:Label>

					</td>
					<td>
						<asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>

					</td>
				</tr>
				<tr>
					<td>
						<asp:Label ID="lblEmail" runat="server" Text="Email :"></asp:Label>

					</td>
					<td>
						<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-primary" OnClick="btnSave_Click" />
						<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-danger" OnClick="btnCancel_Click" />
					</td>
				</tr>
			</table>
		</asp:Panel>
		<asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">AddNew</asp:LinkButton>
		<asp:GridView ID="grdStudentDetail" runat="server" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            onpageindexchanging="gvPerson_PageIndexChanging" 
            onrowcancelingedit="gvPerson_RowCancelingEdit" 
            onrowdatabound="gvPerson_RowDataBound" onrowdeleting="gvPerson_RowDeleting" 
            onrowediting="gvPerson_RowEditing" onrowupdating="gvPerson_RowUpdating" 
            onsorting="gvPerson_Sorting" AllowPaging="True" AllowSorting="True">
        <RowStyle BackColor="White" ForeColor="#003399" />
            <Columns>
                <asp:CommandField ShowEditButton="True" HeaderText="Action Editing" />
                <asp:CommandField ShowDeleteButton="True" HeaderText="Action Deleting"  />
				<asp:TemplateField>
					<HeaderTemplate>
						Select All:
						<asp:CheckBox ID="chkboxSelectAll" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged"
							runat="server" />
					</HeaderTemplate>
					<ItemTemplate>
						<asp:CheckBox ID="chkSelect" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="ID" />
				
                <asp:TemplateField HeaderText="FirstName" SortExpression="FirstName">
                    <EditItemTemplate>
                        <asp:TextBox ID="grdFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblgrdFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField HeaderText="LastName" SortExpression="LastName">
                    <EditItemTemplate>
                        <asp:TextBox ID="grdLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblgrdLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:TemplateField HeaderText="DateOfBirth" SortExpression="LastName">
                    <EditItemTemplate>
                        <asp:TextBox ID="grdDateOfBirth" runat="server" Text='<%# Bind("DateOfBirth","{0:dd/MM/yyyy}") %>'></asp:TextBox>
						<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="grdDateOfBirth" 
						 Format="dd/MM/yyyy">
						</ajaxToolkit:CalendarExtender>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblgrdDateOfBirth" runat="server" Text='<%# Bind("DateOfBirth","{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:TemplateField HeaderText="PhoneNumber" SortExpression="PhoneNumber">
                    <EditItemTemplate>
                        <asp:TextBox ID="grdPhoneNumber" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblgrdPhoneNumber" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:TemplateField HeaderText="Email" SortExpression="Email">
                    <EditItemTemplate>
                        <asp:TextBox ID="grdEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblgrdEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
		</asp:GridView>
	</ContentTemplate></asp:UpdatePanel>
				<asp:Button ID="btnDelete" runat="server" Text="Delete Checked Records" 
				   OnClientClick = 'return confirm("Are you sure you want to delete this students from the list?");' OnClick="btnDelete_Click" CssClass="btn-danger" />
				<asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" CssClass="btn-primary"/>
</asp:Content>