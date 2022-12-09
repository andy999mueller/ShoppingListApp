using ShoppingListApp.ViewModel;

namespace ShoppingListApp;

public partial class MainPage : ContentPage
{
   public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
      BindingContext = vm;
      Loaded += MainPage_Loaded;
   }

   private void MainPage_Loaded(object sender, EventArgs e)
   {
      if (BindingContext is MainPageViewModel vm)
      {
         vm.LoadShoppingListsCommand.ExecuteAsync(null);
      }
   }
}

