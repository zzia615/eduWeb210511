using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TeaInfo 的摘要说明
/// </summary>
public class TeacherInfo
{
    public TeacherInfo()
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
    /// 性别
    /// </summary>
    [StringLength(10)]
    public string sex { get; set; }
    /// <summary>
    /// 年龄
    /// </summary>
    public int? age { get; set; }
    /// <summary>
    /// 联系方式
    /// </summary>
    [StringLength(50)]
    public string lxfs { get; set; }
    /// <summary>
    /// 科目
    /// </summary>
    [StringLength(50)]
    public string kemu { get; set; }
    /// <summary>
    /// 照片路径
    /// </summary>
    [StringLength(500)]
    public string photoUrl { get; set; }
}


public class TeacherORDER
{
    public TeacherORDER()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 编号
    /// </summary>

    [Key(Identity =true)]
    
    public int id { get; set; }
    /// <summary>
    /// 学员账号
    /// </summary>
    [Required]
    [StringLength(50)]
    public string stu_code { get; set; }

    /// <summary>
    /// 预约状态：已预约、已完成
    /// </summary>
    [Required]
    [StringLength(20)]
    public string yyzt { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
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
    /// 性别
    /// </summary>
    [StringLength(10)]
    public string sex { get; set; }
    /// <summary>
    /// 年龄
    /// </summary>
    public int? age { get; set; }
    /// <summary>
    /// 联系方式
    /// </summary>
    [StringLength(50)]
    public string lxfs { get; set; }
    /// <summary>
    /// 科目
    /// </summary>
    [StringLength(50)]
    public string kemu { get; set; }
    /// <summary>
    /// 照片路径
    /// </summary>
    [StringLength(500)]
    public string photoUrl { get; set; }
}