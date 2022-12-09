namespace ShoppingListApp.Model;

public class ShoppingList
{
   public string Identifier
   {
      get; 
      set;
   }

   public string Description
   {
      get; 
      set;
   }

   public IList<Article> Articles
   {
      get;
      set;
   } = new List<Article>();
}