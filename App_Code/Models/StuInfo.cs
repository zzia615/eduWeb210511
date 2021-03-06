using System;

public class StuInfo
{
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
    /// 年级
    /// </summary>
    [StringLength(50)]
    public string grade { get; set; }
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
    /// <summary>
    /// 学生需求
    /// </summary>
    [StringLength(2000)]
    public string needs { get; set; }
    /// <summary>
    /// 学生需求发布日期
    /// </summary>
    public DateTime? pubDate { get; set; }
}