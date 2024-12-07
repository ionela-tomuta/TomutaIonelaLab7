using TomutaIonelaLab7.Models;

namespace TomutaIonelaLab7
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            slist.Date = DateTime.UtcNow;
            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
            {
                BindingContext = new Product()
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (ShopList)BindingContext;
            productsListView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }
        async void OnDeleteItemClicked(object sender, EventArgs e)
        {
            var selectedProduct = productsListView.SelectedItem as Product;
            if (selectedProduct != null)
            {
                var shopList = (ShopList)BindingContext;
                await App.Database.DeleteListProductAsync(shopList.ID, selectedProduct.ID);
                // Reîmprospãtãm lista
                productsListView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
            }
            else
            {
                await DisplayAlert("Error", "Please select an item to delete", "OK");
            }
        }
    }
}