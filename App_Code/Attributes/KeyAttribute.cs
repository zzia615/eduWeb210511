using System;
/// <summary>
/// 主键属性
/// </summary>

    public class KeyAttribute : Attribute
    {
        /// <summary>
        /// 是否自增长
        /// true自增长 false非自增长
        /// 只限定int类型
        /// </summary>
        public bool Identity { get; set; }
    }
