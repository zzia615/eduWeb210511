<%@ Page Title="我的问题列表" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuAskCenter.aspx.cs" Inherits="StuAskCenter" %>

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
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="StuAskAdd.aspx" CssClass="layui-btn">新增问题</asp:HyperLink>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSource1" AllowPaging="True" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="照片" SortExpression="photoUrl" ItemStyle-Width="100">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("photoUrl") %>' Width="100" Height="100"/>
                </ItemTemplate>
                <ItemStyle Width="100px"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="catagory" HeaderText="分类" SortExpression="catagory"/>
            <asp:BoundField DataField="title" HeaderText="问题" SortExpression="title"/>
            <asp:BoundField DataField="ask_state" HeaderText="状态" SortExpression="ask_state"/>
            <asp:BoundField DataField="ask_date" HeaderText="发布日期" SortExpression="ask_date" DataFormatString="{0:d}" ItemStyle-Width="100"/>
            <asp:TemplateField ItemStyle-Width="200">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink3" runat="server" CssClass="layui-btn layui-btn-sm" NavigateUrl='<%#"StuAskView.aspx?id="+Eval("id") %>'>查看</asp:HyperLink>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="关闭" CssClass="layui-btn layui-btn-sm"></asp:LinkButton>
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="layui-btn layui-btn-sm" NavigateUrl='<%#"StuAskEdit.aspx?id="+Eval("id") %>'>修改</asp:HyperLink>
                </ItemTemplate>

            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" DeleteCommand="DELETE FROM [TeacherInfo] WHERE [code] = @code" InsertCommand="INSERT INTO [TeacherInfo] ([code], [name], [pwd], [sex], [age], [lxfs], [kemu], [photoUrl]) VALUES (@code, @name, @pwd, @sex, @age, @lxfs, @kemu, @photoUrl)" SelectCommand="SELECT StuInfo.*,StuAsk.ask_state,StuAsk.ask_date,StuAsk.title,StuAsk.id,StuAsk.catagory,StuAsk.ask_state FROM [StuInfo],[StuAsk] where StuInfo.code=StuAsk.stu_code and StuInfo.code=@code" UpdateCommand="UPDATE [StuAsk] SET [ask_state] = N'关闭' WHERE [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="code" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="code" SessionField="login_code" />
        </SelectParameters>
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
            <asp:Parameter Name="id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
</asp:Content>

