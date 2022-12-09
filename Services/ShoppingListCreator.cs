using ShoppingListApp.Model;
using ShoppingListApp.ViewModel;

namespace ShoppingListApp.Services;

/// <summary>
/// This class creates instances of <see cref="ShoppingList"/> from given <see cref="ShoppingListViewModel"/>.
/// </summary>
public class ShoppingListCreator
{
   /// <summary>
   /// Create a new "empty" shopping list object with the given name
   /// </summary>
   /// <param name="shoppingListViewModel">View-Model of one shopping list</param>
   /// <returns>New instance of a shopping list</returns>
   public ShoppingList CreateShoppingList(ShoppingListViewModel shoppingListViewModel)
   {
      var newShoppingList = new ShoppingList()
      {
         Identifier = shoppingListViewModel.ShoppingList.Identifier,
         Description = shoppingListViewModel.ShoppingList.Description,
      };
      if (shoppingListViewModel.Articles == null) return newShoppingList;
      var articles = shoppingListViewModel.Articles.Select(articleViewModel => articleViewModel.Article).ToList();
      newShoppingList.Articles = articles;
      return newShoppingList;
   }
}