using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    ///     表示业务操作结果的枚举
    /// </summary>
    [Description("业务操作结果的枚举")]
    public enum ResultType
    {
        [Description("操作成功。")]
        Success,

        [Description("Token不正确")]
        TokenError,

        [Description("操作失败")]
        Failed,

        [Description("操作异常。")]
        Exception,
    }
}
