using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public enum TypeCategory
    {
        /// <summary>
        /// 商品
        /// </summary>
        [Description("商品")]
        Shopping,
        /// <summary>
        /// 采购
        /// </summary>
        [Description("采购")]
        Purchase
    }
}
