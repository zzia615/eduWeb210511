using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuProfile : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {
                this.ShowAlert("用户尚未登录", "Login.aspx");
                return;
            }
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
                sex.SelectedValue = "男";
            }
            else
            {
                sex.SelectedValue = "女";
            }
            grade.Text = data.grade;
            lxfs.Text = data.lxfs; 
        }
    }

    protected void btnChangeClicked(object sender, EventArgs e)
    {
        string code = Request.QueryString["code"];
        if (string.IsNullOrEmpty(code))
        {
            this.ShowAlert("用户尚未登录", "Login.aspx");
            return;
        }
        var pp = new System.Data.IDbDataParameter[1];
        pp[0] = Sql.CreateParameter("@code", code);
        var data = Sql.Query<StuInfo>("code=@code", pp).FirstOrDefault();
        if (data == null)
        {
            this.ShowAlert("用户信息不存在", "Default.aspx");
            return;
        }
        data.name = name.Text;
        data.age = age.Text.AsInt();
        //获取性别
        data.sex = sex.SelectedValue;

        data.grade = grade.Text;
        data.lxfs = lxfs.Text;
        Sql.Edit(data);
        this.ShowAlert("修改信息成功", "StuProfile.aspx?code=" + code);
    }

}