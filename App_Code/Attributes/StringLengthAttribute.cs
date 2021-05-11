using System;
/// <summary>
/// 字符串长度属性
/// </summary>

    public class StringLengthAttribute : Attribute
    {
        /// <summary>
        /// 长度大小
        /// </summary>
        public int Length { get; private set; }

        public StringLengthAttribute(int length)
        {
            Length = length;
        }
    }
