using MhwWebsite.Services.CaseStorage;
using System.Text.Json.Serialization;

namespace MhwWebsite.Serialization;

[JsonSerializable(typeof(StoredCase))]
public partial class StoredCaseContext : JsonSerializerContext
{
}