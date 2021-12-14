/*
   1. Sukurti Konsolinę Applikaciją, kuri atspausdina visas nuotraukų nuorodas paimtas iš RestAPI
   https://jsonplaceholder.typicode.com/ kurios priklauso 'Mrs. Dennis Schulist'. Patarimas, naudokite HttpClient klasę.
   Extra: programoje panaudokite Linq Selectus.
   */

using ApiUserGalleryLinks.Models;
using Newtonsoft.Json;
using System.Linq;

var httpClient = new HttpClient();

var httpResponse = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

// get user id
if (httpResponse.IsSuccessStatusCode)
{
    var contentString = await httpResponse.Content.ReadAsStringAsync();

    var users = JsonConvert.DeserializeObject<List<User>>(contentString);
    
    var userID = users.Where(u => u.Name == "Mrs. Dennis Schulist").FirstOrDefault();
}
