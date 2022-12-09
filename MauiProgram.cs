using ShoppingListApp.Services;
using ShoppingListApp.View;
using ShoppingListApp.ViewModel;

namespace ShoppingListApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
      builder.Services.AddSingleton<ShoppingListClientService>();
      builder.Services.AddSingleton<ShoppingListCreator>();
      builder.Services.AddSingleton<ShoppingListViewModelCreator>();
      builder.Services.AddSingleton<MainPage>();
      builder.Services.AddSingleton<MainPageViewModel>();
      builder.Services.AddTransient<DetailPage>();
      builder.Services.AddTransient<DetailPageViewModel>();

      return builder.Build();
	}
}
