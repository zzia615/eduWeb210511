using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuOrder : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Session["login_code"].AsString()))
            {
                this.ShowAlert("请先登录本系统", "Login.aspx");
                return;
            }
            string code = Request.QueryString["code"];
            var pp = new System.Data.IDbDataParameter[1];
            pp[0] = Sql.CreateParameter("@code", code);
            var data = Sql.Query<StuInfo>("code=@code", pp).FirstOrDefault();
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
            grade.Text = data.grade;
            lxfs.Text = data.lxfs;
            Image1.ImageUrl = data.photoUrl;
        }
    }

    protected void btnConfirmClicked(object sender, EventArgs e)
    {
        string code = Session["login_code"].AsString();
        var pp = new System.Data.IDbDataParameter[1];
        pp[0] = Sql.CreateParameter("@code", code);
        var data = Sql.Query<TeacherInfo>("code=@code", pp).FirstOrDefault();
        if (data == null)
        {
            this.ShowAlert("用户信息不存在", "Default.aspx");
            return;
        }

        var pp1 = new System.Data.IDbDataParameter[2];
        pp1[0] = Sql.CreateParameter("@code", code);
        pp1[1] = Sql.CreateParameter("@stu_code", Request.QueryString["code"]);
        int count = Sql.QueryCount<TeacherORDER>("code=@code and stu_code=@stu_code and yyzt=N'已预约'", pp1);
        if (count > 0)
        {
            this.ShowAlert("不允许重复预约", "Default.aspx");
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
            stu_code = Request.QueryString["code"]
        };
        Sql.Insert(order);

        this.ShowAlert("预约成功", "OrderInfo.aspx?code=" + code);

    }
}