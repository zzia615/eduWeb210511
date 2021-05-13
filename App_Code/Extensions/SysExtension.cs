using System;
using System.Collections.Generic;
using System.IO;
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
    /// <summary>
    /// 弹出提示
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg">提示内容</param>
    public static void ShowAlert(this System.Web.UI.Page page, string msg)
    {
        var msgJson = new { code = 0, msg }.AsJson();
        msgJson = msgJson.Replace("'", "");
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script>var msgData=JSON.parse('" + msgJson + "');layui.layer.alert(msgData.msg);</script>");
    }
    /// <summary>
    /// 弹出提示并跳转到指定页面
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg">提示内容</param>
    /// <param name="url">URL地址</param>
    public static void ShowAlert(this System.Web.UI.Page page, string msg,string url)
    {
        var msgJson = new { code = 0, msg }.AsJson();
        msgJson = msgJson.Replace("'", "");
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script>var msgData=JSON.parse('" + msgJson + "');layui.layer.alert(msgData.msg,function(){window.location.href='"+url+"';});</script>");
    }

    public static void SetInputValue(this System.Web.UI.Page page, string inputId, string val)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "setInputValue", "<script>var $=layui.$;$('#"+ inputId + "').val('" + val+ "');layui.form.render();</script>");
    }

    public static void SetRadioChecked(this System.Web.UI.Page page, string inputName, string val,bool selected)
    {
        if (selected)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "setInputValue_"+val, "<script>var $=layui.$;$('input[name=" + inputName + "][value=\"" + val + "\"]').attr('checked',true);layui.form.render();</script>");
        }
        else
        {

            page.ClientScript.RegisterStartupScript(page.GetType(), "setInputValue_" + val, "<script>var $=layui.$;$('input[name=" + inputName + "][value=\"" + val + "\"]').attr('checked',false);layui.form.render();</script>");
        }
    }
    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="path">文件路径</param>
    /// <returns></returns>
    public static string SaveFile(this System.Web.UI.WebControls.FileUpload file,string path)
    {
        if (file.HasFile)
        {
            FileInfo info = new FileInfo(file.FileName);
            string fileName =Guid.NewGuid() + info.Extension;
            file.SaveAs(Path.Combine(path, fileName));
            return fileName;
        }
        else
        {
            return string.Empty;
        }
    }
}
