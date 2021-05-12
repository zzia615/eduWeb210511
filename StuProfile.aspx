<%@ Page Title="个人中心" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuProfile.aspx.cs" Inherits="StuProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<style>
	 
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
	<h2><%=Page.Title %></h2>
	<div class="content-edit">
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>姓名</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="name" ID="name" type="text" 
				lay-verify="required" lay-reqtext="姓名是必填项，岂能为空？" placeholder="请输入" autocomplete="off"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>年龄</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="age" ID="age" type="text" 
				lay-verify="required|number|checkAge" lay-reqtext="年龄是必填项，岂能为空？" placeholder="请输入" autocomplete="off"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>性别</label>
			<div class="layui-input-block">
				<asp:DropDownList ID="sex" runat="server" class="layui-input">
					<asp:ListItem Value="男">男</asp:ListItem>
					<asp:ListItem Value="女">女</asp:ListItem>
				</asp:DropDownList>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>年级</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="grade" ID="grade" type="text" 
				lay-verify="required" lay-reqtext="年级是必填项，岂能为空？" placeholder="请输入" autocomplete="off"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>联系方式</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="lxfs" ID="lxfs" type="text" 
				lay-verify="required" lay-reqtext="联系方式是必填项，岂能为空？" placeholder="请输入" autocomplete="off"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<div class="layui-input-block">
				<asp:Button runat="server" lay-filter="form1" lay-submit="" class="layui-btn" Text="修改" OnClick="btnChangeClicked"/>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
	<script>
        layui.use(["form"], function () {
            var form = layui.form,
                $ = layui.$;

			form.verify({
				checkAge: function (data) {
					if (parseFloat(data) > 200) {
						return "年龄不允许大于200";
                    }
                }

			})

            form.on("submit(form1)", function (res) {

            })
        })
    </script>
</asp:Content>

