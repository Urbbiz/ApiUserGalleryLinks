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
    
    var specificUser = users.Where(u => u.Name == "Mrs. Dennis Schulist").FirstOrDefault();

    // get Album Id
    httpResponse = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/albums");

    if (httpResponse.IsSuccessStatusCode)
    {
        contentString = await httpResponse.Content.ReadAsStringAsync();

        var albums = JsonConvert.DeserializeObject<List<Album>>(contentString);

        var specificAlbum = albums.Where(a => a.UserId == specificUser.Id).FirstOrDefault();

        // to get from the photo urls of photos
        httpResponse = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/photos");

        if (httpResponse.IsSuccessStatusCode)
        {
            contentString = await httpResponse.Content.ReadAsStringAsync();

            var photos = JsonConvert.DeserializeObject<List<Photo>>(contentString);

            var specificPhotosUrl = photos.Where(p => p.AlbumId == specificAlbum.UserId);

            foreach(var oneByOneUrl in specificPhotosUrl)
            {
                Console.WriteLine($"User name: {specificUser.Name} album id: {specificAlbum.UserId} url:{oneByOneUrl.Url}");
            }
        }
    }
}


