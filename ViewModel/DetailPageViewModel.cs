using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingListApp.Services;

namespace ShoppingListApp.ViewModel;

[QueryProperty(nameof(ShoppingList),"ShoppingList")]
public partial class DetailPageViewModel : ObservableObject
{
   /// <summary>
   /// Constructor for this page
   /// </summary>
   /// <param name="creator">Creator instance for creating ViewModels</param>
   public DetailPageViewModel(ShoppingListViewModelCreator creator, ShoppingListCreator shoppingListCreator,ShoppingListClientService httpShoppingListClientService)
   {
      _creator = creator;
      _shoppingListCreator = shoppingListCreator;
      _httpShoppingListClientService = httpShoppingListClientService;
   }

   /// <summary>
   /// Backing field for <see cref="ShoppingList"/>
   /// </summary>
   private ShoppingListViewModel _shoppingList;
   
   /// <summary>
   /// Backing field for <see cref="NewArticle"/>
   /// </summary>
   private string _newArticle;

   /// <summary>
   /// Service class for creating ViewModels
   /// </summary>
   private readonly ShoppingListViewModelCreator _creator;

   /// <summary>
   /// Creator class for Shopping List Model objects from ViewModels
   /// </summary>
   private readonly ShoppingListCreator _shoppingListCreator;

   /// <summary>
   /// Http client service for accessing api
   /// </summary>
   private readonly ShoppingListClientService _httpShoppingListClientService;

   /// <summary>
   /// Shopping list object corresponding to this detail page
   /// </summary>
   public ShoppingListViewModel ShoppingList
   {
      get => _shoppingList;
      set => SetProperty(ref _shoppingList, value);
   }

   /// <summary>
   /// New article string
   /// </summary>
   public string NewArticle
   {
      get => _newArticle;
      set => SetProperty(ref _newArticle, value);
   }

   /// <summary>
   /// Command for adding a new <see cref="ArticleViewModel"/> to the articles of the corresponding <see cref="ShoppingList"/>
   /// </summary>
   /// <param name="newArticleName"></param>
   [RelayCommand]
   public void AddNewArticle(string newArticleName)
   {
      ShoppingList.Articles.Add(_creator.CreateArticleViewModel(newArticleName));
   }

   /// <summary>
   /// Set the <see cref="ArticleViewModel"/> as checked for strike-through in UI
   /// </summary>
   /// <param name="article"><see cref="ArticleViewModel"/> to set checked</param>
   [RelayCommand]
   public void SetArticleUsed(ArticleViewModel article)
   {
      article.IsChecked = !article.IsChecked;
   }

   [RelayCommand]
   public async void UpdateShoppingList()
   {
      await _httpShoppingListClientService.UpdateShoppingList(_shoppingListCreator.CreateShoppingList(ShoppingList));
   }

   [RelayCommand]
   public void DeleteArticle(ArticleViewModel viewModelToDelete)
   {
      ShoppingList.Articles.Remove(viewModelToDelete);
   }
}