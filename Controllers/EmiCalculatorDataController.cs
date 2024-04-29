using EmiCalculator.Interfaces;
using EmiCalculator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmiCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     [Authorize]
    public class EmiCalculatorDataController : ControllerBase
    {
        private readonly IEmiCalculatorDataService _EmiCalculatorDataService;

        private const string spreadsheetId = "1KBDswjeTiT5-q0YbpCFngOLrLz4my87XfKSOXmYy_wc";
        public EmiCalculatorDataController(IEmiCalculatorDataService EmiCalculatorDataService)
        {
            _EmiCalculatorDataService = EmiCalculatorDataService;
        }
        [AllowAnonymous] // Allows anonymous access
        // GET: api/<EmiCalculatorDataController>
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                // Fetches all EMI calculator data asynchronously
                var EmiCalculatorDatas = await FetchEmiCalculatorALLDataAsync();
                return EmiCalculatorDatas;
            }
            catch (Exception ex)
            {
                // Returns an error message in case of exception
                return "Exception Error Occure:"+ ex.Message;
                
            }
        }
        [AllowAnonymous] // Allows anonymous access
        // GET api/<EmiCalculatorDataController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(string id)
        {
            try
            {
                // Fetches  EMI calculator Single Row Data asynchronously
                var EmiCalculatorDatas = await FetchEmiCalculatorSingleRowDataAsync(id);
                return EmiCalculatorDatas;
            }
            catch (Exception ex)
            {
                // Returns an error message in case of exception
                return "Exception Error Occure:" + ex.Message;

            }
        }
        [AllowAnonymous] // Allows anonymous access

        // POST api/<EmiCalculatorDataController>
        [HttpPost]
        public async Task<object> Post([FromBody] EmiCalculatorData EmiCalculatorData)
        {
            try
            {
                // Fetches  EMI calculator Single Row Data asynchronously
                var EmiCalculatorDatas = await PostEmiCalculatorSingleRowDataAsync(EmiCalculatorData);
                return EmiCalculatorDatas;
            }
            catch (Exception ex)
            {
                // Returns an error message in case of exception
                return "Exception Error Occure:" + ex.Message;

            }




        }

        // PUT api/<EmiCalculatorDataController>/5
        [HttpPut("{id}")]
        public EmiCalculatorData Put(int id, [FromBody] EmiCalculatorData value)
        {
            var emp = _EmiCalculatorDataService.UpdateEmiCalculatorData(value);
            return emp;
        }

        // DELETE api/<EmiCalculatorDataController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var isDeleted = _EmiCalculatorDataService.DeleteEmiCalculatorData(id);
            return isDeleted;
        }

        // This method fetches all data from a Google Sheets spreadsheet named "EmiCalculatorData" asynchronously.
        // It returns a list of lists, where each inner list represents a row of data from the spreadsheet.
        private async Task<IList<IList<object>>> FetchEmiCalculatorALLDataAsync()
        {
            // Authenticate and get access to the Google Sheets service
            var service = await loginAsync() as SheetsService; // Cast to SheetsService

            // Define the range of data to retrieve from the spreadsheet
            string range = "EmiCalculatorData!A2:F";

            // Create a request to get values from the specified range
            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Execute the request asynchronously and get the response
            ValueRange response = await request.ExecuteAsync();

            // Extract the values from the response
            IList<IList<object>> values = response.Values;

            // Return the retrieved values
            return values;
        }

        // This method fetches a single row of data from the "EmiCalculatorData" spreadsheet asynchronously based on the provided ID.
        // It returns a list representing the row of data if found, otherwise returns null.
        private async Task<IList<object>> FetchEmiCalculatorSingleRowDataAsync(string id)
        {
            // Authenticate and get access to the Google Sheets service
            var service = await loginAsync() as SheetsService; // Cast to SheetsService

            // Define the spreadsheet ID and the range of data to retrieve
            string spreadsheetId = "1KBDswjeTiT5-q0YbpCFngOLrLz4my87XfKSOXmYy_wc";
            string range = "EmiCalculatorData!A2:F";

            // Create a request to get values from the specified range
            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Execute the request asynchronously and get the response
            ValueRange response = await request.ExecuteAsync();

            // Extract the values from the response
            IList<IList<object>> values = response.Values;

            // If values were retrieved and the list is not empty
            if (values != null && values.Count > 0)
            {
                int rowIndex = -1;
                // Iterate through each row of data
                for (int i = 0; i < values.Count; i++)
                {
                    // Check if the first column of the current row matches the provided ID
                    if (values[i].Count > 0 && values[i][0].ToString() == id.ToString())
                    {
                        // If a match is found, set the rowIndex and return the row of data
                        rowIndex = i + 1;
                        return values[i];
                        break; // Exit the loop since the desired row is found
                    }
                }
            }

            // Return null if the row with the provided ID is not found
            return null;
        }

        private async Task<object> PostEmiCalculatorSingleRowDataAsync(EmiCalculatorData EmiCalculatorFormData)
        {
            // Authenticate and get access to the Google Sheets service
            var service = await loginAsync() as SheetsService; // Cast to SheetsService

            // Define the spreadsheet ID and the range of data to retrieve
            string spreadsheetId = "1KBDswjeTiT5-q0YbpCFngOLrLz4my87XfKSOXmYy_wc";
            string range = "EmiCalculatorData!A:F";

            // Define the range where you want to append the data
            

            // Define the new row data
            IList<object> newRowData = new List<object>() {EmiCalculatorFormData};

            // Create the value range object
            ValueRange valueRange = new ValueRange();
            valueRange.Values = new List<IList<object>> { newRowData };

            // Create the append request
            SpreadsheetsResource.ValuesResource.AppendRequest appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
            appendRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;

            // Execute the request
            AppendValuesResponse appendResponse = appendRequest.Execute();

            // Output the result
            return appendResponse.Updates.UpdatedRows;

           
        }
        // This method handles the authentication process to authorize access to Google Sheets.
        // It returns the SheetsService object after successful authorization.
        private async Task<object> loginAsync()
        {
            // Load the client secret JSON file containing authentication credentials
            UserCredential credential;
            using (var stream = new FileStream("client_secret_1070471107712-fkcclcknkhbcte7u9q0mscf523ea6nsg.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
                // Authorize access using the loaded client secret
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { SheetsService.Scope.Spreadsheets },
                    "user",
                    CancellationToken.None
                );
            }

            // Initialize the Sheets service using the authorized credential
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "My First Project",
            });

            // Return the SheetsService object for further use
            return service;
        }

    }

}
