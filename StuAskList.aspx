<%@ Page Title="问题列表" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuAskList.aspx.cs" Inherits="StuAskList" %>

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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" AllowPaging="True" CssClass="table">
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
            <asp:BoundField DataField="grade" HeaderText="年级" SortExpression="grade" />
            <asp:BoundField DataField="catagory" HeaderText="分类" SortExpression="catagory"/>
            <asp:BoundField DataField="title" HeaderText="问题" SortExpression="title"/>
            <asp:BoundField DataField="ask_state" HeaderText="状态" SortExpression="ask_state"/>
            <asp:BoundField DataField="ask_date" HeaderText="发布日期" SortExpression="ask_date" DataFormatString="{0:d}"/>
            <asp:TemplateField ItemStyle-Width="100">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink3" runat="server" CssClass="layui-btn layui-btn-sm" NavigateUrl='<%#"StuAskView.aspx?id="+Eval("id") %>'>回复</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" DeleteCommand="DELETE FROM [TeacherInfo] WHERE [code] = @code" InsertCommand="INSERT INTO [TeacherInfo] ([code], [name], [pwd], [sex], [age], [lxfs], [kemu], [photoUrl]) VALUES (@code, @name, @pwd, @sex, @age, @lxfs, @kemu, @photoUrl)" SelectCommand="SELECT StuInfo.*,StuAsk.ask_state,StuAsk.ask_date,StuAsk.title,StuAsk.id,StuAsk.catagory,StuAsk.ask_state FROM [StuInfo],[StuAsk] where StuInfo.code=StuAsk.stu_code and StuAsk.ask_state=N'打开'" UpdateCommand="UPDATE [TeacherInfo] SET [name] = @name, [pwd] = @pwd, [sex] = @sex, [age] = @age, [lxfs] = @lxfs, [kemu] = @kemu, [photoUrl] = @photoUrl WHERE [code] = @code">
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

