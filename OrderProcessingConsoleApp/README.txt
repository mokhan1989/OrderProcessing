
---- Running the Console App ----

The console app should run using either standard methods below (Runs on .Net 3.1 or above):

1. Navigating to ~/OrderProcessingConsoleApp/OrderProcessingConsoleApp/bin/Debug/netcoreapp3.1
and running the OrderProcessingConsoleApp.exe file in file explorer or command prompt.

2. Open the the solution in Visual Studio and run debugging from there.

---- Interacting with the App ----

Feel free to change the OrderRequest.json file in ~/OrderProcessingConsoleApp/OrderProcessingConsoleApp/Data
The app should display outputs such as validation or calculations based on what's in the OrderRequest.json file.

---- Future recommendations and limitations

- There is a try catch block I have added in the Program.cs file which should catch MOST exceptions.
There could still be potential to apply validation to the CountryList.json and PartList.json files
as a more graceful alternative to handling these exceptions.

- More detailed invoice
There could be future improvements in the OrderInvoice class to return part number, qauntity, price, 
and sub-total of each item in the order. As the scope was to just return the total incl. VAT I am
just returning the company name and invoice
