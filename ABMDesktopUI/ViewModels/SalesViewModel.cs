using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABMDesktopUI.Library.Api;
using ABMDesktopUI.Library.Models;
using Caliburn.Micro;

namespace ABMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<ProductModel> _products;
        IProductApi _productApi;

        public SalesViewModel(IProductApi productApi)
        {
            _productApi = productApi;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }
        private async Task LoadProducts()
        {
            var productList = await _productApi.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }
        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            { 
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }


        private BindingList<CartProductModel> _cart = new BindingList<CartProductModel>();

        public BindingList<CartProductModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        private int _productQuantity = 1;

        public int ProductQuantity
        {
            get { return _productQuantity; }
            set
            {
                _productQuantity = value;
                NotifyOfPropertyChange(() => ProductQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }
        public string SubTotal
        {
            get
            {
                decimal subTotal = 0;

                foreach(var product in Cart)
                {
                    subTotal += product.Product.RetailPrice * product.QuantityInCart;
                }
                return subTotal.ToString("C");
            }
        }

        public string Tax
        {
            get
            {
                //TODO . will be calculation
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                //TODO . will be calculation
                return "$0.00";
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                //Make sure something is selected
                //Make sure tere is an product quantity

                if(ProductQuantity > 0 && SelectedProduct?.QuantityInStock >= ProductQuantity)
                {
                    output = true;
                }

                return output;
            }
        }
        public void AddToCart()
        {
            CartProductModel existingProduct = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            
            if(existingProduct != null)
            {
                existingProduct.QuantityInCart += ProductQuantity;
                //Don't trust this, needs change to better refresh DisplayProducts in Cart
                Cart.Remove(existingProduct);
                Cart.Add(existingProduct);
            }

            else
            {
                CartProductModel Product = new CartProductModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ProductQuantity
                };
                Cart.Add(Product);
            }

            SelectedProduct.QuantityInStock -= ProductQuantity;
            ProductQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                //Make sure something is selected

                return output;
            }
        }
        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                //Make sure something is in cart

                return output;
            }
        }
        public void CheckOut()
        {

        }


    }
}
