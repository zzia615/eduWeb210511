using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TeacherOrderFinish : System.Web.UI.Page
{
    SqlUtil Sql = new SqlUtil();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"];
            if (string.IsNullOrEmpty(id))
            {
                this.ShowAlert("未传入有效的预约号", "OrderInfo.aspx");
                return;
            }
            var data = Sql.Query<TeacherORDER>("id=@id", new System.Data.IDbDataParameter[]
            {
                Sql.CreateParameter("@id",id.AsInt())
            }).FirstOrDefault();

            if (data == null)
            {
                this.ShowAlert("未查询到预约信息，无法预约", "OrderInfo.aspx");
                return;
            }

            if (data.yyzt != "已预约")
            {
                this.ShowAlert("状态不是已预约，无法操作", "OrderInfo.aspx");
                return;
            }

            data.yyzt = "已完成";

            Sql.Edit(data);

            this.ShowAlert("成功完成预约", "OrderInfo.aspx");
        }
    }
}