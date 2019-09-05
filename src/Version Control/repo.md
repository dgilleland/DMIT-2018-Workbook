# Code in Your Repository

- Create Issues:
  - Proof-of-concept (reading from the API)
  - Save .gitignore to file - `System.IO.File.WriteAllText(path, text)`
    - `System.IO.File.ReadAllText` vs `.ReadAllLines`
  - Command-line args
    - [ ] List supported templates
    - [ ] Display specific template
    - [ ] Create .gitignore
    - [ ] Interactive mode
    - [ ] Help text

----

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Program
    {
        const string API_BASE = "https://www.gitignore.io/api/";
        const string API_LIST_LINES = API_BASE + "list?format=lines";
        const string API_LIST_JSON = API_BASE + "list?format=json";

        static void Main(string[] args)
        {
            DisplayContent(API_LIST_LINES);
            DisplayContent(API_BASE + "macos,windows,visualstudio");
        }
        static void DisplayContent(string path)
        {
            var client = new HttpClient();
            var response = client.GetAsync(path).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine(content);
            }
        }
    }
}
```
