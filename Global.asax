<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        SqlUtil Sql = new SqlUtil();
        // 在应用程序启动时运行的代码
        Sql.CreateTable<AdminInfo>();
        Sql.CreateTable<TeacherInfo>();
        Sql.CreateTable<StuInfo>();
        Sql.CreateTable<TeacherORDER>();
        Sql.CreateTable<StuAsk>();
        Sql.CreateTable<StuAskReply>();
        int count = Sql.QueryCount<AdminInfo>("1=1");
        //默认管理员账号admin 密码123456
        if (count <= 0)
        {
            Sql.Insert(new AdminInfo
            {
                code = "admin",
                name = "admin",
                lxfs = "-",
                pwd = "123456",
                photoUrl="/img/user-default.jpg"
            });
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码
        
    }

    void Session_Start(object sender, EventArgs e)
    {
        // 在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。

    }

</script>
