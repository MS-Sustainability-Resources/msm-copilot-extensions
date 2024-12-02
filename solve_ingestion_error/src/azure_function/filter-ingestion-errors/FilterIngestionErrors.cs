using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;


namespace mcfs.ingestionerrorservice
{
 public class FilterIngestionErrors
    {
        private readonly ILogger<FilterIngestionErrors> _logger;

        public FilterIngestionErrors(ILogger<FilterIngestionErrors> logger)
        {
            _logger = logger;
        }

        [Function("FilterIngestionErrors")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            //\\\\\\\u0022
            string requestBody;
            try
            {
            requestBody = await new StreamReader(req.Body).ReadToEndAsync();
             //requestBody = requestBody.Replace("\\\\\", "");
            requestBody = requestBody.Replace("\\u0022", "\"");
             _logger.LogInformation(requestBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading or processing request body.");
                return new BadRequestObjectResult("Error reading or processing request body.");
            }
            string IngestionErrorString = requestBody;
            string JsonString;
             try
            {
            JsonString = req.Query["ReferenceDataString"];
            JsonString = JsonString.Replace("\\u0022", "\"");
            Console.WriteLine("JsonString "+ JsonString);
            JsonString = JsonString.Replace("\\n", "");
             }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing query parameters.");
                return new BadRequestObjectResult("Error processing query parameters.");
            }
            //string IngestionErrorString=req.Query["IngestionErrorString"];
            //string JsonString =req.Query["ReferenceDataString"];
           // string IngestionErrorString="[[\"Failure date\",\"Error message\",\"How to fix link\",\"Name\",\"Description\",\"Vehicle Type\",\"Distance\",\"Distance Unit\",\"Fuel Type\",\"Fuel Quantity\",\"Fuel Quantity Unit\",\"Cost\",\"Cost unit\",\"Quantity\",\"Quantity unit\",\"Data Quality Type\",\"Organizational Unit\",\"Facility\",\"Country/region\",\"Industrial Process Type\",\"Transaction date\",\"Consumption start date\",\"Consumption end date\",\"Evidence\",\"Origin correlation ID\"],[\"9/10/2024 3:29:02 AM\",\"InvalidAttributeValue (Vehicle Type)\",\"Sydney Fleet 3\",\"Demo data is for illustration purposes only. No real association is intended or inferred.\",\"Railroad Equipment\",\"420.96157286980002\",\"Km\",\"Motor Gasoline\",\"993.82126987029994\",\"L\",\"\",\"\",\"\",\"\",\"Actual\",\"Contoso Pod Business\",\"Contoso Pod Factory 2\",null,\"\",\"11/1/2020 7:00:00 AM\",\"11/1/2020 7:00:00 AM\",\"11/30/2020 8:00:00 AM\",\"\",\"\"],[\"9/10/2024 3:29:02 AM\",\"InvalidAttributeValue (Vehicle Type)\",\"Green fleet 3\",\"Demo data is for illustration purposes only. No real association is intended or inferred.\",\"Railroad Equipment\",\"259.58020136480002\",\"Km\",\"Motor Gasoline\",\"617.71587458370004\",\"L\",\"\",\"\",\"\",\"\",\"Actual\",\"Contoso Pod Business\",\"Contoso Pod Factory 2\",null,\"\",\"10/1/2022 12:00:00 AM\",\"10/1/2022 12:00:00 AM\",\"10/31/2022 12:00:00 AM\",\"\",\"\"],[\"9/10/2024 3:29:02 AM\",\"InvalidAttributeValue (Fuel Type)\",\"Sydney Fleet 3\",\"Demo data is for illustration purposes only. No real association is intended or inferred.\",\"Airport Equipment-Diesel\",\"494.2\",\"Km\",\"Diesel-Airport\",\"807.14290000000005\",\"L\",\"\",\"\",\"\",\"\",\"Actual\",\"Contoso Pod Business\",\"Contoso Pod Factory 2\",null,\"\",\"3/1/2020 8:00:00 AM\",\"3/1/2020 8:00:00 AM\",\"3/31/2020 7:00:00 AM\",\"\",\"\"]";
          //  string JsonString = "[{\"categoryname\":\"Fuel types\",\"entityname\":\"msdyn_fueltype\",\"errormessage\":\"Invalid value for Fuel Type attribute. Please check if it exists.\"},{\"categoryname\":\"Vehicle types\",\"entityname\":\"msdyn_vehicletype\",\"errormessage\":\"Invalid value for Vehicle Type attribute. Please check if it exists.\"}]";
             List<IngestionErrorRecord> records;
            try
            {
                records = ParseCategoryJsonArray(JsonString, IngestionErrorString);
                _logger.LogInformation(records.Count.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing JSON or processing data.");
                return new BadRequestObjectResult("Error parsing JSON or processing data.");
            }
            return new OkObjectResult(JsonSerializer.Serialize(records));
        }
      /// <summary>
      /// Gets the position of the search string in the first array of the input
      /// </summary>
      /// <param name="input"></param>
      /// <param name="searchString"></param>
        internal static int GetPositionInFirstArray(string input, string searchString)
        {
            try
            {
            // Remove the outer brackets and split the input into lines
            var lines = input.Trim('[', ']').Split(new[] { "],[" }, StringSplitOptions.None);

            // Get the first array and split it into fields
            var firstArray = lines[0].ToLower().Trim('[', ']').Split(new[] { "\\\",\\\"" }, StringSplitOptions.None)
                                     .Select(d => d.Trim('\"').Trim('\\')).ToArray();
            // Find the position of the search string in the first array
            return Array.IndexOf(firstArray, searchString.ToLower());
             }
            catch (Exception ex)
            {
                throw new Exception("Error getting position in first array.", ex);
            }
        }
    /// <summary>
    /// Parses the JSON string and returns a list of IngestionErrorRecord instances
    /// </summary>
    /// <param name="jsonString"></param>
    /// <param name="ingestionError"></param>
    /// <returns></returns>
    internal static List<IngestionErrorRecord> ParseCategoryJsonArray(string jsonString,string ingestionError)
    {
       // var categories = new List<ReferenceData>();
        var records=new List<IngestionErrorRecord>();
         try
            {
        using (JsonDocument doc = JsonDocument.Parse(jsonString))
        {
            
            JsonElement root = doc.RootElement;
            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement element in root.EnumerateArray())
                {
                 
                    var CategoryName = element.GetProperty("categoryname").GetString();
                    var EntityName = element.GetProperty("entityname").GetString();
                    //var ErrorMessage = element.GetProperty("errormessage").GetString();
                    string refTypeSingular=ConvertPluralToSingular(CategoryName);
                    Console.WriteLine("refTypeSingular "+refTypeSingular);
                    int position = GetPositionInFirstArray(ingestionError, refTypeSingular); 
                    Console.WriteLine("position "+position);
                    var record = ParseErrors(ingestionError,refTypeSingular,position,EntityName);          
                  //  categories.Add(category);
                 // Console.WriteLine(record[0].referencetypevalue);
                  records.AddRange(record);
                }
            }
        }
    }
        catch (Exception ex)
        {
            throw new Exception("Error parsing category JSON array.", ex);
        }
        return GetGroupedIngestionErrorRecords(records);
    }
        /// <summary>
        /// Groups the IngestionErrorRecord instances by referencetypevalue and sums the errorCount
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        internal static List<IngestionErrorRecord> GetGroupedIngestionErrorRecords(List<IngestionErrorRecord> records)
        {
        try
            {
            return records
                .GroupBy(r => r.referencetypevalue)
                .Select(g => new IngestionErrorRecord
                {
                    referencetypename = g.First().referencetypename,
                    entityname = g.First().entityname,
                    referencetypevalue = g.Key,
                    errorCount = g.Sum(r => r.errorCount)
                })
                .ToList();
             }
            catch (Exception ex)
            {
                throw new Exception("Error grouping ingestion error records.", ex);
            }
        }
        /// <summary>
        /// Parses the ingestion error string and returns a list of IngestionErrorRecord instances
        /// </summary>
        /// <param name="input"></param>
        /// <param name="refString"></param>
        /// <param name="position"></param>
        /// <param name="entityname"></param>
        /// <returns></returns>
        internal static List<IngestionErrorRecord> ParseErrors(string input,string refString, int position, string entityname)
        {
             // Initialize a list to hold the IngestionErrorRecord instances
            var records = new List<IngestionErrorRecord>();
            try
            {
             // Remove the outer brackets and split the input into lines
            var lines = input.Trim('[', ']').Split(new[] { "],[" }, StringSplitOptions.None);         

            // Iterate over each data line starting from the second line
            for (int i = 1; i < lines.Length; i++)
            {
                Console.WriteLine("lines[i] "+ lines[i]);
                // Split the data line into fields
                var data = lines[i].Trim('[', ']').Split(new[] {"\\\",\\\""}, StringSplitOptions.None)
                                    .Select(d => d.Trim('"')).ToArray();
                Console.WriteLine("errormessage "+ data[1]);
                string dataValue = data[1]?.ToString().ToLower().Trim();
                string refStringValue = refString?.ToLower().Trim();
                if (dataValue.Contains(refStringValue))
                {
                    // Get the value at the specified position
                    // "How to fix" link value is not returned, hence -1 from position
                    Console.WriteLine("position "+ position);
                    string valueAtPosition = data[position-1].Trim('"').Trim('\\');

                    // Create and populate the IngestionErrorRecord instance
                    var record = new IngestionErrorRecord
                    {
                        referencetypename = refString,
                        entityname=ConvertSingularToPlural(entityname),
                        referencetypevalue = valueAtPosition,
                        errorCount=1
                        // Other properties can be set here
                    };
                     Console.WriteLine("valueAtPosition "+ valueAtPosition);
                    // Add the record to the list
                    records.Add(record);
                }
            }
            } 
            catch (Exception ex)
            {
                throw new Exception("Error parsing errors.", ex);
            }
            // Return the array of IngestionErrorRecord instances
            return records;
        }
        /// <summary>
        /// Converts a plural noun to a singular noun
        /// </summary>
        /// <param name="plural"></param>
        /// <returns></returns>
        internal static string ConvertPluralToSingular(string plural)
        {
            try
            {
                if (plural.EndsWith("ies"))
                {
                    return plural.Substring(0, plural.Length - 3) + "y";
                }
                else if (plural.EndsWith("s"))
                {
                    return plural.Substring(0, plural.Length - 1);
                }
                return plural;
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting plural to singular.", ex);
            }
        }
        /// <summary>
        /// Converts a singular noun to a plural noun
        /// </summary>
        /// <param name="singular"></param>
        /// <returns></returns>
        internal static string ConvertSingularToPlural(string singular)
        {
            try
            {
                if (singular.EndsWith("y") && !singular.EndsWith("ay") && !singular.EndsWith("ey") && !singular.EndsWith("iy") && !singular.EndsWith("oy") && !singular.EndsWith("uy"))
                {
                    return singular.Substring(0, singular.Length - 1) + "ies";
                }
                else if (singular.EndsWith("s") || singular.EndsWith("sh") || singular.EndsWith("ch") || singular.EndsWith("x") || singular.EndsWith("z"))
                {
                    return singular + "es";
                }
                else
                {
                    return singular + "s";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting singular to plural.", ex);
            }
        }
    }
}
