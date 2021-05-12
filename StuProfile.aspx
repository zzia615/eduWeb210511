<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuProfile.aspx.cs" Inherits="StuProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
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
			lay-verify="required" lay-reqtext="年龄是必填项，岂能为空？" placeholder="请输入" autocomplete="off"></asp:TextBox>
		</div>
	</div>
	<div class="layui-form-item">
		<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>性别</label>
		<div class="layui-input-block">
			<input type="radio" ID="sex_male" name="sex" value="男" title="男" checked="" />
			<input type="radio" ID="sex_female" name="sex" value="女" title="女"/>
		</div>
	</div>
	
	<div class="layui-form-item">
		<div class="layui-input-block">
			<asp:Button runat="server" lay-filter="form1" lay-submit="" class="layui-btn" Text="修改" OnClick="btnChangeClicked"/>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" Runat="Server">
	<script>
        layui.use(["form"], function () {
            var form = layui.form,
                $ = layui.$;

            

            form.on("submit(form1)", function (res) {

            })
        })
    </script>
</asp:Content>

