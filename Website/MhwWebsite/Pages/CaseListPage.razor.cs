using MhwWebsite.Services.CaseStorage;

namespace MhwWebsite.Pages;

public partial class CaseListPage
{
    protected override async Task OnParametersSetAsync()
    {
        this.isLoading = true;

        var z = await this.ZhouyiProvider.GetStoreAsync();
        this.cases = this.CaseStore.EnumrateCases()
            .OrderByDescending(c => c.LastEditAuto)
            .Select(c => new DisplayedCase(c, z));
        this.isLoading = false;
    }

    private IEnumerable<DisplayedCase> cases = Enumerable.Empty<DisplayedCase>();
    private bool isLoading;

    private void ViewCase(StoredCase c)
    {
        this.NavigationManager.NavigateTo($"/cases/{c.IdAuto:N}");
    }
}
