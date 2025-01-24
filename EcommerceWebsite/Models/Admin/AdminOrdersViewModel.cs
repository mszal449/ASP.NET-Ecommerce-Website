using EcommerceWebsite.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EcommerceWebsite.Models.Admin
{
    public class AdminOrdersViewModel
    {
        public List<Entities.Order> Orders { get; set; } = [];
        public OrderState? State { get; set; }
        public List<SelectListItem> OrderStates { get; set; } = [];

        public static List<SelectListItem> GetOrderStates(OrderState? selectedState)
        {
            var states = new List<SelectListItem>
            {
                new SelectListItem { Value = OrderState.Open.ToString(), Text = "Open" },
                new SelectListItem { Value = OrderState.Placed.ToString(), Text = "Placed" }
            };

            if (!selectedState.HasValue) return states;
            var selectedItem = states.Find(s => s.Value == selectedState.ToString());
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }

            return states;
        }
    }
}