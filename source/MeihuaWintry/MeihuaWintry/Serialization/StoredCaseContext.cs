using MeihuaWintry.Services.CaseStorage;
using System.Text.Json.Serialization;

namespace MeihuaWintry.Serialization;

[JsonSerializable(typeof(StoredCase))]
public partial class StoredCaseContext : JsonSerializerContext
{
}