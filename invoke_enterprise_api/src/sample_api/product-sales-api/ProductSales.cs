using System.Text.Json.Serialization;

public class ProductSales
{
    [JsonPropertyName("productname")]
    public string ProductName { get; set; }

    [JsonPropertyName("salesrevenue")]
    public decimal SalesRevenue { get; set; }

    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("owningteam")]
    public string OwningTeam { get; set; }
}