using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// StuAsk 的摘要说明
/// </summary>
public class StuAsk
{
    public StuAsk()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    [Key(Identity =true)]
    public int id { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    [Required]
    [StringLength(50)]
    public string stu_code { get; set; }
    [Required]
    [StringLength(100)]
    public string title { get; set; }
    /// <summary>
    /// 问题分类
    /// </summary>
    [Required]
    [StringLength(100)]
    public string catagory { get; set; }
    /// <summary>
    /// 问题内容
    /// </summary>
    [Required]
    public string content { get; set; }
    /// <summary>
    /// 照片路径
    /// </summary>
    [StringLength(500)]
    public string imageUrl { get; set; }
    /// <summary>
    /// 提问时间
    /// </summary>
    public DateTime ask_date { get; set; }
    /// <summary>
    /// 状态:打开、关闭
    /// </summary>
    [Required]
    [StringLength(20)]
    public string ask_state { get; set; }

}
