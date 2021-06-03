using System;
/// <summary>
/// StuAsk 的摘要说明
/// </summary>
public class StuAskReply
{
    public StuAskReply()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    [Key(Identity = true)]
    public int id { get; set; }

    public int ask_id { get; set; }
    /// <summary>
    /// 账号
    /// </summary>
    [Required]
    [StringLength(50)]
    public string code { get; set; }
    /// <summary>
    /// 角色
    /// </summary>
    [Required]
    [StringLength(20)]
    public string role { get; set; }


    /// <summary>
    /// 解答内容
    /// </summary>
    [Required]
    public string content { get; set; }
    /// <summary>
    /// 照片路径
    /// </summary>
    [StringLength(500)]
    public string imageUrl { get; set; }
    /// <summary>
    /// 解答时间
    /// </summary>
    public DateTime reply_date { get; set; }

}