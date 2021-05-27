<%@ Page Title="确定预约" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuOrder.aspx.cs" Inherits="StuOrder" %>

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
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>头像</label>
			<div class="layui-input-block">
                <asp:Image ID="Image1" runat="server" ClientIDMode="Static" CssClass="layui-circle user-img2"/>
				<div>
					<asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" style="display:none"/>
				</div>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>姓名</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="name" ID="name" type="text" 
				lay-verify="required" lay-reqtext="姓名是必填项，岂能为空？" placeholder="请输入" autocomplete="off" ReadOnly="true"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>年龄</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="age" ID="age" type="text" 
				lay-verify="required|number" lay-reqtext="年龄是必填项，岂能为空？" placeholder="请输入" autocomplete="off" ReadOnly="true"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>性别</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="sex" ID="sex" type="text" 
				lay-verify="required" lay-reqtext="性别是必填项，岂能为空？" placeholder="请输入" autocomplete="off" ReadOnly="true"></asp:TextBox>

			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>年级</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="grade" ID="grade" type="text" 
				lay-verify="required" lay-reqtext="年级是必填项，岂能为空？" placeholder="请输入" autocomplete="off" ReadOnly="true"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>联系方式</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="lxfs" ID="lxfs" type="text" 
				lay-verify="required" lay-reqtext="联系方式是必填项，岂能为空？" placeholder="请输入" autocomplete="off" ReadOnly="true"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<div class="layui-input-block">
				<asp:Button runat="server" lay-filter="form1" lay-submit="" class="layui-btn" Text="确定预约" OnClick="btnConfirmClicked"/>
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
                

            });


            form.on("submit(form1)", function (res) {

            });
            
        });
    </script>
</asp:Content>

