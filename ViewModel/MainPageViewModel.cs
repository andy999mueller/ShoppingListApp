using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingListApp.Services;
using ShoppingListApp.View;

namespace ShoppingListApp.ViewModel
{
   public partial class MainPageViewModel : ObservableObject
   {
      /// <summary>
      /// Constructor for this main page ViewModel
      /// </summary>
      /// <param name="clientService">Client service for accessing api</param>
      /// <param name="viewModelCreator">Creator class for ViewModels</param>
      /// <param name="shoppingListCreator">Creator class for model classes</param>
      public MainPageViewModel(ShoppingListClientService clientService, ShoppingListViewModelCreator viewModelCreator, ShoppingListCreator shoppingListCreator)
      {
         _clientService = clientService;
         _viewModelCreator = viewModelCreator;
         _shoppingListCreator = shoppingListCreator;
         ShoppingLists = new ObservableCollection<ShoppingListViewModel>();
      }

      /// <summary>
      /// Client http service instance for accessing api
      /// </summary>
      private readonly ShoppingListClientService _clientService;

      /// <summary>
      /// Create the shopping list view-model objects with this instance
      /// </summary>
      private readonly ShoppingListViewModelCreator _viewModelCreator;

      /// <summary>
      /// Create the shopping list objects with this instance
      /// </summary>
      private readonly ShoppingListCreator _shoppingListCreator;

      /// <summary>
      /// Backing field for <see cref="NewShoppingListName"/>
      /// </summary>
      private string _newShoppingListName;

      /// <summary>
      /// Backing field for <see cref="IsRefreshing"/>
      /// </summary>
      private bool _isRefreshing;

      /// <summary>
      /// List containing all the shopping lists
      /// </summary>
      public ObservableCollection<ShoppingListViewModel> ShoppingLists
      {
         get;
         set;
      }

      /// <summary>
      /// Name of the new shopping list to be created
      /// </summary>
      public string NewShoppingListName
      {
         get => _newShoppingListName;
         set => SetProperty(ref _newShoppingListName, value);
      }

      /// <summary>
      /// This property controls the refresh of the shopping lists
      /// </summary>
      public bool IsRefreshing
      {
         get => _isRefreshing;
         set => SetProperty(ref _isRefreshing, value);
      }

      /// <summary>
      /// Create a new shopping list
      /// </summary>
      /// <param name="s">String that represents the name of the new shopping list</param>
      /// <returns>Async task</returns>
      [RelayCommand]
      public async Task CreateShoppingList(string s)
      {
         var newShoppingListViewModel = _viewModelCreator.CreateShoppingListViewModel(s);
         var result = await _clientService.CreateShoppingList(_shoppingListCreator.CreateShoppingList(newShoppingListViewModel));
         ShoppingLists.Add(_viewModelCreator.CreateShoppingListViewModel(result));
      }

      /// <summary>
      /// Load the current shopping lists from api and add the ViewModels to it
      /// </summary>
      /// <returns>Async task</returns>
      [RelayCommand]
      public async Task LoadShoppingLists()
      {
         ShoppingLists.Clear();
         var shoppingListsFromApi = await _clientService.GetShoppingLists();
         var shoppingListViewModels = shoppingListsFromApi.Select(shoppingList => _viewModelCreator.CreateShoppingListViewModel(shoppingList)).ToList();
         foreach (var vm in shoppingListViewModels)
         {
            ShoppingLists.Add(vm);
         }
         IsRefreshing = false;
      }

      /// <summary>
      /// Command that is called when users taps(clicks) on a shopping list ViewModel
      /// </summary>
      /// <param name="shoppingList">The corresponding shopping list that was tapped/clicked on</param>
      /// <returns>Async task</returns>
      [RelayCommand]
      public async Task Tap(ShoppingListViewModel shoppingList)
      {
         //Navigate to the Details page including the current selected shopping list
         var dictionary = new Dictionary<string, object> { { "ShoppingList", shoppingList } };
         await Shell.Current.GoToAsync(nameof(DetailPage), dictionary);
      }

      [RelayCommand]
      public async Task Delete(ShoppingListViewModel shoppingListToDelete)
      {
         var index = ShoppingLists.IndexOf(shoppingListToDelete);
         ShoppingLists.Remove(shoppingListToDelete);
         await _clientService.RemoveShoppingList(index);
      }
   }
}
