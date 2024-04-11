using System.Text.Json.Serialization;

namespace TV2.Backend.ClassLibrary.Classes;

public class Metadata
{
    [JsonPropertyName("keywords")]
    public List<Keyword>? Keywords { get; set; }

    /*
    [JsonPropertyName("topics")]
    public List<string>? Topics { get; set; }
    */
}

public class Keyword
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("text")]
    public string Text { get; set; }
    
    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }
    
    [JsonPropertyName("language")]
    public string Language { get; set; }
    
    [JsonPropertyName("instances")]
    public List<Instance> Instances { get; set; }
}

public class Instance
{
    [JsonPropertyName("start")]
    public string Start { get; set; }
    
    [JsonPropertyName("end")]
    public string End { get; set; }
}