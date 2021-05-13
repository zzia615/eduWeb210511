using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProfileInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string code = Session["login_code"].AsString();
        string role = Session["login_role"].AsString();

        if (string.IsNullOrEmpty(code))
        {
            this.ShowAlert("用户未登录", "Login.aspx");
            return;
        }
        if (role == "学员")
            Server.Transfer("StuProfile.aspx?code=" + code);
        else if (role == "教员")
            Server.Transfer("TeacherProfile.aspx?code=" + code);
    }
}