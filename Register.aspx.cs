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
        
    }

    protected void btnRegisterClicked(object sender, EventArgs e)
    {
        try
        {
            string name = Request.Form["name"];
            string code = Request.Form["code"];
            string pwd = Request.Form["pwd"];
            string role = Request.Form["role"];

            switch (role)
            {
                case "教员":
                    {
                        var data = Sql.Insert(new TeacherInfo
                        {
                            code = code,
                            name = name,
                            pwd = pwd,
                            photoUrl = "/img/user-default.jpg"
                        });
                    }
                    break;
                case "学员":
                    {
                        var data = Sql.Insert(new StuInfo
                        {
                            code = code,
                            name = name,
                            pwd = pwd,
                            photoUrl = "/img/user-default.jpg"
                        });
                    }
                    break;
            }


            //跳转到指定页面
            Page.ShowAlert("注册成功，请返回登录");
            return;
        }
        catch
        {
            Page.ShowAlert("注册异常，请检查账号是否被注册");
            return;
        }

    }
}