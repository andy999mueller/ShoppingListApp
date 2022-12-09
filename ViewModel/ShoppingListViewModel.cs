using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ShoppingListApp.Model;

namespace ShoppingListApp.ViewModel;

/// <summary>
/// ViewModel for one <see cref="ShoppingList"/>
/// </summary>
public class ShoppingListViewModel : ObservableObject
{
   public ShoppingListViewModel(ShoppingList shoppingList)
   {
      ShoppingList = shoppingList;
      Articles = new ObservableCollection<ArticleViewModel>();
   }

   /// <summary>
   /// Backing field for <see cref="Identifier"/>
   /// </summary>
   private string _identifier;

   /// <summary>
   /// Backing field for <see cref="Description"/>
   /// </summary>
   private string _description;

   /// <summary>
   /// Identifier for this shopping list ViewModel
   /// </summary>
   public string Identifier
   {
      get => _identifier;
      set => SetProperty(ref _identifier, value);
   }

   /// <summary>
   /// Description for this shopping list ViewModel
   /// </summary>
   public string Description
   {
      get => _description;
      set => SetProperty(ref _description, value);
   }

   /// <summary>
   /// Collection with the article ViewModels
   /// </summary>
   public ObservableCollection<ArticleViewModel> Articles { get; set; }

   /// <summary>
   /// The underlying shopping list
   /// </summary>
   public ShoppingList ShoppingList { get; }
}