<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户注册</title>
	<link rel="stylesheet" type="text/css" href="layui/css/layui.css"/>
	<link rel="stylesheet" type="text/css" href="css/login.css"/>
	<script src="layui/layui.js" type="text/javascript" charset="utf-8"></script>
</head>
<body>
	<h1>用户注册</h1>
	<form class="layui-form" runat="server">
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>用户名</label>
			<div class="layui-input-block">
				<input class="layui-input" name="code" type="text" 
				lay-verify="required" lay-reqtext="用户名是必填项，岂能为空？" placeholder="请输入" autocomplete="off"/>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-password"></i>密码</label>
			<div class="layui-input-block">
				<input class="layui-input" name="pwd" type="password"  
				lay-verify="required" lay-reqtext="密码是必填项，岂能为空？" placeholder="请输入" autocomplete="off"/>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-password"></i>确认密码</label>
			<div class="layui-input-block">
				<input class="layui-input" name="confirmPwd" type="password"  
				lay-verify="required|confirmPass" lay-reqtext="确认密码是必填项，岂能为空？" placeholder="请输入" autocomplete="off"/>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>姓名</label>
			<div class="layui-input-block">
				<input class="layui-input" name="name" type="text" 
				lay-verify="required" lay-reqtext="姓名是必填项，岂能为空？" placeholder="请输入" autocomplete="off"/>
			</div>
		</div>
		<div class="layui-form-item">
			<label class="layui-form-label"><i class="layui-icon layui-icon-user"></i>角色</label>
			<div class="layui-input-block">
				<input type="radio" name="role" value="教员" title="教员" checked="">
				<input type="radio" name="role" value="学员" title="学员">
			</div>
		</div>
			
			
		<div class="layui-form-item">
			<div class="layui-input-block">
				<asp:Button runat="server" lay-filter="form1" lay-submit="" class="layui-btn" Text="注册" OnClick="btnRegisterClicked"/>
			</div>
		</div>
		<div class="layui-form-item">
			<div class="layui-input-block">
				<p class="link-register">已有账号？<a href="login.aspx">登录</a></p>
			</div>
		</div>
	</form>
	<script>
		layui.use(["form"],function(){
            var form = layui.form,
                $ = layui.$;

            form.verify({
                confirmPass: function (value) {
                    if ($("input[name=pwd]").val() !== value) {
                        return "两次密码输入不一致";
                    }
                }
            })

            form.on("submit(form1)", function (res) {

            })
		})
    </script>
</body>
</html>