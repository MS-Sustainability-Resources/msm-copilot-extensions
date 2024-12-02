namespace mcfs.ingestionerrorservice
{
public class ReferenceData
{
    public string CategoryName { get; set; }
    public string EntityName { get; set; }
    public string ErrorMessage { get; set; }
}

public class RequestData
{
    public string IngestionErrorString { get; set; }
    public string ReferenceDataString { get; set; }
}
}