using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AdminInfo 的摘要说明
/// </summary>
public class AdminInfo
{
    public AdminInfo()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 账号
    /// </summary>
    [Key]
    [Required]
    [StringLength(50)]
    public string code { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    [Required]
    [StringLength(20)]
    public string name { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    [StringLength(50)]
    public string pwd { get; set; }
    /// <summary>
    /// 联系方式
    /// </summary>
    [StringLength(50)]
    public string lxfs { get; set; }
    /// <summary>
    /// 照片路径
    /// </summary>
    [StringLength(500)]
    public string photoUrl { get; set; }
}
