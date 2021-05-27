using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuNeedsPublish : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    protected void btnConfirmClicked(object sender, EventArgs e)
    {
        string code = Session["login_code"].AsString();
        var pp = new System.Data.IDbDataParameter[1];
        pp[0] = Sql.CreateParameter("@code", code);
        var data = Sql.Query<StuInfo>("code=@code", pp).FirstOrDefault();
        if (data == null)
        {
            this.ShowAlert("学生信息不存在", "Default.aspx");
            return;
        }
        data.needs = needs.Text;
        data.pubDate = DateTime.Now;
        Sql.Edit(data);

        this.ShowAlert("发布成功");

    }
}