<%@ Page Title="问题查看" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuAskView.aspx.cs" Inherits="StuAskView" %>

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
       .user-img2{
			width:100px;
			height: 100px;
			display: block;
			margin: 0 auto;
		}
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <h2><%=Page.Title %></h2>

    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <div>
                问题标题：<asp:Label ID="Label1" runat="server" Text='<%# Bind("title") %>'></asp:Label>
            </div>
            <div>
                学生姓名：<asp:Label ID="Label5" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                性别：<asp:Label ID="Label6" runat="server" Text='<%# Bind("sex") %>'></asp:Label>
                年龄：<asp:Label ID="Label7" runat="server" Text='<%# Bind("age") %>'></asp:Label>
                年级：<asp:Label ID="Label8" runat="server" Text='<%# Bind("grade") %>'></asp:Label>
            </div>
            <div>
                分类：<asp:Label ID="Label4" runat="server" Text='<%# Bind("catagory") %>'></asp:Label>
                发布时间：<asp:Label ID="Label3" runat="server" Text='<%# Bind("ask_date") %>'></asp:Label>
            </div>
            <div>
                问题内容：<asp:Label ID="Label2" runat="server" Text='<%# Bind("content") %>'></asp:Label>
            </div>
            <div>
                问题图：<asp:Image ID="Image1" runat="server" ImageUrl='<%# Bind("imageUrl") %>'/>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" DeleteCommand="DELETE FROM [TeacherInfo] WHERE [code] = @code" InsertCommand="INSERT INTO [TeacherInfo] ([code], [name], [pwd], [sex], [age], [lxfs], [kemu], [photoUrl]) VALUES (@code, @name, @pwd, @sex, @age, @lxfs, @kemu, @photoUrl)" SelectCommand="SELECT StuInfo.*,StuAsk.ask_state,StuAsk.ask_date,StuAsk.title,StuAsk.id,StuAsk.catagory,StuAsk.content,StuAsk.ask_state,StuAsk.imageUrl FROM [StuInfo],[StuAsk] where StuInfo.code=StuAsk.stu_code and StuAsk.id=@id" UpdateCommand="UPDATE [StuAsk] SET [ask_state] = N'关闭' WHERE [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="code" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="id" />
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

    <h2>回复</h2>
    <div class="content-edit">
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>回复图</label>
			<div class="layui-input-block">
                <asp:Image ID="Image2" runat="server" ClientIDMode="Static" CssClass="layui-circle user-img2"/>
				<div>
					<asp:FileUpload ID="FileUpload2" runat="server" ClientIDMode="Static" style="display:none"/>
				</div>
			</div>
		</div>
		
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>内容</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="content" ID="content" type="text" 
				lay-verify="required" lay-reqtext="内容是必填项，岂能为空？" placeholder="请输入" autocomplete="off"></asp:TextBox>
			</div>
		</div>
		
		<div class="layui-form-item">
			<div class="layui-input-block">
				<asp:Button runat="server" lay-filter="form1" lay-submit="" class="layui-btn" Text="回复" OnClick="btnChangeClicked"/>
			</div>
		</div>
	</div>

    <h2>回复列表</h2>
    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
        <ItemTemplate>
            <div>
                回复角色：<asp:Label ID="Label9" runat="server" Text='<%# Bind("role") %>'></asp:Label>
                回复账号：<asp:Label ID="Label5" runat="server" Text='<%# Bind("code") %>'></asp:Label>
                回复日期：<asp:Label ID="Label1" runat="server" Text='<%# Bind("reply_date") %>'></asp:Label>
            </div>
            <div>
                回复内容：<asp:Label ID="Label2" runat="server" Text='<%# Bind("content") %>'></asp:Label>
            </div>
            <div>
                回复图：<asp:Image ID="Image3" runat="server" ImageUrl='<%# Bind("imageUrl") %>'/>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:conStr %>" SelectCommand="select * from StuAskReply where ask_id=@ask_id">
        <SelectParameters>
            <asp:QueryStringParameter Name="ask_id" QueryStringField="id" />
        </SelectParameters>
    </asp:SqlDataSource>

    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
    <script>
		layui.use(["form", "upload"], function () {
			var form = layui.form,
				$ = layui.$,
				upload = layui.upload;

			form.verify({
				checkAge: function (data) {
					if (parseFloat(data) > 200) {
						return "年龄不允许大于200";
					}
				}

			});


			form.on("submit(form1)", function (res) {

			});
			$("#Image2").on("click", function () {
				$("#FileUpload2").trigger("click");
			});
			$("#FileUpload2").on("change", function () {
                var file = this.files[0];
                if (!/image\/\w+/.test(file.type)) {
					alert("请确保文件为图像类型");
					$("#FileUpload2").val("");
                    return false;
                }
                getImgBase64Data(file, function (result) {
					$("#Image2").attr("src", result);
				});
			});
		});
		function getImgBase64Data(file, callback) {
            var reader = new FileReader();
            reader.onload = function (e) {
                callback(e.target.result);
            };
			reader.readAsDataURL(file); // 读取完后会调用onload方法
        }
    </script>
</asp:Content>

