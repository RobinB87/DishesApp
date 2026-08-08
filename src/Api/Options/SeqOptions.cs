using System.ComponentModel.DataAnnotations;

namespace Api.Options;

public class SeqOptions
{
    public const string SectionName = "Seq";

    [Required, Url]
    public string Url { get; set; } = string.Empty;
}
