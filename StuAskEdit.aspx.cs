using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuAskEdit : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"];
            var pp = new System.Data.IDbDataParameter[2];
            pp[0] = Sql.CreateParameter("@id", id);
            pp[1] = Sql.CreateParameter("@stu_code", Session["login_code"].AsString());
            var data = Sql.Query<StuAsk>("id=@id and stu_code=@stu_code", pp).FirstOrDefault();
            if (data == null)
            {
                this.ShowAlert("问题信息不存在", "Default.aspx");
                return;
            }
            title.Text = data.title;
            content.Text = data.content;
            catagory.Text = data.catagory;
            Image1.ImageUrl = data.imageUrl;
        }
    }

    protected void btnChangeClicked(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        var pp = new System.Data.IDbDataParameter[2];
        pp[0] = Sql.CreateParameter("@id", id);
        pp[1] = Sql.CreateParameter("@stu_code", Session["login_code"].AsString());
        var data = Sql.Query<StuAsk>("id=@id and stu_code=@stu_code", pp).FirstOrDefault();
        if (data == null)
        {
            this.ShowAlert("问题信息不存在", "Default.aspx");
            return;
        }
        data.title = title.Text;
        data.content = content.Text;
        data.catagory = catagory.Text;
        //保存图片START
        string url = "/Upload/Photo/";
        string fileName = FileUpload1.SaveFile(Server.MapPath(url));
        if (!string.IsNullOrEmpty(fileName))
            data.imageUrl = url + fileName;
 
        Sql.Edit(data);
        this.ShowAlert("保存成功", "StuAskCenter.aspx");
    }

}