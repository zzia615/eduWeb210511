<%@ Page Title="新增问题" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuAskAdd.aspx.cs" Inherits="StuAskAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<style>
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
	<div class="content-edit">
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>问题图</label>
			<div class="layui-input-block">
                <asp:Image ID="Image1" runat="server" ClientIDMode="Static" CssClass="layui-circle user-img2"/>
				<div>
					<asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" style="display:none"/>
				</div>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>标题</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="title" ID="title" type="text" 
				lay-verify="required" lay-reqtext="标题是必填项，岂能为空？" placeholder="请输入" autocomplete="off"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>分类</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="catagory" ID="catagory" type="text" 
				lay-verify="required" lay-reqtext="分类是必填项，岂能为空？" placeholder="请输入" autocomplete="off"></asp:TextBox>
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
				<asp:Button runat="server" lay-filter="form1" lay-submit="" class="layui-btn" Text="新增" OnClick="btnChangeClicked"/>
			</div>
		</div>
	</div>
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
			$("#Image1").on("click", function () {
				$("#FileUpload1").trigger("click");
			});
			$("#FileUpload1").on("change", function () {
                var file = this.files[0];
                if (!/image\/\w+/.test(file.type)) {
					alert("请确保文件为图像类型");
					$("#FileUpload1").val("");
                    return false;
                }
                getImgBase64Data(file, function (result) {
					$("#Image1").attr("src", result);
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

