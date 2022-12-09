using System.Text.Json;
using ServiceStack;
using ShoppingListApp.Model;

namespace ShoppingListApp.Services;

public sealed class ShoppingListClientService
{

   public ShoppingListClientService()
   {
      _postClient = new JsonServiceClient();
      _httpClient = new HttpClient();
   }

   private readonly JsonServiceClient _postClient;

   private readonly HttpClient _httpClient;
   
   private static readonly Uri Url = new("https://www.andy-maichingen.de/ShoppingList/api/purchase");

   /// <summary>
   /// Receive the shopping lists from api
   /// </summary>
   /// <returns></returns>
   public async Task<IEnumerable<ShoppingList>> GetShoppingLists()
   {
      var response = await _httpClient.GetAsync(Url);
      return JsonSerializer.Deserialize<IEnumerable<ShoppingList>>(await response.Content.ReadAsStreamAsync());
   }

   /// <summary>
   /// Create a new shopping list based on given shopping list object
   /// </summary>
   /// <param name="shoppingListToCreate">The shopping list that should be created</param>
   /// <returns></returns>
   public async Task<ShoppingList> CreateShoppingList(ShoppingList shoppingListToCreate)
   {
      ShoppingList newShoppingList = null;
      try
      {
         var jsonString = JsonSerializer.Serialize(shoppingListToCreate);
         newShoppingList =  await _postClient.SendAsync<ShoppingList>("POST", Url.AbsoluteUri, jsonString);
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
      }

      return newShoppingList;
   }

   /// <summary>
   /// Delete the shopping list with the given index
   /// </summary>
   /// <param name="index">Index of the current shopping list</param>
   /// <returns></returns>
   public async Task RemoveShoppingList(int index)
   {
     await _httpClient.DeleteAsync(Url.AbsoluteUri+"/"+index);
   }

   /// <summary>
   /// Update the given shopping list on api
   /// </summary>
   /// <param name="shoppingListToUpdate"></param>
   /// <returns></returns>
   public async Task UpdateShoppingList(ShoppingList shoppingListToUpdate)
   {
      var jsonString = JsonSerializer.Serialize(shoppingListToUpdate);
      await _postClient.SendAsync<ShoppingList>("PUT",Url.AbsoluteUri,jsonString);
   }
}