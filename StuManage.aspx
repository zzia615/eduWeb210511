<%@ Page Title="学员信息" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuManage.aspx.cs" Inherits="StuManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <style>
       form{
           padding:10px;
       }
       .table {
           width:100%;
           color:#fff;
           border:solid #fff 1px;
       }

       .table td{
           padding:5px;
       }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <h2><%=Page.Title %></h2>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="code" DataSourceID="SqlDataSource1" AllowPaging="True" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="照片" SortExpression="photoUrl" ItemStyle-Width="100">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("photoUrl") %>' Width="100" Height="100"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="code" HeaderText="账号" ReadOnly="True" SortExpression="code" />
            <asp:BoundField DataField="name" HeaderText="姓名" SortExpression="name" />
            <asp:BoundField DataField="sex" HeaderText="性别" SortExpression="sex" />
            <asp:BoundField DataField="age" HeaderText="年龄" SortExpression="age" />
            <asp:BoundField DataField="lxfs" HeaderText="联系方式" SortExpression="lxfs" />
            <asp:BoundField DataField="grade" HeaderText="年级" SortExpression="grade" />
            <asp:TemplateField ItemStyle-Width="100">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" CssClass="layui-btn layui-btn-danger"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" DeleteCommand="DELETE FROM [StuInfo] WHERE [code] = @code" InsertCommand="INSERT INTO [TeacherInfo] ([code], [name], [pwd], [sex], [age], [lxfs], [kemu], [photoUrl]) VALUES (@code, @name, @pwd, @sex, @age, @lxfs, @kemu, @photoUrl)" SelectCommand="SELECT * FROM [StuInfo]" UpdateCommand="UPDATE [TeacherInfo] SET [name] = @name, [pwd] = @pwd, [sex] = @sex, [age] = @age, [lxfs] = @lxfs, [kemu] = @kemu, [photoUrl] = @photoUrl WHERE [code] = @code">
        <DeleteParameters>
            <asp:Parameter Name="code" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="code" Type="String" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="pwd" Type="String" />
            <asp:Parameter Name="sex" Type="String" />
            <asp:Parameter Name="age" Type="Int32" />
            <asp:Parameter Name="lxfs" Type="String" />
            <asp:Parameter Name="kemu" Type="String" />
            <asp:Parameter Name="photoUrl" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="pwd" Type="String" />
            <asp:Parameter Name="sex" Type="String" />
            <asp:Parameter Name="age" Type="Int32" />
            <asp:Parameter Name="lxfs" Type="String" />
            <asp:Parameter Name="kemu" Type="String" />
            <asp:Parameter Name="photoUrl" Type="String" />
            <asp:Parameter Name="code" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

