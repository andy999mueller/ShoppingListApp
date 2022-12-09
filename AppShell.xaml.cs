using ShoppingListApp.View;

namespace ShoppingListApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
      Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
   }
}
