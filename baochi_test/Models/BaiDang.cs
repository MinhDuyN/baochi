using System;
using System.Collections.Generic;

namespace baochi_test.Models;

public partial class BaiDang
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? HinhAnh { get; set; }

    public string? NoiDung { get; set; }

    public bool Active { get; set; }

    public bool IsHot { get; set; }

    public int? IdDanhMuc { get; set; }

    public virtual DanhMuc? IdDanhMucNavigation { get; set; }
}
