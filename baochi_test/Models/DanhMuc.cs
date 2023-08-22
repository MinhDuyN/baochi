using System;
using System.Collections.Generic;

namespace baochi_test.Models;

public partial class DanhMuc
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<BaiDang> BaiDangs { get; set; } = new List<BaiDang>();
}
