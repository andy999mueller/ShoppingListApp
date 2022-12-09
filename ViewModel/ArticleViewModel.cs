using CommunityToolkit.Mvvm.ComponentModel;
using ShoppingListApp.Model;

namespace ShoppingListApp.ViewModel;

/// <summary>
/// ViewModel class for one <see cref="Article"/>
/// </summary>
public partial class ArticleViewModel : ObservableObject
{
   public ArticleViewModel(Article article)
   {
      Article = article;
      Name = article.Name;
      IsChecked = article.IsChecked;
   }

   [ObservableProperty]
   public string identifier;

   [ObservableProperty]
   public string name;

   [ObservableProperty]
   public TextDecorations textDecoration;

   /// <summary>
   /// Backing field for a<see cref="IsChecked"/>
   /// </summary>
   private bool _isChecked;

   /// <summary>
   /// Whether this article is checked or not. It will also set <see cref="TextDecoration"/> depending on checked state
   /// </summary>
   public bool IsChecked
   {
      get => _isChecked;
      set
      {
         if (SetProperty(ref _isChecked, value))
         {
            TextDecoration = _isChecked ? TextDecorations.Strikethrough : TextDecorations.None;
            Article.IsChecked = _isChecked;
         }
      }
   }

   /// <summary>
   /// The underlying article
   /// </summary>
   public Article Article { get; }
}