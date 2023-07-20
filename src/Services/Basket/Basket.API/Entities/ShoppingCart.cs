using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string username { get; set; }
        public List<ShoppingCartItem> items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
            
            
        }

        public ShoppingCart(string username)
        {
            this.username = username;
        }   

        //propiedad del precio total, que se calcula con el precio de la lista del shopping cart x los precios
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach(var item in items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }

    }
}
