using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EcommerceWebsite.Models.Product;

public enum SortByOption
{
    NameAsc,
    NameDesc,
    PriceAsc,
    PriceDesc,
}

public enum StockOption
{
    All,
    InStock,
    OutOfStock,
}

public class ProductFilterModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public SortByOption SortBy { get; set; }
    public StockOption Stock { get; set; }

    public static List<SelectListItem> GetSortOptions()
    {
        return
        [
            new SelectListItem { Value = SortByOption.NameAsc.ToString(), Text = "Name Ascending" },
            new SelectListItem { Value = SortByOption.NameDesc.ToString(), Text = "Name Descending" },
            new SelectListItem { Value = SortByOption.PriceAsc.ToString(), Text = "Price Ascending" },
            new SelectListItem { Value = SortByOption.PriceDesc.ToString(), Text = "Price Descending" }
        ];
    }

    public static List<SelectListItem> GetStockOptions()
    {
        return
        [
            new SelectListItem { Value = StockOption.All.ToString(), Text = "All" },
            new SelectListItem { Value = StockOption.InStock.ToString(), Text = "In Stock" },
            new SelectListItem { Value = StockOption.OutOfStock.ToString(), Text = "Out of Stock" }
        ];
    }
}