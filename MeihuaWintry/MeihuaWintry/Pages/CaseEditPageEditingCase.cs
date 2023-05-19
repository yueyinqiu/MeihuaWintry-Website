using MeihuaWintry.Services.CaseStorage;

namespace MeihuaWintry.Pages;

sealed class CaseEditPageEditingCase
{
    private readonly Case innerCase;
    public CaseEditPageEditingCase(Case c)
    {
        this.innerCase = c;
    }
    public string Name
    {
        get
        {
            return innerCase.CaseName ?? "未命名占例";
        }
        set
        {
            innerCase.CaseName = value;
        }
    }
    public DateTime? WesternTime
    {
        get
        {
            return innerCase.WesternTime;
        }
        set
        {
            innerCase.WesternTime = value;
        }
    }
    public SolarTime? SolarTime
    {
        get
        {
            return innerCase.SolarTime;
        }
        set
        {
            innerCase.SolarTime = value;
        }
    }
    public LunarTime? LunarTime
    {
        get
        {
            return innerCase.LunarTime;
        }
        set
        {
            innerCase.LunarTime = value;
        }
    }
}
