using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session.Clear();
    }

    protected void btnLoginClicked(object sender, EventArgs e)
    {
        try
        {
            string code = Request.Form["code"];
            string pwd = Request.Form["pwd"];
            string role = Request.Form["role"];
            System.Data.IDbDataParameter[] parms = new System.Data.IDbDataParameter[1];
            switch (role)
            {
                case "管理员":
                    {
                        parms[0] = Sql.CreateParameter("@code", code);
                        var data = Sql.Query<AdminInfo>("code=@code", parms).FirstOrDefault();
                        if (data != null)
                        {
                            Session["login_photo"] = data.photoUrl;
                            Session["login_name"] = data.name;
                            Session["login_pwd"] = data.pwd;
                        }
                    }
                    break;
                case "教员":
                    {
                        parms[0] = Sql.CreateParameter("@code", code);
                        var data = Sql.Query<TeacherInfo>("code=@code", parms).FirstOrDefault();
                        if (data != null)
                        {
                            Session["login_photo"] = data.photoUrl;
                            Session["login_name"] = data.name;
                            Session["login_sex"] = data.sex;
                            Session["login_age"] = data.age;
                            Session["login_pwd"] = data.pwd;
                        }
                    }
                    break;
                case "学员":
                    {
                        parms[0] = Sql.CreateParameter("@code", code);
                        var data = Sql.Query<StuInfo>("code=@code", parms).FirstOrDefault();
                        if (data != null)
                        {
                            Session["login_photo"] = data.photoUrl;
                            Session["login_name"] = data.name;
                            Session["login_sex"] = data.sex;
                            Session["login_age"] = data.age;
                            Session["login_pwd"] = data.pwd;
                        }
                    }
                    break;
            }

            if (string.IsNullOrEmpty(Session["login_name"].AsString()))
            {
                //用户信息不存在
                Page.ShowAlert("用户信息不存在");
                return;
            }
            if (Session["login_pwd"].AsString() != pwd)
            {
                //密码输入有误
                Page.ShowAlert("密码输入有误");
                return;
            }

            Session["login_code"] = code;
            Session["login_role"] = role;
            //跳转到指定页面
            Response.RedirectToPage("Default.aspx");
        }
        catch (Exception ex)
        {
            Page.ShowAlert("登录异常，错误信息:"+ex.Message);
            return;
        }

    }
}