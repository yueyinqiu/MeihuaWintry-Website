using Microsoft.AspNetCore.Components;
using YiJingFramework.Annotating.Zhouyi.Entities;

namespace MhwWebsite.Pages;

public partial class CaseEditPageHexagram
{
    [Parameter]
    public ZhouyiHexagram? Hexagram { get; set; }
}
