using System.Collections.ObjectModel;
using ShoppingListApp.Model;
using ShoppingListApp.ViewModel;

namespace ShoppingListApp.Services
{
   /// <summary>
   /// This class creates instances of <see cref="ShoppingListViewModel"/> or <see cref="ArticleViewModel"/> from given <see cref="ShoppingList"/> or <see cref="Article"/>
   /// </summary>
   public class ShoppingListViewModelCreator
   {
      /// <summary>
      /// This method creates a new ShoppingList ViewModel instance from a given <see cref="ShoppingList"/>
      /// </summary>
      /// <param name="shoppingListToCreate">Shopping list instance to create the ViewModel</param>
      /// <returns>new <see cref="ShoppingListViewModel"/> instance</returns>
      public ShoppingListViewModel CreateShoppingListViewModel(ShoppingList shoppingListToCreate)
      {
         var newShoppingList = new ShoppingListViewModel(shoppingListToCreate)
         {
            Description = shoppingListToCreate.Description,
            Identifier = shoppingListToCreate.Identifier
         };
         var viewModels = shoppingListToCreate.Articles.Select(article => new ArticleViewModel(article)).ToList();
         newShoppingList.Articles = new ObservableCollection<ArticleViewModel>(viewModels);
         return newShoppingList;
      }

      /// <summary>
      /// Create a new shopping list view-model from given name
      /// </summary>
      /// <param name="newShoppingListName">Name of the new shopping list</param>
      /// <returns>ViewModel instance of <see cref="ShoppingListViewModel"/></returns>
      public ShoppingListViewModel CreateShoppingListViewModel(string newShoppingListName)
      {
         return new ShoppingListViewModel(new ShoppingList(){Description = newShoppingListName,Articles = new List<Article>(),Identifier = string.Empty})
         {
            Description = newShoppingListName
         };
      }

      /// <summary>
      /// Create a new <see cref="ArticleViewModel"/>
      /// </summary>
      /// <param name="articleName"></param>
      /// <returns></returns>
      public ArticleViewModel CreateArticleViewModel(string articleName)
      {
         return new ArticleViewModel(new Article(){Name = articleName, Identifier = Guid.NewGuid().ToString(), IsChecked = false})
         {
            Name = articleName
         };
      }
   }
}
