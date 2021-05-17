using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderInfo : System.Web.UI.Page
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
        if (role == "教员")
            Server.Transfer("StuOrderInfo.aspx?code=" + code);
        else if (role == "学员")
            Server.Transfer("TeacherOrderInfo.aspx?code=" + code);
    }
}