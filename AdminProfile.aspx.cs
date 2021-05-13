using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminProfile : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string code = Request.QueryString["code"];
            var pp = new System.Data.IDbDataParameter[1];
            pp[0] = Sql.CreateParameter("@code", code);
            var data = Sql.Query<AdminInfo>("code=@code", pp).FirstOrDefault();
            if (data == null)
            {
                this.ShowAlert("用户信息不存在", "Default.aspx");
                return;
            }
            //姓名
            name.Text = data.name;
            lxfs.Text = data.lxfs;
            Image1.ImageUrl = data.photoUrl;
        }
    }

    protected void btnChangeClicked(object sender, EventArgs e)
    {
        string code = Request.QueryString["code"];
        var pp = new System.Data.IDbDataParameter[1];
        pp[0] = Sql.CreateParameter("@code", code);
        var data = Sql.Query<AdminInfo>("code=@code", pp).FirstOrDefault();
        if (data == null)
        {
            this.ShowAlert("用户信息不存在", "Default.aspx");
            return;
        }
        data.name = name.Text;
        data.lxfs = lxfs.Text;
        //保存图片START
        string url = "/Upload/Photo/";
        string fileName = FileUpload1.SaveFile(Server.MapPath(url));
        if (!string.IsNullOrEmpty(fileName))
            data.photoUrl = url + fileName;
        //保存图片END
        Sql.Edit(data);
        //重新设置缓存信息
        Session["login_photo"] = data.photoUrl;
        Session["login_name"] = data.name;

        this.ShowAlert("修改信息成功", "ProfileInfo.aspx");
    }

}