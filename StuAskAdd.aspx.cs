using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuAskAdd : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnChangeClicked(object sender, EventArgs e)
    {
        var data = new StuAsk();
        data.ask_date = DateTime.Now;
        data.ask_state = "打开";
        data.stu_code = Session["login_code"].AsString();

        data.title = title.Text;
        data.content = content.Text;
        data.catagory = catagory.Text;
        //保存图片START
        string url = "/Upload/Photo/";
        string fileName = FileUpload1.SaveFile(Server.MapPath(url));
        if (!string.IsNullOrEmpty(fileName))
            data.imageUrl = url + fileName;

        Sql.Insert(data);
        this.ShowAlert("保存成功", "StuAskCenter.aspx");
    }

}