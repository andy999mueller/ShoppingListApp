using ShoppingListApp.ViewModel;

namespace ShoppingListApp.View;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailPageViewModel vm)
	{
		InitializeComponent();
      BindingContext = vm;
		NavigatedFrom += OnNavigatedFrom;
   }

   private void OnNavigatedFrom(object sender, NavigatedFromEventArgs e)
   {
      if (BindingContext is DetailPageViewModel vm)
      {
        vm.UpdateShoppingListCommand.Execute(null);
      }
   }
}