using MeihuaWintry.Services.CaseStorage;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace MeihuaWintry.Pages;

public partial class CaseListPage
{
    protected override async Task OnParametersSetAsync()
    {
        isLoading = true;

        var z = await ZhouyiProvider.GetStoreAsync();
        this.cases = CaseStore.EnumrateCases()
            .OrderByDescending(c => c.LastEditAuto)
            .Select(c => new DisplayedCase(c, z));
        isLoading = false;
    }

    private IEnumerable<DisplayedCase> cases = Enumerable.Empty<DisplayedCase>();
    private bool isLoading;

    private void ViewCase(StoredCase c)
    {
        NavigationManager.NavigateTo($"/cases/{c.IdAuto:N}");
    }
}
