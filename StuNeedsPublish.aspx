<%@ Page Title="发布需求" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StuNeedsPublish.aspx.cs" Inherits="StuNeedsPublish" %>

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
			<label class="layui-form-label"><i class="layui-icon layui-icon-username"></i>需求内容</label>
			<div class="layui-input-block">
				<asp:TextBox runat="server" class="layui-input" name="needs" ID="needs" type="text" 
				lay-verify="required" lay-reqtext="需求内容是必填项，岂能为空？" placeholder="请输入" autocomplete="off" TextMode="MultiLine" style="height:200px;"></asp:TextBox>
			</div>
		</div>
		<div class="layui-form-item">
			<div class="layui-input-block">
				<asp:Button runat="server" lay-filter="form1" lay-submit="" class="layui-btn" Text="发布需求" OnClick="btnConfirmClicked"/>
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

