using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TeacherOrder : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string code = Request.QueryString["code"];
            var pp = new System.Data.IDbDataParameter[1];
            pp[0] = Sql.CreateParameter("@code", code);
            var data = Sql.Query<TeacherInfo>("code=@code", pp).FirstOrDefault();
            if (data == null)
            {
                this.ShowAlert("用户信息不存在", "Default.aspx");
                return;
            }
            //姓名
            name.Text = data.name;
            age.Text = "";
            //年龄
            if (data.age != null)
                age.Text = data.age.Value.ToString();

            if (data.sex == "男")
            {
                sex.Text = "男";
            }
            else
            {
                sex.Text = "女";
            }
            kemu.Text = data.kemu;
            lxfs.Text = data.lxfs;
            Image1.ImageUrl = data.photoUrl;
        }
    }

    protected void btnConfirmClicked(object sender, EventArgs e)
    {
        string code = Request.QueryString["code"];
        var pp = new System.Data.IDbDataParameter[1];
        pp[0] = Sql.CreateParameter("@code", code);
        var data = Sql.Query<TeacherInfo>("code=@code", pp).FirstOrDefault();
        if (data == null)
        {
            this.ShowAlert("用户信息不存在", "Default.aspx");
            return;
        }
        //教师预约信息
        TeacherORDER order = new TeacherORDER
        {
            age = data.age,
            code = data.code,
            kemu = data.kemu,
            lxfs = data.lxfs,
            name = data.name,
            photoUrl = data.photoUrl,
            sex = data.sex,
            yyzt = "已预约",
            stu_code = Session["login_code"].AsString()
        };
        Sql.Insert(order);

        this.ShowAlert("预约成功", "OrderInfo.aspx?code=" + code);

    }
}