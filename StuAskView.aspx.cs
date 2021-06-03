using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuAskView : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnChangeClicked(object sender, EventArgs e)
    {
        var data = new StuAskReply();
        data.reply_date = DateTime.Now;
        data.ask_id = Request.QueryString["id"].AsInt();
        data.code = Session["login_code"].AsString();
        data.role = Session["login_role"].AsString();
        data.content = content.Text;

        //保存图片START
        string url = "/Upload/Photo/";
        string fileName = FileUpload2.SaveFile(Server.MapPath(url));
        if (!string.IsNullOrEmpty(fileName))
            data.imageUrl = url + fileName;

        Sql.Insert(data);
        this.ShowAlert("回复成功", "StuAskView.aspx?id="+data.ask_id.AsString());
    }

}