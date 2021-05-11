using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// 扩展类
/// </summary>

public static class ConvertExtension
{
    /// <summary>
    /// 对象转字符串
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string AsString(this object obj)
    {
        if (obj == null) return string.Empty;
        return obj.ToString();
    }
    /// <summary>
    /// 对象转整数
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static int AsInt(this object obj)
    {
        int o;
        int.TryParse(obj.AsString(), out o);
        return o;
    }

    public static long AsLong(this object obj)
    {
        long o;
        long.TryParse(obj.AsString(), out o);
        return o;
    }
    /// <summary>
    /// 对象转可为空的整数
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static int? AsNullInt(this object obj)
    {
        if (string.IsNullOrEmpty(obj.AsString()))
        {
            return null;
        }
        int o;
        int.TryParse(obj.AsString(), out o);
        return o;
    }
    /// <summary>
    /// 对象转日期
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static DateTime AsDateTime(this object obj)
    {
        DateTime o;
        DateTime.TryParse(obj.AsString(), out o);
        return o;
    }
    /// <summary>
    /// 对象转浮点数
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static double AsDouble(this object obj)
    {
        double o;
        double.TryParse(obj.AsString(), out o);
        return o;
    }
    /// <summary>
    /// 转换成%值%用于查询
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string AsLike(this object obj)
    {
        return string.Format("%{0}%", obj.AsString());
    }
    /// <summary>
    /// 对象转十进制数
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static decimal AsDecimal(this object obj)
    {
        decimal o;
        decimal.TryParse(obj.AsString(), out o);
        return o;
    }
    /// <summary>
    /// 对象转可为空的日期
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static DateTime? AsNullDateTime(this object obj)
    {
        if (string.IsNullOrEmpty(obj.AsString()))
        {
            return null;
        }
        DateTime o;
        DateTime.TryParse(obj.AsString(), out o);
        return o;
    }

    /// <summary>
    /// StringBuilder的扩展函数
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="val"></param>
    public static void AppendAnd(this System.Text.StringBuilder sb, string val)
    {
        if (sb.Length > 0) sb.Append(" AND ");
        sb.Append(val);
    }

    public static void RedirectToPage(this System.Web.HttpResponse response, string page)
    {
        response.Redirect(page);
    }
    public static string AsJson(this object obj)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }
    public static void ShowAlert(this System.Web.UI.Page page, string msg)
    {
        var msgJson = new { code = 0, msg }.AsJson();
        msgJson = msgJson.Replace("'", "");
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script>var msgData=JSON.parse('" + msgJson + "');layui.layer.alert(msgData.msg);</script>");
    }
    
}
